using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimator : MonoBehaviour
{
    [SerializeField] GameObject cursor;
    [SerializeField] GameObject infoScreen;
    [SerializeField] GameObject tutorialScreen;


    enum MainMenuState { Main, Info, Tutorial };
    MainMenuState mms = MainMenuState.Main;

    LevelManager lm;
    ScreenFader sf;
    GameObject CatObj;

    int cursorPos = 0;
    bool hasMoved = false;

    private void Start()
    {
        CatObj = this.gameObject;
        lm = FindObjectOfType<LevelManager>();
        sf = FindObjectOfType<ScreenFader>();
    }

    private void Update()
    {
        if (mms == MainMenuState.Main)
        {
            if (Input.GetAxis("Vertical") != 0f && !hasMoved)
            {
                cursorPos -= (int)Mathf.Sign(Input.GetAxis("Vertical"));
                cursorPos = Mathf.Clamp(cursorPos, 0, 2);

                Debug.Log(cursorPos);

                cursor.transform.position = new Vector2(-6.25f, 1.5f - (cursorPos * 1.5f));

                hasMoved = true;

                FindObjectOfType<AudioManager>().Play("Beep");
            }
            else if (Input.GetAxis("Vertical") == 0f)
            {
                hasMoved = false;
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 0"))
            {
                SelectOption();

                FindObjectOfType<AudioManager>().Play("Select");
            }
        }
        else if (mms == MainMenuState.Info)
        {
            // If any input change back to main
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 0")) 
            {
                infoScreen.SetActive(false);
                mms = MainMenuState.Main;

                FindObjectOfType<AudioManager>().Play("Beep");
            }
        }
        else if (mms == MainMenuState.Tutorial) 
        {
            // If any input change back to main
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 0"))
            {
                tutorialScreen.SetActive(false);
                mms = MainMenuState.Main;
                FindObjectOfType<AudioManager>().Play("Beep");
            }
        }

        
    }

    public void StartCatAnimation() 
    {
        StartCoroutine(CatAnim());
    }

    private void SelectOption() 
    {
        if (cursorPos == 0)
        {
            StartCatAnimation();
        }
        else if (cursorPos == 1)
        {
            // Open Popup
            infoScreen.SetActive(true);

            mms = MainMenuState.Info;
        }
        else if (cursorPos == 2) 
        {
            tutorialScreen.SetActive(true);

            mms = MainMenuState.Tutorial;
        }
    }

    IEnumerator CatAnim() 
    {
        float timer = 1f;
        sf.StartFadeOut();

        while (timer > 0f) 
        {
            timer -= Time.deltaTime;
            CatObj.transform.position = new Vector2(CatObj.transform.position.x + 6 * Time.deltaTime * (1f / timer), CatObj.transform.position.y);

            yield return null;
        }

        // Then call fade out

        lm.LoadScene(1);
    }
}
