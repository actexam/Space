using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//制作者　佐々木奏
//敵その4
public class Enemy4 : Enemy
{
    public GameObject lackItem = null;
    Player_Behaviour pb_;
    private float[] lineY = new float[2] { -4, 4 };//Y軸の出現位置

    // Start is called before the first frame update
    void Start()
    {
        ////出現位置Y軸は配列の二択
        float realLineY = lineY[Random.Range(0, 2)];
        transform.position =
            new Vector3(transform.position.x, realLineY, 0);
        this.hp = 1;
        this.moveLR = 0.18f;
        this.moveUD = 0.1f;
        this.fire_IsAbel = true;
        this.threeway_IsAble = false;
        shotLR = 0.03f;
        shotUD = 0.02f * 1f;
        StartCoroutine(base.Mode1());
    }
    //アイテム生成メソッド
    private void AppearLack()
    {
        int[] lacknum = new int[] { 0, 1, 0, 0, 0 };//出現確率を配列に格納
        int hitlack = Random.Range(0, lacknum.Length);//配列の長さ分までの中でランダム値を獲得
        if (lacknum[hitlack] == 1)//配列の値が1なら
        {
            if (lackItem != null)
            {
                Instantiate(lackItem, transform.position, Quaternion.identity);//アイテム生成
            }
        }
    }

    //敵4専用のダメージ計算
    override public void TakeDamage(int damage)
    {
        hp -= damage;//相手からのダメージ分HPを減らす
        if (hp <= 0)
        {
            if (bomEffect != null)//爆発エフェクトが格納されてれば
            {
                Instantiate(bomEffect, transform.position, Quaternion.identity);//爆発エフェクト生成
            }
            AppearLack();//アイテム生成メソッド呼び出し
            Destroy(gameObject);//自身を破壊
        }
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
