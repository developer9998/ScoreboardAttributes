using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace ScoreboardAttributes
{
    [BepInPlugin(Constants.GUID, Constants.Name, Constants.Version)]
    internal class Plugin : BaseUnityPlugin
    {
        public static new ManualLogSource Logger;

        public void Awake()
        {
            Logger = base.Logger;

            Harmony.CreateAndPatchAll(typeof(Plugin).Assembly, Constants.GUID);

            RoomSystem.LeftRoomEvent += Registry.FilterAttributes;
            RoomSystem.PlayerLeftEvent += _ => Registry.FilterAttributes();
        }
    }
}