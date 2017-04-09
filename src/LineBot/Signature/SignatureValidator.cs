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
using System.Security.Cryptography;
using System.Text;

namespace Line
{
    internal class SignatureValidator
    {
        private readonly ILineConfiguration _configuration;

        public SignatureValidator(ILineConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Validate(byte[] content, string signature)
        {
            Guard.NotNullOrEmpty(nameof(content), content);
            Guard.NotNullOrEmpty(nameof(signature), signature);

            byte[] key = Encoding.UTF8.GetBytes(_configuration.ChannelSecret);

            using (HMACSHA256 hmac = new HMACSHA256(key))
            {
                byte[] hash = hmac.ComputeHash(content);

                string base64 = Convert.ToBase64String(hash);
                if (signature != base64)
                    throw new LineBotException($"Invalid signature. Expected {base64}, actual value {signature}.");
            }
        }
    }
}