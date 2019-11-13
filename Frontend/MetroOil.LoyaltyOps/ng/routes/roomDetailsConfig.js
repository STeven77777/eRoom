(function () {
    var app = angular.module('loyaltyApp',
        ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'App.Utils', 'ui.router', 'ui.select', 'ngSanitize', 'ngDialog', 'datePicker']);

    app.config(['$stateProvider', '$urlRouterProvider', '$locationProvider', 'ngDialogProvider', function ($stateProvider, $urlRouterProvider, $locationProvider, ngDialogProvider) {
        $locationProvider.html5Mode(false).hashPrefix('');
        var rooturl = $('#hdUrlPrefix').val();

        $stateProvider
            .state('RoomDetailSearch', {
                url: "/",
                templateUrl: rooturl + "/RoomDetails/Tmpl?Prefix=RoomDetailSearch&type=Index",
                data: { pageTitle: 'RoomDetail Search' },
                controller: "roomDetailsController",
                pageKey: 'RoomDetailSearch'
            })
            .state('RoomDetailInfoNew', {
                url: "/New",
                templateUrl: rooturl + "/RoomDetails/Tmpl?Prefix=RoomDetailInfoNew&type=Index",
                data: { pageTitle: 'Create Business Locations' },
                controller: "newRoomDetailController",
                pageKey: 'RoomDetailInfoNew'
            })
            .state('RoomDetailInfo', {
                url: "/:busnLocationNoHash/Info",
                templateUrl: rooturl + "/RoomDetails/Tmpl?Prefix=RoomDetailInfo&type=Index",
                data: { pageTitle: 'Business Locations Info' },
                controller: "roomDetailInfoController",
                pageKey: 'RoomDetailInfo'
            })

            .state('RoomDetailAddrs', {
                url: "/:busnLocationNoHash/Address",
                templateUrl: rooturl + "/RoomDetails/Tmpl?Prefix=RoomDetailAddrs&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrController",
                pageKey: 'RoomDetailAddrs'
            })
            .state('RoomDetailAddrNew', {
                url: "/:busnLocationNoHash/Address/New",
                templateUrl: rooturl + "/RoomDetails/Tmpl?Prefix=RoomDetailAddrNew&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrNewController",
                pageKey: 'RoomDetailAddrNew'
            })
            .state('RoomDetailAddrD', {
                url: "/:busnLocationNoHash/Address/:busnLocationAddressId",
                templateUrl: rooturl + "/RoomDetails/Tmpl?Prefix=RoomDetailAddrD&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrDetailController",
                pageKey: 'RoomDetailAddrD'
            })

            .state('RoomDetailConts', {
                url: "/:busnLocationNoHash/Contact",
                templateUrl: rooturl + "/RoomDetails/Tmpl?Prefix=RoomDetailConts&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContController",
                pageKey: 'RoomDetailConts'
            })
            .state('RoomDetailContNew', {
                url: "/:busnLocationNoHash/Contact/New",
                templateUrl: rooturl + "/RoomDetails/Tmpl?Prefix=RoomDetailContNew&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContNewController",
                pageKey: 'RoomDetailContNew'
            })
            .state('RoomDetailContD', {
                url: "/:busnLocationNoHash/Contact/:busnLocationContactId",
                templateUrl: rooturl + "/RoomDetails/Tmpl?Prefix=RoomDetailContD&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContDetailController",
                pageKey: 'RoomDetailContD'
            })

            .state('RoomDetailTerminals', {
                url: "/:busnLocationNoHash/Terminals",
                templateUrl: rooturl + "/RoomDetails/Tmpl?Prefix=RoomDetailTerminals&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalsController",
                pageKey: 'RoomDetailTerminals'
            })
            .state('RoomDetailTerminalNew', {
                url: "/:busnLocationNoHash/Terminals/New",
                templateUrl: rooturl + "/RoomDetails/Tmpl?Prefix=RoomDetailTerminalNew&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalNewController",
                pageKey: 'RoomDetailTerminalNew'
            })
            .state('RoomDetailTerminalInfo', {
                url: "/:busnLocationNoHash/Terminals/:terminalIds",
                templateUrl: rooturl + "/RoomDetails/Tmpl?Prefix=RoomDetailTerminalInfo&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalInfoController",
                pageKey: 'RoomDetailTerminalInfo'
            })
            .state('RoomDetailTxnPoints', {
                url: "/:busnLocationNoHash/Transactions",
                templateUrl: rooturl + "/RoomDetails/Tmpl?Prefix=RoomDetailTxnPoints&type=Index",
                data: { pageTitle: 'Business Location Transactions' },
                controller: "busnLotTxnPointsController",
                pageKey: 'RoomDetailTxnPoints'
            })
            .state('RoomDetailCrdRangeAccpt', {
                url: "/:busnLocationNoHash/CardRangeAcceptance",
                templateUrl: rooturl + "/RoomDetails/Tmpl?Prefix=RoomDetailCrdRangeAccpt&type=Index",
                data: { pageTitle: 'Card Range Acceptance' },
                controller: "busnLotCrdRangeAccptController",
                pageKey: 'RoomDetailCrdRangeAccpt'
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