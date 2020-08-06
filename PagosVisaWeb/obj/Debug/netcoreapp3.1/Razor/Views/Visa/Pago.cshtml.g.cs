#pragma checksum "C:\Users\roder\Documents\GitHub\01-electrosur-codigo\PagosVisaWeb\Views\Visa\Pago.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cbaf87d1b43c72a57fab1ea2e780bc22e46e6e15"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Visa_Pago), @"mvc.1.0.view", @"/Views/Visa/Pago.cshtml")]
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
#line 1 "C:\Users\roder\Documents\GitHub\01-electrosur-codigo\PagosVisaWeb\Views\_ViewImports.cshtml"
using PagosVisaWeb;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\roder\Documents\GitHub\01-electrosur-codigo\PagosVisaWeb\Views\_ViewImports.cshtml"
using PagosVisaWeb.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cbaf87d1b43c72a57fab1ea2e780bc22e46e6e15", @"/Views/Visa/Pago.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"99f2668289511a5d522bc63fb530245c524f2a77", @"/Views/_ViewImports.cshtml")]
    public class Views_Visa_Pago : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("/ConfirmacionPago"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 2 "C:\Users\roder\Documents\GitHub\01-electrosur-codigo\PagosVisaWeb\Views\Visa\Pago.cshtml"
   ViewBag.Title = "PagoSuministro"; 

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""row"" style=""margin-top:30px"">
    <div class=""col-12"">
        <div class=""card"">
            <div class=""card-body"">
                <div class=""row"">
                    <div class=""col-lg-3"">
                    </div>
                    <div class=""col-lg-6"">
                        <div class=""border p-3 mt-4 mt-lg-0 rounded"">
                            <h4 class=""header-title mb-3"">Resumen de pago</h4>

                            <div class=""table-responsive"">
                                <table class=""table mb-0"">
                                    <tr>
                                        <td>Número de operación</td>
                                        <td>");
#nullable restore
#line 19 "C:\Users\roder\Documents\GitHub\01-electrosur-codigo\PagosVisaWeb\Views\Visa\Pago.cshtml"
                                       Write(ViewBag.PurchaseNumber.ToString().PadLeft(7, '0'));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                                    </tr>\n                                    <tr>\n                                        <td>Número de contrato</td>\n                                        <td>");
#nullable restore
#line 23 "C:\Users\roder\Documents\GitHub\01-electrosur-codigo\PagosVisaWeb\Views\Visa\Pago.cshtml"
                                       Write(ViewBag.codigo_cliente);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                                    </tr>\n                                    <tr>\n                                        <td>Cliente / Dirección</td>\n                                        <td>");
#nullable restore
#line 27 "C:\Users\roder\Documents\GitHub\01-electrosur-codigo\PagosVisaWeb\Views\Visa\Pago.cshtml"
                                       Write(ViewBag.Recibo.NombreCliente);

#line default
#line hidden
#nullable disable
            WriteLiteral(" / ");
#nullable restore
#line 27 "C:\Users\roder\Documents\GitHub\01-electrosur-codigo\PagosVisaWeb\Views\Visa\Pago.cshtml"
                                                                       Write(ViewBag.Recibo.DireccionCliente);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                                    </tr>\n                                    <tr>\n                                        <td>Fecha Emision</td>\n                                        <td>");
#nullable restore
#line 31 "C:\Users\roder\Documents\GitHub\01-electrosur-codigo\PagosVisaWeb\Views\Visa\Pago.cshtml"
                                       Write(ViewBag.Recibo.FechaEmision.Substring(0, 10));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                                    </tr>\n                                    <tr>\n");
#nullable restore
#line 34 "C:\Users\roder\Documents\GitHub\01-electrosur-codigo\PagosVisaWeb\Views\Visa\Pago.cshtml"
                                         if (!String.IsNullOrEmpty(ViewBag.Recibo.FechaVencimiento))
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <td>FechaVencimiento</td>\n                                            <td>");
#nullable restore
#line 37 "C:\Users\roder\Documents\GitHub\01-electrosur-codigo\PagosVisaWeb\Views\Visa\Pago.cshtml"
                                           Write(ViewBag.Recibo.FechaVencimiento.Substring(0, 10));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n");
#nullable restore
#line 38 "C:\Users\roder\Documents\GitHub\01-electrosur-codigo\PagosVisaWeb\Views\Visa\Pago.cshtml"
                                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    </tr>\n                                    <tr>\n                                        <td>Detalle  Consulta</td>\n                                        <td>\n                                            ");
#nullable restore
#line 43 "C:\Users\roder\Documents\GitHub\01-electrosur-codigo\PagosVisaWeb\Views\Visa\Pago.cshtml"
                                       Write(ViewBag.Recibo.DetalleConsulta);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                                        </td>\n                                    </tr>\n                                    <tr>\n                                        <th>Total :</th>\n                                        <th>S/ ");
#nullable restore
#line 48 "C:\Users\roder\Documents\GitHub\01-electrosur-codigo\PagosVisaWeb\Views\Visa\Pago.cshtml"
                                          Write(ViewBag.Total);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\n                                    </tr>\n                                </table>\n                            </div>\n                        </div>\n\n                        <div class=\"mt-3 text-right\">\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cbaf87d1b43c72a57fab1ea2e780bc22e46e6e159313", async() => {
                WriteLiteral("\n                                ");
#nullable restore
#line 56 "C:\Users\roder\Documents\GitHub\01-electrosur-codigo\PagosVisaWeb\Views\Visa\Pago.cshtml"
                           Write(Html.AntiForgeryToken());

#line default
#line hidden
#nullable disable
                WriteLiteral("\n                                <script src=\"https://static-content-qas.vnforapps.com/v2/js/checkout.js?qa=true\"\n                                        data-sessiontoken=\"");
#nullable restore
#line 58 "C:\Users\roder\Documents\GitHub\01-electrosur-codigo\PagosVisaWeb\Views\Visa\Pago.cshtml"
                                                      Write(ViewBag.Token_Session);

#line default
#line hidden
#nullable disable
                WriteLiteral("\"\n                                        data-merchantid=\"");
#nullable restore
#line 59 "C:\Users\roder\Documents\GitHub\01-electrosur-codigo\PagosVisaWeb\Views\Visa\Pago.cshtml"
                                                    Write(ViewBag.MerchantID);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"""
                                        data-channel=""web""
                                        data-buttonsize=""""
                                        data-buttoncolor=""""
                                        data-merchantlogo=""https://pagos.electrosur.com.pe/appVISAweb/images/logo-light.png""
                                        data-merchantname=""""
                                        data-formbuttoncolor=""#0A0A2A""
                                        data-showamount=""TRUE""
                                        data-purchasenumber=""");
#nullable restore
#line 67 "C:\Users\roder\Documents\GitHub\01-electrosur-codigo\PagosVisaWeb\Views\Visa\Pago.cshtml"
                                                        Write(ViewBag.PurchaseNumber);

#line default
#line hidden
#nullable disable
                WriteLiteral("\"\n                                        data-amount=\"");
#nullable restore
#line 68 "C:\Users\roder\Documents\GitHub\01-electrosur-codigo\PagosVisaWeb\Views\Visa\Pago.cshtml"
                                                Write(ViewBag.Total);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"""
                                        data-usertoken=""""
                                        data-recurrence=""false""
                                        data-frequency=""Quarterly""
                                        data-recurrencetype=""fixed""
                                        data-recurrenceamount=""200""
                                        data-expirationminutes=""20""
                                        data-timeouturl=""http://""
                                        data-documenttype=""0""
                                        data-documentid=""""
                                        data-beneficiaryid=""");
#nullable restore
#line 78 "C:\Users\roder\Documents\GitHub\01-electrosur-codigo\PagosVisaWeb\Views\Visa\Pago.cshtml"
                                                       Write(ViewBag.MerchantID);

#line default
#line hidden
#nullable disable
                WriteLiteral("\"\n                                        data-productid=\"\"\n                                        data-phone=\"\">\n                                </script>\n                            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n                        </div>\n                    </div>\n                </div>\n            </div>\n        </div>\n    </div>\n</div>");
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