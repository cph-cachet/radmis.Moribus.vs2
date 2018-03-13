using System;
using Xamarin.Forms;
using System.Threading;
using System.Globalization;

[assembly:Dependency(typeof(Sensus.Locale_Android))]

namespace Sensus
{
	public class Locale_Android : ILocale
    {
		public void SetLocale (CultureInfo ci){
            
			Thread.CurrentThread.CurrentCulture = ci;
			Thread.CurrentThread.CurrentUICulture = ci;

            
		}

		/// <remarks>
		/// Not sure if we can cache this info rather than querying every time
		/// </remarks>
		public CultureInfo GetCurrent() 
		{
            
			var androidLocale = Java.Util.Locale.Default; // user's preferred locale

			// en, es, ja
			var netLanguage = AndroidToLanguage(androidLocale.Language.Replace ("_", "-")); 
		

	

			var ci = new System.Globalization.CultureInfo (netLanguage);
			Thread.CurrentThread.CurrentCulture = ci;
			Thread.CurrentThread.CurrentUICulture = ci;


			return ci;
		}

        string AndroidToLanguage(string androidlanguage)
        {
            var netLanguage = androidlanguage;
            switch (androidlanguage)
            {
                case "da":
                    netLanguage = "da";
                    break;
                default:
                    netLanguage = "en";
                    break;
            }
            return netLanguage;
        }
	}
}

