mortgageCalculatorApp.factory('loginService', function ($q, $http) {
    return {
        login: function(data) {
            var deferred = $q.defer();
            $http({
                    method: "POST",
                    url: "/Account/Login",
                    data: {
                        email: data.emailAddress,
                        password: data.password,
                        rememberMe: data.rememberMe
                    },
                    cache: false
                })
                .success(function (data) {
                    return deferred.resolve(data);
                });
            return deferred.promise;
        }
    };
});
