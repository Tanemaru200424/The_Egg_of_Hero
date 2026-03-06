using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldTime : MonoBehaviour
{
    //指定した時間のみこのオブジェクトを残す（攻撃判定の持続など）
    public float Time;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Hold");
    }

    //持続
    private IEnumerator Hold()
    {
        yield return new WaitForSeconds(Time);
        Destroy(this.gameObject);
    }
}
