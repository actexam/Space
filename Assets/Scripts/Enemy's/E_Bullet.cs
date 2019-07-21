using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者　佐々木奏
//敵の通常弾クラス
public class E_Bullet : BShooting
{
	//敵のスクリプトから設定
    public void SetState(float shotLR_, float shotUD_)
    {
        shotUD = shotUD_;
        shotLR = shotLR_;
    }
    // Start is called before the first frame update
    void Start()
    {
		this.hp = 1;//一撃死
        Invoke("DESTROY_", 8.0f);//指定時間後に消滅
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(shotLR, shotUD, 0);
    }
	//消滅用
    void DESTROY_()
    {
        Destroy(gameObject);
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "e_bullet" &&
           collision.gameObject.tag != "p_bullet" &&
            collision.gameObject.tag != "enemy")
        {
            AttackAble(collision.gameObject);//上記以外のものに触れたら攻撃
        }
    }
}
