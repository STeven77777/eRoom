(function () {
    var injectParams = ['$http', '$q', 'Utils'];
    var creditAssessmentFactory = function ($http, $q, $rootScope, Utils) {
        var Api = $rootScope.getRootUrl() + '/Financial', factory = {};
       
        factory.getFormData = function (obj) {
            var params = $.param(obj);
            return $http({
                method: 'GET',
                url: Api + '/GetCreditAssessment?' + params
            })
        };

        return factory;
    };

    creditAssessmentFactory.$inject = injectParams;
    angular.module('fleetsApp').factory('creditAssessmentService', creditAssessmentFactory);
}());