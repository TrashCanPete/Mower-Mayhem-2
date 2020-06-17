using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour
{
    public GameObject piece1;
    public bool destroyObject = false;
    private Vector3 playerPos;
    public bool Colliding = false;
    public int points;
    const float displayTime = 1;
    bool hit = false;
    void Update()
    {
        playerPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player"&&!hit)
        {
            Break();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player"&&!hit)
        {
            Break();
        }
    }

    private void Break()
    {
        hit = true;
        Score.Points += points;
        //BonusDisplay.ShowBonus("Destruction", points,displayTime);
        BonusDisplay.ShowBonus(ScoreTypes.Destruction, "Destruction", points);
        Instantiate(piece1, transform.position, transform.rotation);
        if (destroyObject == true)
        {
            Invoke("destroy", 0.05f);
        }
    }
    void destroy()
    {
        Destroy(this.gameObject);
    }
}
