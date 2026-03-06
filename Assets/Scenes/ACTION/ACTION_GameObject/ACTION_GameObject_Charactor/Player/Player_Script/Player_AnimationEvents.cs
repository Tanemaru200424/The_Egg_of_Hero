using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AnimationEvents : MonoBehaviour
{
    //プレイヤーのハリボテに付けることでアニメーションの再生時間に応じて関数を実行
    [SerializeField] private BoxCollider2D nomalattack1bc2d = null;
    [SerializeField] private BoxCollider2D nomalattack2bc2d = null;
    [SerializeField] private BoxCollider2D nomalattack3bc2d = null;
    [SerializeField] private BoxCollider2D strongattack1bc2d = null;
    [SerializeField] private BoxCollider2D strongattack2bc2d = null;
    [SerializeField] private BoxCollider2D strongattack3bc2d = null;
    [SerializeField] private BoxCollider2D specialattackbc2d = null;
    [SerializeField] private BoxCollider2D counterattackbc2d = null;

    [SerializeField] private AudioClip sporn;
    [SerializeField] private AudioClip dead;
    [SerializeField] private AudioClip nomalattack;
    [SerializeField] private AudioClip strongattack;
    [SerializeField] private AudioClip specialattackpre;
    [SerializeField] private AudioClip specialattack;
    [SerializeField] private AudioClip counter;
    [SerializeField] private AudioClip countersuccess;

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
        if (sesource == null)
        {
            sesource = GameManager.instance.sesource;
        }
        sesource.PlayOneShot(sporn);
    }

    //Damage中に実行
    private void Down()
    {
        AllFalse();
    }

    //Death中に実行
    private void Dead_1()
    {
        AllFalse();
    }
    private void Dead_2()
    {
        sesource.PlayOneShot(dead);
    }
    private void Dead_3()
    {
        ACTIONManager.instance.zanki -= 1;
        if (ACTIONManager.instance.zanki > 0)
        {
            ACTIONManager.instance.PlayerSporn();
        }
        else
        {
            ACTIONManager.instance.GameOver();
        }
    }

    //NomalAttack1中に実行
    private void NomalAttack1_1()
    {
        nomalattack1bc2d.enabled = true;
        sesource.PlayOneShot(nomalattack);
    }
    private void NomalAttack1_2()
    {
        nomalattack1bc2d.enabled = false;
        AllFalse();
    }

    //NomalAttack2中に実行
    private void NomalAttack2_1()
    {
        nomalattack2bc2d.enabled = true;
        sesource.PlayOneShot(nomalattack);
    }
    private void NomalAttack2_2()
    {
        nomalattack2bc2d.enabled = false;
    }

    //NomalAttack3中に実行
    private void NomalAttack3_1()
    {
        nomalattack3bc2d.enabled = true;
        sesource.PlayOneShot(nomalattack);
    }
    private void NomalAttack3_2()
    {
        nomalattack3bc2d.enabled = false;
    }

    //StrongAttack1中に実行
    private void StrongAttack1_1()
    {
        strongattack1bc2d.enabled = true;
        sesource.PlayOneShot(strongattack);
    }
    private void StrongAttack1_2()
    {
        strongattack1bc2d.enabled = false;
    }

    //StrongAttack2中に実行
    private void StrongAttack2_1()
    {
        strongattack2bc2d.enabled = true;
        sesource.PlayOneShot(strongattack);
    }
    private void StrongAttack2_2()
    {
        strongattack2bc2d.enabled = false;
    }

    //StrongAttack3中に実行
    private void StrongAttack3_1()
    {
        strongattack3bc2d.enabled = true;
        sesource.PlayOneShot(strongattack);
    }
    private void StrongAttack3_2()
    {
        strongattack3bc2d.enabled = false;
    }

    //SpecialAttackPre中に実行
    private void SpecialAttackPre()
    {
        sesource.PlayOneShot(specialattackpre);
    }

    //SpecialAttack中に実行
    private void SpecialAttack_1()
    {
        specialattackbc2d.enabled = true;
        sesource.PlayOneShot(specialattack);
    }
    private void SpecialAttack_2()
    {
        specialattackbc2d.enabled = false;
    }

    //Counter中に実行
    private void Counter()
    {
        sesource.PlayOneShot(counter);
    }

    //CounterSuccess中に実行
    private void CounterSuccess_1()
    {
        counterattackbc2d.enabled = true;
        sesource.PlayOneShot(countersuccess);
    }
    private void CounterSuccess_2()
    {
        counterattackbc2d.enabled = false;
    }

    //攻撃判定を全部消す
    private void AllFalse()
    {
        nomalattack1bc2d.enabled = false;
        nomalattack2bc2d.enabled = false;
        nomalattack3bc2d.enabled = false;
        strongattack1bc2d.enabled = false;
        strongattack2bc2d.enabled = false;
        strongattack3bc2d.enabled = false;
        specialattackbc2d.enabled = false;
        counterattackbc2d.enabled = false;
    }
}
