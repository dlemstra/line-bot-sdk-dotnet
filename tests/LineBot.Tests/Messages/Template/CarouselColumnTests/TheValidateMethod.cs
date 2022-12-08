﻿// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class CarouselColumnTests
    {
        public class TheValidateMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenTextIsNull()
            {
                var column = new CarouselColumn()
                {
                    ThumbnailUrl = new Uri("https://foo.bar"),
                    Title = "CarouselColumnTitle",
                    Actions = new[] { new PostbackAction() }
                };

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null.", () =>
                {
                    column.Validate();
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenActionsIsNull()
            {
                var column = new CarouselColumn()
                {
                    ThumbnailUrl = new Uri("https://foo.bar"),
                    Text = "CarouselColumnText",
                    Title = "CarouselColumnTitle"
                };

                ExceptionAssert.Throws<InvalidOperationException>("The actions cannot be null.", () =>
                {
                    column.Validate();
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenActionsIsInvalid()
            {
                var column = new CarouselColumn()
                {
                    ThumbnailUrl = new Uri("https://foo.bar"),
                    Text = "CarouselColumnText",
                    Title = "CarouselColumnTitle",
                    Actions = new[] { new PostbackAction() }
                };

                ExceptionAssert.Throws<InvalidOperationException>("The label cannot be null.", () =>
                {
                    column.Validate();
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValid()
            {
                var column = new CarouselColumn()
                {
                    ThumbnailUrl = new Uri("https://foo.bar"),
                    Text = "CarouselColumnText",
                    Title = "CarouselColumnTitle",
                    Actions = new[] { new PostbackAction() { Data = "Foo", Label = "Bar" } }
                };

                column.Validate();
            }
        }
    }
}
