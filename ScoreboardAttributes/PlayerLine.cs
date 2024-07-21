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

        public void Start()
        {
            // Wait for the line to fully initialize

            //float currentTime = Time.time + 5;
            //while (!baseLine.initialized)
            //{
            //    if (Time.time >= currentTime) Destroy(this); // If it takes too long, give up
            //    await Task.Yield();
            //}

            var pronounTagObject = Instantiate(baseLine.playerName.gameObject, baseLine.playerName.transform.parent);
            pronounTagObject.SetActive(true);
            if (pronounTagObject.TryGetComponent(out attributeText))
            {
                attributeText.text = "";
                attributeText.lineSpacing = 0.8f;
                attributeText.supportRichText = false;
                attributeText.alignment = TextAnchor.UpperLeft;

                attributeText.transform.localPosition = new Vector3(-80f, -9f, 0f);
                attributeText.transform.localScale = Vector3.one * 0.32f;
                (attributeText.transform as RectTransform).sizeDelta = new Vector2(227.5f, 25f);

                // Add to the text list
                var textList = baseLine.texts.ToList();
                textList.Add(attributeText);
                baseLine.texts = textList.ToArray();
            }

            InvokeRepeating(nameof(UpdateLine), 1f, 2f);
        }

        public void UpdateLine()
        {
            attributeText.text = PlayerTexts.GetAttributes(baseLine.playerVRRig.Creator);
            attributeText.color = baseLine.playerName.color;
        }
    }
}
