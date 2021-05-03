﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CefSharp;
using CefSharp.WinForms;

namespace BC.CefSharp
{
    internal class LifeSpanHandler : ILifeSpanHandler
    {
        //弹出前触发的事件
        public bool OnBeforePopup(IWebBrowser webBrowser, IBrowser browser, IFrame frame, string targetUrl,
            string targetFrameName, WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures,
            IWindowInfo windowInfo, IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
        {
            //使用源窗口打开链接，取消创建新窗口
            newBrowser = null;
            var chromiumWebBrowser = (ChromiumWebBrowser)webBrowser;
            chromiumWebBrowser.Load(targetUrl);
            return true;
        }

        public void OnAfterCreated(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {

        }

        public bool DoClose(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
            return true;
        }

        public void OnBeforeClose(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {

        }
    }
}
