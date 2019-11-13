(function () {
    var app = angular.module('loyaltyApp',
        ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'App.Utils', 'ui.router', 'ui.select', 'ngSanitize', 'ngDialog', 'datePicker']);

    app.config(['$stateProvider', '$urlRouterProvider', '$locationProvider', 'ngDialogProvider', function ($stateProvider, $urlRouterProvider, $locationProvider, ngDialogProvider) {
        $locationProvider.html5Mode(false).hashPrefix('');
        var rooturl = $('#hdUrlPrefix').val();

        $stateProvider
            .state('RoomTypeSearch', {
                url: "/",
                templateUrl: rooturl + "/RoomTypes/Tmpl?Prefix=RoomTypeSearch&type=Index",
                data: { pageTitle: 'RoomType Search' },
                controller: "roomTypesController",
                pageKey: 'RoomTypeSearch'
            })
            .state('RoomTypeInfoNew', {
                url: "/New",
                templateUrl: rooturl + "/RoomTypes/Tmpl?Prefix=RoomTypeInfoNew&type=Index",
                data: { pageTitle: 'Create Business Locations' },
                controller: "newRoomTypeController",
                pageKey: 'RoomTypeInfoNew'
            })
            .state('RoomTypeInfo', {
                url: "/:busnLocationNoHash/Info",
                templateUrl: rooturl + "/RoomTypes/Tmpl?Prefix=RoomTypeInfo&type=Index",
                data: { pageTitle: 'Business Locations Info' },
                controller: "roomTypeInfoController",
                pageKey: 'RoomTypeInfo'
            })

            .state('RoomTypeAddrs', {
                url: "/:busnLocationNoHash/Address",
                templateUrl: rooturl + "/RoomTypes/Tmpl?Prefix=RoomTypeAddrs&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrController",
                pageKey: 'RoomTypeAddrs'
            })
            .state('RoomTypeAddrNew', {
                url: "/:busnLocationNoHash/Address/New",
                templateUrl: rooturl + "/RoomTypes/Tmpl?Prefix=RoomTypeAddrNew&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrNewController",
                pageKey: 'RoomTypeAddrNew'
            })
            .state('RoomTypeAddrD', {
                url: "/:busnLocationNoHash/Address/:busnLocationAddressId",
                templateUrl: rooturl + "/RoomTypes/Tmpl?Prefix=RoomTypeAddrD&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrDetailController",
                pageKey: 'RoomTypeAddrD'
            })

            .state('RoomTypeConts', {
                url: "/:busnLocationNoHash/Contact",
                templateUrl: rooturl + "/RoomTypes/Tmpl?Prefix=RoomTypeConts&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContController",
                pageKey: 'RoomTypeConts'
            })
            .state('RoomTypeContNew', {
                url: "/:busnLocationNoHash/Contact/New",
                templateUrl: rooturl + "/RoomTypes/Tmpl?Prefix=RoomTypeContNew&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContNewController",
                pageKey: 'RoomTypeContNew'
            })
            .state('RoomTypeContD', {
                url: "/:busnLocationNoHash/Contact/:busnLocationContactId",
                templateUrl: rooturl + "/RoomTypes/Tmpl?Prefix=RoomTypeContD&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContDetailController",
                pageKey: 'RoomTypeContD'
            })

            .state('RoomTypeTerminals', {
                url: "/:busnLocationNoHash/Terminals",
                templateUrl: rooturl + "/RoomTypes/Tmpl?Prefix=RoomTypeTerminals&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalsController",
                pageKey: 'RoomTypeTerminals'
            })
            .state('RoomTypeTerminalNew', {
                url: "/:busnLocationNoHash/Terminals/New",
                templateUrl: rooturl + "/RoomTypes/Tmpl?Prefix=RoomTypeTerminalNew&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalNewController",
                pageKey: 'RoomTypeTerminalNew'
            })
            .state('RoomTypeTerminalInfo', {
                url: "/:busnLocationNoHash/Terminals/:terminalIds",
                templateUrl: rooturl + "/RoomTypes/Tmpl?Prefix=RoomTypeTerminalInfo&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalInfoController",
                pageKey: 'RoomTypeTerminalInfo'
            })
            .state('RoomTypeTxnPoints', {
                url: "/:busnLocationNoHash/Transactions",
                templateUrl: rooturl + "/RoomTypes/Tmpl?Prefix=RoomTypeTxnPoints&type=Index",
                data: { pageTitle: 'Business Location Transactions' },
                controller: "busnLotTxnPointsController",
                pageKey: 'RoomTypeTxnPoints'
            })
            .state('RoomTypeCrdRangeAccpt', {
                url: "/:busnLocationNoHash/CardRangeAcceptance",
                templateUrl: rooturl + "/RoomTypes/Tmpl?Prefix=RoomTypeCrdRangeAccpt&type=Index",
                data: { pageTitle: 'Card Range Acceptance' },
                controller: "busnLotCrdRangeAccptController",
                pageKey: 'RoomTypeCrdRangeAccpt'
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