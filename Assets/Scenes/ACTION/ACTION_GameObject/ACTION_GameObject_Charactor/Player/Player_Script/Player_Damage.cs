using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Damage : MonoBehaviour, IDamageable
{
    //Hpの指定と当たり判定のレイヤー調整。死亡時WaveManagerのプレイヤー生成ポジションを指定
    [SerializeField] private Animator animator = null;//Charactorのアニメーター

    [SerializeField] private GameObject damageeffect = null;//ヒット時のエフェクト
    [SerializeField] private GameObject baseposobject = null;//ヒット時のエフェクトの生成基準位置

    private Vector3 damageeffectpos = new Vector3();//エフェクトの位置

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
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Counter"))
        {
            animator.SetTrigger("success");
        }
        else if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Down") &&
                !animator.GetCurrentAnimatorStateInfo(0).IsName("CounterSuccess"))
        {
            ACTIONManager.instance.hp -= value;
            damageeffectpos = new Vector3
                                (
                                    baseposobject.transform.position.x + Random.Range(-0.5f, 0.5f),
                                    baseposobject.transform.position.y + Random.Range(-0.5f, 0.5f),
                                    baseposobject.transform.position.z
                                );
            Instantiate(damageeffect, damageeffectpos, Quaternion.identity);//エフェクト生成


            if (ACTIONManager.instance.hp > 0)
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("SpecialAttack") &&
                    !animator.GetCurrentAnimatorStateInfo(0).IsName("SpecialAttackPre"))
                {
                    animator.ResetTrigger("nomal");
                    animator.ResetTrigger("strong");
                    animator.ResetTrigger("special");
                    animator.ResetTrigger("counter");
                    animator.ResetTrigger("pursuit");
                    animator.ResetTrigger("success");
                    animator.Play("Down");
                }
            }
            else
            {
                animator.ResetTrigger("nomal");
                animator.ResetTrigger("strong");
                animator.ResetTrigger("special");
                animator.ResetTrigger("counter");
                animator.ResetTrigger("pursuit");
                animator.ResetTrigger("success");
                Death();
            }
        }
    }

    public void Death()
    {
        animator.Play("Dead");
    }
}
