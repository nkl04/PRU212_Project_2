using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private ConfigAudio configAudio;

    private AudioSource audioSource;

    private void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = configAudio.backgroundAudio;
        EventHandlers.OnGameStateUpdateEvent += EventHandlers_OnGameStateUpdateEvent;
    }

    private void EventHandlers_OnGameStateUpdateEvent(GameState gameState)
    {
        if (gameState == GameState.MainMenu)
        {
            audioSource.Play();
        }
    }
}
