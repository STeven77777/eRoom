(function () {
    var app = angular.module('loyaltyApp',
        ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'App.Utils', 'ui.router', 'ui.select', 'ngSanitize', 'ngDialog', 'thatisuday.dropzone', 'datePicker', 'ui.tinymce']);

    app.config(['$stateProvider', '$urlRouterProvider', '$locationProvider', 'ngDialogProvider', 'dropzoneOpsProvider', function ($stateProvider, $urlRouterProvider, $locationProvider, ngDialogProvider, dropzoneOpsProvider) {
        $locationProvider.html5Mode(false).hashPrefix('');
        var rooturl = $('#hdUrlPrefix').val();

        dropzoneOpsProvider.setOptions({
            url: '/upload_url',
            maxFilesize: '10',
        });

        $stateProvider
            //.state('/', {
            //    url: "/",
            //    templateUrl: "index.html",
            //    data: { pageTitle: 'All Members' },
            //    controller: "membersController",
            //    pageKey: 'index'
            //})
            .state('AdSysUsers', {
                url: "/SysUsers",
                templateUrl: rooturl + "/Admin/Tmpl?Prefix=AdSysUsers&type=Index",
                data: { pageTitle: 'Ops User' },
                controller: "sysUsersController",
                pageKey: 'AdSysUsers'
            })
            .state('AdSysUserNew', {
                url: "/SysUsers/New",
                templateUrl: rooturl + "/Admin/Tmpl?Prefix=AdSysUserNew&type=Index",
                data: { pageTitle: 'New Ops User' },
                controller: "sysUserNewController",
                pageKey: 'AdSysUserNew'
            })
            .state('AdSysUserInfo', {
                url: "/SysUsers/:userIdHash",
                templateUrl: rooturl + "/Admin/Tmpl?Prefix=AdSysUserInfo&type=Index",
                data: { pageTitle: 'Ops User Info' },
                controller: "sysUserInfoController",
                pageKey: 'AdSysUserInfo'
            })
            .state('AdUserRoleAccLst', {
                url: "/UserAccessManagement",
                templateUrl: rooturl + "/Admin/Tmpl?Prefix=AdUserRoleAccLst&type=Index",
                data: { pageTitle: 'User Access Management' },
                controller: "sysUserAccessMgmtController",
                pageKey: 'AdUserRoleAccLst'
            })
            .state('AdUserRoleAccNew', {
                url: "/UserAccessManagement/NewUserRoleAccess",
                templateUrl: rooturl + "/Admin/Tmpl?Prefix=AdUserRoleAccNew&type=Index",
                data: { pageTitle: 'New user Role Access' },
                controller: "sysAddUserAccessMgmtController",
                pageKey: 'AdUserRoleAccNew'
            })
            .state('AdUserRoleAccInfo', {
                url: "/UserAccessManagement/:userIdHash",
                templateUrl: rooturl + "/Admin/Tmpl?Prefix=AdUserRoleAccInfo&type=Index",
                data: { pageTitle: 'User Access Management Info' },
                controller: "sysUserAccessManagementInfo",
                pageKey: 'AdUserRoleAccInfo'
            })
            .state('AdRwdItms', {
                url: "/AdRwdItms",
                templateUrl: rooturl + "/Admin/Tmpl?Prefix=AdRwdItms&type=Index",
                data: { pageTitle: 'Reward Items' },
                controller: "rewardItemsController",
                pageKey: 'AdRwdItms'
            })
            .state('AdRwdItmNew', {
                url: "/AdRwdItms/New",
                templateUrl: rooturl + "/Admin/Tmpl?Prefix=AdRwdItmNew&type=Index",
                data: { pageTitle: 'Reward Items' },
                controller: "rewardItemNewController",
                pageKey: 'AdRwdItmNew'
            })
            .state('AdRwdItmInfo', {
                url: "/AdRwdItms/:rewardItemId",
                templateUrl: rooturl + "/Admin/Tmpl?Prefix=AdRwdItmInfo&type=Index",
                data: { pageTitle: 'Reward Items' },
                controller: "rewardItemInfoController",
                pageKey: 'AdRwdItmInfo'
            })
            .state('AdQRCodeCards', {
                url: "/QRCodeCards",
                templateUrl: rooturl + "/Admin/Tmpl?Prefix=AdQRCodeCards&type=Index",
                data: { pageTitle: 'QR Code Cards' },
                controller: "qrCodeCardsController",
                pageKey: 'AdQRCodeCards'
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
            $.unblockUI();
            //console.log('route path' + $location.path());
            var _location = $location.path();
            if (_location === '/new') {
                $rootScope.obj._type = 'new';
                $rootScope.obj.userIdHash = null;
            } else if (_location === '/') {
                $rootScope.obj._type = 'index';
                $rootScope.obj.userIdHash = null;
            } else {
                $rootScope.obj.userIdHash = pre.userIdHash;
                $rootScope.obj.rewardItemId = pre.rewardItemId;
                $rootScope.obj._type = 'edit';
            }
        });

        $rootScope.$on('$viewContentLoaded', function (e, current, pre) {
            console.log('view loaded path' + $location.path());
            App.init();
        });
    }]);
}());