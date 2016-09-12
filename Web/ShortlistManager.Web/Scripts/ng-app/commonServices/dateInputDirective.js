angular.module('commonServices')
    .directive('dateInput', ['dateProcessorSvc', function (dateProcessorSvc) {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attributes, ngModel) {

                ngModel.$parsers.push(parser);
                ngModel.$formatters.push(formatter);

                element.datetimepicker(cfgDatePicker);

                function parser(str) {
                    return dateProcessorSvc.ParseDateTime(str, attributes.dateInput);
                }

                function formatter(dt) {
                    return dateProcessorSvc.FormatDateTime(dt, attributes.dateInput);
                }
        }
    };
}]);