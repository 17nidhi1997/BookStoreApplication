using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreCommonLayer
{
    public class CustomException : Exception
    {
        public enum ExceptionType
        {
            INVALID_INPUT, 
            OPTIONS_NOT_MATCH, 
            NULL_EXCEPTION
        }
        public ExceptionType type;
        public CustomException(ExceptionType type)
        {
            this.type = type;
        }
        public CustomException(ExceptionType type, String message) : base(message)
        {
            this.type = type;
        }
    }
}

