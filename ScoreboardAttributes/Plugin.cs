using BepInEx;
using HarmonyLib;
using System.Reflection;

namespace ScoreboardAttributes
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    internal class Plugin : BaseUnityPlugin
    {
        internal void Awake()
        {
            new Harmony(PluginInfo.GUID).PatchAll(Assembly.GetExecutingAssembly());
        }

        internal void AddLocalAttribute(string attribute)
            => PlayerTexts.RegisterAttribute(attribute, Photon.Pun.PhotonNetwork.LocalPlayer);

        internal void RemoveLocalAttribute()
            => PlayerTexts.UnregisterAttribute(Photon.Pun.PhotonNetwork.LocalPlayer);
    }
}
