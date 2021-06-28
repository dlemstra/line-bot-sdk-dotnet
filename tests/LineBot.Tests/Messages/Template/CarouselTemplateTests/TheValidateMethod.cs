// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class CarouselTemplateTests
    {
        [TestClass]
        public class TheValidateMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenColumnsIsNull()
            {
                ITemplate template = new CarouselTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The columns cannot be null.", () =>
                {
                    template.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenColumnsIsInvalid()
            {
                ITemplate template = new CarouselTemplate()
                {
                    Columns = new[] { new CarouselColumn() }
                };

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null.", () =>
                {
                    template.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenColumnsContainsNull()
            {
                ITemplate template = new CarouselTemplate()
                {
                    Columns = new CarouselColumn[] { null }
                };

                ExceptionAssert.Throws<InvalidOperationException>("The column should not be null.", () =>
                {
                    template.Validate();
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValid()
            {
                ITemplate template = new CarouselTemplate()
                {
                    Columns = new[]
                    {
                        new CarouselColumn()
                        {
                            Text = "Foo",
                            Actions = new[] { new MessageAction() { Label = "Foo", Text = "Bar" } }
                        }
                    }
                };

                template.Validate();
            }
        }
    }
}
