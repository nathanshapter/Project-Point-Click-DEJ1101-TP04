using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClickManager : MonoBehaviour
{
    public static ClickManager Instance { get; private set; }


   [SerializeField] ActiveScene activeScene;
    [SerializeField] TextMeshProUGUI gameText;
    [SerializeField] ProgressionManager pm;

    [SerializeField] bool pastHasBeenVisited = false;
    public bool outsideWasUnlocked = false;


    ScreenFader screenFader;

    Scene scene;
    
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            Debug.LogWarning($"multiple click managers in scene. The extra one was added in this scene{scene.name}");
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);


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

                if(item.GetComponent<Door>() != null)
                {
                    print("door clicked");

                    if (item.GetComponent<Door>().isLocked && !outsideWasUnlocked)
                    {
                        print("door is locked");
                        gameText.text = "The Door is locked.";
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
                    pastHasBeenVisited = true;



                   Animator itemAnim= item.GetComponent<Animator>();

                    itemAnim.SetTrigger("Play");
                }

                else
                {
                    if(item.objectText != null)
                    {
                        gameText.text = item.objectText;
                    }
                }
                

               /* else if (item != null)
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
                }*/
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
