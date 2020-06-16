using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public GameObject loadingText;
    public GameObject pressAnyButton;
    public GameObject loadingCanvas;

    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(LoadScene());
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(3);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(3);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                loadingText.SetActive(false);
                pressAnyButton.SetActive(true);

                if (Input.anyKey)
                {
                    asyncOperation.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}
