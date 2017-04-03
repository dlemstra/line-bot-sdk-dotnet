// <copyright file="LineBot.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.
// </copyright>

using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Encapsulates the bot that can be used to communicatie with the Line API.
    /// </summary>
    public sealed class LineBot : ILineBot
    {
        private readonly HttpClient _client;
        private readonly ILineConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="LineBot"/> class.
        /// </summary>
        /// <param name="configuration">The configuration for the client.</param>
        public LineBot(ILineConfiguration configuration)
            : this(configuration, HttpClientFactory.Create(configuration))
        {
        }

        internal LineBot(ILineConfiguration configuration, HttpClient client)
        {
            _configuration = configuration;
            _client = client;
        }

        /// <summary>
        /// Returns the profile for the specified user.
        /// </summary>
        /// <param name="userId">The id of the user</param>
        /// <returns>The profile for the specified user.</returns>
        public async Task<IUserProfile> GetProfile(string userId)
        {
            Guard.NotNullOrEmpty(nameof(userId), userId);

            HttpResponseMessage response = await _client.GetAsync($"profile/{userId}");
            response.CheckResult();

            string body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserProfile>(body);
        }
    }
}
