using UnityEngine;

public class BinTrigger : MonoBehaviour
{
    public int binID;
    public PopupUI popupUI;

    private void OnTriggerEnter(Collider other)
    {
        Trash3D trash = other.GetComponent<Trash3D>();

        if (trash == null) return;

        if (trash.isHeld) return;

        if (trash.correctBinID == binID)
        {
            other.gameObject.SetActive(false);
        }
        else
        {
            trash.ResetPosition();

            if (popupUI != null)
            {
                popupUI.ShowPopup(trash.wrongBinMessage);
            }
        }
    }
}