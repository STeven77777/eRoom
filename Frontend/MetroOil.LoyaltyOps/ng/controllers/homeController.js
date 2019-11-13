(function () {
    var injectParams = ['$scope', '$rootScope', '$location', '$window', '$timeout'];

    var homeController = function ($scope, $rootScope, $location, $window, $timeout) {
        var vm = this;
        console.log('hello path ' + $rootScope.getRootUrl());
    };
    
    homeController.$inject = injectParams;
    
    angular.module('loyaltyApp').controller('homeController', homeController);
}());