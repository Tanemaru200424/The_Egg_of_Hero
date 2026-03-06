using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TITLE_Script_Button_SEUp : MonoBehaviour
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
        sesource.volume += 0.1f;
    }
}
