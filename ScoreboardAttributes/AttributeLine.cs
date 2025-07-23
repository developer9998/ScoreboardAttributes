using UnityEngine;
using UnityEngine.UI;

namespace ScoreboardAttributes
{
    [RequireComponent(typeof(GorillaPlayerScoreboardLine))]
    internal class AttributeLine : MonoBehaviour
    {
        public GorillaPlayerScoreboardLine baseLine;

        public NetPlayer linePlayer;

        public Text attributeText;

        public void Awake()
        {
            baseLine = GetComponent<GorillaPlayerScoreboardLine>();
        }

        public void Start()
        {
            Text baseText = baseLine.playerName;

            if (baseText == null || !baseText)
            {
                foreach(Transform child in baseLine.transform)
                {
                    if (child.GetComponent<Text>())
                    {
                        baseText = child.GetComponent<Text>();
                        break;
                    }
                }
            }

            if (baseText == null || !baseText) return;

            GameObject attributeTxtObject = Instantiate(baseText.gameObject, baseText.transform.parent);
            attributeTxtObject.SetActive(true);

            attributeText = attributeTxtObject.GetComponent<Text>();

            attributeText.text = "what the helly";
            attributeText.lineSpacing = 0.8f;
            attributeText.supportRichText = true;
            attributeText.alignment = TextAnchor.UpperLeft;

            attributeText.transform.localPosition = new Vector3(-80f, -9f, 0f);
            attributeText.transform.localScale = Vector3.one * 0.32f;
            (attributeText.transform as RectTransform).sizeDelta = new Vector2(227.5f, 25f);

            /*
            Text[] textArray = new Text[baseLine.texts.Length + 1];
            baseLine.texts.CopyTo(textArray, 0);
            textArray[^1] = attributeText;
            baseLine.texts = textArray;
            */

            UpdateText();
        }

        public void UpdateText()
        {
            attributeText.text = Registry.GetAttributes(linePlayer);
            attributeText.color = baseLine.playerVRRig is VRRig playerRig ? playerRig.playerText1.color : Color.white;
        }

        public void OnDestroy()
        {
            if (attributeText) Destroy(attributeText.gameObject);
        }
    }
}