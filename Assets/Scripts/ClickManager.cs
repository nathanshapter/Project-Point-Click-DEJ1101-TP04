using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ClickManager : MonoBehaviour
{

   [SerializeField] ActiveScene activeScene;
    [SerializeField] TextMeshProUGUI gameText;
    [SerializeField] ProgressionManager pm;
    [SerializeField] PocketManager pocketManager;

   


    ScreenFader screenFader;

    private string currentCode = "";
    [SerializeField] int doorCode =6965;

    [SerializeField] ClickableItem moon;

    private void Awake()
    {
        if(activeScene == null)
        {
            FindFirstObjectByType<ActiveScene>();
        }
        screenFader = GetComponent<ScreenFader>();
    }

    // Update is called once per frame
    void Update()
    {
        // click 

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (Input.GetMouseButtonDown(0)) // Left-click
        {
          
            


            if (hit.collider != null && Input.GetMouseButtonDown(0))
            {
                ClickableItem item = hit.collider.GetComponent<ClickableItem>();

                if (item.isDiggable)
                {
                    if (pocketManager.hasShovel)
                    {
                        gameText.text = "Okay... and dig dig dig, and I found, an anchor?";
                        moon.canBePutInPocket = true;
                    }
                    else
                    {
                        gameText.text = "I should probablky find something that can be used to dig";
                    }

                        return;
                }


                if (item.isKeypad)
                {
                    Door unlockedDoor = item.GetComponentInParent<Door>();

                    if(item.keypadNumber ==0)
                    {
                        unlockedDoor.CloseKeyPad();
                        return;
                    }



                    currentCode += item.keypadNumber.ToString();

                    gameText.text = currentCode;


                   if(currentCode.Length == doorCode.ToString().Length)
                    {
                        if(currentCode == doorCode.ToString())
                        {
                            gameText.text = "Correct! The door has unlocked";

                            

                            unlockedDoor.isLocked = false;
                            unlockedDoor.keyPad.SetActive(false);
                           

                        }
                        else
                        {
                            gameText.text = "That is not the code!";
                            currentCode = "";
                        }
                    }
                    
                    return;
                }

                if(item.GetComponent<Door>() != null)
                {
                    Door clickedDoor = item.GetComponent<Door>();
                    print("door clicked");

                    if (clickedDoor.isLocked )
                    {
                        print("door is locked");
                        gameText.text = "The Door is locked. There is a keypad.";
                        clickedDoor.OpenKeyPad();
                        return;
                    }

                    int getDoorPassageNumber = item.GetComponent<Door>().doorPassage;

                    gameText.text = "You open the door.";

                    activeScene.ActivateScene(getDoorPassageNumber);


                    return;
                }
                else if (item.isTimeLever)
                {


                    print("time lever has been clicked and all doors should be unlocked");
                   



                   Animator itemAnim= item.GetComponent<Animator>();

                    itemAnim.SetTrigger("Play");
                }

                else if (item != null)
                {
                    if (item.objectText != null)
                    {

                        switch (pm.currentProgression)
                        {
                            case 1:
                                gameText.text = item.objectText;
                                break;
                            case 2:
                                gameText.text = item.objectText2;
                                break;
                            case 3:
                                gameText.text = item.objectText3;
                                break;
                        }
                    }
                    else if (item.objectText == "")

                    {
                        Debug.LogError($"This item does not have text attached{item.name}");
                    }

                    item.ProcessClick();
                }
            }

          
        }

      


    }
    public void StartEndOfGame()
    {
        print("end of games starting");

        gameText.text = "You now have all of the items required to create a ... lemon";

        screenFader.FadeToWhite();
    }
    

    private IEnumerator ScreenFadeOut()
    {
        yield return new Break();
    }
}
