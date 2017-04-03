// <copyright file="ExceptionAssert.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.
// </copyright>

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    [ExcludeFromCodeCoverage]
    internal static class ExceptionAssert
    {
        public static TException Throws<TException>(string message, Action action)
           where TException : Exception
        {
            TException exception = Throws<TException>(action);
            Assert.IsNotNull(exception.Message);
            if (!exception.Message.StartsWith(message))
                Assert.Fail($"The message `{exception.Message}` does not start with `{message}`.");

            return exception;
        }

        public static void ThrowsArgumentNullException(string paramName, Action action)
        {
            ArgumentException exception = Throws<ArgumentNullException>("Value cannot be null.", action);
            Assert.AreEqual(paramName, exception.ParamName);
        }

        public static void ThrowsArgumentException(string paramName, string message, Action action)
        {
            ArgumentException exception = Throws<ArgumentException>(message, action);
            Assert.AreEqual(paramName, exception.ParamName);
        }

        private static TException Throws<TException>(Action action)
           where TException : Exception
        {
            try
            {
                action();
                Assert.Fail("Exception of type {0} was not thrown.", typeof(TException).Name);
                return null;
            }
            catch (TException exception)
            {
                Type type = exception.GetType();
                if (type != typeof(TException))
                    Assert.Fail("Exception of type {0} was not thrown an exception of type {1} was thrown.", typeof(TException).Name, type.Name);

                return exception;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
