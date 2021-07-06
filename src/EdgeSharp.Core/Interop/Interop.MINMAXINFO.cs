﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Drawing;

namespace EdgeSharp
{
    public static partial class Interop
    {
        public static partial class User32
        {
            public struct MINMAXINFO
            {
                public Point ptReserved;
                public Point ptMaxSize;
                public Point ptMaxPosition;
                public Point ptMinTrackSize;
                public Point ptMaxTrackSize;
            }
        }
    }
}
