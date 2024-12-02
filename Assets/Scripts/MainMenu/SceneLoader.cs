using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject loaderUI;
    [SerializeField] private Slider progressSlider;

    public void LoadScene (int index)
    {
        StartCoroutine(LoadScene_Coroutine(index));
    
    }
    public IEnumerator LoadScene_Coroutine(int index)
    {
        progressSlider.value = 0;
        loaderUI.SetActive(true);

        AsyncOperation asyncOparation = SceneManager.LoadSceneAsync(index);
        asyncOparation.allowSceneActivation = false;
        float progress = 0;
        while (!asyncOparation.isDone)
        {
            progress = Mathf.MoveTowards(progress, asyncOparation.progress, Time.deltaTime);
            progressSlider.value = progress;
            if(progress >= 0.9f)
            {
                progressSlider.value = 1;
                asyncOparation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
