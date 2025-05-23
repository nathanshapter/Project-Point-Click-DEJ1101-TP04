using TMPro;
using UnityEngine;
using Sirenix.OdinInspector;

public class ActiveScene : MonoBehaviour
{
   [SerializeField] GameObject[] scenes;
    [SerializeField] AudioClip[] music;


    /*  
     * 0 = outside present
     * 1 = main room present
     * 2 = nuclear room present
     * 3 = outside past
     * 4 = main room past
     * 5 = nuclear room past
     * 6 = start scene
     * 7 = nuclear room present post explosion
     * 8 = final scene
     * 9 = credits
     */

    [SerializeField] AudioClip[] finalSceneMusic;
    public int activeScene = 1; // used for door to know which time/room to go to / disables and reenables every scene

    [SerializeField] GameObject startCanvas, gameCanvas, startGameButtons;

    public TextMeshProUGUI gameText, putInButtonText;

    public GameObject endGameObject;

    ClickManager cm;

    [SerializeField] TextMeshProUGUI putInPocketText;
    [SerializeField] TextMeshProUGUI throwHookText;
    [SerializeField] EndGameTexts egt;
    
   
    [InfoBox(" * 0 = outside present\r\n     * 1 = main room present\r\n     * 2 = nuclear room present\r\n     * 3 = outside past\r\n     * 4 = main room past\r\n     * 5 = nuclear room past\r\n     * 6 = start scene\r\n     * 7 = nuclear room present post explosion\r\n     * 8 = final scene")]
    


    
    public int manualSceneToActivate;


    string finalString = "Nathan Shapter \n Mathis Tanguay \n Daniel Lamoureux \n  Merci d'avoir jou� :D";

    [SerializeField] GameObject finalCredits;
    public bool isFrench = true;



    [Button("Activate Scene Manually")]
    public void ActivateSceneFromInspector()
    {
        ActivateScene(manualSceneToActivate);
    }
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
        if (isFrench)
        {
            putInPocketText.text = "Mettre en poche";
            throwHookText.text = "Lancer le grappin";
        }
        {
            putInPocketText.text = "Put in pocket";
            throwHookText.text = "Throw hook";
        }

        foreach (var item in scenes)
        {
            item.gameObject.SetActive(false);
        }
        scenes[i].gameObject.SetActive(true);

        activeScene = i;
       

        switch (activeScene)
        {
            case 0:
                SoundManager.Instance.CrossfadeMusic(music[0], 3f);

                break;
            case 1:
                SoundManager.Instance.CrossfadeMusic(music[1], 3f);
                break;
            case 2:
                SoundManager.Instance.CrossfadeMusic(music[2], 3f);
                break;
            case 3:
                SoundManager.Instance.CrossfadeMusic(music[3], 3f);
                break;
            case 4:
                SoundManager.Instance.CrossfadeMusic(music[4], 3f);
                break;
            case 5:
                SoundManager.Instance.CrossfadeMusic(music[5], 3f);
                break;
           
            case 6:
                SoundManager.Instance.FadeInMusic(music[6]);

                

                break;

            case 7:
                break;

            case 8:
                egt.SetTextLanguage();
                break;

            case 9:
                gameText.text = "";

                finalCredits.SetActive(true);

                FindFirstObjectByType<TypeWrite>().StartTyping(finalString);

               
                break;
            
        }


        if(activeScene != 6) // used to transition from starting scene to game
        {
           

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
       

        if(activeScene == 8)
        {
            if (isFrench)
            {
                gameText.text = "Apr�s des exp�riences m�ticuleuses, le Citron Dor� est maintenant entre vos mains. Qu�allez-vous en faire ?";
            }
            else
            {
                gameText.text = "After your extended experiences, the golden lemon is now in your hands. What will you do?";
            }


                endGameObject.SetActive(true);
        }
        else
        {
            endGameObject.SetActive(false);
        }
       
    }
}
