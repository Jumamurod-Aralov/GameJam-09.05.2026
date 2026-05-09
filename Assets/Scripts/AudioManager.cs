using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource source;

    public AudioClip winSound;
    public AudioClip loseSound;

    void Awake()
    {
        Instance = this;
    }

    public void PlayWin()
    {
        if (winSound != null)
            source.PlayOneShot(winSound);
    }

    public void PlayLose()
    {
        if (loseSound != null)
            source.PlayOneShot(loseSound);
    }
}