using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneshotPlayer : MonoBehaviour, ICheckDependencies
{
    [SerializeField] private AudioSource m_AudioSource;

    
    void Awake()
    {
        CheckForReferences();
        m_AudioSource.volume = Random.Range(0.8f, 0.9f);
        m_AudioSource.pitch = Random.Range(0.9f, 1.1f);
    }

    public void CheckForReferences()
    {
        if (m_AudioSource == null)
            m_AudioSource = GetComponent<AudioSource>();
    }
}
