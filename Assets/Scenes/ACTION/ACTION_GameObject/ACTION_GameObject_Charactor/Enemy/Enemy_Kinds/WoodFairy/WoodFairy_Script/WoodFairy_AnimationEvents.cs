using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodFairy_AnimationEvents : MonoBehaviour
{
    //ハリボテに付けることでアニメーションの再生時間に応じて関数を実行
    [SerializeField] private WoodFairy_ActionManager actionmanager = null;

    [SerializeField] private SpriteRenderer attack1rangesprend = null;//攻撃範囲オブジェクトのマーカー
    [SerializeField] private SpriteRenderer attack2rangesprend = null;//攻撃範囲オブジェクトのマーカー

    [SerializeField] private GameObject attack1posobject = null;//攻撃1オブジェクトの生成位置
    [SerializeField] private GameObject attack2posobject = null;//攻撃2オブジェクトの生成位置

    [SerializeField] private GameObject attack1object = null;//攻撃オブジェクト
    [SerializeField] private GameObject attack2object = null;//攻撃オブジェクト

    [SerializeField] private AudioClip sporn;
    [SerializeField] private AudioClip dead;
    [SerializeField] private AudioClip attack1;
    [SerializeField] private AudioClip attack2;

    private AudioSource sesource = null;

    void Start()
    {
        AllFalse();
        sesource = GameManager.instance.sesource;
    }

    //Sporn中に実行
    private void Sporn()
    {
        AllFalse();
        if(sesource == null) 
        {
            sesource = GameManager.instance.sesource;
        }
        sesource.PlayOneShot(sporn);
    }

    //Down中に実行
    private void Down()
    {
        AllFalse();
    }

    //Dead中に実行
    private void Dead_1()
    {
        AllFalse();
        sesource.PlayOneShot(dead);
    }
    private void Dead_2()
    {
        sesource.PlayOneShot(dead);
    }
    private void Dead_3()
    {
        ACTIONManager.instance.enemynum -= 1;
        if (ACTIONManager.instance.enemynum <= 0)
        {
            if (ACTIONManager.instance.wavenum == GameManager.instance.enemieslists.Count)
            {
                ACTIONManager.instance.StartCoroutine("Clear");
            }
            else
            {
                ACTIONManager.instance.StartCoroutine("WaveStart");
            }
        }
        Destroy(this.gameObject);
    }

    //Attack1中に実行
    private void Attack1_1()
    {
        attack1rangesprend.enabled = true;
    }
    private void Attack1_2()
    {
        attack1rangesprend.enabled = false;
        sesource.PlayOneShot(attack1);
        Instantiate(attack1object, attack1posobject.transform.position, this.transform.rotation);//攻撃生成
    }

    //Attack2中に実行
    private void Attack2_1()
    {
        attack2posobject.transform.position = actionmanager.target.transform.position;
        attack2rangesprend.enabled = true;
    }
    private void Attack2_2()
    {
        attack2rangesprend.enabled = false;
        sesource.PlayOneShot(attack2); 
        Instantiate(attack2object, attack2posobject.transform.position, Quaternion.Euler(0, 0, 0));//攻撃生成
    }

    //攻撃状態のリセット
    private void AllFalse()
    {
        attack1rangesprend.enabled = false;
        attack2rangesprend.enabled = false;
    }
}
