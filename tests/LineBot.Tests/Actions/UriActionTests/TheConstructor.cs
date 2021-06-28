// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class UriActionTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ShouldCreateSerializeableObject()
            {
                var action = new UriAction
                {
                    Label = "Foo",
                    Url = new Uri("http://foo.bar")
                };

                string serialized = JsonSerializer.SerializeObject(action);
                Assert.AreEqual(@"{""type"":""uri"",""label"":""Foo"",""uri"":""http://foo.bar""}", serialized);
            }
        }
    }
}