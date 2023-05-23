using BepInEx;
using HarmonyLib;
using ScoreboardAttributes.Scripts;
using System.Reflection;

namespace ScoreboardAttributes
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    internal class Plugin : BaseUnityPlugin
    {
        internal Harmony harmonyPatch;

        internal void Awake()
        {
            if (harmonyPatch == null)
            {
                harmonyPatch = new Harmony(PluginInfo.GUID);
                harmonyPatch.PatchAll(Assembly.GetExecutingAssembly());
            }
        }

        internal void AddLocalAttribute(string attribute)
            => PlayerTexts.RegisterAttribute(attribute, Photon.Pun.PhotonNetwork.LocalPlayer);

        internal void RemoveLocalAttribute()
            => PlayerTexts.UnregisterAttribute(Photon.Pun.PhotonNetwork.LocalPlayer);
    }
}
