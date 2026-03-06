using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Damage : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxhp = 0;
    private int hp = 0;
    private int downpoint = 0;

    [SerializeField] private Animator animator = null;

    [SerializeField] private GameObject damageeffect = null;//ヒット時のエフェクト
    [SerializeField] private GameObject baseposobject = null;//ヒット時のエフェクトの生成基準位置

    private Vector3 damageeffectpos = new Vector3();//エフェクトの位置

    void Start()
    {
        hp = maxhp;
        downpoint = maxhp / 2;
    }

    void FixedUpdate()
    {
        //当たり判定レイヤー調整
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Sporn") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
        {
            this.gameObject.layer = LayerMask.NameToLayer("Muteki");
        }
        else
        {
            this.gameObject.layer = LayerMask.NameToLayer("Charactor");
        }
    }

    //ダメージを受けたら呼び出される
    public void Damage(int value)
    {
        hp -= value;
        downpoint -= value;

        damageeffectpos = new Vector3
                            (
                                baseposobject.transform.position.x + Random.Range(-0.5f, 0.5f),
                                baseposobject.transform.position.y + Random.Range(-0.5f, 0.5f),
                                baseposobject.transform.position.z
                            );
        Instantiate(damageeffect, damageeffectpos, Quaternion.identity);//エフェクト生成

        if (downpoint <= 0)
        {
            animator.ResetTrigger("attack1");
            downpoint = maxhp / 2;
            animator.Play("Down");
        }
        if (hp <= 0)
        {
            animator.ResetTrigger("attack1");
            Death();
        }
    }

    public void Death()
    {
        animator.Play("Dead");
    }
}
