using System.Web.Mvc;
using Umbraco.Web;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace Dragonfly.Umbraco8Theming
{
    /// <summary>
    /// Find the theme setting and retrieve the appropriate view
    /// </summary>
    public class DefaultThemeController : RenderMvcController
    {
        // GET: Default
        public ActionResult Index(ContentModel model)
        {
            var currentTemplateName = model.Content.GetTemplateAlias();
            var siteTheme = model.Content.AncestorOrSelf(1).Value<string>("SiteTheme");
            var templatePath = ThemeHelper.GetFinalThemePath(siteTheme, ThemeHelper.PathType.View, currentTemplateName);

            return View(templatePath, model);
        }
    }
}
