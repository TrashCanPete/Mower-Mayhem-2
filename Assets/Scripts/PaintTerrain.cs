using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PaintTerrain : MonoBehaviour
{
    [HideInInspector] public Terrain t;
    [HideInInspector] public GameObject currentTerrain;
    public float scoreMultiplier = 1;
    [SerializeField] float paintingDist;
    public float size = 7;
    const float mapdiv = 1024;
    const float threshhold = 0.75f;
    float[,,] mainMap = new float[1, 1, 2];
    int width;
    int height;
    Vector3 prevPos;
    Vector3 distSinceUpdate;
    float localScore = 0;
    public bool isPainting = false;
    AudioSource audioSource;
    //float[] textureValues = new float[2];
    const float maxVol = 0.15f;
    private void Start()
    {
        StartCoroutine(Check());
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
            audioSource.volume = 0;
    }
    private IEnumerator Check()
    {
        while (enabled)
        {
            isPainting = false;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.up, out hit, paintingDist))
            {
                if (hit.collider.tag == "Grass")
                {
                    if (hit.transform.gameObject != currentTerrain)
                    {
                        if (hit.transform.gameObject.GetComponent<Terrain>() != null)
                        {
                            GetTerrain(hit.transform.gameObject);
                        }
                    }

                    Paint(transform.position, mainMap);
                    UpdateScore();
                }
            }
            yield return new WaitForSeconds(1 / 60);
        }
    }
    private void OnValidate()
    {
        if (t != null)
        {
            width = (int)(size * t.terrainData.alphamapWidth / t.terrainData.size.x);
            height = (int)(size * t.terrainData.alphamapHeight / t.terrainData.size.x);
            float[,,] map = new float[width, height, t.terrainData.terrainLayers.Length];
            map = SetMap(map);
            mainMap = map;
        }
    }
    void GetTerrain(GameObject obj)
    {
        Debug.Log("New Terrain here");
        t = obj.GetComponent<Terrain>();
        width = (int)(size * t.terrainData.alphamapWidth / t.terrainData.size.x);
        height = (int)(size * t.terrainData.alphamapHeight / t.terrainData.size.x);
        //int width = 7;
        //int height = 7;
        float[,,] map = new float[width, height, t.terrainData.terrainLayers.Length];
        map = SetMap(map);
        currentTerrain = obj;
        mainMap = map;
    }
    float[,,] SetMap(float[,,] map)
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                map[x, y, 0] = 1;
                map[x, y, 1] = 0;
            }
        }
        return map;
    }

    void Paint(Vector3 position, float[,,] map)
    {
        Vector3 checkingPos = GetPos(transform.position + distSinceUpdate);
        bool[] paintAndScore = CheckTexture(checkingPos);
        Vector3 offset = new Vector3(size / 2, 0, size / 2);
        Vector3 pos = GetPos(position, offset);
        if (paintAndScore[1])
        {
            localScore += 1f * Vector3.Distance(transform.position, prevPos) * scoreMultiplier;
            isPainting = true;
            if (audioSource != null)
                audioSource.volume =Mathf.Clamp(audioSource.volume+( 2 * Time.deltaTime),0,maxVol);
        }
        else
        {
            if (audioSource != null)
                audioSource.volume -= 1 * Time.deltaTime;
        }
        if (paintAndScore[0])
            t.terrainData.SetAlphamaps((int)pos.x, (int)pos.z, map);
        distSinceUpdate = (transform.position - prevPos).normalized;
        Debug.DrawLine(transform.position, transform.position + distSinceUpdate, Color.red, 1f / 60f);
        prevPos = transform.position;
    }
    void UpdateScore()
    {
        float dif = localScore - Score.Points;
        int add = (int)localScore;
        localScore -= add;
        Score.Points += add;
    }
    Vector3 GetPos(Vector3 position, Vector3 offset)
    {
        Vector3 pos = (position - offset) - t.GetPosition();
        pos.x = pos.x / t.terrainData.size.x;
        pos.z = pos.z / t.terrainData.size.z;
        pos.x = pos.x * t.terrainData.alphamapWidth;
        pos.z = pos.z * t.terrainData.alphamapHeight;
        pos.x = Mathf.Clamp(pos.x, 0, t.terrainData.alphamapWidth - width);
        pos.z = Mathf.Clamp(pos.z, 0, t.terrainData.alphamapHeight - height);
        return pos;
    }
    Vector3 GetPos(Vector3 position)
    {
        Vector3 pos = (position) - t.GetPosition();
        pos.x = pos.x / t.terrainData.size.x;
        pos.z = pos.z / t.terrainData.size.z;
        pos.x = pos.x * t.terrainData.alphamapWidth;
        pos.z = pos.z * t.terrainData.alphamapHeight;
        pos.x = Mathf.Clamp(pos.x, 0, t.terrainData.alphamapWidth - width);
        pos.z = Mathf.Clamp(pos.z, 0, t.terrainData.alphamapHeight - height);
        return pos;
    }
    bool[] CheckTexture(Vector3 position)
    {
        float[,,] aMap = t.terrainData.GetAlphamaps((int)position.x, (int)position.z, 1, 1);
        float[] textureValues = new float[aMap.GetLength(2)];
        for (int i = 0; i < aMap.GetLength(2); i++)
        {
            textureValues[i] = aMap[0, 0, i];

        }
        bool paint = false;
        bool score = false;
        if (textureValues[1] > threshhold)
        {
            score = true;
        }
        if (textureValues[1] > threshhold || textureValues[0] > threshhold)
        {
            paint = true;
        }
        bool[] paintAndScore = new bool[] { paint, score };
        return paintAndScore;
    }
}