using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    public int counter;
    [SerializeField] TextMeshProUGUI currency;
    [SerializeField] CurrencyHelper currencyCounter;
    private void Start()
    {
        currency = GetComponent<TextMeshProUGUI>();
        currencyCounter = FindAnyObjectByType<CurrencyHelper>();
    }

    private void Update()
    {
        currency.text = "" + counter;
    }

}
