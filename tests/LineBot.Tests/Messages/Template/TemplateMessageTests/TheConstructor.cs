// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class TemplateMessageTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldCreateSerializeableObject()
            {
                var message = new TemplateMessage()
                {
                    AlternativeText = "Alternative",
                    Template = new ConfirmTemplate
                    {
                        Text = "Confirm",
                        OkAction = new UriAction()
                        {
                            Label = "OkLabel",
                            Url = new Uri("https://foo.bar")
                        },
                        CancelAction = new PostbackAction()
                        {
                            Label = "CancelLabel",
                            Data = "Postback",
                            Text = "CancelText"
                        }
                    }
                };

                var serialized = JsonSerializer.SerializeObject(message);
                Assert.Equal(@"{""type"":""template"",""altText"":""Alternative"",""template"":{""type"":""confirm"",""text"":""Confirm"",""actions"":[{""type"":""uri"",""label"":""OkLabel"",""uri"":""https://foo.bar""},{""type"":""postback"",""label"":""CancelLabel"",""data"":""Postback"",""text"":""CancelText""}]}}", serialized);
            }
        }
    }
}
