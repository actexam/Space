using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//制作者　佐々木奏
//敵その１
public class Enemy1 : Enemy, IDamageable
{
    // Start is called before the first frame update
    void Start()
    {
		//出現位置Y軸はランダム化
        transform.position = 
            new Vector3(transform.position.x, Random.Range(-3.0f, 3.0f), 0);
        this.moveLR = 0.04f;
        this.moveUD = 0.1f;
        this.fire_IsAbel = true;
        this.threeway_IsAble = false;
        shotLR = 0.1f;
        shotUD = 0.02f;
        StartCoroutine(base.Mode1());
        StartCoroutine(base.E_Attack(1.5f));//引数は攻撃の間隔
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

    // Update is called once per frame
    void Update()
    {

    }
}
