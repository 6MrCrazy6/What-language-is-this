using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TextSize : MonoBehaviour
    {
        public static void AdjustTextSize(Text textComponent)
        {
            int defaultFontSize = 250;
            int minFontSize = 10;

            textComponent.fontSize = defaultFontSize;

            while (textComponent.preferredWidth > textComponent.rectTransform.rect.width && textComponent.fontSize > minFontSize)
            {
                textComponent.fontSize--;
            }
        }
    }

}
