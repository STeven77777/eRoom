(function () {
    var injectParams = ['$scope', '$rootScope', '$location', '$window', '$timeout', 'ngDialog', 'Utils', '$http', 'commonService'];

    var newInvoiceDetailController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http, commonService) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/InvoiceDetails';

        var Initial = function () {
            $.blockUI();
            var params = $.param({ Prefix: 'InvoiceDetailInfoNew' });
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
                url: ApiUrl + '/AddInvoiceDetail'
            }).then(
                function successCallback(response) {
                    if (response.data) {
                        if (response.data.ResponseCode === 0 && response.data.Result) {
                            response.data.ResponseDesc += '. Click on <a href="' + $rootScope.getRootUrl() + "/InvoiceDetails#/" + response.data.Result.InvoiceDetailIDHash + '/Info">' + response.data.Result.InvoiceDetailIDHash + '</a> to view detail.'
                            //$scope._Object = null;// Reset form
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
            window.location.href = $rootScope.getRootUrl() + "/InvoiceDetails#/";
        };
    }

    var invoiceDetailsController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http, commonService) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/InvoiceDetails';

        $scope.firstInitData = true;

        var Initial = function () {
            $.blockUI();
            var params = $.param({ Prefix: 'InvoiceDetailSearch' });
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
                localStorage.setItem('DataTables_BusnLocationSearch', JSON.stringify(data))
            },
            stateLoadCallback: function (settings) {
                return JSON.parse(localStorage.getItem('DataTables_BusnLocationSearch'))
            },
            aoColumnDefs: [
                { aTargets: [0], bSortable: false },
                { "sClass": "text-center", "aTargets": [0, 1, 2, 3, 4, 5, 6, 7, 8] }
            ],
            order: [],
            colVis: {
                "buttonText": '<i class="material-icons">account_circle</i>'
            },
            //serverSide: true,
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
            id: 'customerLst',
            //ajax: '/Admin/MerchantUserList?' + $.param({ Ind: 1 }),
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

        $scope.OnSearch = function () {
            if ($scope._Object && $scope._Object.CreateDate) {
                $scope._Object.CreateDate = moment($scope._Object.CreateDate).format("YYYY/MM/DD HH:mm:ss");
            }
            var $table = $rootScope.tables[$scope.dtOptions.id];
            var uri = ApiUrl + '/InvoiceDetailSearch?' + ($scope._Object ? $.param($scope._Object) : '');
            var value = angular.extend($scope.dtOptions, {
                serverSide: true, ajax: uri, destroy: true, "drawCallback": function (settings) {
                    $scope.$apply();
                    $.unblockUI();
                }
            });            
            $scope.firstInitData = false;

          
        }
        $scope.OnSearch();
        $scope.ClearSearch = function () {
            $scope._Object = {};
        }

        $scope.$on("gotoDetail", function (event, aData) {
            window.location.href = $rootScope.getRootUrl() + "/InvoiceDetails#/" + aData[9] + "/Info";
        });
    }

    var invoiceDetailInfoController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http, commonService) {
        // local declare

        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/InvoiceDetails';

        var Initial = function () {
            $.blockUI();
            var params = $.param({ Prefix: 'InvoiceDetailInfo', InvoiceDetailIDHash: $rootScope.obj.busnLocationNoHash });
            $http({
                method: 'GET',
                url: ApiUrl + '/FillData?' + params
            })
                .then(function successCallback(response) {
                    if (response.data.ResponseCode === 0) {
                        $scope._Object = response.data.Model;
                        $scope._Selects = response.data.Selects;
                        $scope._GeneralInfo = response.data.GeneralInfo;
                        
                    } else {
                        Utils.finalResultNotify(response.data);
                    }
                    $.unblockUI();
                });
        }

        Initial();

        $scope.SaveInfo = function () {
            $.blockUI();
            //if ($scope._Object.Birthday) {
            //    $scope._Object.Birthday = moment($scope._Object.Birthday).format("DD/MM/YYYY HH:mm:ss");
            //}
            if ($scope._Object.Birthday) {
                $scope._Object.Birthday = moment($scope._Object.Birthday);
            }
            $http({
                method: 'PATCH',
                data: $scope._Object,
                url: ApiUrl + '/UpdateInvoiceDetail'
            }).then(
                function successCallback(response) {
                    if (response.data.ResponseCode === 0) {
                        Initial();
                    }
                    Utils.finalResultNotify(response.data);
                },
                function errorCallback(response) {
                    //
                }
                );
        };

        $rootScope.stsHisLst = {
            colVis: {
                "buttonClass": 'display-none'
            },
            //serverSide: true,
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
            id: 'allApplications',
            edit: {
                level: 'scope'
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

        $scope.ShowHistory = function () {
            $scope.modelShowHistory = true;

            var opts = angular.extend($scope.stsHisLst, {
                serverSide: true,
                ajax: ApiUrl + '/AccountLogList?' + $.param({ InvoiceDetailIDHash: $rootScope.obj.busnLocationNoHash }),
                destroy: true
            });
            $scope.$broadcast('updateDataTable', { options: opts });
            $rootScope.tables[$scope.stsHisLst.id].fnAdjustColumnSizing();
        };

        $scope.SaveSts = function () {
            $.blockUI();
            $http({
                method: 'PATCH',
                data: $scope._Object,
                url: ApiUrl + '/BusnLocationStatus'
            }).then(
                function successCallback(response) {
                    if (response.data.ResponseCode === 0) {
                        $scope.modelChangeSts = false;
                        Initial();
                        Utils.finalResultNotify(response.data);
                    } else {
                        Utils.finalResultNotify(response.data, '#ErrorResultChangeSts');
                    }
                },
                function errorCallback(response) {
                    $.unblockUI();
                });

        };
        $scope.BackToList = function () {
            window.location.href = $rootScope.getRootUrl() + "/InvoiceDetails#/";
        };
    }


    var busnLotAddrController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http) {
        // local declare
        var vm = this;

        var ApiUrl = $rootScope.getRootUrl() + '/InvoiceDetails';
        var CommonUrl = $rootScope.getRootUrl() + '/Common';

        $.blockUI();
        var params = $.param({ Prefix: 'BusnLotAddrs', InvoiceDetailIDHash: $rootScope.obj.busnLocationNoHash });
        $http({
            method: 'GET',
            url: ApiUrl + '/FillData?' + params
        })
            .then(function successCallback(response) {
                if (response.data.ResponseCode === 0) {
                    $scope._GeneralInfo = response.data.GeneralInfo;
                } else {
                    Utils.finalResultNotify(response.data);
                }
                $.unblockUI();
            })
            .then(function () {
                if ($scope._GeneralInfo.RoomID) {
                //if (true) {
                    $scope.dtOptions = {
                        stateSave: true,
                        "stateSaveParams": function (settings, data) {
                            data.search.search = "";
                            data.start = 0;
                            data.length = 10;
                            data.order = [];
                        },
                        stateDuration: 0,
                        stateSaveCallback: function (settings, data) {
                            localStorage.setItem('DataTables_BusnLotAddress', JSON.stringify(data))
                        },
                        stateLoadCallback: function (settings) {
                            return JSON.parse(localStorage.getItem('DataTables_BusnLotAddress'))
                        },
                        aoColumnDefs: [
                            { aTargets: [0], bSortable: false },
                            { aTargets: [0, 1, 2, 3, 4, 5, 6, 7, 8], bVisible: true },
                            { aTargets: ['_all'], bVisible: false }
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
                        id: 'allAddresses',
                        //ajax: 'Rooms/GetList',// + $.param({ EntityId: $scope._GeneralInfo.EntityId }),
                        ajax: CommonUrl + '/AddressList?' + $.param({ EntityId: $scope._GeneralInfo.RoomID }),
                       // ajax: CommonUrl + '/AddressList?' + '123',
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
                            },
                            //"sEmptyTable": "No address yet. Click <a href='" + $rootScope.getRootUrl() + "/Members#/" + $rootScope.obj.acctNoHash + "/Address/New'>+ Add</a> above to create a address."
                            "sEmptyTable": "No address yet. Click + above to create a address."
                        }
                    }
                }
            });

        $scope.$on("gotoDetail", function (event, aData) {
            window.location.href = $rootScope.getRootUrl() + "/InvoiceDetails#/" + $rootScope.obj.busnLocationNoHash + "/Address/" + aData[12];
        });

        $scope.gotoAddressNew = function () {
            window.location.href = $rootScope.getRootUrl() + "/InvoiceDetails#/" + $rootScope.obj.busnLocationNoHash + "/Address/New";
        };
    };

    var busnLotAddrNewController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/InvoiceDetails';
        var CommonUrl = $rootScope.getRootUrl() + '/Common';

        var Initial = function () {
            $.blockUI();
            var params = $.param({ Prefix: 'BusnLotAddrNew', InvoiceDetailIDHash: $rootScope.obj.busnLocationNoHash });
            $http({
                method: 'GET',
                url: ApiUrl + '/FillData?' + params
            })
                .then(function successCallback(response) {
                    if (response.data.ResponseCode === 0) {
                        $scope._Object = response.data.Model;
                        $scope._Selects = response.data.Selects;
                        $scope._GeneralInfo = response.data.GeneralInfo;
                        $.unblockUI();
                    } else {
                        Utils.finalResultNotify(response.data);
                    }
                });
        }

        Initial();

        $scope.backToList = function () {
            window.location.href = $rootScope.getRootUrl() + "/InvoiceDetails#/" + $rootScope.obj.busnLocationNoHash + "/Address";
        };

        $scope.CountryChanged = function () {
            var obj = { CountryCd: $scope._Object.CtryCd };
            $http({
                method: 'GET',
                url: CommonUrl + '/WebGetState?' + $.param(obj)
            })
                .then(
                function successCallback(response) {
                    $scope._Selects.States = response.data;
                },
                function errorCallback(response) {
                    //
                }
                );
        }

        $scope.SaveInfo = function () {
            $.blockUI();
            $scope._Object.EntityId = $scope._GeneralInfo.EntityId;
            $http({
                method: 'POST',
                data: $scope._Object,
                url: CommonUrl + '/AddAddress'
            }).then(
                function successCallback(response) {
                    if (response.data.ResponseCode === 0) {
                        //$scope._Object = null;
                        Initial();
                    }
                    Utils.finalResultNotify(response.data);
                },
                function errorCallback(response) {
                    //
                }
                );
        };
    };

    var busnLotAddrDetailController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/InvoiceDetails';
        var CommonUrl = $rootScope.getRootUrl() + '/Common';

        $.blockUI();
        var params = $.param({ Prefix: 'BusnLotAddrD', BusnLocationAddressId: $rootScope.obj.busnLocationAddressId, InvoiceDetailIDHash: $rootScope.obj.busnLocationNoHash });
        $http({
            method: 'GET',
            url: ApiUrl + '/FillData?' + params
        })
            .then(function successCallback(response) {
                if (response.data.ResponseCode === 0) {
                    $scope._Object = response.data.Model;
                    $scope._Selects = response.data.Selects;
                    $scope._GeneralInfo = response.data.GeneralInfo;
                } else {
                    Utils.finalResultNotify(response.data);
                }
                $.unblockUI();
            });

        $scope.CountryChanged = function () {
            var obj = { CountryCd: $scope._Object.CtryCd };
            $http({
                method: 'GET',
                url: CommonUrl + '/WebGetState?' + $.param(obj)
            })
                .then(
                function successCallback(response) {
                    $scope._Selects.States = response.data;
                },
                function errorCallback(response) {
                    //
                }
                );
        }

        $scope.SaveInfo = function () {
            $.blockUI();
            $http({
                method: 'PATCH',
                data: $scope._Object,
                url: CommonUrl + '/UpdateAddress'
            }).then(
                function successCallback(response) {
                    Utils.finalResultNotify(response.data);
                    $.unblockUI();
                },
                function errorCallback(response) {
                    $.unblockUI();
                }
                );
        };

        $scope.backToList = function () {
            window.location.href = $rootScope.getRootUrl() + "/InvoiceDetails#/" + $rootScope.obj.busnLocationNoHash + "/Address";
        };

        $scope.confirmDeleteAddress = function () {
            if ($scope._Object) {
                ngDialog.open({
                    template: 'ConfirmDelete',
                    width: '30%',
                    controller: ['$scope', '$rootScope', '$http', 'commonService', 'Utils', function ($dialogScope, $rootScope, $http, commonService, Utils) {
                        $dialogScope.deleteAddress = function () {
                            $.blockUI();
                            $http({
                                method: 'POST',
                                data: $dialogScope.ngDialogData,
                                url: CommonUrl + '/DeleteAddress'
                            }).then(
                                function successCallback(response) {
                                    ngDialog.close();
                                    Utils.finalResultNotify(response.data);
                                    //window.location.href = $rootScope.getRootUrl() + "/Merchants#/Accounts/" + $rootScope.obj.merchAcctNoHash + "/Address";
                                },
                                function errorCallback(response) {
                                    ngDialog.close();
                                    $.unblockUI();
                                }
                                );
                        }
                    }],
                    className: 'ngdialog-theme-default',
                    data: { Ids: $scope._Object.Ids }
                });
            }
        };
    };


    var busnLotContController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http) {
        // local declare
        var vm = this;

        var ApiUrl = $rootScope.getRootUrl() + '/InvoiceDetails';
        var CommonUrl = $rootScope.getRootUrl() + '/Common';

        $.blockUI();
        var params = $.param({ Prefix: 'BusnLotConts', InvoiceDetailIDHash: $rootScope.obj.busnLocationNoHash });
        $http({
            method: 'GET',
            url: ApiUrl + '/FillData?' + params
        })
            .then(function successCallback(response) {
                if (response.data.ResponseCode === 0) {
                    $scope._GeneralInfo = response.data.GeneralInfo;
                } else {
                    Utils.finalResultNotify(response.data);
                }
                $.unblockUI();
            })
            .then(function () {
                if ($scope._GeneralInfo.EntityId) {
                    $scope.dtOptions = {
                        stateSave: true,
                        "stateSaveParams": function (settings, data) {
                            data.search.search = "";
                            data.start = 0;
                            data.length = 10;
                            data.order = [];
                        },
                        stateDuration: 0,
                        stateSaveCallback: function (settings, data) {
                            localStorage.setItem('DataTables_BusnLotContact', JSON.stringify(data))
                        },
                        stateLoadCallback: function (settings) {
                            return JSON.parse(localStorage.getItem('DataTables_BusnLotContact'))
                        },
                        aoColumnDefs: [
                            { aTargets: [0], bSortable: false },
                            //{ aTargets: [0, 1, 2, 3, 4, 5, 6, 7, 8], bVisible: true },
                            //{ aTargets: ['_all'], bVisible: false }
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
                        id: 'allAddresses',
                        ajax: CommonUrl + '/ContactList?' + $.param({ EntityId: $scope._GeneralInfo.EntityId }),
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
                            },
                            "sEmptyTable": "No contact yet. Click + above to create a contact."
                        }
                    }
                }
            });

        $scope.$on("gotoDetail", function (event, aData) {
            window.location.href = $rootScope.getRootUrl() + "/InvoiceDetails#/" + $rootScope.obj.busnLocationNoHash + "/Contact/" + aData[7];
        });

        $scope.gotoNew = function () {
            window.location.href = $rootScope.getRootUrl() + "/InvoiceDetails#/" + $rootScope.obj.busnLocationNoHash + "/Contact/New";
        };
    };

    var busnLotContNewController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/InvoiceDetails';
        var CommonUrl = $rootScope.getRootUrl() + '/Common';

        var Initial = function () {
            $.blockUI();
            var params = $.param({ Prefix: 'BusnLotContNew', InvoiceDetailIDHash: $rootScope.obj.busnLocationNoHash });
            $http({
                method: 'GET',
                url: ApiUrl + '/FillData?' + params
            })
                .then(function successCallback(response) {
                    if (response.data.ResponseCode === 0) {
                        $scope._Object = response.data.Model;
                        $scope._Selects = response.data.Selects;
                        $scope._GeneralInfo = response.data.GeneralInfo;
                        $.unblockUI();
                    } else {
                        Utils.finalResultNotify(response.data);
                    }
                });
        }

        Initial();

        $scope.backToList = function () {
            window.location.href = $rootScope.getRootUrl() + "/InvoiceDetails#/" + $rootScope.obj.busnLocationNoHash + "/Contact";
        };

        $scope.CountryChanged = function () {
            var obj = { CountryCd: $scope._Object.CtryCd };
            $http({
                method: 'GET',
                url: CommonUrl + '/WebGetState?' + $.param(obj)
            })
                .then(
                function successCallback(response) {
                    $scope._Selects.States = response.data;
                },
                function errorCallback(response) {
                    //
                }
                );
        }

        $scope.SaveInfo = function () {
            $.blockUI();
            $scope._Object.EntityId = $scope._GeneralInfo.EntityId;
            $http({
                method: 'POST',
                data: $scope._Object,
                url: CommonUrl + '/AddContact'
            }).then(
                function successCallback(response) {
                    if (response.data.ResponseCode === 0) {
                        //$scope._Object = null;
                        Initial();
                    }
                    Utils.finalResultNotify(response.data);
                },
                function errorCallback(response) {
                    //
                }
                );
        };
    };

    var busnLotContDetailController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/InvoiceDetails';
        var CommonUrl = $rootScope.getRootUrl() + '/Common';

        $.blockUI();
        var params = $.param({ Prefix: 'BusnLotContD', BusnLocationContactId: $rootScope.obj.busnLocationContactId, InvoiceDetailIDHash: $rootScope.obj.busnLocationNoHash });
        $http({
            method: 'GET',
            url: ApiUrl + '/FillData?' + params
        })
            .then(function successCallback(response) {
                if (response.data.ResponseCode === 0) {
                    $scope._Object = response.data.Model;
                    $scope._Selects = response.data.Selects;
                    $scope._GeneralInfo = response.data.GeneralInfo;
                } else {
                    Utils.finalResultNotify(response.data);
                }
                $.unblockUI();
            });

        $scope.SaveInfo = function () {
            $.blockUI();
            $http({
                method: 'PATCH',
                data: $scope._Object,
                url: CommonUrl + '/UpdateContact'
            }).then(
                function successCallback(response) {
                    Utils.finalResultNotify(response.data);
                    $.unblockUI();
                },
                function errorCallback(response) {
                    $.unblockUI();
                }
                );
        };

        $scope.backToList = function () {
            window.location.href = $rootScope.getRootUrl() + "/InvoiceDetails#/" + $rootScope.obj.busnLocationNoHash + "/Contact";
        };
    };


    var busnLotTerminalsController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http) {
        // local declare
        var vm = this;

        var ApiUrl = $rootScope.getRootUrl() + '/InvoiceDetails';

        $.blockUI();
        var params = $.param({ Prefix: 'BusnLotTerminals', InvoiceDetailIDHash: $rootScope.obj.busnLocationNoHash });
        $http({
            method: 'GET',
            url: ApiUrl + '/FillData?' + params
        })
            .then(function successCallback(response) {
                if (response.data.ResponseCode === 0) {
                    $scope._GeneralInfo = response.data.GeneralInfo;
                } else {
                    Utils.finalResultNotify(response.data);
                }
                $.unblockUI();
            })
            .then(function () {
                if ($scope._GeneralInfo.EntityId) {
                    $scope.dtOptions = {
                        stateSave: true,
                        "stateSaveParams": function (settings, data) {
                            data.search.search = "";
                            data.start = 0;
                            data.length = 10;
                            data.order = [];
                        },
                        stateDuration: 0,
                        stateSaveCallback: function (settings, data) {
                            localStorage.setItem('DataTables_BusnLotTerminal', JSON.stringify(data))
                        },
                        stateLoadCallback: function (settings) {
                            return JSON.parse(localStorage.getItem('DataTables_BusnLotTerminal'))
                        },
                        aoColumnDefs: [
                            { aTargets: [0], bSortable: false },
                            //{ aTargets: [0, 1, 2, 3, 4, 5, 6, 7, 8], bVisible: true },
                            //{ aTargets: ['_all'], bVisible: false }
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
                        id: 'allAddresses',
                        ajax: ApiUrl + '/TerminalList?' + $.param({ InvoiceDetailIDHash: $scope._GeneralInfo.InvoiceDetailIDHash }),
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
                            },
                            "sEmptyTable": "No contact yet. Click + above to create a contact."
                        }
                    }
                }
            });

        $scope.$on("gotoDetail", function (event, aData) {
            window.location.href = $rootScope.getRootUrl() + "/InvoiceDetails#/" + $rootScope.obj.busnLocationNoHash + "/Terminals/" + aData[7];
        });

        $scope.gotoNew = function () {
            window.location.href = $rootScope.getRootUrl() + "/InvoiceDetails#/" + $rootScope.obj.busnLocationNoHash + "/Terminals/New";
        };
    };

    var busnLotTerminalNewController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/InvoiceDetails';
        //var CommonUrl = $rootScope.getRootUrl() + '/Common';

        var Initial = function () {
            $.blockUI();
            var params = $.param({ Prefix: 'BusnLotTerminalNew', InvoiceDetailIDHash: $rootScope.obj.busnLocationNoHash });
            $http({
                method: 'GET',
                url: ApiUrl + '/FillData?' + params
            })
                .then(function successCallback(response) {
                    if (response.data.ResponseCode === 0) {
                        $scope._Object = response.data.Model;
                        $scope._Selects = response.data.Selects;
                        $scope._GeneralInfo = response.data.GeneralInfo;
                        $.unblockUI();
                    } else {
                        Utils.finalResultNotify(response.data);
                    }
                });
        }

        Initial();

        $scope.backToList = function () {
            window.location.href = $rootScope.getRootUrl() + "/InvoiceDetails#/" + $rootScope.obj.busnLocationNoHash + "/Terminals";
        };

        $scope.SaveInfo = function () {
            $.blockUI();
            $scope._Object.EntityId = $scope._GeneralInfo.EntityId;
            $http({
                method: 'POST',
                data: $scope._Object,
                url: ApiUrl + '/AddTerminals'
            }).then(
                function successCallback(response) {
                    if (response.data.ResponseCode === 0) {
                        Initial();
                    }
                    Utils.finalResultNotify(response.data);
                },
                function errorCallback(response) {
                    //
                }
                );
        };
    };

    var busnLotTerminalInfoController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/InvoiceDetails';
        //var CommonUrl = $rootScope.getRootUrl() + '/Common';

        var Initial = function () {
            $.blockUI();
            var params = $.param({ Prefix: 'BusnLotTerminalInfo', TerminalIds: $rootScope.obj.terminalIds, InvoiceDetailIDHash: $rootScope.obj.busnLocationNoHash });
            $http({
                method: 'GET',
                url: ApiUrl + '/FillData?' + params
            })
                .then(function successCallback(response) {
                    if (response.data.ResponseCode === 0) {
                        $scope._Object = response.data.Model;
                        $scope._Selects = response.data.Selects;
                        $scope._GeneralInfo = response.data.GeneralInfo;
                    } else {
                        Utils.finalResultNotify(response.data);
                    }
                    $.unblockUI();
                });
        }

        Initial();

        $scope.SaveInfo = function () {
            $.blockUI();
            $http({
                method: 'PATCH',
                data: $scope._Object,
                url: ApiUrl + '/UpdateTerminal'
            }).then(
                function successCallback(response) {
                    Utils.finalResultNotify(response.data);
                    $.unblockUI();
                },
                function errorCallback(response) {
                    $.unblockUI();
                }
                );
        };

        $rootScope.stsHisLst = {
            colVis: {
                "buttonClass": 'display-none'
            },
            //serverSide: true,
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
            id: 'allApplications',
            edit: {
                level: 'scope'
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

        $scope.ShowHistory = function () {
            $scope.modelShowHistory = true;

            var opts = angular.extend($scope.stsHisLst, {
                serverSide: true,
                ajax: ApiUrl + '/TerminalLogList?' + $.param({ Ids: $rootScope.obj.terminalIds }),
                destroy: true
            });
            $scope.$broadcast('updateDataTable', { options: opts });
            $rootScope.tables[$scope.stsHisLst.id].fnAdjustColumnSizing();
        };

        $scope.SaveSts = function () {
            $.blockUI();
            $http({
                method: 'PATCH',
                data: $scope._Object,
                url: ApiUrl + '/TerminalStatus'
            }).then(
                function successCallback(response) {
                    if (response.data.ResponseCode === 0) {
                        $scope.modelChangeSts = false;
                        Initial();
                        Utils.finalResultNotify(response.data);
                    } else {
                        Utils.finalResultNotify(response.data, '#ErrorResultChangeSts');
                    }
                },
                function errorCallback(response) {
                    $.unblockUI();
                });

        };

        $scope.backToList = function () {
            window.location.href = $rootScope.getRootUrl() + "/InvoiceDetails#/" + $rootScope.obj.busnLocationNoHash + "/Terminals";
        };
    };


    var busnLotTxnPointsController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/InvoiceDetails';
        $scope.modelTxnDetails = false;
        $.blockUI();
        var RederTxnTbl = function () {
            $.blockUI();
            if ($scope._Object.Birthday) {
                $scope._Object.Birthday = moment($scope._Object.Birthday).format("YYYY/MM/DD HH:mm:ss");
            }
            if ($scope._Object.CreateDate) {
                $scope._Object.CreateDate = moment($scope._Object.CreateDate).format("YYYY/MM/DD HH:mm:ss");
            }
            if ($scope._Object.UpdateDate) {
                $scope._Object.UpdateDate = moment($scope._Object.UpdateDate).format("YYYY/MM/DD HH:mm:ss");
            }
            if ($scope._Object.DeleteDate) {
                $scope._Object.DeleteDate = moment($scope._Object.DeleteDate).format("YYYY/MM/DD HH:mm:ss");
            }

            $scope.$broadcast('updateDataTable', {
                options: angular.extend($scope.txnLst, {
                    serverSide: true,
                    ajax: ApiUrl + '/GetTxnByBusinessLocation?' + ($scope._Object ? $.param($scope._Object) : ''),
                    destroy: true,
                    "drawCallback": function (settings) {
                        $scope.$apply();
                        $.unblockUI();
                    }
                })
            })
        }

        var Initial = function () {
            $.blockUI();

            var params = $.param({ Prefix: 'BusnLotTxnPoints', InvoiceDetailIDHash: $rootScope.obj.busnLocationNoHash });
            $http({
                method: 'GET',
                url: ApiUrl + '/FillData?' + params
            })
                .then(function successCallback(response) {
                    if (response.data.ResponseCode === 0) {
                        $scope._Object = response.data.Model;
                        $scope._Selects = response.data.Selects;
                        $scope._GeneralInfo = response.data.GeneralInfo;

                        // Render Txn
                        $scope.txnLst = {
                            searching: false,
                            serverSide: false,
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
                            id: 'txnLst',
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

                        RederTxnTbl();
                    } else {
                        Utils.finalResultNotify(response.data);
                    }
                });
        }

        Initial();

        $scope.OnSearch = function () {
            RederTxnTbl();
        }

        $scope.SelectTxnType = function (txnType) {
            $scope._Object.TxnInd = txnType;
            RederTxnTbl();
        }

        $scope.txnProductLst = {
            colVis: {
                "buttonClass": 'display-none'
            },
            searching: false,
            serverSide: false,
            //ajax: ApiUrl + '/GetMemberTxnByAcctNo?' + ($scope._Object ? $.param($scope._Object) : ''),
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
            id: 'txnProductLst',
            //edit: {
            //    level: 'scope',
            //    func: 'gotoDetail'
            //},
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

        $scope.$on("gotoDetail", function (event, aData) {
            //ShowTxnDetails(aData[1]);
            $.blockUI();
            $scope.modelTxnDetails = true;
            $scope.$apply();
            $scope._TxnDetail = {};

            var params = $.param({ TxnId: aData[1], TxnInd: $scope._Object.TxnInd });
            $http({
                method: 'GET',
                url: ApiUrl + '/GetTxnDetailByBusinessLocation?' + params
            })
                .then(function successCallback(response) {
                    if (response.data.ResponseCode === 0) {
                        $scope._TxnDetail = response.data.Result;
                        if ($scope._TxnDetail.TxnDate) {
                            $scope._TxnDetail.TxnDate = moment($scope._TxnDetail.TxnDate).format("YYYY/MM/DD, HH:mm:ss");
                        }
                        if ($scope._TxnDetail.PrcsDate) {
                            $scope._TxnDetail.PrcsDate = moment($scope._TxnDetail.PrcsDate).format("YYYY/MM/DD, HH:mm:ss");
                        }
                    } else {
                        Utils.finalResultNotify(response.data);
                    }
                    $.unblockUI();
                });

            $.blockUI();
            $scope.$broadcast('updateDataTable', {
                options: angular.extend($scope.txnProductLst, {
                    serverSide: true,
                    ajax: ApiUrl + '/GetTxnProdDetailByBusinessLocation?' + params,
                    destroy: true,
                    "drawCallback": function (settings) {
                        $scope.$apply();
                        $.unblockUI();
                    }
                })
            })
        });
    };

    var busnLotCrdRangeAccptController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/InvoiceDetails';

        var Initial = function () {
            $.blockUI();
            $scope.IsNewForm = false;
            $scope.NewCardRangeSelect = "";

            var params = $.param({ Prefix: 'BusnLotCrdRangeAccpt', InvoiceDetailIDHash: $rootScope.obj.busnLocationNoHash });
            $http({
                method: 'GET',
                url: ApiUrl + '/FillData?' + params
            })
                .then(function successCallback(response) {
                    if (response.data.ResponseCode === 0) {
                        $scope._Object = response.data.Model;
                        $scope._Selects = response.data.Selects;
                        $scope._GeneralInfo = response.data.GeneralInfo;
                        $.unblockUI();
                    } else {
                        Utils.finalResultNotify(response.data);
                        $.unblockUI();
                    }
                });
        }

        Initial();

        $scope.gotoNew = function () {
            $scope.IsNewForm = true;
        }

        $scope.AddCardRange = function () {
            $.blockUI();
            $http({
                method: 'POST',
                data: {
                    InvoiceDetailIDHash: $scope._GeneralInfo.InvoiceDetailIDHash,
                    CardRangeId: $scope.NewCardRangeSelect
                },
                url: ApiUrl + '/AddCardRange'
            }).then(
                function successCallback(response) {
                    if (response.data.ResponseCode === 0) {
                        Initial();
                    }
                    Utils.finalResultNotify(response.data);
                },
                function errorCallback(response) {
                    //
                }
                );
        }

        $scope.ConfirmDeleteCardRange = function (Ids) {
            ngDialog.open({
                template: 'ConfirmDelete',
                width: '30%',
                controller: ['$scope', '$rootScope', '$http', 'Utils', function ($dialogScope, $rootScope, $http, Utils) {
                    $dialogScope.DeleteCardRange = function () {
                        $.blockUI();
                        $http({
                            method: 'POST',
                            data: {
                                Ids: Ids
                            },
                            url: ApiUrl + '/DeleteCardRange'
                        }).then(
                            function successCallback(response) {
                                ngDialog.close();
                                if (response.data.ResponseCode === 0) {
                                    Initial();
                                }
                                Utils.finalResultNotify(response.data);
                            },
                            function errorCallback(response) {
                                ngDialog.close();
                                $.unblockUI();
                            }
                            );
                    }
                }],
                className: 'ngdialog-theme-default'
                //data: { Ids: $scope._Object.Ids }
            });
        };
    };

    newInvoiceDetailController.$inject = injectParams;
    invoiceDetailsController.$inject = injectParams;
    invoiceDetailInfoController.$inject = injectParams;

    busnLotAddrController.$inject = injectParams;
    busnLotAddrNewController.$inject = injectParams;
    busnLotAddrDetailController.$inject = injectParams;

    busnLotContController.$inject = injectParams;
    busnLotContNewController.$inject = injectParams;
    busnLotContDetailController.$inject = injectParams;

    busnLotTerminalsController.$inject = injectParams;
    busnLotTerminalNewController.$inject = injectParams;
    busnLotTerminalInfoController.$inject = injectParams;

    busnLotTxnPointsController.$inject = injectParams;

    busnLotCrdRangeAccptController.$inject = injectParams;
    
    angular.module('loyaltyApp').controller('newInvoiceDetailController', newInvoiceDetailController);
    angular.module('loyaltyApp').controller('invoiceDetailsController', invoiceDetailsController);
    angular.module('loyaltyApp').controller('invoiceDetailInfoController', invoiceDetailInfoController);

    angular.module('loyaltyApp').controller('busnLotAddrController', busnLotAddrController);
    angular.module('loyaltyApp').controller('busnLotAddrNewController', busnLotAddrNewController);
    angular.module('loyaltyApp').controller('busnLotAddrDetailController', busnLotAddrDetailController);

    angular.module('loyaltyApp').controller('busnLotContController', busnLotContController);
    angular.module('loyaltyApp').controller('busnLotContNewController', busnLotContNewController);
    angular.module('loyaltyApp').controller('busnLotContDetailController', busnLotContDetailController);

    angular.module('loyaltyApp').controller('busnLotTerminalsController', busnLotTerminalsController);
    angular.module('loyaltyApp').controller('busnLotTerminalNewController', busnLotTerminalNewController);
    angular.module('loyaltyApp').controller('busnLotTerminalInfoController', busnLotTerminalInfoController);

    angular.module('loyaltyApp').controller('busnLotTxnPointsController', busnLotTxnPointsController);

    angular.module('loyaltyApp').controller('busnLotCrdRangeAccptController', busnLotCrdRangeAccptController);
}());