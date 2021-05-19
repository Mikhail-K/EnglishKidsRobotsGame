using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnglishKids.RobotsConstructor
{
    public class TransporterLineView : MonoBehaviour
    {
        public void ChangeLines()
        {
            ChangeLine();
            ChangeLine();
        }

        private void ChangeLine()
        {
            var child = transform.GetChild(0);
            var childLast = transform.GetChild(transform.childCount - 1);
            child.GetComponent<RectTransform>().anchoredPosition = childLast.GetComponent<RectTransform>().anchoredPosition - new Vector2(0, child.GetComponent<RectTransform>().sizeDelta.y);
            //child.GetComponent<RectTransform>().position = childLast.position - new Vector3(0, child.GetComponent<RectTransform>().rect.size.y/2);
            child.SetAsLastSibling();
        }
    }
}
