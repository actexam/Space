using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者　佐々木奏
//スクロールクラス
public class AllScroll : MonoBehaviour
{

	[System.NonSerialized] public bool Scroller = true;//スクロールストップ用
	float scrollSp = 0.05f;//スクロール速度

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
        //----------------------------
        //デバッグ用(Sを押すとスクロールが速くなる)
		if(Input.GetKey(KeyCode.S))
		{
			scrollSp = 5f;
		}
		else
		{
			scrollSp = 0.05f;
		}
        //----------------------------
        if (Scroller == true)//スクロールの終点ではない（まだ動く）
		{
			transform.Translate(-scrollSp, 0, 0);
		}
	}
}
