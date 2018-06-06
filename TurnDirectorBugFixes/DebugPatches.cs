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

    [HarmonyPatch(typeof(TurnDirector), "Load")]
    public class TurnDirector_Load_Patch_Debug
    {
        public static void Postfix(TurnDirector __instance)
        {
            Logger.Line("In Load Postfix");

            List<string> debugLines = new List<string>();

            debugLines.Add("Active TurnActorIndex: " + __instance.ActiveTurnActorIndex);
            debugLines.Add("TurnActors:");
            debugLines.Add("Me: " + __instance.Combat.LocalPlayerTeamGuid);
            int index = 0;
            foreach (TurnActor turnActor in __instance.TurnActors)
            {
                debugLines.Add("index: " + index++);
                debugLines.Add("GUID: " + turnActor.GUID);

                AbstractActor abstractActor = __instance.Combat.FindActorByGUID(turnActor.GUID);
                debugLines.Add("name: " + abstractActor.DisplayName);
                debugLines.Add("abstractActor can move after shooting: " + 
                    abstractActor.CanMoveAfterShooting);
                debugLines.Add("... can shoot after sprinting: " +
                    abstractActor.CanShootAfterSprinting);
                debugLines.Add("... is completing Activation" +
                    abstractActor.IsCompletingActivation);
                debugLines.Add("... has moved this round still set" +
                    abstractActor.HasMovedThisRound);
                debugLines.Add("... has sprinted this round still set" +
                    abstractActor.HasSprintedThisRound);
                debugLines.Add("stats to string" + abstractActor.StatCollection.ToString());
            }
            debugLines.Add("CurrentPhase" + __instance.CurrentPhase);
            debugLines.Add("CurrentRound" + __instance.CurrentRound);
            debugLines.Add("IsInterleaved" + __instance.IsInterleaved);
            debugLines.Add("IsInterleavePending" + __instance.IsInterleavePending);

            

            Logger.Block(debugLines.ToArray());
        }
    }

    [HarmonyPatch(typeof(CombatGameState), "Load")]
    public class CombatGameState_Load_Patch_Debug
    {
        public static void Postfix(CombatGameState __instance)
        {
            Logger.Line("in combatstate load");

            Logger.Line("turnDirector GameHasBegun: " + __instance.TurnDirector.GameHasBegun);
            Logger.Line("localPlayerGUID: " + __instance.LocalPlayerTeamGuid);
            Logger.Line("attackDirector any attack active: " + __instance.AttackDirector.IsAnyAttackSequenceActive);
        }
    }

    [HarmonyPatch(typeof(TurnDirector), "InitFromSave")]
    public class TurnDirector_InitFromSave_Patch_Debug
    {
        public static void Postfix(TurnDirector __instance)
        {
            List<string> debugLines = new List<string>(){"turnActors:"};

            foreach(TurnActor actor in __instance.TurnActors)
            {
                debugLines.Add(actor.GUID);
            }

            List<string> turnActorGUIDS = Traverse.Create(__instance).Property("turnActorGUIDS").GetValue<List<string>>();

            debugLines.Add("TurnActorGUIDS: \r\n");
            foreach (string actorGUID in turnActorGUIDS) {
                debugLines.Add(actorGUID);
            }

            debugLines.Add("Active turn actor: " + __instance.ActiveTurnActor.GUID);
            debugLines.Add("Active turn actorindex: " + __instance.ActiveTurnActorIndex);
            debugLines.Add("current round: " + __instance.CurrentRound);
            debugLines.Add("current phase: " + __instance.CurrentPhase);

            Logger.Block(debugLines.ToArray(), MethodBase.GetCurrentMethod());

            __instance.OnTurnActorActivateComplete();

        }
    }
    [HarmonyPatch(typeof(CombatGameState), "_Init")]
    public class CombatGameState_Init_Patch_Debug
    {
        public static void Postfix(CombatGameState __instance)
        {
            List<string> debugLines = new List<string>();

            debugLines.Add("myTeam GUID" + __instance.LocalPlayerTeamGuid);

            Logger.Block(debugLines.ToArray(), MethodBase.GetCurrentMethod());
        }
    }

    [HarmonyPatch(typeof(CombatGameState), "_InitFromSave")]
    public class CombatGameState_InitFromSave_Patch_Debug
    {
        public static void Postfix()
        {
            
        }
    }
}