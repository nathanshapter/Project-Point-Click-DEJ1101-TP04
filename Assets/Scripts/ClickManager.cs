using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ClickManager : MonoBehaviour
{

   [SerializeField] ActiveScene activeScene;
    [SerializeField] TextMeshProUGUI gameText;
    [SerializeField] ProgressionManager pm;

    [SerializeField] bool pastHasBeenVisited = false;

    
    private void Awake()
    {
        if(activeScene == null)
        {
            FindFirstObjectByType<ActiveScene>();
        }
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

                    if (item.GetComponent<Door>().isLocked && !pastHasBeenVisited)
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
 
}
