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
    bool canMoveNext = true;
    const float startCooldown = 0.2f;
    const float movingCooldown = 0.5f;
    float cooldown = startCooldown;

    void Start()
    {
        menu = GetComponent<Menu>();
        index = 3;
    }
    void Update()
    {
        charInd = characterIndex;
        count = index % characters.Length;
        if (index <= 0)
        {
            index = characters.Length;
        }

        var choice = characters[count];
        var currentRotation = transform.rotation;
        var goalRotation = Quaternion.LookRotation(choice.transform.position - transform.position);
        transform.rotation = Quaternion.Lerp(currentRotation, goalRotation, 0.1f);

        float input = Input.GetAxisRaw("Horizontal");
        if (canMoveNext)
        {
            if (Mathf.Abs(input) == 1)
                MoveNext((int)input);
        }
        if (input < 0.5f && input > -0.5f)
        {
            ResetInput();
        }
        characterIndex = count;
        if (Input.GetButtonDown("Handbrake") || Input.GetButtonDown("Submit"))
        {
            Select();
        }

    }
    private void Select()
    {
        if (characterIndex != 2)
        {// THIS PREVENTS THE PLAYER FROM SELECTING THE THIRD CHARACTER THAT ISN'T AVAILABLE YET.
            if (OnSelect.GetPersistentEventCount() > 0)
                OnSelect.Invoke();
            menu.SelectCharacter();
            if (AnalyticsController.Controller != null)
                AnalyticsController.Controller.CharacterID(characterIndex);
            this.enabled = false;
        }
    }
    void ResetInput()
    {
        StopAllCoroutines();
        cooldown = startCooldown;
        canMoveNext = true;
    }
    void MoveNext(int add)
    {
        index += add;
        StopAllCoroutines();
        cooldown = movingCooldown;
        StartCoroutine(MoveNextCooldown());
    }
    IEnumerator MoveNextCooldown()
    {
        canMoveNext = false;
        yield return new WaitForSeconds(cooldown);
        canMoveNext = true;
    }
}
