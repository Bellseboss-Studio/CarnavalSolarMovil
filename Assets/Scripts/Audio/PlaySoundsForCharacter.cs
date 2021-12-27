using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent (typeof (AudioSource))]
public class PlaySoundsForCharacter : MonoBehaviour, ICheckDependencies
{
    [SerializeField] private List<AudioClip> m_FsClips;
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private string m_CharacterName;
    [SerializeField] private AudioMixerGroup m_Output;
    
    [Range(0.5f, 1.0f)]
    [SerializeField] private float m_Volume = 1.0f;

    private void Start()
    {
        m_CharacterName = this.gameObject.name
            .Replace("(Clone)", "")
            .Replace(" ", "")
            .Replace("_","");

        
    }
    public void PlayAttackSound()
    {

    }

    public void PlayDeathSound()
    {

    }
    
    public void PlayFootStepSound()
    {
        if(m_FsClips.Count > 0)
        {
            m_AudioSource.maxDistance = 40;
            m_AudioSource.volume = m_Volume - Random.Range(0f, 0.4f);
            m_AudioSource.pitch = Random.Range(0.9f, 1.2f);
            m_AudioSource.spatialBlend = 0.7f;
            m_AudioSource.outputAudioMixerGroup = m_Output;
            m_AudioSource.PlayOneShot(m_FsClips[Random.Range(0, m_FsClips.Count)]);
        }
        else
        {
            Debug.Log($"No FS Sounds for {this.gameObject.name.Replace("(Clone)", "")}");
        }
    }

    public void CheckForReferences()
    {
        if(m_AudioSource == null)
        {
            m_AudioSource = GetComponent<AudioSource>();
        }
    }
}
