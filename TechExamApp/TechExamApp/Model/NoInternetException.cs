﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TechExamApp.Model
{
    public class NoInternetException : Exception
    {
        public NoInternetException()
        {
        }

        public NoInternetException(string message) : base(message)
        {
        }

        public NoInternetException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoInternetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
