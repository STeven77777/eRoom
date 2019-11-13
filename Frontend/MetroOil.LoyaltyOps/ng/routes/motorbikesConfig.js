(function () {
    var app = angular.module('loyaltyApp',
        ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'App.Utils', 'ui.router', 'ui.select', 'ngSanitize', 'ngDialog', 'datePicker']);

    app.config(['$stateProvider', '$urlRouterProvider', '$locationProvider', 'ngDialogProvider', function ($stateProvider, $urlRouterProvider, $locationProvider, ngDialogProvider) {
        $locationProvider.html5Mode(false).hashPrefix('');
        var rooturl = $('#hdUrlPrefix').val();

        $stateProvider
            .state('MotorbikeSearch', {
                url: "/",
                templateUrl: rooturl + "/Motorbikes/Tmpl?Prefix=MotorbikeSearch&type=Index",
                data: { pageTitle: 'Motorbike Search' },
                controller: "motorbikesController",
                pageKey: 'MotorbikeSearch'
            })
            .state('MotorbikeInfoNew', {
                url: "/New",
                templateUrl: rooturl + "/Motorbikes/Tmpl?Prefix=MotorbikeInfoNew&type=Index",
                data: { pageTitle: 'Create Business Locations' },
                controller: "newMotorbikeController",
                pageKey: 'MotorbikeInfoNew'
            })
            .state('MotorbikeInfo', {
                url: "/:busnLocationNoHash/Info",
                templateUrl: rooturl + "/Motorbikes/Tmpl?Prefix=MotorbikeInfo&type=Index",
                data: { pageTitle: 'Business Locations Info' },
                controller: "motorbikeInfoController",
                pageKey: 'MotorbikeInfo'
            })

            .state('MotorbikeAddrs', {
                url: "/:busnLocationNoHash/Address",
                templateUrl: rooturl + "/Motorbikes/Tmpl?Prefix=MotorbikeAddrs&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrController",
                pageKey: 'MotorbikeAddrs'
            })
            .state('MotorbikeAddrNew', {
                url: "/:busnLocationNoHash/Address/New",
                templateUrl: rooturl + "/Motorbikes/Tmpl?Prefix=MotorbikeAddrNew&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrNewController",
                pageKey: 'MotorbikeAddrNew'
            })
            .state('MotorbikeAddrD', {
                url: "/:busnLocationNoHash/Address/:busnLocationAddressId",
                templateUrl: rooturl + "/Motorbikes/Tmpl?Prefix=MotorbikeAddrD&type=Index",
                data: { pageTitle: 'Business Location Address' },
                controller: "busnLotAddrDetailController",
                pageKey: 'MotorbikeAddrD'
            })

            .state('MotorbikeConts', {
                url: "/:busnLocationNoHash/Contact",
                templateUrl: rooturl + "/Motorbikes/Tmpl?Prefix=MotorbikeConts&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContController",
                pageKey: 'MotorbikeConts'
            })
            .state('MotorbikeContNew', {
                url: "/:busnLocationNoHash/Contact/New",
                templateUrl: rooturl + "/Motorbikes/Tmpl?Prefix=MotorbikeContNew&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContNewController",
                pageKey: 'MotorbikeContNew'
            })
            .state('MotorbikeContD', {
                url: "/:busnLocationNoHash/Contact/:busnLocationContactId",
                templateUrl: rooturl + "/Motorbikes/Tmpl?Prefix=MotorbikeContD&type=Index",
                data: { pageTitle: 'Business Location Contact' },
                controller: "busnLotContDetailController",
                pageKey: 'MotorbikeContD'
            })

            .state('MotorbikeTerminals', {
                url: "/:busnLocationNoHash/Terminals",
                templateUrl: rooturl + "/Motorbikes/Tmpl?Prefix=MotorbikeTerminals&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalsController",
                pageKey: 'MotorbikeTerminals'
            })
            .state('MotorbikeTerminalNew', {
                url: "/:busnLocationNoHash/Terminals/New",
                templateUrl: rooturl + "/Motorbikes/Tmpl?Prefix=MotorbikeTerminalNew&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalNewController",
                pageKey: 'MotorbikeTerminalNew'
            })
            .state('MotorbikeTerminalInfo', {
                url: "/:busnLocationNoHash/Terminals/:terminalIds",
                templateUrl: rooturl + "/Motorbikes/Tmpl?Prefix=MotorbikeTerminalInfo&type=Index",
                data: { pageTitle: 'Business Location Terminal' },
                controller: "busnLotTerminalInfoController",
                pageKey: 'MotorbikeTerminalInfo'
            })
            .state('MotorbikeTxnPoints', {
                url: "/:busnLocationNoHash/Transactions",
                templateUrl: rooturl + "/Motorbikes/Tmpl?Prefix=MotorbikeTxnPoints&type=Index",
                data: { pageTitle: 'Business Location Transactions' },
                controller: "busnLotTxnPointsController",
                pageKey: 'MotorbikeTxnPoints'
            })
            .state('MotorbikeCrdRangeAccpt', {
                url: "/:busnLocationNoHash/CardRangeAcceptance",
                templateUrl: rooturl + "/Motorbikes/Tmpl?Prefix=MotorbikeCrdRangeAccpt&type=Index",
                data: { pageTitle: 'Card Range Acceptance' },
                controller: "busnLotCrdRangeAccptController",
                pageKey: 'MotorbikeCrdRangeAccpt'
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