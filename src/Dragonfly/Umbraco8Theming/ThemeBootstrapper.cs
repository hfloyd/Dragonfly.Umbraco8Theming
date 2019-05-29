using Umbraco.Core.Composing;
using Umbraco.Web;

namespace Dragonfly.Umbraco8Theming
{
    /// <summary>
    /// Set the default RenderMVCcontroller to be the Theme Controller. This will allow us to set the theme templates
    /// </summary>
    public class ThemeBootstrapper : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.SetDefaultRenderMvcController(typeof(DefaultThemeController));
        }

    }
}
