using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CharacterSelectCam : MonoBehaviour
{
    public static int characterIndex = 0;
    public int charInd;
    //vector3 values for each character rotation
    //rotate between each rotation
    //index = x;
    //index % array.count to cycle between each availabe array
    //var array = array[index % array.count]
    //transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, turningRate * Time.deltaTime);
    [SerializeField]
    private GameObject[] characters;
    [SerializeField]
    private int index;
    [SerializeField]
    private int count;

    public Menu menu;
    public UnityEvent OnSelect;

    // Start is called before the first frame update
    void Start()
    {
        menu = GetComponent<Menu>();
        index = 3;
    }

    // Update is called once per frame
    void Update()
    {
        charInd = characterIndex;
        count = index % characters.Length;
        while (index <= 0)
        {
            index += characters.Length;
        }

        var choice = characters[count];
        var currentRotation = transform.rotation;
        var goalRotation = Quaternion.LookRotation(choice.transform.position - transform.position);
        transform.rotation = Quaternion.Lerp(currentRotation, goalRotation, 0.1f);

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            index++;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            index--;
        }
        if (Input.GetButtonDown("Submit"))
        {
            if (characterIndex!=2)
            {// THIS PREVENTS THE PLAYER FROM SELECTING THE THIRD CHARACTER THAT ISN'T AVAILABLE YET.
                if (OnSelect.GetPersistentEventCount() > 0)
                    OnSelect.Invoke();
                menu.SelectCharacter();
                this.enabled = false;
            }
        }
        switch (count)
        {
            case 2:
                Debug.Log("count = " + count);
                characterIndex = 2;
                break;
            case 1:
                Debug.Log("count = " + count);
                characterIndex = 1;
                break;
            default:
                Debug.Log("count = " + count);
                characterIndex = 0;
                break;

        }

    }
}
