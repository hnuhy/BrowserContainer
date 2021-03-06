// Copyright © 2010-2016 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System.Windows.Input;
using System.Windows.Threading;

namespace CefSharp.Wpf
{
    public interface IWpfWebBrowser : IWebBrowser
    {
        /// <summary>
        /// Navigates to the previous page in the browser history. Will automatically be enabled/disabled depending on the
        /// browser state.
        /// </summary>
        ICommand BackCommand { get; }

        /// <summary>
        /// Navigates to the next page in the browser history. Will automatically be enabled/disabled depending on the
        /// browser state.
        /// </summary>
        ICommand ForwardCommand { get; }

        /// <summary>
        /// Reloads the content of the current page. Will automatically be enabled/disabled depending on the browser state.
        /// </summary>
        ICommand ReloadCommand { get; }

        /// <summary>
        /// Prints the current browser contents.
        /// </summary>
        ICommand PrintCommand { get; }

        /// <summary>
        /// Increases the zoom level.
        /// </summary>
        ICommand ZoomInCommand { get; }

        /// <summary>
        /// Decreases the zoom level.
        /// </summary>
        ICommand ZoomOutCommand { get; }

        /// <summary>
        /// Resets the zoom level to the default. (100%)
        /// </summary>
        ICommand ZoomResetCommand { get; }

        /// <summary>
        /// Opens up a new program window (using the default text editor) where the source code of the currently displayed web
        /// page is shown.
        /// </summary>
        ICommand ViewSourceCommand { get; }

        /// <summary>
        /// Command which cleans up the Resources used by the ChromiumWebBrowser
        /// </summary>
        ICommand CleanupCommand { get; }

        /// <summary>
        /// Stops loading the current page.
        /// </summary>
        ICommand StopCommand { get; }

        /// <summary>
        /// Cut selected text to the clipboard.
        /// </summary>
        ICommand CutCommand { get; }

        /// <summary>
        /// Copy selected text to the clipboard.
        /// </summary>
        ICommand CopyCommand { get; }

        /// <summary>
        /// Paste text from the clipboard.
        /// </summary>
        ICommand PasteCommand { get; }

        /// <summary>
        /// Select all text.
        /// </summary>
        ICommand SelectAllCommand { get; }

        /// <summary>
        /// Undo last action.
        /// </summary>
        ICommand UndoCommand { get; }

        /// <summary>
        /// Redo last action.
        /// </summary>
        ICommand RedoCommand { get; }

        /// <summary>
        /// Gets the <see cref="Dispatcher"/> associated with this instance.
        /// </summary>
        Dispatcher Dispatcher { get; }

        /// <summary>
        /// The zoom level at which the browser control is currently displaying.
        /// Can be set to 0 to clear the zoom level (resets to default zoom level).
        /// </summary>
        double ZoomLevel { get; set; }

        /// <summary>
        /// The increment at which the <see cref="ZoomLevel"/> property will be incremented/decremented.
        /// </summary>
        double ZoomLevelIncrement { get; set; }

        /// <summary>
        /// The title of the web page being currently displayed.
        /// </summary>
        /// <remarks>This property is implemented as a Dependency Property and fully supports data binding.</remarks>
        string Title { get; }
    }
}
