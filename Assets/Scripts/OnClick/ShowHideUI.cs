using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShowHideUI : MonoBehaviour
{
    [SerializeField] public GameObject _gameObj;
    public TextMeshProUGUI buttonText;

    public void ShowHide()
    {
        _gameObj.gameObject.SetActive(!_gameObj.gameObject.activeSelf);
    }

}
