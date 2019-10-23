using System.Web.Mvc;
using Umbraco.Core.Composing;
using Umbraco.Core.Logging;
using Umbraco.Web;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace Dragonfly.Umbraco8Theming
{
    using System.Web.Configuration;

    /// <summary>
    /// Find the theme setting and retrieve the appropriate view
    /// </summary>
    public class DefaultThemeController : RenderMvcController
    {
        private readonly ILogger _logger;

        public DefaultThemeController(ILogger logger)
        {
            _logger = logger;
        }

        // GET: Default
        public ActionResult Index(ContentModel model)
        {
            var themeProp = WebConfigurationManager.AppSettings["Dragonfly.ThemePropertyAlias"];

            if (!string.IsNullOrEmpty(themeProp))
            {
                var currentTemplateName = model.Content.GetTemplateAlias();
                var siteTheme = model.Content.AncestorOrSelf(1).Value<string>(themeProp);
                if (siteTheme == "")
                {
                    _logger.Warn<DefaultThemeController>($"Node '{model.Content.AncestorOrSelf(1).Name}' does not have a value for Theme picker property '{themeProp}'.");
                }
                var templatePath = ThemeHelper.GetFinalThemePath(siteTheme, ThemeHelper.PathType.View, currentTemplateName);
                return View(templatePath, model);
            }
            else
            {
                _logger.Warn<DefaultThemeController>($"Web.config AppSetting 'Dragonfly.ThemePropertyAlias' is not set.");
                return base.CurrentTemplate(model);
            }
        }
    }
}
