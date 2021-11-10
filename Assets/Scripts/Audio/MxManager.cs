using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MxManager : MonoBehaviour, ICheckDependencies
{
    public static MxManager MxInstance;

    [SerializeField] GameObject m_MenuMusic;
    [SerializeField] GameObject m_GameplayMx;


    private void Awake()
    {
        CheckForReferences();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        m_MenuMusic.SetActive(false);

        
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

        if (m_MenuMusic == null)
            m_MenuMusic = GameObject.Find(MusicObjects.MenuMxPlayer.ToString());


    }
}
