angular
    .module('commonServices')
    .factory('dateProcessorSvc', function () {

        function DateProcessorSvc() {

            this.errorIndicator = 'Invalid Date';
            this.strictParseMode = true;

            this.ParseAspNetDateTime = function(dt) {

                var m = moment(dt);

                if (m._d.toString() === this.errorIndicator)
                    return null;

                return m._d;
            };


            this.ParseDateTime = function(str, format) {

                var m = moment(str, format, this.strictParseMode);

                if (m._d.toString() === this.errorIndicator) {
                    return null;
                }

                return m._d;
            };

            this.FormatDateTime = function(dt, format) {

                var result = '';

                if (angular.isDate(dt)) {
                    result = moment(dt).format(format);
                }

                return result;
            };

        }

        return new DateProcessorSvc();
    });