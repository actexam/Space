using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者　佐々木奏
//スクロール停止用
public class FinishScroll : MonoBehaviour
{
	[SerializeField] GameObject searchEnemy = null;//ボス戦手前の敵検知のオブジェクト格納
	GameObject parent;//親オブジェクト格納
	private void Start()
	{
		parent = transform.parent.gameObject;//親オブジェクト取得
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name == "LeftWall")//左の壁に触れたら
		{
			parent.GetComponent<AllScroll>().Scroller = false;//スクロールを停止
			searchEnemy.SetActive(true);//敵検知オブジェクトをアクティブ化
		}
	}
}
