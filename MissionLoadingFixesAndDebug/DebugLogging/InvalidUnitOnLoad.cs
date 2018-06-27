using BattleTech;
using Harmony;
using nl.flukeyfiddler.bt.MissionLoadingFixesAndDebug.Utils;
using nl.flukeyfiddler.bt.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace nl.flukeyfiddler.bt.MissionLoadingFixesAndDebug
{
    [HarmonyPatch(typeof(LanceConfiguration), "GetLanceUnits")]
    public class LanceConfiguration_GetLanceUnits_Patch_DEBUG
    {
        public static void Postfix(LanceConfiguration __instance, string teamDefinition, SpawnableUnit[] __result)
        {
            List<string> debugLines = new List<string>();
            string teamName = CombatTeamFactionGuids.FactionGUIDToName.ContainsKey(teamDefinition) ? CombatTeamFactionGuids.FactionGUIDToName[teamDefinition] : "Empty";
            debugLines.Add("teamDefinition: " + teamName);
            debugLines.Add("--------------");

            foreach(SpawnableUnit unit in __result)
            {
                debugLines.Add(String.Format("Unit id: {0}, type: {1}, team: {2}",
                    unit.UnitId, unit.unitType, CombatTeamFactionGuids.FactionGUIDToName[unit.TeamDefinitionGuid]));

                debugLines.Add(String.Format("Pilot: {0}, Initialized from JSON: {1}, Can Load: {2}",
                    unit.Pilot.Description.DisplayName, unit.IsUnitializedJson, unit.CanLoad()));

                switch (unit.unitType)
                {
                    case UnitType.Mech:
                        debugLines.Add(String.Format("name: {0}", unit.Unit.Description.Name));
                        break;
                    case UnitType.Turret:
                        debugLines.Add(String.Format("name: {0}", unit.TUnit.Description.Name));
                        break;
                    case UnitType.Vehicle:
                        debugLines.Add(String.Format("name: {0}", unit.VUnit.Description.Name));
                        break;
                }
                debugLines.Add("--------------");
            }
            Logger.Block(debugLines.ToArray(), MethodBase.GetCurrentMethod());
        }
    }
}
