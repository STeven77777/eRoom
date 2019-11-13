(function () {
    var injectParams = ['$scope', '$rootScope', '$location', '$window', '$timeout', 'ngDialog', 'Utils', '$http', 'commonService'];

    var membersController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http) {
        // local declare
        var ApiUrl = $rootScope.getRootUrl() + '/Members';
        var vm = this;
        $scope.firstInitData = true;
        $scope.searchInputError = true;

        var Initial = function () {
            $.blockUI();
            var params = $.param({ Prefix: 'MemSearch' });
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
        
        $scope.MMFromChanged = function () {
            $scope._Selects.DDFroms = Utils.getDaysByMonth($scope._Object.MMFrom);
        }

        $scope.MMToChanged = function () {
            $scope._Selects.DDTos = Utils.getDaysByMonth($scope._Object.MMTo);
        }

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
                localStorage.setItem('DataTables_MemberSearch', JSON.stringify(data))
            },
            stateLoadCallback: function (settings) {
                return JSON.parse(localStorage.getItem('DataTables_MemberSearch'))
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
            //ajax: '/Members/AccountList?' + $.param({ Ind: 1 }),
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
            $scope.firstInitData = false;
            // Validation input
            if (!$scope._Object || 
                (!$scope._Object.AcctNo
                && !$scope._Object.FullName
                && !$scope._Object.CardNo
                && (!$scope._Object.SelectedAcctStses || !$scope._Object.SelectedAcctStses.length)
                && !$scope._Object.JoinDateFrom
                && !$scope._Object.JoinDateTo
                && !$scope._Object.DOBDateFrom
                && !$scope._Object.DOBDateTo
                && !$scope._Object.HpNo
                && !$scope._Object.EmailAddress
                && !$scope._Object.City
                && (!$scope._Object.SelectedStates || !$scope._Object.SelectedStates.length)
                && !$scope._Object.MMFrom
                && !$scope._Object.DDFrom
                && !$scope._Object.MMTo
                && !$scope._Object.DDTo)
            ) {
                $scope.searchInputError = true;
                return;
            } else {
                $scope.searchInputError = false;
            }

            var $table = $rootScope.tables[$scope.dtOptions.id];

            if ($scope._Object.JoinDateFrom) {
                $scope._Object.JoinDateFrom = moment($scope._Object.JoinDateFrom).format("YYYY/MM/DD HH:mm");
            }
            if ($scope._Object.JoinDateTo) {
                $scope._Object.JoinDateTo = moment($scope._Object.JoinDateTo).format("YYYY/MM/DD HH:mm");
            }
            if ($scope._Object.DOBDateFrom) {
                $scope._Object.DOBDateFrom = moment($scope._Object.DOBDateFrom).format("YYYY/MM/DD");
            }
            if ($scope._Object.DOBDateTo) {
                $scope._Object.DOBDateTo = moment($scope._Object.DOBDateTo).format("YYYY/MM/DD");
            }

            var uri = ApiUrl + '/AccountList?' + ($scope._Object ? $.param($scope._Object) : '');
            //var value = angular.extend($scope.dtOptions, {
            //    serverSide: true,
            //    ajax: uri,
            //    destroy: true,
            //    //"drawCallback": function (settings) {
            //    //    $scope.$apply();
            //    //    $.unblockUI();
            //    //}
            //});
            $scope.$broadcast('updateDataTable', {
                options: angular.extend($scope.dtOptions, {
                    serverSide: true,
                    ajax: uri,
                    destroy: true,
                    //"drawCallback": function (settings) {
                    //    $scope.$apply();
                    //    $.unblockUI();
                    //}
                }) });

        }

        $scope.ClearSearch = function () {
            //$scope._Object = null;
            $scope._Object.AcctNo = null;
            $scope._Object.FullName = null;
            $scope._Object.CardNo = null;
            $scope._Object.SelectedAcctStses = null;
            $scope._Object.JoinDateFrom = null;
            $scope._Object.JoinDateTo = null;
            $scope._Object.DOBDateFrom = null;
            $scope._Object.DOBDateTo = null;
            $scope._Object.HpNo = null;
            $scope._Object.EmailAddress = null;
            $scope._Object.City = null;
            $scope._Object.SelectedStates = null;
            $scope._Object.MMFrom = null;
            $scope._Object.DDFrom = null;
            $scope._Object.MMTo = null;
            $scope._Object.DDTo = null;
        }

        $scope.$on("gotoDetail", function (event, aData) {
            window.location.href = $rootScope.getRootUrl() +"/Members#/" + aData[7] + "/Info";
        });


        //$scope.changeMinMax = function (modelName, newValue) {
        //    //minDate or maxDate updated. Generate events to update relevant pickers

        //    var values = {
        //        minDate: false,
        //        maxDate: false,
        //    }

        //    if (modelName === '_Object.JoinDateFrom') {
        //        values.minDate = newValue;
        //        $scope.$broadcast('pickerUpdate', ['pickerMinDate', 'pickerMinDateDiv', 'pickerMaxSelector'], values);
        //        values.maxDate = $scope._Object.JoinDateTo;
        //    } else if (modelName === '_Object.JoinDateTo') {
        //        values.maxDate = newValue;
        //        $scope.$broadcast('pickerUpdate', ['pickerMaxDate', 'pickerMaxDateDiv', 'pickerMinSelector'], values);
        //        values.minDate = $scope._Object.JoinDateFrom;
        //    }

        //    //For either min/max update, update the pickers which use both.
        //    $scope.$broadcast('pickerUpdate', ['pickerBothDates', 'pickerBothDatesDiv'], values);
        //}
    };

    var newController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http, commonService) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/Members';

        var Initial = function () {
            $.blockUI();
            //membersService.GetFormData({ Prefix: 'MemNew' })
            //    .then(function successCallback(response) {
            //        if (response.data.ResponseCode === 0) {
            //            $scope._Object = response.data.Model;
            //            $scope._Selects = response.data.Selects;
            //        } else {
            //            Utils.finalResultNotify(response.data);
            //        }
            //        $.unblockUI();
            //        //$scope._Object = result.data.Model;
            //        //$scope._Selects = result.data.Selects;
            //        //$.unblockUI();
            //    });

            var params = $.param({ Prefix: 'MemNew' });
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

        $scope.Save = function () {
            $.blockUI();

            // Validation Gender
            if (!$scope._Object.Gender) {
                Utils.ShowNotify('Please select Gender', 'error')
                $.unblockUI();
                return;
            }

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
                url: ApiUrl + '/CreateMember'
            }).then(
                function successCallback(response) {
                    if (response.data) {
                        if (response.data.ResponseCode === 0 && response.data.Result) {
                            response.data.ResponseDesc += '. Click on <a href="' + $rootScope.getRootUrl() + "/Members#/" + response.data.Result.AcctNoHash + '/Info">' + response.data.Result.AcctNo + '</a> to view detail.'
                            Initial();// Reset form
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
    }

    var summaryController = function ($scope, $rootScope, $location, $window, $timeout) {
        // local declare
        var vm = this;
    }

    var infoController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http, commonService) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/Members';

        var Initial = function () {
            $.blockUI();
            var params = $.param({ Prefix: 'MemInfo', AcctNoHash: $rootScope.obj.acctNoHash });
            $http({
                method: 'GET',
                url: ApiUrl + '/FillData?' + params
            })
                .then(function successCallback(response) {
                    if (response.data.ResponseCode === 0) {
                        $scope._Object = response.data.Model;
                        $scope._Selects = response.data.Selects;
                        $scope._GeneralInfo = response.data.Model;
                    } else {
                        Utils.finalResultNotify(response.data);
                    }
                    $.unblockUI();
                });
        }

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
                //var tbl = $rootScope.tables['#allApplications'];
                //tbl.fnAdjustColumnSizing()
                //$('#allApplications').dataTable().fnAdjustColumnSizing();
                $.unblockUI();
            },
            checkBox: false,
            "scrollX": true,
            "ordering": false,
            id: 'acctStsHisLst',
            //ajax: '/Members/AccountLogList?' + $.param({ AcctNoHash: $rootScope.obj.acctNoHash }),
            edit: {
                level: 'scope',
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
            $rootScope.tables[$scope.acctStsHisLst.id].fnAdjustColumnSizing();
            var opts = angular.extend($scope.acctStsHisLst, {
                serverSide: true,
                ajax: ApiUrl + '/AccountLogList?' + $.param({ AcctNoHash: $rootScope.obj.acctNoHash }),
                destroy: true
            });
            $scope.$broadcast('updateDataTable', { options: opts });
        }

        $scope.SaveInfo = function () {
            $.blockUI();
            if ($scope._Object.Dob) {
                $scope._Object.Dob = moment($scope._Object.Dob);
            }

            $http({
                method: 'PATCH',
                data: $scope._Object,
                url: ApiUrl + '/Account'
            }).then(
                function successCallback(response) {
                    //if (response.data.ResponseCode === 0) {
                    //}
                    Utils.finalResultNotify(response.data);
                },
                function errorCallback(response) {
                    //
                }
                );
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
                }
                );
        }

        $scope.GoBack = function () {
            $window.history.back();
        }
    };

    var contactController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/Members';
        var CommonUrl = $rootScope.getRootUrl() + '/Common';

        var Initial = function () {
            $.blockUI();
            var params = $.param({ Prefix: 'MemCont', AcctNoHash: $rootScope.obj.acctNoHash });
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
                url: CommonUrl + '/ContactForMember'
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

        $scope.ChangeHpNo = function () {
            $.blockUI();
            $http({
                method: 'PATCH',
                data: $scope._Object,
                url: CommonUrl + '/ContactMobileUpdate'
            }).then(
                function successCallback(response) {
                    if (response.data.ResponseCode === 0) {
                        $scope.modelChangeHp = false;
                        Initial();
                        Utils.finalResultNotify(response.data);
                    } else {
                        Utils.finalResultNotify(response.data, '#ErrorResultHpNo');
                    }
                },
                function errorCallback(response) {
                    $.unblockUI();
                }
                );
        }

        $scope.contactChangeHist = {
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
            id: 'contactChangeHist',
            //ajax: '/Common/ContactLogList?' + $.param({ EntityId: $scope._Object.EntityId }),
            edit: {
                level: 'scope',
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
            $rootScope.tables[$scope.contactChangeHist.id].fnAdjustColumnSizing();

            var opts = angular.extend($scope.contactChangeHist, {
                serverSide: true,
                ajax: CommonUrl + '/ContactLogList?' + $.param({ Ids: $scope._Object.Ids }),
                destroy: true,
                "drawCallback": function (settings) {
                    $scope.$apply();
                    $.unblockUI();
                }
            });
            $scope.$broadcast('updateDataTable', { options: opts });
        }

        $scope.GoBack = function () {
            $window.history.back();
        }
    };

    var cardsController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http) {
        // local declare
        var vm = this;

        var ApiUrl = $rootScope.getRootUrl() + '/Members';

        $.blockUI();
        var params = $.param({ Prefix: 'MemCrds', AcctNoHash: $rootScope.obj.acctNoHash });
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
                            localStorage.setItem('DataTables_MemberCards', JSON.stringify(data))
                        },
                        stateLoadCallback: function (settings) {
                            return JSON.parse(localStorage.getItem('DataTables_MemberCards'))
                        },
                        aoColumnDefs: [
                            { aTargets: [0], bSortable: false },
                            { aTargets: [0, 1, 2, 3, 4, 5, 6, 7, 8], bVisible: true },
                            //{ aTargets: [1], bVisible: false },
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
                        ajax: ApiUrl + '/Cards?' + $.param({ AcctNoHash: $scope._GeneralInfo.AcctNoHash }),
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
                    }
                }
            });

        $scope.$on("gotoDetail", function (event, aData) {
            if (aData[5] === 'Active') {
                window.location.href = $rootScope.getRootUrl() + "/Members#/" + $rootScope.obj.acctNoHash + "/Cards/Info";
            } else {
                window.location.href = $rootScope.getRootUrl() + "/Members#/" + $rootScope.obj.acctNoHash + "/Cards/" + aData[0];
            }
        });
    };

    var cardInfoController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/Members';

        var Initial = function () {
            $.blockUI();
            var params = $.param({ Prefix: 'MemCrdInfo', AcctNoHash: $rootScope.obj.acctNoHash });
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

        $scope.OpenConfirmation = function () {
            if ($scope._Object.CardType !== $scope._Object.CurrentCardType) {
                $scope.modelConfirmation = true;
            } else {
                Utils.ShowNotify('Please select a new card type to proceed.', 'error');
            }
        }

        $scope.SaveInfo = function () {
            $.blockUI();
            $http({
                method: 'POST',
                data: $scope._Object,
                url: ApiUrl + '/UpdateCard'
            }).then(
                function successCallback(response) {
                    if (response.data) {
                        if (response.data.ResponseCode === 0 && response.data.Result) {
                            Initial();// Reset form
                            $scope.modelConfirmation = false;
                        }
                    }
                    Utils.finalResultNotify(response.data);
                },
                function errorCallback(response) {
                    $.unblockUI();
                }
                );
        };

        $scope.CancelInfo = function () {
            Initial();
        }
    };

    var cardDetailController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/Members';
        var CommonUrl = $rootScope.getRootUrl() + '/Common';
        
        $.blockUI();
        var params = $.param({ Prefix: 'MemCrdD', CardNo: $rootScope.obj.cardNo, AcctNoHash: $rootScope.obj.acctNoHash });
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

        $scope.backToList = function () {
            window.location.href = $rootScope.getRootUrl() + "/Members#/" + $rootScope.obj.acctNoHash + "/Cards";
        };

        //$scope.SaveInfo = function () {
        //    $.blockUI();
        //    $scope._Object.EntityId = $scope._GeneralInfo.EntityId;
        //    $http({
        //        method: 'POST',
        //        data: $scope._Object,
        //        url: CommonUrl + '/AddContact'
        //    }).then(
        //        function successCallback(response) {
        //            if (response.data.ResponseCode === 0) {
        //                $scope._Object = null;
        //            }
        //            Utils.finalResultNotify(response.data);
        //        },
        //        function errorCallback(response) {
        //            //
        //        }
        //        );
        //};
    };

    var addressController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http) {
        // local declare
        var vm = this;
        
        var ApiUrl = $rootScope.getRootUrl() + '/Members';
        var CommonUrl = $rootScope.getRootUrl() + '/Common';

        $.blockUI();
        var params = $.param({ Prefix: 'MemAddr', AcctNoHash: $rootScope.obj.acctNoHash });
        $http({
            method: 'GET',
            url: ApiUrl + '/FillData?' + params
        })
            .then(function successCallback(response) {
                if (response.data.ResponseCode === 0) {
                    //$scope._Object = response.data.Model;
                    //$scope._Selects = response.data.Selects;
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
                            localStorage.setItem('DataTables_MemberAddress', JSON.stringify(data))
                        },
                        stateLoadCallback: function (settings) {
                            return JSON.parse(localStorage.getItem('DataTables_MemberAddress'))
                        },
                        aoColumnDefs: [
                            { aTargets: [0], bSortable: false },
                            { aTargets: [0, 1, 2, 3, 4, 5, 6, 7, 8], bVisible: true },
                            //{ aTargets: [1], bVisible: false },
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
            window.location.href = $rootScope.getRootUrl() + "/Members#/" + $rootScope.obj.acctNoHash + "/Address/" + aData[12];
        });

        $scope.gotoAddressNew = function () {
            window.location.href = $rootScope.getRootUrl() + "/Members#/" + $rootScope.obj.acctNoHash + "/Address/New";
        };
    };

    var addressDetailController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/Members';
        var CommonUrl = $rootScope.getRootUrl() + '/Common';

        $.blockUI();
        var params = $.param({ Prefix: 'MemAddrD', AddressId: $rootScope.obj.addressId, AcctNoHash: $rootScope.obj.acctNoHash  });
        $http({
            method: 'GET',
            url: ApiUrl + '/FillData?' + params
        })
            .then(function successCallback(response) {
                if (response.data.ResponseCode === 0) {
                    //convertDateStringsToDates(response.data.Model);

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
            window.location.href = $rootScope.getRootUrl() + "/Members#/" + $rootScope.obj.acctNoHash + "/Address";
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
                                    //window.location.href = $rootScope.getRootUrl() + "/Members#/" + $rootScope.obj.acctNoHash + "/Address";
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

    var addressNewController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/Members';
        var CommonUrl = $rootScope.getRootUrl() + '/Common';

        var Initial = function () {
            $.blockUI();
            var params = $.param({ Prefix: 'MemAddrNew', AcctNoHash: $rootScope.obj.acctNoHash });
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
            window.location.href = $rootScope.getRootUrl() + "/Members#/" + $rootScope.obj.acctNoHash + "/Address";
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

    var txnPointsController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/Members';
        $scope.modelTxnDetails = false;
        $.blockUI();
        var RederTxnTbl = function () {
            $.blockUI();
            if ($scope._Object.DateFrom) {
                $scope._Object.DateFrom = moment($scope._Object.DateFrom).format("YYYY/MM/DD HH:mm:ss");
            }
            if ($scope._Object.DateTo) {
                $scope._Object.DateTo = moment($scope._Object.DateTo).format("YYYY/MM/DD HH:mm:ss");
            }

            $scope.$broadcast('updateDataTable', {
                options: angular.extend($scope.txnLst, {
                    serverSide: true,
                    ajax: ApiUrl + '/GetMemberTxnByAcctNo?' + ($scope._Object ? $.param($scope._Object) : ''),
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

            var params = $.param({ Prefix: 'MemTxnPoints', AcctNoHash: $rootScope.obj.acctNoHash });
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
                url: ApiUrl + '/GetMemberTxnByTxnId?' + params
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
                    ajax: ApiUrl + '/GetMemberTxnProductsByTxnId?' + params,
                    destroy: true,
                    "drawCallback": function (settings) {
                        $scope.$apply();
                        $.unblockUI();
                    }
                })
            })
        });
    };

    var txnVouchersController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/Members';
        $scope.modelTxnDetails = false;
        $.blockUI();

        var Initial = function () {
            $.blockUI();

            var params = $.param({ Prefix: 'MemTxnVouchers', AcctNoHash: $rootScope.obj.acctNoHash });
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
                    } else {
                        Utils.finalResultNotify(response.data);
                    }
                });
        }

        Initial();

        $scope.OnSearch = function () {
            if ($scope._Object.VoucherStartDateFrom) {
                $scope._Object.VoucherStartDateFrom = moment($scope._Object.VoucherStartDateFrom).format("YYYY/MM/DD HH:mm:ss");
            }
            if ($scope._Object.VoucherStartDateTo) {
                $scope._Object.VoucherStartDateTo = moment($scope._Object.VoucherStartDateTo).format("YYYY/MM/DD HH:mm:ss");
            }

            if ($scope._Object.VoucherExpiredDateFrom) {
                $scope._Object.VoucherExpiredDateFrom = moment($scope._Object.VoucherExpiredDateFrom).format("YYYY/MM/DD HH:mm:ss");
            }
            if ($scope._Object.VoucherExpiredDateTo) {
                $scope._Object.VoucherExpiredDateTo = moment($scope._Object.VoucherExpiredDateTo).format("YYYY/MM/DD HH:mm:ss");
            }

            if ($scope._Object.RedeemedDateFrom) {
                $scope._Object.RedeemedDateFrom = moment($scope._Object.RedeemedDateFrom).format("YYYY/MM/DD HH:mm:ss");
            }
            if ($scope._Object.RedeemedDateTo) {
                $scope._Object.RedeemedDateTo = moment($scope._Object.RedeemedDateTo).format("YYYY/MM/DD HH:mm:ss");
            }
            $scope.$broadcast('updateDataTable', {
                options: angular.extend($scope.txnLst, {
                    serverSide: true,
                    ajax: ApiUrl + '/GetMemberVouchersByAcctNo?' + ($scope._Object ? $.param($scope._Object) : ''),
                    destroy: true,
                    "drawCallback": function (settings) {
                        $scope.$apply();
                        $.unblockUI();
                    }
                })
            })
        }

        $scope.$on("gotoDetail", function (event, aData) {
            window.location.href = $rootScope.getRootUrl() + "/Members#/" + $rootScope.obj.acctNoHash + "/Transactions/Vouchers/" + aData[1];
        });
    };

    var txnVoucherDetailController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http) {
        // local declare
        var vm = this;
        var ApiUrl = $rootScope.getRootUrl() + '/Members';
        var CommonUrl = $rootScope.getRootUrl() + '/Common';

        $rootScope.stsHisLst = {
            searching: false,
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
            //ajax: '/Members/AccountLogList?' + $.param({ AcctNoHash: $rootScope.obj.acctNoHash }),
            edit: {
                level: 'scope',
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
            bLengthChange: false,
            bInfo: false
        };

        var Initial = function () {
            $.blockUI();
            var params = $.param({ Prefix: 'MemTxnVoucherDetail', VoucherNo: $rootScope.obj.voucherNo, AcctNoHash: $rootScope.obj.acctNoHash });
            $http({
                method: 'GET',
                url: ApiUrl + '/FillData?' + params
            })
                .then(function successCallback(response) {
                    if (response.data.ResponseCode === 0) {
                        $scope._Object = response.data.Model;
                        $scope._Selects = response.data.Selects;
                        $scope._GeneralInfo = response.data.GeneralInfo;

                        var opts = angular.extend($scope.stsHisLst, {
                            serverSide: true,
                            ajax: ApiUrl + '/GetMemberVoucherStatusLogs?' + $.param({ VoucherNo: $scope._Object.VoucherNo }),
                            destroy: true,
                            "drawCallback": function (settings) {
                                $scope.$apply();
                                $.unblockUI();
                            }
                        });
                        $scope.$broadcast('updateDataTable', { options: opts });

                        $.unblockUI();
                    } else {
                        Utils.finalResultNotify(response.data);
                    }
                });
        }

        Initial();

        $scope.SaveSts = function () {
            $.blockUI();
            $http({
                method: 'PATCH',
                data: $scope._Object,
                url: ApiUrl + '/MemberVoucher'
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
                }
                );
        }

        $scope.backToList = function () {
            window.location.href = $rootScope.getRootUrl() + "/Members#/" + $rootScope.obj.acctNoHash + "/Transactions/Vouchers";
        };
    };

    var txnStickersController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog, Utils, $http) {

    }

    membersController.$inject = injectParams;
    newController.$inject = injectParams;
    summaryController.$inject = injectParams;
    infoController.$inject = injectParams;
    contactController.$inject = injectParams;

    cardsController.$inject = injectParams;
    cardInfoController.$inject = injectParams;
    cardDetailController.$inject = injectParams;

    addressController.$inject = injectParams;
    addressDetailController.$inject = injectParams;
    addressNewController.$inject = injectParams;
    txnPointsController.$inject = injectParams;
    txnVouchersController.$inject = injectParams;
    txnVoucherDetailController.$inject = injectParams;
    txnStickersController.$inject = injectParams;

    angular.module('loyaltyApp').controller('membersController', membersController);
    angular.module('loyaltyApp').controller('newController', newController);
    angular.module('loyaltyApp').controller('summaryController', summaryController);
    angular.module('loyaltyApp').controller('infoController', infoController);
    angular.module('loyaltyApp').controller('contactController', contactController);

    angular.module('loyaltyApp').controller('cardsController', cardsController);
    angular.module('loyaltyApp').controller('cardInfoController', cardInfoController);
    angular.module('loyaltyApp').controller('cardDetailController', cardDetailController);
    
    angular.module('loyaltyApp').controller('addressController', addressController);
    angular.module('loyaltyApp').controller('addressDetailController', addressDetailController);
    angular.module('loyaltyApp').controller('addressNewController', addressNewController);
    angular.module('loyaltyApp').controller('txnPointsController', txnPointsController);
    angular.module('loyaltyApp').controller('txnVouchersController', txnVouchersController);
    angular.module('loyaltyApp').controller('txnVoucherDetailController', txnVoucherDetailController);
    angular.module('loyaltyApp').controller('txnStickersController', txnStickersController);
}());