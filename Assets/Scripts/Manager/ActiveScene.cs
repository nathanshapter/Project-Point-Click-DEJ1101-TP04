using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveScene : MonoBehaviour
{
    public static ActiveScene Instance { get; private set; }


   
    /*  
     * 1 = outside present
     * 2 = main room present
     * 3 = nuclear room present
     * 4 = outside past
     * 5= main room past
     * 6 = nuclear room past
     * 7 = mixing room
     * 8 = wife on sick bed
     *9 = start menu
     */

  

    [SerializeField] GameObject startCanvas, gameCanvas, startGameButtons;

    ClickManager cm;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            Debug.LogWarning($"multiple active scene managers in scene. The extra one was added in this scene - it needs to be removed");
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        cm = FindFirstObjectByType<ClickManager>();
        //StartScene();

    }

 



    public void ActivateScene(int i)
    {
        /*  foreach (var item in scenes)
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

        print("called");

        SceneManager.LoadScene(i);
    }
}
