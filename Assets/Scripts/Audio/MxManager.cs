using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System.Collections;

public class MxManager : MonoBehaviour, ICheckDependencies
{
    public static MxManager MxInstance;
   
    [SerializeField] private float m_TransitionTime = 0.5f;
    [SerializeField] private List<GameObject> m_MusicTracks = new List<GameObject>();
    [SerializeField] private List<AudioMixerSnapshot> m_MixesSnapshots;
    [SerializeField] private AudioMixer mixer;
    private Dictionary<string, GameObject> m_MxTracks = new Dictionary<string, GameObject>();
    Transform[] transforms;
    private int m_CurrentState = 0;

    private void Awake()
    {
        CheckForReferences();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        transforms = GetComponentsInChildren<Transform>(gameObject);
        foreach(Transform child in transform)
        {
            if(child.gameObject.CompareTag("MxPlayer"))
            {
                m_MusicTracks.Add(child.gameObject);
                m_MxTracks.Add(child.gameObject.name, child.gameObject);
            }
        }
        PlayMusicState(1);
    }

    public void PlayMusicState(int state)
    {
        List<GameObject> goActiveInHierarchy = new List<GameObject>();
        var transitionGo = m_MxTracks["Transition"];
        foreach(var objects in m_MusicTracks)
        {
            if(objects.gameObject.activeInHierarchy)
            {
                goActiveInHierarchy.Add(objects.gameObject);
            }
        }

        var go1 = new GameObject();
        transitionGo.SetActive(true);
        if(m_CurrentState != state)
        {
            m_CurrentState = state;
            switch (state)
            {
                case 1:
                    go1 = m_MxTracks[MusicObjects.MenuMxPlayer.ToString()];
                    go1.SetActive(true);
                    m_MixesSnapshots[0].TransitionTo(m_TransitionTime);
                    StartCoroutine(EnableAndDisableMxObject());
                    break;
                case 7:
                    go1 = m_MxTracks[MusicObjects.GameplayMxPlayer.ToString()];
                    go1.SetActive(true);
                    m_MixesSnapshots[1].TransitionTo(m_TransitionTime);
                    StartCoroutine(EnableAndDisableMxObject());
                    break;
                case 5:
                    //go1 = m_MxTracks[MusicObjects.VictoryMxPlayer.ToString()];
                    //go1.SetActive(true);
                    //m_MixesSnapshots[2].TransitionTo(1);
                    break;
            }    
        }
        

        IEnumerator EnableAndDisableMxObject()
        {
            yield return new WaitForSeconds(m_TransitionTime);
            foreach (var activeObject in goActiveInHierarchy)
            {
                activeObject.SetActive(false);
                Debug.Log("Puff");
            }
        }
    }

    public void CheckForReferences()
    {
        if (MxInstance == null)
        {
            MxInstance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
}


