using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_ActionManager : MonoBehaviour
{
    [SerializeField] private Animator animator = null;//キャラクターのアニメーター

    [SerializeField] private PlayerInput playerinput = null;

    [SerializeField] private Rigidbody2D rb2d = null;
    private Vector2 movederection = new Vector2();
    private float xspeed, yspeed;
    [SerializeField] private float walkspeed = 0.0f;
    [SerializeField] private float spornspeed = 0.0f;

    void Start()
    {

    }

    void Update()
    {
        Debug.Log("aaa");
        if (ACTIONManager.instance.ispause)
        {
            playerinput.actions.FindActionMap("Player").Disable();
        }
        else
        {
            playerinput.actions.FindActionMap("Player").Enable();
        }
    }

    void FixedUpdate()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Walk") ||
           animator.GetCurrentAnimatorStateInfo(0).IsName("SpecialAttack"))
        {
            if (movederection.x > 0)
            {
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (movederection.x < 0)
            {
                this.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            xspeed = movederection.x * walkspeed;
            yspeed = movederection.y * walkspeed;
        }
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Sporn"))
        {
            if (movederection.x > 0)
            {
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (movederection.x < 0)
            {
                this.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            xspeed = movederection.x * spornspeed;
            yspeed = movederection.y * spornspeed;
        }
        else
        {
            xspeed = 0.0f;
            yspeed = 0.0f;
        }
        rb2d.velocity = new Vector2(xspeed, yspeed);
    }

    //移動をおしっぱにしているとき
    public void OnWalk(InputAction.CallbackContext context)
    {
        //方向制御
        Vector2 derection = context.ReadValue<Vector2>();
        float Sin, Cos;
        if (derection.x == 0.0f && derection.y == 0.0f)
        {
            Sin = 0.0f;
            Cos = 0.0f;
        }
        else
        {
            Sin = derection.y / (float)System.Math.Sqrt(System.Math.Pow(derection.x, 2) + System.Math.Pow(derection.y, 2));
            Cos = derection.x / (float)System.Math.Sqrt(System.Math.Pow(derection.x, 2) + System.Math.Pow(derection.y, 2));
        }
        movederection = new Vector2(Cos, Sin);

        //アニメーション再生
        if (derection != Vector2.zero)
        {
            animator.SetBool("walk", true);
        }
        else
        {
            animator.SetBool("walk", false);
        }
    }

    public void OnNomalAttack(InputAction.CallbackContext context)
    {
        //アニメーション再生
        if (context.started &&
            (animator.GetCurrentAnimatorStateInfo(0).IsName("Stand") || 
             animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")))
        {
            animator.SetTrigger("nomal");
        }
    }

    //強攻撃を押したとき
    public void OnStrongAttack(InputAction.CallbackContext context)
    {
        //アニメーション再生
        if (context.started &&
            (animator.GetCurrentAnimatorStateInfo(0).IsName("Stand") ||
             animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")))
        {
            animator.SetTrigger("strong");
        }
    }

    //特殊攻撃を押したとき
    public void OnSpecialAttack(InputAction.CallbackContext context)
    {
        //アニメーション再生
        if (context.started &&
            (animator.GetCurrentAnimatorStateInfo(0).IsName("Stand") ||
             animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")))
        {
            animator.SetTrigger("special");
        }
    }

    //カウンターを押したとき
    public void OnCounter(InputAction.CallbackContext context)
    {
        //アニメーション再生
        if (context.started &&
            (animator.GetCurrentAnimatorStateInfo(0).IsName("Stand") ||
             animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")))
        {
            animator.SetTrigger("counter");
        }
    }
}
