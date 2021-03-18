﻿using System;

namespace SynWord_Server_CSharp.Exceptions
{
    public class UserDoesNotExistException : UserException
    {
        const string message = "User does not exist";

        public UserDoesNotExistException() : base(message) {}
    }
}
