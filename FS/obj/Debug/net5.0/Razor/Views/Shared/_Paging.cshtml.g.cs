#pragma checksum "D:\C#\FeedbackSystem\FS\Views\Shared\_Paging.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9e18727ca43221989d1c9511af5fe4bca0f14d9c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__Paging), @"mvc.1.0.view", @"/Views/Shared/_Paging.cshtml")]
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
#line 1 "D:\C#\FeedbackSystem\FS\Views\_ViewImports.cshtml"
using FS;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\C#\FeedbackSystem\FS\Views\_ViewImports.cshtml"
using FS.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9e18727ca43221989d1c9511af5fe4bca0f14d9c", @"/Views/Shared/_Paging.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4422a526dae085fbe4f28d3b760564a024f2043c", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__Paging : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\C#\FeedbackSystem\FS\Views\Shared\_Paging.cshtml"
  
    int currentPage = Model.currentPage;
    int countPages = Model.countPages;
    var generateUrl = Model.generateUrl;

    if(currentPage > countPages)
        currentPage = countPages;

    if(countPages <= 1)
        return;

    int? preview = null;
    int? next = null;

    if(currentPage > 1)
        preview = currentPage - 1;

    if(currentPage < countPages)
        next = currentPage + 1;

    // Các trang hiện thị trong điều hướng
    List<int> pagesRanges = new List<int>();

    int delta = 5;             // Số trang mở rộng về mỗi bên trang hiện tại
    int remain = delta * 2;      // Số trang hai bên trang hiện tại
    pagesRanges.Add(currentPage);
    // Các trang phát triển về hai bên trang hiện tại
    for(int i = 1; i <= delta; i++) {
        if(currentPage + i <= countPages) {
            pagesRanges.Add(currentPage + i);
            remain--;
        }

        if(currentPage - i >= 1) {
            pagesRanges.Insert(0, currentPage - i);
            remain--;
        }

    }
    // Xử lý thêm vào các trang cho đủ remain (xảy ra ở đầu mút của khoảng trang không đủ
    // trang chèn  vào)
    if(remain > 0) {
        if(pagesRanges[0] == 1) {
            for(int i = 1; i <= remain; i++) {
                if(pagesRanges.Last() + 1 <= countPages) {
                    pagesRanges.Add(pagesRanges.Last() + 1);
                }
            }
        } else {
            for(int i = 1; i <= remain; i++) {
                if(pagesRanges.First() - 1 > 1) {
                    pagesRanges.Insert(0, pagesRanges.First() - 1);
                }
            }
        }
    }


#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<ul class=\"pagination\">\r\n    <!-- Previous page link -->\r\n");
#nullable restore
#line 63 "D:\C#\FeedbackSystem\FS\Views\Shared\_Paging.cshtml"
     if(preview != null) {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <li class=\"page-item\">\r\n            <a class=\"page-link\"");
            BeginWriteAttribute("href", " href=\"", 1844, "\"", 1872, 1);
#nullable restore
#line 65 "D:\C#\FeedbackSystem\FS\Views\Shared\_Paging.cshtml"
WriteAttributeValue("", 1851, generateUrl(preview), 1851, 21, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Trang trước</a>\r\n        </li>\r\n");
#nullable restore
#line 67 "D:\C#\FeedbackSystem\FS\Views\Shared\_Paging.cshtml"
    } else {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <li class=\"page-item disabled\">\r\n            <a class=\"page-link\" href=\"#\" tabindex=\"-1\" aria-disabled=\"true\">Trang trước</a>\r\n        </li>\r\n");
#nullable restore
#line 71 "D:\C#\FeedbackSystem\FS\Views\Shared\_Paging.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    <!-- Numbered page links -->\r\n");
#nullable restore
#line 74 "D:\C#\FeedbackSystem\FS\Views\Shared\_Paging.cshtml"
     foreach(var pageitem in pagesRanges) {
        if(pageitem != currentPage) {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <li class=\"page-item\">\r\n                <a class=\"page-link\"");
            BeginWriteAttribute("href", " href=\"", 2269, "\"", 2298, 1);
#nullable restore
#line 77 "D:\C#\FeedbackSystem\FS\Views\Shared\_Paging.cshtml"
WriteAttributeValue("", 2276, generateUrl(pageitem), 2276, 22, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                    ");
#nullable restore
#line 78 "D:\C#\FeedbackSystem\FS\Views\Shared\_Paging.cshtml"
               Write(pageitem);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </a>\r\n            </li>\r\n");
#nullable restore
#line 81 "D:\C#\FeedbackSystem\FS\Views\Shared\_Paging.cshtml"
        } else {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <li class=\"page-item active\" aria-current=\"page\">\r\n                <a class=\"page-link\" href=\"#\">");
#nullable restore
#line 83 "D:\C#\FeedbackSystem\FS\Views\Shared\_Paging.cshtml"
                                         Write(pageitem);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <span class=\"sr-only\">(current)</span></a>\r\n            </li>\r\n");
#nullable restore
#line 85 "D:\C#\FeedbackSystem\FS\Views\Shared\_Paging.cshtml"
        }
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    <!-- Next page link -->\r\n");
#nullable restore
#line 89 "D:\C#\FeedbackSystem\FS\Views\Shared\_Paging.cshtml"
     if(next != null) {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <li class=\"page-item\">\r\n            <a class=\"page-link\"");
            BeginWriteAttribute("href", " href=\"", 2712, "\"", 2737, 1);
#nullable restore
#line 91 "D:\C#\FeedbackSystem\FS\Views\Shared\_Paging.cshtml"
WriteAttributeValue("", 2719, generateUrl(next), 2719, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Trang sau</a>\r\n        </li>\r\n");
#nullable restore
#line 93 "D:\C#\FeedbackSystem\FS\Views\Shared\_Paging.cshtml"
    } else {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <li class=\"page-item disabled\">\r\n            <a class=\"page-link\" href=\"#\" tabindex=\"-1\" aria-disabled=\"true\">Trang sau</a>\r\n        </li>\r\n");
#nullable restore
#line 97 "D:\C#\FeedbackSystem\FS\Views\Shared\_Paging.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</ul>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
