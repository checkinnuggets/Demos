angular
    .module('commonServices')
    .factory('ajaxHandlerInterceptor', ['$q', 'messageSvc', function ($q, messageSvc) {




        // todo (GG): can these be functions?

    var AjaxHandlerInterceptor = {

        'request': function (config) {
            messageSvc.clear();
            return config;
        },

        'responseError': function (rejection) {

            var errorMessage = 'AJAX request timed out.';

            if (rejection.status !== 0) {
                errorMessage = rejection.data.ExceptionMessage;
            }
            
            messageSvc.addError(errorMessage);
            
            return $q.reject(rejection);
        }
    };
    return AjaxHandlerInterceptor;
}]);
