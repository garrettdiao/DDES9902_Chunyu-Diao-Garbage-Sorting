using System.Collections;
using UnityEngine;

public class BinTrigger : MonoBehaviour
{
    public int binID;
    public WorldSignUI worldSignUI;
    public PollutionEffectController pollutionEffectController;
    public AudioManager audioManager;

    private void OnTriggerEnter(Collider other)
    {
        Trash3D trash = other.GetComponent<Trash3D>();
        if (trash == null) return;

        StartCoroutine(CheckTrashAfterDelay(trash, other.gameObject));
    }

    private IEnumerator CheckTrashAfterDelay(Trash3D trash, GameObject trashObject)
    {
        yield return new WaitForSeconds(0.15f);

        if (trash == null) yield break;
        if (trash.isHeld) yield break;

        if (trash.correctBinID == binID)
        {
            if (audioManager != null)
            {
                audioManager.PlayCorrect();
            }

            trashObject.SetActive(false);
        }
        else
        {
            trash.ResetPosition();

            if (worldSignUI != null)
            {
                worldSignUI.ShowMessage(trash.wrongBinMessage);
            }

            if (pollutionEffectController != null)
            {
                pollutionEffectController.AddPollution();
            }

            if (audioManager != null)
            {
                audioManager.PlayWrong();
            }
        }
    }
}
