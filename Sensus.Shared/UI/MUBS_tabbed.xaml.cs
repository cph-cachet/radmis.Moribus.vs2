using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sensus.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MUBS_tabbed : TabbedPage
    {
        public MUBS_tabbed ()
        {
            InitializeComponent();
        }
    }
}