using System.Web.Optimization;

namespace RoverController
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Default

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when
            // you're ready for production, use the build tool at https://modernizr.com to pick only
            // the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            #endregion Default

            #region Porto Theme

            //******************************************************************************************************//
            //                                              CSS                                                     //
            //******************************************************************************************************//

            #region CSS

            bundles.Add(new StyleBundle("~/Porto/vendor/css/bootstrap", "https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css")
                        .Include("~/Content/Themes/Porto/vendor/bootstrap/css/bootstrap.min.css")
            );
            bundles.Add(new StyleBundle("~/Porto/vendor/css/animate", "https://cdnjs.cloudflare.com/ajax/libs/animate.css/3.5.2/animate.min.css")
                 .Include("~/Content/Themes/Porto/vendor/animate/animate.css")
            );
            bundles.Add(new StyleBundle("~/Porto/vendor/css/magnific-popup", "https://cdnjs.cloudflare.com/ajax/libs/magnific-popup.js/1.1.0/magnific-popup.min.css")
                .Include("~/Content/Themes/Porto/vendor/magnific-popup/magnific-popup.css")
            );
            bundles.Add(new StyleBundle("~/Porto/vendor/css/simple-line-icons", "https://cdnjs.cloudflare.com/ajax/libs/simple-line-icons/2.4.1/css/simple-line-icons.css")
                    .Include("~/Content/Themes/Porto/vendor/simple-line-icons/css/simple-line-icons.css", new CssRewriteUrlTransform())
            );
            bundles.Add(new StyleBundle("~/Porto/vendor/css/font-awesome", "https://use.fontawesome.com/releases/v5.8.1/css/all.css")
                .Include("~/Content/Themes/Porto/vendor/font-awesome/css/all.css", new CssRewriteUrlTransform())
            );
            bundles.Add(new StyleBundle("~/Porto/vendor/css/bootstrap-datepicker", "https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.7.1/css/bootstrap-datepicker3.min.css")
                .Include("~/Content/Themes/Porto/vendor/bootstrap-datepicker/css/bootstrap-datepicker3.css")
            );
            bundles.Add(new StyleBundle("~/Porto/vendor/css/bootstrap-datetimepicker", "https://cdnjs.cloudflare.com/ajax/libs/bootstrap-timepicker/0.5.2/css/bootstrap-timepicker.min.css")
                .Include("~/Content/Themes/Porto/vendor/bootstrap-timepicker/css/bootstrap-timepicker.min.css")
            );
            bundles.Add(new StyleBundle("~/Porto/vendor/bootstrap-multiselect", "https://cdnjs.cloudflare.com/ajax/libs/bootstrap-multiselect/0.9.13/css/bootstrap-multiselect.css")
                .Include("~/Content/vendor/bootstrap-multiselect/bootstrap-multiselect.css")
            );
            bundles.Add(new StyleBundle("~/Porto/vendor/morris", "//cdnjs.cloudflare.com/ajax/libs/morris.js/0.5.0/morris.css")
                .Include("~/Content/Themes/Porto/vendor/morris/morris.css")
            );
            bundles.Add(new StyleBundle("~/Porto/vendor/css/select2")
                .Include("~/Content/Themes/Porto/vendor/select2/css/select2.css")
            );
            bundles.Add(new StyleBundle("~/Porto/vendor/css/select2-bootstrap-theme")
                .Include("~/Content/Themes/Porto/vendor/select2-bootstrap-theme/select2-bootstrap.min.css")
            );
            bundles.Add(new StyleBundle("~/Porto/vendor/css/bootstrap-file-upload")
                .Include("~/Content/Themes/Porto/vendor/bootstrap-fileupload/bootstrap-fileupload.min.css")
            );

            //bundles.Add(new StyleBundle("~/Porto/vendor/css/simple-line-icons",
            //    "https://cdnjs.cloudflare.com/ajax/libs/simple-line-icons/2.4.1/css/simple-line-icons.css")
            //    .Include(
            //        "~/Content/Themes/Porto/vendor/simple-line-icons/css/simple-line-icons.css",
            //        new CssRewriteUrlTransform())
            //);

            //bundles.Add(new StyleBundle("~/Porto/vendor/css/font-awesome",
            //    "https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css")
            //    .Include(
            //        "~/Content/Themes/Porto/vendor/font-awesome/css/font-awesome.css",
            //        new CssRewriteUrlTransform())
            //);

            bundles.Add(new StyleBundle("~/Porto/theme/css").Include("~/Content/Themes/Porto/css/theme.css"));
            //bundles.Add(new StyleBundle("~/Porto/theme/css/skin").Include("~/Content/Themes/Porto/css/skins/superhero.css"));
            bundles.Add(new StyleBundle("~/Porto/theme/css/skin").Include("~/Content/Themes/Porto/css/skins/default.css"));
            bundles.Add(new StyleBundle("~/Porto/theme/css/custom").Include(
                "~/Content/Themes/Porto/css/custom.css",
                "~/Content/Themes/Porto/css/custom.mobile.css",
                "~/Content/Themes/Porto/css/custom.desktop.css"
                ));
            bundles.Add(new StyleBundle("~/Porto/theme/css/skin/custom").Include("~/Content/Themes/Porto/css/skins/superhero.custom.css"));

            bundles.Add(new StyleBundle("~/Porto/datatables/css", "https://cdn.datatables.net/1.10.16/css/dataTables.bootstrap4.min.css")
                .Include("~/Content/Themes/Porto/vendor/datatables/media/css/dataTables.bootstrap4.css")
            );

            bundles.Add(new StyleBundle("~/Porto/datatables/css/buttons", "https://cdn.datatables.net/buttons/1.5.6/css/buttons.dataTables.min.css")
                .Include("~/Content/Themes/Porto/vendor/datatables/extras/TableTools/Buttons-1.4.2/css/buttons.dataTables.min")
            );

            bundles.Add(new StyleBundle("~/Porto/vendor/css/pnotify",
               "https://cdnjs.cloudflare.com/ajax/libs/pnotify/3.2.1/pnotify.css")
               .Include("~/Content/Themes/Porto/vendor/pnotify/pnotify.custom.css")
           );

            #endregion CSS

            //******************************************************************************************************//
            //                                              JS                                                      //
            //******************************************************************************************************//

            #region JS

            bundles.Add(new ScriptBundle("~/Porto/js/modernizr", "https://cdnjs.cloudflare.com/ajax/libs/modernizr/2.8.3/modernizr.min.js")
                .Include("~/Content/Themes/Porto/vendor/modernizr/modernizr.js"));

            bundles.Add(new ScriptBundle("~/Porto/vendor/js/jquery", "https://code.jquery.com/jquery-3.3.1.min.js")
                .Include("~/Content/Themes/Porto/vendor/jquery/jquery-3.3.1.min.js")
            );
            bundles.Add(new ScriptBundle("~/Porto/vendor/js/jquery-validate", "https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.17.0/jquery.validate.min.js")
                .Include("~/Content/Themes/Porto/vendor/jquery-validation/jquery.validate.min.js")
            );
            bundles.Add(new ScriptBundle("~/Porto/vendor/js/jquery-browser-mobile")
                .Include("~/Content/Themes/Porto/vendor/jquery-browser-mobile/jquery.browser.mobile.js")
            );
            bundles.Add(new ScriptBundle("~/Porto/vendor/js/popper", "https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js")
                .Include("~/Content/Themes/Porto/vendor/popper/umd/popper.min.js")
            );
            bundles.Add(new ScriptBundle("~/Porto/vendor/js/bootstrap", "https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js")
                .Include("~/Content/Themes/Porto/vendor/bootstrap/js/bootstrap.min.js")
            );
            bundles.Add(new ScriptBundle("~/Porto/vendor/js/bootstrap-datepicker", "https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.7.1/js/bootstrap-datepicker.min.js")
                .Include("~/Content/Themes/Porto/vendor/bootstrap-datepicker/js/bootstrap-datepicker.js")
            );
            bundles.Add(new ScriptBundle("~/Porto/vendor/js/bootstrap-timepicker", "https://cdnjs.cloudflare.com/ajax/libs/bootstrap-timepicker/0.5.2/js/bootstrap-timepicker.min.js")
                .Include("~/Content/Themes/Porto/vendor/bootstrap-timepicker/bootstrap-timepicker.js")
            );
            bundles.Add(new ScriptBundle("~/Porto/vendor/js/common")
                .Include("~/Content/Themes/Porto/vendor/common/common.js")
            );
            bundles.Add(new ScriptBundle("~/Porto/vendor/js/nanoscroller", "https://cdnjs.cloudflare.com/ajax/libs/jquery.nanoscroller/0.8.7/javascripts/jquery.nanoscroller.min.js")
                .Include("~/Content/Themes/Porto/vendor/nanoscroller/nanoscroller.js")
            );
            bundles.Add(new ScriptBundle("~/Porto/vendor/js/magnific-popup", "https://cdn.jsdelivr.net/npm/magnific-popup@1.1.0/dist/jquery.magnific-popup.min.js")
                .Include("~/Content/Themes/Porto/vendor/magnific-popup/jquery.magnific-popup.js")
            );
            bundles.Add(new ScriptBundle("~/Porto/vendor/js/jquery-placeholder", "https://cdnjs.cloudflare.com/ajax/libs/jquery-placeholder/2.3.1/jquery.placeholder.min.js")
                .Include("~/Content/Themes/Porto/vendor/jquery-placeholder/jquery-placeholder.js")
            );

            bundles.Add(new ScriptBundle("~/Porto/vendor/bootbox/js", "https://cdnjs.cloudflare.com/ajax/libs/bootbox.js/4.4.0/bootbox.js")
                .Include("~/Content/vendor/bootbox/bootbox.min.js")
            );

            bundles.Add(new ScriptBundle("~/Porto/vendor/js/pace", "https://cdnjs.cloudflare.com/ajax/libs/pace/1.0.2/pace.min.js")
                .Include("~/Content/vendor/pace/pace.min.js")
            );

            bundles.Add(new ScriptBundle("~/Porto/vendor/pnotify/js", "https://cdnjs.cloudflare.com/ajax/libs/pnotify/3.2.1/pnotify.js")
               .Include("~/Content/Themes/Porto/vendor/pnotify/pnotify.custom.js")
            );

            bundles.Add(new ScriptBundle("~/Porto/vendor/moment/js", "https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.19.2/moment.min.js")
               .Include("~/Content/Themes/Porto/vendor/moment/moment.js")
            );

            bundles.Add(new ScriptBundle("~/Porto/vendor/js/bootstrap-multiselect", "https://cdnjs.cloudflare.com/ajax/libs/bootstrap-multiselect/0.9.13/js/bootstrap-multiselect.min.js")
               .Include("~/Content/Themes/Porto/vendor/bootstrap-multiselect/bootstrap-multiselect.js")
            );

            bundles.Add(new ScriptBundle("~/Porto/vendor/js/select2", "https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.5/js/select2.full.min.js")
               .Include("~/Content/Themes/Porto/vendor/select2/js/select2.full.min.js")
            );

            bundles.Add(new ScriptBundle("~/Porto/vendor/js/font-awesome", "https://use.fontawesome.com/releases/v5.0.6/js/all.js")
                .Include("~/Content/Themes/Porto/vendor/font-awesome/js/all.js")
            );

            bundles.Add(new ScriptBundle("~/Porto/vendor/js/morris", "//cdnjs.cloudflare.com/ajax/libs/morris.js/0.5.0/morris.min.js")
                .Include("~/Content/Themes/Porto/vendor/morris/morris.js")
            );

            bundles.Add(new ScriptBundle("~/Porto/vendor/js/raphael", "//cdnjs.cloudflare.com/ajax/libs/raphael/2.1.0/raphael-min.js")
                .Include("~/Content/Themes/Porto/vendor/raphael/raphael.js")
            );

            bundles.Add(new ScriptBundle("~/Porto/app/js").Include(
                //Theme Base, Components and Settings
                "~/Content/Themes/Porto/js/theme.js",

                //Theme Custom
                "~/Content/Themes/Porto/js/custom.js",

                //Theme Initialization Files
                "~/Content/Themes/Porto/js/theme.init.js",

                //Overlay
                "~/Content/Themes/Porto/js/spin.min.js"
            ));

            bundles.Add(new ScriptBundle("~/Porto/datatables/js/jquery", "https://cdn.datatables.net/1.10.16/js/jquery.dataTables.min.js")
                .Include("~/Content/Themes/Porto/vendor/datatables/media/js/jquery.dataTables.min.js")
            );
            bundles.Add(new ScriptBundle("~/Porto/datatables/js/bootstrap", "https://cdn.datatables.net/1.10.16/js/dataTables.bootstrap4.min.js")
                .Include("~/Content/Themes/Porto/vendor/datatables/media/js/dataTables.bootstrap4.min.js")
            );
            bundles.Add(new ScriptBundle("~/Porto/datatables/js/tools/dataTables.buttons", "https://cdn.datatables.net/buttons/1.4.2/js/dataTables.buttons.min.js")
                .Include("~/Content/Themes/Porto/vendor/datatables/extras/TableTools/Buttons-1.4.2/js/dataTables.buttons.min.js")
            );
            bundles.Add(new ScriptBundle("~/Porto/datatables/js/tools/buttons.bootstrap4", "https://cdn.datatables.net/buttons/1.4.2/js/buttons.bootstrap4.min.js")
                .Include("~/Content/Themes/Porto/vendor/datatables/extras/TableTools/Buttons-1.4.2/js/buttons.bootstrap4.min.js")
            );
            bundles.Add(new ScriptBundle("~/Porto/datatables/js/tools/buttons.html5", "https://cdn.datatables.net/buttons/1.4.2/js/buttons.html5.min.js")
                .Include("~/Content/Themes/Porto/vendor/datatables/extras/TableTools/Buttons-1.4.2/js/buttons.html5.min.js")
            );
            bundles.Add(new ScriptBundle("~/Porto/datatables/js/tools/buttons.print", "https://cdn.datatables.net/buttons/1.4.2/js/buttons.print.min.js")
                .Include("~/Content/Themes/Porto/vendor/datatables/extras/TableTools/Buttons-1.4.2/js/buttons.print.min.js")
            );
            bundles.Add(new ScriptBundle("~/Porto/datatables/js/tools/jszip", "https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.5/jszip.min.js")
                .Include("~/Content/Themes/Porto/vendor/datatables/extras/TableTools/JSZip-2.5.0//jszip.min.js")
            );
            bundles.Add(new ScriptBundle("~/Porto/datatables/js/tools/pdfmake", "https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.34/pdfmake.min.js")
                .Include("~/Content/Themes/Porto/vendor/datatables/extras/TableTools/pdfmake-0.1.32/pdfmake.min.js")
            );
            bundles.Add(new ScriptBundle("~/Porto/datatables/js/tools/pdfmake.fonts", "https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.34/vfs_fonts.js")
                .Include("~/Content/Themes/Porto/vendor/datatables/extras/TableTools/pdfmake-0.1.32/vfs_fonts.js")
            );
            bundles.Add(new ScriptBundle("~/vendor/bootstrap/typeahead", "https://cdnjs.cloudflare.com/ajax/libs/bootstrap-3-typeahead/4.0.2/bootstrap3-typeahead.min.js")
                .Include("~/Content/vendor/bootstrap/typeahead/bootstrap3-typeahead.min.js")
            );
            bundles.Add(new ScriptBundle("~/Porto/vendor/js/bootstrap-fileupload")
                .Include("~/Content/Themes/Porto/vendor/bootstrap-fileupload/bootstrap-fileupload.min.js")
            );
            bundles.Add(new ScriptBundle("~/Scripts/linqjs", "https://cdnjs.cloudflare.com/ajax/libs/linq.js/2.2.0.2/linq.min.js")
                .Include("~/Scripts/linq.min.js")
            );
            bundles.Add(new ScriptBundle("~/Scripts/date.format")
                .Include("~/Scripts/date.format.js")
            );

            #endregion JS

            #endregion Porto Theme

            bundles.UseCdn = true;
            BundleTable.EnableOptimizations = true;
        }
    }
}