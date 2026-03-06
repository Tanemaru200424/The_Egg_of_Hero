using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodFairy_Lazer_Move : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d = null;
    [SerializeField] private float speed = 0;
    [SerializeField] private float range = 0;

    private float createxpos = 0;

    void Start()
    {
        createxpos = this.transform.position.x;
    }

    void FixedUpdate()
    {
        if (Math.Abs(this.transform.position.x - createxpos) > range)
        {
            Destroy(this.gameObject);
        }
        rb2d.velocity = transform.right * speed;
    }
}
