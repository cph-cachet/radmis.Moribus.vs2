// Copyright 2014 The Rector & Visitors of the University of Virginia
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System;
using System.Threading.Tasks;

namespace Sensus.DataStores.Remote
{
    /// <summary>
    /// When using the Console Remote Data Store, all data accumulated in <see cref="Local.LocalDataStore"/> are simply written to the logging console. This 
    /// is useful for debugging purposes and is not recommended for practical Sensus deployments since it provides no means of moving the data 
    /// off of the device. There is, however, one important exception to this: If you wish for the participants to store all data locally 
    /// until the end of the study, then using the Console Remote Data Store makes sense since it will not upload any data to a remote system. To 
    /// make this work, you must disable <see cref="Local.LocalDataStore.UploadToRemoteDataStore"/>.
    /// </summary>
    public class ConsoleRemoteDataStore : RemoteDataStore
    {
        [JsonIgnore]
        public override string DisplayName
        {
            get { return "Console"; }
        }

        [JsonIgnore]
        public override bool CanRetrieveCommittedData
        {
            get
            {
                return false;
            }
        }

        [JsonIgnore]
        public override bool Clearable
        {
            get { return false; }
        }

        protected override Task<List<Datum>> CommitAsync(IEnumerable<Datum> data, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                List<Datum> committedData = new List<Datum>();

                foreach (Datum datum in data)
                {
                    if (cancellationToken.IsCancellationRequested)
                        break;

                    SensusServiceHelper.Get().Logger.Log("Committed datum to remote console:  " + datum, LoggingLevel.Debug, GetType());
                    committedData.Add(datum);
                }

                return committedData;
            });
        }

        public override string GetDatumKey(Datum datum)
        {
            throw new Exception("Cannot retrieve datum key from Console Remote Data Store.");
        }

        public override Task<T> GetDatum<T>(string datumKey, CancellationToken cancellationToken)
        {
            throw new Exception("Cannot retrieve datum from Console Remote Data Store.");
        }
    }
}