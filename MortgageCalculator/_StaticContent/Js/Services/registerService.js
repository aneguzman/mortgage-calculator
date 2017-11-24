/**
 * Register service
 */
mortgageCalculatorApp.factory('registerService', function ($q, $http) {
    return {

        /**
         * Send POST request to server to register an user.
         * @param {Object} data - The user data
         * @returns {Promise<Object>} - Data object received from the server.
         */
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
