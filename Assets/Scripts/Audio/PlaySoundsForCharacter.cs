using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent (typeof (AudioSource))]
public class PlaySoundsForCharacter : MonoBehaviour, ICheckDependencies
{
    [SerializeField] private List<AudioClip> m_FsClips;
    [SerializeField] private List<AudioClip> m_AttackClips;
    [SerializeField] private List<AudioClip> m_DeathClips;
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private string m_CharacterName;
    [SerializeField] private AudioMixerGroup m_Output;
    
    [Range(0.5f, 1.0f)]
    [SerializeField] private float m_FsVolume = 0.8f;
    [Range(0.5f, 1.0f)]
    [SerializeField] private float m_AttackVolume = 1.0f;
    [Range(0.5f, 1.0f)]
    [SerializeField] private float m_DeathVolume = 1.0f;

    private void Start()
    {
        m_CharacterName = this.gameObject.name
            .Replace("(Clone)", "")
            .Replace(" ", "")
            .Replace("_","");

        
    }
    public void PlayAttackSound()
    {
        SetAudioClipsToPlay(m_AttackClips, m_AttackVolume);
    }

    public void PlayDeathSound()
    {
        SetAudioClipsToPlay(m_DeathClips, m_DeathVolume);
    }
    
    public void PlayFootStepSound()
    {
        SetAudioClipsToPlay(m_FsClips, m_FsVolume);
    }

    public void CheckForReferences()
    {
        if(m_AudioSource == null)
        {
            m_AudioSource = GetComponent<AudioSource>();
        }
    }


    void SetAudioClipsToPlay(List<AudioClip> audioClips, float volume)
    {
        if (audioClips.Count > 0)
        {
            m_AudioSource.maxDistance = 40;
            m_AudioSource.volume = volume - Random.Range(0f, 0.4f);
            m_AudioSource.pitch = Random.Range(0.95f, 1.02f);
            m_AudioSource.spatialBlend = 0.7f;
            m_AudioSource.outputAudioMixerGroup = m_Output;
            if(!m_AudioSource.isPlaying)
            m_AudioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Count)]);
        }
        else
        {
            Debug.Log($"No {audioClips.ToString()} Sounds for {this.gameObject.name.Replace("(Clone)", "")}");
        }
    }

}
