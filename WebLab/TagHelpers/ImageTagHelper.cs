using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;

namespace WebLab.TagHelpers;

[HtmlTargetElement("img", Attributes = "img-action,img-controller")]
public class ImageTagHelper : TagHelper
{
    private readonly LinkGenerator _linkGenerator;

    public ImageTagHelper(LinkGenerator linkGenerator)
    {
        _linkGenerator = linkGenerator;
    }

    [HtmlAttributeName("img-action")]
    public string ImgAction { get; set; } = string.Empty;

    [HtmlAttributeName("img-controller")]
    public string ImgController { get; set; } = string.Empty;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (string.IsNullOrEmpty(ImgAction) || string.IsNullOrEmpty(ImgController))
        {
            output.SuppressOutput();
            return;
        }

        var url = _linkGenerator.GetPathByAction(ImgAction, ImgController) ?? string.Empty;
        output.Attributes.SetAttribute("src", url);
    }
}