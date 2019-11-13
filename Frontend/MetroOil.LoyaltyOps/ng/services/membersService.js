(function () {
    var injectParams = ['$http', '$q', 'Utils'];
    var membersService = function ($http, $q, $rootScope, Utils) {
        var ApiUrl = $rootScope.getRootUrl() + '/Members', factory = {};

        //factory.GetFormData = function (obj) {
        //    //Api = Url || Api;
        //    var params = $.param(obj);
        //    return $http({
        //        method: 'GET',
        //        url: ApiUrl + '/FillData?' + params
        //    })
        //};

        //factory.ClassicLoginCreate = function (obj) {
        //    var params = $.param(obj);
        //    return $http({
        //        method: 'POST',
        //        data: obj,
        //        url: ApiUrl + '/ClassicLoginCreate'
        //    })
        //};

        //factory.WebGetState = function (obj) {
        //    return $http({
        //        method: 'GET',
        //        url: ApiUrl + '/WebGetState?' + $.param(obj)
        //    })
        //};

        //factory.EntityUpdate = function (obj) {
        //    return $http({
        //        method: 'PUT',
        //        data: obj,
        //        url: ApiUrl + '/EntityUpdate'
        //    })
        //};
        
        return factory;
    };

    membersService.$inject = injectParams;

    angular.module('loyaltyApp').factory('membersService', membersService);
}());