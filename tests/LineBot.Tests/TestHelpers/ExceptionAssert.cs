// Copyright 2017-2019 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
//
// Dirk Lemstra licenses this file to you under the Apache License,
// version 2.0 (the "License"); you may not use this file except in compliance
// with the License. You may obtain a copy of the License at:
//
//   https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
// License for the specific language governing permissions and limitations
// under the License.

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
            ArgumentException exception = Throws<ArgumentNullException>(action);
            Assert.AreEqual(paramName, exception.ParamName);
        }

        public static async Task ThrowsArgumentNullExceptionAsync(string paramName, Func<Task> action)
        {
            ArgumentException exception = await ThrowsAsync<ArgumentNullException>(action);
            Assert.AreEqual(paramName, exception.ParamName);
        }

        public static void ThrowsArgumentEmptyException(string paramName, Action action)
        {
            ArgumentException exception = Throws<ArgumentException>("Value cannot be empty.", action);
            Assert.AreEqual(paramName, exception.ParamName);
        }

        public static async Task ThrowsArgumentEmptyExceptionAsync(string paramName, Func<Task> action)
        {
            ArgumentException exception = await ThrowsAsync<ArgumentException>("Value cannot be empty.", action);
            Assert.AreEqual(paramName, exception.ParamName);
        }

        public static void ThrowsArgumentException(string paramName, string message, Action action)
        {
            ArgumentException exception = Throws<ArgumentException>(message, action);
            Assert.AreEqual(paramName, exception.ParamName);
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
