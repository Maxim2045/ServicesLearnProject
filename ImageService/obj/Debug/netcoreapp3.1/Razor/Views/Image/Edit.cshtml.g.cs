#pragma checksum "D:\Загрузки\Apps\Services\ImageService\Views\Image\Edit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "65169bbd91297a99c526f619e92280f081f2829f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Image_Edit), @"mvc.1.0.view", @"/Views/Image/Edit.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"65169bbd91297a99c526f619e92280f081f2829f", @"/Views/Image/Edit.cshtml")]
    public class Views_Image_Edit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ImageService.Models.Image>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Загрузки\Apps\Services\ImageService\Views\Image\Edit.cshtml"
  
    ViewBag.Title = "Редактирование пользователя";

#line default
#line hidden
#nullable disable
            WriteLiteral("<form asp-action=\"edit\" asp-controller=\"image\"");
            BeginWriteAttribute("asp-route-id", " asp-route-id=\"", 139, "\"", 163, 1);
#nullable restore
#line 5 "D:\Загрузки\Apps\Services\ImageService\Views\Image\Edit.cshtml"
WriteAttributeValue("", 154, Model.Id, 154, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n    <div class=\"form-group\">\r\n        <label asp-for=\"Id\" class=\"control-label\">Номер изображения</label>\r\n        <input type=\"text\"");
            BeginWriteAttribute("asp-for", " asp-for=\"", 300, "\"", 319, 1);
#nullable restore
#line 8 "D:\Загрузки\Apps\Services\ImageService\Views\Image\Edit.cshtml"
WriteAttributeValue("", 310, Model.Id, 310, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"form-control\" />\r\n    </div>\r\n    <div class=\"form-group\">\r\n        <label asp-for=\"IdProduct\" class=\"control-label\">Номер продукта</label>\r\n        <input type=\"text\"");
            BeginWriteAttribute("asp-for", " asp-for=\"", 495, "\"", 521, 1);
#nullable restore
#line 12 "D:\Загрузки\Apps\Services\ImageService\Views\Image\Edit.cshtml"
WriteAttributeValue("", 505, Model.ProductId, 505, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"form-control\" />\r\n    </div>\r\n    <div class=\"form-group\">\r\n        <label asp-for=\"Url\" class=\"control-label\">Адрес изображения</label>\r\n        <input type=\"text\"");
            BeginWriteAttribute("asp-for", " asp-for=\"", 694, "\"", 714, 1);
#nullable restore
#line 16 "D:\Загрузки\Apps\Services\ImageService\Views\Image\Edit.cshtml"
WriteAttributeValue("", 704, Model.Url, 704, 10, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"form-control\" />\r\n    </div>\r\n    <div class=\"form-group\">\r\n        <input type=\"submit\" value=\"Сохранить\" class=\"btn btn-default\" />\r\n    </div>\r\n</form>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ImageService.Models.Image> Html { get; private set; }
    }
}
#pragma warning restore 1591