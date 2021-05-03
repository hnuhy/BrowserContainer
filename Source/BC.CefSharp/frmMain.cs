using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using BCCefSharp = CefSharp;
using BCCefSharpWinForms = CefSharp.WinForms;

namespace BC.CefSharp
{
    public partial class frmMain : Form
    {
        public BCCefSharpWinForms.ChromiumWebBrowser browser;

        public frmMain()
        {
            InitializeComponent();
            InitBrowser();
        }

        public void InitBrowser()
        {
            //使用CEF自带的方法，解决High DPI问题
            BCCefSharp.Cef.EnableHighDPISupport();
            BCCefSharp.CefSettings settings = new BCCefSharp.CefSettings();
            settings.Locale = "zh-CN";
            settings.CefCommandLineArgs.Add("--disable-web-security", "1");//关闭同源策略,允许跨域
            //禁用GPU及代理（启用GPU可能会在网页拖拽过程中页面闪烁）
            settings.CefCommandLineArgs.Add("disable-gpu", "1");
            settings.CefCommandLineArgs.Add("no-proxy-server", "1");
            //主要是配置开启Media的命令参数，此配置可以允许摄像头打开摄像
            settings.CefCommandLineArgs.Add("enable-media-stream", "1");
            //访问连接不安全网页时不显示
            settings.CefCommandLineArgs.Add("--ignore-urlfetcher-cert-requests", "1");
            settings.CefCommandLineArgs.Add("--ignore-certificate-errors", "1");
            BCCefSharp.Cef.Initialize(settings);
            browser = new BCCefSharpWinForms.ChromiumWebBrowser("https://localhost:44396/");
            //browser.Load( "http://www.google.ca" );

            //var obj = new BC(System.Threading.SynchronizationContext.Current);
            var obj = new BoundObject(this);
            //C#对象注册到JS

            //旧方法
            browser.RegisterJsObject("boundObject", obj);
            browser.RegisterAsyncJsObject("boundAsync", new AsyncBoundObject());

            //新版
            //这个一定要开启，否则注入C#的对象无效
            //BCCefSharp.CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            //构造要注入的对象，参数为当前线程的调度上下文
            //注册C#对象
            //browser.JavascriptObjectRepository.Register("bc", obj, false, BCCefSharp.BindingOptions.DefaultBinder);


            this.Controls.Add(browser);
            browser.Dock = DockStyle.Fill;
            //不弹出子窗体,控制弹窗的接口是ILifeSpanHandler，并实现OnBeforePopup方法。
            browser.LifeSpanHandler = new LifeSpanHandler();
            //禁用右键的接口是IContextMenuHandler，并实现OnBeforeContextMenu 方法。
            //browser.MenuHandler = new MenuHandler();

            //自定义请求头
            browser.RequestHandler = new RequestHandler();

            //自定义按键处理
            browser.KeyboardHandler = new KeyboardHandler();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //调用页面js
            browser.GetBrowser().MainFrame.ExecuteJavaScriptAsync("jscallback('a');");
            BCCefSharp.Cef.Shutdown();
        }

    }
}
