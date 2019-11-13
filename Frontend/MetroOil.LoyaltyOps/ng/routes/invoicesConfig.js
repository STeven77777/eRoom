(function () {
    var app = angular.module('loyaltyApp',
        ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'App.Utils', 'ui.router', 'ui.select', 'ngSanitize', 'ngDialog', 'datePicker']);

    app.config(['$stateProvider', '$urlRouterProvider', '$locationProvider', 'ngDialogProvider', function ($stateProvider, $urlRouterProvider, $locationProvider, ngDialogProvider) {
        $locationProvider.html5Mode(false).hashPrefix('');
        var rooturl = $('#hdUrlPrefix').val();

        $stateProvider
            .state('InvoiceSearch', {
                url: "/",
                templateUrl: rooturl + "/Invoices/Tmpl?Prefix=InvoiceSearch&type=Index",
                data: { pageTitle: 'Invoice Search' },
                controller: "invoicesController",
                pageKey: 'InvoiceSearch'
            })
            .state('InvoiceInfoNew', {
                url: "/New",
                templateUrl: rooturl + "/Invoices/Tmpl?Prefix=InvoiceInfoNew&type=Index",
                data: { pageTitle: 'Create Business Locations' },
                controller: "newInvoiceController",
                pageKey: 'InvoiceInfoNew'
            })
            .state('InvoiceInfo', {
                url: "/:busnLocationNoHash/Info",
                templateUrl: rooturl + "/Invoices/Tmpl?Prefix=InvoiceInfo&type=Index",
                data: { pageTitle: 'Business Locations Info' },
                controller: "invoiceInfoController",
                pageKey: 'InvoiceInfo'
            })

            .state('InvoiceAddrs', {
                url: "/:busnLocationNoHash/Address",
                templateUrl: rooturl + "/Invoices/Tmpl?Prefix=InvoiceAddrs&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrController",
                pageKey: 'InvoiceAddrs'
            })
            .state('InvoiceAddrNew', {
                url: "/:busnLocationNoHash/Address/New",
                templateUrl: rooturl + "/Invoices/Tmpl?Prefix=InvoiceAddrNew&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrNewController",
                pageKey: 'InvoiceAddrNew'
            })
            .state('InvoiceAddrD', {
                url: "/:busnLocationNoHash/Address/:busnLocationAddressId",
                templateUrl: rooturl + "/Invoices/Tmpl?Prefix=InvoiceAddrD&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrDetailController",
                pageKey: 'InvoiceAddrD'
            })

            .state('InvoiceConts', {
                url: "/:busnLocationNoHash/Contact",
                templateUrl: rooturl + "/Invoices/Tmpl?Prefix=InvoiceConts&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContController",
                pageKey: 'InvoiceConts'
            })
            .state('InvoiceContNew', {
                url: "/:busnLocationNoHash/Contact/New",
                templateUrl: rooturl + "/Invoices/Tmpl?Prefix=InvoiceContNew&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContNewController",
                pageKey: 'InvoiceContNew'
            })
            .state('InvoiceContD', {
                url: "/:busnLocationNoHash/Contact/:busnLocationContactId",
                templateUrl: rooturl + "/Invoices/Tmpl?Prefix=InvoiceContD&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContDetailController",
                pageKey: 'InvoiceContD'
            })

            .state('InvoiceTerminals', {
                url: "/:busnLocationNoHash/Terminals",
                templateUrl: rooturl + "/Invoices/Tmpl?Prefix=InvoiceTerminals&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalsController",
                pageKey: 'InvoiceTerminals'
            })
            .state('InvoiceTerminalNew', {
                url: "/:busnLocationNoHash/Terminals/New",
                templateUrl: rooturl + "/Invoices/Tmpl?Prefix=InvoiceTerminalNew&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalNewController",
                pageKey: 'InvoiceTerminalNew'
            })
            .state('InvoiceTerminalInfo', {
                url: "/:busnLocationNoHash/Terminals/:terminalIds",
                templateUrl: rooturl + "/Invoices/Tmpl?Prefix=InvoiceTerminalInfo&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalInfoController",
                pageKey: 'InvoiceTerminalInfo'
            })
            .state('InvoiceTxnPoints', {
                url: "/:busnLocationNoHash/Transactions",
                templateUrl: rooturl + "/Invoices/Tmpl?Prefix=InvoiceTxnPoints&type=Index",
                data: { pageTitle: 'Business Location Transactions' },
                controller: "busnLotTxnPointsController",
                pageKey: 'InvoiceTxnPoints'
            })
            .state('InvoiceCrdRangeAccpt', {
                url: "/:busnLocationNoHash/CardRangeAcceptance",
                templateUrl: rooturl + "/Invoices/Tmpl?Prefix=InvoiceCrdRangeAccpt&type=Index",
                data: { pageTitle: 'Card Range Acceptance' },
                controller: "busnLotCrdRangeAccptController",
                pageKey: 'InvoiceCrdRangeAccpt'
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