using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class HomeEnvironmentManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] Camera mainCamera;
    public AudioClip[] audioClipList;
    private int audioClipIndex;
    private GameObject studyScene;

    private enum TimeOfDay { Morning, Dusk, Night }
    private TimeOfDay currentTimeOfDay;

    // Colors for different times of day
    public Color morningColor = new Color(0.9f, 0.7f, 0.4f); // Light orange
    public Color duskColor = new Color(0.5f, 0.7f, 0.9f); // Light blue
    public Color nightColor = new Color(0.1f, 0.1f, 0.3f); // Dark blue

    public GameObject exclusionParentObject;

    void Start()
    {
        studyScene = GameObject.FindObjectOfType<StudySceneManager>(true).gameObject;
        DetermineTimeOfDay();
        ChangeCameraBackground();
        PlayAudioClip();
    }

    void Update()
    {
        if (studyScene.activeSelf && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        else if (!studyScene.activeSelf && !audioSource.isPlaying)
        {
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

        ChangeCameraBackground();
        UpdateTextColors();
    }

    void PlayAudioClip()
    {
        if (audioClipList.Length > audioClipIndex)
        {
            audioSource.clip = audioClipList[audioClipIndex];
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    void ChangeCameraBackground()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        switch (currentTimeOfDay)
        {
            case TimeOfDay.Morning:
                mainCamera.backgroundColor = morningColor;
                break;
            case TimeOfDay.Dusk:
                mainCamera.backgroundColor = duskColor;
                break;
            case TimeOfDay.Night:
                mainCamera.backgroundColor = nightColor;
                break;
        }
    }

    void UpdateTextColors()
    {
        if (currentTimeOfDay == TimeOfDay.Night)
        {
            TextMeshProUGUI[] allText = FindObjectsOfType<TextMeshProUGUI>(true);

            foreach (TextMeshProUGUI text in allText)
            {
                // Check if the text element is active, not in the exclusion list, and not a child of the exclusionParentObject
                if (text.gameObject.activeInHierarchy &&
                    !IsChildOfExclusionParent(text.gameObject))
                {
                    text.color = Color.white;
                }
            }
        }
    }

    bool IsChildOfExclusionParent(GameObject obj)
    {
        if (exclusionParentObject == null)
        {
            return false; // No exclusion parent is set
        }

        // Check if the object's parent or any ancestor is the exclusion parent object
        return obj.transform.IsChildOf(exclusionParentObject.transform);
    }
}
