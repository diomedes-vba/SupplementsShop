using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SupplementsShop.Web.ViewTagHelpers;

[HtmlTargetElement("button")]
public class MyDisabledButton : TagHelper
{
    
    [HtmlAttributeName("asp-is-disabled")]
    public bool IsDisabled { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (IsDisabled)
        {
            var d = new TagHelperAttribute("disabled", "disabled");
            output.Attributes.Add(d);
        }
        base.Process(context, output);
    }
}