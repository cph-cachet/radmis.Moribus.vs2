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

using Xamarin.Forms;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

using Sensus.DataStores;

using System.Reflection;
using Xamarin.Forms.Xaml;
using Sensus.MUBS;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Sensus.UI
{
    public class App : Application
    {
        static RegistrationDatabase database;
        public App()
        {
            //MainPage = new SensusMasterDetailPage();
            //MainPage = new MUBS_tabbed();
            MainPage = new AmbitionPage();
        }

        public static RegistrationDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new RegistrationDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("RegSQLite.db3"));
                }
                return database;
            }
        }

        protected override void OnStart()
        {
            base.OnStart();

            AppCenter.Start("ios=" + SensusServiceHelper.APP_CENTER_KEY_IOS + ";" + 
                            "android=" + SensusServiceHelper.APP_CENTER_KEY_ANDROID, 
                            typeof(Analytics), 
                            typeof(Crashes));
        }
    }
}