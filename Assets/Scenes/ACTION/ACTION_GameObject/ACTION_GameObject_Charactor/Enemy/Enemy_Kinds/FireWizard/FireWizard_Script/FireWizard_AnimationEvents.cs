using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWizard_AnimationEvents : MonoBehaviour
{
    //ハリボテに付けることでアニメーションの再生時間に応じて関数を実行
    [SerializeField] private BoxCollider2D attack1bc2d = null;//攻撃オブジェクトの当たり判定

    [SerializeField] private SpriteRenderer attack1rangesprend = null;//攻撃範囲オブジェクトのマーカー
    [SerializeField] private SpriteRenderer attack2_1rangesprend = null;//攻撃範囲オブジェクトのマーカー
    [SerializeField] private SpriteRenderer attack2_2rangesprend = null;//攻撃範囲オブジェクトのマーカー
    [SerializeField] private SpriteRenderer attack2_3rangesprend = null;//攻撃範囲オブジェクトのマーカー

    [SerializeField] private SpriteRenderer attack1sprend = null;//攻撃オブジェクトのマーカー

    [SerializeField] private GameObject attack2_1posobject = null;//攻撃2オブジェクトの生成位置
    [SerializeField] private GameObject attack2_2posobject = null;//攻撃2オブジェクトの生成位置
    [SerializeField] private GameObject attack2_3posobject = null;//攻撃2オブジェクトの生成位置
    [SerializeField] private GameObject attack2object = null;//攻撃2オブジェクト

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
        attack1sprend.enabled = true;
        attack1bc2d.enabled = true;
        sesource.PlayOneShot(attack1);
    }
    private void Attack1_3()
    {
        attack1sprend.enabled = false;
        attack1bc2d.enabled = false;
    }

    //Attack2中に実行
    private void Attack2_1()
    {
        attack2_1rangesprend.enabled = true;
    }
    private void Attack2_2()
    {
        attack2_1rangesprend.enabled = false;
        sesource.PlayOneShot(attack2);
        Instantiate(attack2object, attack2_1posobject.transform.position, this.transform.rotation);//攻撃生成
        attack2_2rangesprend.enabled = true;
    }
    private void Attack2_3()
    {
        attack2_2rangesprend.enabled = false;
        sesource.PlayOneShot(attack2);
        Instantiate(attack2object, attack2_2posobject.transform.position, this.transform.rotation);//攻撃生成
        attack2_3rangesprend.enabled = true;
    }
    private void Attack2_4()
    {
        attack2_3rangesprend.enabled = false;
        sesource.PlayOneShot(attack2);
        Instantiate(attack2object, attack2_3posobject.transform.position, this.transform.rotation);//攻撃生成
    }

    //攻撃状態のリセット
    private void AllFalse()
    {
        attack1bc2d.enabled = false;

        attack1rangesprend.enabled = false;
        attack2_1rangesprend.enabled = false;
        attack2_2rangesprend.enabled = false;
        attack2_3rangesprend.enabled = false;

        attack1sprend.enabled = false;
    }
}
