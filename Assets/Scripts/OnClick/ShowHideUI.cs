using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShowHideUI : MonoBehaviour
{
    [SerializeField] public GameObject _gameObj;
    [SerializeField] public string _text1;
    [SerializeField] public string _text2;
    public TextMeshProUGUI buttonText;
    int counter;

    public void ShowHide()
    {
        _gameObj.gameObject.SetActive(!_gameObj.gameObject.activeSelf);
    }

    public void SetText()
    {
        counter++;
        if (counter%2 == 1)
        {
            buttonText.text = _text1;
        } else {
            buttonText.text = _text2;
        }
    }
}
