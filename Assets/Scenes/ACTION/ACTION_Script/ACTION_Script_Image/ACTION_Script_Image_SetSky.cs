using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ACTION_Script_Image_SetSky : MonoBehaviour
{
    [SerializeField] private Image image;//自分のイメージ
    // Start is called before the first frame update
    void Start()
    {
        image.sprite = GameManager.instance.skysprite;//画像をステージデータで指定した画像に差し替える
    }
}
