(function () {
    var app = angular.module('loyaltyApp',
        ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'App.Utils', 'ui.router', 'ui.select', 'ngSanitize', 'ngDialog', 'datePicker']);

    app.config(['$stateProvider', '$urlRouterProvider', '$locationProvider', 'ngDialogProvider', function ($stateProvider, $urlRouterProvider, $locationProvider, ngDialogProvider) {
        $locationProvider.html5Mode(false).hashPrefix('');
        var rooturl = $('#hdUrlPrefix').val();

        $stateProvider
            .state('WaterUsingSearch', {
                url: "/",
                templateUrl: rooturl + "/WaterUsings/Tmpl?Prefix=WaterUsingSearch&type=Index",
                data: { pageTitle: 'WaterUsing Search' },
                controller: "waterUsingsController",
                pageKey: 'WaterUsingSearch'
            })
            .state('WaterUsingInfoNew', {
                url: "/New",
                templateUrl: rooturl + "/WaterUsings/Tmpl?Prefix=WaterUsingInfoNew&type=Index",
                data: { pageTitle: 'Create Business Locations' },
                controller: "newWaterUsingController",
                pageKey: 'WaterUsingInfoNew'
            })
            .state('WaterUsingInfo', {
                url: "/:busnLocationNoHash/Info",
                templateUrl: rooturl + "/WaterUsings/Tmpl?Prefix=WaterUsingInfo&type=Index",
                data: { pageTitle: 'Business Locations Info' },
                controller: "waterUsingInfoController",
                pageKey: 'WaterUsingInfo'
            })

            .state('WaterUsingAddrs', {
                url: "/:busnLocationNoHash/Address",
                templateUrl: rooturl + "/WaterUsings/Tmpl?Prefix=WaterUsingAddrs&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrController",
                pageKey: 'WaterUsingAddrs'
            })
            .state('WaterUsingAddrNew', {
                url: "/:busnLocationNoHash/Address/New",
                templateUrl: rooturl + "/WaterUsings/Tmpl?Prefix=WaterUsingAddrNew&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrNewController",
                pageKey: 'WaterUsingAddrNew'
            })
            .state('WaterUsingAddrD', {
                url: "/:busnLocationNoHash/Address/:busnLocationAddressId",
                templateUrl: rooturl + "/WaterUsings/Tmpl?Prefix=WaterUsingAddrD&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrDetailController",
                pageKey: 'WaterUsingAddrD'
            })

            .state('WaterUsingConts', {
                url: "/:busnLocationNoHash/Contact",
                templateUrl: rooturl + "/WaterUsings/Tmpl?Prefix=WaterUsingConts&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContController",
                pageKey: 'WaterUsingConts'
            })
            .state('WaterUsingContNew', {
                url: "/:busnLocationNoHash/Contact/New",
                templateUrl: rooturl + "/WaterUsings/Tmpl?Prefix=WaterUsingContNew&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContNewController",
                pageKey: 'WaterUsingContNew'
            })
            .state('WaterUsingContD', {
                url: "/:busnLocationNoHash/Contact/:busnLocationContactId",
                templateUrl: rooturl + "/WaterUsings/Tmpl?Prefix=WaterUsingContD&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContDetailController",
                pageKey: 'WaterUsingContD'
            })

            .state('WaterUsingTerminals', {
                url: "/:busnLocationNoHash/Terminals",
                templateUrl: rooturl + "/WaterUsings/Tmpl?Prefix=WaterUsingTerminals&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalsController",
                pageKey: 'WaterUsingTerminals'
            })
            .state('WaterUsingTerminalNew', {
                url: "/:busnLocationNoHash/Terminals/New",
                templateUrl: rooturl + "/WaterUsings/Tmpl?Prefix=WaterUsingTerminalNew&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalNewController",
                pageKey: 'WaterUsingTerminalNew'
            })
            .state('WaterUsingTerminalInfo', {
                url: "/:busnLocationNoHash/Terminals/:terminalIds",
                templateUrl: rooturl + "/WaterUsings/Tmpl?Prefix=WaterUsingTerminalInfo&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalInfoController",
                pageKey: 'WaterUsingTerminalInfo'
            })
            .state('WaterUsingTxnPoints', {
                url: "/:busnLocationNoHash/Transactions",
                templateUrl: rooturl + "/WaterUsings/Tmpl?Prefix=WaterUsingTxnPoints&type=Index",
                data: { pageTitle: 'Business Location Transactions' },
                controller: "busnLotTxnPointsController",
                pageKey: 'WaterUsingTxnPoints'
            })
            .state('WaterUsingCrdRangeAccpt', {
                url: "/:busnLocationNoHash/CardRangeAcceptance",
                templateUrl: rooturl + "/WaterUsings/Tmpl?Prefix=WaterUsingCrdRangeAccpt&type=Index",
                data: { pageTitle: 'Card Range Acceptance' },
                controller: "busnLotCrdRangeAccptController",
                pageKey: 'WaterUsingCrdRangeAccpt'
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