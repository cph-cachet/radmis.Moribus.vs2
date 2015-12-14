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
using System.Collections.Generic;
using SensusService;
using SensusService.Probes;
using System.IO;
using Newtonsoft.Json;
using System.Threading;
using SensusService.Exceptions;
using SensusUI.Inputs;
using System.Linq;
using System.Globalization;
using SensusUI.UiProperties;

namespace SensusUI.Inputs
{
    public class ItemPickerPageInput : Input
    {
        /*private class TextColorValueConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object selectedItems, CultureInfo culture)
            {
                if (value == null)
                    return Color.Gray;

                return (selectedItems as List<object>).Contains(value) ? Color.Accent : Color.Gray;
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new SensusException("Invalid call to " + GetType().FullName + ".ConvertBack.");
            }
        }*/

        private List<object> _items;
        private bool _multiselect;
        private List<object> _selectedItems;
        private string _textBindingPropertyPath;
        private List<Label> _itemLabels;

        public List<object> Items
        {
            get
            {
                return _items;
            }
        }

        [EditableListUiProperty("Items:", true, 10)]
        public List<string> StringItems
        {
            get
            {
                return _items.Select(item => item.ToString()).ToList();
            }
            // need set method so that binding can set the list via the EditableListUiProperty
            set
            {
                _items = value.Cast<object>().ToList();
            }
        }

        public string TextBindingPropertyPath
        {
            get
            {
                return _textBindingPropertyPath;
            }
            set
            {
                _textBindingPropertyPath = value;
            }
        }

        public override object Value
        {
            get
            {
                return _selectedItems;
            }
        }

        [OnOffUiProperty(null, true, 11)]
        public bool Multiselect
        {
            get
            {
                return _multiselect;
            }
            set
            {
                _multiselect = value;
            }
        }

        public override bool Enabled
        {
            get
            {
                return _itemLabels.Count == 0 ? true : _itemLabels[0].IsEnabled;
            }
            set
            {
                foreach (Label itemLabel in _itemLabels)
                    itemLabel.IsEnabled = value;
            }
        }

        public override string DefaultName
        {
            get
            {
                return "Picker (Page)";
            }
        }

        public ItemPickerPageInput()
        {
            Construct();
        }

        public ItemPickerPageInput(string labelText, List<object> items, string textBindingPropertyPath)
            : base(labelText)
        {
            Construct();

            _items = items;

            if (!string.IsNullOrWhiteSpace(textBindingPropertyPath))
                _textBindingPropertyPath = textBindingPropertyPath.Trim();
        }

        private void Construct()
        {
            _items = new List<object>();
            _multiselect = false;
            _selectedItems = new List<object>();
            _textBindingPropertyPath = ".";
            _itemLabels = new List<Label>();
        }

        public override View GetView(int index)
        {
            if (base.GetView(index) == null)
            {       
                _selectedItems.Clear();
                _itemLabels.Clear();

                StackLayout itemLabelStack = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.FillAndExpand                            
                };

                for (int i = 0; i < _items.Count; ++i)
                {
                    object item = _items[i];

                    Label itemLabel = new Label
                    {
                        FontSize = 20,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        BindingContext = item
                                
                        // set the style ID on the view so that we can retrieve it when unit testing
                        #if UNIT_TESTING
                        , StyleId = Name + " " + i;
                        #endif
                    };

                    itemLabel.SetBinding(Label.TextProperty, _textBindingPropertyPath);

                    TapGestureRecognizer tapRecognizer = new TapGestureRecognizer
                    {
                        NumberOfTapsRequired = 1
                    };

                    Color defaultBackgroundColor = itemLabel.BackgroundColor;

                    tapRecognizer.Tapped += (o, e) =>
                    {
                        if (_selectedItems.Contains(item))
                            _selectedItems.Remove(item);
                        else
                            _selectedItems.Add(item);

                        if (!_multiselect)
                            _selectedItems.RemoveAll(selectedItem => selectedItem != item);

                        foreach (Label label in _itemLabels)
                            label.BackgroundColor = _selectedItems.Contains(label.BindingContext) ? Color.Accent : defaultBackgroundColor;

                        Complete = (Value as List<object>).Count > 0;
                    };
                    
                    itemLabel.GestureRecognizers.Add(tapRecognizer);
                    itemLabelStack.Children.Add(itemLabel);
                    _itemLabels.Add(itemLabel);
                }

                base.SetView(new StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        VerticalOptions = LayoutOptions.Start,
                        Children = { CreateLabel(index), itemLabelStack }
                    });
            }

            return base.GetView(index);
        }

        public override string ToString()
        {
            return base.ToString() + " -- " + _items.Count + " Items";
        }
    }
}