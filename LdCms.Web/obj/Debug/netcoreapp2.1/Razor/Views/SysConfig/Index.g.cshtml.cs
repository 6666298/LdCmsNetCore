#pragma checksum "H:\Git\LdCmsNetCore\LdCms.Web\Views\SysConfig\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "73c91e4d67fdf1f44c45e9ea5d8601787b99f944"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_SysConfig_Index), @"mvc.1.0.view", @"/Views/SysConfig/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/SysConfig/Index.cshtml", typeof(AspNetCore.Views_SysConfig_Index))]
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
#line 1 "H:\Git\LdCmsNetCore\LdCms.Web\Views\SysConfig\Index.cshtml"
using LdCms.Common.Extension;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"73c91e4d67fdf1f44c45e9ea5d8601787b99f944", @"/Views/SysConfig/Index.cshtml")]
    public class Views_SysConfig_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<LdCms.EF.DbModels.Ld_Sys_Config>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/javascript"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/admin/lib/My97DatePicker/4.8/WdatePicker.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/admin/lib/jquery.validation/1.14.0/jquery.validate.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/admin/lib/jquery.validation/1.14.0/validate-methods.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/admin/lib/jquery.validation/1.14.0/messages_zh.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 3 "H:\Git\LdCmsNetCore\LdCms.Web\Views\SysConfig\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(159, 381, true);
            WriteLiteral(@"<nav class=""breadcrumb"">
    <i class=""Hui-iconfont"">&#xe67f;</i> 首页<span class=""c-gray en"">&gt;</span>系统管理<span class=""c-gray en"">&gt;</span>基本设置
    <a class=""btn btn-success radius r"" style=""line-height:1.6em;margin-top:3px"" href=""javascript:location.replace(location.href);"" title=""刷新""><i class=""Hui-iconfont"">&#xe68f;</i></a>
</nav>
<div class=""page-container"">
    <form");
            EndContext();
            BeginWriteAttribute("action", " action=\"", 540, "\"", 570, 1);
#line 12 "H:\Git\LdCmsNetCore\LdCms.Web\Views\SysConfig\Index.cshtml"
WriteAttributeValue("", 549, Url.Action("update"), 549, 21, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(571, 718, true);
            WriteLiteral(@" class=""form form-horizontal"" id=""form-add"" method=""post"">
        <div id=""tab-system"" class=""HuiTab"">
            <div class=""tabBar cl"">
                <span>基本设置</span>
                <span>安全设置</span>
                <span>邮件设置</span>
                <span>其他设置</span>
            </div>
            <div class=""tabCon"">
                <div class=""row cl"">
                    <label class=""form-label col-xs-4 col-sm-2"">
                        <span class=""c-red"">*</span>
                        网站名称：
                    </label>
                    <div class=""formControls col-xs-8 col-sm-9"">
                        <input type=""text"" id=""title"" name=""title"" placeholder=""控制在25个字、50个字节以内""");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 1289, "\"", 1309, 1);
#line 27 "H:\Git\LdCmsNetCore\LdCms.Web\Views\SysConfig\Index.cshtml"
WriteAttributeValue("", 1297, Model.Title, 1297, 12, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1310, 460, true);
            WriteLiteral(@" class=""input-text"">
                    </div>
                </div>
                <div class=""row cl"">
                    <label class=""form-label col-xs-4 col-sm-2"">
                        <span class=""c-red"">*</span>
                        关键词：
                    </label>
                    <div class=""formControls col-xs-8 col-sm-9"">
                        <input type=""text"" id=""keyword"" name=""keyword"" placeholder=""5个左右,8汉字以内,用英文,隔开""");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 1770, "\"", 1792, 1);
#line 36 "H:\Git\LdCmsNetCore\LdCms.Web\Views\SysConfig\Index.cshtml"
WriteAttributeValue("", 1778, Model.Keyword, 1778, 14, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1793, 467, true);
            WriteLiteral(@" class=""input-text"">
                    </div>
                </div>
                <div class=""row cl"">
                    <label class=""form-label col-xs-4 col-sm-2"">
                        <span class=""c-red"">*</span>
                        描述：
                    </label>
                    <div class=""formControls col-xs-8 col-sm-9"">
                        <input type=""text"" id=""description"" name=""description"" placeholder=""空制在80个汉字，160个字符以内""");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 2260, "\"", 2286, 1);
#line 45 "H:\Git\LdCmsNetCore\LdCms.Web\Views\SysConfig\Index.cshtml"
WriteAttributeValue("", 2268, Model.Description, 2268, 18, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2287, 459, true);
            WriteLiteral(@" class=""input-text"">
                    </div>
                </div>
                <div class=""row cl"">
                    <label class=""form-label col-xs-4 col-sm-2"">
                        <span class=""c-red"">*</span>
                        设置网站首页：
                    </label>
                    <div class=""formControls col-xs-8 col-sm-9"">
                        <input type=""text"" id=""homeUrl"" name=""homeUrl"" placeholder=""默认为home/index""");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 2746, "\"", 2768, 1);
