(function () {
    var app = angular.module('loyaltyApp',
    ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'App.Utils', 'ui.router', 'ui.select', 'ngSanitize', 'ngDialog', 'thatisuday.dropzone']);

    app.config(['$stateProvider', '$urlRouterProvider', '$locationProvider', 'ngDialogProvider', 'dropzoneOpsProvider', function ($stateProvider, $urlRouterProvider, $locationProvider, ngDialogProvider, dropzoneOpsProvider) {
        $locationProvider.html5Mode(false).hashPrefix('');
        var viewBase = $('#hdUrlPrefix').val();

        dropzoneOpsProvider.setOptions({
            url : '/upload_url',
            maxFilesize : '10',
        });

        $stateProvider
            .state('/', {
                url: "/",
                templateUrl: "index.html",
                data: { pageTitle: 'All Customers' },
                controller: "customersController",
                pageKey: 'index'
            })
            //.state('allcards', {
            //    url: "/Cards",
            //    templateUrl: "/Customers/Tmpl?Prefix=allcards&type=Index",
            //    data: { pageTitle: 'Card List' },
            //    controller: "allCardsController",
            //    pageKey: 'allcards'
            //})
            .state('accountSummary', {
                url: "/:customerId",
                templateUrl: "/Customers/Tmpl?Prefix=acctsum&type=Index",
                data: { pageTitle: 'Account Summary' },
                controller: "accountSummaryController",
                pageKey: 'acctsum'
            })
            .state('AcctCusInfo', {
                url: "/:customerId/AcctCusInfo",
                templateUrl: "/Customers/Tmpl?Prefix=cusdt&type=Index",
                data: { pageTitle: 'Account Customer Info' },
                controller: "acctCusInfoController",
                pageKey: 'cusdt'
            })
            .state('customerContacts', {
                url: "/:customerId/Contact",
                templateUrl: "/Customers/Tmpl?Prefix=cont&type=Index",
                data: { pageTitle: 'Customer Contacts' },
                controller: "customersContactsController",
                pageKey: 'cont'
            })
            .state('customerContactsDetail', {
                url: "/:customerId/Contact:id",
                templateUrl: "/Customers/Tmpl?Prefix=contd&type=Index",
                data: { pageTitle: 'Customer Contacts' },
                controller: "customersContactsDetailController",
                pageKey: 'contd'
            })
            .state('customerContactsNew', {
                url: "/:customerId/Contact/New",
                templateUrl: "/Customers/Tmpl?Prefix=contnew&type=Index",
                data: { pageTitle: 'Customer Contacts' },
                controller: "customersContactsNewController",
                pageKey: 'contmew'
            })
            .state('customerAddress', {
                url: "/:customerId/Address",
                templateUrl: "/Customers/Tmpl?Prefix=addr&type=Index",
                data: { pageTitle: 'Customer Address' },
                controller: "customerAddressController",
                pageKey: 'addr'
            })
            .state('customerAddressDetail', {
                url: "/:customerId/Address:id",
                templateUrl: "/Customers/Tmpl?Prefix=addrd&type=Index",
                data: { pageTitle: 'Customer Address' },
                controller: "customerAddressDetailController",
                pageKey: 'addrd'
            })
            .state('customerAddressNew', {
                url: "/:customerId/Address/New",
                templateUrl: "/Customers/Tmpl?Prefix=addrnew&type=Index",
                data: { pageTitle: 'Customer Address' },
                controller: "customerAddressNewController",
                pageKey: 'addrnew'
            })
            .state('financialInfo', {
                url: "/:customerId/FinancialInfo",
                templateUrl: "/Customers/Tmpl?Prefix=fini&type=Index",
                data: { pageTitle: 'Financial Info' },
                controller: "financialController",
                pageKey: 'fini'
            })
            .state('financialPosition', {
                url: "/:customerId/FinancialPosition",
                templateUrl: "/Customers/Tmpl?Prefix=finps&type=Index",
                data: { pageTitle: 'Financial Position' },
                controller: "financialPositionController",
                pageKey: 'finps'
            })
            .state('creditability', {
                url: "/:customerId/Creditability",
                templateUrl: "/Customers/Tmpl?Prefix=crda&type=Index",
                data: { pageTitle: 'Creditability' },
                controller: "creditabilityController",
                pageKey: 'crda'
            })
            .state('securityDeposit', {
                url: "/:customerId/SecurityDeposit",
                templateUrl: "/Customers/Tmpl?Prefix=secdeptlst&type=Index",
                data: { pageTitle: 'Security Deposit' },
                controller: "securityDepositLst",
                pageKey: 'secdeptlst'
            })
            .state('securityDepositNew', {
                url: "/:customerId/SecurityDeposit/New",
                templateUrl: "/Customers/Tmpl?Prefix=secdeptnew&type=Index",
                data: { pageTitle: 'Security Deposit New' },
                controller: "securityDepositNew",
                pageKey: 'secdeptnew'
            })
            .state('securityDepositDetail', {
                url: "/:customerId/SecurityDeposit:id",
                templateUrl: "/Customers/Tmpl?Prefix=secdeptd&type=Index",
                data: { pageTitle: 'Security Deposit Detail' },
                controller: "securityDepositDetail",
                pageKey: 'secdeptd'
            })
            .state('productPrice', {
                url: "/:customerId/ProductPrice",
                templateUrl: "/Customers/Tmpl?Prefix=pdp&type=Index",
                data: { pageTitle: 'Product Price' },
                controller: "productPriceController",
                pageKey: 'pdp'
            })
            .state('productPriceDetail', {
                url: "/:customerId/ProductPrice:id",
                templateUrl: "/Customers/Tmpl?Prefix=pdpd&type=Index",
                controller: "productPriceDetailController",
                pageKey: 'pdp'
            })
            .state('productPriceNew', {
                url: "/:customerId/ProductPrice/New",
                templateUrl: "/Customers/Tmpl?Prefix=pdpnew&type=Index",
                controller: "productPriceNewController",
                pageKey: 'pdp'
            })
            .state('discountRebate', {
                url: "/:customerId/DiscountRebate",
                templateUrl: "/Customers/Tmpl?Prefix=dnr&type=Index",
                data: { pageTitle: 'Product Price' },
                controller: "discountRebateController",
                pageKey: 'dnr'
            })
            .state('discountRebateDetail', {
                url: "/:customerId/DiscountRebate:id",
                templateUrl: "/Customers/Tmpl?Prefix=dnrd&type=Index",
                controller: "discountRebateDetailController",
                pageKey: 'dnrd'
            })
            .state('discountRebateNew', {
                url: "/:customerId/DiscountRebate/New",
                templateUrl: "/Customers/Tmpl?Prefix=dnrnew&type=Index",
                controller: "discountRebateNewController",
                pageKey: 'dnrnew'
            })

            .state('productAcceptance', {
                url: "/:customerId/UsageControl/ProductAcceptance",
                templateUrl: "/Customers/Tmpl?Prefix=pacpt&type=Index",
                data: { pageTitle: 'Product Acceptance' },
                controller: "productAcceptanceController",
                pageKey: 'pretr'
            })
            //.state('productrestrictionsDetail', {
            //    url: "/:customerId/UsageControl/ProductRestrictions:id",
            //    templateUrl: "/Customers/Tmpl?Prefix=pretrd&type=Index",
            //    controller: "productRestrictionsDetailController",
            //    pageKey: 'pretrd'
            //})
            //.state('productrestrictionsNew', {
            //    url: "/:customerId/UsageControl/ProductRestrictions/New",
            //    templateUrl: "/Customers/Tmpl?Prefix=pretrnew&type=Index",
            //    controller: "productRestrictionsNewController",
            //    pageKey: 'pretrnew'
            //})

            .state('velocityLimit', {
                url: "/:customerId/UsageControl/VelocityLimit",
                templateUrl: "/Customers/Tmpl?Prefix=vellmt&type=Index",
                data: { pageTitle: 'Velocity Limit' },
                controller: "velocityLimitController",
                pageKey: 'vellmt'
            })
            .state('locationAcceptance', {
                url: "/:customerId/UsageControl/LocationAcceptance",
                templateUrl: "/Customers/Tmpl?Prefix=locacpt&type=Index",
                data: { pageTitle: 'Location Acception' },
                controller: "locationAcceptanceController",
                pageKey: 'locacpt'
            })
            //.state('StationDealer', {
            //    url: "/:customerId/UsageControl/LocationAcceptance:state",
            //    templateUrl: "/Customers/Tmpl?Prefix=state&type=Index",
            //    data: { pageTitle: 'Location Acception' },
            //    controller: "stationDealerController",
            //    pageKey: 'state'
            //})

            .state('costCentre', {
                url: "/:customerId/CostCentre",
                templateUrl: "/Customers/Tmpl?Prefix=cstctr&type=Index",
                data: { pageTitle: 'Cost Centre' },
                controller: "costCentreController",
                pageKey: 'cstctr'
            })
            .state('costCentreDetail', {
                url: "/:customerId/CostCentre:id",
                templateUrl: "/Customers/Tmpl?Prefix=cstctrd&type=Index",
                data: { pageTitle: 'Cost Centre' },
                controller: "costCentreDetailController",
                pageKey: 'cstctrd'
            })
            .state('costCentreNew', {
                url: "/:customerId/CostCentre/New",
                templateUrl: "/Customers/Tmpl?Prefix=cstctrnew&type=Index",
                data: { pageTitle: 'Cost Centre' },
                controller: "costCentreNewController",
                pageKey: 'cstctrnew'
            })

            .state('ccProductRestrictions', {
                url: "/:customerId/CostCentre/ProductRestrictions",
                templateUrl: "/Customers/Tmpl?Prefix=ccpretr&type=Index",
                data: { pageTitle: 'Product Price' },
                controller: "ccProductRestrictionsController",
                pageKey: 'ccpretr'
            })
            .state('ccProductrestrictionsDetail', {
                url: "/:customerId/CostCentre/ProductRestrictions:id",
                templateUrl: "/Customers/Tmpl?Prefix=ccpretrd&type=Index",
                controller: "ccProductRestrictionsDetailController",
                pageKey: 'ccpretrd'
            })
            .state('ccProductrestrictionsNew', {
                url: "/:customerId/CostCentre/ProductRestrictions/New",
                templateUrl: "/Customers/Tmpl?Prefix=ccpretrnew&type=Index",
                controller: "ccProductRestrictionsNewController",
                pageKey: 'ccpretrnew'
            })

            .state('ccVelocityLimit', {
                url: "/:customerId/CostCentre/VelocityLimit",
                templateUrl: "/Customers/Tmpl?Prefix=ccvellmt&type=Index",
                data: { pageTitle: 'Velocity Limit' },
                controller: "ccVelocityLimitController",
                pageKey: 'ccvellmt'
            })
            .state('ccLocationAcceptance', {
                url: "/:customerId/CostCentre/LocationAcceptance",
                templateUrl: "/Customers/Tmpl?Prefix=cclocacpt&type=Index",
                data: { pageTitle: 'Location Acception' },
                controller: "ccLocationAcceptanceController",
                pageKey: 'cclocacpt'
            })
            .state('ccStationDealer', {
                url: "/:customerId/CostCentre/LocationAcceptance:state",
                templateUrl: "/Customers/Tmpl?Prefix=ccstate&type=Index",
                data: { pageTitle: 'Location Acception' },
                controller: "ccStationDealerController",
                pageKey: 'ccstate'
            })
            //.state('allcards', {
            //    url: "/Cards",
            //    templateUrl: "/Customers/Tmpl?Prefix=allcards&type=Index",
            //    data: { pageTitle: 'Card List' },
            //    controller: "allCardsController",
            //    pageKey: 'allcards'
            //})
        
            .state('cardlist', {
                url: "/:customerId/Cardlist",
                templateUrl: "/Customers/Tmpl?Prefix=crdlst&type=Index",
                data: { pageTitle: 'Card List' },
                controller: "cardListController",
                pageKey: 'crdlst'
            })
            .state('cardlistNew', {
                url: "/:customerId/Cards/New",
                templateUrl: "/Customers/Tmpl?Prefix=newcrd&type=Index",
                data: { pageTitle: 'Create Cards' },
                controller: "createCardsController",
                pageKey: 'newcrd'
            })
            //.state('cardlistDetail', {
            //    url: "/Customers/:customerId/Cards/:cardId",
            //    templateUrl: "/Customers/Tmpl?Prefix=crdd&type=Index",
            //    data: { pageTitle: 'Card Details' },
            //    controller: "cardDetailsController",
            //    pageKey: 'crdd'
            //})
            .state('eventnotification', {
                url: "/:customerId/Event-notification",
                templateUrl: "/Customers/Tmpl?Prefix=evntntf&type=Index",
                data: { pageTitle: 'Event & Notification' },
                controller: "eventNotificationController",
                pageKey: 'evntntf'
            })
            .state('eventnotificationDetail', {
                url: "/:customerId/Event-notification:id",
                templateUrl: "/Customers/Tmpl?Prefix=evntntfd&type=Index",
                data: { pageTitle: 'Event & Notification' },
                controller: "eventNotificationDetailController",
                pageKey: 'evntntfd'
            })
            .state('eventnotificationNew', {
                url: "/:customerId/Event-notification/New",
                templateUrl: "/Customers/Tmpl?Prefix=evntntfnew&type=Index",
                data: { pageTitle: 'Event & Notification' },
                controller: "eventNotificationNewController",
                pageKey: 'evntntfnew'
            })
            .state('filemanager', {
                url: "/:customerId/FileManager",
                templateUrl: "/Customers/Tmpl?Prefix=fmgr&type=Index",
                data: { pageTitle: 'File Manager' },
                controller: "fileManagerController",
                pageKey: 'fmgr'
            })
            .state('transactions', {
                url: "/:customerId/Transactions",
                templateUrl: "/Customers/Tmpl?Prefix=txn&type=Index",
                data: { pageTitle: 'Transactions' },
                controller: "transactionsController",
                pageKey: 'txn'
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