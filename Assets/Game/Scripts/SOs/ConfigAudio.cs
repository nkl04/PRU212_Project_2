using UnityEngine;

[CreateAssetMenu(fileName = "Config Audio", menuName = "Scriptable Objects/Config Audio")]
public class ConfigAudio : ScriptableObject
{
    public AudioClip backgroundAudio;
    public AudioClip[] missionAudios;

    [Header("UI Sounds")]
    public AudioClip bigButtonSound;
    public AudioClip smallButtonSound;

    [Header("InGame Sounds")]
    public AudioClip hitEnemySound;
    public AudioClip levelUpSound;

    [Header("Item Collect Sounds")]
    public AudioClip expCollectSound;
    public AudioClip goldCollectSound;
    public AudioClip healthCollectSound;

    [Header("Weapon Sounds")]
    public AudioClip shurikenSound;
}
