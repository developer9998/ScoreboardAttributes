using HarmonyLib;

namespace ScoreboardAttributes
{
    [HarmonyPatch(typeof(GorillaPlayerScoreboardLine))]
    [HarmonyWrapSafe]
    internal class Patch
    {
        [HarmonyPatch("InitializeLine")]
        [HarmonyPostfix]
        internal static void Start(GorillaPlayerScoreboardLine __instance)
        {
            if (__instance.GetComponent<PlayerLine>() == null)
            {
                __instance.gameObject.AddComponent<PlayerLine>().baseLine = __instance;
            }
        }
    }
}
