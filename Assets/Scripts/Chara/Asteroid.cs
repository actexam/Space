using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者　佐々木奏
//小惑星用
public class Asteroid : BShooting
{
    // Start is called before the first frame update
    void Start()
    {
        this.hp = 100;//倒れない
    }
    override public void TakeDamage(int damage)
    {
        //倒れない存在だからHPを減らす処理はない
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AttackAble(collision.gameObject);//触れるもの全てを倒す
    }
}
