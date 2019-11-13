(function () {
    var app = angular.module('loyaltyApp',
    ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'App.Utils', 'ui.router', 'ui.select', 'ngSanitize', 'ngDialog', 'thatisuday.dropzone', 'datePicker']);

    app.config(['$stateProvider', '$urlRouterProvider', '$locationProvider', 'ngDialogProvider', 'dropzoneOpsProvider', function ($stateProvider, $urlRouterProvider, $locationProvider, ngDialogProvider, dropzoneOpsProvider) {
        $locationProvider.html5Mode(false).hashPrefix('');
        var rooturl = $('#hdUrlPrefix').val();

        dropzoneOpsProvider.setOptions({
            url : '/upload_url',
            maxFilesize : '10',
        });

        $stateProvider
            .state('/', {
                url: "/",
                templateUrl: rooturl + "/Members/Tmpl?Prefix=MemSearch",
                data: { pageTitle: 'All Members' },
                controller: "membersController",
                pageKey: 'MemSearch'
            })
            .state('MemNew', {
                url: "/New",
                templateUrl: rooturl + "/Members/Tmpl?Prefix=MemNew&type=Index",
                data: { pageTitle: 'New Member' },
                controller: "newController",
                pageKey: 'MemNew'
            })
            .state('MemSumm', {
                url: "/:acctNoHash",
                templateUrl: rooturl + "/Members/Tmpl?Prefix=MemSumm&type=Index",
                data: { pageTitle: 'Member Summary' },
                controller: "summaryController",
                pageKey: 'MemSumm'
            })
            .state('MemInfo', {
                url: "/:acctNoHash/Info",
                templateUrl: rooturl + "/Members/Tmpl?Prefix=MemInfo&type=Index",
                data: { pageTitle: 'Member Info' },
                controller: "infoController",
                pageKey: 'MemInfo'
            })
            //.state('contact', {
            //    url: "/:acctNoHash/Contact",
            //    templateUrl: rooturl + "/Members/Tmpl?Prefix=cont&type=Index",
            //    data: { pageTitle: 'Member Contact' },
            //    controller: "contactController",
            //    pageKey: 'cont'
            //})
            //.state('contactsNew', {
            //    url: "/:acctNoHash/Contact/New",
            //    templateUrl: rooturl + "/Members/Tmpl?Prefix=contnew&type=Index",
            //    data: { pageTitle: 'Member Contact' },
            //    controller: "contactNewController",
            //    pageKey: 'contmew'
            //})
            .state('MemCont', {
                url: "/:acctNoHash/Contact",
                templateUrl: rooturl + "/Members/Tmpl?Prefix=MemCont&type=Index",
                data: { pageTitle: 'Member Contact' },
                controller: "contactController",
                pageKey: 'MemCont'
            })

            .state('MemCrds', {
                url: "/:acctNoHash/Cards",
                templateUrl: rooturl + "/Members/Tmpl?Prefix=MemCrds&type=Index",
                data: { pageTitle: 'Card History' },
                controller: "cardsController",
                pageKey: 'MemCrds'
            })
            .state('MemCrdInfo', {
                url: "/:acctNoHash/Cards/Info",
                templateUrl: rooturl + "/Members/Tmpl?Prefix=MemCrdInfo&type=Index",
                data: { pageTitle: 'Card Info' },
                controller: "cardInfoController",
                pageKey: 'MemCrdInfo'
            })
            .state('MemCrdD', {
                url: "/:acctNoHash/Cards/:cardNo",
                templateUrl: rooturl + "/Members/Tmpl?Prefix=MemCrdD&type=Index",
                data: { pageTitle: 'Member Card' },
                controller: "cardDetailController",
                pageKey: 'MemCrdD'
            })

            .state('MemAddr', {
                url: "/:acctNoHash/Address",
                templateUrl: rooturl + "/Members/Tmpl?Prefix=MemAddr&type=Index",
                data: { pageTitle: 'Member Address' },
                controller: "addressController",
                pageKey: 'MemAddr'
            })
            .state('MemAddrNew', {
                url: "/:acctNoHash/Address/New",
                templateUrl: rooturl + "/Members/Tmpl?Prefix=MemAddrNew&type=Index",
                data: { pageTitle: 'Member Address' },
                controller: "addressNewController",
                pageKey: 'MemAddrNew'
            })
            .state('MemAddrD', {
                url: "/:acctNoHash/Address/:addressId",
                templateUrl: rooturl + "/Members/Tmpl?Prefix=MemAddrD&type=Index",
                data: { pageTitle: 'Member Address' },
                controller: "addressDetailController",
                pageKey: 'MemAddrD'
            })
            .state('MemTxnPoints', {
                url: "/:acctNoHash/Transactions/Points",
                templateUrl: rooturl + "/Members/Tmpl?Prefix=MemTxnPoints&type=Index",
                data: { pageTitle: 'Transactions' },
                controller: "txnPointsController",
                pageKey: 'MemTxnPoints'
            })
            .state('MemTxnVouchers', {
                url: "/:acctNoHash/Transactions/Vouchers",
                templateUrl: rooturl + "/Members/Tmpl?Prefix=MemTxnVouchers&type=Index",
                data: { pageTitle: 'Transactions' },
                controller: "txnVouchersController",
                pageKey: 'MemTxnVouchers'
            })
            .state('MemTxnVoucherDetail', {
                url: "/:acctNoHash/Transactions/Vouchers/:voucherNo",
                templateUrl: rooturl + "/Members/Tmpl?Prefix=MemTxnVoucherDetail&type=Index",
                data: { pageTitle: 'Transactions' },
                controller: "txnVoucherDetailController",
                pageKey: 'MemTxnVoucherDetail'
            })
            .state('MemTxnStickers', {
                url: "/:acctNoHash/Transactions/Stickers",
                templateUrl: rooturl + "/Members/Tmpl?Prefix=MemTxnStickers&type=Index",
                data: { pageTitle: 'Transactions' },
                controller: "txnStickersController",
                pageKey: 'MemTxnStickers'
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
            //console.log('route path' + $location.path());
            $.unblockUI();
            var _location = $location.path();
            if (_location === '/new') {
                $rootScope.obj._type = 'new';
                $rootScope.obj.acctNoHash = null;
            } else if (_location === '/') {
                $rootScope.obj._type = 'index';
                $rootScope.obj.acctNoHash = null;
            } else {
                $rootScope.obj.acctNoHash = pre.acctNoHash;

                if (pre.addressId) {
                    $rootScope.obj.addressId = pre.addressId;
                }
                if (pre.cardNo) {
                    $rootScope.obj.cardNo = pre.cardNo;
                }
                if (pre.voucherNo) {
                    $rootScope.obj.voucherNo = pre.voucherNo;
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