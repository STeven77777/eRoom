(function () {
    var injectParams = ['$http', '$q', 'Utils'];

    var dataService = function ($http, $q, $rootScope, Utils) {
        var factory = {};

        factory.loadingCounts = function () {
            return {
                enable_count: 0,
                disable_count: 0
            }
        };
        return factory;
    };

    dataService.$inject = injectParams;
    angular.module('loyaltyApp').factory('dataService', dataService);

}());
