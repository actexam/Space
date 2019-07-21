using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者　佐々木奏
//オブジェクト点滅用
public class ObjectBlink : MonoBehaviour
{
    Color color;//オブジェクトのcolor取得用
    SpriteRenderer objectRend; //オブジェクトのSpriteRenderer取得用
    float alpha;//α値格納
    bool blinkBegin;//オブジェクトの点滅開始制御

    // Start is called before the first frame update
    void Start()
    {
        objectRend = GetComponent<SpriteRenderer>();
        color = objectRend.material.color;
        blinkBegin = false;
        Invoke("BlinkBegin", 5.0f);//指定時間後、 オブジェクトの点滅を開始   
        Invoke("LostItem", 12.0f);//指定時間後、オブジェクトを消滅
    }

    // Update is called once per frame
    void Update()
    {
        if (blinkBegin == true)//点滅が開始されたら
        {
            blinkBegin = false;//二度目がおきないようにする
            StartCoroutine(ColorCoroutine());//点滅のコルーチンを開始
        }
    }
    //点滅開始用メソッド
    void BlinkBegin()
    {
        blinkBegin = true;
    }
    //アイテム（オブジェクト）消滅用メソッド
    void LostItem()
    {
        Destroy(gameObject);
    }
    //α値適用のメソッド
    void ChangeA(float alpha)
    {
        color.a = alpha;
        this.objectRend.material.color = color;
    }
    //点滅のコルーチン
    IEnumerator ColorCoroutine()
    {
        while (true)
        {
            for (int i = 0; i < 5; ++i)
            {
                alpha = 0;
                ChangeA(alpha);
                yield return new WaitForEndOfFrame();
            }
            for (int i = 0; i < 5; ++i)
            {
                alpha = 1;
                ChangeA(alpha);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
