﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;

namespace EdgeSharp
{
    public static partial class Interop
    {
        public static partial class User32
        {
            [DllImport(Libraries.User32, SetLastError = true)]
            public static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);

        }
    }
}