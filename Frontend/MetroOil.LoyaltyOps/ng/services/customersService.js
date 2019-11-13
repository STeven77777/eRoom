(function () {
    var injectParams = ['$http', '$q', 'Utils'];
    var customerApi = function ($http, $q, $rootScope, Utils) {
        var Api = $rootScope.getRootUrl() + '/Customers', factory = {};

        factory.GetContactDetail = function (obj) {
            var params = $.param(obj);
            return $http({
                method: 'GET',
                url: Api + '/GetContactDetail?' + params
            })
        };

        factory.SaveContactDetail = function (obj) {
            return $http({
                url: Api + '/SaveContactDetail',
                method: 'POST',
                data: obj
            });
        };

        factory.GetCreditAssessment = function (obj) {
            var params = $.param(obj);
            return $http({
                method: 'GET',
                url: Api + '/GetCreditAssessment?' + params
            })
        };

        factory.GetProductRestrictions = function (obj) {
            var params = $.param(obj);
            return $http({
                method: 'GET',
                url: Api + '/GetProductRestrictions?' + params
            })
        };

        return factory;
    };

    //var productRestrictionsFactory = function ($http, $q, $rootScope, Utils) {
    //    var Api = $rootScope.getRootUrl() + '/Customers', factory = {};

    //    factory.GetProductRestrictions = function (obj) {
    //        var params = $.param(obj);
    //        return $http({
    //            method: 'GET',
    //            url: Api + '/GetProductRestrictions?' + params
    //        })
    //    };

    //    return factory;
    //};

    customerApi.$inject = injectParams;
    //productRestrictionsFactory.$inject = injectParams;

    angular.module('loyaltyApp').factory('customerApi', customerApi);
    //angular.module('loyaltyApp').factory('productRestrictionsFactory', productRestrictionsFactory);
}());