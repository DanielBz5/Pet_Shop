#pragma checksum "C:\Users\Leo M\source\repos\DanielBz5\Pet_Shop\Pet_Shop\Views\Shop\ResultPedido.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ae254ff8ca3403fed5afd3501b3e8a5fc5ea12bd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shop_ResultPedido), @"mvc.1.0.view", @"/Views/Shop/ResultPedido.cshtml")]
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
#line 1 "C:\Users\Leo M\source\repos\DanielBz5\Pet_Shop\Pet_Shop\Views\_ViewImports.cshtml"
using Pet_Shop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Leo M\source\repos\DanielBz5\Pet_Shop\Pet_Shop\Views\_ViewImports.cshtml"
using Pet_Shop.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ae254ff8ca3403fed5afd3501b3e8a5fc5ea12bd", @"/Views/Shop/ResultPedido.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fecdd34a518f420ff847e2a4a3d2cf90255c5086", @"/Views/_ViewImports.cshtml")]
    public class Views_Shop_ResultPedido : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Pet_Shop.Models.Pedido>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "text", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("chave-pagamento"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Shop", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "FinalizaPedido", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div class=\"container-manage\">\r\n    <div class=\"manage-produto\" style=\"width:60%;\">\r\n");
#nullable restore
#line 5 "C:\Users\Leo M\source\repos\DanielBz5\Pet_Shop\Pet_Shop\Views\Shop\ResultPedido.cshtml"
         if (Model.TipoPagamento == "Pix" && Model.StatusPagamento != "approved")
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <h5 class=\"titulo-black\">Etapa 2: Pagamento<i class=\"bi bi-bag-check-fill\"></i></h5><br>\r\n");
#nullable restore
#line 8 "C:\Users\Leo M\source\repos\DanielBz5\Pet_Shop\Pet_Shop\Views\Shop\ResultPedido.cshtml"
        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <h5 class=\"titulo-black\"> Pedido Concluido! <i class=\"bi bi-bag-check-fill\"></i></h5><br>\r\n");
#nullable restore
#line 12 "C:\Users\Leo M\source\repos\DanielBz5\Pet_Shop\Pet_Shop\Views\Shop\ResultPedido.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"result-pedido\">\r\n            <h5 style=\"color:black; font-size:23px;\">Numero do Pedido: ");
#nullable restore
#line 14 "C:\Users\Leo M\source\repos\DanielBz5\Pet_Shop\Pet_Shop\Views\Shop\ResultPedido.cshtml"
                                                                  Write(Model.Cod);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n");
#nullable restore
#line 15 "C:\Users\Leo M\source\repos\DanielBz5\Pet_Shop\Pet_Shop\Views\Shop\ResultPedido.cshtml"
             if (Model.TipoPagamento == "Pix" && Model.StatusPagamento != "approved")
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <span>Leia o QrCod ou Copie a Chave e realize o Pagamento do seu pedido, após o pagamento clique em Finalizar pedido.</span>\r\n                <div class=\"img-qrcod\">\r\n                    <img class=\"img-shop\"");
            BeginWriteAttribute("src", " src=\"", 915, "\"", 982, 2);
            WriteAttributeValue("", 921, "data:image/png;base64,", 921, 22, true);
#nullable restore
#line 19 "C:\Users\Leo M\source\repos\DanielBz5\Pet_Shop\Pet_Shop\Views\Shop\ResultPedido.cshtml"
WriteAttributeValue("", 943, Convert.ToBase64String(Model.QrCode), 943, 39, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" alt=""Imagem do Produto"">
                </div>
                <div class=""row mb-3"">
                    <button type=""button"" id=""copy-pix"" class=""btn btn-primary""><i class=""bi bi-clipboard-check-fill""></i>Copiar</button>
                    <div class=""col-sm-10"">
                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "ae254ff8ca3403fed5afd3501b3e8a5fc5ea12bd7982", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 24 "C:\Users\Leo M\source\repos\DanielBz5\Pet_Shop\Pet_Shop\Views\Shop\ResultPedido.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.ChavePagamento);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    </div>\r\n                </div>\r\n");
            WriteLiteral("                <div class=\"btn-login\">\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ae254ff8ca3403fed5afd3501b3e8a5fc5ea12bd9953", async() => {
                WriteLiteral("<i class=\"bi bi-bag-plus-fill\"></i> Finalizar Pedido");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-Cod", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 29 "C:\Users\Leo M\source\repos\DanielBz5\Pet_Shop\Pet_Shop\Views\Shop\ResultPedido.cshtml"
                                                                                                    WriteLiteral(Model.Cod);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["Cod"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-Cod", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["Cod"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                </div>\r\n");
#nullable restore
#line 31 "C:\Users\Leo M\source\repos\DanielBz5\Pet_Shop\Pet_Shop\Views\Shop\ResultPedido.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <span>Seu pedido foi Concluido e Logo será Entregue.</span>\r\n");
#nullable restore
#line 35 "C:\Users\Leo M\source\repos\DanielBz5\Pet_Shop\Pet_Shop\Views\Shop\ResultPedido.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n    </div>\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Pet_Shop.Models.Pedido> Html { get; private set; }
    }
}
#pragma warning restore 1591