(function () {
    var injectParams = ['$scope', '$rootScope', '$location', '$window', '$timeout', 'ngDialog', 'Utils', '$http', 'commonService'];

    var newMerchUserController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http, commonService) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/Merchants';

        var Initial = function () {
            $.blockUI();
            var params = $.param({ Prefix: 'MerchUserNew' });
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
                url: ApiUrl + '/NewMerchantUser'
            }).then(
                function successCallback(response) {
                    if (response.data) {
                        if (response.data.ResponseCode === 0 && response.data.Result) {
                            response.data.ResponseDesc += '. Click on <a href="' + $rootScope.getRootUrl() + "/Merchants#/Users/" + response.data.Result.UserIdHash + '">' + response.data.Result.UserId + '</a> to view detail.'
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
            window.location.href = $rootScope.getRootUrl() + "/Merchants#/Users";
        };
    }

    var merchUsersController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http, commonService) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/Merchants';

        $scope.firstInitData = true;

        var Initial = function () {
            $.blockUI();
            var params = $.param({ Prefix: 'MerchUsers' });
            $http({
                method: 'GET',
                url: ApiUrl + '/FillData?' + params
            })
                .then(function successCallback(response) {
                    if (response.data.ResponseCode === 0) {
                        $scope._Object = response.data.Model;
                        $scope._Selects = response.data.Selects;
                        //$scope.$apply();
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
                localStorage.setItem('DataTables_MerchantUser', JSON.stringify(data))
            },
            stateLoadCallback: function (settings) {
                return JSON.parse(localStorage.getItem('DataTables_MerchantUser'))
            },
            aoColumnDefs: [
                { aTargets: [0], bSortable: false }
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
            var $table = $rootScope.tables[$scope.dtOptions.id];
            var uri = ApiUrl + '/MerchantUserList?' + ($scope._Object ? $.param($scope._Object) : '');
            var value = angular.extend($scope.dtOptions, {
                serverSide: true, ajax: uri, destroy: true, "drawCallback": function (settings) {
                    $scope.$apply();
                    $.unblockUI();
                }
            });

            $scope.firstInitData = false;
        }

        $scope.ClearSearch = function () {
            $scope._Object = {};
        }

        $scope.$on("gotoDetail", function (event, aData) {
            window.location.href = $rootScope.getRootUrl() + "/Merchants#/Users/" + aData[11];
        });
    }

    var merchUserInfoController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http, commonService) {
        // local declare

        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/Merchants';
        $.blockUI();
        var params = $.param({ Prefix: 'MerchUserInfo', MerchantUserIdHash: $rootScope.obj.merchUserIdHash });
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
                url: ApiUrl + '/UpdateMerchantUser'
            }).then(
                function successCallback(response) {
                    //if (response.ResponseCode === 0) {
                    //    //$scope._Object = null;
                    //}
                    Utils.finalResultNotify(response.data);
                },
                function errorCallback(response) {
                    //
                }
                );
        };

        $scope.BackToList = function () {
            window.location.href = $rootScope.getRootUrl() + "/Merchants#/Users";
        };

        $scope.ResetPassword = function () {
            $.blockUI();
            $http({
                method: 'POST',
                data: { UserIdHash: $scope._Object.UserIdHash },
                url: ApiUrl + '/UserPassword'
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

    var merchAccountsController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http, commonService) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/Merchants';

        $scope.firstInitData = true;

        var Initial = function () {
            $.blockUI();
            var params = $.param({ Prefix: 'MerchAccts' });
            $http({
                method: 'GET',
                url: ApiUrl + '/FillData?' + params
            })
                .then(function successCallback(response) {
                    if (response.data.ResponseCode === 0) {
                        $scope._Object = response.data.Model;
                        $scope._Selects = response.data.Selects;
                        //$scope.$apply();
                    } else {
                        Utils.finalResultNotify(response.data);
                    }
                    $.unblockUI();
                });
        };

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
                localStorage.setItem('DataTables_MerchantUser', JSON.stringify(data));
            },
            stateLoadCallback: function (settings) {
                return JSON.parse(localStorage.getItem('DataTables_MerchantUser'));
            },
            aoColumnDefs: [
                { aTargets: [0], bSortable: false }
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
            var $table = $rootScope.tables[$scope.dtOptions.id];
            var uri = ApiUrl + '/MerchantAcctList?' + ($scope._Object ? $.param($scope._Object) : '');
            var value = angular.extend($scope.dtOptions, {
                serverSide: true, ajax: uri, destroy: true, "drawCallback": function (settings) {
                    $scope.$apply();
                    $.unblockUI();
                }
            });

            $scope.firstInitData = false;
        };

        $scope.ClearSearch = function () {
            $scope._Object = {};
        };

        $scope.$on("gotoDetail", function (event, aData) {
            var lastIndex = aData.length - 1;
            window.location.href = $rootScope.getRootUrl() + "/Merchants#/Accounts/" + aData[lastIndex] + "/Info";
        });
    };

    var newMerchAcctController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http, commonService) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/Merchants';

        var Initial = function () {
            $.blockUI();
            var params = $.param({ Prefix: 'MerchAcctNew' });
            $http({
                method: 'GET',
                url: ApiUrl + '/FillData?' + params
            })
            .then(function successCallback(result) {
                $scope._Object = result.data.Model;
                $scope._Selects = result.data.Selects;
                $.unblockUI();
            });
        };

        Initial();

        $scope.SaveInfo = function () {
            $.blockUI();

            $http({
                method: 'POST',
                data: $scope._Object,
                url: ApiUrl + '/NewMerchantAcct'
            }).then(
                function successCallback(response) {
                    if (response.data) {
                        if (response.data.ResponseCode === 0 && response.data.Result) {
                            response.data.ResponseDesc += '. Click on <a href="' + $rootScope.getRootUrl() + "/Merchants#/Accounts/" + response.data.Result.MerchAcctNoHash + '/Info">' + response.data.Result.AcctNo + '</a> to view detail.';
                            //$scope._Object = null;// Reset form
                            Initial(); // reset form
                        }
                    }
                    Utils.finalResultNotify(response.data);
                },
                function errorCallback(response) {
                    $.unblockUI();
                });
        };

        $scope.BackToList = function () {
            window.location.href = $rootScope.getRootUrl() + "/Merchants#/Accounts";
        };
    };

    var merchAcctInfoController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http, commonService) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/Merchants';

        var Initial = function () {
            $.blockUI();
            var params = $.param({ Prefix: 'MerchAcctInfo', merchAcctNoHash: $rootScope.obj.merchAcctNoHash });
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
        };

        Initial();

        $rootScope.acctStsHisLst = {
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

            var opts = angular.extend($scope.acctStsHisLst, {
                serverSide: true,
                ajax: ApiUrl + '/AccountLogList?' + $.param({ AcctNoHash: $rootScope.obj.merchAcctNoHash }),
                destroy: true
            });
            $scope.$broadcast('updateDataTable', { options: opts });
            $rootScope.tables[$scope.acctStsHisLst.id].fnAdjustColumnSizing();
        };

        $scope.SaveInfo = function () {
            $.blockUI();
           
            $http({
                method: 'PATCH',
                data: $scope._Object,
                url: ApiUrl + '/UpdateMerchantAcct'
            }).then(
                function successCallback(response) {
                    Utils.finalResultNotify(response.data);
                    Initial();
                },
                function errorCallback(response) {
                    
                });
        };

        $scope.SaveAcctSts = function () {
            $.blockUI();
            $http({
                method: 'PATCH',
                data: $scope._Object,
                url: ApiUrl + '/AccountStatus'
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

        $scope.GoBack = function () {
            $window.history.back();
            // window.location.href = $rootScope.getRootUrl() + "/Merchants#/Accounts";
        };
    };

    var merchAcctAddrController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http) {
        // local declare
        var vm = this;
        
        var ApiUrl = $rootScope.getRootUrl() + '/Merchants';
        var CommonUrl = $rootScope.getRootUrl() + '/Common';

        $.blockUI();
        var params = $.param({ Prefix: 'MerchAcctAddrs', merchAcctNoHash: $rootScope.obj.merchAcctNoHash });
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
                            localStorage.setItem('DataTables_MerchAddress', JSON.stringify(data))
                        },
                        stateLoadCallback: function (settings) {
                            return JSON.parse(localStorage.getItem('DataTables_MerchAddress'))
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
                        ajax: CommonUrl + '/AddressList?' + $.param({ EntityId: $scope._GeneralInfo.EntityId }),
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
            window.location.href = $rootScope.getRootUrl() + "/Merchants#/Accounts/" + $rootScope.obj.merchAcctNoHash + "/Address/" + aData[12];
        });

        $scope.gotoAddressNew = function () {
            window.location.href = $rootScope.getRootUrl() + "/Merchants#/Accounts/" + $rootScope.obj.merchAcctNoHash + "/Address/New";
        };
    };

    var merchAcctAddrNewController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/Merchants';
        var CommonUrl = $rootScope.getRootUrl() + '/Common';

        var Initial = function () {
            $.blockUI();
            var params = $.param({ Prefix: 'MerchAcctAddrNew', merchAcctNoHash: $rootScope.obj.merchAcctNoHash });
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
            window.location.href = $rootScope.getRootUrl() + "/Merchants#/Accounts/" + $rootScope.obj.merchAcctNoHash + "/Address";
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

    var merchAcctAddrDetailController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/Merchants';
        var CommonUrl = $rootScope.getRootUrl() + '/Common';

        $.blockUI();
        var params = $.param({ Prefix: 'MerchAcctAddrD', merchAddressId: $rootScope.obj.merchAddressId, merchAcctNoHash: $rootScope.obj.merchAcctNoHash });
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
            window.location.href = $rootScope.getRootUrl() + "/Merchants#/Accounts/" + $rootScope.obj.merchAcctNoHash + "/Address";
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

    var merchAcctContController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http) {
        // local declare
        var vm = this;

        var ApiUrl = $rootScope.getRootUrl() + '/Merchants';
        var CommonUrl = $rootScope.getRootUrl() + '/Common';

        $.blockUI();
        var params = $.param({ Prefix: 'MerchAcctConts', merchAcctNoHash: $rootScope.obj.merchAcctNoHash });
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
                            localStorage.setItem('DataTables_MerchContact', JSON.stringify(data))
                        },
                        stateLoadCallback: function (settings) {
                            return JSON.parse(localStorage.getItem('DataTables_MerchContact'))
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
            window.location.href = $rootScope.getRootUrl() + "/Merchants#/Accounts/" + $rootScope.obj.merchAcctNoHash + "/Contact/" + aData[7];
        });

        $scope.gotoNew = function () {
            window.location.href = $rootScope.getRootUrl() + "/Merchants#/Accounts/" + $rootScope.obj.merchAcctNoHash + "/Contact/New";
        };
    };

    var merchAcctContNewController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/Merchants';
        var CommonUrl = $rootScope.getRootUrl() + '/Common';

        var Initial = function () {
            $.blockUI();
            var params = $.param({ Prefix: 'MerchAcctContNew', merchAcctNoHash: $rootScope.obj.merchAcctNoHash });
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
            window.location.href = $rootScope.getRootUrl() + "/Merchants#/Accounts/" + $rootScope.obj.merchAcctNoHash + "/Contact";
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

    var merchAcctContDetailController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/Merchants';
        var CommonUrl = $rootScope.getRootUrl() + '/Common';

        $.blockUI();
        var params = $.param({ Prefix: 'MerchAcctContD', merchContactId: $rootScope.obj.merchContactId, merchAcctNoHash: $rootScope.obj.merchAcctNoHash });
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
            window.location.href = $rootScope.getRootUrl() + "/Merchants#/Accounts/" + $rootScope.obj.merchAcctNoHash + "/Contact";
        };
    };

    var merchAcctBusnLotsController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http) {
        // local declare
        var vm = this;

        var ApiUrl = $rootScope.getRootUrl() + '/Merchants';
        var CommonUrl = $rootScope.getRootUrl() + '/Common';

        $.blockUI();
        var params = $.param({ Prefix: 'MerchAcctBusnLocations', merchAcctNoHash: $rootScope.obj.merchAcctNoHash });
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
                            localStorage.setItem('DataTables_MerchBusnLot', JSON.stringify(data))
                        },
                        stateLoadCallback: function (settings) {
                            return JSON.parse(localStorage.getItem('DataTables_MerchBusnLot'))
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
                        ajax: ApiUrl + '/BusnLocationListByMerchAcctNo?' + $.param({ AcctNoHash: $scope._GeneralInfo.MerchAcctNoHash }),
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
                            "sEmptyTable": "No business location yet. Click + above to create a business location."
                        }
                    }
                }
            });

        $scope.$on("gotoDetail", function (event, aData) {
            window.location.href = $rootScope.getRootUrl() + "/BusnLocations#/" + aData[7] + "/Info";
        });

        $scope.gotoNew = function () {
            window.location.href = $rootScope.getRootUrl() + "/BusnLocations#/New";
        };
    };

    newMerchUserController.$inject = injectParams;
    merchUsersController.$inject = injectParams;
    merchUserInfoController.$inject = injectParams;
    
    merchAccountsController.$inject = injectParams;
    newMerchAcctController.$inject = injectParams;
    merchAcctInfoController.$inject = injectParams;

    merchAcctAddrController.$inject = injectParams;
    merchAcctAddrNewController.$inject = injectParams;
    merchAcctAddrDetailController.$inject = injectParams;

    merchAcctContController.$inject = injectParams;
    merchAcctContNewController.$inject = injectParams;
    merchAcctContDetailController.$inject = injectParams;

    merchAcctBusnLotsController.$inject = injectParams;

    angular.module('loyaltyApp').controller('newMerchUserController', newMerchUserController);
    angular.module('loyaltyApp').controller('merchUsersController', merchUsersController);
    angular.module('loyaltyApp').controller('merchUserInfoController', merchUserInfoController);

    angular.module('loyaltyApp').controller('merchAccountsController', merchAccountsController);
    angular.module('loyaltyApp').controller('newMerchAcctController', newMerchAcctController);
    angular.module('loyaltyApp').controller('merchAcctInfoController', merchAcctInfoController);

    angular.module('loyaltyApp').controller('merchAcctAddrController', merchAcctAddrController);
    angular.module('loyaltyApp').controller('merchAcctAddrNewController', merchAcctAddrNewController);
    angular.module('loyaltyApp').controller('merchAcctAddrDetailController', merchAcctAddrDetailController);

    angular.module('loyaltyApp').controller('merchAcctContController', merchAcctContController);
    angular.module('loyaltyApp').controller('merchAcctContNewController', merchAcctContNewController);
    angular.module('loyaltyApp').controller('merchAcctContDetailController', merchAcctContDetailController);

    angular.module('loyaltyApp').controller('merchAcctBusnLotsController', merchAcctBusnLotsController);
}());