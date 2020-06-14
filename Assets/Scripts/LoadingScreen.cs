using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{

    public GameObject loadingText;
    public GameObject pressAnyButton;
    public GameObject loadingCanvas;

    private bool playerCanPlay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PressToPlay());
        playerCanPlay = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCanPlay == true && (Input.anyKey))
        {
            SceneManager.LoadScene(3);
        }
    }

    public IEnumerator PressToPlay()
    {
        yield return new WaitForSeconds(5f);
        loadingText.SetActive(false);
        pressAnyButton.SetActive(true);
        playerCanPlay = true;
    }
}
