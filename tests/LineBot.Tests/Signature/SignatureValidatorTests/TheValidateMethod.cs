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

namespace Line.Tests.Signature
{
    public partial class SignatureValidatorTests
    {
        [TestClass]
        public class TheValidateMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenContentIsNull()
            {
                SignatureValidator validator = new SignatureValidator(TestConfiguration.Create());

                ExceptionAssert.ThrowsArgumentNullException("content", () =>
                {
                    validator.Validate(null, "test");
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenContentIsEmpty()
            {
                SignatureValidator validator = new SignatureValidator(TestConfiguration.Create());

                ExceptionAssert.ThrowsArgumentEmptyException("content", () =>
                {
                    validator.Validate(new byte[] { }, "test");
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenSignatureIsNull()
            {
                SignatureValidator validator = new SignatureValidator(TestConfiguration.Create());

                ExceptionAssert.ThrowsArgumentNullException("signature", () =>
                {
                    validator.Validate(new byte[] { 0 }, null);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenSignatureIsEmpty()
            {
                SignatureValidator validator = new SignatureValidator(TestConfiguration.Create());

                ExceptionAssert.ThrowsArgumentEmptyException("signature", () =>
                {
                    validator.Validate(new byte[] { 0 }, string.Empty);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenSignatureLengthIsInvalid()
            {
                SignatureValidator validator = new SignatureValidator(TestConfiguration.Create());

                ExceptionAssert.Throws<LineBotException>("Invalid signature.", () =>
                {
                    validator.Validate(new byte[] { 42 }, "NDI=");
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenSignatureIsInvalid()
            {
                SignatureValidator validator = new SignatureValidator(TestConfiguration.Create());

                ExceptionAssert.Throws<LineBotException>("Invalid signature.", () =>
                {
                    validator.Validate(new byte[] { 42 }, "============================================");
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenSignatureIsValid()
            {
                SignatureValidator validator = new SignatureValidator(TestConfiguration.Create());
                validator.Validate(new byte[] { 42 }, "aajPtaEL8oyiitLlTbSzkFCTDQ7Lr0m/89eDhe6tFx4=");
            }
        }
    }
}
