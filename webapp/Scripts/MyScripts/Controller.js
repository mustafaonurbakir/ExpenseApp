
app.controller('crudController', function ($scope, crudService) {
   
    
    $scope.data = {};
    $scope.data.expenseitemdata = [{
        ExpenseType : "" ,
        Description : "" ,
        Amount : "" ,
        ExpenseDate : ""

    }];
    
    $scope.loadexpenses = function () {
        var promiseGet = crudService.getExpenseItem($scope.CurrentDetailId); //The MEthod Call from service
        alert("ıd: " + $scope.CurrentDetailId);
        promiseGet.then(function (pl) { $scope.ExpenseItem = pl.data },
              function (errorPl) {
                  $log.error('failure loading Employee', errorPl);
              });
    }


    function yukle() {
        alert($scope.CurrentUserId)
        $scope.data.expensedata = {
            CurrentUserId: $scope.CurrentUserId,
                TotalAmount: 0,
                CreatedDate: new Date().toISOString().slice(0, 19).replace('T', ' ')
        };
    }

    $scope.save = function () {
       
        $scope.data.expenseitemdata.push({
            ExpenseType: "",
            Description: "",
            Amount: "",
            ExpenseDate: ""

        } );
            
    };

    $scope.saveform = function () {
        yukle();
        var promisePost = crudService.saveexpenseform($scope.data);
    }


});