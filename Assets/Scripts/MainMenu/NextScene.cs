using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextScene: MonoBehaviour
{
    [SerializeField] Button nextSecne;
    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] int nextSceneIndex;

    void Start()
    {
        nextSecne.onClick.AddListener(OnToTheNextScene);
    }
    private void OnToTheNextScene()
    {
        sceneLoader.LoadScene(nextSceneIndex);
    }
}
