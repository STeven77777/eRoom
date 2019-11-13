
(function () {
    var injectParams = ['$http', '$q'];

    var multipPaymentFactory = function ($http, $q) {
        var serviceBase = '/api/dataservice/', factory = {};
        factory.getApplicationData = function (id) {
            return $http({
                url: serviceBase + '/Applications/ftMilestoneInfo?' + $.param(obj),
                method: 'GET',
            })
        };

        return factory;
    };

    multipPaymentFactory.$inject = injectParams;
    angular.module('loyaltyApp').factory('multipPaymentService', multipPaymentFactory);
}());