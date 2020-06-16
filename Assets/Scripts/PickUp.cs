using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public enum TypesOfPickUps { Timer, Multiplyer }
    public TypesOfPickUps pickUpType;
    [SerializeField]
    private int addedTime;
    public int scoreAdd;
    public LongGrassEffect longGrassCut;
    [Tooltip("This will be set automatically if a parent object contains the AreaSetter Script")] public AreaSetter area;
    private void Start()
    {
        longGrassCut = GetComponent<LongGrassEffect>();
    }
    private void Awake()
    {
        if (area == null)
            if (gameObject.GetComponentInParent<AreaSetter>() != null)
            {
                area = gameObject.GetComponentInParent<AreaSetter>();
            }
        if (area != null)
            area.AddObj(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            LongGrassEffect.longGrassCut = true;
            Score.Points += scoreAdd;
            BonusDisplay.ShowBonus("Cut Weeds", scoreAdd);
            Timer.timeRemaining += addedTime;
            if (area != null)
                area.RemoveObj(gameObject);
            Destroy(this.gameObject);
        }
    }

}
