(function () {
    var injectParams = ['$scope', '$rootScope', '$location', '$window', '$timeout', 'ngDialog', 'Utils', '$http', 'commonService', '$compile'];
    
    var sysUserNewController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http, commonService) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/Admin';

        var Initial = function () {
            $.blockUI();
            var params = $.param({ Prefix: 'AdSysUserNew' });
            $http({
                method: 'GET',
                url: ApiUrl + '/FillData?' + params
            })
                .then(function successCallback(result) {
                    $scope._Object = result.data.Model;
                    $scope._Selects = result.data.Selects;
                    $.unblockUI();
                });
        }

        Initial();

        $scope.SaveInfo = function () {
            $.blockUI();

            // Validation HpNo and HpCtryCode
            if ($scope._Object.HpNo) {
                if (!$scope._Object.HpCtryCode) {
                    Utils.ShowNotify('Please select Mobile Phone No. Country Code', 'error')
                    $.unblockUI();
                    return;
                }
            }

            $http({
                method: 'POST',
                data: $scope._Object,
                url: ApiUrl + '/SysUserNew'
            }).then(
                function successCallback(response) {
                    if (response.data) {
                        if (response.data.ResponseCode === 0 && response.data.Result) {
                            response.data.ResponseDesc += '. Click on <a href="' + $rootScope.getRootUrl() + "/Admin#/SysUsers/" + response.data.Result.UserIdHash + '">' + response.data.Result.UserId + '</a> to view detail.'
                            Initial(); // reset form
                        }
                    }
                    Utils.finalResultNotify(response.data);
                },
                function errorCallback(response) {
                    //
                    $.unblockUI();
                }
                );
        };

        $scope.BackToList = function () {
            window.location.href = $rootScope.getRootUrl() + "/Admin#/SysUsers";
        };
    }

    var sysUsersController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http, commonService) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/Admin';

        $scope.firstInitData = true;

        $scope.dtOptions = {
            searchDelay: 2000,
            stateSave: true,
            "stateSaveParams": function (settings, data) {
                data.search.search = "";
                data.start = 0;
                data.length = 10;
                data.order = [];
            },
            stateDuration: 0,
            stateSaveCallback: function (settings, data) {
                localStorage.setItem('DataTables_SystemUser', JSON.stringify(data))
            },
            stateLoadCallback: function (settings) {
                return JSON.parse(localStorage.getItem('DataTables_SystemUser'))
            },
            aoColumnDefs: [
                { aTargets: [0], bSortable: false }
            ],
            order: [],
            colVis: {
                "buttonText": '<i class="material-icons">account_circle</i>'
            },
            serverSide: true,
            "autoWidth": false,
            "preDrawCallback": function (settings) {
                $.blockUI();
            },
            "drawCallback": function (settings) {
                $.unblockUI();
            },
            checkBox: false,
            "scrollX": true,
            "ordering": true,
            id: 'sysUserLst',
            ajax: ApiUrl + '/SysUserList',
            edit: {
                level: 'scope',
                func: 'gotoDetail'
            },
            oLanguage: {
                sSearchPlaceholder: "Search",
                sLengthMenu: "Show _MENU_",
                sSearch: "<span><i class='fa fa-search'></i></span>",
                oPaginate: {
                    sNext: '<span class="pagination-default"></span><span class="pagination-fa"><i class="fa fa-angle-right" ></i></span>',
                    sPrevious: '<span class="pagination-default"></span><span class="pagination-fa"><i class="fa fa-angle-left" ></i></span>'
                }
            }
        };

        $scope.GotoNew = function () {
            window.location.href = $rootScope.getRootUrl() + "/Admin#/SysUsers/New";
        }

        $scope.$on("gotoDetail", function (event, aData) {
            window.location.href = $rootScope.getRootUrl() + "/Admin#/SysUsers/" + aData[10];
        });
    }

    var sysUserInfoController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http, commonService) {
        // local declare

        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/Admin';
        $.blockUI();
        var params = $.param({ Prefix: 'AdSysUserInfo', UserIdHash: $rootScope.obj.userIdHash });
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
            });

        $scope.SaveInfo = function () {
            $.blockUI();

            // Validation HpNo and HpCtryCode
            if ($scope._Object.HpNo) {
                if (!$scope._Object.HpCtryCode) {
                    Utils.ShowNotify('Please select Mobile Phone No. Country Code', 'error')
                    $.unblockUI();
                    return;
                }
            }

            $http({
                method: 'PATCH',
                data: $scope._Object,
                url: ApiUrl + '/UpdateSysUser'
            }).then(
                function successCallback(response) {
                    if (response.ResponseCode === 0) {
                        $scope._Object = null;
                    }
                    Utils.finalResultNotify(response.data);
                },
                function errorCallback(response) {
                    //
                }
                );
        };

        $scope.BackToList = function () {
            window.location.href = $rootScope.getRootUrl() + "/Admin#/SysUsers";
        };

        $scope.ResetPassword = function () {
            $.blockUI();
            $http({
                method: 'POST',
                data: { UserIdHash: $scope._Object.UserIdHash },
                url: ApiUrl + '/SysUserPassword'
            }).then(
                function successCallback(response) {
                    //if (response.ResponseCode === 0) {
                    //    $scope._Object = null;
                    //}
                    Utils.finalResultNotify(response.data);
                },
                function errorCallback(response) {
                    //
                }
                );
        }
    }

    var sysUserAccessMgmtController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http, commonService) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/Admin';

        $scope.firstInitData = true;

        $scope.dtOptions = {
            searchDelay: 2000,
            stateSave: true,
            "stateSaveParams": function (settings, data) {
                data.search.search = "";
                data.start = 0;
                data.length = 10;
                data.order = [];
            },
            stateDuration: 0,
            stateSaveCallback: function (settings, data) {
                localStorage.setItem('DataTables_UserAccessMgmt', JSON.stringify(data))
            },
            stateLoadCallback: function (settings) {
                return JSON.parse(localStorage.getItem('DataTables_UserAccessMgmt'))
            },
            aoColumnDefs: [
                { aTargets: [0], bSortable: false }
            ],
            order: [],
            colVis: {
                "buttonText": '<i class="material-icons">account_circle</i>'
            },
            serverSide: true,
            "autoWidth": false,
            "preDrawCallback": function (settings) {
                $.blockUI();
            },
            "drawCallback": function (settings) {
                $.unblockUI();
            },
            checkBox: false,
            "scrollX": true,
            "ordering": true,
            id: 'sysUserLst',
            ajax: ApiUrl + '/GetUserAccessMgmt',
            edit: {
                level: 'scope',
                func: 'gotoDetail'
            },
            oLanguage: {
                sSearchPlaceholder: "Search",
                sLengthMenu: "Show _MENU_",
                sSearch: "<span><i class='fa fa-search'></i></span>",
                oPaginate: {
                    sNext: '<span class="pagination-default"></span><span class="pagination-fa"><i class="fa fa-angle-right" ></i></span>',
                    sPrevious: '<span class="pagination-default"></span><span class="pagination-fa"><i class="fa fa-angle-left" ></i></span>'
                }
            }
        };

        $scope.AddNewUserRoleAccess = function () {
            window.location.href = $rootScope.getRootUrl() + "/Admin#/UserAccessManagement/NewUserRoleAccess";
        }

        $scope.$on("gotoDetail", function (event, aData) {
            window.location.href = $rootScope.getRootUrl() + "/Admin#/UserAccessManagement/" + aData[1];
        });
    }

    var sysAddUserAccessMgmtController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http) {
        var vm = this;
        $rootScope.TreeMatrix = [];
        var ApiUrl = $rootScope.getRootUrl() + '/Admin';

        var Initial = function () {
            $.blockUI();
            var params = $.param({ Prefix: 'AdUserRoleAccNew' });
            $http({
                method: 'GET',
                url: ApiUrl + '/FillData?' + params
            })
                .then(function successCallback(result) {
                    $scope._Object = result.data.Model;
                    $rootScope.TreeMatrix = result.data.UserMatrix;
                    $.unblockUI();
                });
        }

        Initial();

        var SetAllElementTreeSts = function (treeMatrix, sts) {
            for (var i in treeMatrix) {
                console.log(treeMatrix[i].Descp)
                treeMatrix[i].GroupStatus = sts;
                if (treeMatrix[i].SubPages) {
                    SetAllElementTreeSts(treeMatrix[i].SubPages, sts);
                }
            }
        }

        $scope.SellectAll = function () {
            $('input:checkbox').not(this).prop('checked', true);
            SetAllElementTreeSts($rootScope.TreeMatrix, true);
        }

        $scope.ClearAll = function () {
            $('input:checkbox').not(this).prop('checked', false);
            SetAllElementTreeSts($rootScope.TreeMatrix, false);
        }

        $scope.ResetMatrix = function () {
            $('input:checkbox').not(this).prop('checked', false);
            SetAllElementTreeSts($rootScope.TreeMatrix, false);
        };

        $scope.SaveInfoUserMatrix = function () {
            $scope.UserMatrixAccess = {
                UserGroupCd: $scope._Object.UserRoleNo,
                UserGroupName: $scope._Object.UserRoleName,
                Sts: 'A',
                MatrixTree: $rootScope.TreeMatrix
            };
            
            // call server site
            $http({
                method: 'PATCH',
                data: $scope.UserMatrixAccess,
                url: ApiUrl + '/SaveUserMatrix'
            }).then(
          function successCallback(response) {
              if (response.data.ResponseCode === 0) {
                  Initial();
              }
              Utils.finalResultNotify(response.data);
          },
          function errorCallback(response) {

          }
          );


        };
        $scope.BackToList = function () {
            window.location.href = $rootScope.getRootUrl() + "/Admin#/UserAccessManagement";
        };
    }

    var sysUserAccessManagementInfo = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http, commonService) {
        var vm = this;
        $rootScope.TreeMatrix = [];
        var ApiUrl = $rootScope.getRootUrl() + '/Admin';

        var Initial = function () {
            $.blockUI();
            var params = $.param({ Prefix: 'AdUserRoleAccInfo', UserIdHash: $rootScope.obj.userIdHash });
            $http({
                method: 'GET',
                url: ApiUrl + '/FillData?' + params
            })
                .then(function successCallback(response) {
                    if (response.data.ResponseCode === 0) {
                        $scope._Object = response.data.Model;
                        $rootScope.TreeMatrix = response.data.UserMatrix;
                        $scope._Selects = response.data.Selects
                    } else {
                        Utils.finalResultNotify(response.data);
                    }
                    $.unblockUI();
                });
        }

        Initial();

        var SetAllElementTreeSts = function (treeMatrix, sts) {
            for (var i in treeMatrix) {
                console.log(treeMatrix[i].Descp)
                treeMatrix[i].GroupStatus = sts;
                if (treeMatrix[i].SubPages) {
                    SetAllElementTreeSts(treeMatrix[i].SubPages, sts);
                }
            }
        }

        $scope.SellectAll = function () {
            $('input:checkbox').not(this).prop('checked', true); 
            SetAllElementTreeSts($rootScope.TreeMatrix, true);
        }

        $scope.ClearAll = function () {
            $('input:checkbox').not(this).prop('checked', false);
            SetAllElementTreeSts($rootScope.TreeMatrix, false);
        }
        
        $scope.ResetMatrix = function () {
            $.blockUI();
            var params = $.param({ Prefix: 'AdUserRoleAccInfo', UserIdHash: $rootScope.obj.userIdHash });
            $http({
                method: 'GET',
                url: ApiUrl + '/FillData?' + params
            })
                .then(function successCallback(response) {
                    if (response.data.ResponseCode === 0) {
                        $rootScope.TreeMatrix = response.data.UserMatrix;
                    } else {
                        Utils.finalResultNotify(response.data);
                    }
                    $.unblockUI();
                });
        };

        $scope.SaveUserRoleAccessInfo = function () {
            $.blockUI();

            if (!$scope._Object.Sts) {
                    Utils.ShowNotify('Please select the status of user role access', 'error')
                    $.unblockUI();
                    return;
            }
            
            $scope.UserMatrixAccess = {
                UserGroupCd: $scope._Object.UserRoleNo,
                UserGroupName: $scope._Object.UserRoleName,
                Sts: $scope._Object.Sts,
                MatrixTree: $rootScope.TreeMatrix
            };

            // call server site
            $http({
                method: 'PATCH',
                data: $scope.UserMatrixAccess,
                url: ApiUrl + '/SaveUserMatrix'
            }).then(
                function successCallback(response) {
                    Utils.finalResultNotify(response.data);
                },
                function errorCallback(response) {

                }
                );
        };

        $scope.BackToList = function () {
            window.location.href = $rootScope.getRootUrl() + "/Admin#/UserAccessManagement";
        };
    }

    var rewardItemsController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http, commonService) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/Admin';

        $scope.firstInitData = true;

        $scope.dtOptions = {
            searchDelay: 2000,
            stateSave: true,
            "stateSaveParams": function (settings, data) {
                data.search.search = "";
                data.start = 0;
                data.length = 10;
                data.order = [];
            },
            stateDuration: 0,
            stateSaveCallback: function (settings, data) {
                localStorage.setItem('DataTables_AdRwdItms', JSON.stringify(data))
            },
            stateLoadCallback: function (settings) {
                return JSON.parse(localStorage.getItem('DataTables_AdRwdItms'))
            },
            aoColumnDefs: [
                { aTargets: [0], bSortable: false }
            ],
            order: [],
            colVis: {
                "buttonText": '<i class="material-icons">account_circle</i>'
            },
            serverSide: true,
            "autoWidth": false,
            "preDrawCallback": function (settings) {
                $.blockUI();
            },
            "drawCallback": function (settings) {
                $.unblockUI();
            },
            checkBox: false,
            "scrollX": true,
            "ordering": true,
            id: 'AdRwdItms',
            ajax: ApiUrl + '/RewardItemList',
            edit: {
                level: 'scope',
                func: 'gotoDetail'
            },
            oLanguage: {
                sSearchPlaceholder: "Search",
                sLengthMenu: "Show _MENU_",
                sSearch: "<span><i class='fa fa-search'></i></span>",
                oPaginate: {
                    sNext: '<span class="pagination-default"></span><span class="pagination-fa"><i class="fa fa-angle-right" ></i></span>',
                    sPrevious: '<span class="pagination-default"></span><span class="pagination-fa"><i class="fa fa-angle-left" ></i></span>'
                }
            }
        };

        $scope.GotoNew = function () {
            window.location.href = $rootScope.getRootUrl() + "/Admin#/AdRwdItms/New";
        }

        $scope.$on("gotoDetail", function (event, aData) {
            window.location.href = $rootScope.getRootUrl() + "/Admin#/AdRwdItms/" + aData[3];
        });
    }

    var rewardItemNewController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http, commonService) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/Admin';
        var CommonUrl = $rootScope.getRootUrl() + '/Common';

        $scope.RemarkTinymceOptions = {
            height: 150,
            toolbar: "undo redo bold italic underline bullist",
            menubar: false,
            statusbar: false
        };

        var Initial = function () {
            $.blockUI();
            var params = $.param({ Prefix: 'AdRwdItmNew' });
            $http({
                method: 'GET',
                url: ApiUrl + '/FillData?' + params
            })
                .then(function successCallback(result) {
                    $scope._Object = result.data.Model;
                    $scope._Selects = result.data.Selects;
                    $.unblockUI();
                });
        }

        Initial();

        $scope.SaveInfo = function () {
            $.blockUI();

            $http({
                method: 'POST',
                data: $scope._Object,
                url: ApiUrl + '/RewardItemNew'
            }).then(
                function successCallback(response) {
                    if (response.data) {
                        if (response.data.ResponseCode === 0 && response.data.Result) {
                            response.data.ResponseDesc += '. Click on <a href="' + $rootScope.getRootUrl() + "/Admin#/AdRwdItms/" + $scope._Object.ProdCd + '">' + $scope._Object.ProdCd + '</a> to view detail.'
                            //Initial(); // reset form
                            $scope._Object = {};
                        }
                    }
                    Utils.finalResultNotify(response.data);
                },
                function errorCallback(response) {
                    //
                    $.unblockUI();
                }
                );
        };

        $scope.ProductClassChanged = function () {
            var obj = { ProdClassCd: $scope._Object.ProdClassCd };
            $http({
                method: 'GET',
                url: CommonUrl + '/GetProductTypes?' + $.param(obj)
            })
                .then(
                function successCallback(response) {
                    $scope._Selects.ProdTypes = response.data;
                },
                function errorCallback(response) {
                    //
                }
                );
        }

        $scope.BackToList = function () {
            window.location.href = $rootScope.getRootUrl() + "/Admin#/AdRwdItms";
        };
    }

    var rewardItemInfoController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http, commonService) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/Admin';
        var CommonUrl = $rootScope.getRootUrl() + '/Common';

        $scope.RemarkTinymceOptions = {
            height: 150,
            toolbar: "undo redo bold italic underline bullist",
            menubar: false,
            statusbar: false
        };

        var Initial = function () {
            $.blockUI();
            var params = $.param({ Prefix: 'AdRwdItmInfo', ProdCd: $rootScope.obj.rewardItemId });
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
                });
        }

        Initial();

        $scope.SaveInfo = function () {
            $.blockUI();

            if ($scope._Object.StartDate) {
                $scope._Object.StartDate = moment($scope._Object.StartDate);
            }
            if ($scope._Object.EndDate) {
                $scope._Object.EndDate = moment($scope._Object.EndDate);
            }
            if ($scope._Object.VoucherValidStartDate) {
                $scope._Object.VoucherValidStartDate = moment($scope._Object.VoucherValidStartDate);
            }
            if ($scope._Object.VoucherValidEndDate) {
                $scope._Object.VoucherValidEndDate = moment($scope._Object.VoucherValidEndDate);
            }

            $http({
                method: 'PATCH',
                data: $scope._Object,
                url: ApiUrl + '/UpdateRewardItem'
            }).then(
                function successCallback(response) {
                    if (response.data.ResponseCode === 0) {
                        Initial(); // reset form
                    }
                    Utils.finalResultNotify(response.data);
                },
                function errorCallback(response) {
                    //
                }
                );
        };

        $scope.ProductClassChanged = function () {
            var obj = { ProdClassCd: $scope._Object.ProdClassCd };
            $http({
                method: 'GET',
                url: CommonUrl + '/GetProductTypes?' + $.param(obj)
            })
                .then(
                function successCallback(response) {
                    $scope._Selects.ProdTypes = response.data;
                },
                function errorCallback(response) {
                    //
                }
                );
        }

        $scope.BackToList = function () {
            window.location.href = $rootScope.getRootUrl() + "/Admin#/AdRwdItms";
        };
    }

    var qrCodeCardsController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http, commonService, $compile) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/Admin';
        $scope._Object = {};

        $scope.firstInitData = true;

        $scope.dtOptions = {
            searchDelay: 2000,
            stateSave: true,
            "stateSaveParams": function (settings, data) {
                data.search.search = "";
                data.start = 0;
                data.length = 10;
                data.order = [];
            },
            stateDuration: 0,
            stateSaveCallback: function (settings, data) {
                localStorage.setItem('DataTables_AdQRCodeCards', JSON.stringify(data))
            },
            stateLoadCallback: function (settings) {
                return JSON.parse(localStorage.getItem('DataTables_AdQRCodeCards'))
            },
            order: [],
            colVis: {
                "buttonText": '<i class="material-icons">account_circle</i>'
            },
            serverSide: false,
            "autoWidth": false,
            "preDrawCallback": function (settings) {
                $.blockUI();
            },
            "drawCallback": function (row) {
                $.unblockUI();
            },
            createdRow: function (row) {
                if (!row.compiled) {
                    $compile(angular.element(row))($scope);
                    row.compiled = true;
                }
            },
            checkBox: false,
            "scrollX": true,
            "ordering": true,
            id: 'AdRwdItms',
            //ajax: ApiUrl + '/QRBatchList',
            edit: {
                level: 'scope'
                //func: 'gotoDetail'
            },
            oLanguage: {
                sSearchPlaceholder: "Search",
                sLengthMenu: "Show _MENU_",
                sSearch: "<span><i class='fa fa-search'></i></span>",
                oPaginate: {
                    sNext: '<span class="pagination-default"></span><span class="pagination-fa"><i class="fa fa-angle-right" ></i></span>',
                    sPrevious: '<span class="pagination-default"></span><span class="pagination-fa"><i class="fa fa-angle-left" ></i></span>'
                }
            },
            aoColumnDefs: [
                { aTargets: [0], bSortable: false },
                {
                targets: 6,
                    render: function (data, type, row) {
                        if (row[7] === 'P') {
                            return "<a ng-click='OnDownloadBatch(\"" + row[1] + "\");' href='javascript:;'>Download File</a>";
                        } else {
                            return "<span>Download File</span>";
                        }
                }
            }]
        };

        var Initial = function () {
            $scope._Object = {};
            var opts = angular.extend($scope.dtOptions, {
                serverSide: true,
                ajax: ApiUrl + '/QRBatchList',
                destroy: true,
                "drawCallback": function (settings) {
                    $scope.$apply();
                    $.unblockUI();
                }
            });
            $scope.$broadcast('updateDataTable', { options: opts });
        }

        Initial();

        $scope.OnGenerateQRCode = function () {
            $.blockUI();

            if (!$scope._Object.Qty) {
                if (!$scope._Object.Qty) {
                    Utils.finalResultNotify({ ResponseCode: '1', ResponseDesc: 'Please input Quantity'}, '#ErrorGenerateQRCode');
                    $.unblockUI();
                    return;
                }
            }

            $http({
                method: 'POST',
                data: $scope._Object,
                url: ApiUrl + '/QRBatchGenerate'
            }).then(
                function successCallback(response) {
                    if (response.data.ResponseCode === 0) {
                        $scope.modelGenerateQRCode = false;
                        Initial();
                        Utils.finalResultNotify(response.data);
                    } else {
                        Utils.finalResultNotify(response.data, '#ErrorResultChangeSts');
                    }
                },
                function errorCallback(response) {
                    $.unblockUI();
                }
            );
        }

        $scope.OnDownloadBatch = function (batchId) {
            $.blockUI();
            $http({
                method: 'GET',
                url: ApiUrl + '/QRBatchDownload?batchId=' + batchId
            })
                .then(function successCallback(result) {
                    let csvContent = "data:text/csv;charset=utf-8,";
                    result.data.forEach(function (rowArray) {
                        csvContent += rowArray.Token + "\r\n";
                    }); 
                    var encodedUri = encodeURI(csvContent);
                    var link = document.createElement("a");
                    link.setAttribute("href", encodedUri);
                    link.setAttribute("download", moment(new Date()).format("YYYYMMDD") + '-' + "QRCodeBatch-" + batchId + ".csv");
                    document.body.appendChild(link); 
                    link.click();

                    $.unblockUI();
                },
                function errorCallback(response) {
                    $.unblockUI();
                });
        }
    }

    sysUserNewController.$inject = injectParams;
    sysUsersController.$inject = injectParams;
    sysUserInfoController.$inject = injectParams;
    sysUserAccessMgmtController.$inject = injectParams;
    sysAddUserAccessMgmtController.$inject = injectParams;
    sysUserAccessManagementInfo.$inject = injectParams;
    rewardItemsController.$inject = injectParams;
    rewardItemNewController.$inject = injectParams;
    rewardItemInfoController.$inject = injectParams;
    qrCodeCardsController.$inject = injectParams;

    angular.module('loyaltyApp').controller('sysUserNewController', sysUserNewController);
    angular.module('loyaltyApp').controller('sysUserAccessMgmtController', sysUserAccessMgmtController);
    angular.module('loyaltyApp').controller('sysAddUserAccessMgmtController', sysAddUserAccessMgmtController);
    angular.module('loyaltyApp').controller('sysUsersController', sysUsersController);
    angular.module('loyaltyApp').controller('sysUserInfoController', sysUserInfoController);
    angular.module('loyaltyApp').controller('sysUserAccessManagementInfo', sysUserAccessManagementInfo);
    angular.module('loyaltyApp').controller('rewardItemsController', rewardItemsController);
    angular.module('loyaltyApp').controller('rewardItemNewController', rewardItemNewController);
    angular.module('loyaltyApp').controller('rewardItemInfoController', rewardItemInfoController);
    angular.module('loyaltyApp').controller('qrCodeCardsController', qrCodeCardsController);
}());