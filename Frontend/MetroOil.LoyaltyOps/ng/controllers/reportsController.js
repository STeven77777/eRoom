(function () {
    var injectParams = ['$scope', '$rootScope', '$location', '$window', '$timeout', 'ngDialog', 'Utils', '$http'];
    
    var rptViewerController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/Reports';
        $scope.formData = {};
        $scope.formTemplate = {};

        var datatableRpt = null;

        var Initial = function () {
            $.blockUI();
            $scope.firstInitData = true;

            var params = $.param({ Prefix: 'RptViewer' });
            $http({
                method: 'GET',
                url: ApiUrl + '/FillData?' + params
            })
                .then(function successCallback(response) {
                    if (response.data.ResponseCode === 0) {
                        $scope._Object = response.data.Model;
                        $scope._Selects = response.data.Selects;
                    } else {
                        Utils.finalResultNotify(response.data);
                    }
                    $.unblockUI();
                })
        }

        Initial();

        $scope.RptNameChanged = function () {
            $.blockUI();
            $scope.firstInitData = true;

            if ($scope._Object.RptId) {
                var obj = { RptId: $scope._Object.RptId };
                $http({
                    method: 'GET',
                    url: ApiUrl + '/GetReportControl?' + $.param(obj)
                })
                    .then(
                    function successCallback(response) {
                        $scope.formData = {};
                        $scope.formTemplate = response.data;
                        Utils.CloseNotify();
                        $.unblockUI();
                    },
                    function errorCallback(response) {
                        $.unblockUI();
                    }
                );
            }
        }

        var exportFunc = function (e, dt, button, config) {
            var self = this;
            var oldStart = dt.settings()[0]._iDisplayStart;

            dt.one('preXhr', function (e, s, data) {
                // Just this once, load all data from the server...
                data.AllowPaging = false;

                dt.one('preDraw', function (e, settings) {
                    // Call the original action function
                    if (button[0].className.indexOf('buttons-excel') >= 0) {
                        if ($.fn.dataTable.ext.buttons.excelHtml5.available(dt, config)) {
                            $.fn.dataTable.ext.buttons.excelHtml5.action.call(self, e, dt, button, config);
                        }
                        else {
                            $.fn.dataTable.ext.buttons.excelFlash.action.call(self, e, dt, button, config);
                        }
                    }
                    else if (button[0].className.indexOf('buttons-pdf') >= 0) {
                        if ($.fn.dataTable.ext.buttons.pdfHtml5.available(dt, config)) {
                            $.fn.dataTable.ext.buttons.pdfHtml5.action.call(self, e, dt, button, config);
                        }
                        else {
                            $.fn.dataTable.ext.buttons.pdfFlash.action.call(self, e, dt, button, config);
                        }
                    }
                    //else if (button[0].className.indexOf('buttons-print') >= 0) {
                    //    $.fn.dataTable.ext.buttons.print.action(e, dt, button, config);
                    //}

                    dt.one('preXhr', function (e, s, data) {
                        // DataTables thinks the first item displayed is index 0, but we're not drawing that.
                        // Set the property to what it was before exporting.
                        settings._iDisplayStart = oldStart;
                        data.start = oldStart;
                    });

                    // Reload the grid with the original page. Otherwise, API functions like table.cell(this) don't work properly.
                    setTimeout(dt.ajax.reload, 0);

                    // Prevent rendering of the full data to the DOM
                    return false;
                });
            });

            // Requery the server with the new one-time export settings
            dt.ajax.reload();
            //setTimeout(dt.ajax.reload, 0);
        }

        $scope.OnSearch = function () {
            $.blockUI();
            if (!$scope._Object.RptId) {
                Utils.ShowNotify('Please select a Report Name', 'error')
                $.unblockUI();
                return;
            }

            var rptExportName = '';
            for (var i = 0; i < $scope._Selects.RptNames.length; i++) {
                if ($scope._Selects.RptNames[i].Value === $scope._Object.RptId) {
                    rptExportName = $scope._Selects.RptNames[i].Text;
                    break;
                }
            }

            // Convert data before sending to server
            var formData = $scope.formData;
            var formDataCount = $scope.formTemplate.length;
            var filterParameterList = '';
            for (var i = 1; i <= formDataCount; i++) {
                var dataItem = formData[i];
                if (moment.isMoment(dataItem)) {
                    filterParameterList += moment(dataItem).format('YYYY-MM-DD');
                } else if (typeof dataItem === 'undefined') {
                    filterParameterList += '';
                } else {
                    filterParameterList += dataItem;
                }

                if (i !== formDataCount) {
                    filterParameterList += '|';
                }
            }
            
            var obj = { RptId: $scope._Object.RptId };
            $http({
                method: 'GET',
                url: ApiUrl + '/GetReportColumns?' + $.param(obj)
            })
                .then(
                function successCallback(response) {
                    var columns = response.data;
                    var aaColumns = [];

                    if (columns && columns.length > 0) {
                        for (i = 0; i < columns.length; i++) {
                            aaColumns.push({ title: columns[i] });
                        }
                    }
                    else {
                        aaColumns.push({ title: '' });
                    }
                    
                    if (datatableRpt) {
                        datatableRpt.destroy();
                        $('#datatableRpt').empty();
                    }
                    
                    var datatableRptOpts = {
                        ajax: ApiUrl + '/GetReportViewer?RptId=' + $scope._Object.RptId + '&FilterParameterList=' + filterParameterList,
                        destroy: true,
                        dom: 'Blfrtip',
                        serverSide: true,
                        columns: aaColumns,
                        bSort: false,
                        buttons: [
                            {
                                extend: "excel",
                                text: 'Download',
                                action: exportFunc,
                                title: moment(new Date()).format("YYYYMMDD") + '-' + rptExportName,
                                customizeData: function (data) {
                                    for (var i = 0; i < data.body.length; i++) {
                                        for (var j = 0; j < data.body[i].length; j++) {
                                            if (data.body[i][j].length >= 12) {
                                                data.body[i][j] = '\u200C' + data.body[i][j];
                                            }
                                        }
                                    }
                                },
                                orientation: 'landscape'
                            }
                        ],
                        colVis: {
                            "buttonClass": 'display-none'
                        },
                        "autoWidth": false,
                        "preDrawCallback": function (settings) {
                            $.blockUI();
                        },
                        "drawCallback": function (settings) {
                            $.unblockUI();
                        },
                        checkBox: false,
                        "scrollX": true,
                        "ordering": false,
                        paging: true,
                        searching: false,
                        id: 'reportViewer',
                        oLanguage: {
                            sSearchPlaceholder: "Search",
                            sLengthMenu: "Show _MENU_",
                            sSearch: "<span><i class='fa fa-search'></i></span>",
                            oPaginate: {
                                sNext: '<span class="pagination-default"></span><span class="pagination-fa"><i class="fa fa-angle-right" ></i></span>',
                                sPrevious: '<span class="pagination-default"></span><span class="pagination-fa"><i class="fa fa-angle-left" ></i></span>'
                            }
                        },
                        bFilter: false,
                        destroy: true
                    };
                    datatableRpt = $('#datatableRpt').DataTable(datatableRptOpts);
                    $scope.firstInitData = false;
                    $.unblockUI();
                },
                function errorCallback(response) {
                    $.unblockUI();
                }
                );

        }

        $scope.ClearSearch = function () {
            $.blockUI();
            $scope._Object.RptId = '';
            $scope.firstInitData = true;
            $scope.formData = {};
            $scope.formTemplate = {};
            $.unblockUI();
        }
    };
    
    rptViewerController.$inject = injectParams;
    
    angular.module('loyaltyApp').controller('rptViewerController', rptViewerController);
}());