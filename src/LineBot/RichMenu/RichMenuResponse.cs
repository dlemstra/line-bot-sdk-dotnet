﻿// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
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
using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Rich menu response object.
    /// </summary>
    internal class RichMenuResponse : RichMenu, IRichMenuResponse
    {
        private string _id;

        [JsonProperty("richMenuId")]
        public string Id
        {
            get => _id;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidOperationException("The rich menu id cannot be null or whitespace.");

                _id = value;
            }
        }

        internal override void Validate()
        {
            base.Validate();

            if (_id == null)
                throw new InvalidOperationException("The rich menu id cannot be null.");
        }
    }
}