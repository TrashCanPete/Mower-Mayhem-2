using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CharacterSelector : MonoBehaviour
{
    public GameObject characterPrefab;
    public TextMeshProUGUI output;
    public Font font;
    const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ&.0123456789";
    public float offset = 40;
    int selection = 0;
    Vector3 startPos;
    int charCount = 0;
    bool canMoveNext = true;
    const float startCooldown = 0.2f;
    const float movingCooldown = 0.08f;
    float cooldown = startCooldown;
    private void Start()
    {
        startPos = transform.position;
        for(int i=0; i < characters.Length; i++)
        {
            GameObject g = Instantiate(characterPrefab, transform.position, Quaternion.identity);
            g.transform.position = transform.position + new Vector3(offset*i, 0, 0);
            g.transform.parent = transform;
            TextMeshProUGUI text = g.GetComponent<TextMeshProUGUI>();
            text.text = characters[i].ToString();
        }
    }
    private void Update()
    {
        float input = Input.GetAxisRaw("Horizontal");
        if (canMoveNext)
        {
            if (input == 1)
            {
                ChangeSelection(1);
            }
            else if (input == -1)
            {
                ChangeSelection(-1);
            }
        }
        if (input < 0.5f&&input>-0.5f)
        {
            StopAllCoroutines();
            cooldown = startCooldown;
            canMoveNext = true;
        }
        if (Input.GetButtonDown("Handbrake")|| Input.GetButtonDown("Submit"))
        {
            AddLetter();
        }
    }
    IEnumerator MoveNextCooldown()
    {
        canMoveNext = false;
        yield return new WaitForSeconds(cooldown);
        canMoveNext = true;
    }
    void ChangeSelection(int increment)
    {

        selection += increment;
        if (selection < 0)
            selection = characters.Length-1;
        if (selection > characters.Length-1)
            selection = 0;
        Vector3 pos = startPos + new Vector3(-offset * selection, 0, 0);
        transform.position = pos;
        StartCoroutine(MoveNextCooldown());
        cooldown = movingCooldown;
    }
    void AddLetter()
    {
        charCount++;
        output.text += characters[selection];
        if (charCount == 3)
            Finish();
    }
    void Finish()
    {
        output.gameObject.GetComponent<Animator>().SetTrigger("Flash");
        this.enabled = false;
    }
}
