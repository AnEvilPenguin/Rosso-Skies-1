using System.IO;

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