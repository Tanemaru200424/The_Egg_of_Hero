using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TITLE_Script_Button_UIOpen : MonoBehaviour
{
    [SerializeField] private AudioClip clip = null;
    private AudioSource sesource = null;

    [SerializeField] private GameObject closeui = null;
    [SerializeField] private GameObject openui = null;
    [SerializeField] private GameObject setbutton = null;

    void Start()
    {
        sesource = GameManager.instance.sesource;
    }

    public void OnClick()
    {
        sesource.PlayOneShot(clip);
        closeui.SetActive(false);
        openui.SetActive(true);
        TITLEManager.instance.firstsetbutton = this.gameObject;
        TITLEManager.instance.nowgroup = openui;
        TITLEManager.instance.canclose = true;
        EventSystem.current.SetSelectedGameObject(setbutton);
    }
}
