#pragma checksum "C:\Users\ybinh\Desktop\Sem6\projectv5\COMP313-Team5\WeLinkUp\WeLinkUp\Views\Event\EventDetail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7af0d9b9180c8e3237dd6f6068951932db74546f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Event_EventDetail), @"mvc.1.0.view", @"/Views/Event/EventDetail.cshtml")]
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
#line 1 "C:\Users\ybinh\Desktop\Sem6\projectv5\COMP313-Team5\WeLinkUp\WeLinkUp\Views\_ViewImports.cshtml"
using WeLinkUp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\ybinh\Desktop\Sem6\projectv5\COMP313-Team5\WeLinkUp\WeLinkUp\Views\_ViewImports.cshtml"
using WeLinkUp.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\ybinh\Desktop\Sem6\projectv5\COMP313-Team5\WeLinkUp\WeLinkUp\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7af0d9b9180c8e3237dd6f6068951932db74546f", @"/Views/Event/EventDetail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f643486ab6396a0e844b25da3645afec75c47987", @"/Views/_ViewImports.cshtml")]
    public class Views_Event_EventDetail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<WeLinkUp.Models.EventDetailModel>
    {
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 4 "C:\Users\ybinh\Desktop\Sem6\projectv5\COMP313-Team5\WeLinkUp\WeLinkUp\Views\Event\EventDetail.cshtml"
   ViewData["Title"] = "Event Details"; 

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<html>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7af0d9b9180c8e3237dd6f6068951932db74546f3941", async() => {
                WriteLiteral("\r\n    <br />\r\n    <br />\r\n\r\n    <h1 class=\"w3-content\">Event Details</h1>\r\n    <hr />\r\n    <h3>Event Title:    ");
#nullable restore
#line 13 "C:\Users\ybinh\Desktop\Sem6\projectv5\COMP313-Team5\WeLinkUp\WeLinkUp\Views\Event\EventDetail.cshtml"
                   Write(Html.DisplayFor(model => model.Events.EventTitle));

#line default
#line hidden
#nullable disable
                WriteLiteral("</h3>\r\n    <h3>Event Host:    ");
#nullable restore
#line 14 "C:\Users\ybinh\Desktop\Sem6\projectv5\COMP313-Team5\WeLinkUp\WeLinkUp\Views\Event\EventDetail.cshtml"
                  Write(Html.DisplayFor(model => model.Events.HostId));

#line default
#line hidden
#nullable disable
                WriteLiteral("</h3>\r\n    <h3>Location:   ");
#nullable restore
#line 15 "C:\Users\ybinh\Desktop\Sem6\projectv5\COMP313-Team5\WeLinkUp\WeLinkUp\Views\Event\EventDetail.cshtml"
               Write(Html.DisplayFor(model => model.Events.Location));

#line default
#line hidden
#nullable disable
                WriteLiteral("</h3>\r\n    <h3>Date:    ");
#nullable restore
#line 16 "C:\Users\ybinh\Desktop\Sem6\projectv5\COMP313-Team5\WeLinkUp\WeLinkUp\Views\Event\EventDetail.cshtml"
            Write(Html.DisplayFor(model => model.Events.Date));

#line default
#line hidden
#nullable disable
                WriteLiteral("</h3>\r\n    <h3>Start Time:    ");
#nullable restore
#line 17 "C:\Users\ybinh\Desktop\Sem6\projectv5\COMP313-Team5\WeLinkUp\WeLinkUp\Views\Event\EventDetail.cshtml"
                  Write(Html.DisplayFor(model => model.Events.StartTime));

#line default
#line hidden
#nullable disable
                WriteLiteral("</h3>\r\n    <h3>End Time:    ");
#nullable restore
#line 18 "C:\Users\ybinh\Desktop\Sem6\projectv5\COMP313-Team5\WeLinkUp\WeLinkUp\Views\Event\EventDetail.cshtml"
                Write(Html.DisplayFor(model => model.Events.EndTime));

#line default
#line hidden
#nullable disable
                WriteLiteral("</h3>\r\n\r\n    <asp:Image runat=\"server\" ID=\"image1\" enable=\"false\"");
                BeginWriteAttribute("imageUrl", " imageUrl=\"", 703, "\"", 714, 0);
                EndWriteAttribute();
                WriteLiteral("></asp:Image>\r\n    <asp:Button runat=\"server\" id=\"btnShowImg\" Text=\"Show Image\" OnClick=\"btnShowImg_Click\"> </asp:Button>\r\n\r\n");
#nullable restore
#line 23 "C:\Users\ybinh\Desktop\Sem6\projectv5\COMP313-Team5\WeLinkUp\WeLinkUp\Views\Event\EventDetail.cshtml"
     if (DateTime.Compare(DateTime.Now, Convert.ToDateTime(Model.Events.Date)) > 0)
    {

#line default
#line hidden
#nullable disable
                WriteLiteral("        <h3>Event is already passed 1</h3>\r\n");
#nullable restore
#line 26 "C:\Users\ybinh\Desktop\Sem6\projectv5\COMP313-Team5\WeLinkUp\WeLinkUp\Views\Event\EventDetail.cshtml"
    }
    else if (DateTime.Compare(DateTime.Now, Convert.ToDateTime(Model.Events.Date)) == 0 && DateTime.Compare(DateTime.Now, Convert.ToDateTime(Model.Events.EndTime)) > 0)
    {

#line default
#line hidden
#nullable disable
                WriteLiteral("        <h3>Event is already passed 2</h3>\r\n");
#nullable restore
#line 30 "C:\Users\ybinh\Desktop\Sem6\projectv5\COMP313-Team5\WeLinkUp\WeLinkUp\Views\Event\EventDetail.cshtml"
    }
    else
    {

#line default
#line hidden
#nullable disable
                WriteLiteral(@"        <asp:Button ID=""joinBtn""
                    runat=""server""
                    Text=""Join Event""
                    Font-Bold=""true""
                    width=""200%""></asp:Button>
        <asp:Button ID=""modiBtn""
                    runat=""server""
                    Text=""Modify Event""
                    width=""200%""> </asp:Button>
        <asp:Button ID=""cancelBtn""
                    runat=""server""
                    Text=""Cancel Event""
                    width=""200%""> </asp:Button>
");
#nullable restore
#line 46 "C:\Users\ybinh\Desktop\Sem6\projectv5\COMP313-Team5\WeLinkUp\WeLinkUp\Views\Event\EventDetail.cshtml"
    }

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n");
#nullable restore
#line 48 "C:\Users\ybinh\Desktop\Sem6\projectv5\COMP313-Team5\WeLinkUp\WeLinkUp\Views\Event\EventDetail.cshtml"
     foreach (AttendeeList user in Model.AttendeeList)
    {

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7af0d9b9180c8e3237dd6f6068951932db74546f8639", async() => {
                    WriteLiteral("\r\n    <input type=\"text\"");
                    BeginWriteAttribute("value", " value=\"", 1847, "\"", 1867, 1);
#nullable restore
#line 51 "C:\Users\ybinh\Desktop\Sem6\projectv5\COMP313-Team5\WeLinkUp\WeLinkUp\Views\Event\EventDetail.cshtml"
WriteAttributeValue("", 1855, user.UserId, 1855, 12, false);

#line default
#line hidden
#nullable disable
                    EndWriteAttribute();
                    WriteLiteral(" />\r\n    <input type=\"text\"");
                    BeginWriteAttribute("value", " value=\"", 1895, "\"", 1915, 1);
#nullable restore
#line 52 "C:\Users\ybinh\Desktop\Sem6\projectv5\COMP313-Team5\WeLinkUp\WeLinkUp\Views\Event\EventDetail.cshtml"
WriteAttributeValue("", 1903, user.Status, 1903, 12, false);

#line default
#line hidden
#nullable disable
                    EndWriteAttribute();
                    WriteLiteral(" />\r\n\r\n");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
#nullable restore
#line 55 "C:\Users\ybinh\Desktop\Sem6\projectv5\COMP313-Team5\WeLinkUp\WeLinkUp\Views\Event\EventDetail.cshtml"
    }

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
</html>
<script>
        protected void btnShowImg_Click(object sender, EventArgs e)
        {
            if (image1.enable = ""false"") {
                image1.enable = ""true"";
            } else {
                image1.enable = ""false"";
            }
        }
</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<WeLinkUp.Models.EventDetailModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
