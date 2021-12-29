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
        PlayMusicState(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        if (m_CurrentState != SceneManager.GetActiveScene().buildIndex)
        {
            PlayMusicState(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void PlayMusicState(int state)
    {
        List<GameObject> goActiveInHierarchy = new List<GameObject>();
        var transitionGo = m_MxTracks["Transition"];


        foreach (var objects in m_MusicTracks)
        {
            if(objects.gameObject.activeInHierarchy)
            {
                goActiveInHierarchy.Add(objects.gameObject);
            }
        }

        StartCoroutine(EnableAndDisableTransition());

        var go1 = new GameObject();
        m_CurrentState = SceneManager.GetActiveScene().buildIndex;
        switch (state)
        {
            case 0:
                go1 = m_MxTracks[MusicObjects.MenuMxPlayer.ToString()];
                //go1.SetActive(true);
               // m_MixesSnapshots[0].TransitionTo(m_TransitionTime);
                StartCoroutine(EnableAndDisableMxObjects(go1, 0));
                break;
            case 1:
                go1 = m_MxTracks[MusicObjects.GameplayMxPlayer.ToString()];
                //go1.SetActive(true);
                //m_MixesSnapshots[1].TransitionTo(m_TransitionTime);
                StartCoroutine(EnableAndDisableMxObjects(go1, 1));
                break;
            case 2:
                Debug.Log("Nada por aquí, nada por allá");
                break;
            default:
                Debug.Log($"The state is: {m_CurrentState.ToString()}");
                break;
        }

        IEnumerator EnableAndDisableTransition()
        {
            transitionGo.SetActive(true);
            yield return new WaitForSeconds(4);
            transitionGo.SetActive(false);

        }

        IEnumerator EnableAndDisableMxObjects(GameObject go1, int snapshot)
        {
            go1.SetActive(true);
            m_MixesSnapshots[snapshot].TransitionTo(m_TransitionTime);
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


