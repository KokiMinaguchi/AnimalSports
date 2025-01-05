using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using R3;
using R3.Triggers;
using UnityEngine.Rendering;
using UnityEditor;
using Unity.VisualScripting;


public sealed class AudioPlayer
{
    #region Field

    // 再生用ソース
    private AudioSource _audioSource;
    // 再生中の音の名前
    private string _name;
    // 再生終了後の処理
    private Action _callback;
    // 再生までの待ち時間
    private float _delay;
    // フェード時間
    private float _fadeTime;

    #endregion

    #region Property

    public AudioClip Clip { get { return _audioSource.clip; } set { _audioSource.clip = value; } }
    public float Volume { get { return _audioSource.volume; } set { _audioSource.volume = value; } }
    public float Pitch { get { return _audioSource.pitch; } set { _audioSource.pitch = value; } }
    public bool IsLoop { get { return _audioSource.loop; } set { _audioSource.loop = value; } }
    public bool IsPlaying { get { return _audioSource.isPlaying; } }
    public float FadeTime { get { return _fadeTime; } set { _fadeTime = value; } }

    #endregion

    #region Method

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="audioSource">AddComponentで作成したオーディオソース</param>
    public AudioPlayer(AudioSource audioSource)
    {
        _audioSource = audioSource;
        _audioSource.playOnAwake = false;
    }
    /// <summary>
    /// 再生開始
    /// </summary>
    public void Play()
    {
        _audioSource.Play();
    }
    /// <summary>
    /// 再生停止
    /// </summary>
    public void Stop()
    {
        _audioSource.Stop();
    }
    /// <summary>
    /// 再生一時停止
    /// </summary>
    public void Pause()
    {
        _audioSource.Pause();
    }
    /// <summary>
    /// 再生再開
    /// </summary>
    public void UnPause()
    {
        _audioSource.UnPause();
    }
    public void FadeIn(float fadeTime)
    {
        _audioSource.DOFade(1.0f, fadeTime);
    }
    public void FadeOut(float fadeTime)
    {
        _audioSource.DOFade(0.0f, fadeTime);
    }

    #endregion
}

