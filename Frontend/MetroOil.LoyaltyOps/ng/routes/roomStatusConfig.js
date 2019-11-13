(function () {
    var app = angular.module('loyaltyApp',
        ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'App.Utils', 'ui.router', 'ui.select', 'ngSanitize', 'ngDialog', 'datePicker']);

    app.config(['$stateProvider', '$urlRouterProvider', '$locationProvider', 'ngDialogProvider', function ($stateProvider, $urlRouterProvider, $locationProvider, ngDialogProvider) {
        $locationProvider.html5Mode(false).hashPrefix('');
        var rooturl = $('#hdUrlPrefix').val();

        $stateProvider
            .state('RoomStatusSearch', {
                url: "/",
                templateUrl: rooturl + "/RoomStatus/Tmpl?Prefix=RoomStatusSearch&type=Index",
                data: { pageTitle: 'RoomStatus Search' },
                controller: "roomStatusController",
                pageKey: 'RoomStatusSearch'
            })
            .state('RoomStatusInfoNew', {
                url: "/New",
                templateUrl: rooturl + "/RoomStatus/Tmpl?Prefix=RoomStatusInfoNew&type=Index",
                data: { pageTitle: 'Create Business Locations' },
                controller: "newRoomStatusController",
                pageKey: 'RoomStatusInfoNew'
            })
            .state('RoomStatusInfo', {
                url: "/:busnLocationNoHash/Info",
                templateUrl: rooturl + "/RoomStatus/Tmpl?Prefix=RoomStatusInfo&type=Index",
                data: { pageTitle: 'Business Locations Info' },
                controller: "roomStatusInfoController",
                pageKey: 'RoomStatusInfo'
            })

            .state('RoomStatusAddrs', {
                url: "/:busnLocationNoHash/Address",
                templateUrl: rooturl + "/RoomStatus/Tmpl?Prefix=RoomStatusAddrs&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrController",
                pageKey: 'RoomStatusAddrs'
            })
            .state('RoomStatusAddrNew', {
                url: "/:busnLocationNoHash/Address/New",
                templateUrl: rooturl + "/RoomStatus/Tmpl?Prefix=RoomStatusAddrNew&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrNewController",
                pageKey: 'RoomStatusAddrNew'
            })
            .state('RoomStatusAddrD', {
                url: "/:busnLocationNoHash/Address/:busnLocationAddressId",
                templateUrl: rooturl + "/RoomStatus/Tmpl?Prefix=RoomStatusAddrD&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrDetailController",
                pageKey: 'RoomStatusAddrD'
            })

            .state('RoomStatusConts', {
                url: "/:busnLocationNoHash/Contact",
                templateUrl: rooturl + "/RoomStatus/Tmpl?Prefix=RoomStatusConts&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContController",
                pageKey: 'RoomStatusConts'
            })
            .state('RoomStatusContNew', {
                url: "/:busnLocationNoHash/Contact/New",
                templateUrl: rooturl + "/RoomStatus/Tmpl?Prefix=RoomStatusContNew&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContNewController",
                pageKey: 'RoomStatusContNew'
            })
            .state('RoomStatusContD', {
                url: "/:busnLocationNoHash/Contact/:busnLocationContactId",
                templateUrl: rooturl + "/RoomStatus/Tmpl?Prefix=RoomStatusContD&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContDetailController",
                pageKey: 'RoomStatusContD'
            })

            .state('RoomStatusTerminals', {
                url: "/:busnLocationNoHash/Terminals",
                templateUrl: rooturl + "/RoomStatus/Tmpl?Prefix=RoomStatusTerminals&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalsController",
                pageKey: 'RoomStatusTerminals'
            })
            .state('RoomStatusTerminalNew', {
                url: "/:busnLocationNoHash/Terminals/New",
                templateUrl: rooturl + "/RoomStatus/Tmpl?Prefix=RoomStatusTerminalNew&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalNewController",
                pageKey: 'RoomStatusTerminalNew'
            })
            .state('RoomStatusTerminalInfo', {
                url: "/:busnLocationNoHash/Terminals/:terminalIds",
                templateUrl: rooturl + "/RoomStatus/Tmpl?Prefix=RoomStatusTerminalInfo&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalInfoController",
                pageKey: 'RoomStatusTerminalInfo'
            })
            .state('RoomStatusTxnPoints', {
                url: "/:busnLocationNoHash/Transactions",
                templateUrl: rooturl + "/RoomStatus/Tmpl?Prefix=RoomStatusTxnPoints&type=Index",
                data: { pageTitle: 'Business Location Transactions' },
                controller: "busnLotTxnPointsController",
                pageKey: 'RoomStatusTxnPoints'
            })
            .state('RoomStatusCrdRangeAccpt', {
                url: "/:busnLocationNoHash/CardRangeAcceptance",
                templateUrl: rooturl + "/RoomStatus/Tmpl?Prefix=RoomStatusCrdRangeAccpt&type=Index",
                data: { pageTitle: 'Card Range Acceptance' },
                controller: "busnLotCrdRangeAccptController",
                pageKey: 'RoomStatusCrdRangeAccpt'
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