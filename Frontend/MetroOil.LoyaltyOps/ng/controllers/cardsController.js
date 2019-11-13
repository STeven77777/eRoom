(function () {
    var injectParams = ['$scope', '$rootScope', '$location', '$window', '$timeout', 'ngDialog'];
    
    var cardsController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
        var vm = this;
        //$scope.searchResult = false;
        $('.search-data').hide();
        $('.not-search').show();

        $rootScope.OnSearch = function () {
            $scope.customerLst = {
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
                id: 'customerLst',
                ajax: '/Home/GetSearchCards?' + $.param({ Ind: 1 }),
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

            $('.search-data').show();
            $('.not-search').hide();
        }

        $rootScope.$on("gotoDetail", function (event, aData) {
            //var _acctNo = aData[0];
            window.location.href = "/Cards#/738278";
        });
    };

    var cardNewController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
        // local declare
        var vm = this;
        $scope.backToList = function () {
            window.location.href = "/";
        };
    };

    var cardSummaryController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
        // local declare
        var vm = this;
        $scope.RecentTxnItems = [];

        $scope.RecentTxnItems.push({ TxnDate: "25/02/2018, 17:00:00", TxnAmt: "50.00", TxnNo: "1005", LocationNo: "7243" });
        $scope.RecentTxnItems.push({ TxnDate : "25/02/2018, 10:00:00", TxnAmt : "45.00", TxnNo : "1004", LocationNo : "7748" });
        $scope.RecentTxnItems.push({ TxnDate : "24/02/2018, 08:00:00", TxnAmt : "60.00", TxnNo : "1003", LocationNo : "7021" });
        $scope.RecentTxnItems.push({ TxnDate : "22/02/2018, 20:00:00", TxnAmt : "30.00", TxnNo : "1002", LocationNo : "2849" });
        $scope.RecentTxnItems.push({ TxnDate : "19/02/2018, 15:00:00", TxnAmt : "50.55", TxnNo : "1001", LocationNo : "4859" });
        //$scope.recentTxn = {
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
        //    id: 'recentTxn',
        //    ajax: '/Cards/GetRecentTxn?' + $.param({ Ind: 1 }),
        //    filter: false,
        //    searching: false,
        //    bInfo: false
        //};

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

        $rootScope.ReplaceCard = function () {
            $rootScope.TerminateCurCrd = true;
            ngDialog.open({
                template: 'CrdReplace',
                width: '40%',
                //controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: 'Replace Card' }
            });
        }

        $rootScope.GeneratePin = function (val) {
            ngDialog.open({
                template: 'CrdGeneratePin',
                width: '40%',
                //controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: 'Confirmation' }
            });
        }
    };

    var cardInfoController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
        // local declare
        var vm = this;

        $scope.openCardStsHis = function (cardNo) {
            //alert('a');
            $rootScope.crdStsHisLst = {
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
                ajax: '/Cards/GetCardHistoryStatus?' + $.param({ cardNo: cardNo }),
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
                template: 'CrdStsHis',
                width: '60%',
                //controller: 'acctCusInfoController',
                className: 'ngdialog-theme-default',
                data: {}
            });
        }

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

        $rootScope.ReplaceCard = function () {
            $rootScope.TerminateCurCrd = true;
            ngDialog.open({
                template: 'CrdReplace',
                width: '40%',
                //controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: 'Replace Card' }
            });
        }

        $rootScope.GeneratePin = function (val) {
            ngDialog.open({
                template: 'CrdGeneratePin',
                width: '40%',
                //controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: 'Confirmation' }
            });
        }
    };

    var contactsController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
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
            ajax: '/Cards/GetAllContacts?' + $.param({ Ind: 1 }),
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
                "sEmptyTable": "No contacts yet. Click <a href='/Cards#/738278/Contact/New'>+ Add</a> above to create a contact."
            }
        };

        $scope.$on("gotoContactDetail", function (event, aData) {
            $scope.gotoContactDetail();
        });

        $scope.gotoContactDetail = function () {
            window.location.href = "/Cards#/738278/Contact:564";
        };

        $scope.gotoContactNew = function () {
            window.location.href = "/Cards#/738278/Contact/New";
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

        $rootScope.ReplaceCard = function () {
            $rootScope.TerminateCurCrd = true;
            ngDialog.open({
                template: 'CrdReplace',
                width: '40%',
                //controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: 'Replace Card' }
            });
        }

        $rootScope.GeneratePin = function (val) {
            ngDialog.open({
                template: 'CrdGeneratePin',
                width: '40%',
                //controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: 'Confirmation' }
            });
        }
    };

    var contactsDetailController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
        // local declare
        var vm = this;
        $scope.backToList = function () {
            window.location.href = "/Cards#/738278/Contact";
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

        $rootScope.ReplaceCard = function () {
            $rootScope.TerminateCurCrd = true;
            ngDialog.open({
                template: 'CrdReplace',
                width: '40%',
                //controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: 'Replace Card' }
            });
        }

        $rootScope.GeneratePin = function (val) {
            ngDialog.open({
                template: 'CrdGeneratePin',
                width: '40%',
                //controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: 'Confirmation' }
            });
        }

        $scope.deleteContact = function () {
            ngDialog.open({
                template: 'ConfirmDelete',
                width: '30%',
                //controller: 'acctCusInfoController',
                className: 'ngdialog-theme-default',
                data: {}
            });
        };
    };

    var contactsNewController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
        // local declare
        var vm = this;
        $scope.backToList = function () {
            window.location.href = "/Cards#/738278/Contact";
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

        $rootScope.ReplaceCard = function () {
            $rootScope.TerminateCurCrd = true;
            ngDialog.open({
                template: 'CrdReplace',
                width: '40%',
                //controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: 'Replace Card' }
            });
        }

        $rootScope.GeneratePin = function (val) {
            ngDialog.open({
                template: 'CrdGeneratePin',
                width: '40%',
                //controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: 'Confirmation' }
            });
        }
    };

    var addressController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
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
                localStorage.setItem('DataTables_CardAddress', JSON.stringify(data))
            },
            stateLoadCallback: function (settings) {
                return JSON.parse(localStorage.getItem('DataTables_CardAddress'))
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
            id: 'allAddresses',
            ajax: '/Cards/GetAllAddresses?' + $.param({ Ind: 1 }),
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
                "sEmptyTable": "No address yet. Click <a href='/Cards#/738278/Address/New'>+ Add</a> above to create a address."
            }
        };
        $scope.$on("gotoAddressDetail", function (event, aData) {
            $scope.gotoAddressDetail();
        });

        $scope.gotoAddressDetail = function () {
            window.location.href = "/Cards#/738278/Address:898";
        };

        $scope.gotoAddressNew = function () {
            window.location.href = "/Cards#/738278/Address/New";
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

        $rootScope.ReplaceCard = function () {
            $rootScope.TerminateCurCrd = true;
            ngDialog.open({
                template: 'CrdReplace',
                width: '40%',
                //controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: 'Replace Card' }
            });
        }

        $rootScope.GeneratePin = function (val) {
            ngDialog.open({
                template: 'CrdGeneratePin',
                width: '40%',
                //controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: 'Confirmation' }
            });
        }
    };

    var addressDetailController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
        // local declare
        var vm = this;
        $scope.backToList = function () {
            window.location.href = "/Cards#/738278/Address";
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

        $rootScope.ReplaceCard = function () {
            $rootScope.TerminateCurCrd = true;
            ngDialog.open({
                template: 'CrdReplace',
                width: '40%',
                //controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: 'Replace Card' }
            });
        }

        $rootScope.GeneratePin = function (val) {
            ngDialog.open({
                template: 'CrdGeneratePin',
                width: '40%',
                //controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: 'Confirmation' }
            });
        }

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

    var addressNewController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
        // local declare
        var vm = this;
        $scope.backToList = function () {
            window.location.href = "/Cards#/738278/Address";
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

        $rootScope.ReplaceCard = function () {
            $rootScope.TerminateCurCrd = true;
            ngDialog.open({
                template: 'CrdReplace',
                width: '40%',
                //controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: 'Replace Card' }
            });
        }

        $rootScope.GeneratePin = function (val) {
            ngDialog.open({
                template: 'CrdGeneratePin',
                width: '40%',
                //controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: 'Confirmation' }
            });
        }
    };

    var pintriesController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
        // local declare
        var vm = this;
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

        $rootScope.ReplaceCard = function () {
            $rootScope.TerminateCurCrd = true;
            ngDialog.open({
                template: 'CrdReplace',
                width: '40%',
                //controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: 'Replace Card' }
            });
        }

        $rootScope.GeneratePin = function (val) {
            ngDialog.open({
                template: 'CrdGeneratePin',
                width: '40%',
                //controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: 'Confirmation' }
            });
        }
    };

    var productPriceController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
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
            ajax: '/Cards/GetProductPriceLst?' + $.param({ Ind: 1 }),
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
            window.location.href = "/Cards#/738278/ProductPrice:122"
        });

        $rootScope.gotoProductPriceNew = function () {
            //var _acctNo = aData[0];
            window.location.href = "/Cards#/738278/ProductPrice/New"
        };
    };

    var productPriceDetailController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
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
            window.location.href = "/Cards#/738278/ProductPrice"
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
                ajax: '/Cards/GetPricingModelHisLst?' + $.param({ Ind: 1 }),
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

    var productPriceNewController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
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
                ajax: '/Cards/GetPricingModelHisLst?' + $.param({ Ind: 1 }),
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
            window.location.href = "/Cards#/738278/ProductPrice"
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

    var discountRebateController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
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
            ajax: '/Cards/GetDiscountRebateLst?' + $.param({ Ind: 1 }),
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
            window.location.href = "/Cards#/738278/DiscountRebate:111"
        });

        $rootScope.gotoCreateNew = function () {
            //var _acctNo = aData[0];
            window.location.href = "/Cards#/738278/DiscountRebate/New"
        };
    };

    var discountRebateDetailController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
        // local declare
        var vm = this;

        $scope.isEditMode = false;
        $scope.ValueSettingItems = [];

        $scope.loadValueSettingNew = function () {
            $scope.ValueSettingItems.unshift({
                TierValue: null, BasicValue: null, BilledValue: null, isEdit: true
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
            TierValue: '22.1', BasicValue: '23.4', BilledValue: '54.3', isEdit: false
        });

        $scope.backToList = function () {
            //window.history.back();
            window.location.href = "/Cards#/738278/DiscountRebate"
        }
    };

    var discountRebateNewController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
        // local declare
        var vm = this;
        $scope.isEditMode = false;
        $scope.ValueSettingItems = [];

        $scope.loadValueSettingNew = function () {
            $scope.ValueSettingItems.unshift({
                TierValue: null, BasicValue: null, BilledValue: null, isEdit: true
            });
        };

        $scope.valueSettingCancel = function (item) {
            //$scope.ValueSettingItems.splice($scope.ValueSettingItems.indexOf(item), 1);
            item.isEdit = false;
        };

        $scope.valueSettingEdit = function (item) {
            item.isEdit = true;
        };

        $scope.backToList = function () {
            //window.history.back();
            window.location.href = "/Cards#/738278/DiscountRebate"
        }
    };

    var productAcceptanceController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
        // local declare
        var vm = this;
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
            ajax: '/Cards/GetProductItemsByGrp?' + $.param({ Ind: 1 }),
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
    };

    //var productRestrictionsDetailController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
    //    // local declare
    //    var vm = this;
    //    $scope._userId = '167';
    //    //$scope.promise = customerApi.GetProductRestrictions({ UserId: 167 }).then(function successCallback(response) {
    //    //    $scope._Object = response.data.Model;
    //    //    $scope._Selects = response.data.Selects;
    //    //}, function errorCallback(response) { });

    //    $scope.backToList = function () {
    //        window.location.href = "/Cards#/738278/UsageControl/ProductRestrictions"
    //    }
    //};

    //var productRestrictionsNewController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
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
    //        //$scope.ValueSettingItems.splice($scope.ValueSettingItems.indexOf(item), 1);
    //        item.isEdit = false;
    //    };

    //    $scope.valueSettingEdit = function (item) {
    //        item.isEdit = true;
    //    };

    //    $scope.backToList = function () {
    //        window.location.href = "/Cards#/738278/UsageControl/ProductRestrictions"
    //    }
    //};

    var velocityLimitController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
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

        $scope.DailyLmtItems.push({ ProductGroup: '002 Diesel only', AmtLmt: '100.00', VlmLmt: '-', CntLmt: '10', isEdit: false });
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

    var locationAcceptanceController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
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
        //    ajax: '/Cards/GetLocationAcceptanceLst?' + $.param({ Ind: 1 }),
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
        //                    data = '<a href="Cards#/738278/UsageControl/LocationAcceptance:' + row[2] + '">' + data + '</a>';
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

    //var stationDealerController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
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
    //        ajax: '/Cards/GetStationLst?' + $.param({ Ind: 1 }),
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

    //    $scope.gotoLocationAcpt = function () {
    //        window.location.href = "/Cards#/738278/UsageControl/LocationAcceptance"
    //    }
    //};

    cardsController.$inject = injectParams;
    cardNewController.$inject = injectParams;
    cardSummaryController.$inject = injectParams;
    cardInfoController.$inject = injectParams;
    contactsController.$inject = injectParams;
    contactsDetailController.$inject = injectParams;
    contactsNewController.$inject = injectParams;
    addressController.$inject = injectParams;
    addressDetailController.$inject = injectParams;
    addressNewController.$inject = injectParams;
    pintriesController.$inject = injectParams;
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

    angular.module('loyaltyApp').controller('cardsController', cardsController);
    angular.module('loyaltyApp').controller('cardNewController', cardNewController);
    angular.module('loyaltyApp').controller('cardSummaryController', cardSummaryController);
    angular.module('loyaltyApp').controller('cardInfoController', cardInfoController);
    angular.module('loyaltyApp').controller('contactsController', contactsController);
    angular.module('loyaltyApp').controller('contactsDetailController', contactsDetailController);
    angular.module('loyaltyApp').controller('contactsNewController', contactsNewController);
    angular.module('loyaltyApp').controller('addressController', addressController);
    angular.module('loyaltyApp').controller('addressDetailController', addressDetailController);
    angular.module('loyaltyApp').controller('addressNewController', addressNewController);
    angular.module('loyaltyApp').controller('pintriesController', pintriesController);
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
}());