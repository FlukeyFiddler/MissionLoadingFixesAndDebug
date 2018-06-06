using Harmony;
using nl.flukeyfiddler.bt.TurnDirectorBugFixes.Util;
using nl.flukeyfiddler.bt.Utils.Logger;
using System.IO;
using System.Reflection;

namespace nl.flukeyfiddler.bt.TurnDirectorBugFixes
{
    public class TurnDirectorBugFixes
    {
        public static void Init(string modDirectory, string settingsJSON)
        {
            var harmony = HarmonyInstance.Create("nl.flukeyfiddler.bt.TurnDirectorBugFixes");
            harmony.PatchAll(Assembly.GetExecutingAssembly());


            Logger.SetLogFilePath(new LogFilePath(Path.Combine(modDirectory, "Log.txt")));
            Logger.GameStarted();
        }
    }
}
