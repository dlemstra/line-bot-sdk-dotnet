// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Xunit;

namespace Line.Tests.Signature
{
    public partial class SignatureValidatorTests
    {
        public class TheValidateMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenContentIsNull()
            {
                var validator = new SignatureValidator(TestConfiguration.Create());

                ExceptionAssert.ThrowsArgumentNullException("content", () =>
                {
                    validator.Validate(null, "test");
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenContentIsEmpty()
            {
                var validator = new SignatureValidator(TestConfiguration.Create());

                ExceptionAssert.ThrowsArgumentEmptyException("content", () =>
                {
                    validator.Validate(new byte[] { }, "test");
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenSignatureIsNull()
            {
                var validator = new SignatureValidator(TestConfiguration.Create());

                ExceptionAssert.ThrowsArgumentNullException("signature", () =>
                {
                    validator.Validate(new byte[] { 0 }, null);
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenSignatureIsEmpty()
            {
                var validator = new SignatureValidator(TestConfiguration.Create());

                ExceptionAssert.ThrowsArgumentEmptyException("signature", () =>
                {
                    validator.Validate(new byte[] { 0 }, string.Empty);
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenSignatureLengthIsInvalid()
            {
                var validator = new SignatureValidator(TestConfiguration.Create());

                ExceptionAssert.Throws<LineBotException>("Invalid signature.", () =>
                {
                    validator.Validate(new byte[] { 42 }, "NDI=");
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenSignatureIsInvalid()
            {
                var validator = new SignatureValidator(TestConfiguration.Create());

                ExceptionAssert.Throws<LineBotException>("Invalid signature.", () =>
                {
                    validator.Validate(new byte[] { 42 }, "============================================");
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSignatureIsValid()
            {
                var validator = new SignatureValidator(TestConfiguration.Create());
                validator.Validate(new byte[] { 42 }, "aajPtaEL8oyiitLlTbSzkFCTDQ7Lr0m/89eDhe6tFx4=");
            }
        }
    }
}
