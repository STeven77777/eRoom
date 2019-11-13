(function () {
    var injectParams = ['$http', '$q', 'Utils'];
    var accountFactory = function ($http, $q, $rootScope) {
        var serviceBase = '/api/dataservice/', factory = {};
        var Api = $rootScope.getRootUrl() + '/Account';
        factory.getFormData = function (obj) {
            var params = $.param(obj);
            return $http({
                method: 'GET',
                url: Api + '/FillData?' + params
            })
        };
        factory.getFormData = function (obj) {
            var params = $.param(obj);
            return $http({
                method: 'GET',
                url: Api + '/GetData?' + params
            })
        };
        return factory;
    };

    accountFactory.$inject = injectParams;
    angular.module('loyaltyApp').factory('accountService', accountFactory);
}());