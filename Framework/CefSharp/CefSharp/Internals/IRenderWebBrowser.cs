// Copyright © 2010-2016 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System;

namespace CefSharp.Internals
{
    public interface IRenderWebBrowser : IWebBrowserInternal
    {
        ScreenInfo GetScreenInfo();
        ViewRect GetViewRect();

        BitmapInfo CreateBitmapInfo(bool isPopup);
        void InvokeRenderAsync(BitmapInfo bitmapInfo);

        void SetCursor(IntPtr cursor, CefCursorType type);

        bool StartDragging(IDragData dragData, DragOperationsMask mask, int x, int y);

        void SetPopupIsOpen(bool show);
        void SetPopupSizeAndPosition(int width, int height, int x, int y);
    };
}
