using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySE()
    {
        SoundManager.Instance.PlaySE("SE/cancel000");
    }

    public void StopSE()
    {
        SoundManager.Instance.StopSE("SE/cancel000");
    }

    public void PlayBGM()
    {
        SoundManager.Instance.PlayBGM(SoundManager.titleBGM);
        //SoundManager.Instance.(SoundManager.bgm000);

    }

    public void StopBGM()
    {
        //SoundManager.Instance.StopBGM(SoundManager.bgm000);
    }
    public void PauseBGM()
    {
        //SoundManager.Instance.PauseBGM(SoundManager.bgm000);
    }
    public void UnPauseBGM()
    {
        //SoundManager.Instance.UnPauseBGM(SoundManager.bgm000);
    }
}
