mortgageCalculatorApp.controller('LoginController', function ($rootScope, $scope, $routeParams, $location, loginService, $window) {

    $scope.login = function() {
        loginService.login($scope.loginInfo)
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
    $scope.loginInfo = {
        emailAddress: '',
        password: '',
        rememberMe: false,
        returnUrl: $routeParams.returnUrl,
        loginFailure: false
    };

});