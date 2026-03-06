using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TITLE_Script_Button_Exit : MonoBehaviour
{
    [SerializeField] private AudioClip clip = null;
    private AudioSource sesource = null;

    void Start()
    {
        sesource = GameManager.instance.sesource;
    }

    public void OnClick()
    {
        sesource.PlayOneShot(clip);
        TITLEManager.instance.StartCoroutine("ExitGame");
    }
}
