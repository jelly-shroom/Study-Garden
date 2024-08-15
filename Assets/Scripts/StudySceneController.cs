using System.Collections;
using UnityEngine;

public class StudySceneController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClipList;
    private int audioClipIndex = 0;
    private Coroutine audioCoroutine;

    void Start()
    {
        // Start the coroutine that plays audio clips sequentially
        audioCoroutine = StartCoroutine(PlayClipsSequentially());
    }

    private IEnumerator PlayClipsSequentially()
    {
        while (true)
        {
            // Play the current clip
            audioSource.clip = audioClipList[audioClipIndex];
            audioSource.Play();

            // Wait until the clip has finished playing
            yield return new WaitForSeconds(audioSource.clip.length);

            // Move to the next clip
            audioClipIndex++;

            // If we've reached the end of the list, reset the index to 0
            if (audioClipIndex >= audioClipList.Length)
            {
                audioClipIndex = 0;
            }
        }
    }

    // This is called when the object is disabled
    void OnDisable()
    {
        // Stop the coroutine
        if (audioCoroutine != null)
        {
            StopCoroutine(audioCoroutine);
        }

        // Stop the audio playback
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
