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

namespace Line.Tests
{
    [ExcludeFromCodeCoverage]
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
