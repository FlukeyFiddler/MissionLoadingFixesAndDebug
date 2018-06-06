using System;
using BattleTech;
using Harmony;
using nl.flukeyfiddler.bt.TurnDirectorBugFixes.Utils;

namespace nl.flukeyfiddler.bt.TurnDirectorBugFixes
{
    
    [HarmonyPatch(typeof(TurnDirector), "Load")]
    public class TurnDirector_Load_Patch
    {
        public static void Postfix()
        {
            Logger.Line("sdfasf");  
        }
    }
    
}
