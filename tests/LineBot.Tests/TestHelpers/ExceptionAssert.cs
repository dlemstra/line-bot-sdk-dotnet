// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.Threading.Tasks;
using Xunit;

namespace Line.Tests
{
    internal static class ExceptionAssert
    {
        public static TException Throws<TException>(string message, Action action)
            where TException : Exception
        {
            var exception = Throws<TException>(action);
            Assert.NotNull(exception.Message);
            if (!exception.Message.StartsWith(message))
                Assert.Fail($"The message `{exception.Message}` does not start with `{message}`.");

            return exception;
        }

        public static async Task<TException> ThrowsAsync<TException>(string message, Func<Task> action)
            where TException : Exception
        {
            var exception = await ThrowsAsync<TException>(action);
            Assert.NotNull(exception.Message);
            if (!exception.Message.StartsWith(message))
                Assert.Fail($"The message `{exception.Message}` does not start with `{message}`.");

            return exception;
        }

        public static void ThrowsArgumentNullException(string paramName, Action action)
        {
            ArgumentException exception = Throws<ArgumentNullException>(action);
            Assert.Equal(paramName, exception.ParamName);
        }

        public static async Task ThrowsArgumentNullExceptionAsync(string paramName, Func<Task> action)
        {
            ArgumentException exception = await ThrowsAsync<ArgumentNullException>(action);
            Assert.Equal(paramName, exception.ParamName);
        }

        public static void ThrowsArgumentEmptyException(string paramName, Action action)
        {
            var exception = Throws<ArgumentException>("Value cannot be empty.", action);
            Assert.Equal(paramName, exception.ParamName);
        }

        public static async Task ThrowsArgumentEmptyExceptionAsync(string paramName, Func<Task> action)
        {
            var exception = await ThrowsAsync<ArgumentException>("Value cannot be empty.", action);
            Assert.Equal(paramName, exception.ParamName);
        }

        public static void ThrowsArgumentException(string paramName, string message, Action action)
        {
            var exception = Throws<ArgumentException>(message, action);
            Assert.Equal(paramName, exception.ParamName);
        }

        public static async Task<LineBotException> ThrowsUnknownError(Func<Task> action)
        {
            return await ThrowsAsync<LineBotException>("Unknown error", action);
        }

        public static async Task<TException> ThrowsAsync<TException>(Func<Task> action)
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

        public static TException Throws<TException>(Action action)
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
            Assert.Fail($"Exception of type {typeof(TException).Name} was not thrown.");
            return null;
        }

        private static TException CheckException<TException>(TException exception)
            where TException : Exception
        {
            var type = exception.GetType();
            if (type != typeof(TException))
                Assert.Fail($"Exception of type {typeof(TException).Name} was not thrown an exception of type {type.Name} was thrown.");

            return exception;
        }
    }
}
