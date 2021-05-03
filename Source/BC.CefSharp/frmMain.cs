using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using CefSharp;
using CefSharp.WinForms;

namespace BC.CefSharp
{
    public partial class frmMain : Form
    {
        public ChromiumWebBrowser browser;

        public frmMain()
        {
            InitializeComponent();
            InitBrowser();
        }

        public void InitBrowser()
        {
            //使用CEF自带的方法，解决High DPI问题
            Cef.EnableHighDPISupport();
            CefSettings settings = new CefSettings();
            settings.Locale = "zh-CN";
            //禁用GPU及代理（启用GPU可能会在网页拖拽过程中页面闪烁）
            settings.CefCommandLineArgs.Add("disable-gpu", "1");
            settings.CefCommandLineArgs.Add("no-proxy-server", "1");
            //主要是配置开启Media的命令参数，此配置可以允许摄像头打开摄像
            settings.CefCommandLineArgs.Add("enable-media-stream", "1");
            //访问连接不安全网页时不显示
            settings.CefCommandLineArgs.Add("--ignore-urlfetcher-cert-requests", "1");
            settings.CefCommandLineArgs.Add("--ignore-certificate-errors", "1");
            Cef.Initialize(settings);
            browser = new ChromiumWebBrowser("http://www.baidu.com");
            this.Controls.Add(browser);
            browser.Dock = DockStyle.Fill;
            //不弹出子窗体,控制弹窗的接口是ILifeSpanHandler，并实现OnBeforePopup方法。
            browser.LifeSpanHandler = new LifeSpanHandler();
            //禁用右键的接口是IContextMenuHandler，并实现OnBeforeContextMenu 方法。
            browser.MenuHandler = new MenuHandler();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }
    }
}
