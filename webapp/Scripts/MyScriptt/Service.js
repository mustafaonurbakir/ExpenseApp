/// <reference path="../angular.js" />
/// <reference path="Module.js" />


app.service('angularindexService', function ($http) {

    
    //Create new record
    this.post = function (Expense) {
        var request = $http({
            method: "post",
            url: "/api/ExpensesAPI",
            data: ExpenseItem
        });
        return request;
    }
    //Get Single Records
    this.get = function (EmpNo) {
        return $http.get("/api/ExpensesAPI/" + EmpNo);
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
            url: "/api/ExpensesAPI/" + EmpNo,
            data: ExpenseItem
        });
        return request;
    }
    //Delete the Record
    this.delete = function (Id) {
        var request = $http({
            method: "delete",
            url: "/api/ExpensesAPI/" + Id
        });
        return request;
    }






    this.getExpenseByUserId = function (CurrentUserId) { //for employee/index
       
        Indata = {
            'User' : CurrentUserId
        }
        var request = $http.post("/api/ExpensesAPI/getExpenseByUserId", Indata);
        return request;
    }

    this.getExpensesByStatus = function (request) { //for employee/index

        Indata = {
            'Role': request
        }
        var request = $http.post("/api/ExpensesAPI/getExpenseByStatus", Indata);
        return request;

    }

    this.getExpenseItem = function (CurrentDetailId) {

        Indata = {
            'DetailId': CurrentDetailId
        }
        var request = $http.post("/api/ExpenseItemsAPI/GetExpenseItem", Indata);
        return request;
    }

    this.ApproveForm = function (CurrentDetailId) {
        alert("servise geldi");
        Indata = {
            'DetailId': CurrentDetailId
        }
        var request = $http.post("/api/ExpenseItemsAPI/ApproveForm", Indata);
        return request;
    }

    this.EditForm = function (CurrentDetailId) {
        alert("servise geldi");
        Indata = {
            'DetailId': CurrentDetailId
        }
        var request = $http.post("/api/ExpenseItemsAPI/EditForm", Indata);
        return request;
    }

    this.Payform = function (CurrentDetailId) {
        alert("servise geldi");
        Indata = {
            'DetailId': CurrentDetailId
        }
        var request = $http.post("/api/ExpenseItemsAPI/Payform", Indata);
        return request;
    }

    this.ConfirmReject = function (data) {
        alert("servise geldi");
        //Indata = {
        //    'DetailId': data
        //}
        var request = $http.post("/api/ExpenseItemsAPI/ConfirmReject", data);
        return request;
    }

});
