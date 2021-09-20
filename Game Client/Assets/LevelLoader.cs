using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;
    
    [HideInInspector] public bool IsAnimEnded = false;

    [SerializeField] private Animator transition;
    [SerializeField] private Slider slider;

    private float transitionTime = 1f;
    private AsyncOperation asyncLoadScene;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Destroing already exists, destroing object");
            Destroy(this);
        }

        // DontDestroyOnLoad(this.gameObject);
    }


    public void LoadScene(int _levelIndex)
    {
        StartCoroutine(LoadLevelCoroutine(_levelIndex));
    }

    IEnumerator LoadLevelCoroutine(int _levelIndex)
    {
        slider.gameObject.SetActive(true);
        asyncLoadScene = SceneManager.LoadSceneAsync(_levelIndex);
        asyncLoadScene.allowSceneActivation = false;

        transition.SetTrigger("Start");

        while (asyncLoadScene.isDone == false && IsAnimEnded == false)
        {
            float progress = Mathf.Clamp01(asyncLoadScene.progress / .9f);

            slider.value = progress;

            yield return null;
        }

        IsAnimEnded = false;
        asyncLoadScene.allowSceneActivation = true;
    }
}

