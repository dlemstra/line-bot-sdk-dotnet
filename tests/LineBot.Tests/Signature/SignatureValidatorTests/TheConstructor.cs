// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests.Signature
{
    public partial class SignatureValidatorTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
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
