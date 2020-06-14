using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInCharacter : MonoBehaviour
{
    public Transform playerStart;
    public Vector3 playertransformPosition;
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    void Awake()
    {
        switch (CharacterSelectCam.characterIndex)
        {
            case 2:
                Spawn(player1);
                break;
            case 1:
                Spawn(player2);
                break;
            default:
                Spawn(player1);
                break;
        }
    }
    void Spawn(GameObject player)
    {
        Instantiate(player, playerStart.position, playerStart.rotation);
    }
}
