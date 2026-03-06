using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    //被ダメージ用スクリプトに付与するインタフェース
    public void Damage(int value);//value分ダメージを受ける
    public void Death();//オブジェクト破壊またはイベント発生
}

public interface Initializeable
{ 
    public void Initialize(); 
}