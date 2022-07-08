using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace Bubbles.Utils
{
    internal class Resource
    {

        public static string Read(string path)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Bubbles." + path;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                return result;
            }
        }
    }
}
