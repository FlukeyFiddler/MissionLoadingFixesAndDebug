using BattleTech;
using Harmony;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System.Collections;

namespace nl.flukeyfiddler.bt.MissionLoadingFixesAndDebug.Fixes
{
    [HarmonyPatch(typeof(LevelLoader))]
    public class LevelLoader_Load_Patch
    {
        static List<string> debugLines = new List<string>();

        static LevelLoader LevelLoaderInstance;

        //static bool loggedOnce = false;
        
        private static MethodBase TargetMethod(HarmonyInstance harmony) {
            MethodInfo methodInfo = AccessTools.Method(typeof(LevelLoader), "Load").GetType().GetNestedType("IEnumerator").GetMethod("MoveNext");
            debugLines.Add("hmm");
            debugLines.Add("type of Load: " + AccessTools.Method(typeof(LevelLoader), "Load").GetType());
            debugLines.Add("Nested type: " + AccessTools.Method(typeof(LevelLoader), "Load").GetType().GetNestedType("IEnumerator"));
            debugLines.Add("got me a numerator: " + methodInfo.FullDescription());
            return methodInfo;
        }
        
        // MethodBase original, 
        private static IEnumerable<CodeInstruction> Transpiler(LevelLoader __instance, IEnumerable<CodeInstruction> instructions)
        {

            LevelLoaderInstance = __instance;
           // if (!loggedOnce)
          //  {
                debugLines.Add("In the transpiler");

            //  }
            //MethodInfo mi = AccessTools.Property(typeof(Vector3), nameof(Vector3.magnitude)).GetGetMethod();
            MethodInfo originalUnloadMethod = AccessTools.Method(typeof(Resources), "UnloadUnusedAssets");
            MethodInfo replacementUnloadMethod = AccessTools.Method(typeof(LevelLoader_Load_Patch), "getReplacementUnloadUnusedAssets");

            return instructions.MethodReplacer(originalUnloadMethod, replacementUnloadMethod);
            /*
            debugLines.Add("MethodInfo: " + originalUnloadMethod.Name);

            instructions.DoIf(instruction => instruction.operand == originalUnloadMethod, delegate(CodeInstruction instruction) {
                debugLines.Add("Found the call at: " + instruction.ToString());
            });

            instructions.Do(instruction => {
                debugLines.Add("operand: " + instruction.operand);
            });

            */

            /*
             // If the return type is an iterator -- need to go searching for its MoveNext method which contains the actual code you'll want to inject
                if (hookedMethod.ReturnType.Name.Equals("IEnumerator"))
                {
                     var nestedIterator = game.GetType(hookType).NestedTypes.First(x => x.Name.Contains(hookMethod) && x.Name.Contains("Iterator"));
                     hookedMethod = nestedIterator.Methods.First(x => x.Name.Equals("MoveNext"));
               } 

             */
            //Traverse.Create<Resources>().Method("UnloadUnusedAssets"). 
            // original.
            /*

             ldarg.0
             call 
             stfld

             * */
            //instructions.
            // Type type = __result.Current.GetType();
            // var sfd = Traverse.Create(originalMethod).Method("Load").GetValue();
            // loggedOnce = true;
           
           // return instructions;
        }
        // MethodBase __originalMethod, IEnumerator __result
        private static void Postfix() {
            Utils.Logger.Block(debugLines.ToArray(), MethodBase.GetCurrentMethod());

           // MethodInfo nextIteration = __originalMethod.GetType().GetNestedType("Iterator").GetMethod("MoveNext");//.GetNestedType("Iterator").GetMethod("MoveNext");
            //MissionLoadingFixesAndDebug.harmony.RemovePatch(__result.GetType().GetMethod("Load"), HarmonyPatchType.Transpiler, MissionLoadingFixesAndDebug.harmony.Id);
           // MissionLoadingFixesAndDebug.harmony.Patch(nextIteration, null, null, new HarmonyMethod(typeof(LevelLoader_Load_Patch).GetMethod("Transpiler")));

        }

        private static IEnumerator getReplacementUnloadUnusedAssets()
        {
            debugLines.Add("replacing method");
            AsyncOperation originalUnloadUnusedAssets = Traverse.Create(typeof(Resources)).Method("UnloadUnusedAssets").GetValue<AsyncOperation>();
            IEnumerator replacementUnLoadUnusedAssets = Traverse.Create(typeof(LevelLoader)).Method("YieldForLoadOperation").GetValue<IEnumerator>(originalUnloadUnusedAssets);
            return replacementUnLoadUnusedAssets;
        }

    }
}
