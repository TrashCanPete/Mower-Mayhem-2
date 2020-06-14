using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportCollision : MonoBehaviour
{
    AreaSetter area;
    private void Start()
    {
        area = gameObject.GetComponentInParent<AreaSetter>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            area.touchingPlayer = true;
        }
    }
}
