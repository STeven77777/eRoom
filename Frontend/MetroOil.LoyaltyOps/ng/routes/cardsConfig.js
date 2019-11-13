(function () {
    var app = angular.module('loyaltyApp',
    ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'App.Utils', 'ui.router', 'ui.select', 'ngSanitize', 'ngDialog']);

    app.config(['$stateProvider', '$urlRouterProvider', '$locationProvider', 'ngDialogProvider', function ($stateProvider, $urlRouterProvider, $locationProvider, ngDialogProvider) {
        $locationProvider.html5Mode(false).hashPrefix('');
        var viewBase = $('#hdUrlPrefix').val();

        $stateProvider
            .state('/', {
                url: "/",
                templateUrl: "index.html",
                //data: { pageTitle: 'All Card' },
                controller: "cardsController",
                pageKey: 'index'
            })
            .state('cardsNew', {
                url: "/cardsNew",
                templateUrl: "/Cards/Tmpl?Prefix=crdnew&type=Index",
                data: { pageTitle: 'Create New ' },
                controller: "cardNewController",
                pageKey: 'crdnew'
            })
            .state('cardSummary', {
                url: "/:CardId",
                templateUrl: "/Cards/Tmpl?Prefix=crdsumm&type=Index",
                data: { pageTitle: 'Card Summary' },
                controller: "cardSummaryController",
                pageKey: 'crdsumm'
            })
            .state('cardInfo', {
                url: "/:CardId/CardInfo",
                templateUrl: "/Cards/Tmpl?Prefix=crdinf&type=Index",
                data: { pageTitle: 'Card Info' },
                controller: "cardInfoController",
                pageKey: 'crdinf'
            })
            .state('contacts', {
                url: "/:CardId/Contact",
                templateUrl: "/Cards/Tmpl?Prefix=cont&type=Index",
                data: { pageTitle: 'Contacts' },
                controller: "contactsController",
                pageKey: 'cont'
            })
            .state('contactsDetail', {
                url: "/:CardId/Contact:id",
                templateUrl: "/Cards/Tmpl?Prefix=contd&type=Index",
                data: { pageTitle: 'Contacts' },
                controller: "contactsDetailController",
                pageKey: 'contd'
            })
            .state('contactsNew', {
                url: "/:CardId/Contact/New",
                templateUrl: "/Cards/Tmpl?Prefix=contnew&type=Index",
                data: { pageTitle: 'Contacts' },
                controller: "contactsNewController",
                pageKey: 'contmew'
            })
            .state('address', {
                url: "/:CardId/Address",
                templateUrl: "/Cards/Tmpl?Prefix=addr&type=Index",
                data: { pageTitle: 'Address' },
                controller: "addressController",
                pageKey: 'addr'
            })
            .state('addressDetail', {
                url: "/:CardId/Address:id",
                templateUrl: "/Cards/Tmpl?Prefix=addrd&type=Index",
                data: { pageTitle: 'Address' },
                controller: "addressDetailController",
                pageKey: 'addrd'
            })
            .state('addressNew', {
                url: "/:CardId/Address/New",
                templateUrl: "/Cards/Tmpl?Prefix=addrnew&type=Index",
                data: { pageTitle: 'Address' },
                controller: "addressNewController",
                pageKey: 'addrnew'
            })
            .state('pintries', {
                url: "/:CardId/PINTries",
                templateUrl: "/Cards/Tmpl?Prefix=pintries&type=Index",
                data: { pageTitle: 'PIN Tries' },
                controller: "pintriesController",
                pageKey: 'pintries'
            })

            .state('productPrice', {
                url: "/:CardId/ProductPrice",
                templateUrl: "/Cards/Tmpl?Prefix=pdp&type=Index",
                data: { pageTitle: 'Product Price' },
                controller: "productPriceController",
                pageKey: 'pdp'
            })
            .state('productPriceDetail', {
                url: "/:CardId/ProductPrice:id",
                templateUrl: "/Cards/Tmpl?Prefix=pdpd&type=Index",
                controller: "productPriceDetailController",
                pageKey: 'pdp'
            })
            .state('productPriceNew', {
                url: "/:CardId/ProductPrice/New",
                templateUrl: "/Cards/Tmpl?Prefix=pdpnew&type=Index",
                controller: "productPriceNewController",
                pageKey: 'pdp'
            })
            .state('discountRebate', {
                url: "/:CardId/DiscountRebate",
                templateUrl: "/Cards/Tmpl?Prefix=dnr&type=Index",
                data: { pageTitle: 'Product Price' },
                controller: "discountRebateController",
                pageKey: 'dnr'
            })
            .state('discountRebateDetail', {
                url: "/:CardId/DiscountRebate:id",
                templateUrl: "/Cards/Tmpl?Prefix=dnrd&type=Index",
                controller: "discountRebateDetailController",
                pageKey: 'dnrd'
            })
            .state('discountRebateNew', {
                url: "/:CardId/DiscountRebate/New",
                templateUrl: "/Cards/Tmpl?Prefix=dnrnew&type=Index",
                controller: "discountRebateNewController",
                pageKey: 'dnrnew'
            })

            .state('productAcceptance', {
                url: "/:CardId/UsageControl/ProductAcceptance",
                templateUrl: "/Cards/Tmpl?Prefix=pacpt&type=Index",
                data: { pageTitle: 'Product Acceptance' },
                controller: "productAcceptanceController",
                pageKey: 'pacpt'
            })
            //.state('productrestrictionsDetail', {
            //    url: "/:CardId/UsageControl/ProductRestrictions:id",
            //    templateUrl: "/Cards/Tmpl?Prefix=pretrd&type=Index",
            //    controller: "productRestrictionsDetailController",
            //    pageKey: 'pretrd'
            //})
            //.state('productrestrictionsNew', {
            //    url: "/:CardId/UsageControl/ProductRestrictions/New",
            //    templateUrl: "/Cards/Tmpl?Prefix=pretrnew&type=Index",
            //    controller: "productRestrictionsNewController",
            //    pageKey: 'pretrnew'
            //})

            .state('velocityLimit', {
                url: "/:CardId/UsageControl/VelocityLimit",
                templateUrl: "/Cards/Tmpl?Prefix=vellmt&type=Index",
                data: { pageTitle: 'Velocity Limit' },
                controller: "velocityLimitController",
                pageKey: 'vellmt'
            })
            .state('locationAcceptance', {
                url: "/:CardId/UsageControl/LocationAcceptance",
                templateUrl: "/Cards/Tmpl?Prefix=locacpt&type=Index",
                data: { pageTitle: 'Location Acception' },
                controller: "locationAcceptanceController",
                pageKey: 'locacpt'
            })
            //.state('StationDealer', {
            //    url: "/:CardId/UsageControl/LocationAcceptance:state",
            //    templateUrl: "/Cards/Tmpl?Prefix=state&type=Index",
            //    data: { pageTitle: 'Location Acception' },
            //    controller: "stationDealerController",
            //    pageKey: 'state'
            //})
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
        $rootScope.$on('$stateChangeStart', function (e, current, pre) {
            console.log('state change start')
            $rootScope.$broadcast('$stateChanged', current.pageKey);
        });

        $rootScope.$on('$stateChangeSuccess', function (e, current, pre) {
            console.log('route path' + $location.path());
        });

        $rootScope.$on('$viewContentLoaded', function (e, current, pre) {
            console.log('view loaded path' + $location.path());
            App.init();
        });
    }]);
}());