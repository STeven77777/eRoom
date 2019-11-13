(function () {
    var injectParams = ['$http', '$q', 'Utils'];
    var applicationsService = function ($http, $q, $rootScope, Utils) {
        var Api = $rootScope.getRootUrl() + '/Applications', factory = {};
        factory.getApplicationData = function (id) {
            return $http({
                url: serviceBase + '/Applications/ftMilestoneInfo?' + $.param(obj),
                method: 'GET',
            })
        };

        factory.getFormData = function (obj) {
            var params = $.param(obj);
            return $http({
                method: 'GET',
                url: Api + '/GetData?' + params
            })
        };

        factory.postApplicationGeneralInfo = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Application/SaveApplicationGeneralInfo',
                method: 'POST',
                data: obj
            });
        };

        return factory;
    };

    applicationsService.$inject = injectParams;
    angular.module('loyaltyApp').factory('applicationsService', applicationsService);
}());