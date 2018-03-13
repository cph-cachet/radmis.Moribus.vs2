using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Sensus
{
    public interface ILocale
    {
        CultureInfo GetCurrent();

        void SetLocale(CultureInfo ci);
    }
}
