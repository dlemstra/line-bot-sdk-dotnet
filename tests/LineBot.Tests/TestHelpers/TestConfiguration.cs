// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

namespace Line.Tests
{
    public sealed class TestConfiguration : ILineConfiguration
    {
        private TestConfiguration()
        {
        }

        public string ChannelAccessToken => "ChannelAccessToken";

        public string ChannelSecret => "ChannelSecret";

        public static ILineConfiguration Create()
        {
            return new TestConfiguration();
        }

        public static ILineBot CreateBot()
        {
            return new LineBot(new TestConfiguration(), TestHttpClient.Create(), new EmptyLineBotLogger());
        }

        public static ILineBot CreateBot(TestHttpClient httpClient)
        {
            return new LineBot(new TestConfiguration(), httpClient, new EmptyLineBotLogger());
        }

        public static ILineBot CreateBot(ILineBotLogger logger)
        {
            return new LineBot(new TestConfiguration(), TestHttpClient.Create(), logger);
        }
    }
}
