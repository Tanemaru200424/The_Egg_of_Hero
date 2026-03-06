using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ACTION_Script_Button_Returngame : MonoBehaviour
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
        ACTIONManager.instance.Returngame();
    }
}
