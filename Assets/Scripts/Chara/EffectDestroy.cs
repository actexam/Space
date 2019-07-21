using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者　佐々木奏
//エフェクト制御用
public class EffectDestroy : MonoBehaviour
{
    public GameObject explo = null;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(explo, transform.position, Quaternion.identity);
        Invoke("ByeEffect", 0.6f);//指定時間後にエフェクトを消滅させる
    }
    void ByeEffect()
    {     
        Destroy(gameObject); //エフェクトを消滅させる
    }
    
}
