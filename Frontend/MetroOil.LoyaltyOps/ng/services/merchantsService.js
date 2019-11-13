(function () {
    var injectParams = ['$http', '$q', 'Utils'];
    var merchantService = function ($http, $q, $rootScope, Utils) {
        var ApiRootUrl = $rootScope.getRootUrl() + '/Merchants', factory = {};

        //factory.GetContactDetail = function (obj) {
        //    var params = $.param(obj);
        //    return $http({
        //        method: 'GET',
        //        url: Api + '/GetContactDetail?' + params
        //    })
        //};

        factory.MerchantAcctCreate = function (obj) {
            return $http({
                url: ApiRootUrl + '/MerchantAcctCreate',
                method: 'POST',
                data: obj
            });
        };

        factory.MerchantBusnLocationCreate = function (obj) {
            return $http({
                url: ApiRootUrl + '/MerchantBusnLocationCreate',
                method: 'POST',
                data: obj
            });
        };

        return factory;
    };

    merchantService.$inject = injectParams;

    angular.module('loyaltyApp').factory('merchantService', merchantService);
}());