(function () {
    var app = angular.module('loyaltyApp',
    ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'App.Utils', 'ui.router', 'ui.select', 'ngSanitize', 'ngDialog']);

    app.config(['$stateProvider', '$urlRouterProvider', '$locationProvider', 'ngDialogProvider', function ($stateProvider, $urlRouterProvider, $locationProvider, ngDialogProvider) {
        $locationProvider.html5Mode(false).hashPrefix('');
        var viewBase = $('#hdUrlPrefix').val();

        $stateProvider
            .state('/', {
                url: "/",
                templateUrl: "index.html",
                data: { pageTitle: 'All Locations' },
                controller: "locationsController",
                pageKey: 'index'
            })
            .state('locationProfile', {
                url: "/:locationId",
                templateUrl: "/Locations/Tmpl?Prefix=lotpf&type=Index",
                data: { pageTitle: 'Location Profile' },
                controller: "locationProfileController",
                pageKey: 'lotpf'
            })
            .state('LocBusInfo', {
                url: "/:locationId/LocBusInfo",
                templateUrl: "/Locations/Tmpl?Prefix=locbusinf&type=Index",
                data: { pageTitle: 'Location Business Info' },
                controller: "locBusInfoController",
                pageKey: 'locbusinf'
            })
            .state('Terminal', {
                url: "/:locationId/Terminal",
                templateUrl: "/Locations/Tmpl?Prefix=terminal&type=Index",
                data: { pageTitle: 'Terminal' },
                controller: "terminalController",
                pageKey: 'terminal'
            })
            .state('TerminalDetail', {
                url: "/:locationId/Terminal/:terminalId",
                templateUrl: "/Locations/Tmpl?Prefix=terminald&type=Index",
                data: { pageTitle: 'Terminal Detail' },
                controller: "terminalDetailController",
                pageKey: 'terminald'
            })
            .state('financialInfo', {
                url: "/:locationId/FinancialInfo",
                templateUrl: "/Locations/Tmpl?Prefix=financial&type=Index",
                data: { pageTitle: 'Finance / Bank Info' },
                controller: "financialInfoController",
                pageKey: 'financial'
            })
            .state('topUpLimitController', {
                url: "/:locationId/TopUpLimit",
                templateUrl: "/Locations/Tmpl?Prefix=topuplimit&type=Index",
                data: { pageTitle: 'Top Up Limit' },
                controller: "topUpLimitontroller",
                pageKey: 'topuplimit'
            })
            .state('transactions', {
                url: "/:locationId/Transactions",
                templateUrl: "/Locations/Tmpl?Prefix=txn&type=Index",
                data: { pageTitle: 'Transactions' },
                controller: "transactionsController",
                pageKey: 'txn'
            })
            .state('contacts', {
                url: "/:locationId/Contact",
                templateUrl: "/Locations/Tmpl?Prefix=cont&type=Index",
                data: { pageTitle: 'Contacts' },
                controller: "contactsController",
                pageKey: 'cont'
            })
            .state('contactsDetail', {
                url: "/:locationId/Contact:id",
                templateUrl: "/Locations/Tmpl?Prefix=contd&type=Index",
                data: { pageTitle: 'Contacts' },
                controller: "contactsDetailController",
                pageKey: 'contd'
            })
            .state('contactsNew', {
                url: "/:locationId/Contact/New",
                templateUrl: "/Locations/Tmpl?Prefix=contnew&type=Index",
                data: { pageTitle: 'Contacts' },
                controller: "contactsNewController",
                pageKey: 'contmew'
            })
            .state('address', {
                url: "/:locationId/Address",
                templateUrl: "/Locations/Tmpl?Prefix=addr&type=Index",
                data: { pageTitle: 'Address' },
                controller: "addressController",
                pageKey: 'addr'
            })
            .state('addressDetail', {
                url: "/:locationId/Address:id",
                templateUrl: "/Locations/Tmpl?Prefix=addrd&type=Index",
                data: { pageTitle: 'Address' },
                controller: "addressDetailController",
                pageKey: 'addrd'
            })
            .state('addressNew', {
                url: "/:locationId/Address/New",
                templateUrl: "/Locations/Tmpl?Prefix=addrnew&type=Index",
                data: { pageTitle: 'Address' },
                controller: "addressNewController",
                pageKey: 'addrnew'
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
        $rootScope.$on('$stateChangeStart', function (e, current, pre) {
            console.log('state change start')
            $rootScope.$broadcast('$stateChanged', current.pageKey);
        });

        $rootScope.$on('$stateChangeSuccess', function (e, current, pre) {
            console.log('route path' + $location.path());
        });

        $rootScope.$on('$viewContentLoaded', function (e, current, pre) {
            console.log('view loaded path' + $location.path());
            App.init();
        });
    }]);
}());