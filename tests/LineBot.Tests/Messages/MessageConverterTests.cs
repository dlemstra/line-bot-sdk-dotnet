// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
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

namespace Line.Tests.Messages
{
    [TestClass]
    public class MessageConverterTests
    {
        [TestMethod]
        public void Convert_TooManyMessagesAreNull_ThrowsException()
        {
            ExceptionAssert.Throws<InvalidOperationException>("The maximum number of messages is 5.", () =>
            {
                MessageConverter.Convert(new ISendMessage[6]);
            });
        }

        [TestMethod]
        public void Convert_NullValueInArray_ThrowsException()
        {
            ExceptionAssert.Throws<InvalidOperationException>("The message should not be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[1] { null });
            });
        }

        [TestMethod]
        public void Convert_InvalidType_ThrowsException()
        {
            ExceptionAssert.Throws<NotSupportedException>("Invalid message type.", () =>
            {
                MessageConverter.Convert(new InvalidMessage[1] { new InvalidMessage() });
            });
        }

        [TestMethod]
        public void Convert_TextMessage_InstanceIsPreserved()
        {
            TextMessage messsage = new TextMessage()
            {
                Text = "Test"
            };

            ISendMessage[] messages = MessageConverter.Convert(new ISendMessage[] { messsage });

            Assert.AreEqual(1, messages.Length);
            Assert.AreEqual(messsage, messages[0]);
        }

