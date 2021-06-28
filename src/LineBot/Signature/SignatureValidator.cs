// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

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

            // It is no secret which hashing method is used to validate the signature so we can do a quick exit here.
            if (expectedHash.Length != SignatureLength)
                return false;

            using (var hmac = new HMACSHA256(_key))
            {
                var hash = hmac.ComputeHash(content);

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