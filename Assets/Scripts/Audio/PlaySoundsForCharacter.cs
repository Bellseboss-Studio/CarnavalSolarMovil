using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class PlaySoundsForCharacter : MonoBehaviour, ICheckDependencies
{
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private string m_CharacterName;

    public void PlayAttackSound()
    {

    }

    public void PlayDeathSound()
    {

    }
    
    public void PlayFootStepSound()
    {
        m_CharacterName = this.gameObject.name.Replace("(Clone)", "").Replace(" ", "");
        SfxManager.Instance.PlayFootstepSound(m_CharacterName, m_AudioSource);
    }

    public void CheckForReferences()
    {
        if(m_AudioSource == null)
        {
            m_AudioSource = GetComponent<AudioSource>();
        }
    }
}
