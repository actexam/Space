using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//制作者　佐々木奏
//キャラクタとして登録されるオブジェクトのほとんどに使うスーパークラス
public class BShooting : MonoBehaviour,IDamageable
{
    //キャラクタ共通の爆発エフェクト格納(エラー回避のためnullで初期化)
    public GameObject bomEffect = null;

    [System.NonSerialized]public int   hp;    //キャラクタのHP
	protected float moveLR;//キャラ横移動用
	protected float moveUD;//キャラ縦移動
	protected float shotLR;//弾横移動用
	protected float shotUD;//弾縦移動用
	protected bool fire_IsAbel;//弾発射制御
	protected bool threeway_IsAble;//3way用制御

	protected enum Angle_LR//キャラクタの左右向き
	{
		Left = -1,
		Neutral = 0,
		Right = 1
	};
	protected Angle_LR angle_LR;

	protected enum Angle_UD//キャラクタの上下向き
	{
		Down = -1,
		Neutral = 0,
		Up = 1
	};
	protected Angle_UD angle_UD;

    //IDamageのダメージ計算用メソッド
    virtual public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            if (bomEffect != null)
            {
                Instantiate(bomEffect, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

    //触れたオブジェクトが攻撃可能か調べるメソッド
    //(各スクリプトの当たり判定のメソッドに記述)
    virtual public void AttackAble(GameObject target_)
    {
        var damageable = target_.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(1);
        }
    }

    //コンストラクタ
	protected BShooting()
	{
        hp = 2;
		moveLR = 0;
		moveUD = 0;
		shotLR = 0;
		shotUD = 0;
		fire_IsAbel = false;
		threeway_IsAble = false;
		angle_LR = Angle_LR.Neutral;
		angle_UD = Angle_UD.Neutral;
	}

	//-----------------------------------------------------------------------------
	//移動の処理
	virtual protected void Move(float moveLR_, float moveUD_)
	{
		transform.Translate(moveLR_, moveUD_, 0);
	}
	//-----------------------------------------------------------------------------
}
