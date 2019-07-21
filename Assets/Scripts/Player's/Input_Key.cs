using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者　佐々木奏
//プレイヤーのうちキー入力を担当するクラス
public partial class Player_Behaviour
{
   
    void Update()
    {
        GetMoveKey();//移動用キー入力確認
		GetBullet();//弾発射用キー入力確認
	}
    void FixedUpdate()
    {
        Move(moveLR * (float)angle_LR, moveUD * (float)angle_UD);
    }
    void GetMoveKey()//移動用キー入力確認
    {
        angle_LR = Angle_LR.Neutral;//ニュートラルにする
        angle_UD = Angle_UD.Neutral;//ニュートラルにする

		//右の壁にぶつかっていない
		if (Input.GetKey(KeyCode.RightArrow) && Hit_RightWall == false)
        {
            angle_LR = Angle_LR.Right;
        }
		//左の壁にぶつかっていない
		else if (Input.GetKey(KeyCode.LeftArrow) && Hit_LeftWall == false)
        {
            angle_LR = Angle_LR.Left;
        }
		//上の壁にぶつかっていない
		if (Input.GetKey(KeyCode.UpArrow) && Hit_TopWall == false)
        {
            angle_UD = Angle_UD.Up;
        }
		//下の壁にぶつかっていない
		else if (Input.GetKey(KeyCode.DownArrow) && Hit_BottomWall == false)
        {
            angle_UD = Angle_UD.Down;
        }
    }
    void GetBullet()//弾発射用キー入力確認
    {
        bullet_Cnt += Time.deltaTime;//カウントを増やす
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (bullet_Cnt >= shoot_time)//カウントが発射間隔を超えていれば
            {
                P_Bullet(shotLR, shotUD);//飛ばす方向と速度を指定
				bullet_Cnt = 0;//カウントをゼロにする
            }
        }
    }
}
