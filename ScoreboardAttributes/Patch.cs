using HarmonyLib;

namespace ScoreboardAttributes
{
    [HarmonyPatch(typeof(GorillaPlayerScoreboardLine), "Start", MethodType.Normal)]
    internal class Patch
    {
        internal static void Prefix(GorillaPlayerScoreboardLine __instance)
        {
            if (__instance.GetComponent<PlayerLine>() == null)
            {
                __instance.gameObject.AddComponent<PlayerLine>().baseLine = __instance;
            }
        }
    }
}
