(function () {
    var app = angular.module('loyaltyApp',
    ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'App.Utils', 'ui.router', 'ui.select', 'ngSanitize']);

    app.config(['$stateProvider', '$urlRouterProvider', '$locationProvider', function ($stateProvider, $urlRouterProvider, $locationProvider) {
        $locationProvider.html5Mode(false).hashPrefix('');
        var viewBase = $('#hdUrlPrefix').val();

        $stateProvider
            .state('/', {
                url: "/",
                templateUrl: "index.html",
                //data: { pageTitle: 'All Corporate' },
                //controller: "CorporateController",
                pageKey: 'index'
            })
            .state('corporateProfile', {
                url: "/CorporateProfile",
                templateUrl: "/Corporate/Tmpl?Prefix=corpf&type=Index",
                data: { pageTitle: 'Corporate Profile' },
                controller: "corporateProfileController",
                pageKey: 'corpf'
            })
        ;
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