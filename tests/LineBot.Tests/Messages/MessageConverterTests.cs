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
        public void Convert_ImagemapMessageAreaWithoutWidth_ThrowsException()
        {
            TestImagemapMessage message = new TestImagemapMessage()
            {
                Actions = new TestImagemapUriAction[]
                {
                    new TestImagemapUriAction()
                    {
                        Area = new TestImagemapArea()
                        {
                            Width = 0
                        }
                    }
                }
            };

            ExceptionAssert.Throws<InvalidOperationException>("The width should be at least 1.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { message });
            });
        }

        [TestMethod]
        public void Convert_ImagemapMessageAreaUrlIsNull_ThrowsException()
        {
            TestImagemapMessage message = new TestImagemapMessage()
            {
                Actions = new TestImagemapUriAction[]
                {
                    new TestImagemapUriAction()
                    {
                       Url = null
                    }
                }
            };

            ExceptionAssert.Throws<InvalidOperationException>("The url cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { message });
            });
        }

        [TestMethod]
        public void Convert_ImagemapMessageAreaWithoutHeight_ThrowsException()
        {
            TestImagemapMessage message = new TestImagemapMessage()
            {
                Actions = new TestImagemapUriAction[]
                {
                    new TestImagemapUriAction()
                    {
                        Area = new TestImagemapArea()
                        {
                            Height = 0
                        }
                    }
                }
            };

            ExceptionAssert.Throws<InvalidOperationException>("The height should be at least 1.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { message });
            });
        }

        [TestMethod]
        public void Convert_ImagemapMessageActionWithoutText_ThrowsException()
        {
            TestImagemapMessage message = new TestImagemapMessage()
            {
                Actions = new TestImagemapMessageAction[]
                {
                    new TestImagemapMessageAction()
                    {
                        Text = null
                    }
                }
            };

            ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { message });
            });
        }

        [TestMethod]
        public void Convert_TemplateMessageAlternativeTextIsNull_ThrowsException()
        {
            TemplateMessage message = new TemplateMessage()
            {
            };

            ExceptionAssert.Throws<InvalidOperationException>("The alternative text cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { message });
            });
        }

        [TestMethod]
        public void Convert_TemplateMessageTemplateIsNull_ThrowsException()
        {
            TemplateMessage message = new TemplateMessage()
            {
                AlternativeText = "AlternativeText"
            };

            ExceptionAssert.Throws<InvalidOperationException>("The template cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { message });
            });
        }

        [TestMethod]
        public void Convert_TemplateMessageWithButtonsTemplate_InstanceIsPreserved()
        {
            TemplateMessage message = new TemplateMessage()
            {
                AlternativeText = "Alternative",
                Template = new ButtonsTemplate()
                {
                    ThumbnailUrl = new Uri("https://foo.bar"),
                    Title = "ButtonsTitle",
                    Text = "ButtonsText",
                    Actions = new ITemplateAction[]
                    {
                        new PostbackAction()
                        {
                            Label = "PostbackLabel",
                            Text = "PostbackText",
                            Data = "PostbackData",
                        }
                    }
                }
            };

            ISendMessage[] messages = MessageConverter.Convert(new ISendMessage[] { message });

            Assert.AreEqual(1, messages.Length);

            ITemplateMessage templateMessage = messages[0] as ITemplateMessage;
            Assert.AreEqual(message, templateMessage);

            IButtonsTemplate buttonsTemplate = templateMessage.Template as IButtonsTemplate;
            Assert.AreEqual(message.Template, buttonsTemplate);

            ITemplateAction action = buttonsTemplate.Actions.First();
            Assert.AreEqual(action, ((ButtonsTemplate)message.Template).Actions.First());
        }

        [TestMethod]
        public void Convert_TemplateMessageWithCustomIButtonsTemplate_ConvertedToConfirmTemplate()
        {
            TestTemplateMessage message = new TestTemplateMessage()
            {
                Template = new TestButtonsTemplate()
            };

            ISendMessage[] messages = MessageConverter.Convert(new ISendMessage[] { message });

            Assert.AreEqual(1, messages.Length);
            Assert.AreNotEqual(message, messages[0]);

            TemplateMessage templateMessage = messages[0] as TemplateMessage;
            Assert.AreEqual("AlternativeText", templateMessage.AlternativeText);

            ButtonsTemplate template = templateMessage.Template as ButtonsTemplate;
            Assert.AreEqual(new Uri("https://bar.foo"), template.ThumbnailUrl);
            Assert.AreEqual("ButtonsTitle", template.Title);
            Assert.AreEqual("ButtonsText", template.Text);

            ITemplateAction[] actions = template.Actions.ToArray();

            PostbackAction action = actions[0] as PostbackAction;
            Assert.AreEqual("PostbackLabel", action.Label);
            Assert.AreEqual("PostbackData", action.Data);
            Assert.AreEqual("PostbackText", action.Text);
        }

        [TestMethod]
        public void Convert_TemplateMessageWithButtonsTemplateAndTextIsNull_ThrowsException()
        {
            TemplateMessage message = new TemplateMessage()
            {
                AlternativeText = "Alternative",
                Template = new ButtonsTemplate()
                {
                    ThumbnailUrl = new Uri("https://foo.bar"),
                    Title = "ButtonsTitle",
                    Actions = new ITemplateAction[]
                    {
                        new PostbackAction()
                        {
                            Label = "PostbackLabel",
                            Text = "PostbackText",
                            Data = "PostbackData",
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
        public void Convert_TemplateMessageWithButtonsTemplateAndActionsIsNull_ThrowsException()
        {
            TemplateMessage message = new TemplateMessage()
            {
                AlternativeText = "Alternative",
                Template = new ButtonsTemplate()
                {
                    ThumbnailUrl = new Uri("https://foo.bar"),
                    Title = "ButtonsTitle",
                    Text = "ButtonsText"
                }
            };

            ExceptionAssert.Throws<InvalidOperationException>("The actions cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { message });
            });
        }

        [TestMethod]
        public void Convert_TemplateMessageWithButtonsTemplateAndPostbackActionLabelIsNull_ThrowsException()
        {
            TemplateMessage message = new TemplateMessage()
            {
                AlternativeText = "Alternative",
                Template = new ButtonsTemplate()
                {
                    ThumbnailUrl = new Uri("https://foo.bar"),
                    Title = "ButtonsTitle",
                    Text = "ButtonsText",
                    Actions = new ITemplateAction[]
                    {
                        new PostbackAction()
                        {
                            Text = "PostbackText",
                            Data = "PostbackData",
                        }
                    }
                }
            };

            ExceptionAssert.Throws<InvalidOperationException>("The label cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { message });
            });
        }

        [TestMethod]
        public void Convert_TemplateMessageWithButtonsTemplateAndPostbackActionDataIsNull_ThrowsException()
        {
            TemplateMessage message = new TemplateMessage()
            {
                AlternativeText = "Alternative",
                Template = new ButtonsTemplate()
                {
                    ThumbnailUrl = new Uri("https://foo.bar"),
                    Title = "ButtonsTitle",
                    Text = "ButtonsText",
                    Actions = new ITemplateAction[]
                    {
                        new PostbackAction()
                        {
                            Label = "PostbackLabel",
                            Text = "PostbackText"
                        }
                    }
                }
            };

            ExceptionAssert.Throws<InvalidOperationException>("The data cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { message });
            });
        }

        [TestMethod]
        public void Convert_TemplateMessageWithConfirmTemplate_InstanceIsPreserved()
        {
            TemplateMessage message = new TemplateMessage()
            {
                AlternativeText = "Alternative",
                Template = new ConfirmTemplate()
                {
                    Text = "ButtonsText",
                    OkAction = new MessageAction()
                    {
                        Label = "PostbackLabel",
                        Text = "PostbackText"
                    },
                    CancelAction = new UriAction()
                    {
                        Label = "PostbackLabel",
                        Url = new Uri("http://foo.bar")
                    }
                }
            };

            ISendMessage[] messages = MessageConverter.Convert(new ISendMessage[] { message });

            Assert.AreEqual(1, messages.Length);

            ITemplateMessage templateMessage = messages[0] as ITemplateMessage;
            Assert.AreEqual(message, templateMessage);

            IConfirmTemplate confirmTemplate = templateMessage.Template as IConfirmTemplate;
            Assert.AreEqual(message.Template, confirmTemplate);

            Assert.AreEqual(confirmTemplate.OkAction, ((ConfirmTemplate)message.Template).OkAction);
            Assert.AreEqual(confirmTemplate.CancelAction, ((ConfirmTemplate)message.Template).CancelAction);
        }

        [TestMethod]
        public void Convert_TemplateMessageWithCustomIConfirmTemplate_ConvertedToConfirmTemplate()
        {
            TestTemplateMessage message = new TestTemplateMessage()
            {
                Template = new TestConfirmTemplate()
            };

            ISendMessage[] messages = MessageConverter.Convert(new ISendMessage[] { message });

            Assert.AreEqual(1, messages.Length);
            Assert.AreNotEqual(message, messages[0]);

            TemplateMessage templateMessage = messages[0] as TemplateMessage;
            Assert.AreEqual("AlternativeText", templateMessage.AlternativeText);

            ConfirmTemplate template = templateMessage.Template as ConfirmTemplate;
            Assert.AreEqual("ConfirmText", template.Text);

            MessageAction okAction = template.OkAction as MessageAction;
            Assert.AreEqual("MessageLabel", okAction.Label);
            Assert.AreEqual("MessageText", okAction.Text);

            UriAction cancelAction = template.CancelAction as UriAction;
            Assert.AreEqual("UriLabel", cancelAction.Label);
            Assert.AreEqual(new Uri("tel://uri"), cancelAction.Url);
        }

        [TestMethod]
        public void Convert_TemplateMessageWithConfirmTemplateAndTextIsNull_ThrowsException()
        {
            TemplateMessage message = new TemplateMessage()
            {
                AlternativeText = "Alternative",
                Template = new ConfirmTemplate()
                {
                    OkAction = new MessageAction()
                    {
                        Label = "PostbackLabel",
                        Text = "PostbackText"
                    },
                    CancelAction = new UriAction()
                    {
                        Label = "PostbackLabel",
                        Url = new Uri("http://foo.bar")
                    }
                }
            };

            ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { message });
            });
        }

        [TestMethod]
        public void Convert_TemplateMessageWithConfirmTemplateAndOkActionIsNull_ThrowsException()
        {
            TemplateMessage message = new TemplateMessage()
            {
                AlternativeText = "Alternative",
                Template = new ConfirmTemplate()
                {
                    Text = "ConfirmText",
                    CancelAction = new UriAction()
                    {
                        Label = "PostbackLabel",
                        Url = new Uri("http://foo.bar")
                    }
                }
            };

            ExceptionAssert.Throws<InvalidOperationException>("The ok action cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { message });
            });
        }

        [TestMethod]
        public void Convert_TemplateMessageWithConfirmTemplateAndCancelActionIsNull_ThrowsException()
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
                    }
                }
            };

            ExceptionAssert.Throws<InvalidOperationException>("The cancel action cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { message });
            });
        }

        [TestMethod]
        public void Convert_TemplateMessageWithConfirmTemplateAndMessageActionLabelIsNull_ThrowsException()
        {
            TemplateMessage message = new TemplateMessage()
            {
                AlternativeText = "Alternative",
                Template = new ConfirmTemplate()
                {
                    Text = "ConfirmText",
                    OkAction = new MessageAction()
                    {
                        Text = "PostbackText"
                    },
                    CancelAction = new UriAction()
                    {
                        Label = "PostbackLabel",
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
        public void Convert_TemplateMessageWithConfirmTemplateAndMessageActionTextIsNull_ThrowsException()
        {
            TemplateMessage message = new TemplateMessage()
            {
                AlternativeText = "Alternative",
                Template = new ConfirmTemplate()
                {
                    Text = "ConfirmText",
                    OkAction = new MessageAction()
                    {
                        Label = "PostbackLabel"
                    },
                    CancelAction = new UriAction()
                    {
                        Label = "PostbackLabel",
                        Url = new Uri("http://foo.bar")
                    }
                }
            };

            ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { message });
            });
        }

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
        public void Convert_TemplateMessageWithCarouselTemplate_InstanceIsPreserved()
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
                            Text = "CarouselText",
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

            ISendMessage[] messages = MessageConverter.Convert(new ISendMessage[] { message });

            Assert.AreEqual(1, messages.Length);

            ITemplateMessage templateMessage = messages[0] as ITemplateMessage;
            Assert.AreEqual(message, templateMessage);

            ICarouselTemplate buttonsTemplate = templateMessage.Template as ICarouselTemplate;
            Assert.AreEqual(message.Template, buttonsTemplate);

            ICarouselColumn column = buttonsTemplate.Columns.First();
            Assert.AreEqual(column, ((CarouselTemplate)message.Template).Columns.First());
        }

        [TestMethod]
        public void Convert_TemplateMessageWithCustomICarouselTemplate_ConvertedToCarouselTemplate()
        {
            TestTemplateMessage message = new TestTemplateMessage()
            {
                Template = new TestCarouselTemplate()
            };

            ISendMessage[] messages = MessageConverter.Convert(new ISendMessage[] { message });

            Assert.AreEqual(1, messages.Length);
            Assert.AreNotEqual(message, messages[0]);

            TemplateMessage templateMessage = messages[0] as TemplateMessage;
            Assert.AreEqual("AlternativeText", templateMessage.AlternativeText);

            CarouselTemplate template = templateMessage.Template as CarouselTemplate;

            CarouselColumn column = template.Columns.First() as CarouselColumn;
            Assert.AreEqual(new Uri("https://carousel.url"), column.ThumbnailUrl);
            Assert.AreEqual("CarouselTitle", column.Title);
            Assert.AreEqual("CarouselText", column.Text);

            ITemplateAction[] actions = column.Actions.ToArray();

            MessageAction action = actions[0] as MessageAction;
            Assert.AreEqual("MessageLabel", action.Label);
            Assert.AreEqual("MessageText", action.Text);
        }

        [TestMethod]
        public void Convert_TemplateMessageWithCarouselTemplateAndColumnsIsNull_ThrowsException()
        {
            TemplateMessage message = new TemplateMessage()
            {
                AlternativeText = "Alternative",
                Template = new CarouselTemplate()
            };

            ExceptionAssert.Throws<InvalidOperationException>("The columns cannot be null.", () =>
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

        [TestMethod]
        public void Convert_TemplateMessageInvalidTemplateType_ThrowsException()
        {
            TestTemplateMessage message = new TestTemplateMessage()
            {
                Template = new InvalidTemplate()
            };

            ExceptionAssert.Throws<NotSupportedException>("Invalid template type.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { message });
            });
        }

        [ExcludeFromCodeCoverage]
        private class TestTemplateMessage : ITemplateMessage
        {
            public string AlternativeText => "AlternativeText";

            public ITemplate Template { get; set; }
        }

        [ExcludeFromCodeCoverage]
        private class TestButtonsTemplate : IButtonsTemplate
        {
            public Uri ThumbnailUrl => new Uri("https://bar.foo");

            public string Title => "ButtonsTitle";

            public string Text => "ButtonsText";

            public IEnumerable<ITemplateAction> Actions => new ITemplateAction[] { new TestPostbackAction() };
        }

        [ExcludeFromCodeCoverage]
        private class TestConfirmTemplate : IConfirmTemplate
        {
            public string Text => "ConfirmText";

            public ITemplateAction OkAction => new TestMessageAction();

            public ITemplateAction CancelAction => new TestUriAction();
        }

        [ExcludeFromCodeCoverage]
        private class TestCarouselTemplate : ICarouselTemplate
        {
            public IEnumerable<ICarouselColumn> Columns => new ICarouselColumn[] { new TestCarouselColumn() };
        }

        [ExcludeFromCodeCoverage]
        private class TestCarouselColumn : ICarouselColumn
        {
            public Uri ThumbnailUrl => new Uri("https://carousel.url");

            public string Title => "CarouselTitle";

            public string Text => "CarouselText";

            public IEnumerable<ITemplateAction> Actions => new ITemplateAction[] { new TestMessageAction() };
        }

        [ExcludeFromCodeCoverage]
        private class TestPostbackAction : IPostbackAction
        {
            public string Label => "PostbackLabel";

            public string Data => "PostbackData";

            public string Text => "PostbackText";
        }

        [ExcludeFromCodeCoverage]
        private class TestMessageAction : IMessageAction
        {
            public string Label => "MessageLabel";

            public string Text => "MessageText";
        }

        [ExcludeFromCodeCoverage]
        private class TestUriAction : IUriAction
        {
            public string Label => "UriLabel";

            public Uri Url => new Uri("tel://uri");
        }

        [ExcludeFromCodeCoverage]
        private class InvalidTemplate : ITemplate
        {
        }
    }
}