#line 54 "H:\Git\LdCmsNetCore\LdCms.Web\Views\SysConfig\Index.cshtml"
WriteAttributeValue("", 2754, Model.HomeUrl, 2754, 14, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2769, 469, true);
            WriteLiteral(@" class=""input-text"">
                    </div>
                </div>
                <div class=""row cl"">
                    <label class=""form-label col-xs-4 col-sm-2"">
                        <span class=""c-red"">*</span>
                        css、js、images路径配置：
                    </label>
                    <div class=""formControls col-xs-8 col-sm-9"">
                        <input type=""text"" id=""styleSrc"" name=""styleSrc"" placeholder=""默认为空，为相对路径""");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 3238, "\"", 3261, 1);
#line 63 "H:\Git\LdCmsNetCore\LdCms.Web\Views\SysConfig\Index.cshtml"
WriteAttributeValue("", 3246, Model.StyleSrc, 3246, 15, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(3262, 465, true);
            WriteLiteral(@" class=""input-text"">
                    </div>
                </div>
                <div class=""row cl"">
                    <label class=""form-label col-xs-4 col-sm-2"">
                        <span class=""c-red"">*</span>
                        上传目录配置：
                    </label>
                    <div class=""formControls col-xs-8 col-sm-9"">
                        <input type=""text"" id=""uploadRoot"" name=""uploadRoot"" placeholder=""默认为uploadfile""");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 3727, "\"", 3752, 1);
#line 72 "H:\Git\LdCmsNetCore\LdCms.Web\Views\SysConfig\Index.cshtml"
WriteAttributeValue("", 3735, Model.UploadRoot, 3735, 17, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(3753, 470, true);
            WriteLiteral(@" class=""input-text"">
                    </div>
                </div>
                <div class=""row cl"">
                    <label class=""form-label col-xs-4 col-sm-2"">
                        <span class=""c-red"">*</span>
                        底部版权信息：
                    </label>
                    <div class=""formControls col-xs-8 col-sm-9"">
                        <input type=""text"" id=""copyright"" name=""copyright"" placeholder=""&copy; 2016 H-ui.net""");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 4223, "\"", 4247, 1);
#line 81 "H:\Git\LdCmsNetCore\LdCms.Web\Views\SysConfig\Index.cshtml"
WriteAttributeValue("", 4231, Model.Copyright, 4231, 16, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(4248, 359, true);
            WriteLiteral(@" class=""input-text"">
                    </div>
                </div>
                <div class=""row cl"">
                    <label class=""form-label col-xs-4 col-sm-2"">备案号：</label>
                    <div class=""formControls col-xs-8 col-sm-9"">
                        <input type=""text"" id=""icpNumber"" name=""icpNumber"" placeholder=""京ICP备00000000号""");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 4607, "\"", 4631, 1);
