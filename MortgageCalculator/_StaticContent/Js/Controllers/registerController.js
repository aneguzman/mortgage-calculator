mortgageCalculatorApp.controller('RegisterController', function ($scope, $location, registerService) {

    $scope.register = function () {
        registerService.register($scope.registerInfo)
            .then(function (data) {
                if (data.success) {
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