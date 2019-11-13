(function () {
    var app = angular.module('loyaltyApp',
        ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'App.Utils', 'ui.router', 'ui.select', 'ngSanitize', 'ngDialog', 'datePicker']);

    app.config(['$stateProvider', '$urlRouterProvider', '$locationProvider', 'ngDialogProvider', function ($stateProvider, $urlRouterProvider, $locationProvider, ngDialogProvider) {
        $locationProvider.html5Mode(false).hashPrefix('');
        var rooturl = $('#hdUrlPrefix').val();

        $stateProvider
            .state('ServiceSearch', {
                url: "/",
                templateUrl: rooturl + "/Services/Tmpl?Prefix=ServiceSearch&type=Index",
                data: { pageTitle: 'Service Search' },
                controller: "servicesController",
                pageKey: 'ServiceSearch'
            })
            .state('ServiceInfoNew', {
                url: "/New",
                templateUrl: rooturl + "/Services/Tmpl?Prefix=ServiceInfoNew&type=Index",
                data: { pageTitle: 'Create Business Locations' },
                controller: "newServiceController",
                pageKey: 'ServiceInfoNew'
            })
            .state('ServiceInfo', {
                url: "/:busnLocationNoHash/Info",
                templateUrl: rooturl + "/Services/Tmpl?Prefix=ServiceInfo&type=Index",
                data: { pageTitle: 'Business Locations Info' },
                controller: "serviceInfoController",
                pageKey: 'ServiceInfo'
            })

            .state('ServiceAddrs', {
                url: "/:busnLocationNoHash/Address",
                templateUrl: rooturl + "/Services/Tmpl?Prefix=ServiceAddrs&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrController",
                pageKey: 'ServiceAddrs'
            })
            .state('ServiceAddrNew', {
                url: "/:busnLocationNoHash/Address/New",
                templateUrl: rooturl + "/Services/Tmpl?Prefix=ServiceAddrNew&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrNewController",
                pageKey: 'ServiceAddrNew'
            })
            .state('ServiceAddrD', {
                url: "/:busnLocationNoHash/Address/:busnLocationAddressId",
                templateUrl: rooturl + "/Services/Tmpl?Prefix=ServiceAddrD&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrDetailController",
                pageKey: 'ServiceAddrD'
            })

            .state('ServiceConts', {
                url: "/:busnLocationNoHash/Contact",
                templateUrl: rooturl + "/Services/Tmpl?Prefix=ServiceConts&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContController",
                pageKey: 'ServiceConts'
            })
            .state('ServiceContNew', {
                url: "/:busnLocationNoHash/Contact/New",
                templateUrl: rooturl + "/Services/Tmpl?Prefix=ServiceContNew&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContNewController",
                pageKey: 'ServiceContNew'
            })
            .state('ServiceContD', {
                url: "/:busnLocationNoHash/Contact/:busnLocationContactId",
                templateUrl: rooturl + "/Services/Tmpl?Prefix=ServiceContD&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContDetailController",
                pageKey: 'ServiceContD'
            })

            .state('ServiceTerminals', {
                url: "/:busnLocationNoHash/Terminals",
                templateUrl: rooturl + "/Services/Tmpl?Prefix=ServiceTerminals&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalsController",
                pageKey: 'ServiceTerminals'
            })
            .state('ServiceTerminalNew', {
                url: "/:busnLocationNoHash/Terminals/New",
                templateUrl: rooturl + "/Services/Tmpl?Prefix=ServiceTerminalNew&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalNewController",
                pageKey: 'ServiceTerminalNew'
            })
            .state('ServiceTerminalInfo', {
                url: "/:busnLocationNoHash/Terminals/:terminalIds",
                templateUrl: rooturl + "/Services/Tmpl?Prefix=ServiceTerminalInfo&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalInfoController",
                pageKey: 'ServiceTerminalInfo'
            })
            .state('ServiceTxnPoints', {
                url: "/:busnLocationNoHash/Transactions",
                templateUrl: rooturl + "/Services/Tmpl?Prefix=ServiceTxnPoints&type=Index",
                data: { pageTitle: 'Business Location Transactions' },
                controller: "busnLotTxnPointsController",
                pageKey: 'ServiceTxnPoints'
            })
            .state('ServiceCrdRangeAccpt', {
                url: "/:busnLocationNoHash/CardRangeAcceptance",
                templateUrl: rooturl + "/Services/Tmpl?Prefix=ServiceCrdRangeAccpt&type=Index",
                data: { pageTitle: 'Card Range Acceptance' },
                controller: "busnLotCrdRangeAccptController",
                pageKey: 'ServiceCrdRangeAccpt'
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