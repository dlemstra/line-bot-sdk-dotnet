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

namespace Line
{
    /// <summary>
    /// Encapsulates the interface for a beacon.
    /// </summary>
    public interface IBeacon : IReplyToken
    {
        /// <summary>
        /// Gets the type of the beacon.
        /// </summary>
        BeaconType BeaconType { get; }

        /// <summary>
        /// Gets the hardware ID of the beacon that was detected.
        /// </summary>
        string Hwid { get; }
    }
}
