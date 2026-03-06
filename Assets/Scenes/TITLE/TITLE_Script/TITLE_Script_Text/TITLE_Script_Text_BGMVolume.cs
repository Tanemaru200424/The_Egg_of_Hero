using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TITLE_Script_Text_BGMVolume : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text text = null;
    private AudioSource bgmsource = null;

    void Start()
    {
        bgmsource = GameManager.instance.bgmsource;
    }

    void Update()
    {
        if (text != null) 
        {
            text.text = (int)(bgmsource.volume * 100 + 0.01f) + " %";
        }
    }
}
