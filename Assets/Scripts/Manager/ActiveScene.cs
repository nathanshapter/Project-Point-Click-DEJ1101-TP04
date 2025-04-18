using UnityEngine;
using UnityEngine.SceneManagement;

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
     * 6 = mixing room
     * 7 = wife on sick bed
     * 8 = start menu
     */

   public int activeScene = 1; // used for door to know which time/room to go to / disables and reenables every scene

    [SerializeField] GameObject startCanvas, gameCanvas, startGameButtons;

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
        ActivateScene(8);
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
                cm.outsideWasUnlocked = true;
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                print("start mixing room procedure");
                break;
            case 7:
                print("start final scene");
                break;
            case 8:
                break;
            case 9:
                break;
        }


        if(activeScene != 8) // used to transition from starting scene to game
        {
            print("Deactivate start screen and activate all game stuff");

            startCanvas.SetActive(false);
            startGameButtons.SetActive(false);
            gameCanvas.SetActive(true);
        }
        /*
        if(activeScene == 6) // takes to the mixing room
        {
            print("start mixing room procedure");
        }
        else if(activeScene == 7) // takes to the room with the wife
        {
            print("start final scene");
        }*/
    }
}
