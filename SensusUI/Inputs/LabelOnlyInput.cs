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
using Xamarin.Forms;

namespace SensusUI.Inputs
{
    public class LabelOnlyInput : Input
    {
        public override object Value
        {
            get
            {
                return null;
            }
        }

        public override bool Enabled
        {
            get
            {
                return true;
            }
            set
            {
            }
        }

        public override string DefaultName
        {
            get
            {
                return "Read-only Text";
            }
        }

        public LabelOnlyInput()
        {
            Construct(true);
        }

        public LabelOnlyInput(string labelText)
            : base(labelText)
        {
            Construct(true);
        }

        public LabelOnlyInput(string labelText, int labelFontSize)
            : base(labelText, labelFontSize)
        {
            Construct(true);
        }

        public LabelOnlyInput(string labelText, bool complete)
            : base(labelText)
        {
            Construct(complete);
        }

        public LabelOnlyInput(string name, string labelText)
            : base(name, labelText)
        {
            Construct(true);
        }

        private void Construct(bool complete)
        {
            Complete = complete;
            Required = false;
            DisplayNumber = false;
            ShouldBeStored = false;
        }

        public override View GetView(int index)
        {
            if (base.GetView(index) == null)
                base.SetView(CreateLabel(-1));

            return base.GetView(index);
        }
    }
}