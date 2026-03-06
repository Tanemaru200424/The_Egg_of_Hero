using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodFairy_AttackChecker : MonoBehaviour
{
    [SerializeField] private WoodFairy_ActionManager actionmanager = null;
    [SerializeField] private int attacknum = 0;
    [SerializeField] private int attackweight = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            actionmanager.attackarray[attacknum] = attackweight;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            actionmanager.attackarray[attacknum] = 0;
        }
    }
}
