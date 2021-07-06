// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using static EdgeSharp.Interop;

namespace EdgeSharp
{
    internal partial class Interops
    {
        internal partial class Kernel32
        {
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern IntPtr GetModuleHandleW(string moduleName);
        }
    }
}
