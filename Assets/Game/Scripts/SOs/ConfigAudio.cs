using UnityEngine;

[CreateAssetMenu(fileName = "Config Audio", menuName = "Scriptable Objects/Config Audio")]
public class ConfigAudio : ScriptableObject
{
    public AudioClip backgroundAudio;

    public AudioClip[] missionAudios;
}
