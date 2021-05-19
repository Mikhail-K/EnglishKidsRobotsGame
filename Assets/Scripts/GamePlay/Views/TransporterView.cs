using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace EnglishKids.RobotsConstructor
{
    public class TransporterView : MonoBehaviour
    {
        public Canvas canvas;
        public RectTransform container;
        public RectTransform transporterLine;
        public TransporterLineView transporterLineView;
        public GameObject block;

        public float tranpoterAnimationDuation = 3f;
        public RectTransform transporterGoalPosition;
        public RectTransform tranporterStartPosition;

        public Vector2 yRandomOffset = new Vector2(10f, 20f);
        public Vector2 xRandomOffset = new Vector2(-40f, 40f);
        public Vector2 defaultScreenSize;

        private List<RectTransform> currentParts = new List<RectTransform>();

        public bool AddPart(RectTransform part)
        {
            Transform cacheParent = part.parent;
            part.SetParent(container);

            Vector2 containerSize = new Vector2(container.sizeDelta.x * container.localScale.x, container.sizeDelta.y * container.localScale.y);
            Vector2 partSize = new Vector2(part.sizeDelta.x * part.localScale.x, part.sizeDelta.y * part.localScale.y);
           
            if (currentParts != null && currentParts.Count > 0)
            {
                RectTransform lastPart = currentParts[currentParts.Count - 1];

                Vector2 lastPartSize = new Vector2(lastPart.sizeDelta.x * lastPart.localScale.x, lastPart.sizeDelta.y * lastPart.localScale.y);

                float xOffset = Random.Range(containerSize.x / 3, containerSize.x / 2);

                Vector2 position = lastPart.anchoredPosition + new Vector2(partSize.x / 2f + partSize.x / 2f + xOffset, 0f);
                part.anchoredPosition = position;

                if (!RectContainsRect(containerSize, container.position, partSize, part.position))
                {
                    position = new Vector2(XOffset(), lastPart.anchoredPosition.y) - new Vector2(0f, partSize.y / 2f + lastPartSize.y / 2f + YOffset());
                    part.anchoredPosition = position;
                }
            }
            else
            {
                part.anchoredPosition = new Vector2(XOffset(), -(partSize.y / 2 + YOffset()));
            }

            if (RectContainsRect(containerSize, container.position, partSize, part.position))
            {
                currentParts.Add(part);
                part.gameObject.SetActive(true);
                return true;
            }
            else
            {
                part.SetParent(cacheParent);
                return false;
            }
        }

        public float YOffset()
        {
            return Random.Range(yRandomOffset.x, yRandomOffset.y);
        }

        public float XOffset()
        {
            return Random.Range(xRandomOffset.x, xRandomOffset.y);
        }

        [ContextMenu("MoveTransporterLine")]
        public void MoveTransporterLine()
        {
            SoundsManager.Play("new parts");
            container.DOAnchorPos(transporterGoalPosition.anchoredPosition, tranpoterAnimationDuation).OnComplete(OnLineStoped);
            block.SetActive(true);
        }

        public bool RectContainsRect(Vector2 size1, Vector2 pos1, Vector2 size2, Vector2 pos2)
        {
            pos1 /= canvas.scaleFactor;
            pos2 /= canvas.scaleFactor;
            
            Rect newRect1 = new Rect(pos1, size1);
            Rect newRect2 = new Rect(pos2, size2);

            newRect1.center = pos1;
            newRect1.size = size1;

            newRect2.center = pos2;
            newRect2.size = size2;

            return newRect1.Contains(newRect2.min) && newRect1.Contains(newRect2.max);
        }

        public void ResetTransporter()
        {
            transporterLine.SetParent(canvas.transform);
            container.anchoredPosition = tranporterStartPosition.anchoredPosition;
            transporterLine.SetParent(container);
        }

        public int GetAmountOfActiveParts()
        {
            return currentParts.Count;
        }

        public void ClearTransporterLine()
        {
            //currentParts.ForEach(x => x.gameObject.SetActive(false));
            currentParts.Clear();
        }

        private void OnLineStoped()
        {
            SoundsManager.Play("new parts");
            transporterLineView.ChangeLines();
            block.SetActive(false);
        }
    }
}
