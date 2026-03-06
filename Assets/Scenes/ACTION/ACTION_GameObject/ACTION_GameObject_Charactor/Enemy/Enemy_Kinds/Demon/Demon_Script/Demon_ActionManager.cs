using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Demon_ActionManager : MonoBehaviour
{
    [SerializeField] private float maxcooltime = 0;
    private float cooltime = 0;
    public int[] attackarray = new int[4];

    [SerializeField] private float maxwalkchangetime = 0;
    private float walkchangetime = 0;
    [SerializeField] private int[] walkarray = new int[6];

    [SerializeField] private Rigidbody2D rb2d = null;
    [SerializeField] private float walkspeed = 0.0f;
    [SerializeField] private float attack1speed = 0.0f;
    [SerializeField] private float attack4speed = 0.0f;
    private Vector2 movederection = new Vector2();

    [SerializeField] private Animator animator = null;

    public GameObject target = null;

    void Start()
    {
        cooltime = maxcooltime;
        walkchangetime = 0;
        for (int i = 0; i < attackarray.Length; i++) 
        {
            attackarray[i] = 0;
        }
        target = SearchedObject("Player");
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            cooltime = maxcooltime;
            walkchangetime = 0;
            animator.SetBool("walk",false);
            rb2d.velocity = new Vector2(0, 0);
            target = SearchedObject("Player");
        }
        else
        {
            if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Stand")) ||
                 animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            {
                cooltime -= Time.deltaTime;
                walkchangetime -= Time.deltaTime;
            }

            if (cooltime <= 0)
            {
                switch (SelectActionNumber(attackarray))
                {
                    case 0:
                        animator.SetTrigger("attack1");
                        cooltime = maxcooltime;
                        break;
                    case 1:
                        animator.SetTrigger("attack2");
                        cooltime = maxcooltime;
                        break;
                    case 2:
                        animator.SetTrigger("attack3");
                        cooltime = maxcooltime;
                        break;
                    case 3:
                        animator.SetTrigger("attack4");
                        cooltime = maxcooltime;
                        break;
                    case -1:
                        cooltime = 0;
                        break;
                    default:
                        break;
                }
            }

            if (walkchangetime <= 0)
            {
                switch (SelectActionNumber(walkarray))
                {
                    case 0:
                        movederection = DerectionControl(target.transform.position);
                        break;
                    case 1:
                        if (target.transform.position.x - this.transform.position.x > 0.1f)
                        {
                            movederection = new Vector2(1, 0);
                        }
                        else if (target.transform.position.x - this.transform.position.x < -0.1f)
                        {
                            movederection = new Vector2(-1, 0);
                        }
                        else
                        {
                            movederection = new Vector2(0, 0);
                        }
                        break;
                    case 2:
                        if (target.transform.position.y - this.transform.position.y > 0.1f)
                        {
                            movederection = new Vector2(0, 1);
                        }
                        else if (target.transform.position.y - this.transform.position.y < -0.1f)
                        {
                            movederection = new Vector2(0, -1);
                        }
                        else
                        {
                            movederection = new Vector2(0, 0);
                        }
                        break;
                    case 3:
                        movederection = -DerectionControl(target.transform.position);
                        break;
                    case 4:
                        if (target.transform.position.x - this.transform.position.x > 0.1f)
                        {
                            movederection = new Vector2(-1, 0);
                        }
                        else if (target.transform.position.x - this.transform.position.x < -0.1f)
                        {
                            movederection = new Vector2(1, 0);
                        }
                        else
                        {
                            movederection = new Vector2(0, 0);
                        }
                        break;
                    case 5:
                        if (target.transform.position.y - this.transform.position.y > 0.1f)
                        {
                            movederection = new Vector2(0, -1);
                        }
                        else if (target.transform.position.y - this.transform.position.y < -0.1f)
                        {
                            movederection = new Vector2(0, 1);
                        }
                        else
                        {
                            movederection = new Vector2(0, 0);
                        }
                        break;
                    default:
                        movederection = new Vector2(0, 0);
                        break;
                }
                walkchangetime = maxwalkchangetime;
            }

            if(movederection == new Vector2(0, 0))
            {
                animator.SetBool("walk", false);
            }
            else
            {
                animator.SetBool("walk", true);
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Sporn") ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("Stand") ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1Pre") ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2Pre") ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3Pre") ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("Attack4Pre") ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("Attack4") ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            {
                if(target.transform.position.x > this.transform.position.x)
                {
                    this.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    this.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            }

            if(animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            {
                rb2d.velocity = movederection * walkspeed;
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
            {
                rb2d.velocity = transform.right * attack1speed;
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack4"))
            {
                rb2d.velocity = DerectionControl(target.transform.position) * attack4speed;
            }
            else
            {
                rb2d.velocity = new Vector2(0, 0);
            }
        }
    }

    //アクション番号抽選
    int SelectActionNumber(int[] ActionWeight)
    {
        int Total = 0;//優先度合計
        int Number = 0;//リターン番号

        //優先度合計
        foreach (int Elem in ActionWeight)
        {
            Total += Elem;
        }

        if (Total <= 0)
        {
            return -1;
        }
        else
        {
            //合計に0から1をかける
            float RandomPoint = Random.value * Total;

            //アクション番号抽選
            for (; Number < ActionWeight.Length; Number++)
            {
                if (RandomPoint < ActionWeight[Number])
                {
                    break;
                }
                else
                {
                    RandomPoint -= ActionWeight[Number];
                }
            }
            return Number;
        }
    }

    //ターゲットを探索する関数
    GameObject SearchedObject(string targettag)
    {
        float tmpDis = 0;           //距離用一時変数
        float nearDis = 0;          //最も近いオブジェクトの距離
        //string nearObjName = "";    //オブジェクト名称
        GameObject target = null; //オブジェクト

        //タグ指定されたオブジェクトを配列で取得する

        foreach (GameObject candidate in GameObject.FindGameObjectsWithTag(targettag))
        {
            //自身と取得したオブジェクトの距離を取得
            tmpDis = Vector3.Distance(candidate.transform.position, this.transform.position);

            //オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
            //一時変数に距離を格納
            if (nearDis == 0 || nearDis > tmpDis)
            {
                nearDis = tmpDis;
                //nearObjName = obs.name;
                target = candidate;
            }

        }
        //最も近かったオブジェクトを返す
        return target;
    }

    //向き調整
    Vector2 DerectionControl(Vector3 targetpos)
    {
        float XDefference, YDefference, Cos, Sin;
        XDefference = targetpos.x - this.transform.position.x;
        YDefference = targetpos.y - this.transform.position.y;
        if (Mathf.Abs(XDefference) > 0.1f)
        {
            Cos = XDefference / Mathf.Sqrt(XDefference * XDefference + YDefference * YDefference);
        }
        else
        {
            Cos = 0.0f;
        }
        if (Mathf.Abs(YDefference) > 0.1f)
        {
            Sin = YDefference / Mathf.Sqrt(XDefference * XDefference + YDefference * YDefference);
        }
        else
        {
            Sin = 0.0f;
        }
        return new Vector2(Cos, Sin);
    }
}
