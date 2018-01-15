// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
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

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    [TestClass]
    public class SignatureValidatorTests
    {
        [TestMethod]
        public void Constructor_ConfigurationNull_ThrowsException()
        {
            ExceptionAssert.ThrowsArgumentNullException("configuration", () =>
            {
                new SignatureValidator(null);
            });
        }

        [TestMethod]
        public void Validate_ContentNull_ThrowsException()
        {
            ExceptionAssert.ThrowsArgumentNullException("content", () =>
            {
                SignatureValidator validator = new SignatureValidator(TestConfiguration.Create());
                validator.Validate(null, "test");
            });
        }

        [TestMethod]
        public void Validate_ContentEmpty_ThrowsException()
        {
            ExceptionAssert.ThrowsArgumentEmptyException("content", () =>
            {
                SignatureValidator validator = new SignatureValidator(TestConfiguration.Create());
                validator.Validate(new byte[] { }, "test");
            });
        }

        [TestMethod]
        public void Validate_SignatureNull_ThrowsException()
        {
            ExceptionAssert.ThrowsArgumentNullException("signature", () =>
            {
                SignatureValidator validator = new SignatureValidator(TestConfiguration.Create());
                validator.Validate(new byte[] { 0 }, null);
            });
        }

        [TestMethod]
        public void Validate_SignatureEmpty_ThrowsException()
        {
            ExceptionAssert.ThrowsArgumentEmptyException("signature", () =>
            {
                SignatureValidator validator = new SignatureValidator(TestConfiguration.Create());
                validator.Validate(new byte[] { 0 }, string.Empty);
            });
        }

        [TestMethod]
        public void Validate_SignatureInvalid_ThrowsException()
        {
            ExceptionAssert.Throws<LineBotException>("Invalid signature.", () =>
            {
                SignatureValidator validator = new SignatureValidator(TestConfiguration.Create());
                validator.Validate(new byte[] { 42 }, "FooBar");
            });
        }

        [TestMethod]
        public void Validate_SignatureIncorrectLength_ThrowsException()
        {
            ExceptionAssert.Throws<LineBotException>("Invalid signature.", () =>
            {
                SignatureValidator validator = new SignatureValidator(TestConfiguration.Create());
                validator.Validate(new byte[] { 42 }, "NDI=");
            });
        }

        [TestMethod]
        public void Validate_SignatureValid_ThrowsNoException()
        {
            SignatureValidator validator = new SignatureValidator(TestConfiguration.Create());
            validator.Validate(new byte[] { 42 }, "aajPtaEL8oyiitLlTbSzkFCTDQ7Lr0m/89eDhe6tFx4=");
        }
    }
}
