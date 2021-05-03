using CefSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BCCefSharp = CefSharp;
using BCCefSharpWinForms = CefSharp.WinForms;

namespace BC.CefSharp
{
    public class RequestHandler : BCCefSharp.IRequestHandler
    {
        public bool GetAuthCredentials(IWebBrowser browserControl, IBrowser browser, IFrame frame, bool isProxy, string host, int port, string realm, string scheme, IAuthCallback callback)
        {
            return false;
        }

        public IResponseFilter GetResourceResponseFilter(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response)
        {
            //if (!response.ResponseHeaders["Content-Type"].Contains("application/json"))
            //{
            //    return null;
            //}

            //return FilterManager.CreateFilter(request.Identifier.ToString());

            return null;
        }

        public bool OnBeforeBrowse(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, bool isRedirect)
        {
            return false;
        }


        // 其他方法不是重点（不过实现一次接口你都想死，太多方法了！）
        // 其他参数也不是重点
        // 一定要重新覆盖访问头，直接赋值没卵用！
        public CefReturnValue OnBeforeResourceLoad(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IRequestCallback callback)
        {
            var headers = request.Headers;
            headers.Set("accept-language", "zh-CN,zh;q=0.8,en;q=0.6");
            //headers["User-Agent"] = "My User Agent";
            request.Headers = headers;

            return CefReturnValue.Continue;
        }

        public bool OnCertificateError(IWebBrowser browserControl, IBrowser browser, CefErrorCode errorCode, string requestUrl, ISslInfo sslInfo, IRequestCallback callback)
        {
            return false;
        }

        public bool OnOpenUrlFromTab(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, WindowOpenDisposition targetDisposition, bool userGesture)
        {
            return false;
        }

        public void OnPluginCrashed(IWebBrowser browserControl, IBrowser browser, string pluginPath)
        { 
        }

        public bool OnProtocolExecution(IWebBrowser browserControl, IBrowser browser, string url)
        {
            return false;
        }

        public bool OnQuotaRequest(IWebBrowser browserControl, IBrowser browser, string originUrl, long newSize, IRequestCallback callback)
        {
            return false;
        }

        public void OnRenderProcessTerminated(IWebBrowser browserControl, IBrowser browser, CefTerminationStatus status)
        { 
        }

        public void OnRenderViewReady(IWebBrowser browserControl, IBrowser browser)
        { 
        }

        public void OnResourceLoadComplete(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response, UrlRequestStatus status, long receivedContentLength)
        { 
        }

        public void OnResourceRedirect(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, ref string newUrl)
        { 
        }

        public bool OnResourceResponse(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response)
        {
            return false;
        }
    }

    public class AppendResponseFilter : IResponseFilter
    {
        private static Encoding encoding = Encoding.UTF8;



        public event Action<string, string, string, long> VOIDFUN;
        private string _url;
        private string _type;
        public AppendResponseFilter(string url, string type)
        {
            _url = url;
            _type = type;

        }
        bool IResponseFilter.InitFilter()
        {
            return true;
        }
        FilterStatus IResponseFilter.Filter(Stream dataIn, out long dataInRead, Stream dataOut, out long dataOutWritten)
        {
            if (dataIn == null)
            {
                dataInRead = 0;
                dataOutWritten = 0;

                return FilterStatus.Done;
            }
            dataInRead = dataIn.Length;
            dataOutWritten = Math.Min(dataInRead, dataOut.Length);

            byte[] buffer = new byte[dataOutWritten];
            int bytesRead = dataIn.Read(buffer, 0, (int)dataOutWritten);


            var s = System.Text.Encoding.UTF8.GetString(buffer);
            VOIDFUN?.BeginInvoke(s, _url, _type, dataInRead, null, null);
            dataOut.Write(buffer, 0, bytesRead);
            return FilterStatus.Done;
        }

        public void Dispose()
        {

        }

        #region
        public event Action<byte[]> NotifyData;
        private int contentLength = 0;
        public List<byte> dataAll = new List<byte>();

        public void SetContentLength(int contentLength)
        {
            this.contentLength = contentLength;
        }
        #endregion
    }



    public class FilterManager
    {
        private static Dictionary<string, IResponseFilter> dataList = new Dictionary<string, IResponseFilter>();

        public static IResponseFilter CreateFilter(string guid)
        {
            lock (dataList)
            {
                var filter = new AppendResponseFilter("", "");
                dataList.Add(guid, filter);

                return filter;
            }
        }

        public static IResponseFilter GetFileter(string guid)
        {
            lock (dataList)
            {
                return dataList[guid];
            }
        }
    }
}
