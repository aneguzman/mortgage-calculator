mortgageCalculatorApp.factory('registerService', function ($q, $http) {
    return {
        register: function (data) {
            var deferred = $q.defer();
            $http({
                    method: "POST",
                    url: "/Account/Register",
                    data: {
                        email: data.emailAddress,
                        password: data.password,
                        confirmPassword: data.confirmPassword,
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
