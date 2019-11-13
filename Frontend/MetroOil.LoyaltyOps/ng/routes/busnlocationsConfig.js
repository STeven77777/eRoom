(function () {
    var app = angular.module('loyaltyApp',
        ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'App.Utils', 'ui.router', 'ui.select', 'ngSanitize', 'ngDialog', 'datePicker']);

    app.config(['$stateProvider', '$urlRouterProvider', '$locationProvider', 'ngDialogProvider', function ($stateProvider, $urlRouterProvider, $locationProvider, ngDialogProvider) {
        $locationProvider.html5Mode(false).hashPrefix('');
        var rooturl = $('#hdUrlPrefix').val();

        $stateProvider
            .state('BusnLotSearch', {
                url: "/",
                templateUrl: rooturl + "/BusnLocations/Tmpl?Prefix=BusnLotSearch&type=Index",
                data: { pageTitle: 'Business Locations Search' },
                controller: "busnLotsController",
                pageKey: 'BusnLotSearch'
            })
            .state('BusnLotNew', {
                url: "/New",
                templateUrl: rooturl + "/BusnLocations/Tmpl?Prefix=BusnLotNew&type=Index",
                data: { pageTitle: 'Create Business Locations' },
                controller: "newBusnLotController",
                pageKey: 'BusnLotNew'
            })
            .state('BusnLotInfo', {
                url: "/:busnLocationNoHash/Info",
                templateUrl: rooturl + "/BusnLocations/Tmpl?Prefix=BusnLotInfo&type=Index",
                data: { pageTitle: 'Business Locations Info' },
                controller: "busnLotInfoController",
                pageKey: 'BusnLotInfo'
            })

            .state('BusnLotAddrs', {
                url: "/:busnLocationNoHash/Address",
                templateUrl: rooturl + "/BusnLocations/Tmpl?Prefix=BusnLotAddrs&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrController",
                pageKey: 'BusnLotAddrs'
            })
            .state('BusnLotAddrNew', {
                url: "/:busnLocationNoHash/Address/New",
                templateUrl: rooturl + "/BusnLocations/Tmpl?Prefix=BusnLotAddrNew&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrNewController",
                pageKey: 'BusnLotAddrNew'
            })
            .state('BusnLotAddrD', {
                url: "/:busnLocationNoHash/Address/:busnLocationAddressId",
                templateUrl: rooturl + "/BusnLocations/Tmpl?Prefix=BusnLotAddrD&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrDetailController",
                pageKey: 'BusnLotAddrD'
            })

            .state('BusnLotConts', {
                url: "/:busnLocationNoHash/Contact",
                templateUrl: rooturl + "/BusnLocations/Tmpl?Prefix=BusnLotConts&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContController",
                pageKey: 'BusnLotConts'
            })
            .state('BusnLotContNew', {
                url: "/:busnLocationNoHash/Contact/New",
                templateUrl: rooturl + "/BusnLocations/Tmpl?Prefix=BusnLotContNew&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContNewController",
                pageKey: 'BusnLotContNew'
            })
            .state('BusnLotContD', {
                url: "/:busnLocationNoHash/Contact/:busnLocationContactId",
                templateUrl: rooturl + "/BusnLocations/Tmpl?Prefix=BusnLotContD&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContDetailController",
                pageKey: 'BusnLotContD'
            })

            .state('BusnLotTerminals', {
                url: "/:busnLocationNoHash/Terminals",
                templateUrl: rooturl + "/BusnLocations/Tmpl?Prefix=BusnLotTerminals&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalsController",
                pageKey: 'BusnLotTerminals'
            })
            .state('BusnLotTerminalNew', {
                url: "/:busnLocationNoHash/Terminals/New",
                templateUrl: rooturl + "/BusnLocations/Tmpl?Prefix=BusnLotTerminalNew&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalNewController",
                pageKey: 'BusnLotTerminalNew'
            })
            .state('BusnLotTerminalInfo', {
                url: "/:busnLocationNoHash/Terminals/:terminalIds",
                templateUrl: rooturl + "/BusnLocations/Tmpl?Prefix=BusnLotTerminalInfo&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalInfoController",
                pageKey: 'BusnLotTerminalInfo'
            })
            .state('BusnLotTxnPoints', {
                url: "/:busnLocationNoHash/Transactions",
                templateUrl: rooturl + "/BusnLocations/Tmpl?Prefix=BusnLotTxnPoints&type=Index",
                data: { pageTitle: 'Business Location Transactions' },
                controller: "busnLotTxnPointsController",
                pageKey: 'BusnLotTxnPoints'
            })
            .state('BusnLotCrdRangeAccpt', {
                url: "/:busnLocationNoHash/CardRangeAcceptance",
                templateUrl: rooturl + "/BusnLocations/Tmpl?Prefix=BusnLotCrdRangeAccpt&type=Index",
                data: { pageTitle: 'Card Range Acceptance' },
                controller: "busnLotCrdRangeAccptController",
                pageKey: 'BusnLotCrdRangeAccpt'
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