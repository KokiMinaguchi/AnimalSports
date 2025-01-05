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

    // �Đ��p�\�[�X
    private AudioSource _audioSource;
    // �Đ����̉��̖��O
    private string _name;
    // �Đ��I����̏���
    private Action _callback;
    // �Đ��܂ł̑҂�����
    private float _delay;
    // �t�F�[�h����
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
    /// �R���X�g���N�^
    /// </summary>
    /// <param name="audioSource">AddComponent�ō쐬�����I�[�f�B�I�\�[�X</param>
    public AudioPlayer(AudioSource audioSource)
    {
        _audioSource = audioSource;
        _audioSource.playOnAwake = false;
    }
    /// <summary>
    /// �Đ��J�n
    /// </summary>
    public void Play()
    {
        _audioSource.Play();
    }
    /// <summary>
    /// �Đ���~
    /// </summary>
    public void Stop()
    {
        _audioSource.Stop();
    }
    /// <summary>
    /// �Đ��ꎞ��~
    /// </summary>
    public void Pause()
    {
        _audioSource.Pause();
    }
    /// <summary>
    /// �Đ��ĊJ
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

    // �����t�@�C���̃��\�[�X�e�[�u��
    Dictionary<string, AudioClip> audioDictionary = new Dictionary<string, AudioClip>();

    //AudioSource�i�X�s�[�J�[�j�𓯎��ɖ炵�������̐������p��
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
        // AudioSource�쐬
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
        // ���ʐݒ�
        //AudioImporter audioImporter = assetImporter as AudioImporter;
        //string path = audioImporter.assetPath;


        // menuSE�̓��m�����ɐݒ�
        // BGM�̓o�b�N�O���E���h�ǂݍ��݂ɐݒ�
        // 
        // �ʐݒ�
        AudioImporterSampleSettings audioImporterSampleSettings;
        audioImporterSampleSettings.compressionFormat = AudioCompressionFormat.Vorbis;
        audioImporterSampleSettings.quality = 100.0f;
        audioImporterSampleSettings.sampleRateSetting = AudioSampleRateSetting.OverrideSampleRate;
        audioImporterSampleSettings.sampleRateOverride = 44100;
        // 
        audioImporterSampleSettings.loadType = AudioClipLoadType.CompressedInMemory;
        // BGM,Voice�Ȃǂ�Streaming�ɐݒ�
    }


    public void PlaySE(string audioName, float volume = 1.0f,
        float pitch = 1.0f, bool isLoop = false, float delay = 0.0f, Action _callback = null)
    {
        var audioClip = audioDictionary.GetValueOrDefault(audioName);
        // null�`�F�b�N
        if (audioClip == null || volume <= 0.0f) return;

        // ���g�p��AudioPlayer���擾
        var audioPlayer = GetUnusedAudioPlayer(_audioPlayerSE);
        if (audioPlayer == null) return;
        
        // �p�����[�^��ݒ肵�čĐ�
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
        // �t�@�C�������L�[��AudioClip�擾
        var audioClip = audioDictionary.GetValueOrDefault(audioName);
        // null�`�F�b�N
        if (audioClip == null || volume <= 0.0f) return;

        // ���g�p��AudioPlayer���擾
        var audioPlayer = GetUnusedAudioPlayer(_audioPlayerBGM);
        if (audioPlayer == null) return;

        // �p�����[�^��ݒ肵�čĐ�
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
                audioPlayer.FadeOut(fadeTime);// �m�C�Y���
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
                audioPlayer.FadeIn(fadeTime);// �m�C�Y���
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
    /// ���g�p��AudioPlayer���擾
    /// </summary>
    /// <param name="audioPlayers">AudioPlayer�̃��X�g</param>
    /// <returns></returns>
    private AudioPlayer GetUnusedAudioPlayer(List<AudioPlayer> audioPlayers)
    {
        for (var i = 0; i < audioPlayers.Count; ++i)
        {
            if (audioPlayers[i].IsPlaying == false) return audioPlayers[i];
        }

        //���g�p��AudioSource�͌�����܂���ł���
        return null;
    }
}
