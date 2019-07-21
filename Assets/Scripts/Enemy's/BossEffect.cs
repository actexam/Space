using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEffect : MonoBehaviour
{
    [SerializeField] GameObject bossEff = null;
    float nextEffTime = 0.12f;
    float nowEffTime = 0;
    GameObject parent;
    Color color;//オブジェクトのcolor取得用
    SpriteRenderer objectRend; //オブジェクトのSpriteRenderer取得用
    [SerializeField] float alpha;//α値格納 
    Boss boss;

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
        objectRend = parent.GetComponent<SpriteRenderer>();
        color = objectRend.material.color;
        boss = parent.GetComponent<Boss>();
    }

    // Update is called once per frame
    void Update()
    {
        if (boss.Live == false)
        {
            FadeOut();
            EffectAppear();
        }
    }

    void FadeOut()
    {
        color.a -= 0.003f;
        objectRend.material.color = color;
        alpha = color.a;
        if (color.a < 0)
        {
            Destroy(parent);
        }
    }

    void EffectAppear()
    {
        if (nextEffTime <= nowEffTime)
        {
            Vector3 effPos = new Vector3(transform.position.x + Random.Range(-2.0f, 2.0f),//X
                                         transform.position.y+Random.Range(-2.0f, 2.0f),//Y
                                         0);                       //Z
            Instantiate(bossEff, effPos, Quaternion.identity);
            nowEffTime = 0;
        }
        nowEffTime += Time.deltaTime;
    }
}
