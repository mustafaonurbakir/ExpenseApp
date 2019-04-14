app.controller("Logincontroller", function ($scope, Loginservice) {
    'use strict';

    angular
        .module('app')
        .controller('Logincontroller', Logincontroller);

    Logincontroller.$inject = ['$scope'];

    function Logincontroller($scope) {
        $scope.title = 'Logincontroller';

        activate();

        function activate() { }
    }

    $scope.login = function () {

        var reply = Loginservice.login()

    }

})();
