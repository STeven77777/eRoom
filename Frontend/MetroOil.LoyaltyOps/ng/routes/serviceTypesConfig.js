(function () {
    var app = angular.module('loyaltyApp',
        ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'App.Utils', 'ui.router', 'ui.select', 'ngSanitize', 'ngDialog', 'datePicker']);

    app.config(['$stateProvider', '$urlRouterProvider', '$locationProvider', 'ngDialogProvider', function ($stateProvider, $urlRouterProvider, $locationProvider, ngDialogProvider) {
        $locationProvider.html5Mode(false).hashPrefix('');
        var rooturl = $('#hdUrlPrefix').val();

        $stateProvider
            .state('ServiceTypeSearch', {
                url: "/",
                templateUrl: rooturl + "/ServiceTypes/Tmpl?Prefix=ServiceTypeSearch&type=Index",
                data: { pageTitle: 'ServiceType Search' },
                controller: "serviceTypesController",
                pageKey: 'ServiceTypeSearch'
            })
            .state('ServiceTypeInfoNew', {
                url: "/New",
                templateUrl: rooturl + "/ServiceTypes/Tmpl?Prefix=ServiceTypeInfoNew&type=Index",
                data: { pageTitle: 'Create Business Locations' },
                controller: "newServiceTypeController",
                pageKey: 'ServiceTypeInfoNew'
            })
            .state('ServiceTypeInfo', {
                url: "/:busnLocationNoHash/Info",
                templateUrl: rooturl + "/ServiceTypes/Tmpl?Prefix=ServiceTypeInfo&type=Index",
                data: { pageTitle: 'Business Locations Info' },
                controller: "serviceTypeInfoController",
                pageKey: 'ServiceTypeInfo'
            })

            .state('ServiceTypeAddrs', {
                url: "/:busnLocationNoHash/Address",
                templateUrl: rooturl + "/ServiceTypes/Tmpl?Prefix=ServiceTypeAddrs&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrController",
                pageKey: 'ServiceTypeAddrs'
            })
            .state('ServiceTypeAddrNew', {
                url: "/:busnLocationNoHash/Address/New",
                templateUrl: rooturl + "/ServiceTypes/Tmpl?Prefix=ServiceTypeAddrNew&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrNewController",
                pageKey: 'ServiceTypeAddrNew'
            })
            .state('ServiceTypeAddrD', {
                url: "/:busnLocationNoHash/Address/:busnLocationAddressId",
                templateUrl: rooturl + "/ServiceTypes/Tmpl?Prefix=ServiceTypeAddrD&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrDetailController",
                pageKey: 'ServiceTypeAddrD'
            })

            .state('ServiceTypeConts', {
                url: "/:busnLocationNoHash/Contact",
                templateUrl: rooturl + "/ServiceTypes/Tmpl?Prefix=ServiceTypeConts&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContController",
                pageKey: 'ServiceTypeConts'
            })
            .state('ServiceTypeContNew', {
                url: "/:busnLocationNoHash/Contact/New",
                templateUrl: rooturl + "/ServiceTypes/Tmpl?Prefix=ServiceTypeContNew&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContNewController",
                pageKey: 'ServiceTypeContNew'
            })
            .state('ServiceTypeContD', {
                url: "/:busnLocationNoHash/Contact/:busnLocationContactId",
                templateUrl: rooturl + "/ServiceTypes/Tmpl?Prefix=ServiceTypeContD&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContDetailController",
                pageKey: 'ServiceTypeContD'
            })

            .state('ServiceTypeTerminals', {
                url: "/:busnLocationNoHash/Terminals",
                templateUrl: rooturl + "/ServiceTypes/Tmpl?Prefix=ServiceTypeTerminals&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalsController",
                pageKey: 'ServiceTypeTerminals'
            })
            .state('ServiceTypeTerminalNew', {
                url: "/:busnLocationNoHash/Terminals/New",
                templateUrl: rooturl + "/ServiceTypes/Tmpl?Prefix=ServiceTypeTerminalNew&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalNewController",
                pageKey: 'ServiceTypeTerminalNew'
            })
            .state('ServiceTypeTerminalInfo', {
                url: "/:busnLocationNoHash/Terminals/:terminalIds",
                templateUrl: rooturl + "/ServiceTypes/Tmpl?Prefix=ServiceTypeTerminalInfo&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalInfoController",
                pageKey: 'ServiceTypeTerminalInfo'
            })
            .state('ServiceTypeTxnPoints', {
                url: "/:busnLocationNoHash/Transactions",
                templateUrl: rooturl + "/ServiceTypes/Tmpl?Prefix=ServiceTypeTxnPoints&type=Index",
                data: { pageTitle: 'Business Location Transactions' },
                controller: "busnLotTxnPointsController",
                pageKey: 'ServiceTypeTxnPoints'
            })
            .state('ServiceTypeCrdRangeAccpt', {
                url: "/:busnLocationNoHash/CardRangeAcceptance",
                templateUrl: rooturl + "/ServiceTypes/Tmpl?Prefix=ServiceTypeCrdRangeAccpt&type=Index",
                data: { pageTitle: 'Card Range Acceptance' },
                controller: "busnLotCrdRangeAccptController",
                pageKey: 'ServiceTypeCrdRangeAccpt'
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