using UnityEngine;
public class BinTrigger : MonoBehaviour
{
    public int binID;
    public WorldSignUI worldSignUI;
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
            if (worldSignUI != null)
            {
                worldSignUI.ShowMessage(trash.wrongBinMessage);
            }
        }
    }
}