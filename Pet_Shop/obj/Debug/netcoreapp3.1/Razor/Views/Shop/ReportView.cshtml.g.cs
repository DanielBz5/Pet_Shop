#pragma checksum "C:\Users\Leo M\source\repos\DanielBz5\Pet_Shop\Pet_Shop\Views\Shop\ReportView.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5fb5d24e6c0864767d90af144d9db439283378e3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shop_ReportView), @"mvc.1.0.view", @"/Views/Shop/ReportView.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
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
#line 1 "C:\Users\Leo M\source\repos\DanielBz5\Pet_Shop\Pet_Shop\Views\Shop\ReportView.cshtml"
using Pet_Shop.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Leo M\source\repos\DanielBz5\Pet_Shop\Pet_Shop\Views\Shop\ReportView.cshtml"
using System.Collections.Generic;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5fb5d24e6c0864767d90af144d9db439283378e3", @"/Views/Shop/ReportView.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fecdd34a518f420ff847e2a4a3d2cf90255c5086", @"/Views/_ViewImports.cshtml")]
    public class Views_Shop_ReportView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Pet_Shop.Models.Produto>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(" // Importa o namespace MeuProjeto.Models para a view\r\n");
            WriteLiteral(" // Importa o namespace System.Collections.Generic para a view\r\n\r\n\r\n");
            WriteLiteral("\r\n");
#nullable restore
#line 7 "C:\Users\Leo M\source\repos\DanielBz5\Pet_Shop\Pet_Shop\Views\Shop\ReportView.cshtml"
  
    ViewData["Title"] = "Relatório de Produtos";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h2>");
#nullable restore
#line 12 "C:\Users\Leo M\source\repos\DanielBz5\Pet_Shop\Pet_Shop\Views\Shop\ReportView.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h2>

<rsweb:ReportViewer ID=""ReportViewer1"" runat=""server"" Width=""100%"" Height=""600px"">
    <LocalReport ReportPath=""~/Reports/Produtos.rdl"">
        <DataSources>
            <rsweb:ReportDataSource Name=""DataSet1"" DataSourceId=""ObjectDataSource1"" />
        </DataSources>
    </LocalReport>
</rsweb:ReportViewer>

<asp:ObjectDataSource ID=""ObjectDataSource1"" runat=""server"" TypeName=""System.Collections.Generic.List`1[[Pet_Shop.Models.Produto, Pet_Shop]]""
                      SelectMethod=""GetData"">
    <SelectParameters>
        <asp:Parameter Name=""dataSource"" Type=""Object"" DefaultValue=""<%= ViewBag.ReportData %>"" />
    </SelectParameters>
</asp:ObjectDataSource>
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Pet_Shop.Models.Produto>> Html { get; private set; }
    }
}
#pragma warning restore 1591
