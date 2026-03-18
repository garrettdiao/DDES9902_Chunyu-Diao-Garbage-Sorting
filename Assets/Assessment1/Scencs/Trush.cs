using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragByInterface : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int correctBinID;
    [TextArea] public string wrongBinMessage;
    public PopupUI popupUI;

    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector2 startPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        startPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        bool droppedOnBin = false;
        bool isCorrectBin = false;

        foreach (RaycastResult result in results)
        {
            BinUI bin = result.gameObject.GetComponent<BinUI>();

            if (bin != null)
            {
                droppedOnBin = true;

                if (bin.binID == correctBinID)
                {
                    isCorrectBin = true;
                }

                break;
            }
        }

        if (isCorrectBin)
        {
            gameObject.SetActive(false);
        }
        else
        {
            rectTransform.anchoredPosition = startPosition;

            if (droppedOnBin && popupUI != null)
            {
                popupUI.ShowPopup(wrongBinMessage);
            }
        }
    }
}