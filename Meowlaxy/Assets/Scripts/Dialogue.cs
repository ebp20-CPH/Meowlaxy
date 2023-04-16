using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] GameObject ParentToClose;

    [SerializeField] EnemySpawner es;
    [SerializeField] PlayerController pc;


    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed = .4f;

    private int index;
    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 0"))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else 
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }

    }
    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            es.SetIsTicking(true);
            gameObject.SetActive(false);
        }
    }

    /*
     array size 6 lines
    speed .09
    April 16, 2023 Mission Meowxy 
     CIA Agent Salt: Professor Guvastas when creating a classified organism in his private laboratory. 
      Released a destructive monster that would sweep away all cats on planet earth.
     This evil genius disappeared from earth and his monster now known as Guvaston has traveled his way to space to easily target the earth as well as all of its cats. 
        We need you Special Agent Whiskers to defeat Guvaston. Stop him at all costs!!!
     Special Agent Whiskers: Yes sir, it is up to me to defend our planet Earth and all its cats. I will not disappoint you sir or planet earth! MEOW!!! 
     */

}