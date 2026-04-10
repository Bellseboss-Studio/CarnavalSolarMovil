using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SnapshotsManager : MonoBehaviour
{
    [SerializeField] private List<AudioMixerGroup> m_MusicMixerOutputs;
    private Dictionary<string, AudioMixerGroup> m_MusicMixerGroups;

    [SerializeField] private List<AudioMixerSnapshot> m_Snapshots;
    private Dictionary<string, AudioMixerSnapshot> m_MixerSnapthots;


    private void Start()
    {
        foreach (var output in m_MusicMixerOutputs)
        {
            m_MusicMixerGroups.Add(output.name, output);
        }

        foreach (var snapshot in m_Snapshots)
        {
            m_MixerSnapthots.Add(snapshot.name, snapshot);
        }
    }
}
