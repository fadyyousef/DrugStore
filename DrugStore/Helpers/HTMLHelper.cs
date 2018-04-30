using System.Web.Mvc;

public static class ExtensionMethods
{
    public static MvcHtmlString DialogFormLink(this HtmlHelper htmlHelper,
        string linkText, string dialogContentUrl,
        string dialogId, string dialogTitle, string updateTargetId, string updateUrl)
    {
        TagBuilder builder = new TagBuilder("a");
        if (!string.IsNullOrEmpty(dialogId))
        {
            builder.Attributes.Add("href", dialogContentUrl + "?id=" + dialogId);
        }
        else
        {
            builder.Attributes.Add("href", dialogContentUrl);
        }
        builder.SetInnerText(linkText);
        builder.Attributes.Add("data-dialog-title", dialogTitle);
        builder.Attributes.Add("data-update-target-id", updateTargetId);
        builder.Attributes.Add("data-update-url", updateUrl);

        // Add a css class named dialogLink that will be
        // used to identify the anchor tag and to wire up
        // the jQuery functions
        builder.AddCssClass("dialogLink");
        builder.AddCssClass("btn btn-danger");

        return new MvcHtmlString(builder.ToString());
    }
}
