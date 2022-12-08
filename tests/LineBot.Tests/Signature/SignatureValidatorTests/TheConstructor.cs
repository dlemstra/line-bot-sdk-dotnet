// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Xunit;

namespace Line.Tests.Signature
{
    public partial class SignatureValidatorTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldThrowExceptionWhenConfigurationIsNull()
            {
                ExceptionAssert.ThrowsArgumentNullException("configuration", () =>
                {
                    new SignatureValidator(null);
                });
            }
        }
    }
}
