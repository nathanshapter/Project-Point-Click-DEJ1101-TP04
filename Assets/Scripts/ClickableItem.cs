using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class ClickableItem : MonoBehaviour
{

    // this script activates when an item is clicked

  
    
    [SerializeField] bool isDoor = false;
    public bool isTimeLever = false;
    public bool canBePutInPocket = false;
    [SerializeField] bool isPocketBackGround;

    ActiveScene activeScene;

   public string objectText, objectText2, objectText3;
   [HideInInspector] public string textetodisplay;
    Camera cam;

    public Color pastColor = new Color32(0xEF, 0xD1, 0x84, 0xFF);
    public Color presentColor = new Color32(0xD8, 0xE6, 0xEC, 0xFF);
    public float lerpSpeed = 2f;

    [SerializeField] int itemInPocketID; // items to pick up 1 is redbull 2 is moon 3 is radioactive earth 4 is shovel

   [SerializeField] GameObject putInPocketButton;
    [SerializeField] TextMeshProUGUI putInPocketText;

    [SerializeField] GameObject itemToDisable;

    [SerializeField] bool isStartMenu;
   public bool isKeypad;
    public int keypadNumber;
    [SerializeField] int startMenuID = 0;

    public bool isDiggable = false;

    [SerializeField] bool isNuclearDial = false;

    [SerializeField] bool doesClickingOpenSomething = false;
    [SerializeField] GameObject itemToOpen;

    Rigidbody2D rb;
     /*
      * 1 = start game
      * 2 = options
      * 3 = credits 
      * 4 = quit game
      */

    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        cam = Camera.main;

        if (canBePutInPocket)
        {
            putInPocketButton.gameObject.SetActive(false);
            putInPocketText.gameObject.SetActive(false);
        }
        activeScene = FindFirstObjectByType<ActiveScene>();
        if(activeScene.activeScene == 1)
        {
            cam.backgroundColor = presentColor;
        }

    }
    public void ProcessClick()
    {
        if (isNuclearDial)
        {
            itemToDisable.SetActive(false);
            return;
        }

        if (doesClickingOpenSomething)
        {
            itemToOpen.gameObject.SetActive(true);

        }

        if (isStartMenu)
        {
           

            switch (startMenuID)
            {
                case 1:
                    print("start game");
                    activeScene.ActivateScene(1);


                    break;
                case 2:
                    print("open options");
                    break;
                case 3:
                    print("open options");
                    break;
                case 4:
                    print("quit game");
                    break;
            }
                


            return;
        }


        print($"You have clicked {this.name} ");


        if (canBePutInPocket)
        {
            putInPocketButton.gameObject.SetActive(true);
            putInPocketText.gameObject.SetActive(true);

           
        }
        else if (isPocketBackGround)
        {
            PocketManager pocketManager = FindFirstObjectByType<PocketManager>();
            pocketManager.PutItemInPocket(itemInPocketID);
            
            itemToDisable.SetActive(false);
            putInPocketText.gameObject.SetActive(false);
            putInPocketButton.gameObject.SetActive(false);
            
        }
    }



    public void ChangeTime() // controlled by the lever in nuclear scene to change time from past to present and present to past
    {       

        if (cam == null)
            cam = Camera.main;

        TextMeshProUGUI gameText = FindFirstObjectByType<ActiveScene>().gameText;

        VerticalDragManager vm = FindFirstObjectByType<VerticalDragManager>();

        if(activeScene.activeScene == 2)
        {


            activeScene.ActivateScene(5);
            cam.backgroundColor = pastColor;
            gameText.text = "Hmm, la couleur de fond a chang� je me demande ce que le d�veloppeur essaie de me dire.";
        }
        else if(activeScene.activeScene == 5 )
        {
            if (!vm.explosionHasCommenced)
            {
                activeScene.ActivateScene(2);
            }
            else
            {
                activeScene.ActivateScene(7);
            }


                print("tiem changed to present");
            cam.backgroundColor = presentColor;
            gameText.text = "Hmm, la couleur de fond a chang� je me demande ce que le d�veloppeur essaie de me dire.";
        }
        else if(activeScene.activeScene == 7)
        {
            activeScene.ActivateScene(5);
            cam.backgroundColor = pastColor;
            gameText.text = "Hmm, la couleur de fond a chang� je me demande ce que le d�veloppeur essaie de me dire.";
        }
        




     /*   if (activeScene.activeScene == 2 && !FindFirstObjectByType<VerticalDragManager>().explosionHasCommenced)
        {
            activeScene.ActivateScene(5);
            print("tiem changed to past");
            cam.backgroundColor = pastColor;

            gameText.text = "Hmm, la couleur de fond a chang� je me demande ce que le d�veloppeur essaie de me dire.";

        }



        else
        {
            if (FindFirstObjectByType<VerticalDragManager>().explosionHasCommenced)
            {
                if (activeScene.activeScene == 7)
                {
                    activeScene.ActivateScene(2);
                }
                else
                {
                    activeScene.ActivateScene(7);
                }


            }
            else
            {

            }


            print("tiem changed to present");
            cam.backgroundColor = presentColor;
            gameText.text = "Hmm, la couleur de fond a chang� je me demande ce que le d�veloppeur essaie de me dire.";
        }

        */
    }
}
