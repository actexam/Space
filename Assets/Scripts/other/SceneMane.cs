using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//作成者　佐々木奏
//シーン遷移に使用
public class SceneMane : MonoBehaviour
{
	[SerializeField] GameObject player = null;//プレイヤを格納
    [SerializeField] GameObject boss = null;//ボスを格納

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
        if(boss==false&&player==true)//ボスを倒したら(プレイヤの生存が条件)
        {
            Invoke("LoadEnd", 10.0f);//指定時間後、LoadEndを実行
        }
		if (player == false)//プレイヤが消滅したら
		{
			Invoke("LoadTitle", 5.0f);//指定時間後、LoadTitleを実行
        }
	}
    private void LoadEnd()
    {
        SceneManager.LoadScene("End");//遷移したいシーン名を記述
    }
	private void LoadTitle()
	{
		SceneManager.LoadScene("Title");//遷移したいシーン名を記述
	}
}
