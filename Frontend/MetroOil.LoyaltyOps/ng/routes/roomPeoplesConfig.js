(function () {
    var app = angular.module('loyaltyApp',
        ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'App.Utils', 'ui.router', 'ui.select', 'ngSanitize', 'ngDialog', 'datePicker']);

    app.config(['$stateProvider', '$urlRouterProvider', '$locationProvider', 'ngDialogProvider', function ($stateProvider, $urlRouterProvider, $locationProvider, ngDialogProvider) {
        $locationProvider.html5Mode(false).hashPrefix('');
        var rooturl = $('#hdUrlPrefix').val();

        $stateProvider
            .state('RoomPeopleSearch', {
                url: "/",
                templateUrl: rooturl + "/RoomPeoples/Tmpl?Prefix=RoomPeopleSearch&type=Index",
                data: { pageTitle: 'RoomPeople Search' },
                controller: "roomPeoplesController",
                pageKey: 'RoomPeopleSearch'
            })
            .state('RoomPeopleInfoNew', {
                url: "/New",
                templateUrl: rooturl + "/RoomPeoples/Tmpl?Prefix=RoomPeopleInfoNew&type=Index",
                data: { pageTitle: 'Create Business Locations' },
                controller: "newRoomPeopleController",
                pageKey: 'RoomPeopleInfoNew'
            })
            .state('RoomPeopleInfo', {
                url: "/:busnLocationNoHash/Info",
                templateUrl: rooturl + "/RoomPeoples/Tmpl?Prefix=RoomPeopleInfo&type=Index",
                data: { pageTitle: 'Business Locations Info' },
                controller: "roomPeopleInfoController",
                pageKey: 'RoomPeopleInfo'
            })

            .state('RoomPeopleAddrs', {
                url: "/:busnLocationNoHash/Address",
                templateUrl: rooturl + "/RoomPeoples/Tmpl?Prefix=RoomPeopleAddrs&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrController",
                pageKey: 'RoomPeopleAddrs'
            })
            .state('RoomPeopleAddrNew', {
                url: "/:busnLocationNoHash/Address/New",
                templateUrl: rooturl + "/RoomPeoples/Tmpl?Prefix=RoomPeopleAddrNew&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrNewController",
                pageKey: 'RoomPeopleAddrNew'
            })
            .state('RoomPeopleAddrD', {
                url: "/:busnLocationNoHash/Address/:busnLocationAddressId",
                templateUrl: rooturl + "/RoomPeoples/Tmpl?Prefix=RoomPeopleAddrD&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrDetailController",
                pageKey: 'RoomPeopleAddrD'
            })

            .state('RoomPeopleConts', {
                url: "/:busnLocationNoHash/Contact",
                templateUrl: rooturl + "/RoomPeoples/Tmpl?Prefix=RoomPeopleConts&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContController",
                pageKey: 'RoomPeopleConts'
            })
            .state('RoomPeopleContNew', {
                url: "/:busnLocationNoHash/Contact/New",
                templateUrl: rooturl + "/RoomPeoples/Tmpl?Prefix=RoomPeopleContNew&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContNewController",
                pageKey: 'RoomPeopleContNew'
            })
            .state('RoomPeopleContD', {
                url: "/:busnLocationNoHash/Contact/:busnLocationContactId",
                templateUrl: rooturl + "/RoomPeoples/Tmpl?Prefix=RoomPeopleContD&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContDetailController",
                pageKey: 'RoomPeopleContD'
            })

            .state('RoomPeopleTerminals', {
                url: "/:busnLocationNoHash/Terminals",
                templateUrl: rooturl + "/RoomPeoples/Tmpl?Prefix=RoomPeopleTerminals&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalsController",
                pageKey: 'RoomPeopleTerminals'
            })
            .state('RoomPeopleTerminalNew', {
                url: "/:busnLocationNoHash/Terminals/New",
                templateUrl: rooturl + "/RoomPeoples/Tmpl?Prefix=RoomPeopleTerminalNew&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalNewController",
                pageKey: 'RoomPeopleTerminalNew'
            })
            .state('RoomPeopleTerminalInfo', {
                url: "/:busnLocationNoHash/Terminals/:terminalIds",
                templateUrl: rooturl + "/RoomPeoples/Tmpl?Prefix=RoomPeopleTerminalInfo&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalInfoController",
                pageKey: 'RoomPeopleTerminalInfo'
            })
            .state('RoomPeopleTxnPoints', {
                url: "/:busnLocationNoHash/Transactions",
                templateUrl: rooturl + "/RoomPeoples/Tmpl?Prefix=RoomPeopleTxnPoints&type=Index",
                data: { pageTitle: 'Business Location Transactions' },
                controller: "busnLotTxnPointsController",
                pageKey: 'RoomPeopleTxnPoints'
            })
            .state('RoomPeopleCrdRangeAccpt', {
                url: "/:busnLocationNoHash/CardRangeAcceptance",
                templateUrl: rooturl + "/RoomPeoples/Tmpl?Prefix=RoomPeopleCrdRangeAccpt&type=Index",
                data: { pageTitle: 'Card Range Acceptance' },
                controller: "busnLotCrdRangeAccptController",
                pageKey: 'RoomPeopleCrdRangeAccpt'
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