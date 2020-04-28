using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {
    private Text process;//To show the loading process
    public string SceneName;
    private AsyncOperation op;
    bool isLoading = false;

    void Awake()
    {
        process = GetComponent<Text>();
        gameObject.SetActive(false);
        EventCenter.AddListener(EventDefine.StartLoadScene, StartLoadScene);
        
    }
    void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.StartLoadScene, StartLoadScene);
    }
    private void StartLoadScene()
    {
        gameObject.SetActive(true);
        StartCoroutine("Load");
    }
    //laod the scene
    IEnumerator Load()
    {
        int startProcess = -1;
        int endProcess = 100;
        while (startProcess < endProcess)
        {
            startProcess++;
            ShowProcess(startProcess);
            if (isLoading == false)
            {
                op = SceneManager.LoadSceneAsync(SceneName);
                op.allowSceneActivation = false;
                isLoading = true;
            }
            yield return new WaitForEndOfFrame();
        }
        if(startProcess == 100)
        {
            op.allowSceneActivation = true;
            StopCoroutine("Load");
        }
        
        
    }
    //Show the process of loading
    private void ShowProcess(int value)
    {
        process.text = value.ToString() + "%";
    }
}
