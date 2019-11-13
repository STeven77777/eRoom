(function () {
    var app = angular.module('loyaltyApp',
        ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'App.Utils', 'ui.router', 'ui.select', 'ngSanitize', 'ngDialog', 'datePicker']);

    app.config(['$stateProvider', '$urlRouterProvider', '$locationProvider', 'ngDialogProvider', function ($stateProvider, $urlRouterProvider, $locationProvider, ngDialogProvider) {
        $locationProvider.html5Mode(false).hashPrefix('');
        var rooturl = $('#hdUrlPrefix').val();

        $stateProvider
            .state('ElectricUsingSearch', {
                url: "/",
                templateUrl: rooturl + "/ElectricUsings/Tmpl?Prefix=ElectricUsingSearch&type=Index",
                data: { pageTitle: 'ElectricUsing Search' },
                controller: "electricUsingsController",
                pageKey: 'ElectricUsingSearch'
            })
            .state('ElectricUsingInfoNew', {
                url: "/New",
                templateUrl: rooturl + "/ElectricUsings/Tmpl?Prefix=ElectricUsingInfoNew&type=Index",
                data: { pageTitle: 'Create Business Locations' },
                controller: "newElectricUsingController",
                pageKey: 'ElectricUsingInfoNew'
            })
            .state('ElectricUsingInfo', {
                url: "/:busnLocationNoHash/Info",
                templateUrl: rooturl + "/ElectricUsings/Tmpl?Prefix=ElectricUsingInfo&type=Index",
                data: { pageTitle: 'Business Locations Info' },
                controller: "electricUsingInfoController",
                pageKey: 'ElectricUsingInfo'
            })

            .state('ElectricUsingAddrs', {
                url: "/:busnLocationNoHash/Address",
                templateUrl: rooturl + "/ElectricUsings/Tmpl?Prefix=ElectricUsingAddrs&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrController",
                pageKey: 'ElectricUsingAddrs'
            })
            .state('ElectricUsingAddrNew', {
                url: "/:busnLocationNoHash/Address/New",
                templateUrl: rooturl + "/ElectricUsings/Tmpl?Prefix=ElectricUsingAddrNew&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrNewController",
                pageKey: 'ElectricUsingAddrNew'
            })
            .state('ElectricUsingAddrD', {
                url: "/:busnLocationNoHash/Address/:busnLocationAddressId",
                templateUrl: rooturl + "/ElectricUsings/Tmpl?Prefix=ElectricUsingAddrD&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrDetailController",
                pageKey: 'ElectricUsingAddrD'
            })

            .state('ElectricUsingConts', {
                url: "/:busnLocationNoHash/Contact",
                templateUrl: rooturl + "/ElectricUsings/Tmpl?Prefix=ElectricUsingConts&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContController",
                pageKey: 'ElectricUsingConts'
            })
            .state('ElectricUsingContNew', {
                url: "/:busnLocationNoHash/Contact/New",
                templateUrl: rooturl + "/ElectricUsings/Tmpl?Prefix=ElectricUsingContNew&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContNewController",
                pageKey: 'ElectricUsingContNew'
            })
            .state('ElectricUsingContD', {
                url: "/:busnLocationNoHash/Contact/:busnLocationContactId",
                templateUrl: rooturl + "/ElectricUsings/Tmpl?Prefix=ElectricUsingContD&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContDetailController",
                pageKey: 'ElectricUsingContD'
            })

            .state('ElectricUsingTerminals', {
                url: "/:busnLocationNoHash/Terminals",
                templateUrl: rooturl + "/ElectricUsings/Tmpl?Prefix=ElectricUsingTerminals&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalsController",
                pageKey: 'ElectricUsingTerminals'
            })
            .state('ElectricUsingTerminalNew', {
                url: "/:busnLocationNoHash/Terminals/New",
                templateUrl: rooturl + "/ElectricUsings/Tmpl?Prefix=ElectricUsingTerminalNew&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalNewController",
                pageKey: 'ElectricUsingTerminalNew'
            })
            .state('ElectricUsingTerminalInfo', {
                url: "/:busnLocationNoHash/Terminals/:terminalIds",
                templateUrl: rooturl + "/ElectricUsings/Tmpl?Prefix=ElectricUsingTerminalInfo&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalInfoController",
                pageKey: 'ElectricUsingTerminalInfo'
            })
            .state('ElectricUsingTxnPoints', {
                url: "/:busnLocationNoHash/Transactions",
                templateUrl: rooturl + "/ElectricUsings/Tmpl?Prefix=ElectricUsingTxnPoints&type=Index",
                data: { pageTitle: 'Business Location Transactions' },
                controller: "busnLotTxnPointsController",
                pageKey: 'ElectricUsingTxnPoints'
            })
            .state('ElectricUsingCrdRangeAccpt', {
                url: "/:busnLocationNoHash/CardRangeAcceptance",
                templateUrl: rooturl + "/ElectricUsings/Tmpl?Prefix=ElectricUsingCrdRangeAccpt&type=Index",
                data: { pageTitle: 'Card Range Acceptance' },
                controller: "busnLotCrdRangeAccptController",
                pageKey: 'ElectricUsingCrdRangeAccpt'
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