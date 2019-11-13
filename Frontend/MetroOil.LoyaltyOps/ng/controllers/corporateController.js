(function () {
    var injectParams = ['$scope', '$rootScope', '$location', '$window', '$timeout'];
    
    var corporateProfileController = function ($scope, $rootScope, $location, $window, $timeout) {
        // local declare
        var vm = this;
        //$scope.backToList = function () {
        //    window.location.href = "/";
        //};
    };

    corporateProfileController.$inject = injectParams;

    angular.module('loyaltyApp').controller('corporateProfileController', corporateProfileController);
}());