public sealed class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    #region Field

    // 音声ファイルのリソーステーブル
    Dictionary<string, AudioClip> audioDictionary = new Dictionary<string, AudioClip>();

    //AudioSource（スピーカー）を同時に鳴らしたい音の数だけ用意
    private const int _bgmPlayerNum = 2;
    private const int _sePlayerNum = 10;
    private List<AudioPlayer> _audioPlayerBGM = new List<AudioPlayer>();
    private List<AudioPlayer> _audioPlayerSE = new List<AudioPlayer>();
    //private AudioPlayer[] _audioPlayerEnvironment = new AudioPlayer[1];
    //private AudioPlayer[] _audioPlayerJingle = new AudioPlayer[1];
    //private AudioPlayer[] _audioPlayerVoice = new AudioPlayer[3];

    #endregion
    
    public const string Cancel000 = "SE/cancel000";
    public const string Coin000 = "SE/coin000";
    public const string bgm000 = "BGM/bgm000";
    public const string bgm001 = "BGM/bgm001";

    #region Unity

    protected override void Awake()
    {
        base.Awake();
        audioDictionary.Add(Cancel000, Resources.Load(Cancel000) as AudioClip);
        audioDictionary.Add(bgm000, Resources.Load(bgm000) as AudioClip);
        audioDictionary.Add(bgm001, Resources.Load(bgm001) as AudioClip);
    }

    private void Start()
    {
        // AudioSource作成
        for (int i = 0; i < _sePlayerNum; ++i)
        {
            _audioPlayerSE.Add(new AudioPlayer(gameObject.AddComponent<AudioSource>()));
        }
        for (int i = 0; i < _bgmPlayerNum; ++i)
        {
            _audioPlayerBGM.Add(new AudioPlayer(gameObject.AddComponent<AudioSource>()));
        }
    }

    #endregion

    public void AudioParameterSetting(AudioClip audioClip)
    {
        // 共通設定
        //AudioImporter audioImporter = assetImporter as AudioImporter;
        //string path = audioImporter.assetPath;


        // menuSEはモノラルに設定
        // BGMはバックグラウンド読み込みに設定
        // 
        // 個別設定
        AudioImporterSampleSettings audioImporterSampleSettings;
        audioImporterSampleSettings.compressionFormat = AudioCompressionFormat.Vorbis;
        audioImporterSampleSettings.quality = 100.0f;
        audioImporterSampleSettings.sampleRateSetting = AudioSampleRateSetting.OverrideSampleRate;
        audioImporterSampleSettings.sampleRateOverride = 44100;
        // 
        audioImporterSampleSettings.loadType = AudioClipLoadType.CompressedInMemory;
        // BGM,VoiceなどはStreamingに設定
    }


    public void PlaySE(string audioName, float volume = 1.0f,
        float pitch = 1.0f, bool isLoop = false, float delay = 0.0f, Action _callback = null)
    {
        var audioClip = audioDictionary.GetValueOrDefault(audioName);
        // nullチェック
        if (audioClip == null || volume <= 0.0f) return;

        // 未使用のAudioPlayerを取得
        var audioPlayer = GetUnusedAudioPlayer(_audioPlayerSE);
        if (audioPlayer == null) return;
        
        // パラメータを設定して再生
        audioPlayer.Clip = audioClip;
        audioPlayer.Volume = volume;
        audioPlayer.Pitch = pitch;
        audioPlayer.IsLoop = isLoop;
        audioPlayer.Play();
    }
    public void StopSE(string audioName)
    {
        foreach (var audioPlayer in _audioPlayerSE)
        {
            if (audioPlayer.Clip.name == Path.GetFileNameWithoutExtension(audioName))
            {
                audioPlayer.Stop();
            }
        }
    }

    public void PlayBGM(string audioName, float volume = 1.0f,
        float pitch = 1.0f, bool isLoop = true, float delay = 0.0f)
    {
        // ファイル名をキーにAudioClip取得
        var audioClip = audioDictionary.GetValueOrDefault(audioName);
        // nullチェック
        if (audioClip == null || volume <= 0.0f) return;

        // 未使用のAudioPlayerを取得
        var audioPlayer = GetUnusedAudioPlayer(_audioPlayerBGM);
        if (audioPlayer == null) return;

        // パラメータを設定して再生
        audioPlayer.Clip = audioClip;
        audioPlayer.Volume = volume;
        audioPlayer.Pitch = pitch;
        audioPlayer.IsLoop = isLoop;
        audioPlayer.Play();
    }
    public void StopBGM(string audioName)
    {
        foreach (var audioPlayer in _audioPlayerBGM)
        {
            if (audioPlayer.Clip.name == Path.GetFileNameWithoutExtension(audioName))
            {
                audioPlayer.Stop();
            }
        }
    }

    public void PauseBGM(string audioName, float fadeTime = 0.1f)
    {
        foreach(var audioPlayer in _audioPlayerBGM)
        {
            if (audioPlayer.Clip.name == Path.GetFileNameWithoutExtension(audioName))
            {
                audioPlayer.FadeOut(fadeTime);// ノイズ回避
                audioPlayer.Pause();
            }
        }
    }
    public void UnPauseBGM(string audioName, float fadeTime = 0.1f)
    {
        foreach (var audioPlayer in _audioPlayerBGM)
        {
            if (audioPlayer.Clip.name == Path.GetFileNameWithoutExtension(audioName))
            {
                audioPlayer.FadeIn(fadeTime);// ノイズ回避
                audioPlayer.UnPause();
            }
        }
    }

    public void FadeInBGM(string audioName, float fadeTime)
    {
        foreach (var audioPlayer in _audioPlayerBGM)
        {
            if (audioPlayer.Clip.name == Path.GetFileNameWithoutExtension(audioName))
            {
                audioPlayer.FadeIn(fadeTime);
            }
        }
    }

    public void FadeOutBGM(string audioName, float fadeTime)
    {
        foreach (var audioPlayer in _audioPlayerBGM)
        {
            if (audioPlayer.Clip.name == Path.GetFileNameWithoutExtension(audioName))
            {
                audioPlayer.FadeOut(fadeTime);
            }
        }
    }

    public void PauseAll()
    {

    }

    public void CrossFade()
    {
        // TODO
    }

    /// <summary>
    /// 未使用のAudioPlayerを取得
    /// </summary>
    /// <param name="audioPlayers">AudioPlayerのリスト</param>
    /// <returns></returns>
    private AudioPlayer GetUnusedAudioPlayer(List<AudioPlayer> audioPlayers)
    {
        for (var i = 0; i < audioPlayers.Count; ++i)
        {
            if (audioPlayers[i].IsPlaying == false) return audioPlayers[i];
        }

        //未使用のAudioSourceは見つかりませんでした
        return null;
    }
}
