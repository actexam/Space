using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者　佐々木奏
//ボス用クラス(大分独自に組んでる*汎用性皆無*)
public class Boss : Enemy
{
    bool live = true;//ボスの生存確認用(BossEffectで使用)
    public int hp_;//デバッグ用
    public bool Live//プロパティで他スクリプトからは取得のみ可能
    {
        get { return live; }
        private set { }
    }
    float attackSpan = 0.6f;//攻撃の間隔
    float nowTime = 0;//攻撃のタイムカウンター
    bool Tracking_On;//追尾弾発射制御
    int MCnt;//BMove1・BMove3のアップダウンを何回繰り返すかの変数(ランダムで数値取得)
    //ランダムで次のBMoveを決めるのに使う
    string[] mode = new string[] { "BMove1", "BMove2", "BMove3", "BMove4" };


    // Start is called before the first frame update
    void Start()
    {
        this.hp = 1;
        this.moveLR = 0.34f;
        this.moveUD = 0.12f;
        this.shotLR = 0.1f;
        this.shotUD = 0.03f;
        this.fire_IsAbel = true;
        this.threeway_IsAble = true;
        this.Tracking_On = false;
        this.MCnt = 5;
        StartCoroutine(BMove0());//最初はBMove0から
    }

    // Update is called once per frame
    void Update()
    {
        if (fire_IsAbel == true)//攻撃可能
        {
            Boss_Attack();
        }
        hp_ = hp;//デバッグ用
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "e_bullet")
        {
            AttackAble(collision.gameObject);//上記以外のオブジェクトに触れたら攻撃
        }
    }

    //IDamageのダメージ計算用メソッド(Boss用に一部変更)
    override public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            live = false;//BossEffectに自身の敗北を送る
            fire_IsAbel = false;//攻撃を停止
            StopAllCoroutines();//全てのコルーチンを停止
            //ヒエラルキーのオブジェクトを全て検知する
            foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
            {
                // シーン上に存在するオブジェクトならば処理.
                if (obj.activeInHierarchy)
                {
                    if (obj.tag == "e_bullet")
                    {
                        Destroy(obj);//敵の弾を全て破壊
                    }
                }
            }
        }
    }

    //敵の攻撃ルーチン(コルーチンでは制御が難しかったのでメソッド化)
    void Boss_Attack()
    {
        if (nowTime > attackSpan)
        {
            Vector3 bossPos = new Vector3(transform.position.x - 1, transform.position.y, 0);
            GameObject clone = Instantiate(E_Bullet, bossPos, Quaternion.identity);//弾を生成
            clone.GetComponent<E_Bullet>().SetState(-shotLR, 0);//飛ばす方向と速度を指定
            if (threeway_IsAble == true)//3Wayが可能なら
            {
                GameObject clone2 = Instantiate(E_Bullet, bossPos, Quaternion.identity);
                clone2.GetComponent<E_Bullet>().SetState(-shotLR, shotUD);
                GameObject clone3 = Instantiate(E_Bullet, bossPos, Quaternion.identity);
                clone3.GetComponent<E_Bullet>().SetState(-shotLR, -shotUD);
            }
            nowTime = 0;
        }
        nowTime += Time.deltaTime;
    }

    //次のコルーチンを呼ぶ(同じものは連続で呼ばれない)
    //引数に現在のコルーチン名を取得
    string CoroutineChanged(string nowMove)
    {
        Debug.Log("メソッド");//デバッグ用
        string nextMove = nowMove;//とりあえず今の情報を取得
        while (nextMove == nowMove)//違うのがでるまで繰り返す
        {
            nextMove = mode[Random.Range(0, mode.Length)];//配列よりランダムに取得
            if (nextMove != nowMove)
            {
                Debug.Log("kita");//デバッグ用
                break;//違うのを取得したらループ終了
            }
        }
        if (nextMove == "BMove1")
        {
            this.shotLR = 0.12f;
            this.shotUD = 0.03f;
            fire_IsAbel = true;//攻撃を開始
            MCnt = Random.Range(1, 5);//行動回数をランダム決定
        }
        if (nextMove == "BMove2")
        {
            fire_IsAbel = false;//突撃時は攻撃を停止
        }
        if (nextMove == "BMove3")
        {
            this.shotLR = 0.14f;
            this.shotUD = 0.03f * 2;
            fire_IsAbel = true;//攻撃を開始
            MCnt = Random.Range(1, 5);//行動回数をランダム決定
        }
        if (nextMove == "BMove4")
        {
            this.shotLR = 0.1f;
            this.shotUD = 0.03f;
            fire_IsAbel = true;//攻撃を開始
            Tracking_On = true;//追尾弾発射
        }
        return nextMove;
    }

    //上下移動(最初の一回のみ)
    IEnumerator BMove0()
    {
        Debug.Log("0");//デバッグ用
        for (int j = 0; j < 5; ++j)//BMove0のみ回数固定
        {
            for (int i = 0; i < 28; ++i)
            {
                transform.Translate(0, moveUD, 0);
                yield return null;
            }
            for (int i = 0; i < 56; ++i)
            {
                transform.Translate(0, -moveUD, 0);
                yield return null;
            }
            for (int i = 0; i < 28; ++i)
            {

                transform.Translate(0, moveUD, 0);
                yield return null;
            }
        }
        fire_IsAbel = false;//突撃に入るため攻撃を停止
        StartCoroutine(BMove2());//BMove0のみ次の行動固定
    }

    //上下移動(BMove0と同じだが、次に実行されるコルーチンがランダム)  
    IEnumerator BMove1()
    {
        Debug.Log("1");//デバッグ用
        for (int j = 0; j < MCnt; ++j)//回数ランダム
        {
            for (int i = 0; i < 28; ++i)
            {
                transform.Translate(0, moveUD, 0);
                yield return null;
            }
            for (int i = 0; i < 56; ++i)
            {
                transform.Translate(0, -moveUD, 0);
                yield return null;
            }
            for (int i = 0; i < 28; ++i)
            {
                transform.Translate(0, moveUD, 0);
                yield return null;
            }
        }
        string next = CoroutineChanged("BMove1");//次のコルーチンを決定(引数に今のコルーチン名)
        StartCoroutine(next);//次のコルーチン開始
    }

    //一定フレーム停止後横に突撃
    IEnumerator BMove2()
    {
        Debug.Log("2");//デバッグ用
        for (int i = 0; i < 15; ++i)
        {
            yield return null;
        }
        for (int i = 0; i < 36; ++i)
        {
            transform.Translate(-moveLR, 0, 0);
            yield return null;
        }
        for (int i = 0; i < 36; ++i)
        {
            transform.Translate(moveLR, 0, 0);
            yield return null;
        }
        string next = CoroutineChanged("BMove2");//次のコルーチンを決定(引数に今のコルーチン名)
        StartCoroutine(next);//次のコルーチン開始
    }
    //BMove1の範囲が狭くなったバージョン
    IEnumerator BMove3()
    {
        Debug.Log("3");//デバッグ用
        for (int j = 0; j < MCnt; ++j)
        {
            for (int i = 0; i < 18; ++i)
            {
                transform.Translate(0, moveUD, 0);
                yield return null;
            }
            for (int i = 0; i < 36; ++i)
            {
                transform.Translate(0, -moveUD, 0);
                yield return null;
            }
            for (int i = 0; i < 18; ++i)
            {
                transform.Translate(0, moveUD, 0);
                yield return null;
            }
        }
        string next = CoroutineChanged("BMove3");//次のコルーチンを決定(引数に今のコルーチン名)
        StartCoroutine(next);//次のコルーチン開始
    }
    //その場で停止して追尾弾を発射(1発のみ)
    IEnumerator BMove4()
    {
        Debug.Log("4");//デバッグ用
        for (int i = 0; i < 80; ++i)
        {
            if (Tracking_On == true)
            {
                Tracking_On = false;//一度しか呼ばれないようにする
                TrackingAttack(0.04f, 5.0f);//引数は(速度、追尾時間)
            }
            yield return null;
        }
        string next = CoroutineChanged("BMove4");//次のコルーチンを決定(引数に今のコルーチン名)
        StartCoroutine(next);//次のコルーチン開始
    }
}
