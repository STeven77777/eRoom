(function () {
    var app = angular.module('loyaltyApp',
        ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'oc.lazyLoad', 'App.Utils']);

    app.config(['$routeProvider', function ($routeProvider) {
        var viewBase = $('#hdUrlPrefix').val();

         $routeProvider
            .when('/application', {
                controller: 'applicationsController',
                templateUrl: 'index.html',
                pageKey: 'application'
            })
            .when('/Account', {
                controller: 'accountController',
                templateUrl: viewBase + '/Account/Tmpl?Prefix=cao&type=Index',
                pagekey: 'account'
            })
            .when('/customeredit/:customerId', {
                controller: 'CustomerEditController',
                templateUrl: viewBase + 'customers/customerEdit.html',
                pagekey: 'customer',
                secure: true
            })
            .when('/orders', {
                controller: 'OrdersController',
                templateUrl: viewBase + 'orders/orders.html',
                pageKey: 'order'
            })
            .when('/about', {
                controller: 'AboutController',
                templateUrl: viewBase + 'about.html',
                pagekey: 'about'
            })
            .when('/login/:redirect*?', {
                controller: 'LoginController',
                templateUrl: viewBase + 'login.html',
                pageKey: 'login'
            })
        //}

    // Map to customer controller services
    //.otherwise({ redirectTo: '/' });
    }]);

    app.run(['$rootScope', '$location', 'Utils', '$route', function ($rootScope, $location, Utils,$route) {
        $rootScope.tables = {};
        $rootScope.$on('$routeChangeStart', function (e, current, pre) {
            //console.log($route.current);
            $rootScope.$broadcast('routeChanged', current.pageKey);
        });

        $rootScope.$on('$routeChangeSuccess', function (e, current, pre) {
         console.log('rounte path' + $location.path());
        });

    }]);
}());