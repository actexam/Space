using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者　佐々木奏
//爆発エフェクト消滅
public class BomDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ByeEffect", 0.6f);//指定時間後にエフェクトを消滅させる
    }
    void ByeEffect()
    {
        Destroy(gameObject); //エフェクトを消滅させる
    }
}
