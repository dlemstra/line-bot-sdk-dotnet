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

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Line
{
    [JsonObject]
    internal sealed class LineEventCollection : ILineEvents
    {
        [JsonProperty("destination")]
        string ILineEvents.Destination { get; set; }

        [JsonProperty("events")]
        List<ILineEvent> ILineEvents.Events { get; set; }

        public IEnumerator<ILineEvent> GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (this as ILineEvents).GetEnumerator();
        }

        internal static ILineEvents Empty()
        {
            return new LineEventCollection();
        }
    }
}