        [TestMethod]
        public void Convert_TextMessageWithoutText_ThrowsException()
        {
            TextMessage message = new TextMessage();

            ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { message });
            });
        }

        [TestMethod]
        public void Convert_CustomITextMessage_ConvertedToTextMessage()
        {
            TestTextMessage message = new TestTextMessage();

            ISendMessage[] messages = MessageConverter.Convert(new ISendMessage[] { message });

            Assert.AreEqual(1, messages.Length);
            Assert.AreNotEqual(message, messages[0]);

            TextMessage textMessage = messages[0] as TextMessage;
            Assert.AreEqual("TestTextMessage", textMessage.Text);
        }

        [TestMethod]
        public void Convert_ImageMessage_InstanceIsPreserved()
        {
            ImageMessage message = new ImageMessage()
            {
                PreviewUrl = new Uri("https://foo.previewUrl"),
                Url = new Uri("https://foo.url")
            };

            ISendMessage[] messages = MessageConverter.Convert(new ISendMessage[] { message });

            Assert.AreEqual(1, messages.Length);
            Assert.AreEqual(message, messages[0]);
        }

        [TestMethod]
        public void Convert_ImageMessageWithoutUrl_ThrowsException()
        {
            ImageMessage message = new ImageMessage()
            {
                PreviewUrl = new Uri("https://foo.previewUrl")
            };

            ExceptionAssert.Throws<InvalidOperationException>("The url cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { message });
            });
        }

        [TestMethod]
        public void Convert_ImageMessageWithoutPreviewUrl_ThrowsException()
        {
            ImageMessage message = new ImageMessage()
            {
                Url = new Uri("https://foo.url")
            };

            ExceptionAssert.Throws<InvalidOperationException>("The preview url cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { message });
            });
        }

        [TestMethod]
        public void Convert_CustomIImageMessage_ConvertedToImageMessage()
        {
            TestImageMessage message = new TestImageMessage();

            ISendMessage[] messages = MessageConverter.Convert(new ISendMessage[] { message });

            Assert.AreEqual(1, messages.Length);
            Assert.AreNotEqual(message, messages[0]);

            ImageMessage imageMessage = messages[0] as ImageMessage;
            Assert.AreEqual(new Uri("https://foo.url"), imageMessage.Url);
            Assert.AreEqual(new Uri("https://foo.previewUrl"), imageMessage.PreviewUrl);
        }

        [TestMethod]
        public void Convert_VideoMessage_InstanceIsPreserved()
        {
            VideoMessage message = new VideoMessage()
            {
                PreviewUrl = new Uri("https://foo.previewUrl"),
                Url = new Uri("https://foo.url")
            };

            ISendMessage[] messages = MessageConverter.Convert(new ISendMessage[] { message });

            Assert.AreEqual(1, messages.Length);
            Assert.AreEqual(message, messages[0]);
        }

        [TestMethod]
        public void Convert_VideoMessageWithoutUrl_ThrowsException()
        {
            VideoMessage message = new VideoMessage()
            {
                PreviewUrl = new Uri("https://foo.previewUrl")
            };

            ExceptionAssert.Throws<InvalidOperationException>("The url cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { message });
            });
        }

        [TestMethod]
        public void Convert_VideoMessageWithoutPreviewUrl_ThrowsException()
        {
            VideoMessage message = new VideoMessage()
            {
                Url = new Uri("https://foo.url")
            };

            ExceptionAssert.Throws<InvalidOperationException>("The preview url cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { message });
            });
        }

        [TestMethod]
        public void Convert_CustomIVideoMessage_ConvertedToVideoMessage()
        {
            TestVideoMessage message = new TestVideoMessage();

            ISendMessage[] messages = MessageConverter.Convert(new ISendMessage[] { message });

            Assert.AreEqual(1, messages.Length);
            Assert.AreNotEqual(message, messages[0]);

            VideoMessage videoMessage = messages[0] as VideoMessage;
            Assert.AreEqual(new Uri("https://foo.url"), videoMessage.Url);
            Assert.AreEqual(new Uri("https://foo.previewUrl"), videoMessage.PreviewUrl);
        }

        [TestMethod]
        public void Convert_AudioMessage_InstanceIsPreserved()
        {
            AudioMessage message = new AudioMessage()
            {
                Url = new Uri("https://foo.url"),
                Duration = 10000
            };

            ISendMessage[] messages = MessageConverter.Convert(new ISendMessage[] { message });

            Assert.AreEqual(1, messages.Length);
            Assert.AreEqual(message, messages[0]);
        }

        [TestMethod]
        public void Convert_AudioMessageWithoutUrl_ThrowsException()
        {
            AudioMessage message = new AudioMessage()
            {
                Duration = 10000
            };

            ExceptionAssert.Throws<InvalidOperationException>("The url cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { message });
            });
        }

        [TestMethod]
        public void Convert_VideoMessageWithoutDuration_ThrowsException()
        {
            AudioMessage message = new AudioMessage()
            {
                Url = new Uri("https://foo.url")
            };

            ExceptionAssert.Throws<InvalidOperationException>("The duration should be at least 1 millisecond.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { message });
            });
        }

        [TestMethod]
        public void Convert_CustomIAudioMessage_ConvertedToAudioMessage()
        {
            TestAudioMessage message = new TestAudioMessage();

            ISendMessage[] messages = MessageConverter.Convert(new ISendMessage[] { message });

            Assert.AreEqual(1, messages.Length);
            Assert.AreNotEqual(message, messages[0]);

            AudioMessage audioMessage = messages[0] as AudioMessage;
            Assert.AreEqual(new Uri("https://foo.url"), audioMessage.Url);
            Assert.AreEqual(1000, audioMessage.Duration);
        }

        [TestMethod]
        public void Convert_LocationMessage_InstanceIsPreserved()
        {
            LocationMessage message = new LocationMessage()
            {
                Title = "Title",
                Address = "Address"
            };

            ISendMessage[] messages = MessageConverter.Convert(new ISendMessage[] { message });

            Assert.AreEqual(1, messages.Length);
            Assert.AreEqual(message, messages[0]);
        }

        [TestMethod]
        public void Convert_TextMessageWithoutTitle_ThrowsException()
        {
            LocationMessage message = new LocationMessage()
            {
                Address = "Address"
            };

            ExceptionAssert.Throws<InvalidOperationException>("The title cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { message });
            });
        }

        [TestMethod]
        public void Convert_TextMessageWithoutAddress_ThrowsException()
        {
            LocationMessage message = new LocationMessage()
            {
                Title = "Title",
            };

            ExceptionAssert.Throws<InvalidOperationException>("The address cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { message });
            });
        }

        [TestMethod]
        public void Convert_CustomILocationMessage_ConvertedToTextMessage()
        {
            TestLocationMessage message = new TestLocationMessage();

            ISendMessage[] messages = MessageConverter.Convert(new ISendMessage[] { message });

            Assert.AreEqual(1, messages.Length);
            Assert.AreNotEqual(message, messages[0]);

            LocationMessage textMessage = messages[0] as LocationMessage;
            Assert.AreEqual("Title", textMessage.Title);
            Assert.AreEqual("Address", textMessage.Address);
            Assert.AreEqual(53.2014355m, textMessage.Latitude);
            Assert.AreEqual(5.7988737m, textMessage.Longitude);
        }

        [TestMethod]
        public void Convert_StickerMessage_InstanceIsPreserved()
        {
            StickerMessage message = new StickerMessage()
            {
                PackageId = "PackageId",
                StickerId = "StickerId"
            };

            ISendMessage[] messages = MessageConverter.Convert(new ISendMessage[] { message });

            Assert.AreEqual(1, messages.Length);
            Assert.AreEqual(message, messages[0]);
        }

        [TestMethod]
        public void Convert_StickerMessageWithoutPackageId_ThrowsException()
        {
            StickerMessage message = new StickerMessage()
            {
                StickerId = "StickerId"
            };

            ExceptionAssert.Throws<InvalidOperationException>("The package id cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { message });
            });
        }

        [TestMethod]
        public void Convert_StickerMessageWithoutStickerId_ThrowsException()
        {
            StickerMessage message = new StickerMessage()
            {
                PackageId = "PackageId"
            };

            ExceptionAssert.Throws<InvalidOperationException>("The sticker id cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { message });
            });
        }

        [TestMethod]
        public void Convert_CustomIStickerMessage_ConvertedToTextMessage()
        {
            TestStickerMessage message = new TestStickerMessage();

            ISendMessage[] messages = MessageConverter.Convert(new ISendMessage[] { message });

            Assert.AreEqual(1, messages.Length);
            Assert.AreNotEqual(message, messages[0]);

            StickerMessage textMessage = messages[0] as StickerMessage;
            Assert.AreEqual("PackageId", textMessage.PackageId);
            Assert.AreEqual("StickerId", textMessage.StickerId);
        }

        [TestMethod]
        public void Convert_ImagemapMessage_InstanceIsPreserved()
        {
            ImagemapMessage message = new ImagemapMessage()
            {
                BaseUrl = new Uri("https://foo.bar"),
                BaseSize = new ImagemapSize(1040, 1040),
                AlternativeText = "Alternative",
                Actions = new ImagemapAction[]
                {
                    new ImagemapMessageAction("Text", 1, 2, 3, 4),
                    new ImagemapUriAction("https://url.foo", 1, 2, 3, 4),
                }
            };

            ISendMessage[] messages = MessageConverter.Convert(new ISendMessage[] { message });

            Assert.AreEqual(1, messages.Length);

            IImagemapMessage imagemapMessage = messages[0] as IImagemapMessage;
            Assert.AreEqual(message, imagemapMessage);
            Assert.AreEqual(message.BaseSize, imagemapMessage.BaseSize);

            IImagemapAction[] actions = imagemapMessage.Actions.ToArray();
            Assert.AreEqual(message.Actions.First(), actions[0]);

            ImagemapAction action = message.Actions.Skip(1).First();
            Assert.AreEqual(action, actions[1]);
            Assert.AreEqual(action.Area, actions[1].Area);
        }

        [TestMethod]
        public void Convert_ImagemapMessageWithoutBaseUrl_ThrowsException()
        {
            ImagemapMessage message = new ImagemapMessage()
            {
                BaseSize = new ImagemapSize(1040, 1040),
                AlternativeText = "Alternative",
                Actions = new ImagemapAction[]
                {
                    new ImagemapMessageAction("Text", 1, 2, 3, 4),
                }
            };

            ExceptionAssert.Throws<InvalidOperationException>("The base url cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { message });
            });
        }

        [TestMethod]
        public void Convert_ImagemapMessageWithoutBaseSize_ThrowsException()
        {
            ImagemapMessage message = new ImagemapMessage()
            {
                BaseUrl = new Uri("https://foo.bar"),
                AlternativeText = "Alternative",
                Actions = new ImagemapAction[]
                {
                    new ImagemapMessageAction("Text", 1, 2, 3, 4),
                }
            };

            ExceptionAssert.Throws<InvalidOperationException>("The base size cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { message });
            });
        }

        [TestMethod]
        public void Convert_ImagemapMessageWithoutAlternativeText_ThrowsException()
        {
            ImagemapMessage message = new ImagemapMessage()
            {
                BaseUrl = new Uri("https://foo.bar"),
                BaseSize = new ImagemapSize(1040, 1040),
                Actions = new ImagemapAction[]
                {
                    new ImagemapMessageAction("Text", 1, 2, 3, 4),
                }
            };

            ExceptionAssert.Throws<InvalidOperationException>("The alternative text cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { message });
            });
        }

        [TestMethod]
        public void Convert_ImagemapMessageWithoutActions_ThrowsException()
        {
            ImagemapMessage message = new ImagemapMessage()
            {
                BaseUrl = new Uri("https://foo.bar"),
                BaseSize = new ImagemapSize(1040, 1040),
                AlternativeText = "Alternative"
            };

            ExceptionAssert.Throws<InvalidOperationException>("The actions cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { message });
            });
        }

        [TestMethod]
        public void Convert_CustomIImageMapMessage_ConvertedToTextMessage()
        {
            TestImagemapMessage message = new TestImagemapMessage();

            ISendMessage[] messages = MessageConverter.Convert(new ISendMessage[] { message });

            Assert.AreEqual(1, messages.Length);
            Assert.AreNotEqual(message, messages[0]);

            ImagemapMessage imagemapMessage = messages[0] as ImagemapMessage;
            Assert.AreEqual(new Uri("https://foo.url"), imagemapMessage.BaseUrl);
            Assert.AreEqual(1040, imagemapMessage.BaseSize.Width);
            Assert.AreEqual(520, imagemapMessage.BaseSize.Height);
            Assert.AreEqual("Alternative", imagemapMessage.AlternativeText);

            IImagemapAction[] actions = imagemapMessage.Actions.ToArray();

            ImagemapUriAction uriAction = actions[0] as ImagemapUriAction;
            Assert.AreEqual(new Uri("https://foo.bar"), uriAction.Url);
            Assert.AreEqual(4, uriAction.Area.X);
            Assert.AreEqual(3, uriAction.Area.Y);
            Assert.AreEqual(2, uriAction.Area.Width);
            Assert.AreEqual(1, uriAction.Area.Height);

            ImagemapMessageAction messageAction = actions[1] as ImagemapMessageAction;
            Assert.AreEqual("TestImagemapMessageAction", messageAction.Text);
            Assert.AreEqual(4, messageAction.Area.X);
            Assert.AreEqual(3, messageAction.Area.Y);
            Assert.AreEqual(2, messageAction.Area.Width);
            Assert.AreEqual(1, messageAction.Area.Height);
        }

        [TestMethod]
        public void Convert_InvalidAction_ThrowsException()
        {
            TestImagemapMessage message = new TestImagemapMessage()
            {
                Actions = new InvalidAction[] { new InvalidAction() }
            };

            ExceptionAssert.Throws<NotSupportedException>("Invalid action type.", () =>
            {
                MessageConverter.Convert(new ISendMessage[1] { message });
            });
        }

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

        [ExcludeFromCodeCoverage]
        private class TestTextMessage : ITextMessage
        {
            public string Text => nameof(TestTextMessage);
        }

        [ExcludeFromCodeCoverage]
        private class TestImageMessage : IImageMessage
        {
            public Uri Url => new Uri("https://foo.url");

            public Uri PreviewUrl => new Uri("https://foo.previewUrl");
        }

        [ExcludeFromCodeCoverage]
        private class TestVideoMessage : IVideoMessage
        {
            public Uri Url => new Uri("https://foo.url");

            public Uri PreviewUrl => new Uri("https://foo.previewUrl");
        }

        [ExcludeFromCodeCoverage]
        private class TestAudioMessage : IAudioMessage
        {
            public Uri Url => new Uri("https://foo.url");

            public int Duration => 1000;
        }

        [ExcludeFromCodeCoverage]
        private class TestLocationMessage : ILocationMessage
        {
            public string Title => "Title";

            public string Address => "Address";

            public decimal Latitude => 53.2014355m;

            public decimal Longitude => 5.7988737m;
        }

        [ExcludeFromCodeCoverage]
        private class TestStickerMessage : IStickerMessage
        {
            public string PackageId => nameof(PackageId);

            public string StickerId => nameof(StickerId);
        }

        [ExcludeFromCodeCoverage]
        private class TestImagemapMessage : IImagemapMessage
        {
            public TestImagemapMessage()
            {
                Actions = new IImagemapAction[]
                {
                    new TestImagemapUriAction(),
                    new TestImagemapMessageAction()
                };
            }

            public Uri BaseUrl => new Uri("https://foo.url");

            public string AlternativeText => "Alternative";

            public IImagemapSize BaseSize => new TestImageMapSize();

            public IEnumerable<IImagemapAction> Actions { get; set; }
        }

        [ExcludeFromCodeCoverage]
        private class TestImageMapSize : IImagemapSize
        {
            public int Width => 1040;

            public int Height => 520;
        }

        [ExcludeFromCodeCoverage]
        private class TestImagemapUriAction : IImagemapUriAction
        {
            public TestImagemapUriAction()
            {
                Area = new TestImagemapArea();
                Url = new Uri("https://foo.bar");
            }

            public Uri Url { get; set; }

            public IImagemapArea Area { get; set; }
        }

        [ExcludeFromCodeCoverage]
        private class TestImagemapMessageAction : IImagemapMessageAction
        {
            public TestImagemapMessageAction()
            {
                Text = nameof(TestImagemapMessageAction);
            }

            public string Text { get; set; }

            public IImagemapArea Area => new TestImagemapArea();
        }

        [ExcludeFromCodeCoverage]
        private class TestImagemapArea : IImagemapArea
        {
            public TestImagemapArea()
            {
                Width = 2;
                Height = 1;
            }

            public int X => 4;

            public int Y => 3;

            public int Width { get; set; }

            public int Height { get; set; }
        }

        [ExcludeFromCodeCoverage]
        private class InvalidMessage : ISendMessage
        {
        }

        [ExcludeFromCodeCoverage]
        private class InvalidAction : IImagemapAction
        {
            public IImagemapArea Area => throw new NotImplementedException();
        }
    }
}
