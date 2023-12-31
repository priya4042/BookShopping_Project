#pragma checksum "E:\Asp.Net Core MVC\Blogger_Project\Blogger_Project\Areas\Admins\Views\Views\Home\Post.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ae1752d0332251614bc352164d33f19fb47e6d9d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admins_Views_Views_Home_Post), @"mvc.1.0.view", @"/Areas/Admins/Views/Views/Home/Post.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "E:\Asp.Net Core MVC\Blogger_Project\Blogger_Project\Areas\Admins\Views\Views\_ViewImports.cshtml"
using Blogger_Project;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\Asp.Net Core MVC\Blogger_Project\Blogger_Project\Areas\Admins\Views\Views\_ViewImports.cshtml"
using Blogger_Project.Models.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ae1752d0332251614bc352164d33f19fb47e6d9d", @"/Areas/Admins/Views/Views/Home/Post.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d103b2541f62d28eb804a6ba86f948b28fb44e3b", @"/Areas/Admins/Views/Views/_ViewImports.cshtml")]
    public class Areas_Admins_Views_Views_Home_Post : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Blog_Models.Post>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Login", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Auth", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 4 "E:\Asp.Net Core MVC\Blogger_Project\Blogger_Project\Areas\Admins\Views\Views\Home\Post.cshtml"
  
    ViewBag.Title = Model.Title;
    ViewBag.Description = Model.Description;
    ViewBag.Keywords = $"{Model.Tags?.Replace(",", " ")} {Model.Category}";
    var base_path = Context.Request.PathBase;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"container\">\r\n    <div class=\"post no-shadow\">\r\n");
#nullable restore
#line 20 "E:\Asp.Net Core MVC\Blogger_Project\Blogger_Project\Areas\Admins\Views\Views\Home\Post.cshtml"
         if (!String.IsNullOrEmpty(Model.Images))
        {
            var image_path = $"{base_path}/Image/{Model.Images}";

#line default
#line hidden
#nullable disable
            WriteLiteral("            <img");
            BeginWriteAttribute("src", " src=\"", 654, "\"", 671, 1);
#nullable restore
#line 23 "E:\Asp.Net Core MVC\Blogger_Project\Blogger_Project\Areas\Admins\Views\Views\Home\Post.cshtml"
WriteAttributeValue("", 660, image_path, 660, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n            <span class=\"title\">");
#nullable restore
#line 24 "E:\Asp.Net Core MVC\Blogger_Project\Blogger_Project\Areas\Admins\Views\Views\Home\Post.cshtml"
                           Write(Model.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n");
#nullable restore
#line 25 "E:\Asp.Net Core MVC\Blogger_Project\Blogger_Project\Areas\Admins\Views\Views\Home\Post.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n    <div class=\"post-body\">\r\n        ");
#nullable restore
#line 28 "E:\Asp.Net Core MVC\Blogger_Project\Blogger_Project\Areas\Admins\Views\Views\Home\Post.cshtml"
   Write(Html.Raw(Model.Body));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n");
#nullable restore
#line 30 "E:\Asp.Net Core MVC\Blogger_Project\Blogger_Project\Areas\Admins\Views\Views\Home\Post.cshtml"
     if (User.Identity.IsAuthenticated)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"comment-section\">\r\n");
#nullable restore
#line 33 "E:\Asp.Net Core MVC\Blogger_Project\Blogger_Project\Areas\Admins\Views\Views\Home\Post.cshtml"
              
                await Html.RenderPartialAsync("_MainComment", new { PostId = Model.Id, MainCommentId = 0 });
            

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 37 "E:\Asp.Net Core MVC\Blogger_Project\Blogger_Project\Areas\Admins\Views\Views\Home\Post.cshtml"
             foreach (var c in Model.MainComments)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <p>\r\n                    ");
#nullable restore
#line 40 "E:\Asp.Net Core MVC\Blogger_Project\Blogger_Project\Areas\Admins\Views\Views\Home\Post.cshtml"
               Write(c.Message);

#line default
#line hidden
#nullable disable
            WriteLiteral(" --- ");
#nullable restore
#line 40 "E:\Asp.Net Core MVC\Blogger_Project\Blogger_Project\Areas\Admins\Views\Views\Home\Post.cshtml"
                              Write(c.Created);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </p>\r\n                <div style=\"margin-left: 20px;\">\r\n                    <h4>Sub Comments</h4>\r\n");
#nullable restore
#line 44 "E:\Asp.Net Core MVC\Blogger_Project\Blogger_Project\Areas\Admins\Views\Views\Home\Post.cshtml"
                      
                        await Html.RenderPartialAsync("_MainComment", new  { PostId = Model.Id, MainCommentId = c.Id });
                    

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 48 "E:\Asp.Net Core MVC\Blogger_Project\Blogger_Project\Areas\Admins\Views\Views\Home\Post.cshtml"
                     foreach (var sc in c.SubComments)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <p>\r\n                            ");
#nullable restore
#line 51 "E:\Asp.Net Core MVC\Blogger_Project\Blogger_Project\Areas\Admins\Views\Views\Home\Post.cshtml"
                       Write(sc.Message);

#line default
#line hidden
#nullable disable
            WriteLiteral(" --- ");
#nullable restore
#line 51 "E:\Asp.Net Core MVC\Blogger_Project\Blogger_Project\Areas\Admins\Views\Views\Home\Post.cshtml"
                                       Write(sc.Created);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </p>\r\n");
#nullable restore
#line 53 "E:\Asp.Net Core MVC\Blogger_Project\Blogger_Project\Areas\Admins\Views\Views\Home\Post.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </div>\r\n");
#nullable restore
#line 55 "E:\Asp.Net Core MVC\Blogger_Project\Blogger_Project\Areas\Admins\Views\Views\Home\Post.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n");
#nullable restore
#line 57 "E:\Asp.Net Core MVC\Blogger_Project\Blogger_Project\Areas\Admins\Views\Views\Home\Post.cshtml"
    }
    else
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div>\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ae1752d0332251614bc352164d33f19fb47e6d9d9499", async() => {
                WriteLiteral("Sign In");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(" to comment on this awesome post!\r\n        </div>\r\n");
#nullable restore
#line 63 "E:\Asp.Net Core MVC\Blogger_Project\Blogger_Project\Areas\Admins\Views\Views\Home\Post.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Blog_Models.Post> Html { get; private set; }
    }
}
#pragma warning restore 1591
