using System.Web.Optimization;

namespace ShortlistManager.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // JS
            bundles.Add(new ScriptBundle("~/js/jquery-helpers").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/util/jquery-form-helpers.js"

            ));


            bundles.Add(new ScriptBundle("~/js/modernizr").Include(
                        "~/Scripts/modernizr-*"
            ));


            bundles.Add(new ScriptBundle("~/js/core").Include(
                    "~/Scripts/jquery-{version}.js",
                    "~/Scripts/bootstrap.js",
                    "~/Scripts/moment.js",
                    "~/Scripts/smalot-datetimepicker/bootstrap-datetimepicker.js",
                    "~/Scripts/util/bootstrap-datetimepicker-config.js",
                    "~/Scripts/respond.js"
            ));

            bundles.Add(new ScriptBundle("~/js/ng").Include(
                "~/Scripts/angular.js",
                "~/Scripts/angular-route.js",
                "~/Scripts/ng-app/commonServices/app.js",
                "~/Scripts/ng-app/commonServices/ajaxHandlerInterceptor.js",
                "~/Scripts/ng-app/commonServices/messageSvc.js",
                "~/Scripts/ng-app/commonServices/dateProcessorSvc.js",
                "~/Scripts/ng-app/commonServices/dateInputDirective.js"
             ));

            bundles.Add(new ScriptBundle("~/js/ng-ShortlistApp").Include(
                "~/Scripts/ng-app/shortlistApp/Models/player.js",
                "~/Scripts/ng-app/shortlistApp/dataSvc.js",
                "~/Scripts/ng-app/shortlistApp/listingCtrl.js",
                "~/Scripts/ng-app/shortlistApp/formCtrl.js",
                "~/Scripts/ng-app/shortlistApp/app.js"
             ));


            // CSS
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/angular-helpers.css",
                      "~/Content/smalot-datetimepicker/bootstrap-datetimepicker.css"
            ));


        }
    }
}
