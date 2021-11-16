using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SfxManager : MonoBehaviour, ICheckDependencies
{

    public static SfxManager Instance;

    [Header("UI SFx")]
    [SerializeField] private List<GameObject> m_UISfx;

    [Header ("Activation Sfx")]
    [SerializeField] private List<GameObject> m_AttacksSfx;

    [Header("Impact Sfx")]
    [SerializeField] private List<GameObject> m_ImpactSfx;


    [SerializeField] private Dictionary<string, GameObject> SfxDictionary = new Dictionary<string, GameObject>();
    [SerializeField] private Dictionary<string, string> SDictionary = new Dictionary<string, string>();

    [SerializeField] private AudioMixerGroup SfxOutput;

    private void Awake()
    {
        CheckForReferences();
        AssignOutputs();
        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {
        foreach(var sfx in m_AttacksSfx)
        {
            SfxDictionary.Add(sfx.name, sfx);
        }

        foreach (var sfx in m_ImpactSfx)
        {
            SfxDictionary.Add(sfx.name, sfx);
        }

        foreach (var sfx in m_UISfx)
        {
            SfxDictionary.Add(sfx.name, sfx);
        }
    }

    
    public void PlaySound(string name)
    {
        StartCoroutine(ActivateSfxObject(name));
    }

    IEnumerator ActivateSfxObject(string name)
    {
        var go = SfxDictionary[name];
        go.SetActive(true);
        var clipLength = go.gameObject.GetComponent<AudioSource>().clip.length;
        yield return new WaitForSeconds(clipLength);
        go.SetActive(false);
    }

    public void CheckForReferences()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void AssignOutputs()
    {
        foreach(var sfx in m_AttacksSfx)
        {
            var audioSource = sfx.GetComponent<AudioSource>();
            audioSource.outputAudioMixerGroup = SfxOutput;
        }

        foreach (var sfx in m_ImpactSfx)
        {
            var audioSource = sfx.GetComponent<AudioSource>();
            audioSource.outputAudioMixerGroup = SfxOutput;
        }
    }
}
