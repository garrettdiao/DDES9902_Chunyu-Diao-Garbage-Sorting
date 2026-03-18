using TMPro;
using UnityEngine;

public class PopupUI : MonoBehaviour
{
    public GameObject popupPanel;
    public TextMeshProUGUI popupText;

    public void ShowPopup(string message)
    {
        if (popupPanel == null)
        {
            Debug.LogError("popupPanel is not assigned on: " + gameObject.name);
            return;
        }

        if (popupText == null)
        {
            Debug.LogError("popupText is not assigned on: " + gameObject.name);
            return;
        }

        popupPanel.SetActive(true);
        popupText.text = message;
    }

    public void HidePopup()
    {
        if (popupPanel != null)
        {
            popupPanel.SetActive(false);
        }
    }
}