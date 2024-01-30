using UnityEngine;

public class BAckground : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play(); // This plays the audio clip.
    }
}