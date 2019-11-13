(function () {
    var injectParams = ['$scope', '$rootScope', '$location', '$window', '$timeout', 'customerApi', 'ngDialog', 'Utils'];
    var customersController = function ($scope, $rootScope, $location, $window, $timeout) {
        // local declare
        var vm = this;
        $('.search-data').hide();
        $('.not-search').show();

        $rootScope.OnSearch = function () {
            $scope.customerLst = {
                stateSave: true,
                "stateSaveParams": function (settings, data) {
                    data.search.search = "";
                    data.start = 0;
                    data.length = 10;
                    data.order = [];
                },
                stateDuration:0,
                stateSaveCallback: function (settings, data) {
                    localStorage.setItem('DataTables_CustomerSearch', JSON.stringify(data))
                },
                stateLoadCallback: function (settings) {
                    return JSON.parse(localStorage.getItem('DataTables_CustomerSearch'))
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
                id: 'customerLst',
                ajax: '/Customers/GetSearchCustomers?' + $.param({ Ind: 1 }),
                edit: {
                    level: 'scope',
                    func: 'gotoCustomerDetail'
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

            $rootScope.$on("gotoCustomerDetail", function (event, aData) {
                var _acctNo = aData[0];
                //alert(_acctNo);
                window.location.href = "/Customers#/9894934943";
            });

            $('.search-data').show();
            $('.not-search').hide();
        }
    };

    var accountSummaryController = function ($scope, $rootScope, $location, $window, $timeout) {
        // local declare
        var vm = this;
    }

    var acctCusInfoController = function ($scope, $rootScope, $location, $window, $timeout, customerApi, ngDialog) {
        // local declare
        var vm = this;

        $scope.showStatusHistory = function () {
            $rootScope.acctStsHisLst = {
                colVis: {
                    "buttonClass": 'display-none'
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
                "ordering": false,
                id: 'allApplications',
                ajax: '/Customers/GetAcctStatusHis?' + $.param({ Ind: 1 }),
                edit: {
                    level: 'scope',
                    //func: 'gotoCustomerDetail'
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

            ngDialog.open({
                template: 'AcctStsHis',
                width: '60%',
                //controller: 'acctCusInfoController',
                className: 'ngdialog-theme-default',
                data: { }
            });
        };
        
        $scope.openCorporateAccount = function () {
            window.location = "/#/";
            window.location.reload(true)
        }

        $rootScope.ChangeSts = function (oldSts, newSts) {
            title = 'Change Status';
            if (newSts == 'Active') {
                title = 'Activate Account';
            } else if (newSts == 'Suspended') {
                title = 'Suspend Card';
            } else if (newSts == 'Terminated') {
                title = 'Terminate Card';
            }
            ngDialog.open({
                template: 'AccountChangeSts',
                width: '55%',
                //controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: title, oldSts: oldSts, newSts: newSts }
            });
        }
    };

    var customersContactsController = function ($scope, $rootScope, $location, $window, $timeout) {
        // local declare
        var vm = this;
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
                localStorage.setItem('DataTables_CustomerContact', JSON.stringify(data))
            },
            stateLoadCallback: function (settings) {
                return JSON.parse(localStorage.getItem('DataTables_CustomerContact'))
            },
            aoColumnDefs: [
                { aTargets: [0], bSortable: false },
                { aTargets: [0, 1, 2, 3, 4, 5, 6, 7], bVisible: true },
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
            "ordering": false,
            id: 'allApplications',
            ajax: '/Customers/GetAllContacts?' + $.param({ Ind: 1 }),
            edit: {
                level: 'scope',
                func: 'gotoContactDetail'
            },
            oLanguage: {
                sSearchPlaceholder: "Search",
                sLengthMenu: "Show _MENU_",
                sSearch: "<span><i class='fa fa-search'></i></span>",
                oPaginate: {
                    sNext: '<span class="pagination-default"></span><span class="pagination-fa"><i class="fa fa-angle-right" ></i></span>',
                    sPrevious: '<span class="pagination-default"></span><span class="pagination-fa"><i class="fa fa-angle-left" ></i></span>'
                },
                "sEmptyTable": "No contacts yet. Click <a href='/Customers#/9894934943/Contact/New'>+ Add</a> above to create a contact."
            }
        };

        $scope.$on("gotoContactDetail", function (event, aData) {
            $scope.gotoContactDetail();
        });

        $scope.gotoContactDetail = function () {
            window.location.href = "/Customers#/9894934943/Contact:564";
        };

        $scope.gotoContactNew = function () {
            window.location.href = "/Customers#/9894934943/Contact/New";
        };
    };

    var customersContactsDetailController = function ($scope, $rootScope, $location, $window, $timeout, customerApi, ngDialog, Utils) {
        // local declare
        var vm = this;
        $scope.promise = customerApi.GetContactDetail({ AccountID: $scope._userId })
            .then(
            function successCallback(response) {
                $scope._Object = response.data.Model;
                $scope._Selects = response.data.Selects;
            },
            function errorCallback(response) {
                //
            }
        );

        $scope.SaveContactDetail = function () {
            //$scope._Object.acctNo = acctNo;
            Utils.InfoNotify();
            customerApi.SaveContactDetail($scope._Object)
                .then(
                function successCallback(response) {
                    Utils.finalResultNotify(response.data);
                    },
                    function errorCallback(response) {
                        //
                    }
                );
        };

        $scope.backToList = function () {
            window.location.href = "/Customers#/9894934943/Contact";
        };

        $scope.deleteContact= function () {
            ngDialog.open({
                template: 'ConfirmDelete',
                width: '30%',
                //controller: 'acctCusInfoController',
                className: 'ngdialog-theme-default',
                data: {}
            });
        };
    };

    var customersContactsNewController = function ($scope, $rootScope, $location, $window, $timeout) {
        // local declare
        var vm = this;
        $scope.backToList = function () {
            window.location.href = "/Customers#/9894934943/Contact";
        };
    };

    var customerAddressController = function ($scope, $rootScope, $location, $window, $timeout) {
        // local declare
        var vm = this;
        $rootScope.addressDetail = false;
        $rootScope.addressNew = false;
        $scope.addressList = true;
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
                localStorage.setItem('DataTables_CustomerAddress', JSON.stringify(data))
            },
            stateLoadCallback: function (settings) {
                return JSON.parse(localStorage.getItem('DataTables_CustomerAddress'))
            },
            aoColumnDefs: [
                { aTargets: [0], bSortable: false },
                { aTargets: [0, 1, 2, 3, 4, 5, 6, 7], bVisible: true },
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
            ajax: '/Customers/GetAllAddresses?' + $.param({ Ind: 1 }),
            edit: {
                level: 'scope',
                func: 'gotoAddressDetail'
            },
            oLanguage: {
                sSearchPlaceholder: "Search",
                sLengthMenu: "Show _MENU_",
                sSearch: "<span><i class='fa fa-search'></i></span>",
                oPaginate: {
                    sNext: '<span class="pagination-default"></span><span class="pagination-fa"><i class="fa fa-angle-right" ></i></span>',
                    sPrevious: '<span class="pagination-default"></span><span class="pagination-fa"><i class="fa fa-angle-left" ></i></span>'
                },
                "sEmptyTable": "No address yet. Click <a href='/Customers#/Address/New'>+ Add</a> above to create a address."
            }
        };
        $scope.$on("gotoAddressDetail", function (event, aData) {
            $scope.gotoAddressDetail();
        });

        $scope.gotoAddressDetail = function () {
            window.location.href = "/Customers#/9894934943/Address:898";
        };

        $scope.gotoAddressNew = function () {
            window.location.href = "/Customers#/9894934943/Address/New";
        };
    };

    var customerAddressDetailController = function ($scope, $rootScope, $location, $window, $timeout, customerApi, ngDialog) {
        // local declare
        var vm = this;
        $scope.backToList = function () {
            window.location.href = "/Customers#/9894934943/Address";
        };

        $scope.deleteAddress = function () {
            ngDialog.open({
                template: 'ConfirmDelete',
                width: '30%',
                //controller: 'acctCusInfoController',
                className: 'ngdialog-theme-default',
                data: {}
            });
        };
    };

    var customerAddressNewController = function ($scope, $rootScope, $location, $window, $timeout) {
        // local declare
        var vm = this;
        $scope.backToList = function () {
            window.location.href = "/Customers#/9894934943/Address";
        };
    };

    var financialController = function ($scope, $rootScope, $location, $window, $timeout, customerApi, ngDialog) {
        // local declare
        var vm = this;

        $scope.promise = customerApi.GetCreditAssessment({ UserId: $scope._userId })
            .then(
                function successCallback(response) {
                    $scope._Object = response.data.Model;
                    $scope._Selects = response.data.Selects;
                },
                function errorCallback(response) {
                    //
                }
            );

        //$scope.showBankAccNoSample = function () {
        //    ngDialog.open({
        //        template: 'bankAccNoSample',
        //        width: '70%',
        //        //height: '555px',
        //        //controller: 'acctCusInfoController',
        //        className: 'ngdialog-theme-default',
        //        //data: { title: 'Application Approval Workflow' }
        //    });
        //};
    };

    var financialPositionController = function ($scope, $rootScope, $location, $window, $timeout, customerApi) {
        $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
            $.fn.dataTable.tables({ visible: true, api: true }).columns.adjust();
        });

        var vm = this;
        $scope.instantTxn = {
            stateSave: true,
            "stateSaveParams": function (settings, data) {
                data.search.search = "";
                data.start = 0;
                data.length = 10;
                data.order = [];
            },
            stateDuration:0,
            stateSaveCallback: function (settings, data) {
                localStorage.setItem('DataTables_CustomerInstantTxn', JSON.stringify(data))
            },
            stateLoadCallback: function (settings) {
                return JSON.parse(localStorage.getItem('DataTables_CustomerInstantTxn'))
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
            id: 'instantTxn',
            ajax: '/Customers/GetInstantTxn?' + $.param({ Ind: 1 }),
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

        //$scope.onlineTxn = {
        //    stateSave: true,
        //    "stateSaveParams": function (settings, data) {
        //        data.search.search = "";
        //        data.start = 0;
        //        data.length = 10;
        //        data.order = [];
        //    },
        //    stateDuration: 0,
        //    stateSaveCallback: function (settings, data) {
        //        localStorage.setItem('DataTables_CustomerInstantTxn', JSON.stringify(data))
        //    },
        //    stateLoadCallback: function (settings) {
        //        return JSON.parse(localStorage.getItem('DataTables_CustomerInstantTxn'))
        //    },
        //    aoColumnDefs: [
        //        { aTargets: [0], bSortable: false }
        //    ],
        //    order: [],
        //    colVis: {
        //        "buttonText": '<i class="material-icons">account_circle</i>'
        //    },
        //    serverSide: true,
        //    "autoWidth": false,
        //    "preDrawCallback": function (settings) {
        //        $.blockUI();
        //    },
        //    "drawCallback": function (settings) {
        //        $.unblockUI();
        //    },
        //    checkBox: false,
        //    "scrollX": true,
        //    "ordering": false,
        //    id: 'onlineTxn',
        //    ajax: '/Customers/GetInstantTxn?' + $.param({ Ind: 1 }),
        //    oLanguage: {
        //        sSearchPlaceholder: "Search",
        //        sLengthMenu: "Show _MENU_",
        //        sSearch: "<span><i class='fa fa-search'></i></span>",
        //        oPaginate: {
        //            sNext: '<span class="pagination-default"></span><span class="pagination-fa"><i class="fa fa-angle-right" ></i></span>',
        //            sPrevious: '<span class="pagination-default"></span><span class="pagination-fa"><i class="fa fa-angle-left" ></i></span>'
        //        }
        //    }
        //};

        $scope.onlineTxn = {
            stateSave: true,
            "stateSaveParams": function (settings, data) {
                data.search.search = "";
                data.start = 0;
                data.length = 10;
                data.order = [];
            },
            stateDuration: 0,
            stateSaveCallback: function (settings, data) {
                localStorage.setItem('DataTables_CustomerOnlineTxn', JSON.stringify(data))
            },
            stateLoadCallback: function (settings) {
                return JSON.parse(localStorage.getItem('DataTables_CustomerOnlineTxn'))
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
            id: 'onlineTxn',
            ajax: '/Customers/GetOnlineTxn?' + $.param({ Ind: 1 }),
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

        $scope.batchTxn = {
            stateSave: true,
            "stateSaveParams": function (settings, data) {
                data.search.search = "";
                data.start = 0;
                data.length = 10;
                data.order = [];
            },
            stateDuration: 0,
            stateSaveCallback: function (settings, data) {
                localStorage.setItem('DataTables_CustomerBatchTxn', JSON.stringify(data))
            },
            stateLoadCallback: function (settings) {
                return JSON.parse(localStorage.getItem('DataTables_CustomerBatchTxn'))
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
            id: 'batchTxn',
            ajax: '/Customers/GetBatchTxn?' + $.param({ Ind: 1 }),
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

        $scope.offlineTxn = {
            stateSave: true,
            "stateSaveParams": function (settings, data) {
                data.search.search = "";
                data.start = 0;
                data.length = 10;
                data.order = [];
            },
            stateDuration: 0,
            stateSaveCallback: function (settings, data) {
                localStorage.setItem('DataTables_CustomerOfflineTxn', JSON.stringify(data))
            },
            stateLoadCallback: function (settings) {
                return JSON.parse(localStorage.getItem('DataTables_CustomerOfflineTxn'))
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
            id: 'offlineTxn',
            ajax: '/Customers/GetOfflineTxn?' + $.param({ Ind: 1 }),
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
    };

    var creditabilityController = function ($scope, $rootScope, $location, $window, $timeout, customerApi, ngDialog) {
        // local declare
        var vm = this;
        //$scope.crdAssFrm = true;
        //$scope.crdAssHisFrm = false;
        //$scope.securityDepositNewFrm = false;
        //$scope.securityDepositLstFrm = false;
        //$scope.securityDepositHisFrm = false;
        $scope.reqSecurityDepositVal = true;

        $scope.promise = customerApi.GetCreditAssessment({ UserId: $scope._userId })
        .then(
            function successCallback(response) {
                $scope._Object = response.data.Model;
                $scope._Selects = response.data.Selects;
            },
            function errorCallback(response) {
                //
            }
        );

        $scope.openCreditHis = function () {
            $rootScope.crdAssHis = {
                colVis: {
                    "buttonClass": 'display-none'
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
                "ordering": false,
                id: 'allApplications',
                ajax: '/Customers/GetCreditLimitHistory?' + $.param({ Ind: 1 }),
                //edit: {
                //    level: 'scope',
                //    func: 'gotoCustomerDetail'
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

            ngDialog.open({
                template: 'creditLimitHis',
                width: '60%',
                //controller: 'creditAssessmentController',
                className: 'ngdialog-theme-default',
                data: { }
            });
        };

        $scope.loadSecurityDepositLst = function () {
            window.location = "/Customers#/9894934943/SecurityDeposit";
            window.location.reload(true)
        }
    };

    var securityDepositLst = function ($scope, $rootScope, $location, $window, $timeout) {
        // local declare
        var vm = this;
        $scope.reqSecurityDepositVal = true;

        $scope.securityDepositLst = {
            stateSave: true,
            "stateSaveParams": function (settings, data) {
                data.search.search = "";
                data.start = 0;
                data.length = 10;
                data.order = [];
            },
            stateDuration: 0,
            stateSaveCallback: function (settings, data) {
                localStorage.setItem('DataTables_CusSecurityDeposit', JSON.stringify(data))
            },
            stateLoadCallback: function (settings) {
                return JSON.parse(localStorage.getItem('DataTables_CusSecurityDeposit'))
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
            id: 'allApplications',
            ajax: '/Customers/GetSecurityDepositLst?' + $.param({ Ind: 1 }),
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

        $scope.gotoNew = function () {
            window.location.href = "/Customers#/9894934943/SecurityDeposit/New";
        }

        $rootScope.$on("gotoDetail", function (event, aData) {
            //var _acctNo = aData[0];
            window.location.href = "/Customers#/9894934943/SecurityDeposit:666";
        });
    };

    var securityDepositNew = function ($scope, $rootScope, $location, $window, $timeout, customerApi, ngDialog) {
        // local declare
        var vm = this;
        $scope.backToList = function () {
            window.history.back();
        }

        //$scope.showBankAccNoSample = function () {
        //    ngDialog.open({
        //        template: 'bankAccNoSample',
        //        width: '70%',
        //        className: 'ngdialog-theme-default'
        //    });
        //};
    };

    var securityDepositDetail = function ($scope, $rootScope, $location, $window, $timeout, customerApi, ngDialog) {
        // local declare
        var vm = this;
        $scope.backToList = function () {
            window.history.back();
        }

        $scope.showSecurityDepositHis = function () {
            $rootScope.securityDepositHis = {
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
                "ordering": false,
                id: 'allApplications',
                ajax: '/Customers/GetSecurityDepositHis?' + $.param({ Ind: 1 }),
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

            ngDialog.open({
                template: 'securityDepositHis',
                width: '60%',
                className: 'ngdialog-theme-default'
            });
        };

        //$scope.showBankAccNoSample = function () {
        //    ngDialog.open({
        //        template: 'bankAccNoSample',
        //        width: '70%',
        //        className: 'ngdialog-theme-default'
        //    });
        //};
    };

    var productPriceController = function ($scope, $rootScope, $location, $window, $timeout) {
        // local declare
        var vm = this;
        $scope.ProductPriceLst = {
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
            "ordering": false,
            id: 'ProductPriceLst',
            ajax: '/Customers/GetProductPriceLst?' + $.param({ Ind: 1 }),
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
            },
            "order": [[1, 'asc']]
        };

        $rootScope.$on("gotoDetail", function (event, aData) {
            //var _acctNo = aData[0];
            window.location.href = "/Customers#/9894934943/ProductPrice:122"
        });

        $rootScope.gotoProductPriceNew = function () {
            //var _acctNo = aData[0];
            window.location.href = "/Customers#/9894934943/ProductPrice/New"
        };
    };

    var productPriceDetailController = function ($scope, $rootScope, $location, $window, $timeout, customerApi, ngDialog) {
        // local declare
        var vm = this;
        $scope.isEditMode = false;
        $scope.ProductPriceItems = [];

        //$scope.loadPricingModelHis = function () {
        //    $('.prm-history').show();
        //    $('.main-content').hide();
        //};

        //$scope.backToMainContent = function () {
        //    $('.prm-history').hide();
        //    $('.main-content').show();
        //};

        $scope.backToList = function () {
            //window.history.back();
            window.location.href = "/Customers#/9894934943/ProductPrice"
        }

        $scope.showPricingModelHis = function () {
            $rootScope.PricingModelHisLst = {
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
                "ordering": false,
                id: 'ProductPriceLst',
                ajax: '/Customers/GetPricingModelHisLst?' + $.param({ Ind: 1 }),
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
                },
                "order": [[1, 'asc']]
            };

            ngDialog.open({
                template: 'pricingModelHis',
                width: '60%',
                className: 'ngdialog-theme-default'
            });
        };

        $scope.ProductPriceItems.push({
            UnitPrice: '11.9', ValidityFrom: '22-03-2017', ValidityTo: '22-03-2018', isEdit: false
        });

        $scope.loadPricingModelNew = function () {
            $scope.ProductPriceItems.unshift({
                UnitPrice: null, ValidityFrom: null, ValidityTo: null, isEdit: true
            });
        };

        $scope.productPirceCancel = function (item) {
            $scope.ProductPriceItems.splice($scope.ProductPriceItems.indexOf(item), 1);
            //item.isEdit = false;
        };

        $scope.productPirceEdit = function (item) {
            item.isEdit = true;
        };
    };

    var productPriceNewController = function ($scope, $rootScope, $location, $window, $timeout, customerApi, ngDialog) {
        // local declare
        var vm = this;
        $scope.isEditMode = false;
        $scope.ProductPriceItems = [];

        $scope.showPricingModelHis = function () {
            $rootScope.PricingModelHisLst = {
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
                "ordering": false,
                id: 'ProductPriceLst',
                ajax: '/Customers/GetPricingModelHisLst?' + $.param({ Ind: 1 }),
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
                },
                "order": [[1, 'asc']]
            };

            ngDialog.open({
                template: 'pricingModelHis',
                width: '60%',
                className: 'ngdialog-theme-default'
            });
        };

        //$scope.backToMainContent = function () {
        //    $('.prm-history').hide();
        //    $('.main-content').show();
        //};
        $scope.backToList = function () {
            //window.history.back();
            window.location.href = "/Customers#/9894934943/ProductPrice"
        }

        $scope.loadPricingModelNew = function () {
            $scope.ProductPriceItems.unshift({
                UnitPrice: null, ValidityFrom: null, ValidityTo: null, isEdit: true
            });
        };

        $scope.productPirceCancel = function (item) {
            $scope.ProductPriceItems.splice($scope.ProductPriceItems.indexOf(item), 1);
            //item.isEdit = false;
        };

        $scope.productPirceEdit = function (item) {
            item.isEdit = true;
        };
    };

    var discountRebateController = function ($scope, $rootScope, $location, $window, $timeout) {
        // local declare
        var vm = this;
        $scope.DiscountRebateLst = {
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
            "ordering": false,
            id: 'ProductPriceLst',
            ajax: '/Customers/GetDiscountRebateLst?' + $.param({ Ind: 1 }),
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
            },
            "order": [[1, 'asc']]
        };

        $rootScope.$on("gotoDetail", function (event, aData) {
            //var _acctNo = aData[0];
            window.location.href = "/Customers#/9894934943/DiscountRebate:111"
        });

        $rootScope.gotoCreateNew = function () {
            //var _acctNo = aData[0];
            window.location.href = "/Customers#/9894934943/DiscountRebate/New"
        };
    };

    var discountRebateDetailController = function ($scope, $rootScope, $location, $window, $timeout) {
        // local declare
        var vm = this;

        $scope.isEditMode = false;
        $scope.ValueSettingItems = [];

        $scope.loadValueSettingNew = function () {
            $scope.ValueSettingItems.unshift({
                TierValueFrom: null, TierValueTo: null, BasicValue: null, BilledValue: null, isEdit: true
            });
        };

        $scope.valueSettingCancel = function (item) {
            //$scope.ValueSettingItems.splice($scope.ValueSettingItems.indexOf(item), 1);
            item.isEdit = false;
        };

        $scope.valueSettingEdit = function (item) {
            item.isEdit = true;
        };

        $scope.ValueSettingItems.push({
            TierValueFrom: '22.1', TierValueTo: '23.2', BasicValue: '23.4', BilledValue: '54.3', isEdit: false
        });

        $scope.backToList = function () {
            //window.history.back();
            window.location.href = "/Customers#/9894934943/DiscountRebate"
        }
    };

    var discountRebateNewController = function ($scope, $rootScope, $location, $window, $timeout) {
        // local declare
        var vm = this;
        $scope.isEditMode = false;
        $scope.ValueSettingItems = [];
        var editingItem = null;

        $scope.loadValueSettingNew = function () {
            $scope.ValueSettingItems.unshift({
                TierValueFrom: null, TierValueTo: null, BasicValue: null, BilledValue: null, isEdit: true
            });
        };

        $scope.valueSettingCancel = function (item) {
            //$scope.ValueSettingItems.splice($scope.ValueSettingItems.indexOf(item), 1);
            item = editingItem;
            item.isEdit = false;
        };

        $scope.valueSettingSave = function (item) {
            item.isEdit = false;
        };

        $scope.valueSettingEdit = function (item) {
            item.isEdit = true;
            editingItem = item;
        };

        $scope.backToList = function () {
            //window.history.back();
            window.location.href = "/Customers#/9894934943/DiscountRebate"
        }
    };

    var productAcceptanceController = function ($scope, $rootScope, $location, $window, $timeout, customerApi, ngDialog) {
        // local declare
        var vm = this;
        //$scope.showProductGroupHistory = function () {
        //    $rootScope.ProductGroupHisLst = {
        //        colVis: {
        //            "buttonClass": 'display-none'
        //        },
        //        serverSide: true,
        //        "autoWidth": false,
        //        "preDrawCallback": function (settings) {
        //            $.blockUI();
        //        },
        //        "drawCallback": function (settings) {
        //            $.unblockUI();
        //        },
        //        checkBox: false,
        //        "scrollX": true,
        //        "ordering": false,
        //        id: 'ProductPriceLst',
        //        ajax: '/Customers/GetProductGrpHisLst?' + $.param({ Ind: 1 }),
        //        //edit: {
        //        //    level: 'scope',
        //        //    func: 'gotoDetail'
        //        //},
        //        oLanguage: {
        //            sSearchPlaceholder: "Search",
        //            sLengthMenu: "Show _MENU_",
        //            sSearch: "<span><i class='fa fa-search'></i></span>",
        //            oPaginate: {
        //                sNext: '<span class="pagination-default"></span><span class="pagination-fa"><i class="fa fa-angle-right" ></i></span>',
        //                sPrevious: '<span class="pagination-default"></span><span class="pagination-fa"><i class="fa fa-angle-left" ></i></span>'
        //            }
        //        },
        //        "order": [[1, 'asc']]
        //    };

        //    ngDialog.open({
        //        template: 'productGroupHis',
        //        width: '60%',
        //        className: 'ngdialog-theme-default'
        //    });
        //};
        //$scope.ProductGroupItems = [];

        //$scope.ProductGroupItems.push({
        //    ProductGroup: '001 Petrol & Diesel only', isEdit: false
        //});

        //$scope.loadProductGroupNew = function () {
        //    $scope.ProductGroupItems.unshift({
        //        ProductGroup: null, isEdit: true
        //    });
        //};

        //$scope.productGroupCancel = function (item) {
        //    $scope.ProductGroupItems.splice($scope.ProductGroupItems.indexOf(item), 1);
        //    //item.isEdit = false;
        //};

        //$scope.productGroupEdit = function (item) {
        //    item.isEdit = true;
        //};

        $scope.productItemsTbl = {
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
            "ordering": false,
            id: 'ProductPriceLst',
            ajax: '/Customers/GetProductItemsByGrp?' + $.param({ Ind: 1 }),
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
            },
            "order": [[1, 'asc']]
        };

        //$rootScope.$on("gotoDetail", function (event, aData) {
        //    //var _acctNo = aData[0];
        //    window.location.href = "/Customers#/9894934943/UsageControl/ProductRestrictions:545"
        //});

        //$rootScope.gotoNew = function () {
        //    //var _acctNo = aData[0];
        //    window.location.href = "/Customers#/9894934943/UsageControl/ProductRestrictions/New"
        //};
    };

    //var productRestrictionsDetailController = function ($scope, $rootScope, $location, $window, $timeout, customerApi) {
    //    // local declare
    //    var vm = this;
    //    $scope._userId = '167';
    //    $scope.promise = customerApi.GetProductRestrictions({ UserId: 167 }).then(function successCallback(response) {
    //        $scope._Object = response.data.Model;
    //        $scope._Selects = response.data.Selects;
    //    }, function errorCallback(response) { });

    //    $scope.backToList = function () {
    //        window.location.href = "/Customers#/9894934943/UsageControl/ProductRestrictions"
    //    }
    //};

    //var productRestrictionsNewController = function ($scope, $rootScope, $location, $window, $timeout) {
    //    // local declare
    //    var vm = this;
    //    $scope.isEditMode = false;
    //    $scope.ValueSettingItems = [];

    //    $scope.loadValueSettingNew = function () {
    //        $scope.ValueSettingItems.unshift({
    //            TierValue: null, BasicValue: null, BilledValue: null, isEdit: true
    //        });
    //    };

    //    $scope.valueSettingCancel = function (item) {
    //        item.isEdit = false;
    //    };

    //    $scope.valueSettingEdit = function (item) {
    //        item.isEdit = true;
    //    };

    //    $scope.backToList = function () {
    //        window.location.href = "/Customers#/9894934943/UsageControl/ProductRestrictions"
    //    }
    //};

    var velocityLimitController = function ($scope, $rootScope, $location, $window, $timeout) {
        // local declare
        var vm = this;

        $scope.TxnLmtItems = [];

        $scope.TxnLmtItems.push({ ProductGroup: '002 Diesel only', AmtLmt: '100.00', VlmLmt: '-', isEdit: false });
        $scope.TxnLmtItems.push({ ProductGroup: '003 Petrol only', AmtLmt: '100.00', VlmLmt: '-', isEdit: false });
        $scope.TxnLmtItems.push({ ProductGroup: '004 Petrol & Service only', AmtLmt: '150.00', VlmLmt: '-', isEdit: false });
        $scope.TxnLmtItems.push({ ProductGroup: '005 Service only', AmtLmt: '50.00', VlmLmt: '-', isEdit: false });

        $scope.addTxnLmt = function () {
            const tableDiv = document.getElementById("TxnLmtTbl");
            window.scrollTo({
                'behavior': 'smooth',
                'left': 0,
                'top': tableDiv.offsetTop - 50
            });

            $scope.TxnLmtItems.unshift({
                ProductGroup: '001 Petrol & Diesel only', AmtLmt: '100.00', VlmLmt: '', isEdit: true
            });
        };

        $scope.txnLmtCancel = function (item) {
            //$scope.TxnLmtItems.splice($scope.TxnLmtItems.indexOf(item), 1);
            item.isEdit = false;
        };

        $scope.txnLmtEdit = function (item) {
            item.isEdit = true;
        };


        //----------
        $scope.DailyLmtItems = [];

        $scope.DailyLmtItems.push({ ProductGroup: '002 Diesel only', AmtLmt: '100.00', VlmLmt: '-', CntLmt:'10', isEdit: false });
        $scope.DailyLmtItems.push({ ProductGroup: '003 Petrol only', AmtLmt: '100.00', VlmLmt: '-', CntLmt: '10', isEdit: false });
        $scope.DailyLmtItems.push({ ProductGroup: '004 Petrol & Service only', AmtLmt: '150.00', VlmLmt: '-', CntLmt: '10', isEdit: false });
        $scope.DailyLmtItems.push({ ProductGroup: '005 Service only', AmtLmt: '50.00', VlmLmt: '-', CntLmt: '10', isEdit: false });

        $scope.addDailyLmt = function () {
            const tableDiv = document.getElementById("DailyLmtTbl");
            window.scrollTo({
                'behavior': 'smooth',
                'left': 0,
                'top': tableDiv.offsetTop - 50
            });

            $scope.DailyLmtItems.unshift({
                ProductGroup: '001 Petrol & Diesel only', AmtLmt: '100.00', VlmLmt: '', isEdit: true
            });
        };

        $scope.dailyLmtCancel = function (item) {
            //$scope.DailyLmtItems.splice($scope.DailyLmtItems.indexOf(item), 1);
            item.isEdit = false;
        };

        $scope.dailyLmtEdit = function (item) {
            item.isEdit = true;
        };

        //----------
        $scope.WeeklyLmtItems = [];
        $scope.WeeklyLmtItems.push({ ProductGroup: '002 Diesel only', AmtLmt: '1000.00', VlmLmt: '-', CntLmt: '100', isEdit: false });
        $scope.WeeklyLmtItems.push({ ProductGroup: '003 Petrol only', AmtLmt: '1000.00', VlmLmt: '-', CntLmt: '100', isEdit: false });
        $scope.WeeklyLmtItems.push({ ProductGroup: '004 Petrol & Service only', AmtLmt: '1000.00', VlmLmt: '-', CntLmt: '100', isEdit: false });
        $scope.WeeklyLmtItems.push({ ProductGroup: '005 Service only', AmtLmt: '1000.00', VlmLmt: '-', CntLmt: '100', isEdit: false });

        $scope.addWeeklyLmt = function () {
            const tableDiv = document.getElementById("WeeklyLmtTbl");
            window.scrollTo({
                'behavior': 'smooth',
                'left': 0,
                'top': tableDiv.offsetTop - 50
            });

            $scope.WeeklyLmtItems.unshift({
                ProductGroup: '001 Petrol & Diesel only', AmtLmt: '1000.00', VlmLmt: '', CntLmt: '', isEdit: true
            });
        };

        $scope.weeklyLmtCancel = function (item) {
            //$scope.WeeklyLmtItems.splice($scope.WeeklyLmtItems.indexOf(item), 1);
            item.isEdit = false;
        };

        $scope.weeklyLmtEdit = function (item) {
            item.isEdit = true;
        };

        //--------------
        //----------
        $scope.MonthlyLmtItems = [];
        $scope.MonthlyLmtItems.push({ ProductGroup: '002 Diesel only', AmtLmt: '5000.00', VlmLmt: '-', CntLmt: '500', isEdit: false });
        $scope.MonthlyLmtItems.push({ ProductGroup: '003 Petrol only', AmtLmt: '5000.00', VlmLmt: '-', CntLmt: '500', isEdit: false });
        $scope.MonthlyLmtItems.push({ ProductGroup: '004 Petrol & Service only', AmtLmt: '5000.00', VlmLmt: '-', CntLmt: '500', isEdit: false });
        $scope.MonthlyLmtItems.push({ ProductGroup: '005 Service only', AmtLmt: '5000.00', VlmLmt: '-', CntLmt: '500', isEdit: false });

        $scope.addMonthlyLmt = function () {
            const tableDiv = document.getElementById("MonthlyLmtTbl");
            window.scrollTo({
                'behavior': 'smooth',
                'left': 0,
                'top': tableDiv.offsetTop - 50
            });

            $scope.MonthlyLmtItems.unshift({
                ProductGroup: '001 Petrol & Diesel only', AmtLmt: '5000.00', VlmLmt: '', CntLmt: '', isEdit: true
            });
        };

        $scope.monthlyLmtCancel = function (item) {
            //$scope.MonthlyLmtItems.splice($scope.MonthlyLmtItems.indexOf(item), 1);
            item.isEdit = false;
        };

        $scope.monthlyLmtEdit = function (item) {
            item.isEdit = true;
        };
    };

    var locationAcceptanceController = function ($scope, $rootScope, $location, $window, $timeout) {
        // local declare
        var vm = this;

        //$scope.tblList = {
        //    serverSide: true,
        //    "autoWidth": false,
        //    "preDrawCallback": function (settings) {
        //        $.blockUI();
        //    },
        //    "drawCallback": function (settings) {
        //        $.unblockUI();
        //    },
        //    checkBox: false,
        //    "scrollX": true,
        //    "ordering": false,
        //    id: 'tblList',
        //    ajax: '/Customers/GetLocationAcceptanceLst?' + $.param({ Ind: 1 }),
        //    //edit: {
        //    //    level: 'scope',
        //    //    func: 'gotoDetail'
        //    //},
        //    oLanguage: {
        //        sSearchPlaceholder: "Search",
        //        sLengthMenu: "Show _MENU_",
        //        sSearch: "<span><i class='fa fa-search'></i></span>",
        //        oPaginate: {
        //            sNext: '<span class="pagination-default"></span><span class="pagination-fa"><i class="fa fa-angle-right" ></i></span>',
        //            sPrevious: '<span class="pagination-default"></span><span class="pagination-fa"><i class="fa fa-angle-left" ></i></span>'
        //        }
        //    },
        //    columnDefs: [
        //        {
        //            targets: 3,
        //            render: function ( data, type, row, meta ) {
        //                if(type === 'display'){
        //                    data = '<a href="Customers#/9894934943/UsageControl/LocationAcceptance:' + row[2] + '">' + data + '</a>';
        //                }
        //                return data;
        //            }
        //        },
        //        {
        //            targets: 0,
        //            render: function (data, type, full, meta) {
        //                return '<input type="checkbox" name="id[]" value="' + $('<div/>').text(data).html() + '">';
        //            }
        //        }
        //    ],
        //    "order": [[1, 'asc']]
        //};
    };

    //var stationDealerController = function ($scope, $rootScope, $location, $window, $timeout) {
    //    // local declare
    //    var vm = this;

    //    $scope.tblList = {
    //        serverSide: true,
    //        "autoWidth": false,
    //        "preDrawCallback": function (settings) {
    //            $.blockUI();
    //        },
    //        "drawCallback": function (settings) {
    //            $.unblockUI();
    //        },
    //        checkBox: false,
    //        "scrollX": true,
    //        "ordering": false,
    //        id: 'tblList',
    //        ajax: '/Customers/GetStationLst?' + $.param({ Ind: 1 }),
    //        //edit: {
    //        //    level: 'scope',
    //        //    func: 'gotoDetail'
    //        //},
    //        oLanguage: {
    //            sSearchPlaceholder: "Search",
    //            sLengthMenu: "Show _MENU_",
    //            sSearch: "<span><i class='fa fa-search'></i></span>",
    //            oPaginate: {
    //                sNext: '<span class="pagination-default"></span><span class="pagination-fa"><i class="fa fa-angle-right" ></i></span>',
    //                sPrevious: '<span class="pagination-default"></span><span class="pagination-fa"><i class="fa fa-angle-left" ></i></span>'
    //            }
    //        },
    //        columnDefs: [
    //            {
    //                targets: 0,
    //                render: function (data, type, full, meta) {
    //                    return '<input type="checkbox" name="id[]" value="' + $('<div/>').text(data).html() + '">';
    //                }
    //            }
    //        ],
    //        "order": [[1, 'asc']]
    //    };

    //    $scope.gotoLocationAcpt = function() {
    //        window.location.href = "/Customers#/9894934943/UsageControl/LocationAcceptance"
    //    }
    //};

    var costCentreController = function ($scope, $rootScope, $location, $window, $timeout) {
        // local declare
        var vm = this;
        $scope.tblLst = {
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
            "ordering": false,
            id: 'tblLst',
            ajax: '/Customers/GetCostCentreLst?' + $.param({ Ind: 1 }),
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
            },
            "order": [[1, 'asc']]
        };

        $rootScope.$on("gotoDetail", function (event, aData) {
            //var _acctNo = aData[0];
            window.location.href = "/Customers#/9894934943/CostCentre:122"
        });

        $rootScope.gotoProductPriceNew = function () {
            //var _acctNo = aData[0];
            window.location.href = "/Customers#/9894934943/CostCentre/New"
        };
    };

    var costCentreDetailController = function ($scope, $rootScope, $location, $window, $timeout) {
        // local declare
        var vm = this;

        $scope.backToList = function () {
            window.location.href = "/Customers#/9894934943/CostCentre";
        };
    };

    var costCentreNewController = function ($scope, $rootScope, $location, $window, $timeout) {
        // local declare
        var vm = this;

        $scope.backToList = function () {
            window.location.href = "/Customers#/9894934943/CostCentre";
        };
    };

    ///
    var ccProductRestrictionsController = function ($scope, $rootScope, $location, $window, $timeout) {
        // local declare
        var vm = this;
        $scope.tblOpts = {
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
            "ordering": false,
            id: 'ProductPriceLst',
            ajax: '/Customers/GetProductRestrictionsLst?' + $.param({ Ind: 1 }),
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
            },
            "order": [[1, 'asc']]
        };

        $rootScope.$on("gotoDetail", function (event, aData) {
            //var _acctNo = aData[0];
            window.location.href = "/Customers#/9894934943/CostCentre/ProductRestrictions:545"
        });

        $rootScope.gotoNew = function () {
            //var _acctNo = aData[0];
            window.location.href = "/Customers#/9894934943/CostCentre/ProductRestrictions/New"
        };
    };

    var ccProductRestrictionsDetailController = function ($scope, $rootScope, $location, $window, $timeout) {
        // local declare
        var vm = this;
        $scope._userId = '167';

        $scope.backToList = function () {
            window.location.href = "/Customers#/9894934943/CostCentre/ProductRestrictions"
        }
    };

    var ccProductRestrictionsNewController = function ($scope, $rootScope, $location, $window, $timeout) {
        // local declare
        var vm = this;

        $scope.backToList = function () {
            window.location.href = "/Customers#/9894934943/CostCentre/ProductRestrictions"
        }
    };

    var ccVelocityLimitController = function ($scope, $rootScope, $location, $window, $timeout) {
        // local declare
        var vm = this;
    };

    var ccLocationAcceptanceController = function ($scope, $rootScope, $location, $window, $timeout) {
        // local declare
        var vm = this;

        //$scope.tblList = {
        //    serverSide: true,
        //    "autoWidth": false,
        //    "preDrawCallback": function (settings) {
        //        $.blockUI();
        //    },
        //    "drawCallback": function (settings) {
        //        $.unblockUI();
        //    },
        //    checkBox: false,
        //    "scrollX": true,
        //    "ordering": false,
        //    id: 'tblList',
        //    ajax: '/Customers/GetLocationAcceptanceLst?' + $.param({ Ind: 1 }),
        //    //edit: {
        //    //    level: 'scope',
        //    //    func: 'gotoDetail'
        //    //},
        //    oLanguage: {
        //        sSearchPlaceholder: "Search",
        //        sLengthMenu: "Show _MENU_",
        //        sSearch: "<span><i class='fa fa-search'></i></span>",
        //        oPaginate: {
        //            sNext: '<span class="pagination-default"></span><span class="pagination-fa"><i class="fa fa-angle-right" ></i></span>',
        //            sPrevious: '<span class="pagination-default"></span><span class="pagination-fa"><i class="fa fa-angle-left" ></i></span>'
        //        }
        //    },
        //    columnDefs: [
        //        {
        //            targets: 3,
        //            render: function (data, type, row, meta) {
        //                if (type === 'display') {
        //                    data = '<a href="Customers#/9894934943/CostCentre/LocationAcceptance:' + row[2] + '">' + data + '</a>';
        //                }
        //                return data;
        //            }
        //        },
        //        {
        //            targets: 0,
        //            render: function (data, type, full, meta) {
        //                return '<input type="checkbox" name="id[]" value="' + $('<div/>').text(data).html() + '">';
        //            }
        //        }
        //    ],
        //    "order": [[1, 'asc']]
        //};
    };

    var ccStationDealerController = function ($scope, $rootScope, $location, $window, $timeout) {
        // local declare
        var vm = this;

        $scope.tblList = {
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
            "ordering": false,
            id: 'tblList',
            ajax: '/Customers/GetStationLst?' + $.param({ Ind: 1 }),
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
            },
            columnDefs: [
                {
                    targets: 0,
                    render: function (data, type, full, meta) {
                        return '<input type="checkbox" name="id[]" value="' + $('<div/>').text(data).html() + '">';
                    }
                }
            ],
            "order": [[1, 'asc']]
        };

        $scope.gotoLocationAcpt = function () {
            window.location.href = "/Customers#/9894934943/CostCentre/LocationAcceptance"
        }
    };

    //var allCardsController = function ($scope, $rootScope, $location, $window, $timeout) {
    //    // local declare
    //    var vm = this;
    //    $scope.pendingCrds = {
    //        serverSide: true,
    //        "autoWidth": false,
    //        "preDrawCallback": function (settings) {
    //            $.blockUI();
    //        },
    //        "drawCallback": function (settings) {
    //            $.unblockUI();
    //        },
    //        checkBox: false,
    //        "scrollX": true,
    //        "ordering": false,
    //        id: 'tblLst',
    //        ajax: '/Customers/GetPendingCards?' + $.param({ Ind: 1 }),
    //        edit: {
    //            level: 'scope',
    //            func: 'gotoDetail'
    //        },
    //        oLanguage: {
    //            sSearchPlaceholder: "Search",
    //            sLengthMenu: "Show _MENU_",
    //            sSearch: "<span><i class='fa fa-search'></i></span>",
    //            oPaginate: {
    //                sNext: '<span class="pagination-default"></span><span class="pagination-fa"><i class="fa fa-angle-right" ></i></span>',
    //                sPrevious: '<span class="pagination-default"></span><span class="pagination-fa"><i class="fa fa-angle-left" ></i></span>'
    //            }
    //        },
    //        "order": [[1, 'asc']]
    //    };

    //    $scope.allCrds = {
    //        serverSide: true,
    //        "autoWidth": false,
    //        "preDrawCallback": function (settings) {
    //            $.blockUI();
    //        },
    //        "drawCallback": function (settings) {
    //            $.unblockUI();
    //        },
    //        checkBox: false,
    //        "scrollX": true,
    //        "ordering": false,
    //        id: 'tblLst',
    //        ajax: '/Customers/GetAllCards?' + $.param({ Ind: 1 }),
    //        edit: {
    //            level: 'scope',
    //            func: 'gotoDetail'
    //        },
    //        oLanguage: {
    //            sSearchPlaceholder: "Search",
    //            sLengthMenu: "Show _MENU_",
    //            sSearch: "<span><i class='fa fa-search'></i></span>",
    //            oPaginate: {
    //                sNext: '<span class="pagination-default"></span><span class="pagination-fa"><i class="fa fa-angle-right" ></i></span>',
    //                sPrevious: '<span class="pagination-default"></span><span class="pagination-fa"><i class="fa fa-angle-left" ></i></span>'
    //            }
    //        },
    //        "order": [[1, 'asc']]
    //    };

    //    $rootScope.$on("gotoDetail", function (event, aData) {
    //        //var _acctNo = aData[0];
    //        window.location.href = "/Cards#/738278";
    //    });

    //    //$rootScope.gotoNew = function () {
    //    //    window.location.href = "/Customers#/9894934943/Cards/New";
    //    //};

    //};

    var cardListController = function ($scope, $rootScope, $location, $window, $timeout) {
        // local declare
        var vm = this;
        $scope.pendingCards = {
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
            "ordering": false,
            id: 'tblLst',
            ajax: '/Customers/GetPendingCards?' + $.param({ Ind: 1 }),
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
            },
            "order": [[1, 'asc']]
        };

        $scope.createdCards = {
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
            "ordering": false,
            id: 'tblLst',
            ajax: '/Customers/GetCreatedCards?' + $.param({ Ind: 1 }),
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
            },
            "order": [[1, 'asc']]
        };

        $rootScope.$on("gotoDetail", function (event, aData) {
            //var _acctNo = aData[0];
            window.location.href = "/Cards#/738278";
        });

        $rootScope.gotoNew = function () {
            //var _acctNo = aData[0];
            window.location.href = "/Customers#/9894934943/Cards/New"
        };

    };
    
    var createCardsController = function ($scope, $rootScope, $location, $window, $timeout) {
        // local declare
        var vm = this;
        $scope.backToList = function () {
            //window.location.href = "/Customers#/9894934943/Cardlist";
            history.back();
        };
    };

    //var cardDetailsController = function ($scope, $rootScope, $location, $window, $timeout) {
    //    // local declare
    //    var vm = this;
    //    $scope.backToList = function () {
    //        //window.location.href = "/Customers#/9894934943/Cardlist";
    //        history.back();
    //    };
    //};

    var eventNotificationController = function ($scope, $rootScope, $location, $window, $timeout) {
        // local declare
        var vm = this;
        $scope.tblLst = {
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
            "ordering": false,
            id: 'tblLst',
            ajax: '/Customers/GetEventNotificationLst?' + $.param({ Ind: 1 }),
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
            },
            "order": [[1, 'asc']]
        };

        $rootScope.$on("gotoDetail", function (event, aData) {
            //var _acctNo = aData[0];
            window.location.href = "/Customers#/9894934943/Event-notification:122"
        });

        $rootScope.gotoNew = function () {
            //var _acctNo = aData[0];
            window.location.href = "/Customers#/9894934943/Event-notification/New"
        };
    };

    var eventNotificationDetailController = function ($scope, $rootScope, $location, $window, $timeout) {
        // local declare
        var vm = this;
        //$scope.isEditMode = false;
        $scope.NotificationCriteriaItems = [];

        $scope.NotificationCriteriaItems.push({
            CheckingPeriod: 'Set Checking Period', FrequencyChecking: 'Day', NoChecking:'3', MinValue:'1', MaxValue:'5', isEdit: false
        });

        $scope.loadNotificationCriteriaItemsNew = function () {
            $scope.NotificationCriteriaItems.unshift({
                CheckingPeriod: null, FrequencyChecking: null, NoChecking: null, MinValue: null, MaxValue: null, isEdit: true
            });
        };

        $scope.notificationCriteriaItemsCancel = function (item) {
            $scope.NotificationCriteriaItems.splice($scope.NotificationCriteriaItems.indexOf(item), 1);
        };

        $scope.notificationCriteriaItemsEdit = function (item) {
            item.isEdit = true;
        };

        // Notification Recipient
        $scope.NotificationRecipientItems = [];

        $scope.NotificationRecipientItems.push({
            RecipientName: 'Ali Muhammad', MobilePhoneNo: '012-29393840', Email: 'abc@gmail.com', PreferredLanguage: 'English', NotificationChannel: 'SMS', isEdit: false
        });

        $scope.loadNotificationRecipientItemsNew = function () {
            $scope.NotificationRecipientItems.unshift({
                isEdit: true
            });
        };

        $scope.notificationRecipientItemsCancel = function (item) {
            $scope.NotificationRecipientItems.splice($scope.NotificationRecipientItems.indexOf(item), 1);
            //item.isEdit = false;
        };

        $scope.notificationRecipientItemsEdit = function (item) {
            item.isEdit = true;
        };

        $scope.backToMainContent = function () {
            window.location.href = "/Customers#/9894934943/Event-notification";
            window.location.reload(true)
        };

        $scope.occurrenceVal = true;

        $scope.onDisableNotify = function (evt) {
            if (!$(evt.currentTarget).hasClass('selected')) {
                var _sibling = $(evt.currentTarget).parent().children()
                $(_sibling).removeClass('selected');
                $(evt.currentTarget).addClass('selected')
            }
            return;
        }

        $scope.onActivateNotify = function (evt) {
            if (!$(evt.currentTarget).hasClass('selected')) {
                var _sibling = $(evt.currentTarget).parent().children()
                $(_sibling).removeClass('selected');
                $(evt.currentTarget).addClass('selected')
            }
            return;
        }
    };

    var eventNotificationNewController = function ($scope, $rootScope, $location, $window, $timeout) {
        // local declare
        var vm = this;
        //$scope.isEditMode = false;
        $scope.NotificationCriteriaItems = [];

        $scope.NotificationCriteriaItems.push({
            CheckingPeriod: 'Set Checking Period', FrequencyChecking: 'Day', NoChecking: '3', MinValue: '1', MaxValue: '5', isEdit: false
        });

        $scope.loadNotificationCriteriaItemsNew = function () {
            $scope.NotificationCriteriaItems.unshift({
                CheckingPeriod: null, FrequencyChecking: null, NoChecking: null, MinValue: null, MaxValue: null, isEdit: true
            });
        };

        $scope.notificationCriteriaItemsCancel = function (item) {
            $scope.NotificationCriteriaItems.splice($scope.NotificationCriteriaItems.indexOf(item), 1);
        };

        $scope.notificationCriteriaItemsEdit = function (item) {
            item.isEdit = true;
        };

        // Notification Recipient
        $scope.NotificationRecipientItems = [];

        //$scope.NotificationRecipientItems.push({
        //    RecipientName: 'Ali Muhammad', MobilePhoneNo: '012-29393840', Email: 'abc@gmail.com', PreferredLanguage: 'English', NotificationChannel: 'SMS', isEdit: false
        //});

        $scope.loadNotificationRecipientItemsNew = function () {
            $scope.NotificationRecipientItems.unshift({
                isEdit: true
            });
        };

        $scope.notificationRecipientItemsCancel = function (item) {
            $scope.NotificationRecipientItems.splice($scope.NotificationRecipientItems.indexOf(item), 1);
            //item.isEdit = false;
        };

        $scope.notificationRecipientItemsEdit = function (item) {
            item.isEdit = true;
        };

        $scope.backToMainContent = function () {
            window.location.href = "/Customers#/9894934943/Event-notification";
            window.location.reload(true)
        };

        $scope.occurrenceVal = true;
        
        $scope.onDisableNotify = function (evt) {
            if (!$(evt.currentTarget).hasClass('selected')) {
                var _sibling = $(evt.currentTarget).parent().children()
                $(_sibling).removeClass('selected');
                $(evt.currentTarget).addClass('selected')
            }
            return;
        }

        $scope.onActivateNotify = function (evt) {
            if (!$(evt.currentTarget).hasClass('selected')) {
                var _sibling = $(evt.currentTarget).parent().children()
                $(_sibling).removeClass('selected');
                $(evt.currentTarget).addClass('selected')
            }
            return;
        }
    };

    var fileManagerController = function ($scope, $rootScope, $location, $window, $timeout, customerApi, ngDialog) {
        // local declare
        var vm = this;
        //Dropzone.autoDiscover = false;

        $scope.dtOptions = {
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
            "ordering": false,
            id: 'tblLst',
            ajax: '/Customers/GetFileManagerLst?' + $.param({ Ind: 1 }),
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
            },
            columnDefs: [
                {
                    targets: 0,
                    render: function (data, type, full, meta) {
                        return '<input type="checkbox" name="id[]" value="' + $('<div/>').text(data).html() + '">';
                    }
                }
            ],
            "order": [[1, 'asc']]
        };

        $scope.dzOptions = {
            url: '/alt_upload_url',
            dictDefaultMessage: 'Drop files here or click to upload',
            //paramName : 'photo',
            //maxFilesize : '10',
            //acceptedFiles : 'image/jpeg, images/jpg, image/png',
            addRemoveLinks: true,
        };


        //Handle events for dropzone
        //Visit http://www.dropzonejs.com/#events for more events
        $scope.dzCallbacks = {
            'addedfile': function (file) {
                console.log(file);
                $rootScope.newFile = file;
            },
            'success': function (file, xhr) {
                console.log(file, xhr);
            },
        };

        $scope.showUploadFiles = function () {

            //Set options for dropzone
            //Visit http://www.dropzonejs.com/#configuration-options for more options
            $rootScope.dzOptions = {
                url: '/alt_upload_url',
                dictDefaultMessage: 'Drop files here or click to upload',
                //paramName : 'photo',
                //maxFilesize : '10',
                //acceptedFiles : 'image/jpeg, images/jpg, image/png',
                addRemoveLinks : true,
	        };
	
	
            //Handle events for dropzone
            //Visit http://www.dropzonejs.com/#events for more events
            $rootScope.dzCallbacks = {
                'addedfile' : function(file){
                    console.log(file);
                    $rootScope.newFile = file;
                },
                'success' : function(file, xhr){
                    console.log(file, xhr);
                },
	        };
	
	
            //Apply methods for dropzone
            //Visit http://www.dropzonejs.com/#dropzone-methods for more methods
            //$rootScope.dzMethods = {};
            //$rootScope.removeNewFile = function () {
            //    $rootScope.dzMethods.removeFile($rootScope.newFile); //We got $scope.newFile from 'addedfile' event callback
            //}


            ngDialog.open({
                template: 'dropzonemodel',
                width: '60%',
                className: 'ngdialog-theme-default'
            });


            //var myDropzone = new Dropzone("#my-dropzone");
            //myDropzone.on("addedfile", function (file) {
            //    // Create the remove button
            //    var removeButton = Dropzone.createElement("<a href='javascript:;'' class='btn red btn-sm btn-block'>Remove</a>");

            //    // Capture the Dropzone instance as closure.
            //    var _this = this;

            //    // Listen to the click event
            //    removeButton.addEventListener("click", function (e) {
            //        // Make sure the button click doesn't submit the form:
            //        e.preventDefault();
            //        e.stopPropagation();

            //        // Remove the file preview.
            //        _this.removeFile(file);
            //        // If you want to the delete the file on the server as well,
            //        // you can do the AJAX request here.
            //    });

            //    // Add the button to the file preview element.
            //    file.previewElement.appendChild(removeButton);
            //});
        };
    };

    // Transactions
    var transactionsController = function ($scope, $rootScope, $location, $window, $timeout) {
        // local declare
        var vm = this;
        $scope.txnLst = {
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
            "ordering": false,
            id: 'txnLst',
            ajax: '/Customers/GetTransactionsLst?' + $.param({ Ind: 1 }),
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
            },
            "order": [[1, 'asc']]
        };

        $scope.txnPstedLst = {
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
            "ordering": false,
            id: 'txnPstedLst',
            ajax: '/Customers/GetTransactionsPstedLst?' + $.param({ Ind: 1 }),
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
            },
            "order": [[1, 'asc']]
        };

        $rootScope.$on("gotoDetail", function (event, aData) {
            //var _acctNo = aData[0];
            //window.location.href = "/Customers#/9894934943/SecurityDeposit:666";
            var $this = $('#TxnDetailTarget'),
				href = $this.attr('href'),
				$target = $($this.attr('data-target') || (href && href.replace(/.*(?=#[^\s]+$)/, ''))), //strip for ie7
				option = $target.data('modal') ? 'toggle' : $.extend({ remote: !/#/.test(href) && href }, $target.data(), $this.data());

            event.preventDefault();
            $target
				.modal(option)
				.one('hide', function () {
				    $this.focus();
				});

            $scope.txnItemLst = {
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
                "ordering": false,
                id: 'txnLst',
                ajax: '/Customers/GetTransactionItemLst?' + $.param({ Ind: 1 }),
                oLanguage: {
                    sSearchPlaceholder: "Search",
                    sLengthMenu: "Show _MENU_",
                    sSearch: "<span><i class='fa fa-search'></i></span>",
                    oPaginate: {
                        sNext: '<span class="pagination-default"></span><span class="pagination-fa"><i class="fa fa-angle-right" ></i></span>',
                        sPrevious: '<span class="pagination-default"></span><span class="pagination-fa"><i class="fa fa-angle-left" ></i></span>'
                    }
                },
                "order": [[1, 'asc']]
            };
            $scope.$apply();
        });
    };

    customersController.$inject = injectParams;
    accountSummaryController.$inject = injectParams;
    acctCusInfoController.$inject = injectParams;
    customersContactsController.$inject = injectParams;
    customersContactsDetailController.$inject = injectParams;
    customersContactsNewController.$inject = injectParams;
    customerAddressController.$inject = injectParams;
    customerAddressDetailController.$inject = injectParams;
    customerAddressNewController.$inject = injectParams;
    financialController.$inject = injectParams;
    financialPositionController.$inject = injectParams;
    creditabilityController.$inject = injectParams;
    securityDepositLst.$inject = injectParams;
    securityDepositNew.$inject = injectParams;
    securityDepositDetail.$inject = injectParams;
    productPriceController.$inject = injectParams;
    productPriceDetailController.$inject = injectParams;
    productPriceNewController.$inject = injectParams;
    discountRebateController.$inject = injectParams;
    discountRebateDetailController.$inject = injectParams;
    discountRebateNewController.$inject = injectParams;
    productAcceptanceController.$inject = injectParams;
    //productRestrictionsDetailController.$inject = injectParams;
    //productRestrictionsNewController.$inject = injectParams;
    velocityLimitController.$inject = injectParams;
    locationAcceptanceController.$inject = injectParams;
    //stationDealerController.$inject = injectParams;
    costCentreController.$inject = injectParams;
    costCentreDetailController.$inject = injectParams;
    costCentreNewController.$inject = injectParams;
    ccProductRestrictionsController.$inject = injectParams;
    ccProductRestrictionsDetailController.$inject = injectParams;
    ccProductRestrictionsNewController.$inject = injectParams;
    ccVelocityLimitController.$inject = injectParams;
    ccLocationAcceptanceController.$inject = injectParams;
    ccStationDealerController.$inject = injectParams;
    //allCardsController.$inject = injectParams;

    cardListController.$inject = injectParams;
    createCardsController.$inject = injectParams;
    //cardDetailsController.$inject = injectParams;
    eventNotificationController.$inject = injectParams;
    eventNotificationDetailController.$inject = injectParams;
    eventNotificationNewController.$inject = injectParams;
    fileManagerController.$inject = injectParams;
    transactionsController.$inject = injectParams;

    angular.module('loyaltyApp').controller('customersController', customersController);
    angular.module('loyaltyApp').controller('accountSummaryController', accountSummaryController);
    angular.module('loyaltyApp').controller('acctCusInfoController', acctCusInfoController);
    angular.module('loyaltyApp').controller('customersContactsController', customersContactsController);
    angular.module('loyaltyApp').controller('customersContactsDetailController', customersContactsDetailController);
    angular.module('loyaltyApp').controller('customersContactsNewController', customersContactsNewController);
    angular.module('loyaltyApp').controller('customerAddressController', customerAddressController);
    angular.module('loyaltyApp').controller('customerAddressDetailController', customerAddressDetailController);
    angular.module('loyaltyApp').controller('customerAddressNewController', customerAddressNewController);
    angular.module('loyaltyApp').controller('financialController', financialController);
    angular.module('loyaltyApp').controller('financialPositionController', financialPositionController);
    angular.module('loyaltyApp').controller('creditabilityController', creditabilityController);
    angular.module('loyaltyApp').controller('securityDepositLst', securityDepositLst);
    angular.module('loyaltyApp').controller('securityDepositNew', securityDepositNew);
    angular.module('loyaltyApp').controller('securityDepositDetail', securityDepositDetail);
    angular.module('loyaltyApp').controller('productPriceController', productPriceController);
    angular.module('loyaltyApp').controller('productPriceDetailController', productPriceDetailController);
    angular.module('loyaltyApp').controller('productPriceNewController', productPriceNewController);
    angular.module('loyaltyApp').controller('discountRebateController', discountRebateController);
    angular.module('loyaltyApp').controller('discountRebateDetailController', discountRebateDetailController);
    angular.module('loyaltyApp').controller('discountRebateNewController', discountRebateNewController);
    angular.module('loyaltyApp').controller('productAcceptanceController', productAcceptanceController);
    //angular.module('loyaltyApp').controller('productRestrictionsDetailController', productRestrictionsDetailController);
    //angular.module('loyaltyApp').controller('productRestrictionsNewController', productRestrictionsNewController);
    angular.module('loyaltyApp').controller('velocityLimitController', velocityLimitController);
    angular.module('loyaltyApp').controller('locationAcceptanceController', locationAcceptanceController);
    //angular.module('loyaltyApp').controller('stationDealerController', stationDealerController);
    angular.module('loyaltyApp').controller('costCentreController', costCentreController);
    angular.module('loyaltyApp').controller('costCentreDetailController', costCentreDetailController);
    angular.module('loyaltyApp').controller('ccLocationAcceptanceController', ccLocationAcceptanceController);
    angular.module('loyaltyApp').controller('costCentreNewController', costCentreNewController);
    angular.module('loyaltyApp').controller('ccProductRestrictionsController', ccProductRestrictionsController);
    angular.module('loyaltyApp').controller('ccProductRestrictionsDetailController', ccProductRestrictionsDetailController);
    angular.module('loyaltyApp').controller('ccProductRestrictionsNewController', ccProductRestrictionsNewController);
    angular.module('loyaltyApp').controller('ccVelocityLimitController', ccVelocityLimitController);
    angular.module('loyaltyApp').controller('costCentreNewController', costCentreNewController);
    angular.module('loyaltyApp').controller('ccStationDealerController', ccStationDealerController);
    //angular.module('loyaltyApp').controller('allCardsController', allCardsController);

    angular.module('loyaltyApp').controller('cardListController', cardListController);
    angular.module('loyaltyApp').controller('createCardsController', createCardsController);
    //angular.module('loyaltyApp').controller('cardDetailsController', cardDetailsController);
    angular.module('loyaltyApp').controller('eventNotificationController', eventNotificationController);
    angular.module('loyaltyApp').controller('eventNotificationDetailController', eventNotificationDetailController);
    angular.module('loyaltyApp').controller('eventNotificationNewController', eventNotificationNewController);
    angular.module('loyaltyApp').controller('fileManagerController', fileManagerController);
    angular.module('loyaltyApp').controller('transactionsController', transactionsController);
}());