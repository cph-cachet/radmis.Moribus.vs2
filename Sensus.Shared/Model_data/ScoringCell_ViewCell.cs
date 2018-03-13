using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using FFImageLoading.Forms;
namespace Sensus.Model_data
{
    public class ScoringCell_ViewCell : ViewCell
    {
        public ScoringCell_ViewCell()
        {
            // This is our views:
            var image = new CachedImage();
            var nameLabel = new Label();
            var scoreLabel = new Label();
            var boxView = new BoxView();
            var horizontalLayout = new StackLayout();
            var verticalLayout = new StackLayout();

            // Bindings
            image.SetBinding(CachedImage.SourceProperty, new Binding("ImageScore"));
            nameLabel.SetBinding(Label.TextProperty, new Binding("NameScore"));
            scoreLabel.SetBinding(Label.TextProperty, new Binding("ScoreLabel"));

            // Properties
            nameLabel.TextColor = Color.Black;
            nameLabel.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
            scoreLabel.TextColor = Color.Black;
            scoreLabel.FontAttributes = FontAttributes.Bold;
            scoreLabel.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));


            // How the view should be designed:
            horizontalLayout.Orientation = StackOrientation.Horizontal;
            horizontalLayout.HorizontalOptions = LayoutOptions.Fill;
            image.HorizontalOptions = LayoutOptions.Start;
            image.DownsampleToViewSize = true;
            nameLabel.VerticalOptions = LayoutOptions.Center;
            scoreLabel.HorizontalOptions = LayoutOptions.EndAndExpand;
            scoreLabel.VerticalOptions = LayoutOptions.Center;

            // Adding the views to the design:
            horizontalLayout.Children.Add(image);
            horizontalLayout.Children.Add(nameLabel);
            horizontalLayout.Children.Add(scoreLabel);


            // Adding the design to parent view:
            View = horizontalLayout;
        }

    }
}
