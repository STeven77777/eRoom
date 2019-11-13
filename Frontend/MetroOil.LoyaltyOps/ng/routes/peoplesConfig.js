(function () {
    var app = angular.module('loyaltyApp',
        ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'App.Utils', 'ui.router', 'ui.select', 'ngSanitize', 'ngDialog', 'datePicker']);

    app.config(['$stateProvider', '$urlRouterProvider', '$locationProvider', 'ngDialogProvider', function ($stateProvider, $urlRouterProvider, $locationProvider, ngDialogProvider) {
        $locationProvider.html5Mode(false).hashPrefix('');
        var rooturl = $('#hdUrlPrefix').val();

        $stateProvider
            .state('PeopleSearch', {
                url: "/",
                templateUrl: rooturl + "/Peoples/Tmpl?Prefix=PeopleSearch&type=Index",
                data: { pageTitle: 'People Search' },
                controller: "peoplesController",
                pageKey: 'PeopleSearch'
            })
            .state('PeopleInfoNew', {
                url: "/New",
                templateUrl: rooturl + "/Peoples/Tmpl?Prefix=PeopleInfoNew&type=Index",
                data: { pageTitle: 'Create Business Locations' },
                controller: "newPeopleController",
                pageKey: 'PeopleInfoNew'
            })
            .state('PeopleInfo', {
                url: "/:busnLocationNoHash/Info",
                templateUrl: rooturl + "/Peoples/Tmpl?Prefix=PeopleInfo&type=Index",
                data: { pageTitle: 'Business Locations Info' },
                controller: "peopleInfoController",
                pageKey: 'PeopleInfo'
            })

            .state('PeopleAddrs', {
                url: "/:busnLocationNoHash/Address",
                templateUrl: rooturl + "/Peoples/Tmpl?Prefix=PeopleAddrs&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrController",
                pageKey: 'PeopleAddrs'
            })
            .state('PeopleAddrNew', {
                url: "/:busnLocationNoHash/Address/New",
                templateUrl: rooturl + "/Peoples/Tmpl?Prefix=PeopleAddrNew&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrNewController",
                pageKey: 'PeopleAddrNew'
            })
            .state('PeopleAddrD', {
                url: "/:busnLocationNoHash/Address/:busnLocationAddressId",
                templateUrl: rooturl + "/Peoples/Tmpl?Prefix=PeopleAddrD&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrDetailController",
                pageKey: 'PeopleAddrD'
            })

            .state('PeopleConts', {
                url: "/:busnLocationNoHash/Contact",
                templateUrl: rooturl + "/Peoples/Tmpl?Prefix=PeopleConts&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContController",
                pageKey: 'PeopleConts'
            })
            .state('PeopleContNew', {
                url: "/:busnLocationNoHash/Contact/New",
                templateUrl: rooturl + "/Peoples/Tmpl?Prefix=PeopleContNew&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContNewController",
                pageKey: 'PeopleContNew'
            })
            .state('PeopleContD', {
                url: "/:busnLocationNoHash/Contact/:busnLocationContactId",
                templateUrl: rooturl + "/Peoples/Tmpl?Prefix=PeopleContD&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContDetailController",
                pageKey: 'PeopleContD'
            })

            .state('PeopleTerminals', {
                url: "/:busnLocationNoHash/Terminals",
                templateUrl: rooturl + "/Peoples/Tmpl?Prefix=PeopleTerminals&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalsController",
                pageKey: 'PeopleTerminals'
            })
            .state('PeopleTerminalNew', {
                url: "/:busnLocationNoHash/Terminals/New",
                templateUrl: rooturl + "/Peoples/Tmpl?Prefix=PeopleTerminalNew&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalNewController",
                pageKey: 'PeopleTerminalNew'
            })
            .state('PeopleTerminalInfo', {
                url: "/:busnLocationNoHash/Terminals/:terminalIds",
                templateUrl: rooturl + "/Peoples/Tmpl?Prefix=PeopleTerminalInfo&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalInfoController",
                pageKey: 'PeopleTerminalInfo'
            })
            .state('PeopleTxnPoints', {
                url: "/:busnLocationNoHash/Transactions",
                templateUrl: rooturl + "/Peoples/Tmpl?Prefix=PeopleTxnPoints&type=Index",
                data: { pageTitle: 'Business Location Transactions' },
                controller: "busnLotTxnPointsController",
                pageKey: 'PeopleTxnPoints'
            })
            .state('PeopleCrdRangeAccpt', {
                url: "/:busnLocationNoHash/CardRangeAcceptance",
                templateUrl: rooturl + "/Peoples/Tmpl?Prefix=PeopleCrdRangeAccpt&type=Index",
                data: { pageTitle: 'Card Range Acceptance' },
                controller: "busnLotCrdRangeAccptController",
                pageKey: 'PeopleCrdRangeAccpt'
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