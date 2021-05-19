using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;

namespace EnglishKids.RobotsConstructor
{
    public class RobotPartView : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public Image image;
        public RectTransform slot;
        public CanvasGroup canvasGroup;
        public Image dragArea;

        [SerializeField]
        private float dragAreaSize = 1.4f;
        [SerializeField]
        private float rotationDuraton = 0.25f;

        private Vector3? startPosition = null;
        private Tween rotationTween;
        private Vector3 rotation;

        public event Action onMounted;

        private void Start()
        {
            SetRandomRotation();

            CreateCanvasGroup();

            CreateBiggerAreaSize();
        }

        private void SetStartPosition()
        {
            if (startPosition == null)
                startPosition = transform.position;
        }

        private void CreateCanvasGroup()
        {
            if (!canvasGroup)
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        private void CreateBiggerAreaSize()
        {
            if (!dragArea)
            {
                var go = new GameObject(name + "DragArea");
                go.transform.SetParent(transform, false);
                go.transform.localPosition = Vector3.zero;
                dragArea = go.AddComponent<Image>();
                dragArea.color = new Color(0, 0, 0, 0.01f);
                dragArea.rectTransform.sizeDelta = image.rectTransform.sizeDelta * dragAreaSize;
            }
        }

        public void GenerateSlot()
        {
            if (slot != null)
                return;

            var copy = new GameObject("Slot");
            var imageCopy = copy.AddComponent<Image>();
            imageCopy.sprite = image.sprite;
            imageCopy.SetNativeSize();
            imageCopy.rectTransform.sizeDelta *= 1.1f;
            copy.transform.SetParent(transform.parent);
            copy.transform.position = transform.position;
            copy.GetComponent<Image>().color = new Color(0, 0, 0, 0.01f);
            slot = copy.GetComponent<RectTransform>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            SetStartPosition();
            RotateToDefault();
            image.raycastTarget = false;
            SetInteractable(false);

            SoundsManager.Play("pick");
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            image.raycastTarget = true;
            canvasGroup.blocksRaycasts = true;

            if (CheckIfInSlot())
            {
                MountToCorrectSlot();
            }
            else
            {
                ResetToTransporterLine();
            }
        }

        public void SetRandomRotation()
        {
            float rotationZ = UnityEngine.Random.Range(-90, 90);
            image.transform.eulerAngles = new Vector3(0, 0, rotationZ);
            rotation = image.transform.eulerAngles;
        }

        private bool CheckIfInSlot()
        {
            var pos = slot.InverseTransformPoint(transform.position);
            return slot.rect.Contains(pos);
        }

        private void ResetRotation()
        {
            if (rotationTween != null)
            {
                rotationTween.Kill();
            }

            image.transform.eulerAngles = rotation;
        }

        private void RotateToDefault()
        {
            if (rotationTween != null)
            {
                rotationTween.Kill();
            }

            rotationTween = image.transform.DORotate(Vector3.zero, rotationDuraton, RotateMode.Fast);
        }

        //TODO сделать сортивроку по индексу трансформа
        private void MountToCorrectSlot()
        {
            transform.SetParent(slot.parent);
            transform.position = slot.position;
            image.raycastTarget = false;
            onMounted?.Invoke();

            SoundsManager.Play("correct answer");
            SetInteractable(false);
        }

        private void ResetToTransporterLine()
        {
            transform.position = startPosition.Value;
            ResetRotation();

            SoundsManager.Play("wrong answer");
            SetInteractable(true);
        }

        private void SetInteractable(bool interactable)
        {
            canvasGroup.interactable = interactable;
            canvasGroup.blocksRaycasts = interactable;
        }
    }
}
