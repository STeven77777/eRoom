(function () {
    var injectParams = ['$http', '$q', 'Utils'];
    var cardAcctSignUpFactory = function ($http, $q, $rootScope) {
        var serviceBase = '/api/dataservice/', factory = {};
        var Api = $rootScope.getRootUrl() + '/Account';
        factory.getFormData = function (obj) {
            var params = $.param(obj);
            return $http({
                method: 'GET',
                url: Api + '/FillData?' + params
            })
        };
        return factory;
    };

    cardAcctSignUpFactory.$inject = injectParams;
    angular.module('loyaltyApp').factory('cardAcctSignUpService', cardAcctSignUpFactory);
}());