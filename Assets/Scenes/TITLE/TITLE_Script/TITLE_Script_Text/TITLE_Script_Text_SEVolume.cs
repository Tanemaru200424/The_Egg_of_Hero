using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TITLE_Script_Text_SEVolume : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text text = null;
    private AudioSource sesource = null;

    void Start()
    {
        sesource = GameManager.instance.sesource;
    }

    void Update()
    {
        if (text != null) 
        {
            text.text = (int)(sesource.volume * 100 + 0.01f) + " %";
        }
    }
}
