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
using System.Security.Cryptography;
using System.Text;

namespace Line
{
    internal sealed class SignatureValidator
    {
        private const int SignatureLength = 32;
        private readonly byte[] _key;

        public SignatureValidator(ILineConfiguration configuration)
        {
            Guard.NotNull(nameof(configuration), configuration);

            _key = Encoding.UTF8.GetBytes(configuration.ChannelSecret);
        }

        public void Validate(byte[] content, string signature)
        {
            Guard.NotNullOrEmpty(nameof(content), content);
            Guard.NotNullOrEmpty(nameof(signature), signature);

            if (!IsValidHash(content, signature))
                throw new LineBotException($"Invalid signature.");
        }

        private bool IsValidHash(byte[] content, string signature)
        {
            byte[] expectedHash;

            try
            {
                expectedHash = Convert.FromBase64String(signature);
            }
            catch (FormatException)
            {
                return false;
            }

            // It is no secret wich hashing method is used to validate the signature so we can do a quick exit here.
            if (expectedHash.Length != SignatureLength)
            {
                return false;
            }

            using (HMACSHA256 hmac = new HMACSHA256(_key))
            {
                byte[] hash = hmac.ComputeHash(content);

                int result = 0;

                for (int i = 0; i < SignatureLength; i++)
                {
                    result |= hash[i] ^ expectedHash[i];
                }

                return result == 0;
            }
        }
    }
}