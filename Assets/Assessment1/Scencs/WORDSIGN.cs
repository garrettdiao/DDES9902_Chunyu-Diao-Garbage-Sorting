using System.Collections;
using TMPro;
using UnityEngine;
public class WorldSignUI : MonoBehaviour
{
    public GameObject signRoot;
    public TextMeshPro signText;
    public float displayDuration = 3f;
    private Coroutine hideCoroutine;
    public void ShowMessage(string message)
    {
        if (signRoot == null)
        {
            Debug.LogError("signRoot is not assigned.");
            return;
        }
        if (signText == null)
        {
            Debug.LogError("signText is not assigned.");
            return;
        }
        signRoot.SetActive(true);
        signText.text = message;
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }
        hideCoroutine = StartCoroutine(HideAfterDelay());
    }
    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        signRoot.SetActive(false);
    }
}