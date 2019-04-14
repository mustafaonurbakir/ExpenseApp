//
//
//
//

app.controller('angularindexController', function ($scope, angularindexService) {
   
    
    $scope.loadexpenses = function() {
         if ($scope.CurrentUserRole === "Employee") {
             var promiseGet = angularindexService.getExpenseByUserId($scope.CurrentUserId); //The MEthod Call from service

            promiseGet.then(function (pl) { $scope.Expense = pl.data },
                  function (errorPl) {
                      $log.error('failure loading Employee', errorPl);
                  });
        }
        else if ($scope.CurrentUserRole ==="Manager") {
            //var request = { CurrentUserId: 'Manager' };
            var promiseGet = angularindexService.getExpensesByStatus($scope.CurrentUserRole); //The MEthod Call from service

            promiseGet.then(function (pl) { $scope.Expense = pl.data },
                  function (errorPl) {
                      $log.error('failure loading Employee', errorPl);
                  });
        }
        else if ($scope.CurrentUserRole === "Accountant") {
            var promiseGet = angularindexService.getExpensesByStatus($scope.CurrentUserRole); //The MEthod Call from service

            promiseGet.then(function (pl) { $scope.Expense = pl.data },
                  function (errorPl) {
                      $log.error('failure loading Employee', errorPl);
                  });
        }
            
    }
    
    function loadexpenseitems (Emp) {
        var promiseGet = angularindexService.getExpenseItem(Emp.ID); //The MEthod Call from service
            promiseGet.then(function (pl) { $scope.ExpenseItem = pl.data },
                  function (errorPl) {
                      $log.error('failure loading Employee', errorPl);
                  });
    }

    $scope.get = function (Emp) {
        $scope.showhide = false;
        loadexpenseitems(Emp);
        $scope.CurrentDetail = Emp;
    }


    $scope.ApproveForm = function () {
        var temp = $scope.CurrentDetail.ID;
        var promiseGet = angularindexService.ApproveForm(temp);
    }

    $scope.Save = function () {
        var temp = $scope.CurrentDetail.ID;
        var promiseGet = angularindexService.EditForm(temp);
    }

    $scope.Payform = function () {
        var temp = $scope.CurrentDetail.ID;
        var promiseGet = angularindexService.Payform(temp);
    }

    $scope.ConfirmReject = function () {

        var DetailId = $scope.CurrentDetail.ID;
        var RejectDefiniton = $scope.RejectDefiniton;
        var data = {DetailId, RejectDefiniton };

        var promiseGet = angularindexService.ConfirmReject(data);
    }
    
});