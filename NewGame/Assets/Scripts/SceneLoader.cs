using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private Image _progress;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadLevel());
    }

    // Update is called once per frame
    IEnumerator LoadLevel()
    {
        AsyncOperation gamelevel = SceneManager.LoadSceneAsync("SampleScene");
        while(gamelevel.progress < 1)
        {
            _progress.fillAmount = gamelevel.progress;
            yield return new WaitForSecondsRealtime(100f);
        }
    }

}
