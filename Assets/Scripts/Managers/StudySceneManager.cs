using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class StudySceneManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    private AudioClip[] audioClipList;
    private int audioClipIndex = 0;
    private Coroutine audioCoroutine;

    [SerializeField] VideoPlayer videoPlayer;
    private VideoClip[] videoClipList;
    private int videoClipIndex = 0;

    void OnEnable()
    {
        audioClipList = Resources.LoadAll<AudioClip>("Audio/Study");
        videoClipList = Resources.LoadAll<VideoClip>("bg");

        // Make sure there are clips to play
        if (audioClipList.Length == 0 || videoClipList.Length == 0)
        {
            Debug.LogWarning("No audio or video clips found in the specified folders.");
            return;
        }

        audioClipIndex = Random.Range(0, audioClipList.Length);
        videoClipIndex = Random.Range(0, videoClipList.Length);

        PlayVideoClip();

        audioSource.clip = audioClipList[audioClipIndex];
        audioSource.Play();

        // Start the coroutine that plays audio clips sequentially
        audioCoroutine = StartCoroutine(PlayClipsSequentially());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            videoClipIndex++;
            if (videoClipIndex >= videoClipList.Length)
            {
                videoClipIndex = 0;
            }
            PlayVideoClip();
        }
    }

    private void PlayVideoClip()
    {
        videoPlayer.clip = videoClipList[videoClipIndex];
        videoPlayer.Play();
    }

    private IEnumerator PlayClipsSequentially()
    {
        while (true)
        {
            // Wait until the current audio clip finishes playing
            yield return new WaitForSeconds(audioSource.clip.length);

            audioClipIndex++;
            if (audioClipIndex >= audioClipList.Length)
            {
                audioClipIndex = 0;
            }
            audioSource.clip = audioClipList[audioClipIndex];
            audioSource.Play();
        }
    }

    void OnDisable()
    {
        if (audioCoroutine != null)
        {
            StopCoroutine(audioCoroutine);
        }

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
