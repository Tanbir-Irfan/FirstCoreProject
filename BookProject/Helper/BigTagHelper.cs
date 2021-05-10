using System;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BookProject.Helper
{
    //[HtmlTargetElement("big", Attributes = "big")]
    [HtmlTargetElement("big")]
    [HtmlTargetElement(Attributes = "big")]
    public class BigTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "h3";
            output.Attributes.RemoveAll("big");
            output.Attributes.SetAttribute("class", "h3");
            //output.PreContent.SetHtmlContent()
        }
    }
}
