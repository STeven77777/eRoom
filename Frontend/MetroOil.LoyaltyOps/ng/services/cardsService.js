(function () {
    var injectParams = ['$http', '$q', 'Utils'];
    var creditAssessmentFactory = function ($http, $q, $rootScope, Utils) {
        var Api = $rootScope.getRootUrl() + '/Customers', factory = {};
       
        factory.getFormData = function (obj) {
            var params = $.param(obj);
            return $http({
                method: 'GET',
                url: Api + '/GetCreditAssessment?' + params
            })
        };

        return factory;
    };

    var productRestrictionsFactory = function ($http, $q, $rootScope, Utils) {
        var Api = $rootScope.getRootUrl() + '/Customers', factory = {};

        factory.getFormData = function (obj) {
            var params = $.param(obj);
            return $http({
                method: 'GET',
                url: Api + '/GetProductRestrictions?' + params
            })
        };

        return factory;
    };

    creditAssessmentFactory.$inject = injectParams;
    productRestrictionsFactory.$inject = injectParams;

    angular.module('loyaltyApp').factory('creditAssessmentService', creditAssessmentFactory);
    angular.module('loyaltyApp').factory('productRestrictionsFactory', productRestrictionsFactory);
}());