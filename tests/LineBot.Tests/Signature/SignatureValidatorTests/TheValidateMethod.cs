// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

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
