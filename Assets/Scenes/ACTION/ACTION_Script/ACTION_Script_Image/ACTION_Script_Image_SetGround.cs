using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ACTION_Script_Image_SetGround : MonoBehaviour
{
    [SerializeField] private Image image;//自分のイメージ
    // Start is called before the first frame update
    void Start()
    {
        image.sprite = GameManager.instance.groundsprite;//画像をステージデータで指定した画像に差し替える
    }
}