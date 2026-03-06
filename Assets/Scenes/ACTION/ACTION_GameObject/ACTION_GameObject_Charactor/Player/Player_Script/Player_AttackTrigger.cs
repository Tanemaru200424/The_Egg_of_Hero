using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AttackTrigger : MonoBehaviour
{
    //追撃トリガー。弱強攻撃の1段2段につける
    [SerializeField] private Animator animator = null;
    private bool ishit = false;

    [SerializeField] private BoxCollider2D bc2d = null;

    void Start()
    {
        ishit = false;
    }

    void FixedUpdate()
    {
        if (!bc2d.enabled)
        {
            ishit = false;
        }
    }

    //敵に当たった時
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && !ishit)
        {
            ishit = true;
            animator.SetTrigger("pursuit");
        }
    }
}
