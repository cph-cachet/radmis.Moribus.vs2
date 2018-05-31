using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Sensus
{
	public class AmbitionViewModel
	{

        public ObservableCollection<Ambition> Ambitions { get; set; }
        public AmbitionViewModel ()
		{
            Ambitions = new ObservableCollection<Ambition>
        {
            new Ambition
            {
                Source="physical.png",
                Name = AppResources.physical_amb
            },
                new Ambition
            {
                Source = "work.png",
                Name = AppResources.work_amb
                },
            new Ambition
            {
                Source = "fritid.png",
                Name = AppResources.fritid_amb
            },
            new Ambition
            {
                Source = "sleep.png",
                Name = AppResources.sleep_amb
            },
            new Ambition
            {
                Source = "practical.png",
                Name = AppResources.practical_amb
            },
            new Ambition
            {
                Source = "social.png",
                Name = AppResources.social_amb
            },
            new Ambition
            {
                Source = "healthy.png",
                Name = AppResources.healthy_amb
            }
        };
        }
	}
}