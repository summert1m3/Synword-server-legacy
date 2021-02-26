﻿using System;

namespace SynWord_Server_CSharp.Exceptions
{
    public class MaxSymbolLimitReachedException : Exception
    {
        const string message = "Max symbol limit reached";

        public MaxSymbolLimitReachedException() : base(message) {}
    }
}
