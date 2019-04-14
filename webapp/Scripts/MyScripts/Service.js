/// <reference path="../angular.js" />
/// <reference path="Module.js" />


app.service('crudService', function ($http) {

    
    //Create new record
    this.post = function (ExpenseItem) {
        var request = $http({
            method: "post",
            url: "/api/ExpenseItemsAPI",
            data:  ExpenseItem
        });
        return request;
    }
    //Get Single Records
    this.get = function (EmpNo) {
        return $http.get("/api/ExpenseItemsAPI/" + EmpNo);
    }

    //Get All items
    //this.getExpenseItem = function () {
    //    return $http.get("/api/ExpenseItemsAPI/");
    //}

    this.getExpenseItem = function (CurrentDetailId) {

        Indata = {
            'DetailId': CurrentDetailId
        }
        var request = $http.post("/api/ExpenseItemsAPI/GetExpenseItem", Indata);
        return request;
    }




    this.getcreateexpensedb = function (Expense) {
        var request = $http({
            method: "post",
            url: "/api/ExpensesAPI",
            data: Expense
        });
        return request;
    }


    //Update the Record
    this.put = function (EmpNo, ExpenseItem) {
        var request = $http({
            method: "put",
            url: "/api/ExpenseItemsAPI/" + EmpNo,
            data: ExpenseItem
        });
        return request;
    }
    //Delete the Record
    this.delete = function (Id) {
        var request = $http({
            method: "delete",
            url: "/api/ExpenseItemsAPI/" + Id
        });
        return request;
    }

    this.saveexpenseform = function (data) {
        var request = $http({
            method: "post",
            url: "/api/ExpensesAPI/saveExpense",
            data: data
        });
        return request;
    }

});
