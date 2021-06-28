// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

namespace Line.Tests
{
    public class TestMessage : IMessage
    {
        public TestMessage()
            : this(MessageType.Text)
        {
        }

        public TestMessage(MessageType messageType)
        {
            MessageType = messageType;
        }

        public string Id => "testMessage";

        public ILocation Location { get; }

        public MessageType MessageType { get; }

        public string ReplyToken => "testReplyToken";

        public ISticker Sticker { get; }

        public string Text => "testText";
    }
}
