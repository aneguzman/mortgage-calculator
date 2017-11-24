/**
 * Middleware that intercept an Unauthorized Response and redirect to the Login page
 */
mortgageCalculatorApp.factory('authHttpResponseMiddleware', function ($q, $location) {
    return {
        response: function(response) {
            if (response.status === 401) {
                console.log("Response 401");
            }
            return response || $q.when(response);
        },
        responseError: function(rejection) {
            if (rejection.status === 401) {
                console.log("Response Error 401", rejection);
                $location.path('/Login').search('returnUrl', $location.path());
            }
            return $q.reject(rejection);
        }
    };
});
