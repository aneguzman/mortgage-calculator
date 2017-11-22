mortgageCalculatorApp.controller('LoginController', function ($scope, $routeParams, $location, loginService) {

    $scope.login = function() {
        loginService.login($scope.loginInfo)
            .then(function (data) {
                if (data.success) {
                    if ($scope.loginInfo.returnUrl !== undefined) {
                        $location.path($scope.loginInfo.returnUrl);
                    } else {
                        $location.path('/History');
                    }
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
    $scope.loginInfo = {
        emailAddress: '',
        password: '',
        rememberMe: false,
        returnUrl: $routeParams.returnUrl,
        loginFailure: false
    };

});