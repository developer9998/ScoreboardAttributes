using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace ScoreboardAttributes
{
    public static class PlayerTexts
    {
        public class Data
        {
            public string AttributeText;
            public Assembly Assembly;
        }

        public static Dictionary<NetPlayer, List<Data>> keyValuePairs = new Dictionary<NetPlayer, List<Data>>();

        public static void UnregisterAttribute(Player player)
        {
            var callingAssembly = Assembly.GetCallingAssembly();
            if (keyValuePairs.ContainsKey(player))
            {
                var existingData = keyValuePairs[player].First(a => a.Assembly == callingAssembly);
                if (existingData != null) keyValuePairs[player].Remove(existingData);

                if (keyValuePairs[player].Count == 0) keyValuePairs.Remove(player);
            }
        }

        public static void RegisterAttribute(string attributeText, Player player)
        {
            if (keyValuePairs.Count > 0)
            {
                var nullKeys = keyValuePairs.Keys.ToList().Where(a => a == null).ToList();
                nullKeys.ForEach(key => keyValuePairs.Remove(key));
            }

            var callingAssembly = Assembly.GetCallingAssembly();
            if (!keyValuePairs.ContainsKey(player))
            {
                Data playerData = new Data
                {
                    AttributeText = attributeText,
                    Assembly = callingAssembly
                };
                keyValuePairs.Add(player, new List<Data>() { playerData });
            }
            else if (keyValuePairs.ContainsKey(player))
            {
                var assemblyValueExists = false;
                foreach (var key in keyValuePairs[player])
                {
                    if (key.Assembly == callingAssembly)
                    {
                        key.AttributeText = attributeText;
                        assemblyValueExists = true;
                        break;
                    }
                }
                if (!assemblyValueExists)
                {
                    Data playerData = new Data
                    {
                        AttributeText = attributeText,
                        Assembly = callingAssembly
                    };
                    var playerList = keyValuePairs[player];
                    playerList.Add(playerData);
                    keyValuePairs[player] = playerList;
                }
            }
        }

        public static string GetAttributes(NetPlayer player)
        {
            if (player != null)
            {
                if (keyValuePairs.ContainsKey(player))
                {
                    if (keyValuePairs.TryGetValue(player, out var attributes) && attributes.Count > 0)
                    {
                        List<string> attributeValues = new List<string>();
                        attributes.ForEach(a => attributeValues.Add(a.AttributeText));
                        string returnString = attributeValues.Count == 1 ? attributeValues[0] : string.Join(", ", attributeValues);
                        return returnString.ToUpper();
                    }
                }
            }

            return "";
        }
    }
}
