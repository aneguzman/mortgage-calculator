mortgageCalculatorApp.controller('RegisterController', function ($rootScope, $scope, $location, registerService, $window) {

    $scope.register = function () {
        registerService.register($scope.registerInfo)
            .then(function (data) {
                if (data.success) {
                    $window.localStorage.setItem('loggedUser', data.user);
                    $rootScope.user = $window.localStorage.getItem('loggedUser');
                    $location.path('/History');
                } else {
                    alert(data.message);
                }
            },
            function () { //error
                alert('There was an error processing your request. Please try again.');
            });
    }

    /**
     * INIT
     */
    $scope.registerInfo = {
        emailAddress: '',
        password: '',
        confirmPassword: '',
    };

});