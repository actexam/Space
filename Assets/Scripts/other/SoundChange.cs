using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者　佐々木奏
//BGM制御
public class SoundChange : MonoBehaviour
{
    private AudioSource Aud_BGM = null;
    public AudioClip Aud_Normal = null;//通常
    public AudioClip Aud_Boss = null;//ボス
    public AudioClip Aud_Clear = null;//クリア
    public GameObject player = null;//プレイヤ生存確認用
    public GameObject boss = null;//ボス生存確認用
    Boss boss_Living = null;//BossのプロパティLive使用
    [System.NonSerialized] public bool Aud_ChangeBoss;//ボスBGMに変更のスイッチ(外部から使用)
    private bool Aud_ChangeClear = true;//クリアBGMに変更のスイッチ
    // Start is called before the first frame update
    void Start()
    {
        Aud_BGM = GetComponent<AudioSource>();
        Aud_BGM.clip = Aud_Normal;//まずは通常BGM
        Aud_BGM.Play();
        Aud_ChangeBoss = false;
        boss_Living = boss.GetComponent<Boss>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == true)//BGMはプレイヤの生存が前提
        {
            if (Aud_ChangeBoss == true)
            {
                Aud_BGM.clip = Aud_Boss;//ボス戦BGM
                Aud_BGM.Play();
                Aud_ChangeBoss = false;//一度しか呼ばれない 
                Aud_ChangeClear = true;//ボスクリア後に変更するため
            }
            if (boss_Living.Live == false && boss == true)//ボスHP0(まだ画面上に存在)
            {
                Aud_BGM.Stop();//BGM停止
            }
            if (boss == false && Aud_ChangeClear == true)//ボスが存在しない=死亡
            {
                Aud_BGM.clip = Aud_Clear;//クリアBGM
                Aud_BGM.Play();
                Aud_ChangeClear = false;//一度しか呼ばれない 
            }
        }
    }
}
