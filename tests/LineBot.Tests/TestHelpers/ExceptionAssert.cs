// <copyright file="ExceptionAssert.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.
// </copyright>

using System;
using System.Threading.Tasks;
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

        public static async Task<TException> ThrowsAsync<TException>(string message, Func<Task> action)
            where TException : Exception
        {
            TException exception = await ThrowsAsync<TException>(action);
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

        public static async Task ThrowsArgumentNullExceptionAsync(string paramName, Func<Task> action)
        {
            ArgumentException exception = await ThrowsAsync<ArgumentNullException>("Value cannot be null.", action);
            Assert.AreEqual(paramName, exception.ParamName);
        }

        public static async Task ThrowsArgumentEmptyExceptionAsync(string paramName, Func<Task> action)
        {
            ArgumentException exception = await ThrowsAsync<ArgumentException>("Value cannot be empty.", action);
            Assert.AreEqual(paramName, exception.ParamName);
        }

        private static async Task<TException> ThrowsAsync<TException>(Func<Task> action)
            where TException : Exception
        {
            try
            {
                await action();
                return AssertNotThrown<TException>();
            }
            catch (TException exception)
            {
                return CheckException(exception);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static TException Throws<TException>(Action action)
            where TException : Exception
        {
            try
            {
                action();
                return AssertNotThrown<TException>();
            }
            catch (TException exception)
            {
                return CheckException(exception);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static TException AssertNotThrown<TException>()
            where TException : Exception
        {
            Assert.Fail("Exception of type {0} was not thrown.", typeof(TException).Name);
            return null;
        }

        private static TException CheckException<TException>(TException exception)
            where TException : Exception
        {
            Type type = exception.GetType();
            if (type != typeof(TException))
                Assert.Fail("Exception of type {0} was not thrown an exception of type {1} was thrown.", typeof(TException).Name, type.Name);

            return exception;
        }
    }
}
