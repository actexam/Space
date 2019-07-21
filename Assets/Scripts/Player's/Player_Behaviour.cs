using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者　佐々木奏
//プレイヤーのうち行動・挙動を担当するクラス
public partial class Player_Behaviour : BShooting
{
    public GameObject bullet = null;//Bulletを格納する
    float bullet_Cnt;
    float shoot_time;
    //--------------------
    //移動制限用bool型変数
    bool Hit_RightWall;
    bool Hit_LeftWall;
    bool Hit_TopWall;
    bool Hit_BottomWall;
    //--------------------
    [System.NonSerialized] public bool Is_Three;//3方向攻撃用 
    //プレイヤーの攻撃（ショット）メソッド
    void P_Bullet(float shotLR, float shotUD)
    {
        //プレイヤの座標を弾用のVector3にセット
        Vector3 bullet_pos = new Vector3(transform.position.x + 0.6f, transform.position.y, 0);
        GameObject bullClone = Instantiate(bullet, bullet_pos, Quaternion.identity);//弾の生成
        bullClone.GetComponent<Bullet>().SetState(shotLR, 0);//飛ばす方向と速度を設定
        if (Is_Three == true) //3方向攻撃が可能な場合
        {
            GameObject bullClone2 = Instantiate(bullet, bullet_pos, Quaternion.identity);//弾の生成
            bullClone2.GetComponent<Bullet>().SetState(shotLR, shotUD);//飛ばす方向と速度を設定
            GameObject bullClone3 = Instantiate(bullet, bullet_pos, Quaternion.identity);//弾の生成
            bullClone3.GetComponent<Bullet>().SetState(shotLR, -shotUD);//飛ばす方向と速度を設定
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.hp = 1;
        this.moveLR = 0.14f;
        this.moveUD = 0.14f;
        this.shotLR = 0.1f * 1.2f;
        this.shotUD = 0.04f;
        bullet_Cnt = 0;
        shoot_time = 0.26f;
        Hit_BottomWall = false;
        Hit_LeftWall = false;
        Hit_TopWall = false;
        Hit_RightWall = false;
        Is_Three = false;
    }

    // Update is called once per frame
    //void Update()
    //{

    //}
}
