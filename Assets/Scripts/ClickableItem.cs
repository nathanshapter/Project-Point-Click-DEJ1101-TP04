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
    Camera cam;

    public Color pastColor = new Color32(0xEF, 0xD1, 0x84, 0xFF);
    public Color presentColor = new Color32(0xD8, 0xE6, 0xEC, 0xFF);
    public float lerpSpeed = 2f;

    [SerializeField] int itemInPocketID; // this is only for redbull, moonlight and radioactive

   [SerializeField] GameObject putInPocketButton;
    [SerializeField] TextMeshProUGUI putInPocketText;

    [SerializeField] GameObject itemToDisable;

    [SerializeField] bool isStartMenu;
    [SerializeField] int startMenuID = 0;

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
        if (isStartMenu)
        {
            print("start menu");

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

        if (activeScene.activeScene == 2)
        {
            activeScene.ActivateScene(5);
            print("tiem changed to past");        
            cam.backgroundColor = pastColor;

        }
        else
        {
            activeScene.ActivateScene(2);
            print("tiem changed to present");
            cam.backgroundColor = presentColor;
        }

        
    }
}
