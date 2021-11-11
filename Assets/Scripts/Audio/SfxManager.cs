using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SfxManager : MonoBehaviour, ICheckDependencies
{

    public static SfxManager Instance;

    [SerializeField] private List<GameObject> AttacksSfx;
    [SerializeField] private AudioMixerGroup SfxOutput;
    [SerializeField] private Dictionary<string, GameObject> SfxDictionary = new Dictionary<string, GameObject>();
    [SerializeField] private Dictionary<string, string> SDictionary = new Dictionary<string, string>();

    private void Awake()
    {
        CheckForReferences();
        AssignOutputs();
        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {

        foreach(var sfx in AttacksSfx)
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
        var clip = go.gameObject.GetComponent<AudioSource>().clip.length;
        yield return new WaitForSeconds(clip);
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
        foreach(var sfx in AttacksSfx)
        {
            var audioSource = sfx.GetComponent<AudioSource>();
            audioSource.outputAudioMixerGroup = SfxOutput;
        }
    }
}
