#pragma checksum "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Pagos\BuscarSuministro.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "28cf8a81017f9ec684857229e5b2fff3db50bfcc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Pagos_BuscarSuministro), @"mvc.1.0.view", @"/Views/Pagos/BuscarSuministro.cshtml")]
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
#line 1 "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\_ViewImports.cshtml"
using PagosVisaWeb;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\_ViewImports.cshtml"
using PagosVisaWeb.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"28cf8a81017f9ec684857229e5b2fff3db50bfcc", @"/Views/Pagos/BuscarSuministro.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"99f2668289511a5d522bc63fb530245c524f2a77", @"/Views/_ViewImports.cshtml")]
    public class Views_Pagos_BuscarSuministro : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "BuscarSuministro", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("formLogin"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Pagos\BuscarSuministro.cshtml"
   int cont = 0; 

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div class=""row"" style=""margin-top:30px"">
    <div class=""col-12"">
        <div class=""card"">
            <div class=""card-body"">
                <h4 class=""header-title"">Recibos pendiente de pago</h4>
                <div class=""row"">
                    <div class=""col-lg-4"">
                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "28cf8a81017f9ec684857229e5b2fff3db50bfcc4942", async() => {
                WriteLiteral("\n                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "28cf8a81017f9ec684857229e5b2fff3db50bfcc5226", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper);
#nullable restore
#line 10 "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Pagos\BuscarSuministro.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary = global::Microsoft.AspNetCore.Mvc.Rendering.ValidationSummary.ModelOnly;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-summary", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\n                            <div class=\"form-group mb-3\">\n                                <div class=\"input-group\">\n");
#nullable restore
#line 13 "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Pagos\BuscarSuministro.cshtml"
                                       var codigo_cliente = (ViewBag.codigo_client != null) ? ViewBag.codigo_client : ""; 

#line default
#line hidden
#nullable disable
                WriteLiteral("                                    <input type=\"text\" class=\"form-control\" placeholder=\"Número de contrato\"\n                                           aria-label=\"Código del cliente\" name=\"codigo_cliente\"");
                BeginWriteAttribute("value", "\n                                           value=\"", 914, "\"", 980, 1);
#nullable restore
#line 16 "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Pagos\BuscarSuministro.cshtml"
WriteAttributeValue("", 965, codigo_cliente, 965, 15, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" required>
                                    <div class=""input-group-append"">
                                        <button class=""btn btn-dark""><i class=""uil uil-search-alt""></i> Buscar</button>
                                    </div>
                                </div>
                            </div>
                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n                    </div>\n                </div>\n                <div class=\"tab-pane show active\" id=\"basic-datatable-preview\">\n");
#nullable restore
#line 26 "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Pagos\BuscarSuministro.cshtml"
                     if (ViewBag.Error != null)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <span style=\"color:red\">");
#nullable restore
#line 28 "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Pagos\BuscarSuministro.cshtml"
                                           Write(ViewBag.Error);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\n                        <br />\n");
#nullable restore
#line 30 "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Pagos\BuscarSuministro.cshtml"
                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 31 "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Pagos\BuscarSuministro.cshtml"
                     if (ViewBag.Recibos != null)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                        <table id=""basic-datatable"" class=""table dt-responsive nowrap w-100"">
                            <thead>
                                <tr>
                                    <th>Código Suministro</th>
                                    <th>Cliente / Dirección</th>
                                    <th>Fecha Emision</th>
                                    <th>FechaVencimiento</th>
                                    <th>Detalle  Consulta</th>
                                    <th>Monto</th>
                                </tr>
                            </thead>
                            <tbody>
");
#nullable restore
#line 45 "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Pagos\BuscarSuministro.cshtml"
                             foreach (var item in (List<PagosVisaWeb.Controllers.Recibos>)ViewBag.Recibos)
                            {
                                if (cont == 0)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <tr>\n                                    <td>");
#nullable restore
#line 50 "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Pagos\BuscarSuministro.cshtml"
                                   Write(item.CodigoSuministro);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                                    <td>");
#nullable restore
#line 51 "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Pagos\BuscarSuministro.cshtml"
                                   Write(item.NombreCliente);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <br />");
#nullable restore
#line 51 "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Pagos\BuscarSuministro.cshtml"
                                                             Write(item.DireccionCliente);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                                    <td>");
#nullable restore
#line 52 "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Pagos\BuscarSuministro.cshtml"
                                   Write(item.FechaEmision.Substring(0, 10));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n\n");
#nullable restore
#line 54 "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Pagos\BuscarSuministro.cshtml"
                                     if (!String.IsNullOrEmpty(item.FechaVencimiento))
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <td>");
#nullable restore
#line 56 "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Pagos\BuscarSuministro.cshtml"
                                       Write(item.FechaVencimiento.Substring(0, 10));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n");
#nullable restore
#line 57 "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Pagos\BuscarSuministro.cshtml"
                                    }
                                    else
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <td>-</td>\n");
#nullable restore
#line 61 "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Pagos\BuscarSuministro.cshtml"
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <td>");
#nullable restore
#line 62 "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Pagos\BuscarSuministro.cshtml"
                                   Write(item.DetalleConsulta);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                                    <td class=\"text-right\">\n                                        <a class=\"btn btn-primary\" href=\"/PagarVisa/\">Pagar S/ ");
#nullable restore
#line 64 "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Pagos\BuscarSuministro.cshtml"
                                                                                          Write(item.MontoAPagarConsulta);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\n                                    </td>\n                                    </tr> \n");
#nullable restore
#line 67 "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Pagos\BuscarSuministro.cshtml"
                                    cont++;
                                    }
                                    else
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <tr>\n                                        <td>");
#nullable restore
#line 72 "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Pagos\BuscarSuministro.cshtml"
                                       Write(item.CodigoSuministro);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                                        <td>");
#nullable restore
#line 73 "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Pagos\BuscarSuministro.cshtml"
                                       Write(item.NombreCliente);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <br />");
#nullable restore
#line 73 "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Pagos\BuscarSuministro.cshtml"
                                                                 Write(item.DireccionCliente);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                                        <td>");
#nullable restore
#line 74 "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Pagos\BuscarSuministro.cshtml"
                                       Write(item.FechaEmision.Substring(0, 10));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n");
#nullable restore
#line 75 "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Pagos\BuscarSuministro.cshtml"
                                         if (item.FechaVencimiento != null)
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <td>\n                                        ");
#nullable restore
#line 78 "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Pagos\BuscarSuministro.cshtml"
                                   Write(item.FechaVencimiento.Substring(0, 10));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                                        </td> \n");
#nullable restore
#line 80 "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Pagos\BuscarSuministro.cshtml"
                                        }
                                        else
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <td>-</td>\n");
#nullable restore
#line 84 "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Pagos\BuscarSuministro.cshtml"
                                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <td>");
#nullable restore
#line 85 "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Pagos\BuscarSuministro.cshtml"
                                       Write(item.DetalleConsulta);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                                        <td class=\"text-right\">S/ ");
#nullable restore
#line 86 "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Pagos\BuscarSuministro.cshtml"
                                                             Write(item.MontoAPagarConsulta);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                                    </tr>\n");
#nullable restore
#line 88 "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Pagos\BuscarSuministro.cshtml"
                                    }
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                            </tbody>\n                        </table>\n");
#nullable restore
#line 92 "C:\01-electrosur-codigo\9. Codigo fuente\PagosVisaWeb\Views\Pagos\BuscarSuministro.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </div>\n            </div>\n        </div>\n    </div> \n</div>");
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
