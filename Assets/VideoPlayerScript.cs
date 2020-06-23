using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoPlayerScript : MonoBehaviour
{
    public UnityEngine.Video.VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Menu Gameplay loop_V2.mp4");
        videoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
