using BattleTech;
using BattleTech.UI;
using Harmony;
using nl.flukeyfiddler.bt.TurnDirectorBugFixes.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace nl.flukeyfiddler.bt.TurnDirectorBugFixes
{
    [HarmonyPatch(typeof(MainMenu), "HandleEscapeKeypress")]
    public class MainMenu_HandleEscapeKeyPress_Patch_Debug
    {
        public static void Postfix(MainMenu __instance)
        {
            Logger.Line("Main Menu");
        }
    }      

    [HarmonyPatch(typeof(GameInstance), "CanSave")]
    public class GameInstance_CanSave_Patch_Debug
    {
        public static void Postfix(GameInstance __instance, bool __result)
        {
            if (__result ==  false && __instance.Combat != null
                && Helper.stackManagerStillTwo(__instance.Combat.StackManager)) {
                Logger.Line("returning true, but there are more than 2 Sequences in the stack",
                    MethodBase.GetCurrentMethod());
            }
        }
    }    

    [HarmonyPatch(typeof(CombatGameState), "CanSave")]
    public class CombatGameState_CanSave_Patch_Debug
    {
        public static void Postfix(CombatGameState __instance, bool __result)
        {
            if (__result == false && Helper.stackManagerStillTwo(__instance.StackManager))
            {
                Logger.Line("returning true, but there are more than 2 Sequences in the stack",
                    MethodBase.GetCurrentMethod());
            }
        }
    }

    [HarmonyPatch(typeof(StackManager), "CanSave")]
    public class StackManager_CanSave_Patch_Debug
    {
        public static void Postfix(StackManager __instance, bool __result)
        {
            if (__result == false && Helper.stackManagerStillTwo(__instance))
            {
                Logger.Line("returning true, but there are more than 2 Sequences in the stack",
                    MethodBase.GetCurrentMethod());
            }
        }
    }

    public class Helper
    {
        public static bool stackManagerStillTwo(StackManager stackManager)
        {
            int sequencesInStack = Traverse.Create(stackManager).Field("SequenceStack").
                Property("Count").GetValue<int>();
            return sequencesInStack == 2;
        }
    }
   
}