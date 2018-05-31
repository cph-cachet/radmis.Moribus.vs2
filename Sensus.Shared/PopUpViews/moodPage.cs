using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Sensus.Model_data;
using Sensus.DataStores;
using Sensus.UI;


namespace Sensus.PopUpViews
{
	
	public partial class moodPage : ContentPage
	{
        
        public List<lystmest> scoreSelections { get; set; }
        private DateTime curDate;
        public ListView lstView;
        private Registration selectedMood;
        public int selectedScore;
        
        public int newestID;


        public DatePicker overviewDate;


        public moodPage()
        {
            Title = "Select Mood";
            ToolbarItems.Add(new ToolbarItem("mytitle", "plus.png", async () =>
            {
                await DisplayAlert("working", "how are you", "exit");
            }
           ));

            curDate = DateTime.Today;
            StackLayout myMood = new StackLayout();

           // _db = new Database(DependencyService.Get<ISQLite>().GetLocalFilePath("IBA.db3"));
            
            


            scoreSelections = new List<lystmest>();
          
            //CultureInfo culture = new CultureInfo("da-DK");
            //System.Threading.Thread.CurrentThread.CurrentCulture = culture;

            

            overviewDate = new DatePicker
            {
                Format = "D",
                HorizontalOptions = LayoutOptions.Start,
                MaximumDate = curDate,
                MinimumDate = curDate.AddDays(-7)
            };


            lstView = new ListView();

            lstView.ItemTemplate = new DataTemplate(typeof(ScoringCell_ViewCell));

            scoreSelections.Add(new lystmest { NameScore = AppResources.Mood1, ImageScore = "godt_.png", ScoreLabel = "+0.5" });
            scoreSelections.Add(new lystmest { NameScore = AppResources.Mood2, ImageScore = "ok_.png", ScoreLabel = "0" });
            scoreSelections.Add(new lystmest { NameScore = AppResources.Mood3, ImageScore = "darligt_.png", ScoreLabel = "-1" });
            scoreSelections.Add(new lystmest { NameScore = AppResources.Mood4, ImageScore = "meget_darligt_.png", ScoreLabel = "-2" });
            scoreSelections.Add(new lystmest { NameScore = AppResources.Mood5, ImageScore = "ekstremt_darligt_.png", ScoreLabel = "-3" });
            scoreSelections.Add(new lystmest { NameScore = "\u269C", ImageScore = "ekstremt_darligt_.png", ScoreLabel = "-3" });

            lstView.ItemsSource = scoreSelections;
            lstView.ItemSelected += LstView_ItemSelected;
            lstView.VerticalOptions = LayoutOptions.FillAndExpand;
            // Accomodate iPhone status bar.
            //his.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 20, 5);

            int margIt;
            switch (Device.RuntimePlatform)
            {
            case Device.iOS:
                margIt = 20; break;
            default:
                margIt = 0; break;
            };
            this.Padding = new Thickness(10, margIt, 20, 5);

            myMood.Children.Add(overviewDate);
            myMood.Children.Add(lstView);

            Content = myMood;
            


        }

        async void LstView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                selectedMood = new Registration();
                selectedMood.didDate = curDate;
                selectedMood.didSocial = true;
                selectedScore = (lstView.ItemsSource as List<lystmest>).IndexOf(e.SelectedItem as lystmest);
                selectedMood.didMood = selectedScore;
                selectedMood.didEdit = curDate;

                await App.Database.SaveItemAsync(selectedMood);
                /*
                //The previous event is:
                selectedMood = _db.FindDayActivity_mood(overviewDate.Date);
                if (selectedMood == null)
                { 
                    selectedMood = new Mood();
                    selectedMood.From = overviewDate.Date;
                }
                selectedScore = (lstView.ItemsSource as List<lystmestring>).IndexOf(e.SelectedItem as lystmestring);
                int finalscore;
                if (selectedScore == 0)
                    finalscore = 1; // this should be 0.5, but then we need to make the variable in database as DOUBLE
                else
                    finalscore = 1 - selectedScore;

                selectedMood.value = finalscore;
               

                
                newestID = _db.UpdateActivity_mood(selectedMood);

                
                // Then navigate to the statistics page:
                var mdp = Application.Current.MainPage as MasterDetailPage;
                Page1 myStats = new Page1();
                mdp.Detail = new NavigationPage(myStats);
                //await mdp.Detail.Navigation.PushAsync(new Page1());

                // scroll to buttom:

                myStats.ScrollToMood();
                */


            }
        }

	

       
    }
}
