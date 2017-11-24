/**
 * Login service
 */
mortgageCalculatorApp.factory('loginService', function ($q, $http) {
    return {

        /**
         * Sends POST request to server for login
         * @param {Object} data - The user data
         * @returns {Promise<Object>} - Data object received from server.
         */
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
