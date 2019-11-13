(function () {
    var injectParams = ['$scope', '$rootScope', '$location', '$window', '$timeout', 'ngDialog'];
    
    var locationsController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
        // local declare
        var vm = this;
        $scope.locationLst = {
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
            id: 'allLocations',
            ajax: '/Locations/GetAllLocations?' + $.param({ Ind: 1 }),
            edit: {
                level: 'scope',
                func: 'gotoProfile'
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
            //"createdRow": function (row, data, index) {
            //    var acctStsIdx = 2;
            //    switch (data[acctStsIdx]) {
            //        case 'Active':
            //            $('td', row).eq(acctStsIdx).addClass('txt-green');
            //            break;
            //        case 'Blocked':
            //            $('td', row).eq(acctStsIdx).addClass('txt-orange');
            //            break;
            //        case 'Suspended':
            //            $('td', row).eq(acctStsIdx).addClass('txt-red');
            //            break;
            //        case 'Closed':
            //            $('td', row).eq(acctStsIdx).addClass('txt-gray');
            //            break;
            //    }
            //}
        };

        $rootScope.$on("gotoProfile", function (event, aData) {
            var _acctNo = aData[0];
            //alert(_acctNo);
            window.location.href = "/Locations#/56484950";
        });
    };

    var locationProfileController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
        // local declare
        var vm = this;
        //$scope.backToList = function () {
        //    window.location.href = "/";
        //};

        $rootScope.ChangeSts = function (oldSts, newSts) {
            title = 'Change Status';
            if (newSts == 'Active') {
                title = 'Activate Location';
            } else if (newSts == 'Suspended') {
                title = 'Suspend Location';
            } else if (newSts == 'Terminated') {
                title = 'Terminate Location';
            }
            ngDialog.open({
                template: 'ChangeSts',
                width: '40%',
                //controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: title, oldSts: oldSts, newSts: newSts }
            });
        }
    };

    var locBusInfoController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
        // local declare
        var vm = this;
        //$scope.backToList = function () {
        //    window.location.href = "/";
        //};

        $rootScope.ChangeSts = function (oldSts, newSts) {
            title = 'Change Status';
            if (newSts == 'Active') {
                title = 'Activate Location';
            } else if (newSts == 'Suspended') {
                title = 'Suspend Location';
            } else if (newSts == 'Terminated') {
                title = 'Terminate Location';
            }
            ngDialog.open({
                template: 'ChangeSts',
                width: '40%',
                //controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: title, oldSts: oldSts, newSts: newSts }
            });
        }
    };

    var terminalController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
        // local declare
        var vm = this;
        //$scope.backToList = function () {
        //    window.location.href = "/";
        //};

        $scope.tablelst = {
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
            id: 'allLocations',
            ajax: '/Locations/GetAllTerminal?' + $.param({ Ind: 1 }),
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
            //"createdRow": function (row, data, index) {
            //    var acctStsIdx = 2;
            //    switch (data[acctStsIdx]) {
            //        case 'Active':
            //            $('td', row).eq(acctStsIdx).addClass('txt-green');
            //            break;
            //        case 'Blocked':
            //            $('td', row).eq(acctStsIdx).addClass('txt-orange');
            //            break;
            //        case 'Suspended':
            //            $('td', row).eq(acctStsIdx).addClass('txt-red');
            //            break;
            //        case 'Closed':
            //            $('td', row).eq(acctStsIdx).addClass('txt-gray');
            //            break;
            //    }
            //}
        };

        $rootScope.$on("gotoDetail", function (event, aData) {
            var _no = aData[1];
            window.location.href = "/Locations#/56484950/Terminal/" + _no;
        });

        $rootScope.ChangeSts = function (oldSts, newSts) {
            title = 'Change Status';
            if (newSts == 'Active') {
                title = 'Activate Location';
            } else if (newSts == 'Suspended') {
                title = 'Suspend Location';
            } else if (newSts == 'Terminated') {
                title = 'Terminate Location';
            }
            ngDialog.open({
                template: 'ChangeSts',
                width: '40%',
                //controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: title, oldSts: oldSts, newSts: newSts }
            });
        };
    };

    var terminalDetailController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
        // local declare
        var vm = this;
        $scope.backToList = function () {
            window.location.href = "/Locations#/56484950/Terminal";
        };

        $rootScope.ChangeSts = function (oldSts, newSts) {
            title = 'Change Status';
            if (newSts == 'Active') {
                title = 'Activate Location';
            } else if (newSts == 'Suspended') {
                title = 'Suspend Location';
            } else if (newSts == 'Terminated') {
                title = 'Terminate Location';
            }
            ngDialog.open({
                template: 'ChangeSts',
                width: '40%',
                //controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: title, oldSts: oldSts, newSts: newSts }
            });
        };

        $scope.ChangeTerminalSts = function (oldSts, newSts) {
            title = 'Change Status';
            if (newSts == 'Active') {
                title = 'Activate Terminal';
            } else if (newSts == 'Suspended') {
                title = 'Suspend Terminal';
            } else if (newSts == 'Terminated') {
                title = 'Close Terminal';
            }
            ngDialog.open({
                template: 'ChangeTerminalSts',
                width: '40%',
                //controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: title, oldSts: oldSts, newSts: newSts }
            });
        }

        $scope.ReplaceTerminal = function () {
            $rootScope.CloseCurTer = true;
            ngDialog.open({
                template: 'TerminalReplace',
                width: '40%',
                //controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: 'Replace Terminal' }
            });
        }
    };

    var financialInfoController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
        // local declare
        var vm = this;
        //$scope.backToList = function () {
        //    window.location.href = "/";
        //};
    };

    var topUpLimitontroller = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
        // local declare
        var vm = this;
        //$scope.backToList = function () {
        //    window.location.href = "/";
        //};
    };

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
            ajax: '/Locations/GetTransactionsLst?' + $.param({ Ind: 1 }),
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
            ajax: '/Locations/GetTransactionsPstedLst?' + $.param({ Ind: 1 }),
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

        $scope.txnSettlementLst = {
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
            ajax: '/Locations/GetTransactionsSettlementLst?' + $.param({ Ind: 1 }),
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
                ajax: '/Locations/GetTransactionItemLst?' + $.param({ Ind: 1 }),
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

    var contactsController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
        // local declare
        var vm = this;
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
            id: 'contactsMerchant',
            ajax: '/Merchants/GetAllContacts?' + $.param({ Ind: 1 }),
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
                "sEmptyTable": "No contacts yet. Click <a href='/Locations#/56484950/Contact/New'>+ Add</a> above to create a contact."
            },
            "order": [[1, 'asc']]
        };

        $scope.$on("gotoContactDetail", function (event, aData) {
            $scope.gotoContactDetail();
        });

        $scope.gotoContactDetail = function () {
            window.location.href = "/Locations#/56484950/Contact:564";
        };

        $scope.gotoContactNew = function () {
            window.location.href = "/Locations#/56484950/Contact/New";
        };

        $rootScope.ChangeSts = function (oldSts, newSts) {
            title = 'Change Status';
            if (newSts == 'Active') {
                title = 'Activate Card';
            } else if (newSts == 'Suspended') {
                title = 'Suspend Card';
            } else if (newSts == 'Terminated') {
                title = 'Terminate Card';
            }
            ngDialog.open({
                template: 'CrdChangeSts',
                width: '40%',
                //controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: title, oldSts: oldSts, newSts: newSts }
            });
        }
    };

    var contactsDetailController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
        // local declare
        var vm = this;
        $scope.backToList = function () {
            window.location.href = "/Locations#/56484950/Contact";
        };

        $rootScope.ChangeSts = function (oldSts, newSts) {
            title = 'Change Status';
            if (newSts == 'Active') {
                title = 'Activate Card';
            } else if (newSts == 'Suspended') {
                title = 'Suspend Card';
            } else if (newSts == 'Terminated') {
                title = 'Terminate Card';
            }
            ngDialog.open({
                template: 'CrdChangeSts',
                width: '40%',
                //controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: title, oldSts: oldSts, newSts: newSts }
            });
        }
    };

    var contactsNewController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
        // local declare
        var vm = this;
        $scope.backToList = function () {
            window.location.href = "/Locations#/56484950/Contact";
        };

        $rootScope.ChangeSts = function (oldSts, newSts) {
            title = 'Change Status';
            if (newSts == 'Active') {
                title = 'Activate Card';
            } else if (newSts == 'Suspended') {
                title = 'Suspend Card';
            } else if (newSts == 'Terminated') {
                title = 'Terminate Card';
            }
            ngDialog.open({
                template: 'CrdChangeSts',
                width: '40%',
                //controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: title, oldSts: oldSts, newSts: newSts }
            });
        }
    };

    var addressController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
        // local declare
        var vm = this;
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
            id: 'allAddresses',
            ajax: '/Merchants/GetAllAddresses?' + $.param({ Ind: 1 }),
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
                "sEmptyTable": "No address yet. Click <a href='/Locations#/56484950/Address/New'>+ Add</a> above to create a address."
            },
            "order": [[1, 'asc']]
        };
        $scope.$on("gotoAddressDetail", function (event, aData) {
            $scope.gotoAddressDetail();
        });

        $scope.gotoAddressDetail = function () {
            window.location.href = "/Locations#/56484950/Address:898";
        };

        $scope.gotoAddressNew = function () {
            window.location.href = "/Locations#/56484950/Address/New";
        };

        $rootScope.ChangeSts = function (oldSts, newSts) {
            title = 'Change Status';
            if (newSts == 'Active') {
                title = 'Activate Card';
            } else if (newSts == 'Suspended') {
                title = 'Suspend Card';
            } else if (newSts == 'Terminated') {
                title = 'Terminate Card';
            }
            ngDialog.open({
                template: 'CrdChangeSts',
                width: '40%',
                //controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: title, oldSts: oldSts, newSts: newSts }
            });
        }
    };

    var addressDetailController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
        // local declare
        var vm = this;
        $scope.backToList = function () {
            window.location.href = "/Locations#/56484950/Address";
        };

        $rootScope.ChangeSts = function (oldSts, newSts) {
            title = 'Change Status';
            if (newSts == 'Active') {
                title = 'Activate Card';
            } else if (newSts == 'Suspended') {
                title = 'Suspend Card';
            } else if (newSts == 'Terminated') {
                title = 'Terminate Card';
            }
            ngDialog.open({
                template: 'CrdChangeSts',
                width: '40%',
                //controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: title, oldSts: oldSts, newSts: newSts }
            });
        }
    };

    var addressNewController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
        // local declare
        var vm = this;
        $scope.backToList = function () {
            window.location.href = "/Locations#/56484950/Address";
        };

        $rootScope.ChangeSts = function (oldSts, newSts) {
            title = 'Change Status';
            if (newSts == 'Active') {
                title = 'Activate Card';
            } else if (newSts == 'Suspended') {
                title = 'Suspend Card';
            } else if (newSts == 'Terminated') {
                title = 'Terminate Card';
            }
            ngDialog.open({
                template: 'CrdChangeSts',
                width: '40%',
                //controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: title, oldSts: oldSts, newSts: newSts }
            });
        }
    };

    locationsController.$inject = injectParams;
    locationProfileController.$inject = injectParams;
    locBusInfoController.$inject = injectParams;
    terminalController.$inject = injectParams;
    terminalDetailController.$inject = injectParams;
    financialInfoController.$inject = injectParams;
    topUpLimitontroller.$inject = injectParams;
    transactionsController.$inject = injectParams;
    contactsController.$inject = injectParams;
    contactsDetailController.$inject = injectParams;
    contactsNewController.$inject = injectParams;
    addressController.$inject = injectParams;
    addressDetailController.$inject = injectParams;
    addressNewController.$inject = injectParams;

    angular.module('loyaltyApp').controller('locationsController', locationsController);
    angular.module('loyaltyApp').controller('locationProfileController', locationProfileController);
    angular.module('loyaltyApp').controller('locBusInfoController', locBusInfoController);
    angular.module('loyaltyApp').controller('terminalController', terminalController);
    angular.module('loyaltyApp').controller('terminalDetailController', terminalDetailController);
    angular.module('loyaltyApp').controller('financialInfoController', financialInfoController);
    angular.module('loyaltyApp').controller('topUpLimitontroller', topUpLimitontroller);
    angular.module('loyaltyApp').controller('transactionsController', transactionsController);
    angular.module('loyaltyApp').controller('contactsController', contactsController);
    angular.module('loyaltyApp').controller('contactsDetailController', contactsDetailController);
    angular.module('loyaltyApp').controller('contactsNewController', contactsNewController);
    angular.module('loyaltyApp').controller('addressController', addressController);
    angular.module('loyaltyApp').controller('addressDetailController', addressDetailController);
    angular.module('loyaltyApp').controller('addressNewController', addressNewController);
}());