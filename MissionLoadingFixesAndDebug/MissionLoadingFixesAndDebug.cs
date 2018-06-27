using Harmony;
using nl.flukeyfiddler.bt.MissionLoadingFixesAndDebug.Utils;
using nl.flukeyfiddler.bt.Utils.Logger;
using System.IO;
using System.Reflection;

namespace nl.flukeyfiddler.bt.MissionLoadingFixesAndDebug
{
    public class MissionLoadingFixesAndDebug
    {
        public static HarmonyInstance harmony;

        public static void Init(string modDirectory, string settingsJSON)
        {
            harmony = HarmonyInstance.Create("nl.flukeyfiddler.bt.MissionLoadingFixesAndDebug");
            harmony.PatchAll(Assembly.GetExecutingAssembly());


            Logger.SetLogFilePath(new LogFilePath(Path.Combine(modDirectory, "Log.txt")));
            Logger.GameStarted();
        }
    }
}
