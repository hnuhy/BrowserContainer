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
            Cef.EnableHighDPISupport();
            CefSettings settings = new CefSettings();
            settings.Locale = "zh-CN";
            Cef.Initialize(settings);
            browser = new ChromiumWebBrowser("http://www.baidu.com");
            this.Controls.Add(browser);
            browser.Dock = DockStyle.Fill;
            browser.LifeSpanHandler = new LifeSpanHandler();
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
