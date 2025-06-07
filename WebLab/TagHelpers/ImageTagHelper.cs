
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;

namespace WebLab.TagHelpers;

[HtmlTargetElement("img", Attributes = "img-action,img-controller")]
public class ImageTagHelper : TagHelper
{
    public string ImgAction { get; set; }
    public string ImgController { get; set; }

    private readonly LinkGenerator _linkGenerator;

    public ImageTagHelper(LinkGenerator linkGenerator)
    {
        _linkGenerator = linkGenerator;
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        string url = _linkGenerator.GetPathByAction(ImgAction, ImgController);
        output.Attributes.SetAttribute("src", url);
    }
}