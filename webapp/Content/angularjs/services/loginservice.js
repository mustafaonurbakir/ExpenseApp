app.service('logincontroller', function ($http) {
    this.login = function () {
        return $http({
            method: "post",
            data: request,
            url: window.actionUrls.loginPage
        });
    }
});