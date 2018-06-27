using System;
using System.Reflection;
using BattleTech;
using Harmony;
using nl.flukeyfiddler.bt.MissionLoadingFixesAndDebug.Utils;
using UnityEngine;
using UnityEngine.PostProcessing;

namespace nl.flukeyfiddler.bt.MissionLoadingFixesAndDebug
{
    // prevent stream of nullreferenceExceptions being logged from PostProcessor
    // this happens to me when all advanced videos settings are off
    // Videocard GTX745
    [HarmonyPatch(typeof(PostProcessingBehaviour), "Update")]
    public class PostProcessingBehaviour_Update_Patch
    {
        public static bool loggedIt;


        private static bool Prefix(PostProcessingBehaviour __instance, ref DepthOfFieldComponent ___m_DepthOfField)
        {
            try
            {
                if (Application.isPlaying && !__instance.useMotionBlur &&
                    !__instance.useAmbientOcclusion && !__instance.useAntiAliasing
                    && !__instance.useSSR && !__instance.useScreenSpaceShadows)
                {
                    if(!loggedIt)
                    {
                        loggedIt = true;
                    }

                    if (___m_DepthOfField == null)
                    {
                        if (!loggedIt)
                        {
                            loggedIt = true;
                        }
                       
                        __instance.enabled = false;
                    }
                    else
                    {
                        if (!loggedIt)
                        {
                            loggedIt = true;
                        }

                        __instance.enabled = ___m_DepthOfField.active;
                    }
                }

            } catch (NullReferenceException ex)
            {
                //Utils.Logger.Minimal(ex.ToString());
            }

            return false;
        }
    }

    // Garbagecollect before loading a Sim Combat Game
    [HarmonyPatch(typeof(SimGameState), "OnLanceConfiguratorAccept")]
    public class SimGameState_OnLanceConfiguratorAccept_Patch
    {
        private static void Postfix()
        {
           Resources.UnloadUnusedAssets();
        }
    }
}
