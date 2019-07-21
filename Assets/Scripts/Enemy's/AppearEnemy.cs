using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者　佐々木奏
//エリアごとの敵の生成クラス
public class AppearEnemy : MonoBehaviour
{
    [SerializeField] GameObject[] enemys = null;//インスペクターから調整
    [SerializeField] GameObject rightwall = null;
    [SerializeField] GameObject leftwall = null;
    [SerializeField] float appearNextTime = 0;//インスペクターから調整　敵生成の間隔時間
	private float elapsedTime;
    private bool Is_OK_Appear;


    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0f;
        Is_OK_Appear = false;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > appearNextTime)//一定時間毎に敵を生成
        {
            elapsedTime = 0f;
            AppearEnemys();
        }
    }
	//敵生成
    void AppearEnemys()
    {
        if (Is_OK_Appear)
        {
            var randomValue = Random.Range(0, enemys.Length);
            GameObject.Instantiate
                (enemys[randomValue], rightwall.transform.position, Quaternion.identity);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == rightwall)
        {
            Is_OK_Appear = true;//敵の生成を可能にする
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == rightwall)
        {
            Is_OK_Appear = true;//敵の生成を可能にする
		}
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == leftwall)
        {
            Destroy(gameObject);//自身の消滅
        }
    }
}
