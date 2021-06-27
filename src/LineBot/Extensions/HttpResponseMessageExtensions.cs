// Copyright 2017-2019 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
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

using System.Net.Http;
using System.Threading.Tasks;

namespace Line
{
    internal static class HttpResponseMessageExtensions
    {
        public static async Task CheckResult(this HttpResponseMessage self)
        {
            if (self.IsSuccessStatusCode)
                return;

            if (self.Content == null)
                throw new LineBotException(self.StatusCode);

            LineError? error = await self.Content.DeserializeObject<LineError>();

            if (error == null || string.IsNullOrWhiteSpace(error.Message))
                throw new LineBotException(self.StatusCode);

            throw new LineBotException(self.StatusCode, error);
        }
    }
}
