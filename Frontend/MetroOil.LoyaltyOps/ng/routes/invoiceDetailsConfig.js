(function () {
    var app = angular.module('loyaltyApp',
        ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'App.Utils', 'ui.router', 'ui.select', 'ngSanitize', 'ngDialog', 'datePicker']);

    app.config(['$stateProvider', '$urlRouterProvider', '$locationProvider', 'ngDialogProvider', function ($stateProvider, $urlRouterProvider, $locationProvider, ngDialogProvider) {
        $locationProvider.html5Mode(false).hashPrefix('');
        var rooturl = $('#hdUrlPrefix').val();

        $stateProvider
            .state('InvoiceDetailSearch', {
                url: "/",
                templateUrl: rooturl + "/InvoiceDetails/Tmpl?Prefix=InvoiceDetailSearch&type=Index",
                data: { pageTitle: 'InvoiceDetail Search' },
                controller: "invoiceDetailsController",
                pageKey: 'InvoiceDetailSearch'
            })
            .state('InvoiceDetailInfoNew', {
                url: "/New",
                templateUrl: rooturl + "/InvoiceDetails/Tmpl?Prefix=InvoiceDetailInfoNew&type=Index",
                data: { pageTitle: 'Create Business Locations' },
                controller: "newInvoiceDetailController",
                pageKey: 'InvoiceDetailInfoNew'
            })
            .state('InvoiceDetailInfo', {
                url: "/:busnLocationNoHash/Info",
                templateUrl: rooturl + "/InvoiceDetails/Tmpl?Prefix=InvoiceDetailInfo&type=Index",
                data: { pageTitle: 'Business Locations Info' },
                controller: "invoiceDetailInfoController",
                pageKey: 'InvoiceDetailInfo'
            })

            .state('InvoiceDetailAddrs', {
                url: "/:busnLocationNoHash/Address",
                templateUrl: rooturl + "/InvoiceDetails/Tmpl?Prefix=InvoiceDetailAddrs&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrController",
                pageKey: 'InvoiceDetailAddrs'
            })
            .state('InvoiceDetailAddrNew', {
                url: "/:busnLocationNoHash/Address/New",
                templateUrl: rooturl + "/InvoiceDetails/Tmpl?Prefix=InvoiceDetailAddrNew&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrNewController",
                pageKey: 'InvoiceDetailAddrNew'
            })
            .state('InvoiceDetailAddrD', {
                url: "/:busnLocationNoHash/Address/:busnLocationAddressId",
                templateUrl: rooturl + "/InvoiceDetails/Tmpl?Prefix=InvoiceDetailAddrD&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrDetailController",
                pageKey: 'InvoiceDetailAddrD'
            })

            .state('InvoiceDetailConts', {
                url: "/:busnLocationNoHash/Contact",
                templateUrl: rooturl + "/InvoiceDetails/Tmpl?Prefix=InvoiceDetailConts&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContController",
                pageKey: 'InvoiceDetailConts'
            })
            .state('InvoiceDetailContNew', {
                url: "/:busnLocationNoHash/Contact/New",
                templateUrl: rooturl + "/InvoiceDetails/Tmpl?Prefix=InvoiceDetailContNew&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContNewController",
                pageKey: 'InvoiceDetailContNew'
            })
            .state('InvoiceDetailContD', {
                url: "/:busnLocationNoHash/Contact/:busnLocationContactId",
                templateUrl: rooturl + "/InvoiceDetails/Tmpl?Prefix=InvoiceDetailContD&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContDetailController",
                pageKey: 'InvoiceDetailContD'
            })

            .state('InvoiceDetailTerminals', {
                url: "/:busnLocationNoHash/Terminals",
                templateUrl: rooturl + "/InvoiceDetails/Tmpl?Prefix=InvoiceDetailTerminals&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalsController",
                pageKey: 'InvoiceDetailTerminals'
            })
            .state('InvoiceDetailTerminalNew', {
                url: "/:busnLocationNoHash/Terminals/New",
                templateUrl: rooturl + "/InvoiceDetails/Tmpl?Prefix=InvoiceDetailTerminalNew&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalNewController",
                pageKey: 'InvoiceDetailTerminalNew'
            })
            .state('InvoiceDetailTerminalInfo', {
                url: "/:busnLocationNoHash/Terminals/:terminalIds",
                templateUrl: rooturl + "/InvoiceDetails/Tmpl?Prefix=InvoiceDetailTerminalInfo&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalInfoController",
                pageKey: 'InvoiceDetailTerminalInfo'
            })
            .state('InvoiceDetailTxnPoints', {
                url: "/:busnLocationNoHash/Transactions",
                templateUrl: rooturl + "/InvoiceDetails/Tmpl?Prefix=InvoiceDetailTxnPoints&type=Index",
                data: { pageTitle: 'Business Location Transactions' },
                controller: "busnLotTxnPointsController",
                pageKey: 'InvoiceDetailTxnPoints'
            })
            .state('InvoiceDetailCrdRangeAccpt', {
                url: "/:busnLocationNoHash/CardRangeAcceptance",
                templateUrl: rooturl + "/InvoiceDetails/Tmpl?Prefix=InvoiceDetailCrdRangeAccpt&type=Index",
                data: { pageTitle: 'Card Range Acceptance' },
                controller: "busnLotCrdRangeAccptController",
                pageKey: 'InvoiceDetailCrdRangeAccpt'
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