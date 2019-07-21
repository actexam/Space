using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者　佐々木奏
//敵のスーパークラス
public class Enemy : BShooting
{
	//敵の行動パターン

	//横一直線に移動
    protected IEnumerator Mode1()
    {
        while (true)
        {
            transform.Translate(-moveLR, 0, 0);
            yield return null;
        }
    }
	//ジグザグに移動
    protected IEnumerator Mode2()
    {
        for (int j = 0; j < 60; ++j)
        {
            for (int i = 0; i < 40; ++i)
            {
                transform.Translate(-moveLR, moveUD, 0);
                yield return null;
            }
            for (int i = 0; i < 80; ++i)
            {
                transform.Translate(-moveLR, -moveUD, 0);
                yield return null;
            }
            for (int i = 0; i < 40; ++i)
            {
                transform.Translate(-moveLR, moveUD, 0);
                yield return null;
            }
        }
    }
	//一定時間横に移動したら右斜めからの横移動
    protected IEnumerator Mode3()
    {
        for (int i = 0; i < 60; ++i)
        {
            transform.Translate(-moveLR, 0, 0);
            yield return null;
        }
        for (int i = 0; i < 30; ++i)
        {
            transform.Translate(moveLR, -moveUD * 0.6f, 0);
            yield return null;
        }
        while (true)
        {
            transform.Translate(-moveLR, 0, 0);
            yield return null;
        }
    }
	//waveで指定した時間横移動後、上下移動
    protected int wave;
    protected IEnumerator Mode4(int wave)
    {
        for (int i = 0; i < wave; ++i)
        {
            transform.Translate(-moveLR, 0, 0);
            yield return null;
        }
        while (true)
        {
            for (int i = 0; i < 30; ++i)
            {
                transform.Translate(0, moveUD, 0);
                yield return null;
            }
            for (int i = 0; i < 60; ++i)
            {
                transform.Translate(0, -moveUD, 0);
                yield return null;
            }
            for (int i = 0; i < 30; ++i)
            {
                transform.Translate(0, moveUD, 0);
                yield return null;
            }
        }
    }

    public GameObject E_Bullet = null;//敵共通のE_Bulletを格納する
    public GameObject Tracking_Shot = null;//敵共通の追尾弾を格納する
	//敵の攻撃ルーチン
	protected IEnumerator E_Attack(float span)
    {
        while (true)
        {
            GameObject clone = Instantiate(E_Bullet, transform.position, Quaternion.identity);//弾を生成
            clone.GetComponent<E_Bullet>().SetState(-shotLR, 0);//飛ばす方向と速度を指定
            if (threeway_IsAble == true)//3Wayが可能なら
            {
                GameObject clone2 = Instantiate(E_Bullet, transform.position, Quaternion.identity);
                clone2.GetComponent<E_Bullet>().SetState(-shotLR, shotUD);
                GameObject clone3 = Instantiate(E_Bullet, transform.position, Quaternion.identity);
                clone3.GetComponent<E_Bullet>().SetState(-shotLR, -shotUD);
            }
            yield return new WaitForSeconds(span);//敵のスクリプトからspan(弾の発射間隔)を指定
        }
    }
	//追尾弾の生成
    protected void TrackingAttack(float trackingSpeed,float trackingLife)
    {
        GameObject clone = Instantiate(Tracking_Shot, transform.position, Quaternion.identity);
        clone.GetComponent<TrackingBullet>().Tracking_On(trackingSpeed,trackingLife);//追尾弾の速度、追尾時間を設定
    }
}
