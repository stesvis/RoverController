using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using RoverController.Lib;

namespace RoverController.Web.HtmlHelperExtensions
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString UserActionButton(
            this AjaxHelper ajaxHelper,
            string primaryKey,
            bool canDelete)
        {
            var html = "<div class=\"\">" +
                        "    <div class=\"btn-group flex-wrap show\">" +
                        "        <button type = \"button\"" +
                        "                class=\"mb-1 mt-1 mr-1 btn btn-quaternary dropdown-toggle\"" +
                        "                data-toggle=\"dropdown\"" +
                        "                aria-expanded=\"true\">" +
                        "            <i class=\"icons icon-menu\"></i> <span class=\"caret\"></span>" +
                        "        </button>" +
                        "        <div class=\"dropdown-menu\"" +
                        "             x-placement=\"bottom-start\"" +
                        "             style=\"position: absolute; transform: translate3d(-8px, 35px, 0px); top: 0px; left: 0px; will-change: transform;\">" +
                        "            <ul class=\"list-unstyled mb-2\">" +
                        "                <li>" +
                        "                    <a role = \"menuitem\" class=\"dropdown-item text-1 js-edit\"" +
                        $"                       primaryKey=\"{primaryKey}\">" +
                        "                        <i class=\"far fa-edit\"></i>" +
                        "                        Edit" +
                        "                    </a>" +
                        "                </li>";
            if (canDelete)
            {
                html +=
                    "                    <li class=\"divider\"></li>" +
                    "                    <li>" +
                    "                        < a role = \"menuitem\" class=\"dropdown-item text-1 text-danger js-delete\"" +
                    $"                           primaryKey=\"{primaryKey}\">" +
                    "                            <i class=\"far fa-trash-alt\"></i>" +
                    "                            Delete" +
                    "                        </a>" +
                    "                    </li>";
            }

            html +=
                                "            </ul>" +
                                "        </div>" +
                                "    </div>" +
                                "</div>";
            html = $"<div class=\"btn - group flex - wrap show\">         <button type=\"button\"                 class=\"mb - 1 mt - 1 mr - 1 btn btn-quaternary dropdown - toggle\"                 data-toggle=\"dropdown\"                 aria-expanded=\"true\">             <i class=\"icons icon-menu\"></i> <span class=\"caret\"></span>         </button>         <div class=\"dropdown - menu\"              x-placement=\"bottom - start\"              style=\"position: absolute; transform: translate3d(-8px, 35px, 0px); top: 0px; left: 0px; will - change: transform; \">             <ul class=\"list - unstyled mb - 2\">                 <li>                     <a role=\"menuitem\" class=\"dropdown - item text - 1 js - edit\"                        primaryKey=\"{primaryKey}\">                         <i class=\"far fa-edit\"></i>                         Edit                     </a>                 </li>             </ul>         </div>     </div>";

            return MvcHtmlString.Create(html);
        }

        /// <summary>
        /// Renders a Font-Awesome view link button
        /// </summary>
        /// <param name="ajaxHelper"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static MvcHtmlString FaViewLinkButton(
            this AjaxHelper ajaxHelper,
            string actionName, string controllerName,
            int id,
            string extraClasses = "")
        {
            return RawActionLink(
                ajaxHelper,
                "<span class=\"fa fa-eye\"></span>",
                actionName, controllerName,
                new { id = id },
                new AjaxOptions { UpdateTargetId = "content" },
                new { @class = $"btn btn-info {extraClasses}", primaryKey = id }
                );
        }

        /// <summary>
        /// Renders a Font-Awesome edit link button
        /// </summary>
        /// <param name="ajaxHelper"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static MvcHtmlString FaEditLinkButton(
            this AjaxHelper ajaxHelper,
            string actionName,
            string controllerName,
            object id,
            string extraClasses = "")
        {
            var onclick = "window.event.cancelBubble = true;";
            //if (!appService.UserCanModifyClient(appService.CurrentUserId, appService.CurrentClientId))
            //{
            //    onclick += "return false;";
            //}

            //return RawActionLink(
            //    ajaxHelper,
            //    "<span class=\"fa fa-pencil\"></span>",
            //    actionName, controllerName,
            //    new { id = id },
            //    new AjaxOptions { UpdateTargetId = "content" },
            //    new { @class = $"btn btn-warning {extraClasses}", primaryKey = id, onclick = onclick }
            //    );

            return RawActionLink(
                ajaxHelper,
                "<i class=\"fa fa-pencil\"></i>",
                actionName, controllerName,
                new { id = id },
                new AjaxOptions { UpdateTargetId = "content" },
                new { @class = $" {extraClasses}", primaryKey = id, onclick = onclick }
                );
        }

        /// <summary>
        /// Renders a Font-Awesome success link button
        /// </summary>
        /// <param name="ajaxHelper"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="id"></param>
        /// <param name="extraClasses"></param>
        /// <returns></returns>
        public static MvcHtmlString FaCheckmarkLinkButton(
            this AjaxHelper ajaxHelper,
            //string actionName,
            //string controllerName,
            int id
            //string extraClasses = ""
            )
        {
            //return RawActionLink(
            //    ajaxHelper,
            //    "<span class=\"fa fa-check-square-o\"></span>",
            //    actionName, controllerName,
            //    new { id = id },
            //    new AjaxOptions { UpdateTargetId = "content" },
            //    new { @class = $"btn btn-success {extraClasses}", primaryKey = id }
            //    );
            var buttonLink = $"<button primaryKey=\"{id}\" class=\"btn btn-success js-complete\" >" +
            "<span class=\"fa fa-check-square-o\"></span>" +
            "</button>";
            return MvcHtmlString.Create(buttonLink);
        }

        /// <summary>
        /// Renders a Font-Awesome undo link button
        /// </summary>
        /// <param name="ajaxHelper"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static MvcHtmlString FaUndoLinkButton(
           this AjaxHelper ajaxHelper,
           //string actionName,
           //string controllerName,
           int id
           //string extraClasses = ""
           )
        {
            //return RawActionLink(
            //    ajaxHelper,
            //    "<span class=\"fa fa-check-square-o\"></span>",
            //    actionName, controllerName,
            //    new { id = id },
            //    new AjaxOptions { UpdateTargetId = "content" },
            //    new { @class = $"btn btn-success {extraClasses}", primaryKey = id }
            //    );
            var buttonLink = $"<button primaryKey=\"{id}\" class=\"btn btn-default js-undo\" >" +
            "<span class=\"fa fa-undo\"></span>" +
            "</button>";
            return MvcHtmlString.Create(buttonLink);
        }

        /// <summary>
        /// Renders a Font-Awesome delete link button
        /// </summary>
        /// <param name="ajaxHelper"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static MvcHtmlString FaDeleteLinkButton(this AjaxHelper ajaxHelper, object id, string extraClasses = "")
        {
            //var buttonLink = $"<button primaryKey=\"{id}\" class=\"btn btn-danger js-delete  {extraClasses}\" >" +
            // "<span class=\"fa fa-trash\"></span>" +
            // "</button>";
            var buttonLink = $"<a href=\"#\" primaryKey=\"{id}\"" +
                                  $"class=\"text-danger js-delete {extraClasses}\"" +
                                  "onclick=\"window.event.cancelBubble = true;\">" +
                                  "<i class=\"fa fa-trash\"></i>" +
                              "</a>";
            return MvcHtmlString.Create(buttonLink);
        }

        private const string idsToReuseKey = "__htmlPrefixScopeExtensions_IdsToReuse_";

        public static IDisposable BeginCollectionItem(this HtmlHelper html, string collectionName)
        {
            var htmlFieldPrefix = html.ViewData.TemplateInfo.HtmlFieldPrefix;
            if (htmlFieldPrefix.Contains(collectionName))
            {
                collectionName = htmlFieldPrefix.Substring(0, htmlFieldPrefix.LastIndexOf(collectionName) + collectionName.Length);
            }

            var idsToReuse = GetIdsToReuse(html.ViewContext.HttpContext, collectionName);
            string itemIndex = idsToReuse.Count > 0 ? idsToReuse.Dequeue() : Guid.NewGuid().ToString();

            // autocomplete="off" is needed to work around a very annoying Chrome behaviour whereby
            // it reuses old values after the user clicks "Back", which causes the xyz.index and
            // xyz[...] values to get out of sync.
            html.ViewContext.Writer.WriteLine(string.Format("<input type=\"hidden\" name=\"{0}.index\" autocomplete=\"off\" value=\"{1}\" />", collectionName, html.Encode(itemIndex)));

            return BeginHtmlFieldPrefixScope(html, string.Format("{0}[{1}]", collectionName, itemIndex));
        }

        public static IDisposable BeginHtmlFieldPrefixScope(this HtmlHelper html, string htmlFieldPrefix)
        {
            return new HtmlFieldPrefixScope(html.ViewData.TemplateInfo, htmlFieldPrefix);
        }

        public static string DisplayShortNameFor<TModel, TValue>(this global::System.Web.Mvc.HtmlHelper<global::System.Collections.Generic.IEnumerable<TModel>> t, global::System.Linq.Expressions.Expression<global::System.Func<TModel, TValue>> exp)
        {
            CustomAttributeNamedArgument? DisplayName = null;
            var prop = exp.Body as MemberExpression;
            if (prop != null)
            {
                var DisplayAttrib = (from c in prop.Member.GetCustomAttributesData()
                                     where c.AttributeType == typeof(DisplayAttribute)
                                     select c).FirstOrDefault();
                if (DisplayAttrib != null)
                    DisplayName = DisplayAttrib.NamedArguments.Where(d => d.MemberName == "ShortName").FirstOrDefault();
            }
            return DisplayName.HasValue ? DisplayName.Value.TypedValue.Value.ToString() : "";
        }

        private static Queue<string> GetIdsToReuse(HttpContextBase httpContext, string collectionName)
        {
            // We need to use the same sequence of IDs following a server-side validation failure,
            // otherwise the framework won't render the validation error messages next to each item.
            string key = idsToReuseKey + collectionName;
            var queue = (Queue<string>)httpContext.Items[key];
            if (queue == null)
            {
                httpContext.Items[key] = queue = new Queue<string>();
                var previouslyUsedIds = httpContext.Request[collectionName + ".index"];
                if (!previouslyUsedIds.IsEmpty())
                    foreach (string previouslyUsedId in previouslyUsedIds.Split(','))
                        queue.Enqueue(previouslyUsedId);
            }
            return queue;
        }

        private class HtmlFieldPrefixScope : IDisposable
        {
            private readonly TemplateInfo templateInfo;
            private readonly string previousHtmlFieldPrefix;

            public HtmlFieldPrefixScope(TemplateInfo templateInfo, string htmlFieldPrefix)
            {
                this.templateInfo = templateInfo;

                previousHtmlFieldPrefix = templateInfo.HtmlFieldPrefix;
                templateInfo.HtmlFieldPrefix = htmlFieldPrefix;
            }

            public void Dispose()
            {
                templateInfo.HtmlFieldPrefix = previousHtmlFieldPrefix;
            }
        }

        private static MvcHtmlString RawActionLink(
            this AjaxHelper ajaxHelper,
            string linkText,
            string actionName,
            string controllerName,
            object routeValues,
            AjaxOptions ajaxOptions,
            object htmlAttributes
        )
        {
            var repID = Guid.NewGuid().ToString();
            var lnk = ajaxHelper.ActionLink(repID, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);
            return MvcHtmlString.Create(lnk.ToString().Replace(repID, linkText));
        }
    }
}