// Code goes here
var app = angular.module('app', ['ui.bootstrap']);


////////////////////////////////////////////////////////////////////////////////
/// TREEVIEW  //////////////////////////////////////////////////////////////////
app.directive('treeView', function ($compile) {
    return {
        restrict: 'E',
        scope: {
            localNodes: '=model',
            localClick: '&click'
        },
        link: function (scope, tElement, tAttrs, transclude) {

            var maxLevels = (angular.isUndefined(tAttrs.maxlevels)) ? 10 : tAttrs.maxlevels;
            var hasCheckBox = (angular.isUndefined(tAttrs.checkbox)) ? false : true;
            scope.showItems = [];

            scope.showHide = function (ulId) {
                var hideThis = document.getElementById(ulId);
                var showHide = angular.element(hideThis).attr('class');
                angular.element(hideThis).attr('class', (showHide === 'show' ? 'hide' : 'show'));
            }

            scope.showIcon = function (node) {
                if (!angular.isUndefined(node.SubPages1)) return true;
            }

            scope.checkIfChildren = function (node) {
                if (!angular.isUndefined(node.SubPages1)) return true;
            }

            /////////////////////////////////////////////////
            /// SELECT ALL CHILDRENS
            // as seen at: http://jsfiddle.net/incutonez/D8vhb/5/
            function parentCheckChange(item) {
                for (var i in item.SubPages1) {
                    item.SubPages1[i].checked = item.checked;
                    if (item.SubPages1[i].SubPages1) {
                        parentCheckChange(item.SubPages1[i]);
                    }
                }
            }

            scope.checkChange = function (node) {
                if (node.SubPages1) {
                    parentCheckChange(node);
                }
            }
            /////////////////////////////////////////////////

            function renderTreeView(collection, level, max) {
                var text = '';
                text += '<li ng-repeat="n in ' + collection + '" >';
                text += '<span ng-show=showIcon(n) class="show-hide" ng-click=showHide(n.id)><i class="fa fa-plus-square"></i></span>';
                text += '<span ng-show=!showIcon(n) style="padding-right: 13px"></span>';

                if (hasCheckBox) {
                    text += '<input class="tree-checkbox" type=checkbox ng-model=n.checked ng-change=checkChange(n)>';
                }


                text += '<span class="edit" ng-click=localClick({node:n})><i class="fa fa-pencil"></i></span>'


                text += '<label>{{n.tuan}}</label>';

                if (level < max) {
                    text += '<ul id="{{n.id}}" class="hide" ng-if=checkIfChildren(n)>' + renderTreeView('n.SubPages1', level + 1, max) + '</ul></li>';
                } else {
                    text += '</li>';
                }

                return text;
            }// end renderTreeView();

            try {
                var text = '<ul class="tree-view-wrapper">';
                text += renderTreeView('localNodes', 1, maxLevels);
                text += '</ul>';
                tElement.html(text);
                $compile(tElement.contents())(scope);
            }
            catch (err) {
                tElement.html('<b>ERROR!!!</b> - ' + err);
                $compile(tElement.contents())(scope);
            }
        }
    };
});

app.controller('treeCtrl', ['$scope', 'dialog', function ($scope, dialog) {

    $scope.nodes = [
      {
          id: 2, tuan: '1ª Habilitação', checked: true,


          SubPages1: [
            { id: 3, tuan: 'Level2 - A', checked: true },
            { id: 4, tuan: 'Level2 - B', checked: true }
          ]
      },

      {
          id: 3, tuan: 'Veículo', checked: true,
          entity: [
              {
                  entidade: {
                      img: 'http://placehold.it/500',
                      obrigatorio: true
                  }
              }
          ],

          SubPages1: [
            { id: 3, name: 'Level2 - A', checked: true },
            { id: 4, name: 'Level2 - B', checked: true },
            {
                id: 5, name: 'Level2 - C', checked: true, children: [
                { id: 6, name: 'Level3 - A', checked: false },
                {
                    id: 7, name: 'Level3 - B', checked: false, children: [
                    { id: 8, name: 'Level4 - A', checked: false }
                    ]
                }
                ]
            }
          ]
      },
      {
          id: 4, name: 'Habilitação', checked: true,
          entity: [
              {
                  entidade: {
                      img: 'http://placehold.it/500',
                      obrigatorio: true
                  }
              }
          ],

          SubPages1: [
            { id: 3, name: 'Level2 - A', checked: true },
            { id: 4, name: 'Level2 - B', checked: true },
            {
                id: 5, name: 'Level2 - C', checked: true, children: [
                { id: 6, name: 'Level3 - A', checked: false },
                {
                    id: 7, name: 'Level3 - B', checked: false, children: [
                    { id: 8, name: 'Level4 - A', checked: false }
                    ]
                }
                ]
            }
          ]
      },
    ];


    $scope.myClick = function (node) {
        //alert('Clicked [' + node.name + '] state is [' + node.checked + ']');

        var confirm = dialog.confirm('Editar', node);
        confirm.result.then(function (btn) {

        });
    };

}]);


////////////////////////////////////////////////////////////////////////////////////////////
/// DIALOG  ////////////////////////////////////////////////////////////////////////////////
app.controller('dialogConfirmCtrl', ['$scope', '$modalInstance', 'header', 'msg',
    function ($scope, $modalInstance, header, msg) {
        //-- Variables -----//

        $scope.header = (angular.isDefined(header)) ? header : 'Confirmation';
        //$scope.msg = (angular.isDefined(msg)) ? msg : 'Confirmation required.';
        $scope.node = msg;
        //-- Methods -----//

        $scope.no = function () {
            $modalInstance.dismiss('no');
        }; // end close

        $scope.yes = function () {
            $modalInstance.close('yes');
        }; // end yes
    }
]);


app.service('dialog', ["$modal", function ($modal) {
    var _confirm = function (header, msg, stat) {
        return $modal.open({
            templateUrl: 'confirm.html',
            controller: 'dialogConfirmCtrl',
            backdrop: (stat ? 'static' : true),
            keyboard: (stat ? false : true),
            resolve: {
                header: function () {
                    return angular.copy(header);
                },
                msg: function () {
                    return angular.copy(msg);
                }
            }
        }); // end modal.open
    };

    return {
        confirm: _confirm
    };

}]);