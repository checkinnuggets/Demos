var shortlistApp = angular.module('shortlistApp', [ 'ngRoute', 'commonServices', 'shortlistApp.services'])
    .controller('ListingCtrl', ListingCtrl, 'dataService')
    .controller('FormCtrl', FormCtrl, 'dataService');


shortlistApp.config(['$httpProvider', '$routeProvider', function ($httpProvider, $routeProvider) {

    // Wire up Ajax handler...
    $httpProvider.interceptors.push('ajaxHandlerInterceptor');

    // Setup Routing...
    $routeProvider
            .when('/', {
                templateUrl: '/Pages/Spa/listing.html',
                controller: 'ListingCtrl'
            })
            .when('/form/:id?', {
                templateUrl: '/Pages/Spa/form.html',
                controller: 'FormCtrl'
            });


    // Perhaps there's a nicer way to solve this, but the .NET method
    // (System.Web.Mvc.AjaxRequestExtensions.IsAjaxRequest) checks specifically for
    // this header, so I'm solving this by adding this header so in the event that
    // any framework code calls IsAjaxRequest, it has the expected behaviour.
    // See: https://github.com/angular/angular.js/commit/3a75b1124d062f64093a90b26630938558909e8d
    $httpProvider.defaults.headers.common['X-Requested-With'] = 'XMLHttpRequest';
}]);