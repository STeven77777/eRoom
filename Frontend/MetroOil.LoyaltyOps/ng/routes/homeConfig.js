(function () {
    var app = angular.module('loyaltyApp', ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'App.Utils','ui.router']);

    app.config(['$stateProvider', '$urlRouterProvider', '$locationProvider', function ($stateProvider, $urlRouterProvider, $locationProvider) {
        $locationProvider.html5Mode(false).hashPrefix('');
        var viewBase = $('#hdUrlPrefix').val();
        $urlRouterProvider.otherwise('/');

        $stateProvider
            .state('/', {
                url: "/",
                templateUrl: "index.html",
                data: { pageTitle: 'Home Page' },
                controller: "homeController"
            })

    }]);

    app.run(['$rootScope', '$location', 'Utils', '$route', function ($rootScope, $location, Utils, $route) {
        $rootScope.tables = {};
        $rootScope.$on('$routeChangeStart', function (e, current, pre) {
            //console.log($route.current);
            $.blockUI();
            $rootScope.$broadcast('routeChanged', current.pageKey);
        });

        $rootScope.$on('$routeChangeSuccess', function (e, current, pre) {
            console.log('rounte path' + $location.path());
            $.unblockUI();
        });

        $rootScope.$on('$viewContentLoaded', function (e, current, pre) {
            console.log('view loaded path' + $location.path());
            App.init();
        });
    }]);
}());