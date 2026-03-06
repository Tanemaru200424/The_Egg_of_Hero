using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TITLE_Script_Button_BGMDown : MonoBehaviour
{
    [SerializeField] private AudioClip clip = null;
    private AudioSource bgmsource = null;
    private AudioSource sesource = null;

    void Start()
    {
        bgmsource = GameManager.instance.bgmsource;
        sesource = GameManager.instance.sesource;
    }

    public void OnClick()
    {
        sesource.PlayOneShot(clip);
        bgmsource.volume -= 0.1f;
    }
}
