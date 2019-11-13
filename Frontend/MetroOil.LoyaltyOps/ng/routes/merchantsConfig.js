(function () {
    var app = angular.module('loyaltyApp',
        ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'App.Utils', 'ui.router', 'ui.select', 'ngSanitize', 'ngDialog', 'datePicker']);

    app.config(['$stateProvider', '$urlRouterProvider', '$locationProvider', 'ngDialogProvider', function ($stateProvider, $urlRouterProvider, $locationProvider, ngDialogProvider) {
        $locationProvider.html5Mode(false).hashPrefix('');
        var rooturl = $('#hdUrlPrefix').val();

        $stateProvider
            //.state('/', {
            //    url: "/",
            //    templateUrl: "index.html",
            //    data: { pageTitle: 'All Merchants' },
            //    controller: "merchantsController",
            //    pageKey: 'merchindex'
            //})

            .state('MerchUserNew', {
                url: "/Users/New",
                templateUrl: rooturl + "/Merchants/Tmpl?Prefix=MerchUserNew&type=Index",
                data: { pageTitle: 'New Merchant User' },
                controller: "newMerchUserController",
                pageKey: 'MerchUserNew'
            })
            .state('MerchUsers', {
                url: "/Users",
                templateUrl: rooturl + "/Merchants/Tmpl?Prefix=MerchUsers&type=Index",
                data: { pageTitle: 'Merchant User List' },
                controller: "merchUsersController",
                pageKey: 'MerchUsers'
            })
            .state('MerchUserInfo', {
                url: "/Users/:merchUserIdHash",
                templateUrl: rooturl + "/Merchants/Tmpl?Prefix=MerchUserInfo&type=Index",
                data: { pageTitle: 'Member Info' },
                controller: "merchUserInfoController",
                pageKey: 'MerchUserInfo'
            })
            .state('MerchAccts', {
                url: "/Accounts",
                templateUrl: rooturl + "/Merchants/Tmpl?Prefix=MerchAccts&type=Index",
                data: { pageTitle: 'Merchant Accounts List' },
                controller: "merchAccountsController",
                pageKey: 'MerchAccts'
            })
            .state('MerchAcctNew', {
                url: "/Accounts/New",
                templateUrl: rooturl + "/Merchants/Tmpl?Prefix=MerchAcctNew&type=Index",
                data: { pageTitle: 'New Merchant Acct' },
                controller: "newMerchAcctController",
                pageKey: 'MerchAcctNew'
            })
            .state('MerchAcctInfo', {
                url: "/Accounts/:merchAcctNoHash/Info",
                templateUrl: rooturl + "/Merchants/Tmpl?Prefix=MerchAcctInfo&type=Index",
                data: { pageTitle: 'Merchant Account Info' },
                controller: "merchAcctInfoController",
                pageKey: 'MerchAcctInfo'
            })

            .state('MerchAcctAddrs', {
                url: "/Accounts/:merchAcctNoHash/Address",
                templateUrl: rooturl + "/Merchants/Tmpl?Prefix=MerchAcctAddrs&type=Index",
                data: { pageTitle: 'Merchant Account Address' },
                controller: "merchAcctAddrController",
                pageKey: 'MerchAcctAddrs'
            })
            .state('MerchAcctAddrNew', {
                url: "/Accounts/:merchAcctNoHash/Address/New",
                templateUrl: rooturl + "/Merchants/Tmpl?Prefix=MerchAcctAddrNew&type=Index",
                data: { pageTitle: 'Merchant Account Address' },
                controller: "merchAcctAddrNewController",
                pageKey: 'MerchAcctAddrNew'
            })
            .state('MerchAcctAddrD', {
                url: "/Accounts/:merchAcctNoHash/Address/:merchAddressId",
                templateUrl: rooturl + "/Merchants/Tmpl?Prefix=MerchAcctAddrD&type=Index",
                data: { pageTitle: 'Merchant Account Address' },
                controller: "merchAcctAddrDetailController",
                pageKey: 'MerchAcctAddrD'
            })

            .state('MerchAcctConts', {
                url: "/Accounts/:merchAcctNoHash/Contact",
                templateUrl: rooturl + "/Merchants/Tmpl?Prefix=MerchAcctConts&type=Index",
                data: { pageTitle: 'Merchant Account Contact' },
                controller: "merchAcctContController",
                pageKey: 'MerchAcctConts'
            })
            .state('MerchAcctContNew', {
                url: "/Accounts/:merchAcctNoHash/Contact/New",
                templateUrl: rooturl + "/Merchants/Tmpl?Prefix=MerchAcctContNew&type=Index",
                data: { pageTitle: 'Merchant Account Contact' },
                controller: "merchAcctContNewController",
                pageKey: 'MerchAcctContNew'
            })
            .state('MerchAcctContD', {
                url: "/Accounts/:merchAcctNoHash/Contact/:merchContactId",
                templateUrl: rooturl + "/Merchants/Tmpl?Prefix=MerchAcctContD&type=Index",
                data: { pageTitle: 'Merchant Account Contact' },
                controller: "merchAcctContDetailController",
                pageKey: 'MerchAcctContD'
            })

            .state('MerchAcctBusnLocations', {
                url: "/Accounts/:merchAcctNoHash/BusinessLocations",
                templateUrl: rooturl + "/Merchants/Tmpl?Prefix=MerchAcctBusnLocations&type=Index",
                data: { pageTitle: 'List of Business Locations' },
                controller: "merchAcctBusnLotsController",
                pageKey: 'MerchAcctBusnLocations'
            })
        ;

        ngDialogProvider.setDefaults({
            className: 'ngdialog-theme-default',
            plain: false,
            showClose: true,
            closeByDocument: true,
            closeByEscape: true,
            appendTo: false,
            preCloseCallback: function () {
                console.log('default pre-close callback');
            }
        });
    }]);

    app.run(['$rootScope', '$location', 'Utils', '$route', function ($rootScope, $location, Utils, $route) {
        $rootScope.tables = {};
        $rootScope.obj = {};
        $rootScope.$on('$stateChangeStart', function (e, current, pre) {
            console.log('state change start')
            $.blockUI();
            $rootScope.$broadcast('$stateChanged', current.pageKey);
        });

        $rootScope.$on('$stateChangeSuccess', function (e, current, pre) {
            console.log('route path' + $location.path());
            $.unblockUI();
            var _location = $location.path();
            if (_location === '/new') {
                $rootScope.obj._type = 'new';
                $rootScope.obj.merchAcctNoHash = null;
            } else if (_location === '/') {
                $rootScope.obj._type = 'index';
                $rootScope.obj.merchAcctNoHash = null;
            } else {
                $rootScope.obj.merchAcctNoHash = pre.merchAcctNoHash;
                $rootScope.obj.merchUserIdHash = pre.merchUserIdHash;

                if (pre.id) {
                    $rootScope.obj.ids = pre.id;
                }

                if (pre.merchAddressId) {
                    $rootScope.obj.merchAddressId = pre.merchAddressId;
                }

                if (pre.merchContactId) {
                    $rootScope.obj.merchContactId = pre.merchContactId;
                }

                $rootScope.obj._type = 'edit';
            }
        });

        $rootScope.$on('$viewContentLoaded', function (e, current, pre) {
            console.log('view loaded path' + $location.path());
            App.init();
        });
    }]);
}());