#line 87 "H:\Git\LdCmsNetCore\LdCms.Web\Views\SysConfig\Index.cshtml"
WriteAttributeValue("", 4615, Model.IcpNumber, 4615, 16, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(4632, 350, true);
            WriteLiteral(@" class=""input-text"">
                    </div>
                </div>
                <div class=""row cl"">
                    <label class=""form-label col-xs-4 col-sm-2"">统计代码：</label>
                    <div class=""formControls col-xs-8 col-sm-9"">
                        <textarea class=""textarea"" id=""statisticsCode"" name=""statisticsCode"">");
            EndContext();
            BeginContext(4983, 20, false);
#line 93 "H:\Git\LdCmsNetCore\LdCms.Web\Views\SysConfig\Index.cshtml"
                                                                                        Write(Model.StatisticsCode);

#line default
#line hidden
            EndContext();
            BeginContext(5003, 420, true);
            WriteLiteral(@"</textarea>
                    </div>
                </div>
            </div>
            <div class=""tabCon"">
                <div class=""row cl"">
                    <label class=""form-label col-xs-4 col-sm-2"">允许访问后台的IP列表：</label>
                    <div class=""formControls col-xs-8 col-sm-9"">
                        <textarea class=""textarea"" name=""loginIpAddressWhiteList"" id=""loginIpAddressWhiteList"">");
            EndContext();
            BeginContext(5424, 29, false);
#line 101 "H:\Git\LdCmsNetCore\LdCms.Web\Views\SysConfig\Index.cshtml"
                                                                                                          Write(Model.LoginIpAddressWhiteList);

#line default
#line hidden
            EndContext();
            BeginContext(5453, 353, true);
            WriteLiteral(@"</textarea>
                    </div>
                </div>
                <div class=""row cl"">
                    <label class=""form-label col-xs-4 col-sm-2"">后台登录失败最大次数：</label>
                    <div class=""formControls col-xs-8 col-sm-9"">
                        <input type=""text"" class=""input-text"" id=""maxLoginFail"" name=""maxLoginFail""");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 5806, "\"", 5833, 1);
#line 107 "H:\Git\LdCmsNetCore\LdCms.Web\Views\SysConfig\Index.cshtml"
WriteAttributeValue("", 5814, Model.MaxLoginFail, 5814, 19, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(5834, 407, true);
            WriteLiteral(@">
                    </div>
                </div>
                <div class=""row cl"">
                    <label class=""form-label col-xs-4 col-sm-2"">开启验证：</label>
                    <div class=""formControls col-xs-8 col-sm-9 skin-minimal"">
                        <div class=""check-box"">
                            <input type=""checkbox"" id=""isLoginIpAddress"" name=""isLoginIpAddress"" value=""1"" ");
            EndContext();
            BeginContext(6243, 48, false);
#line 114 "H:\Git\LdCmsNetCore\LdCms.Web\Views\SysConfig\Index.cshtml"
                                                                                                       Write(Model.IsLoginIpAddress.ToBool() ? "checked" : "");

#line default
#line hidden
            EndContext();
            BeginContext(6292, 1055, true);
            WriteLiteral(@" />
                            <label for=""checkbox-1"">&nbsp;</label>
                        </div>
                    </div>
                </div>
            </div>
            <div class=""tabCon"">
                <div class=""row cl"">
                    <label class=""form-label col-xs-4 col-sm-2""><span class=""c-red"">*</span>邮件发送模式：</label>
                    <div class=""formControls col-xs-8 col-sm-9"">
                        <span class=""select-box"">
                            <select class=""select"" size=""1"" id=""emailSendPattern"" name=""emailSendPattern"">
                                <option value=""SMTP"" selected=""selected"">SMTP</option>
                            </select>
                        </span>
                    </div>
                </div>
                <div class=""row cl"">
                    <label class=""form-label col-xs-4 col-sm-2"">SMTP服务器：</label>
                    <div class=""formControls col-xs-8 col-sm-9"">
                        <input type=""text"" ");
            WriteLiteral("id=\"emailHost\" name=\"emailHost\"");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 7347, "\"", 7371, 1);
#line 134 "H:\Git\LdCmsNetCore\LdCms.Web\Views\SysConfig\Index.cshtml"
WriteAttributeValue("", 7355, Model.EmailHost, 7355, 16, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(7372, 353, true);
            WriteLiteral(@" class=""input-text"">
                    </div>
                </div>
                <div class=""row cl"">
                    <label class=""form-label col-xs-4 col-sm-2"">SMTP 端口：</label>
                    <div class=""formControls col-xs-8 col-sm-9"">
                        <input type=""text"" id=""emailPort"" name=""emailPort"" class=""input-text""");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 7725, "\"", 7749, 1);
#line 140 "H:\Git\LdCmsNetCore\LdCms.Web\Views\SysConfig\Index.cshtml"
WriteAttributeValue("", 7733, Model.EmailPort, 7733, 16, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(7750, 300, true);
            WriteLiteral(@" >
                    </div>
                </div>
                <div class=""row cl"">
                    <label class=""form-label col-xs-4 col-sm-2"">邮箱帐号：</label>
                    <div class=""formControls col-xs-8 col-sm-9"">
                        <input type=""text"" class=""input-text""");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 8050, "\"", 8074, 1);
#line 146 "H:\Git\LdCmsNetCore\LdCms.Web\Views\SysConfig\Index.cshtml"
WriteAttributeValue("", 8058, Model.EmailName, 8058, 16, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(8075, 356, true);
            WriteLiteral(@" id=""emailName"" name=""emailName"">
                    </div>
                </div>
                <div class=""row cl"">
                    <label class=""form-label col-xs-4 col-sm-2"">邮箱密码：</label>
                    <div class=""formControls col-xs-8 col-sm-9"">
                        <input type=""password"" id=""emailPassword"" name=""emailPassword""");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 8431, "\"", 8459, 1);
#line 152 "H:\Git\LdCmsNetCore\LdCms.Web\Views\SysConfig\Index.cshtml"
WriteAttributeValue("", 8439, Model.EmailPassword, 8439, 20, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(8460, 339, true);
            WriteLiteral(@" class=""input-text"">
                    </div>
                </div>
                <div class=""row cl"">
                    <label class=""form-label col-xs-4 col-sm-2"">收件邮箱地址：</label>
                    <div class=""formControls col-xs-8 col-sm-9"">
                        <input type=""text"" id=""emailAddress"" name=""emailAddress""");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 8799, "\"", 8826, 1);
#line 158 "H:\Git\LdCmsNetCore\LdCms.Web\Views\SysConfig\Index.cshtml"
WriteAttributeValue("", 8807, Model.EmailAddress, 8807, 19, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(8827, 448, true);
            WriteLiteral(@" class=""input-text"">
                    </div>
                </div>
            </div>
            <div class=""tabCon"">
            </div>
        </div>
        <div class=""row cl"">
            <div class=""col-xs-8 col-sm-9 col-xs-offset-4 col-sm-offset-2"">
                <button class=""btn btn-primary radius"" type=""submit""><i class=""Hui-iconfont"">&#xe632;</i> 保存 </button>
            </div>
        </div>
    </form>
</div>
");
            EndContext();
            DefineSection("scripts", async() => {
                BeginContext(9354, 34, true);
                WriteLiteral("\r\n    <!--请在下方写此页面业务相关的脚本-->\r\n    ");
                EndContext();
                BeginContext(9388, 92, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b2404a23b9684b2f9b4251a0e261ff29", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(9480, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(9486, 102, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3716e749127046b283b34a07dde9d1fb", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(9588, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(9594, 103, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cf0f7653be9e49f7919b06696526f033", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(9697, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(9703, 98, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5ae4f722137c46e0803c1faae62547b2", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(9801, 2590, true);
                WriteLiteral(@"
    <script type=""text/javascript"">
        (function ($) {
            $.mainu = {
                init: function () {
                    $('.skin-minimal input').iCheck({
                        checkboxClass: 'icheckbox-blue',
                        radioClass: 'iradio-blue',
                        increaseArea: '20%'
                    });
                    $(""#tab-system"").Huitab({
                        index: 0
                    });
                },
                formSubmit: function () {
                    $(""#form-add"").validate({
                        rules: {
                            title: {
                                required: true,
                                minlength: 2,
                                maxlength: 200
                            }
                        },
                        onkeyup: false,
                        focusCleanup: true,
                        success: ""valid"",
                        submitHandler: func");
                WriteLiteral(@"tion (form) {
                            var isLoginIpAddress = $(""input[name='isLoginIpAddress']"").is(':checked');
                            $(form).ajaxSubmit({
                                type: ""POST"",
                                cache: false,
                                data: { isLoginIpAddress: isLoginIpAddress },
                                dataType: ""json"",
                                error: function (XMLHttpRequest, textStatus, errorThrown) {
                                    if (XMLHttpRequest.status != 200) {
                                        layer.alert(""POST[FAIL]"", { icon: 5 });
                                    }
                                },
                                success: function (result) {
                                    var state = result.state;          //错误代码
                                    var message = result.message;        //错误说明
                                    if (state == ""success"") {
                        ");
                WriteLiteral(@"                layer.msg(message, { icon: 1, time: 3000 });
                                    } else {
                                        layer.msg(message, { icon: 1, time: 3000 });
                                    }
                                }
                            });
                        }
                    });
                }
            };
            $(function () {
                $.mainu.init();
                $.mainu.formSubmit();
            });
        })(jQuery);
    </script>
");
                EndContext();
            }
            );
            BeginContext(12394, 2, true);
            WriteLiteral("\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<LdCms.EF.DbModels.Ld_Sys_Config> Html { get; private set; }
    }
}
#pragma warning restore 1591