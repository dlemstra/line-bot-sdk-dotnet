// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

namespace Line.Tests
{
    public partial class ISendMessageExtensionsTests
    {
        private class InvalidMessage : ISendMessage
        {
            MessageType ISendMessage.Type
                => (MessageType)42;

            public void Validate()
            {
            }

            void ISendMessage.Validate()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
