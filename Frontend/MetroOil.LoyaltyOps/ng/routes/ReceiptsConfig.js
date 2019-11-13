(function () {
    var app = angular.module('loyaltyApp',
        ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'App.Utils', 'ui.router', 'ui.select', 'ngSanitize', 'ngDialog', 'datePicker']);

    app.config(['$stateProvider', '$urlRouterProvider', '$locationProvider', 'ngDialogProvider', function ($stateProvider, $urlRouterProvider, $locationProvider, ngDialogProvider) {
        $locationProvider.html5Mode(false).hashPrefix('');
        var rooturl = $('#hdUrlPrefix').val();

        $stateProvider
            .state('ReceiptSearch', {
                url: "/",
                templateUrl: rooturl + "/Receipts/Tmpl?Prefix=ReceiptSearch&type=Index",
                data: { pageTitle: 'Receipt Search' },
                controller: "receiptsController",
                pageKey: 'ReceiptSearch'
            })
            .state('ReceiptInfoNew', {
                url: "/New",
                templateUrl: rooturl + "/Receipts/Tmpl?Prefix=ReceiptInfoNew&type=Index",
                data: { pageTitle: 'Create Business Locations' },
                controller: "newReceiptController",
                pageKey: 'ReceiptInfoNew'
            })
            .state('ReceiptInfo', {
                url: "/:busnLocationNoHash/Info",
                templateUrl: rooturl + "/Receipts/Tmpl?Prefix=ReceiptInfo&type=Index",
                data: { pageTitle: 'Business Locations Info' },
                controller: "receiptInfoController",
                pageKey: 'ReceiptInfo'
            })

            .state('ReceiptAddrs', {
                url: "/:busnLocationNoHash/Address",
                templateUrl: rooturl + "/Receipts/Tmpl?Prefix=ReceiptAddrs&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrController",
                pageKey: 'ReceiptAddrs'
            })
            .state('ReceiptAddrNew', {
                url: "/:busnLocationNoHash/Address/New",
                templateUrl: rooturl + "/Receipts/Tmpl?Prefix=ReceiptAddrNew&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrNewController",
                pageKey: 'ReceiptAddrNew'
            })
            .state('ReceiptAddrD', {
                url: "/:busnLocationNoHash/Address/:busnLocationAddressId",
                templateUrl: rooturl + "/Receipts/Tmpl?Prefix=ReceiptAddrD&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrDetailController",
                pageKey: 'ReceiptAddrD'
            })

            .state('ReceiptConts', {
                url: "/:busnLocationNoHash/Contact",
                templateUrl: rooturl + "/Receipts/Tmpl?Prefix=ReceiptConts&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContController",
                pageKey: 'ReceiptConts'
            })
            .state('ReceiptContNew', {
                url: "/:busnLocationNoHash/Contact/New",
                templateUrl: rooturl + "/Receipts/Tmpl?Prefix=ReceiptContNew&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContNewController",
                pageKey: 'ReceiptContNew'
            })
            .state('ReceiptContD', {
                url: "/:busnLocationNoHash/Contact/:busnLocationContactId",
                templateUrl: rooturl + "/Receipts/Tmpl?Prefix=ReceiptContD&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContDetailController",
                pageKey: 'ReceiptContD'
            })

            .state('ReceiptTerminals', {
                url: "/:busnLocationNoHash/Terminals",
                templateUrl: rooturl + "/Receipts/Tmpl?Prefix=ReceiptTerminals&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalsController",
                pageKey: 'ReceiptTerminals'
            })
            .state('ReceiptTerminalNew', {
                url: "/:busnLocationNoHash/Terminals/New",
                templateUrl: rooturl + "/Receipts/Tmpl?Prefix=ReceiptTerminalNew&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalNewController",
                pageKey: 'ReceiptTerminalNew'
            })
            .state('ReceiptTerminalInfo', {
                url: "/:busnLocationNoHash/Terminals/:terminalIds",
                templateUrl: rooturl + "/Receipts/Tmpl?Prefix=ReceiptTerminalInfo&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalInfoController",
                pageKey: 'ReceiptTerminalInfo'
            })
            .state('ReceiptTxnPoints', {
                url: "/:busnLocationNoHash/Transactions",
                templateUrl: rooturl + "/Receipts/Tmpl?Prefix=ReceiptTxnPoints&type=Index",
                data: { pageTitle: 'Business Location Transactions' },
                controller: "busnLotTxnPointsController",
                pageKey: 'ReceiptTxnPoints'
            })
            .state('ReceiptCrdRangeAccpt', {
                url: "/:busnLocationNoHash/CardRangeAcceptance",
                templateUrl: rooturl + "/Receipts/Tmpl?Prefix=ReceiptCrdRangeAccpt&type=Index",
                data: { pageTitle: 'Card Range Acceptance' },
                controller: "busnLotCrdRangeAccptController",
                pageKey: 'ReceiptCrdRangeAccpt'
            })
        ;

        ngDialogProvider.setDefaults({
            className: 'ngdialog-theme-default',
            plain: false,
            showClose: true,
            closeByDocument: true,
            closeByEscape: true,
            appendTo: false,
            preCloseCallback: function () {
                console.log('default pre-close callback');
            }
        });
    }]);

    app.run(['$rootScope', '$location', 'Utils', '$route', function ($rootScope, $location, Utils, $route) {
        $rootScope.tables = {};
        $rootScope.obj = {};
        $rootScope.$on('$stateChangeStart', function (e, current, pre) {
            console.log('state change start')
            $.blockUI();
            $rootScope.$broadcast('$stateChanged', current.pageKey);
        });

        $rootScope.$on('$stateChangeSuccess', function (e, current, pre) {
            console.log('route path' + $location.path());
            $.unblockUI();
            var _location = $location.path();
            if (_location === '/new') {
                $rootScope.obj._type = 'new';
                $rootScope.obj.busnLocationNoHash = null;
            } else if (_location === '/') {
                $rootScope.obj._type = 'index';
                $rootScope.obj.busnLocationNoHash = null;
            } else {
                if (pre.busnLocationNoHash) {
                    $rootScope.obj.busnLocationNoHash = pre.busnLocationNoHash;
                }

                if (pre.busnLocationContactId) {
                    $rootScope.obj.busnLocationContactId = pre.busnLocationContactId;
                }

                if (pre.busnLocationAddressId) {
                    $rootScope.obj.busnLocationAddressId = pre.busnLocationAddressId;
                }

                if (pre.terminalIds) {
                    $rootScope.obj.terminalIds = pre.terminalIds;
                }
                
                $rootScope.obj._type = 'edit';
            }
        });

        $rootScope.$on('$viewContentLoaded', function (e, current, pre) {
            console.log('view loaded path' + $location.path());
            App.init();
        });
    }]);
}());