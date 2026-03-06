using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TITLE_Script_Button_ToPlay : MonoBehaviour
{
    [SerializeField] private AudioClip clip = null;
    private AudioSource sesource = null;

    [SerializeField] private Sprite groundsprite = null;//地面の画像スプライト
    [SerializeField] private Sprite skysprite = null;//空の画像スプライト

    [SerializeField] private List<GameManager.enemieslist> enemieslists = new List<GameManager.enemieslist>();

    void Start()
    {
        sesource = GameManager.instance.sesource;
    }


    public void OnClick()
    {
        sesource.PlayOneShot(clip);
        GameManager.instance.groundsprite = groundsprite;
        GameManager.instance.skysprite = skysprite;
        GameManager.instance.enemieslists = enemieslists;
        TITLEManager.instance.StartCoroutine("ToACTIONScene");
    }
}
