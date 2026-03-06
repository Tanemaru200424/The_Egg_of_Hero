using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AttackPower : MonoBehaviour
{
    //攻撃判定に取り付ける。攻撃力を指定。ヒット時相手のダメージ関数を呼び出す。
    [SerializeField] private int power = 0;

    [SerializeField] private AudioClip hit = null;

    private AudioSource sesource = null;

    private bool ishit = false;

    [SerializeField] private List<GameObject> hitlist = new List<GameObject>();

    [SerializeField] private BoxCollider2D bc2d = null;

    void Start()
    {
        sesource = GameManager.instance.sesource;
        ishit = false;
    }

    void FixedUpdate()
    {
        if (!bc2d.enabled)
        {
            hitlist.Clear();
            ishit = false;
        }
    }

    //敵に当たった時
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !hitlist.Contains(other.gameObject))
        {
            hitlist.Add(other.gameObject);
            IDamageable damageable = other.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.Damage(power);
                if (!ishit)
                {
                    ishit = true;
                    sesource.PlayOneShot(hit);
                }
            }
        }
    }
}
