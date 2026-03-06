using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DepthExpression : MonoBehaviour
{
    //キャラやエフェクトのオブジェクトに付ける
    [SerializeField] private Renderer Rend = null;//操作するレイヤーを指定
    private float Y;//オブジェクトのY座標を変換する用

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Y = transform.position.y * 10;
        Rend.sortingOrder = -(int)Y;
    }
}
