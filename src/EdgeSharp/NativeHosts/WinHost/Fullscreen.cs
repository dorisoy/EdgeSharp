﻿// Copyright (c) 2021 The EdgeSharp Authors. All rights reserved.
// Use of this source code is governed by MIT license that can be found in the LICENSE file.

using EdgeSharp.Core.Configuration;
using System;
using System.Drawing;
using static EdgeSharp.Interop;
using static EdgeSharp.Interop.User32;

namespace EdgeSharp.NativeHosts
{
    public partial class WinNativeHost
    {
        public virtual void ToggleFullscreen(IntPtr hWnd)
        {
            bool isWindowed = IsWindowed(hWnd);
            if (isWindowed)
            {
                WINDOWPLACEMENT wpPrev;
                var placement = GetWindowPlacement(hWnd, out wpPrev);
                if (placement == BOOL.TRUE)
                {
                    switch (wpPrev.showCmd)
                    {
                        case SW.RESTORE:
                        case SW.NORMAL:
                            _windowOptions.WindowState = WindowState.Normal;
                            break;
                        case SW.SHOWMAXIMIZED:
                            _windowOptions.WindowState = WindowState.Maximize;
                            break;
                    }

                    _windowStylePlacement.WindowPlacement = wpPrev;
                    _windowStylePlacement.State = _windowOptions.WindowState;
                }

                var styles = _windowStylePlacement.FullscreenStyles;
                var exStyles = _windowStylePlacement.FullscreenExStyles;
                SetWindowLong(hWnd, GWL.STYLE, (IntPtr)styles);
                SetWindowLong(hWnd, GWL.EXSTYLE, (IntPtr)exStyles);
                _windowOptions.KioskMode = false;
                _windowOptions.Fullscreen = true;
                _windowOptions.WindowState = WindowState.Fullscreen;
                ShowWindow(hWnd, SW.SHOWMAXIMIZED);
                UpdateWindow(hWnd);
            }
            else
            {
                var styles = _windowStylePlacement.Styles;
                var exStyles = _windowStylePlacement.ExStyles;
                SetWindowLong(hWnd, GWL.STYLE, (IntPtr)styles);
                SetWindowLong(hWnd, GWL.EXSTYLE, (IntPtr)exStyles);
                _windowOptions.KioskMode = false;
                _windowOptions.Fullscreen = false;
                _windowOptions.WindowState = _windowStylePlacement.State == WindowState.Fullscreen || _windowStylePlacement.State == WindowState.Maximize ? WindowState.Maximize : WindowState.Normal;
                _windowStylePlacement.State = _windowOptions.WindowState;
                 var placement = _windowStylePlacement.WindowPlacement;
                 SetWindowPlacement(hWnd, ref placement);
                ShowWindow(hWnd, _windowOptions.WindowState == WindowState.Maximize ? SW.SHOWMAXIMIZED : SW.SHOWNORMAL);
                UpdateWindow(hWnd);
            }
        }

        // https://www.youtube.com/watch?v=0GQSOZe_D4I
        protected virtual void SetFullscreenScreen(IntPtr hWnd, int style, int styleEx)
        {
            Size fullscreenSize = new Size();
            var windowHDC = GetDC(hWnd);
            fullscreenSize.Width = Gdi32.GetDeviceCaps(windowHDC, Gdi32.DeviceCapability.HORZRES);
            fullscreenSize.Height = Gdi32.GetDeviceCaps(windowHDC, Gdi32.DeviceCapability.VERTRES);
            ReleaseDC(hWnd, windowHDC);

            SetWindowLong(hWnd, GWL.STYLE, (IntPtr)style);
            SetWindowLong(hWnd, GWL.EXSTYLE, (IntPtr)styleEx);

            SetWindowPos(hWnd, HWND_TOP, 0, 0, fullscreenSize.Width, fullscreenSize.Height, SWP.NOZORDER | SWP.FRAMECHANGED);
        }

        protected virtual bool IsWindowed(IntPtr hWnd)
        {
            return _windowOptions.WindowState != WindowState.Fullscreen;
        }
    }
}
