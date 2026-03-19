using UnityEngine;

public class BinTrigger : MonoBehaviour
{
    public int binID;

    private void OnTriggerEnter(Collider other)
    {
        Trash3D trash = other.GetComponent<Trash3D>();

        if (trash == null)
        {
            Debug.Log("No Trash3D found on: " + other.gameObject.name);
            return;
        }

        if (trash.isHeld)
        {
            Debug.Log("Object is still being held: " + other.gameObject.name);
            return;
        }

        Debug.Log("Trash: " + other.gameObject.name +
                  " | Trash correctBinID: " + trash.correctBinID +
                  " | Current binID: " + binID);

        if (trash.correctBinID == binID)
        {
            Debug.Log("Correct bin");
            other.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Wrong bin");
            trash.ResetPosition();
        }
    }
}