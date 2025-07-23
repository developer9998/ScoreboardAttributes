using HarmonyLib;
using UnityEngine;

namespace ScoreboardAttributes
{
    [HarmonyPatch(typeof(GorillaPlayerScoreboardLine))]
    internal class Patches
    {
        [HarmonyPatch(nameof(GorillaPlayerScoreboardLine.InitializeLine))]
        [HarmonyPrefix]
        internal static void InitializePatch(GorillaPlayerScoreboardLine __instance)
        {
            if (__instance.TryGetComponent(out AttributeLine extension))
            {
                if (extension.linePlayer != __instance.linePlayer)
                {
                    Object.Destroy(extension);
                    goto AddComponent;
                }
                extension.UpdateText();
                return;
            }

        AddComponent:
            __instance.AddComponent<AttributeLine>().linePlayer = __instance.linePlayer;
        }

        /*
        [HarmonyPatch(nameof(GorillaPlayerScoreboardLine.UpdateLine))]
        [HarmonyPostfix]
        internal static void UpdatePatch(GorillaPlayerScoreboardLine __instance)
        {
            
        }
        */

        [HarmonyPatch(nameof(GorillaPlayerScoreboardLine.ResetData))]
        [HarmonyPrefix]
        internal static void ResetPatch(GorillaPlayerScoreboardLine __instance)
        {
            if (__instance.TryGetComponent(out AttributeLine extension))
            {
                Object.Destroy(extension);
            }
        }
    }
}
