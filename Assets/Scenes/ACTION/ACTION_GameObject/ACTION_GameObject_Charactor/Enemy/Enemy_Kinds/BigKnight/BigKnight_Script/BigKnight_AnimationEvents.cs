using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigKnight_AnimationEvents : MonoBehaviour
{
    //ハリボテに付けることでアニメーションの再生時間に応じて関数を実行
    [SerializeField] private BoxCollider2D attack2bc2d = null;//攻撃オブジェクトの当たり判定
    [SerializeField] private BoxCollider2D attack3bc2d = null;//攻撃オブジェクトの当たり判定
    [SerializeField] private BoxCollider2D attack4bc2d = null;//攻撃オブジェクトの当たり判定

    [SerializeField] private SpriteRenderer attack1rangesprend = null;//攻撃範囲オブジェクトのマーカー
    [SerializeField] private SpriteRenderer attack2rangesprend = null;//攻撃範囲オブジェクトのマーカー
    [SerializeField] private SpriteRenderer attack3rangesprend = null;//攻撃範囲オブジェクトのマーカー
    [SerializeField] private SpriteRenderer attack4rangesprend = null;//攻撃範囲オブジェクトのマーカー

    [SerializeField] private SpriteRenderer attack2sprend = null;//攻撃オブジェクトのマーカー
    [SerializeField] private SpriteRenderer attack3sprend = null;//攻撃オブジェクトのマーカー
    [SerializeField] private SpriteRenderer attack4sprend = null;//攻撃オブジェクトのマーカー

    [SerializeField] private GameObject attack1posobject = null;//攻撃1オブジェクトの生成位置
    [SerializeField] private GameObject attack1object = null;//攻撃1オブジェクト

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
        Instantiate(attack1object, attack1posobject.transform.position, this.transform.rotation);//攻撃生成
        sesource.PlayOneShot(attack1);
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
        attack4rangesprend.enabled = true;
    }
    private void Attack4_2()
    {
        attack4rangesprend.enabled = false;
        attack4sprend.enabled = true;
        attack4bc2d.enabled = true;
        sesource.PlayOneShot(attack4);
    }
    private void Attack4_3()
    {
        attack4sprend.enabled = false;
        attack4bc2d.enabled = false;
    }


    //攻撃状態のリセット
    private void AllFalse()
    {
        attack2bc2d.enabled = false;
        attack3bc2d.enabled = false;
        attack4bc2d.enabled = false;

        attack1rangesprend.enabled = false;
        attack2rangesprend.enabled = false;
        attack3rangesprend.enabled = false;
        attack4rangesprend.enabled = false;

        attack2sprend.enabled = false;
        attack3sprend.enabled = false;
        attack4sprend.enabled = false;
    }
}
