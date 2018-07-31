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

using System.Net.Http;
using System.Threading.Tasks;
using Line.RichMenu;

namespace Line
{
    /// <summary>
    /// Encapsulates the bot that can be used to communicatie with the Line API.
    /// </summary>
    public partial class LineBot : ILineBot
    {
        /// <summary>
        /// Gets a rich menu via a rich menu ID.
        /// </summary>
        /// <param name="richMenuId">ID of an uploaded rich menu.</param>
        /// <returns>.</returns>
        public async Task<RichMenuResponse> GetRichMenu(string richMenuId)
        {
            Guard.NotNullOrEmpty(nameof(richMenuId), richMenuId);

            HttpResponseMessage response = await _client.PostAsync($"richmenu/" + richMenuId, null);
            await response.CheckResult();

            return await response.Content.DeserializeObject<RichMenuResponse>();
        }

        /// <summary>
        /// Creates a rich menu.
        /// </summary>
        /// <param name="richMenu">The rich menu represented as a rich menu object.</param>
        /// <returns>.</returns>
        public async Task<string> CreateRichMenu(RichMenu.RichMenu richMenu)
        {
            Guard.NotNull(nameof(richMenu), richMenu);

            StringContent content = CreateStringContent(richMenu);

            HttpResponseMessage response = await _client.PostAsync($"richmenu", content);
            await response.CheckResult();

            return await response.Content.DeserializeObject<string>();
        }

        /// <summary>
        /// Deletes a rich menu.
        /// </summary>
        /// <param name="richMenuId">ID of an uploaded rich menu.</param>
        public async void DeleteRichMenu(string richMenuId)
        {
            Guard.NotNullOrEmpty(nameof(richMenuId), richMenuId);

            HttpResponseMessage response = await _client.DeleteAsync($"richmenu/" + richMenuId);
            await response.CheckResult();
        }

        /// <summary>
        /// Gets the ID of the rich menu linked to a user.
        /// </summary>
        /// <param name="userId">User ID. Found in the source object of webhook event objects. Do not use the LINE ID used in the LINE app.</param>
        /// <returns>.</returns>
        public async Task<string> GetRichMenuIdByUserId(string userId)
        {
            Guard.NotNullOrEmpty(nameof(userId), userId);

            HttpResponseMessage response = await _client.PostAsync($"user/" + userId + "/richmenu", null);
            await response.CheckResult();

            return await response.Content.DeserializeObject<string>();
        }

        /// <summary>
        /// Links a rich menu to a user. Only one rich menu can be linked to a user at one time.
        /// </summary>
        /// <param name="richMenuId">ID of an uploaded rich menu.</param>
        /// <param name="userId">User ID. Found in the source object of webhook event objects. Do not use the LINE ID used in the LINE app.</param>
        public async void LinkRichMenuToUser(string richMenuId, string userId)
        {
            Guard.NotNullOrEmpty(nameof(richMenuId), richMenuId);
            Guard.NotNullOrEmpty(nameof(userId), userId);

            HttpResponseMessage response = await _client.PostAsync($"user/" + userId + "/richmenu/" + richMenuId, null);
            await response.CheckResult();
        }

        /// <summary>
        /// Unlinks a rich menu from a user.
        /// </summary>
        /// <param name="userId">User ID. Found in the source object of webhook event objects. Do not use the LINE ID used in the LINE app.</param>
        public async void LinkRichMenuToUser(string userId)
        {
            Guard.NotNullOrEmpty(nameof(userId), userId);

            HttpResponseMessage response = await _client.DeleteAsync($"user/" + userId + "/richmenu/");
            await response.CheckResult();
        }

        /// <summary>
        /// Downloads an image associated with a rich menu.
        /// </summary>
        /// <param name="richMenuId">ID of the rich menu with the image to be downloaded.</param>
        /// <returns>.</returns>
        public async Task<byte[]> DownloadRichMenuImage(string richMenuId)
        {
            Guard.NotNullOrEmpty(nameof(richMenuId), richMenuId);

            HttpResponseMessage response = await _client.GetAsync($"richmenu/" + richMenuId + "/content");
            await response.CheckResult();

            return await response.Content.DeserializeObject<byte[]>();
        }

        /// <summary>
        /// Uploads and attaches an image to a rich menu.
        /// </summary>
        /// <param name="richMenuId">The target rich menu id.</param>
        /// <param name="richMenuImage">The rich menu Image.</param>
        public async void UploadRichMenuImage(string richMenuId, byte[] richMenuImage)
        {
            Guard.NotNullOrEmpty(nameof(richMenuId), richMenuId);
            Guard.NotNull(nameof(richMenuImage), richMenuImage);

            var content = CreateImageContent(richMenuImage);
            HttpResponseMessage response = await _client.PostAsync($"richmenu/" + richMenuId + "/content", content);
            await response.CheckResult();
        }

        /// <summary>
        /// Gets a list of all uploaded rich menus.
        /// </summary>
        /// <returns>.</returns>
        public async Task<RichMenuResponse[]> GetRichMenuList()
        {
            HttpResponseMessage response = await _client.GetAsync($"richmenu/list");
            await response.CheckResult();
            var result = await response.Content.DeserializeObject<RichMenuResponse[]>();

            return result;
        }
    }
}
