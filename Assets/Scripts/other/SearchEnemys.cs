using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者　佐々木奏
//ボス手前で敵を検知する
public class SearchEnemys : MonoBehaviour
{
	public GameObject Dark = null;//暗幕格納
	public GameObject lastBack = null;//最終戦の背景格納
    public GameObject boss = null;//ボスを格納
    public GameObject BGM = null;//BGM変更のため
    public GameObject player = null;//プレイヤの生存確認用
	int enemysNum;//敵の数
	int saveNum;//敵の数を事前にいれる
	bool Awake;//ボス戦開始用

	// Start is called before the first frame update
	void Start()
	{
		EnemysCount();//敵を検知する
		Awake = false;
	}

    //一番最後に処理したいのでLateUpdateを使用
	private void LateUpdate()
	{
        if (player == true)//プレイヤの生存が前提
        {
            EnemysCount();//敵を検知する
            if (enemysNum <= 0 && Awake == false)//敵がいなくなったら
            {
                BGM.GetComponent<AudioSource>().Stop();//BGMを止める
                Awake = true;//ボス戦準備
                Invoke("AwakeBoss", 2.0f);
            }
        }
        else//プレイヤの死亡
        {
            Destroy(gameObject);//このオブジェクトを消滅
        }
	}
	void AwakeBoss()
	{
		Dark.SetActive(true);//暗幕を展開
		Invoke("BeginLastBattle", 1.0f);
	}
	void BeginLastBattle()
	{
        //暗幕を消し、ボスと最終背景を展開
		Dark.SetActive(false);
		lastBack.SetActive(true);
		boss.SetActive(true);
        boss.GetComponent<Boss>().enabled = false;
        Invoke("Fight", 0.5f);//ボス戦開始
	}
    //敵を検知するメソッド
	void EnemysCount()
	{
		saveNum = 0;
		foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
		{
			// シーン上に存在するオブジェクトならば処理.
			if (obj.activeInHierarchy)
			{
				if (obj.tag == "enemy")
				{
					++saveNum;
				}
			}
		}
		enemysNum = saveNum;
	}

    void Fight()//ボス戦開始
    {
        BGM.GetComponent<SoundChange>().Aud_ChangeBoss = true;//BossBGM変更開始
        boss.GetComponent<Boss>().enabled = true;
        Destroy(gameObject);//このオブジェクトを消滅
    }
}
