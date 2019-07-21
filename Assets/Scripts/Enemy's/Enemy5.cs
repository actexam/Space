using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//制作者　佐々木奏
//敵その5
public class Enemy5 : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        this.hp = 3;
        this.moveLR = 0.08f;
        this.moveUD = 0.1f;
        this.fire_IsAbel = true;
        this.threeway_IsAble = true;
        this.wave = Random.Range(20, 70);//Mode4を使用時に横移動のフレーム指定
        shotLR = 0.1f;
        shotUD = 0.02f;
        StartCoroutine(base.Mode4(this.wave));
        StartCoroutine(base.E_Attack(3.0f));//引数は攻撃の間隔
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "e_bullet"
             && collision.gameObject.tag != "enemy"
             && collision.gameObject.tag != "e_track")
        {
            AttackAble(collision.gameObject);//上記以外のオブジェクトに触れたら攻撃
		}
    }
}
