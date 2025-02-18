using UnityEngine;
using UnityEngine.EventSystems;

namespace InfiniteMap
{
    public class DragUI : GMono, IDragHandler
    {
        [SerializeField] private Transform details;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private RectTransform detailsRectTransform;
        [SerializeField] private Transform canvasTrans;
        [SerializeField] private Canvas canvas;

        protected override void Start()
        {
            base.Start();
            canvas = canvasTrans.GetComponent<Canvas>();
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadRectTransform();
            LoadDetails();
            LoadDetailsRectTransform();
        }

        private void LoadRectTransform()
        {
            if(rectTransform != null) return;

            rectTransform = GetComponent<RectTransform>();
        }

        private void LoadDetails()
        {
            if(details != null) return;

            details = transform.parent.Find("BG");
        }

        private void LoadDetailsRectTransform()
        {
            if (detailsRectTransform != null) return;

            detailsRectTransform = details.GetComponent<RectTransform>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            
            float halfHeight = rectTransform.rect.height / 2;
            float detailsHalfHeight = detailsRectTransform.rect.height / 2;

            detailsRectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y - halfHeight - detailsHalfHeight);
        }
    }
}