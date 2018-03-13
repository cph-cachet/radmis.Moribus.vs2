using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using Xamarin.Forms;
using Sensus;

[assembly: Dependency(typeof(AndroidFileHelper))]
namespace Sensus
{
    public class AndroidFileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}