using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ScoreboardAttributes
{
    internal class PlayerLine : MonoBehaviour
    {
        public GorillaPlayerScoreboardLine baseLine;

        public Text attributeText;

        public async void Start()
        {
            // Wait for the line to fully initialize
            float currentTime = Time.time + 3;
            while (!baseLine.initialized)
            {
                if (Time.time >= currentTime) Destroy(this); // If it takes too long, give up
                await Task.Yield();
            }

            var pronounTagObject = Instantiate(baseLine.playerName.gameObject, baseLine.playerName.transform.parent);
            pronounTagObject.SetActive(true);
            if (pronounTagObject.TryGetComponent(out attributeText))
            {
                attributeText.text = "";
                attributeText.horizontalOverflow = HorizontalWrapMode.Overflow;
                attributeText.supportRichText = false;
                attributeText.transform.localPosition = new Vector3(-95.5f, -5.5f, 0f);
                attributeText.transform.localScale = Vector3.one * 0.35f;

                // Add to the text list
                var textList = baseLine.texts.ToList();
                textList.Add(attributeText);
                baseLine.texts = textList.ToArray();
            }

            InvokeRepeating(nameof(UpdateLine), 1f, 2f);
        }

        public void UpdateLine()
        {
            attributeText.text = PlayerTexts.GetAttributes(baseLine.linePlayer);
            attributeText.color = baseLine.playerName.color;
        }
    }
}
