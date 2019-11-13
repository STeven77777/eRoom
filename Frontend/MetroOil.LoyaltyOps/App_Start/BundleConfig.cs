using System.Web;
using System.Web.Optimization;

namespace MetroOil.LoyaltyOps
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Bunldes Css files
            bundles.Add(new StyleBundle("~/Content/Styles/css")
                      .Include("~/Content/fonts.opensans.css")
                      .Include("~/assets/global/plugins/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform())
                      .Include("~/assets/global/plugins/material-icons/css/material-icons.css", new CssRewriteUrlTransform())
                      .Include("~/assets/global/plugins/simple-line-icons/simple-line-icons.min.css", new CssRewriteUrlTransform())
                      .Include("~/assets/global/plugins/bootstrap/css/bootstrap.min.css")
                      .Include("~/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css")
                      .Include("~/assets/global/plugins/bootstrap-multiselect/css/bootstrap-multiselect.css")
                      .Include("~/assets/global/plugins/jquery-multi-select/css/multi-select.css")
                      .Include("~/assets/global/plugins/dropzone/dropzone.min.css")

                      .Include("~/assets/global/plugins/ngDialog-master/css/ngDialog.css")
                      .Include("~/assets/global/plugins/ngDialog-master/css/ngDialog-theme-default.css")
                      .Include("~/assets/global/plugins/ngDialog-master/css/ngDialog-theme-plain.css")
                      .Include("~/assets/global/plugins/ngDialog-master/css/ngDialog-custom-width.css")
                      );

            bundles.Add(new StyleBundle("~/Content/Styles/plugin").Include(
                      "~/assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css",
                      "~/assets/global/plugins/bootstrap-timepicker/css/bootstrap-timepicker.css",
                      "~/Content/angular-datepicker.css",
                      "~/assets/global/plugins/angularjs/plugins/ui-select/selectize.css",
                      "~/assets/global/plugins/morris/morris.css"
                      )
                      .Include(
                            "~/assets/global/plugins/datatables/datatables.min.css",
                            "~/assets/global/plugins/datatables/dataTables.colVis.css",
                            "~/assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css"
                            //"~/assets/global/plugins/angular-editor-master/stylesheets/simditor.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/Styles/global").Include(
                      "~/assets/global/css/components.css",
                      "~/assets/global/css/plugins.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/Styles/error").Include(
                     "~/assets/pages/css/error.css"
                     ));

            bundles.Add(new StyleBundle("~/Content/Styles/layout")
                      .Include("~/assets/layouts/layout/css/layout.min.css", new CssRewriteUrlTransform())
                      .Include("~/assets/layouts/layout/css/themes/cardtrend.css", new CssRewriteUrlTransform())
                      );

            bundles.Add(new StyleBundle("~/Content/Styles/custom").Include(
                      "~/assets/layouts/layout/css/custom.css",
                      "~/Content/select.css"
                      ));
            #endregion

            #region Bundles Css for page
            bundles.Add(new StyleBundle("~/Content/Styles/login").Include(
                      "~/Content/login-2.css"
                      ));
            #endregion

            #region Bundles Javascript files
            bundles.Add(new ScriptBundle("~/Content/Scripts/core").Include(
                        "~/assets/global/plugins/jquery.min.js",
                        "~/assets/global/plugins/bootstrap/js/bootstrap.min.js",
                        "~/assets/global/plugins/js.cookie.min.js",
                        "~/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                        "~/assets/global/plugins/jquery.blockui.min.js",
                        "~/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js",
                        "~/assets/global/plugins/bootstrap-multiselect/js/bootstrap-multiselect.js",
                        "~/Scripts/custom.layout.js",
                        "~/Scripts/underscore.js",
                        "~/assets/global/plugins/dropzone/dropzone.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/Content/Scripts/global").Include(
                        "~/assets/global/scripts/app.js",
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.js"
                        ));

            bundles.Add(new ScriptBundle("~/Content/Scripts/component").Include(
                        //"~/assets/pages/scripts/components-date-time-pickers.js",
                        "~/assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js",
                        "~/assets/global/plugins/bootstrap-timepicker/js/bootstrap-timepicker.js",
                        "~/assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.js",
                        "~/assets/pages/scripts/components-bootstrap-multiselect.js",

                        "~/assets/global/plugins/datatables/jquery.dataTables.min.js",
                        //"~/assets/global/plugins/datatables/datatables.min.js",
                        "~/assets/global/plugins/datatables/dataTables.colVis.js",

                        "~/assets/global/plugins/amcharts/amcharts/amcharts.js",
                        "~/assets/global/plugins/amcharts/amcharts/serial.js",
                        "~/assets/global/plugins/amcharts/amcharts/pie.js",
                        "~/assets/global/plugins/amcharts/amcharts/radar.js",
                        "~/assets/global/plugins/amcharts/amcharts/themes/light.js",
                        "~/assets/global/plugins/morris/morris.min.js",
                        "~/assets/global/plugins/morris/raphael-min.js",
                        "~/assets/global/plugins/jquery-multi-select/js/jquery.multi-select.js",
                        "~/Scripts/quicksearch.js",
                        "~/assets/pages/scripts/charts-google.js"
                        ));

            bundles.Add(new ScriptBundle("~/Content/Scripts/layout").Include(
                        "~/assets/layouts/layout/scripts/layout.js",
                        "~/assets/layouts/global/scripts/quick-sidebar.min.js",
                        "~/assets/layouts/global/scripts/quick-nav.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/Content/Scripts/angularjs").Include(
                        "~/assets/global/plugins/angularjs/angular.js",
                        "~/assets/global/plugins/angularjs/angular-route.min.js",
                        "~/assets/global/plugins/angularjs/angular-ui-router.js",
                        "~/assets/global/plugins/angularjs/angular-resource.js",
                        "~/assets/global/plugins/angularjs/angular-animate.min.js",
                        "~/assets/global/plugins/angularjs/angular-ui-bootstrap.js",
                        "~/assets/global/plugins/angularjs/angular-sanitize.min.js",
                        "~/assets/global/plugins/PNotify/pnotify.custom.js",
                        "~/Scripts/pageScripts.js",
                        "~/Scripts/select.js",
                        "~/ng/directives/wcOverlay.js",
                        "~/assets/global/plugins/ngDialog-master/js/ngDialog.js",
                        "~/assets/global/plugins/angularjs/ng-dropzone.js",
                        "~/assets/global/plugins/angularjs/plugins/ui-bootstrap-tpls.min.js",
                        "~/assets/global/plugins/moment.min.js",
                        "~/assets/global/plugins/moment-timezone-with-data.js",
                        "~/Scripts/datepicker/datePicker.js",
                        "~/Scripts/datepicker/datePickerUtils.js",
                        "~/Scripts/datepicker/dateRange.js",
                        "~/Scripts/datepicker/input.js",
                        "~/ng/dynamic-forms.js",
                        "~/ng/angulardynamicform.js"
                        //"~/assets/global/plugins/angular-editor-master/javascripts/simditor/simditor-all.js",
                        //"~/assets/global/plugins/angular-editor-master/javascripts/angular-editor.js"
                        , "~/assets/global/plugins/ui-tinymce-master/tinymce.min.js"
                        , "~/assets/global/plugins/ui-tinymce-master/ui-tinymce.js"
                        ));

            #endregion

            #region Bundle Javascript for page
            bundles.Add(new ScriptBundle("~/Content/Scripts/homepage").Include(
                          "~/ng/routes/homeConfig.js",
                          "~/ng/controllers/homeController.js"
                          ));

            bundles.Add(new ScriptBundle("~/Content/Scripts/login-page").Include(
                          "~/Scripts/login.page.js"
                          ));

            //bundles.Add(new ScriptBundle("~/Content/Scripts/customers").Include(
            //            "~/ng/routes/customersConfig.js",
            //            //"~/ng/services/customersService.js",
            //            "~/ng/controllers/customersController.js"
            //            ));

            bundles.Add(new ScriptBundle("~/Content/Scripts/cards").Include(
                        "~/ng/routes/cardsConfig.js",
                        //"~/ng/services/cardsService.js",
                        "~/ng/controllers/cardsController.js"
                        ));

            bundles.Add(new ScriptBundle("~/Content/Scripts/merchants").Include(
                        "~/ng/routes/merchantsConfig.js",
                        "~/ng/services/commonService.js",
                        "~/ng/controllers/merchantsController.js"
                        ));

            bundles.Add(new ScriptBundle("~/Content/Scripts/busnlocations").Include(
                        "~/ng/routes/busnlocationsConfig.js",
                        "~/ng/services/commonService.js",
                        "~/ng/controllers/busnlocationsController.js"
                        ));

            bundles.Add(new ScriptBundle("~/Content/Scripts/members").Include(
                        "~/ng/routes/membersConfig.js",
                        //"~/ng/services/membersService.js",
                        "~/ng/services/commonService.js",
                        "~/ng/controllers/membersController.js"
                        ));

            bundles.Add(new ScriptBundle("~/Content/Scripts/admin").Include(
                        "~/ng/routes/adminConfig.js",
                        //"~/ng/services/adminService.js",
                        "~/ng/services/commonService.js",
                        "~/ng/controllers/adminController.js"
                        ));

            bundles.Add(new ScriptBundle("~/Content/Scripts/reports").Include(
                        "~/ng/routes/reportsConfig.js",
                        "~/ng/controllers/reportsController.js",
                        "~/Scripts/dataTables.buttons.min.js",
                        "~/Scripts/buttons.flash.min.js",
                        "~/Scripts/jszip.min.js",
                        "~/Scripts/pdfmake.min.js",
                        "~/Scripts/vfs_fonts.js",
                        "~/Scripts/buttons.html5.min.js",
                        "~/Scripts/buttons.print.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/Content/Scripts/peoples").Include(
                   "~/ng/routes/peoplesConfig.js",
                   "~/ng/services/commonService.js",
                   "~/ng/controllers/peoplesController.js"
                   ));

            bundles.Add(new ScriptBundle("~/Content/Scripts/waterUsings").Include(
                    "~/ng/routes/waterUsingsConfig.js",
                    "~/ng/services/commonService.js",
                    "~/ng/controllers/waterUsingsController.js"
                    ));

            bundles.Add(new ScriptBundle("~/Content/Scripts/electricUsings").Include(
                   "~/ng/routes/electricUsingsConfig.js",
                   "~/ng/services/commonService.js",
                   "~/ng/controllers/electricUsingsController.js"
                   ));

            bundles.Add(new ScriptBundle("~/Content/Scripts/invoices").Include(
                   "~/ng/routes/invoicesConfig.js",
                   "~/ng/services/commonService.js",
                   "~/ng/controllers/invoicesController.js"
                   ));

            bundles.Add(new ScriptBundle("~/Content/Scripts/invoiceDetails").Include(
                   "~/ng/routes/invoiceDetailsConfig.js",
                   "~/ng/services/commonService.js",
                   "~/ng/controllers/invoiceDetailsController.js"
                   ));

            bundles.Add(new ScriptBundle("~/Content/Scripts/motorbikes").Include(
                   "~/ng/routes/motorbikesConfig.js",
                   "~/ng/services/commonService.js",
                   "~/ng/controllers/motorbikesController.js"
                   ));

            bundles.Add(new ScriptBundle("~/Content/Scripts/motorTypes").Include(
                   "~/ng/routes/motorTypesConfig.js",
                   "~/ng/services/commonService.js",
                   "~/ng/controllers/motorTypesController.js"
                   ));

            bundles.Add(new ScriptBundle("~/Content/Scripts/receipts").Include(
                   "~/ng/routes/receiptsConfig.js",
                   "~/ng/services/commonService.js",
                   "~/ng/controllers/receiptsController.js"
                   ));

            bundles.Add(new ScriptBundle("~/Content/Scripts/roomDetails").Include(
                   "~/ng/routes/roomDetailsConfig.js",
                   "~/ng/services/commonService.js",
                   "~/ng/controllers/roomDetailsController.js"
                   ));

            bundles.Add(new ScriptBundle("~/Content/Scripts/roomPeoples").Include(
                   "~/ng/routes/roomPeoplesConfig.js",
                   "~/ng/services/commonService.js",
                   "~/ng/controllers/roomPeoplesController.js"
                   ));

            bundles.Add(new ScriptBundle("~/Content/Scripts/roomStatus").Include(
                   "~/ng/routes/roomStatusConfig.js",
                   "~/ng/services/commonService.js",
                   "~/ng/controllers/roomStatusController.js"
                   ));

            bundles.Add(new ScriptBundle("~/Content/Scripts/roomTypes").Include(
                   "~/ng/routes/roomTypesConfig.js",
                   "~/ng/services/commonService.js",
                   "~/ng/controllers/roomTypesController.js"
                   ));

            bundles.Add(new ScriptBundle("~/Content/Scripts/services").Include(
                   "~/ng/routes/servicesConfig.js",
                   "~/ng/services/commonService.js",
                   "~/ng/controllers/servicesController.js"
                   ));

            bundles.Add(new ScriptBundle("~/Content/Scripts/serviceTypes").Include(
                   "~/ng/routes/serviceTypesConfig.js",
                   "~/ng/services/commonService.js",
                   "~/ng/controllers/serviceTypesController.js"
                   ));

           
            #endregion
        }
    }
}
