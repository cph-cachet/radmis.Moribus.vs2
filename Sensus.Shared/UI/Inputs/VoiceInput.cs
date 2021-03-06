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

using System;
using Xamarin.Forms;
using Newtonsoft.Json;
using Sensus.Exceptions;
using Sensus.UI.UiProperties;
using System.Threading.Tasks;

namespace Sensus.UI.Inputs
{
    public class VoiceInput : Input, IVariableDefiningInput
    {
        private string _outputMessage;
        private string _outputMessageRerun;
        private string _response;
        private bool _enabled;
        private string _definedVariable;

        /// <summary>
        /// Message to generate speech for when displaying this input.
        /// </summary>
        /// <value>The output message.</value>
        [EntryStringUiProperty("Output Message:", true, 11)]
        public string OutputMessage
        {
            get { return _outputMessage; }
            set { _outputMessage = value; }
        }

        /// <summary>
        /// Message to generate speech for when displaying this input for a second time.
        /// </summary>
        /// <value>The output message.</value>
        [EntryStringUiProperty("Output Message Rerun:", true, 12)]
        public string OutputMessageRerun
        {
            get { return _outputMessageRerun; }
            set { _outputMessageRerun = value; }
        }

        /// <summary>
        /// The name of the variable in <see cref="Protocol.VariableValueUiProperty"/> that this input should
        /// define the value for. For example, if you wanted this input to supply the value for a variable
        /// named `study-name`, then set this field to `study-name` and the user's selection will be used as
        /// the value for this variable. 
        /// </summary>
        /// <value>The defined variable.</value>
        [EntryStringUiProperty("Define Variable:", true, 2)]
        public string DefinedVariable
        {
            get
            {
                return _definedVariable;
            }
            set
            {
                _definedVariable = value?.Trim();
            }
        }

        public override object Value
        {
            get
            {
                return _response;
            }
        }

        [JsonIgnore]
        public override bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
            }
        }

        public override string DefaultName
        {
            get
            {
                return "Voice Prompt";
            }
        }

        public VoiceInput()
        {
            Construct("", "");
        }

        public VoiceInput(string outputMessage, string outputMessageRerun)
            : base()
        {
            Construct(outputMessage, outputMessageRerun);
        }

        public VoiceInput(string name, string outputMessage, string outputMessageRerun)
            : base(name, null)
        {
            Construct(outputMessage, outputMessageRerun);
        }

        private void Construct(string outputMessage, string outputMessageRerun)
        {
            _enabled = true;
            _outputMessage = outputMessage;
            _outputMessageRerun = outputMessageRerun;
        }

        public override View GetView(int index)
        {
            return null;
        }

        protected override void SetView(View value)
        {
            SensusException.Report("Cannot set View on VoiceInput.");
        }

        public Task<string> RunAsync(DateTimeOffset? firstRunTimestamp, Action postDisplayCallback)
        {
            return Task.Run(async () =>
            {
                string outputMessage = _outputMessage;

                #region temporal analysis
                if (firstRunTimestamp.HasValue && !string.IsNullOrWhiteSpace(_outputMessageRerun))
                {
                    TimeSpan promptAge = DateTimeOffset.UtcNow - firstRunTimestamp.Value;

                    int daysAgo = (int)promptAge.TotalDays;
                    string daysAgoStr;
                    if (daysAgo == 0)
                    {
                        daysAgoStr = "today";
                    }
                    else if (daysAgo == 1)
                    {
                        daysAgoStr = "yesterday";
                    }
                    else
                    {
                        daysAgoStr = promptAge.TotalDays + " days ago";
                    }

                    outputMessage = string.Format(_outputMessageRerun, daysAgoStr + " at " + firstRunTimestamp.Value.LocalDateTime.ToString("h:mm tt"));
                }
                #endregion

                await SensusServiceHelper.Get().TextToSpeechAsync(outputMessage);

                _response = await SensusServiceHelper.Get().RunVoicePromptAsync(outputMessage, postDisplayCallback);

                Viewed = true;

                if (string.IsNullOrWhiteSpace(_response))
                {
                    _response = null;
                }

                Complete = _response != null;

                return _response;
            });
        }
    }
}