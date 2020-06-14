using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTerrainTexture : MonoBehaviour
{
    public GameObject notMowing;
    public GameObject Mowing;
    public Transform playerTransform;
    public Terrain t;

    public int posX;
    public int posZ;
    public float[] textureValues;

    public Score score;

    void Start()
    {
        t = Terrain.activeTerrain;
        playerTransform = gameObject.transform;
        if (textureValues[0] == 0)
        {
            Debug.Log("Mowing Grass");
            Mowing.SetActive(true);
            notMowing.SetActive(false);
        }
        else if (textureValues[0] == 1)
        {
            Debug.Log("Not Mowing Grass");
            Mowing.SetActive(false);
            notMowing.SetActive(true);
        }
    }

    void Update()
    {
        // For better performance, move this out of update 
        // and only call it when you need a footstep.
        GetTerrainTexture();

        if(textureValues[0] == 0)
        {
            Debug.Log("Mowing Grass");
            Mowing.SetActive(true);
            notMowing.SetActive(false);
            Score.Points += 1;
        }
        else if(textureValues[0] == 1)
        {
            Debug.Log("Not Mowing Grass");
            Mowing.SetActive(false);
            notMowing.SetActive(true);
        }
    }

    public void GetTerrainTexture()
    {
        ConvertPosition(playerTransform.position);
        CheckTexture();
    }

    void ConvertPosition(Vector3 playerPosition)
    {
        Vector3 terrainPosition = playerPosition - t.transform.position;

        Vector3 mapPosition = new Vector3
        (terrainPosition.x / t.terrainData.size.x, 0,
        terrainPosition.z / t.terrainData.size.z);

        float xCoord = mapPosition.x * t.terrainData.alphamapWidth;
        float zCoord = mapPosition.z * t.terrainData.alphamapHeight;

        posX = (int)xCoord;
        posZ = (int)zCoord;
    }

    void CheckTexture()
    {
        float[,,] aMap = t.terrainData.GetAlphamaps(posX, posZ, 1, 1);
        textureValues[0] = aMap[0, 0, 0];
        textureValues[1] = aMap[0, 0, 1];
    }
}
