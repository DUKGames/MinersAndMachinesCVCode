using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager i;

    [SerializeField] private List<KeyValuePairSound> keyValuePairList;
    private Dictionary<SoundType, AudioSource> soundDictionary;

    [SerializeField] private List<KeyValuePairMusic> keyValuePairList2;
    private Dictionary<MusicType, AudioSource> musicDictionary;

    public enum SoundType
    {
        ClickSound,
        UpgradeSound,
        BuySound,
        DozerEngineWorkSound,
        DozerDriveBackSound,
        ExcavatorEngineWorkSound,
        ExcavatorDriveBackSound,
        WashplantWorkSound,
        RefuelSound,
        GoldPanSound,
        BuildSound,
        SellSound,
        DrillerSound,
        PanelSound,
        LoadingScreen,
        NotEnoughtMoneySound,
    }

    private void Awake()
    {
        if (SoundManager.i == null)
        {
            i = this;
            DontDestroyOnLoad(this);
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Init()
    {
        soundDictionary = new Dictionary<SoundType, AudioSource>();
        foreach(var keyvalue in keyValuePairList)
        {
            soundDictionary.Add(keyvalue.key, keyvalue.value);
        }

        musicDictionary = new Dictionary<MusicType, AudioSource>();
        foreach (var keyvalue in keyValuePairList2)
        {
            musicDictionary.Add(keyvalue.key, keyvalue.value);
        }
    }
    
    public void PlaySound(SoundType gameSound)
    {
        if(!soundDictionary[gameSound].isPlaying)
            soundDictionary[gameSound].Play();
    }
    public void StopSound(SoundType gameSound)
    {
        if(soundDictionary[gameSound] != null)
        if (soundDictionary[gameSound].isPlaying)
            soundDictionary[gameSound].Stop();
    }

    public void PlayMusic(MusicType musicType)
    {
        if(!musicDictionary[musicType].isPlaying)
            musicDictionary[musicType].Play();
    }
    public void StopMusic(MusicType musicType)
    {
        if (musicDictionary[musicType].isPlaying)
            musicDictionary[musicType].Stop();
    }

    public enum MusicType
    {
        MenuMusic,
        GameMusic,
        GameMusic2,
    }

    [System.Serializable]
    public class KeyValuePairSound
    {
        public SoundType key;
        public AudioSource value;
    }

    [System.Serializable]
    public class KeyValuePairMusic
    {
        public MusicType key;
        public AudioSource value;
    }
}

