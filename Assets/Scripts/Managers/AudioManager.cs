using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    public AudioClip[] audioClipList;
    private int audioClipIndex;
    private GameObject studyScene;

    private enum TimeOfDay { Morning, Dusk, Night }
    private TimeOfDay currentTimeOfDay;

    // Start is called before the first frame update
    void Start()
    {
        studyScene = GameObject.FindObjectOfType<StudySceneController>(true).gameObject;
        DetermineTimeOfDay();
        PlayAudioClip();
    }

    // Update is called once per frame
    void Update()
    {
        if (studyScene.activeSelf && audioSource.isPlaying)
        {
            // Pause the audio when the study scene is active
            audioSource.Stop();
        }
        else if (!studyScene.activeSelf && !audioSource.isPlaying)
        {
            // Resume the audio when the study scene is inactive
            audioSource.Play();
        }
    }

    void DetermineTimeOfDay()
    {
        System.DateTime currentTime = System.DateTime.Now;
        int hour = currentTime.Hour;

        if (hour >= 6 && hour < 12)
        {
            currentTimeOfDay = TimeOfDay.Morning;
            audioClipIndex = 0;
        }
        else if (hour >= 12 && hour < 18)
        {
            currentTimeOfDay = TimeOfDay.Dusk;
            audioClipIndex = 1;
        }
        else
        {
            currentTimeOfDay = TimeOfDay.Night;
            audioClipIndex = 2;
        }
    }

    void PlayAudioClip()
    {
        if (audioClipList.Length > audioClipIndex)
        {
            audioSource.clip = audioClipList[audioClipIndex];
            audioSource.loop = true; // Ensure the clip loops infinitely
            audioSource.Play();
        }
    }
}
