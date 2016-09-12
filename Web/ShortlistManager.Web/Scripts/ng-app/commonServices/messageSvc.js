angular
    .module('commonServices')
    .factory('messageSvc', ['$rootScope', function ($rootScope) {
        
    function MessageSvc() {

        /*
            This service could be injected into controllers as necessary, with the
            message array in this service rather than on rootScope, but that means
            that each controller has to implement some logic to tell it how to handle
            the messages.  There's also the issue where a message is only relavent to
            a certain controller which this service would have to be extended to handle
            so we'll keep it simple for the purposes of this example.
        */
        $rootScope.messages = [];

        this.add = function (prefix, content, style) {
            $rootScope.messages.push({ 'prefix': prefix, 'content': content, 'style' : style });
        };

        this.addError = function(content) {
            this.add('Error', content, 'alert-danger');
        };

        this.addWarning = function(content) {
            this.add('Warning', content, 'alert-warning');
        };

        this.addInfo = function(content) {
            this.add('Info', content, 'alert-info');
        };

        this.addSuccess = function(content) {
            this.add('Success', content, 'alert-success');
        };

        this.clear = function() {
            $rootScope.messages = [];
        };

    }

    return new MessageSvc();
}]);