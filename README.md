# Dragonfly.Umbraco8Theming #

A Theming system for Umbraco version 8 created by [Heather Floyd](https://www.HeatherFloyd.com).

For a general explanation, see the article:

 [How to Create Multiple Unique Sites in One Installation Using Theming and the Umbraco Grid](https://24days.in/umbraco-cms/2016/unique-sites-using-theming/)

This package will install a few examples in the "Views" folder, along with a Themes folder which includes an unstyled "starter" theme.

Credit goes to [Shannon Deminick's *Articulate* package](https://github.com/Shazwazza/Articulate) for inspiration and initial code.

**NOTE** This project is being ported from the [v7 version](https://github.com/hfloyd/Dragonfly.UmbracoTheming) and is still "under construction".

## Installation ##
[![Nuget Downloads](https://buildstats.info/nuget/Dragonfly.Umbraco8Theming)](https://www.nuget.org/packages/Dragonfly.Umbraco8Theming/)

     PM>   Install-Package Dragonfly.Umbraco8Theming

## Resources ##
GitHub Repository: [https://github.com/hfloyd/Dragonfly.Umbraco8Theming](https://github.com/hfloyd/Dragonfly.Umbraco8Theming)

## Setup ##
On your root Document Type, use the included "Theme Picker" Property Type to add a property for the site's chosen theme. 

The Controller which runs for each page request needs to determine the Themed View file to render the page with, so if you already have custom controllers operating in your site, be sure to include something that will route the theme correctly. For example:

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
	            var siteTheme = model.Content.AncestorOrSelf(1).Value<string>("Theme");
	            var templatePath = ThemeHelper.GetFinalThemePath(siteTheme, ThemeHelper.PathType.View, currentTemplateName);
	
	            return View(templatePath, model);
	        }
	    }
	}

If you are not using any custom controllers, you can enable the 'DefaultThemeController' via the addition of two Web.config entries:

	<add key="Dragonfly.EnableDefaultThemeController" value="true" />
  	<add key="Dragonfly.ThemePropertyAlias" value="Theme" />

## Theme Configuration ##

*New in Version 0.2*

You can now store information about the Theme via an XML 'Theme.config' file. See the [example config](https://github.com/hfloyd/Dragonfly.Umbraco8Theming/blob/master/src/Dragonfly/Themes/%7ECopyForNewTheme/Theme.config) for format.

If the file doesn't exist in a Theme, a default one will be created.

This is most useful to store information about the GridRenderer used for the Theme - in the event that different Themes use different rendering files.

example for a View:
    
    var thisTheme = Model.Theme;
    var themeConfiguration = ThemeHelper.GetThemeConfig(thisTheme);
    
    @Html.GetTypedGridHtml(Model.GridProperty, themeConfiguration.GridRenderer)

This allows you to store the basic renderers all together outside of the Themes for reuse across Themes.