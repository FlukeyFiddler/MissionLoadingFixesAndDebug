using System;
using BattleTech;
using Harmony;
using nl.flukeyfiddler.bt.TurnDirectorBugFixes.Utils;

namespace nl.flukeyfiddler.bt.TurnDirectorBugFixes
{
    [HarmonyPatch(typeof(GameInstance), "Save")]
    public class GameInstance_Save_Patch
    {
        public static bool Postfix(GameInstance __instance)
        {
            MultiSequence multiSequence = __instance.Combat.StackManager.GetAnyParentableMultiSequence();
            if (multiSequence != null)
            {
                Logger.Line("Sequence not complete!");
                return true;
            }
            return true;
        }
    }
}
