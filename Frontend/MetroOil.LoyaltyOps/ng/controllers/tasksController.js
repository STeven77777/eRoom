(function () {
    var injectParams = ['$scope', '$rootScope', '$location', '$window', '$timeout', 'ngDialog'];
    
    var myTasksController = function ($scope, $rootScope, $location, $window, $timeout, ngDialog) {
        // local declare
        var vm = this;
        //$scope.backToList = function () {
        //    window.location.href = "/";
        //};
        $scope.ApproveApplication = function (val) {
            ngDialog.open({
                template: 'ApproveApplication',
                controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: 'Confirmation', appId: val }
            });
        }

        $scope.ReturnApplication = function (val) {
            ngDialog.open({
                template: 'ReturnApplication',
                controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: 'Return Application', appId: val }
            });
        }

        $scope.RejectApplication = function (val) {
            ngDialog.open({
                template: 'RejectApplication',
                controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: 'Confirmation', appId: val }
            });
        }

        $scope.ApproveTransaction= function (val) {
            ngDialog.open({
                template: 'ApproveTransaction',
                controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: 'Confirmation', appId: val }
            });
        }

        $scope.ReturnTransaction = function (val) {
            ngDialog.open({
                template: 'ReturnTransaction',
                controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: 'Return Transaction', appId: val }
            });
        }

        $scope.RejectTransaction= function (val) {
            ngDialog.open({
                template: 'RejectTransaction',
                controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: 'Confirmation', appId: val }
            });
        }


        $scope.CloseCollection= function (val) {
            ngDialog.open({
                template: 'CloseCollection',
                controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: 'Confirmation', appId: val }
            });
        }

        $scope.CollectionFollowUp = function (val) {
            ngDialog.open({
                template: 'CollectionFollowUp',
                controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: 'Collection Follow Up', appId: val }
            });
        }

        $scope.MultipleAdjApr = function (val) {
            $rootScope.batchLst = {
                colVis: {
                    "buttonClass": 'display-none'
                },
                serverSide: true,
                "autoWidth": false,
                "preDrawCallback": function (settings) {
                    $.blockUI();
                },
                "drawCallback": function (settings) {
                    $.unblockUI();
                },
                checkBox: false,
                "scrollX": true,
                "ordering": false,
                id: 'MultipleAdjApr',
                ajax: '/Tasks/GetTxns?' + $.param({ Ind: 1 }),
                //edit: {
                //    level: 'scope',
                //    func: 'gotoCustomerDetail'
                //},
                oLanguage: {
                    sSearchPlaceholder: "Search",
                    sLengthMenu: "Show _MENU_",
                    sSearch: "<span><i class='fa fa-search'></i></span>",
                    oPaginate: {
                        sNext: '<span class="pagination-default"></span><span class="pagination-fa"><i class="fa fa-angle-right" ></i></span>',
                        sPrevious: '<span class="pagination-default"></span><span class="pagination-fa"><i class="fa fa-angle-left" ></i></span>'
                    }
                }
            };

            ngDialog.open({
                width: '80%',
                template: 'MultipleAdjApr',
                controller: 'myTasksController',
                className: 'ngdialog-theme-default',
                data: { title: 'Multiple Adjustment Approval', appId: val }
            });
        }
    };

    myTasksController.$inject = injectParams;

    angular.module('loyaltyApp').controller('myTasksController', myTasksController);
}());