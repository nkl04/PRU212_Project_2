using System;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private ConfigAudio configAudio;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource soundPlay;

    private new void Awake()
    {
        base.Awake();
        audioSource.clip = configAudio.backgroundAudio;
        EventHandlers.OnGameStateUpdateEvent += EventHandlers_OnGameStateUpdateEvent;
        EventHandlers.OnGameStartEvent += EventHandlers_OnGameStartEvent;
        EventHandlers.OnWeaponAttackEvent += EventHandlers_OnWeaponAttackEvent;
        EventHandlers.OnEnemyHitEvent += EventHandlers_OnEnemyHitEvent;
        EventHandlers.OnExpCollectedEvent += EventHandlers_OnExpCollectedEvent;
    }

    private void OnDestroy()
    {
        EventHandlers.OnGameStateUpdateEvent -= EventHandlers_OnGameStateUpdateEvent;
        EventHandlers.OnGameStartEvent -= EventHandlers_OnGameStartEvent;
        EventHandlers.OnWeaponAttackEvent -= EventHandlers_OnWeaponAttackEvent;
        EventHandlers.OnEnemyHitEvent -= EventHandlers_OnEnemyHitEvent;
        EventHandlers.OnExpCollectedEvent -= EventHandlers_OnExpCollectedEvent;
    }

    private void EventHandlers_OnExpCollectedEvent(float arg1, float arg2, Transform p)
    {
        PlayClip(configAudio.expCollectSound, p.position);
    }

    private void EventHandlers_OnGameStartEvent(ConfigLevel level)
    {
        PlayMusicBg(configAudio.missionAudios[GameManager.Instance.ConfigLevelHolder.levels.IndexOf(level)], .3f);
    }
    private void EventHandlers_OnEnemyHitEvent(Enemy_Base obj)
    {
        PlayClip(configAudio.hitEnemySound, obj.transform.position);
    }

    private void EventHandlers_OnGameStateUpdateEvent(GameState gameState)
    {
        if (gameState == GameState.MainMenu)
        {
            PlayMusicBg(configAudio.backgroundAudio);
        }
    }
    private void EventHandlers_OnWeaponAttackEvent(Weapon weapon)
    {
        if (weapon == null) return;
        if (weapon is Shuriken)
        {
            PlayClip(configAudio.shurikenSound, weapon.Player.gameObject.transform.position);
        }
    }

    public void PlayMusicBg(AudioClip clip, float volume = 1f)
    {
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
    }
    public void PlayClip(AudioClip clips, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(clips, position, volume);
    }

    public void MuteSound()
    {
        soundPlay.mute = !soundPlay.mute;
    }
    public void MuteMusic()
    {
        audioSource.mute = !audioSource.mute;
    }
}
