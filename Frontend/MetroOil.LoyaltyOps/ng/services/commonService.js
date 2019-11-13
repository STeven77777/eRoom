(function () {
    var injectParams = ['$http', '$q', '$rootScope', 'Utils'];
    var commonService = function ($http, $q, $rootScope, Utils) {

        var CommonUrl = $rootScope.getRootUrl() + '/Common', factory = {};

        factory.GetRefLib = function (refType) {
            return $http({
                method: 'GET',
                url: CommonUrl + '/GetRefLib?refType=' + refType
            })
        };
        
        factory.WebGetState = function (obj) {
            return $http({
                method: 'GET',
                url: CommonUrl + '/WebGetState?' + $.param(obj)
            })
        };

        return factory;
    };

    commonService.$inject = injectParams;

    angular.module('loyaltyApp').factory('commonService', commonService);
}());