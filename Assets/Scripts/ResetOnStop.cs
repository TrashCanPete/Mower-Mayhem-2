using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
[RequireComponent(typeof(Terrain))]
public class ResetOnStop : MonoBehaviour
{
    float[,,] map;
    Terrain t;
    void Start()
    {
        t = GetComponent<Terrain>();
        map = t.terrainData.GetAlphamaps(0, 0, t.terrainData.alphamapWidth, t.terrainData.alphamapHeight);
    }
    private void OnDestroy()
    {
        t.terrainData.SetAlphamaps(0, 0, map);
    }
}
#endif
