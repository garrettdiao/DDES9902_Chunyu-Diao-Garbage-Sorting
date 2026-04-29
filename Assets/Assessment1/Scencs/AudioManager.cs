using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip correctSound;
    public AudioClip wrongSound;
    public AudioClip failSound;

    public void PlayCorrect()
    {
        audioSource.PlayOneShot(correctSound);
    }

    public void PlayWrong()
    {
        audioSource.PlayOneShot(wrongSound);
    }

    public void PlayFail()
    {
        audioSource.PlayOneShot(failSound);
    }
}
