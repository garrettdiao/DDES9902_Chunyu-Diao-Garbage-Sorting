using TMPro;
using UnityEngine;

public class PopupUI : MonoBehaviour
{
    public GameObject popupPanel;
    public TextMeshProUGUI popupText;

    public void ShowPopup(string message)
    {
        popupPanel.SetActive(true);
        popupText.text = message;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HidePopup()
    {
        popupPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}