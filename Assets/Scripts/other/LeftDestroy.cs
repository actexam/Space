using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//作成者　佐々木奏
//左の壁にアタッチする
public class LeftDestroy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Untagged" &&
            collision.gameObject.tag != "player")
        {
            Destroy(collision.gameObject);//上記以外のものが触れたら消滅させる
        }
    }
}
