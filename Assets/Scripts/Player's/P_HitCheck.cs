using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者　佐々木奏
//プレイヤーのうち当たり判定を担当するクラス
public partial class Player_Behaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string ObjectName = collision.gameObject.name;//衝突したオブジェクトの名前を取得
        string ObjectTag = collision.gameObject.tag;//衝突したオブジェクトのタグ名を取得
		if (ObjectName == "RightWall")
        {
            Hit_RightWall = true;//右移動を制限
        }
        if (ObjectName == "LeftWall")
        {
            Hit_LeftWall = true;//左移動を制限
		}
        if (ObjectName == "TopWall")
        {
            Hit_TopWall = true;//上移動を制限
		}
        if (ObjectName == "BottomWall")
        {
            Hit_BottomWall = true;//下移動を制限
		}

        if (ObjectTag == "item")
        {
            Is_Three = true;//3Way攻撃を可能にする
            Destroy(collision.gameObject);
        }
        if (ObjectTag != "p_bullet")
        {
            AttackAble(collision.gameObject);//上記以外のオブジェクトに触れたら攻撃
		}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        string ObjectName = collision.gameObject.name;
        if (ObjectName == "RightWall")
        {
            Hit_RightWall = false;//右移動を可能に
		}
        if (ObjectName == "LeftWall")
        {
            Hit_LeftWall = false;//左移動を可能に
		}
        if (ObjectName == "TopWall")
        {
            Hit_TopWall = false;//上移動を可能に
		}
        if (ObjectName == "BottomWall")
        {
            Hit_BottomWall = false;//下移動を可能に
		}
    }
}
