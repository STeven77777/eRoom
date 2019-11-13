(function () {
    var app = angular.module('loyaltyApp',
        ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'App.Utils', 'ui.router', 'ui.select', 'ngSanitize', 'ngDialog', 'datePicker']);

    app.config(['$stateProvider', '$urlRouterProvider', '$locationProvider', 'ngDialogProvider', function ($stateProvider, $urlRouterProvider, $locationProvider, ngDialogProvider) {
        $locationProvider.html5Mode(false).hashPrefix('');
        var rooturl = $('#hdUrlPrefix').val();

        $stateProvider
            .state('MotorTypeSearch', {
                url: "/",
                templateUrl: rooturl + "/MotorTypes/Tmpl?Prefix=MotorTypeSearch&type=Index",
                data: { pageTitle: 'MotorType Search' },
                controller: "motorTypesController",
                pageKey: 'MotorTypeSearch'
            })
            .state('MotorTypeInfoNew', {
                url: "/New",
                templateUrl: rooturl + "/MotorTypes/Tmpl?Prefix=MotorTypeInfoNew&type=Index",
                data: { pageTitle: 'Create Business Locations' },
                controller: "newMotorTypeController",
                pageKey: 'MotorTypeInfoNew'
            })
            .state('MotorTypeInfo', {
                url: "/:busnLocationNoHash/Info",
                templateUrl: rooturl + "/MotorTypes/Tmpl?Prefix=MotorTypeInfo&type=Index",
                data: { pageTitle: 'Business Locations Info' },
                controller: "motorTypeInfoController",
                pageKey: 'MotorTypeInfo'
            })

            .state('MotorTypeAddrs', {
                url: "/:busnLocationNoHash/Address",
                templateUrl: rooturl + "/MotorTypes/Tmpl?Prefix=MotorTypeAddrs&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrController",
                pageKey: 'MotorTypeAddrs'
            })
            .state('MotorTypeAddrNew', {
                url: "/:busnLocationNoHash/Address/New",
                templateUrl: rooturl + "/MotorTypes/Tmpl?Prefix=MotorTypeAddrNew&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrNewController",
                pageKey: 'MotorTypeAddrNew'
            })
            .state('MotorTypeAddrD', {
                url: "/:busnLocationNoHash/Address/:busnLocationAddressId",
                templateUrl: rooturl + "/MotorTypes/Tmpl?Prefix=MotorTypeAddrD&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrDetailController",
                pageKey: 'MotorTypeAddrD'
            })

            .state('MotorTypeConts', {
                url: "/:busnLocationNoHash/Contact",
                templateUrl: rooturl + "/MotorTypes/Tmpl?Prefix=MotorTypeConts&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContController",
                pageKey: 'MotorTypeConts'
            })
            .state('MotorTypeContNew', {
                url: "/:busnLocationNoHash/Contact/New",
                templateUrl: rooturl + "/MotorTypes/Tmpl?Prefix=MotorTypeContNew&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContNewController",
                pageKey: 'MotorTypeContNew'
            })
            .state('MotorTypeContD', {
                url: "/:busnLocationNoHash/Contact/:busnLocationContactId",
                templateUrl: rooturl + "/MotorTypes/Tmpl?Prefix=MotorTypeContD&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContDetailController",
                pageKey: 'MotorTypeContD'
            })

            .state('MotorTypeTerminals', {
                url: "/:busnLocationNoHash/Terminals",
                templateUrl: rooturl + "/MotorTypes/Tmpl?Prefix=MotorTypeTerminals&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalsController",
                pageKey: 'MotorTypeTerminals'
            })
            .state('MotorTypeTerminalNew', {
                url: "/:busnLocationNoHash/Terminals/New",
                templateUrl: rooturl + "/MotorTypes/Tmpl?Prefix=MotorTypeTerminalNew&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalNewController",
                pageKey: 'MotorTypeTerminalNew'
            })
            .state('MotorTypeTerminalInfo', {
                url: "/:busnLocationNoHash/Terminals/:terminalIds",
                templateUrl: rooturl + "/MotorTypes/Tmpl?Prefix=MotorTypeTerminalInfo&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalInfoController",
                pageKey: 'MotorTypeTerminalInfo'
            })
            .state('MotorTypeTxnPoints', {
                url: "/:busnLocationNoHash/Transactions",
                templateUrl: rooturl + "/MotorTypes/Tmpl?Prefix=MotorTypeTxnPoints&type=Index",
                data: { pageTitle: 'Business Location Transactions' },
                controller: "busnLotTxnPointsController",
                pageKey: 'MotorTypeTxnPoints'
            })
            .state('MotorTypeCrdRangeAccpt', {
                url: "/:busnLocationNoHash/CardRangeAcceptance",
                templateUrl: rooturl + "/MotorTypes/Tmpl?Prefix=MotorTypeCrdRangeAccpt&type=Index",
                data: { pageTitle: 'Card Range Acceptance' },
                controller: "busnLotCrdRangeAccptController",
                pageKey: 'MotorTypeCrdRangeAccpt'
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