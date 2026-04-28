using UnityEngine;

public class AutoDoorTrigger : MonoBehaviour
{
    public Transform doorMesh;
    public Vector3 openOffset = new Vector3(2.5f, 0f, 0f);
    public float openSpeed = 1.2f;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool shouldOpen = false;

    void Start()
    {
        closedPosition = doorMesh.position;
        openPosition = closedPosition + openOffset;
    }

    void Update()
    {
        if (shouldOpen)
        {
            doorMesh.position = Vector3.MoveTowards(
                doorMesh.position,
                openPosition,
                openSpeed * Time.deltaTime
            );
        }
        else
        {
            doorMesh.position = Vector3.MoveTowards(
                doorMesh.position,
                closedPosition,
                openSpeed * Time.deltaTime
            );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter: " + other.name);
        shouldOpen = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit: " + other.name);
        shouldOpen = false;
    }
}
