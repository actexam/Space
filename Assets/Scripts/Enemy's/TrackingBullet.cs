using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者　佐々木奏
//敵の追尾弾
public class TrackingBullet : BShooting
{
    private float trackingSpeed = 0;//移動速度
    GameObject player;//プレイヤーオブジェクト取得用
    private float Life_Tracking = 5.0f;//追尾時間(初期は5秒間)
    private float life_Cnt = 0;//時間を足していく変数

	//外部より速度と追尾時間を設定
    public void Tracking_On(float trackingSpeed_, float lifeTracking)
    {
        this.trackingSpeed = trackingSpeed_;
        this.Life_Tracking = lifeTracking;
    }
    void DESTROY_()
    {
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
		this.hp = 1;
        player = GameObject.FindWithTag("player");
        Invoke("DESTROY_", 20.0f);//指定時間後、自身を破壊
    }

    // Update is called once per frame
    void Update()
    {
        life_Cnt += Time.deltaTime;//時間時間をカウント
    }
    private void FixedUpdate()
    {
		//プレイヤーが生存状態でカウンタが追尾時間以下なら
        if (life_Cnt <= Life_Tracking && player == true)
        {
			//プレイヤーを追尾する
            transform.position = Vector3.MoveTowards
                (transform.position, player.transform.position, trackingSpeed);
        }
        else
        {
            transform.Translate(-trackingSpeed, 0, 0);//その場から横に移動しだす
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "e_bullet"
             && collision.gameObject.tag != "enemy"
             && collision.gameObject.tag != "p_bullet")
        {
            AttackAble(collision.gameObject);//上記以外のオブジェクトに触れたら攻撃
		}
    }
}
