// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Newtonsoft.Json;

namespace Line
{
    internal sealed class ReplyMessage
    {
        public ReplyMessage(string replyToken, ISendMessage[] messages)
        {
            messages.Validate();

            ReplyToken = replyToken;
            Messages = messages;
        }

        [JsonProperty("replyToken")]
        public string ReplyToken { get; }

        [JsonProperty("messages")]
        public ISendMessage[] Messages { get; }
    }
}
