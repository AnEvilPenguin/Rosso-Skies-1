using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RossoSkies1.scripts.Util
{
    internal static class Constants
    {
        public static readonly string FolderPath = Path.Combine(
            System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData),
            "EvilPenguinIndustries\\Rosso_Skies"
        );
    }
}