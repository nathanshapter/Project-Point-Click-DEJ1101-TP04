using TMPro;
using UnityEngine;

public class ClickableItem : MonoBehaviour
{

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
    [SerializeField] GameObject itemToDisable; // used for button to remove object from scene
    

    private void Start()
    {
        cam = Camera.main;

        if (canBePutInPocket)
        {
            putInPocketButton.gameObject.SetActive(false);
            putInPocketText.gameObject.SetActive(false);
        }
       

    }
    public void ProcessClick()
    {
        


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



    public void ChangeTime()
    {
        activeScene = FindFirstObjectByType<ActiveScene>();

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
