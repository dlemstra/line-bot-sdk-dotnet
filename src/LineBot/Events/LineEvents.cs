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
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Line
{
    internal sealed class LineEvents : ILineEvents
    {
        private string _destination = null;
        private ILineEvent[] _lineEvents = null;

        public LineEvents(ILineEvent[] lineEvents = null, string destination = null)
        {
            _lineEvents = lineEvents;
            _destination = null;
        }

        [JsonProperty("destination")]
        string ILineEvents.Destination => _destination;

        [JsonProperty("events")]
        ILineEvent[] ILineEvents.Events => _lineEvents;

        public IEnumerator<ILineEvent> GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new LineEventEnum(_lineEvents);
        }

        internal static ILineEvents Empty()
        {
            return new LineEvents(new ILineEvent[0]);
        }
    }
}