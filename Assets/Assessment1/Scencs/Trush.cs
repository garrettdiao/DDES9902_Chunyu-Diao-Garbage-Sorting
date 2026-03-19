using System.Collections;
using UnityEngine;

public class Trash3D : MonoBehaviour
{
    public int correctBinID;
    [TextArea] public string wrongBinMessage;
    public bool isHeld = false;

    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    public void ResetPosition()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        transform.position = startPosition;
        transform.rotation = startRotation;
    }

    public void SetHeldState(bool heldState)
    {
        isHeld = heldState;
    }

    public void ReleaseAfterDelay(float delay)
    {
        StartCoroutine(ReleaseCoroutine(delay));
    }

    private IEnumerator ReleaseCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        isHeld = false;
    }
}