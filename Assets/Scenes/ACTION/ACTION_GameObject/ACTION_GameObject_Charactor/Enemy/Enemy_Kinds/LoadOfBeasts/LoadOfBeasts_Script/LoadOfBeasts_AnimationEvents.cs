using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOfBeasts_AnimationEvents : MonoBehaviour
{
    //ハリボテに付けることでアニメーションの再生時間に応じて関数を実行
    [SerializeField] private LoadOfBeasts_ActionManager actionmanager = null;

    [SerializeField] private BoxCollider2D attack1bc2d = null;//攻撃オブジェクトの当たり判定
    [SerializeField] private BoxCollider2D attack2bc2d = null;//攻撃オブジェクトの当たり判定
    [SerializeField] private BoxCollider2D attack3bc2d = null;//攻撃オブジェクトの当たり判定

    [SerializeField] private SpriteRenderer attack1rangesprend = null;//攻撃範囲オブジェクトのマーカー
    [SerializeField] private SpriteRenderer attack2rangesprend = null;//攻撃範囲オブジェクトのマーカー
    [SerializeField] private SpriteRenderer attack3rangesprend = null;//攻撃範囲オブジェクトのマーカー
    [SerializeField] private SpriteRenderer attack4rangesprend = null;//攻撃範囲オブジェクトのマーカー

    [SerializeField] private SpriteRenderer attack1sprend = null;//攻撃オブジェクトのマーカー
    [SerializeField] private SpriteRenderer attack2sprend = null;//攻撃オブジェクトのマーカー
    [SerializeField] private SpriteRenderer attack3sprend = null;//攻撃オブジェクトのマーカー

    [SerializeField] private GameObject attack4posobject = null;//攻撃4オブジェクトの生成位置
    [SerializeField] private GameObject attack4object = null;//攻撃4オブジェクト

    [SerializeField] private AudioClip sporn;
    [SerializeField] private AudioClip dead;
    [SerializeField] private AudioClip attack1;
    [SerializeField] private AudioClip attack2;
    [SerializeField] private AudioClip attack3;
    [SerializeField] private AudioClip attack4;

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
        attack2rangesprend.enabled = true;
    }
    private void Attack2_2()
    {
        attack2rangesprend.enabled = false;
        attack2sprend.enabled = true;
        attack2bc2d.enabled = true;
        sesource.PlayOneShot(attack2);
    }
    private void Attack2_3()
    {
        attack2sprend.enabled = false;
        attack2bc2d.enabled = false;
    }

    //Attack3中に実行
    private void Attack3_1()
    {
        attack3rangesprend.enabled = true;
    }
    private void Attack3_2()
    {
        attack3rangesprend.enabled = false;
        attack3sprend.enabled = true;
        attack3bc2d.enabled = true;
        sesource.PlayOneShot(attack3);
    }
    private void Attack3_3()
    {
        attack3sprend.enabled = false;
        attack3bc2d.enabled = false;
    }

    //Attack4中に実行
    private void Attack4_1()
    {
        attack4posobject.transform.position = actionmanager.target.transform.position;
        attack4rangesprend.enabled = true;
    }
    private void Attack4_2()
    {
        attack4rangesprend.enabled = false;
        sesource.PlayOneShot(attack4);
        Instantiate(attack4object, attack4posobject.transform.position, Quaternion.Euler(0, 0, 0));//攻撃生成
    }

    //攻撃状態のリセット
    private void AllFalse()
    {
        attack1bc2d.enabled = false;
        attack2bc2d.enabled = false;
        attack3bc2d.enabled = false;

        attack1rangesprend.enabled = false;
        attack2rangesprend.enabled = false;
        attack3rangesprend.enabled = false;
        attack4rangesprend.enabled = false;

        attack1sprend.enabled = false;
        attack2sprend.enabled = false;
        attack3sprend.enabled = false;
    }
}
