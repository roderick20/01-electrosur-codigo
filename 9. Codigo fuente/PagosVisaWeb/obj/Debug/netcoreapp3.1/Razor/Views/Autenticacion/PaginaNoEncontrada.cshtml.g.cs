#pragma checksum "D:\GitHub\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Autenticacion\PaginaNoEncontrada.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "67ec6bd79a2706b3c4c9aaefca1ca27982393f5c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Autenticacion_PaginaNoEncontrada), @"mvc.1.0.view", @"/Views/Autenticacion/PaginaNoEncontrada.cshtml")]
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
#line 1 "D:\GitHub\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\_ViewImports.cshtml"
using PagosVisaWeb;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\GitHub\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\_ViewImports.cshtml"
using PagosVisaWeb.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"67ec6bd79a2706b3c4c9aaefca1ca27982393f5c", @"/Views/Autenticacion/PaginaNoEncontrada.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"99f2668289511a5d522bc63fb530245c524f2a77", @"/Views/_ViewImports.cshtml")]
    public class Views_Autenticacion_PaginaNoEncontrada : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "D:\GitHub\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Autenticacion\PaginaNoEncontrada.cshtml"
   ViewData["Title"] = "Página no encontrada";
                Layout = "~/Views/Shared/_Layout_login.cshtml"; 

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""account-pages mt-5 mb-5"">
    <div class=""container"">
        <div class=""row justify-content-center"">
            <div class=""col-lg-5"">
                <div class=""card"">
                    <div class=""card-body p-4"">
                        <div class=""text-center w-75 m-auto"">
");
#nullable restore
#line 11 "D:\GitHub\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Autenticacion\PaginaNoEncontrada.cshtml"
                             if (ViewBag.Mensaje != null)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <h4 class=\"text-dark-50 text-center mt-0 font-weight-bold\" style=\"margin-bottom: 20px;\">");
#nullable restore
#line 13 "D:\GitHub\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Autenticacion\PaginaNoEncontrada.cshtml"
                                                                                                           Write(ViewBag.Mensaje);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4> ");
#nullable restore
#line 13 "D:\GitHub\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Autenticacion\PaginaNoEncontrada.cshtml"
                                                                                                                                      }
                                    else
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <h4 class=\"text-dark-50 text-center mt-0 font-weight-bold\" style=\"margin-bottom: 20px;\">Página no encontrada</h4>");
#nullable restore
#line 16 "D:\GitHub\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Autenticacion\PaginaNoEncontrada.cshtml"
                                                                                                                                         }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                        </div>
                        <p class=""text-muted""><a href=""/"" class=""text-muted ml-1 btn btn-primary text-white btn-sm""><b>Inicio</b></a></p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
");
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
