﻿// Copyright 2014 The Rector & Visitors of the University of Virginia
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

using System;
using Com.Empatica.Empalink;
using Com.Empatica.Empalink.Delegates;
using SensusUI.UiProperties;
using SensusService.Probes.User.Empatica;

namespace Sensus.Android.Probes.User.Empatica
{
    public class AndroidEmpaticaWristbandProbe : EmpaticaWristbandProbe
    {
        private AndroidEmpaticaWristbandListener _listener;

        public AndroidEmpaticaWristbandProbe()
        {
        }

        protected override void Initialize()
        {
            base.Initialize();

            _listener = new AndroidEmpaticaWristbandListener();
        }

        protected override void AuthenticateAsync(Action<Exception> callback)
        {
            _listener.AuthenticateAsync(EmpaticaKey, callback);
        }

        public override void DiscoverAndConnectDevices()
        {
            
        }

        protected override void DisconnectDevices()
        {
            
        }
    }
}