using TMPro;
using UnityEngine;

public class ActiveScene : MonoBehaviour
{
   [SerializeField] GameObject[] scenes;
    /*  
     * 0 = outside present
     * 1 = main room present
     * 2 = nuclear room present
     * 3 = outside past
     * 4 = main room past
     * 5 = nuclear room past
     * 6 = start scene
     * 7 = nuclear room present post explosion
     * 8 = 
     */

   public int activeScene = 1; // used for door to know which time/room to go to / disables and reenables every scene

    [SerializeField] GameObject startCanvas, gameCanvas, startGameButtons;

    public TextMeshProUGUI gameText, putInButtonText;

    ClickManager cm;
    private void Start()
    {
        cm = FindFirstObjectByType<ClickManager>();
        StartScene();

    }

    private void StartScene()
    {
        foreach (var item in scenes)
        {
            item.SetActive(true);
        }


        gameCanvas.SetActive(false);
        startGameButtons.SetActive(true);
        startCanvas.SetActive(true);
        ActivateScene(6);
    }

    public void ActivateScene(int i)
    {
        foreach (var item in scenes)
        {
            item.gameObject.SetActive(false);
        }
        scenes[i].gameObject.SetActive(true);

        activeScene = i;
       

        switch (activeScene)
        {
            case 0:
                break;
            case 1:
                  break;
            case 2:
                break;
            case 3:
               
                break;
            case 4:
                break;
            case 5:
                break;
           
            case 6:
                break;
            
        }


        if(activeScene != 6) // used to transition from starting scene to game
        {
            print("Deactivate start screen and activate all game stuff");

            startCanvas.SetActive(false);
            startGameButtons.SetActive(false);
            gameCanvas.SetActive(true);
        }

        if(activeScene == 0 || activeScene == 3)
        {
          //  gameText.color = Color.black;
            putInButtonText.color = Color.white;
        }
        else
        {
           // gameText.color = Color.white;
            putInButtonText.color = Color.black;
        }
       
        if(activeScene == 6) // takes to the mixing room
        {
            print("start mixing room procedure");
        }
        else if(activeScene == 7) // takes to the room with the wife
        {
            print("start final scene");
        }
    }
}
