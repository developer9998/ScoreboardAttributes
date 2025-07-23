using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ScoreboardAttributes
{
    public static class Registry
    {
        internal static Dictionary<NetPlayer, List<PlayerAttribute>> dataPerPlayerCollection = [];

        public static void AddAttribute(NetPlayer player, string text)
        {
            Assembly assembly = Assembly.GetCallingAssembly();
            AddAttribute(player, text, assembly);
        }

        internal static void AddAttribute(NetPlayer player, string text, Assembly assembly)
        {
            PlayerAttribute playerData;

            if (dataPerPlayerCollection.TryGetValue(player, out List<PlayerAttribute> attributes))
            {
                foreach (PlayerAttribute data in attributes)
                {
                    if (data.Assembly == assembly)
                    {
                        data.Text = text;
                        UpdateLines(player);
                        return;
                    }
                }

                playerData = new()
                {
                    Text = text,
                    Assembly = assembly
                };
                attributes.Add(playerData);
                UpdateLines(player);
                return;
            }

            playerData = new()
            {
                Text = text,
                Assembly = assembly
            };
            dataPerPlayerCollection.Add(player, [playerData]);
            UpdateLines(player);
        }

        public static void RemoveAttribute(NetPlayer player)
        {
            Assembly assembly = Assembly.GetCallingAssembly();
            RemoveAttribute(player, assembly);
        }

        internal static void RemoveAttribute(NetPlayer player, Assembly assembly)
        {
            if (dataPerPlayerCollection.TryGetValue(player, out List<PlayerAttribute> attributes))
            {
                if (attributes.Find(a => a.Assembly == assembly) is PlayerAttribute data && data is not null)
                {
                    attributes.Remove(data);
                    UpdateLines(player);
                }

                if (attributes.Count == 0) dataPerPlayerCollection.Remove(player);
            }
        }

        public static string GetAttributes(NetPlayer player)
        {
            if (dataPerPlayerCollection.TryGetValue(player, out List<PlayerAttribute> attributes) && attributes.Count > 0)
            {
                var attributeTexts = attributes.Select(attribute => attribute.Text.ToUpper()).ToList();
                return attributeTexts.Count == 1 ? attributeTexts[0] : string.Join(", ", attributeTexts);
            }

            return "";
        }

        internal static void FilterAttributes()
        {
            try
            {
                if (dataPerPlayerCollection.Count > 0)
                {
                    for (int i = 0; i < dataPerPlayerCollection.Count; i++)
                    {
                        NetPlayer recordedPlayer = dataPerPlayerCollection.ElementAtOrDefault(i).Key;
                        if (recordedPlayer == null || recordedPlayer.IsNull || (!recordedPlayer.IsLocal && !recordedPlayer.InRoom))
                        {
                            dataPerPlayerCollection.Remove(recordedPlayer);
                            i--;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Plugin.Logger.LogFatal("Attributes could not be filtered");
                Plugin.Logger.LogError(ex);
            }
        }

        internal static void UpdateLines(NetPlayer player)
        {
            foreach(GorillaPlayerScoreboardLine scoreboardLine in GorillaScoreboardTotalUpdater.allScoreboardLines)
            {
                if (scoreboardLine.linePlayer == player && scoreboardLine.TryGetComponent(out AttributeLine extension))
                {
                    extension.UpdateText();
                }
            }
        }

        internal class PlayerAttribute
        {
            public string Text;
            public Assembly Assembly;
        }
    }
}
