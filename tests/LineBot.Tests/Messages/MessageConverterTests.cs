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

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    [TestClass]
    public partial class MessageConverterTests
    {
        [TestMethod]
        public void Convert_TemplateMessageWithConfirmTemplateAndUriActionLabelIsNull_ThrowsException()
        {
            TemplateMessage message = new TemplateMessage()
            {
                AlternativeText = "Alternative",
                Template = new ConfirmTemplate()
                {
                    Text = "ConfirmText",
                    OkAction = new MessageAction()
                    {
                        Label = "PostbackLabel",
                        Text = "PostbackText"
                    },
                    CancelAction = new UriAction()
                    {
                        Url = new Uri("http://foo.bar")
                    }
                }
            };

            ExceptionAssert.Throws<InvalidOperationException>("The label cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { message });
            });
        }

        [TestMethod]
        public void Convert_TemplateMessageWithConfirmTemplateAndUriActionUrlIsNull_ThrowsException()
        {
            TemplateMessage message = new TemplateMessage()
            {
                AlternativeText = "Alternative",
                Template = new ConfirmTemplate()
                {
                    Text = "ConfirmText",
                    OkAction = new MessageAction()
                    {
                        Label = "PostbackLabel",
                        Text = "PostbackText"
                    },
                    CancelAction = new UriAction()
                    {
                        Label = "PostbackLabel"
                    }
                }
            };

            ExceptionAssert.Throws<InvalidOperationException>("The url cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { message });
            });
        }

        [TestMethod]
        public void Convert_TemplateMessageWithCarouselTemplateAndColumnTextIsNull_ThrowsException()
        {
            TemplateMessage message = new TemplateMessage()
            {
                AlternativeText = "Alternative",
                Template = new CarouselTemplate()
                {
                    Columns = new ICarouselColumn[]
                    {
                        new CarouselColumn()
                        {
                            ThumbnailUrl = new Uri("https://foo.bar"),
                            Title = "CarouselTitle",
                            Actions = new ITemplateAction[]
                            {
                                new MessageAction()
                                {
                                    Label = "PostbackLabel",
                                    Text = "PostbackText"
                                }
                            }
                        }
                    }
                }
            };

            ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { message });
            });
        }

        [TestMethod]
        public void Convert_TemplateMessageWithCarouselTemplateAndColumnActionsIsNull_ThrowsException()
        {
            TemplateMessage message = new TemplateMessage()
            {
                AlternativeText = "Alternative",
                Template = new CarouselTemplate()
                {
                    Columns = new ICarouselColumn[]
                    {
                        new CarouselColumn()
                        {
                            ThumbnailUrl = new Uri("https://foo.bar"),
                            Title = "CarouselTitle",
                            Text = "CarouselText"
                        }
                    }
                }
            };

            ExceptionAssert.Throws<InvalidOperationException>("The actions cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { message });
            });
        }
    }
}
