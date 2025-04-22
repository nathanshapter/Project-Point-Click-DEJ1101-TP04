using Sirenix.OdinInspector;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ClickManager : MonoBehaviour
{
    [TabGroup("Scene & Manager", TextColor = "green", Icon = SdfIconType.GearFill)]    
    [SerializeField] ActiveScene activeScene;

    [TabGroup("Scene & Manager", TextColor = "green", Icon = SdfIconType.GearFill)]
    [SerializeField] ProgressionManager pm;

    [TabGroup("Scene & Manager", TextColor = "green", Icon = SdfIconType.GearFill)]
    [SerializeField] PocketManager pocketManager;

    [TabGroup("Scene & Manager", TextColor = "green", Icon = SdfIconType.GearFill)]
    ScreenFader screenFader;

    

    [TabGroup("UI", TextColor = "blue", Icon = SdfIconType.Thermometer)]
    [SerializeField] TextMeshProUGUI gameText;

    [TabGroup("UI", TextColor = "blue", Icon = SdfIconType.Thermometer)]
    public GameObject finalText;



    [TabGroup("Keypad_Logic", TextColor = "yellow", Icon = SdfIconType.Key)]
    [SerializeField] int doorCode = 6965;

    private string currentCode = "";

    int itemclickedtiem;


    [TabGroup("Clickables", TextColor = "orange", Icon = SdfIconType.Mouse)]
    [SerializeField] ClickableItem moon;

    [TabGroup("Clickables", TextColor = "orange", Icon = SdfIconType.Mouse)]
    [SerializeField] ClickableItem bookWithCode;

    [TabGroup("Clickables", TextColor = "orange", Icon = SdfIconType.Mouse)]
    [SerializeField] ClickableItem secondBookWithCode;

    [TabGroup("Clickables", TextColor = "orange", Icon = SdfIconType.Mouse)]
    [SerializeField] ClickableItem treeWithCode;

    ClickableItem olditem;




    private ClickableItem previouslyHoveredItem;





    private void Awake()
    {
        if (activeScene == null)
        {
            FindFirstObjectByType<ActiveScene>();
        }
        screenFader = GetComponent<ScreenFader>();
    }

    private void Start()
    {
        CreateDoorCode();

        print(doorCode);

        bookWithCode.objectText = $"Le code pour la porte est {doorCode}";
        secondBookWithCode.objectText = $"Le code pour la porte est {doorCode.ToString().Substring(0, 2)}, les dernières chiffres sont grattés. ";
        treeWithCode.objectText = $"Une note indique que le code est {doorCode} dommage que ce soit trop tard.";
    }

    [Button("Create new Door Code")]
    [InfoBox("Won't work after Game start as keypad is understood at runtime")]
    private void CreateDoorCode() // has to not create 0 because there is no 0 on keypad
    {
        string code = "";
        for (int i = 0; i < 5; i++)
        {
            code += Random.Range(1, 10).ToString(); 
        }
        doorCode = int.Parse(code);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        ClickableItem currentItem = hit.collider != null ? hit.collider.GetComponent<ClickableItem>() : null;

        if (hit.collider != null && hit.collider.GetComponent<ClickableItem>())
        {
            ClickableItem item = hit.collider.GetComponent<ClickableItem>();
            if (item == null)
                return;

            

            if (currentItem != previouslyHoveredItem)
            {
                if (previouslyHoveredItem != null)
                    previouslyHoveredItem.IncreaseScale(false); // Reset old item

                if (currentItem != null)
                    currentItem.IncreaseScale(true); // Scale new one

                previouslyHoveredItem = currentItem;
            }




        }
        if (currentItem == null && previouslyHoveredItem != null)
        {
            previouslyHoveredItem.IncreaseScale(false);
            previouslyHoveredItem = null;
        }

        if (Input.GetMouseButtonDown(0)) // Left-click
        {




            if (hit.collider != null && Input.GetMouseButtonDown(0))
            {

                ClickableItem item = hit.collider.GetComponent<ClickableItem>();
                if (item == null)
                    return;

                if (item == olditem) // i dont remember if this does anything and im too afraid to remove it
                {
                    itemclickedtiem++;
                    if (itemclickedtiem > 2)
                    {
                        itemclickedtiem = 0;
                    }

                }
                else
                {
                    itemclickedtiem = 0;
                }

                if (item.isDiggable)
                {
                    if (pocketManager.hasShovel)
                    {
                        item.Desactiver();
                        gameText.text = "D’accord… alors j’ai creusé, creusé, creusé, et j’ai trouvé… un grappin?";
                        moon.canBePutInPocket = true;
                    }
                    else
                    {
                        gameText.text = "Je devrais probablement trouver quelque chose pour creuser.";
                    }

                    return;
                }


                if (item.isKeypad)
                {
                    Door unlockedDoor = item.GetComponentInParent<Door>();

                    if (item.keypadNumber == 0)
                    {
                        unlockedDoor.CloseKeyPad();
                        return;
                    }



                    currentCode += item.keypadNumber.ToString();

                    gameText.text = currentCode;


                    if (currentCode.Length == doorCode.ToString().Length)
                    {
                        if (currentCode == doorCode.ToString())
                        {
                            gameText.text = "Correct ! La porte est déverrouillée";



                            unlockedDoor.isLocked = false;
                            unlockedDoor.keyPad.SetActive(false);


                        }
                        else
                        {
                            gameText.text = "Ce n’est pas le bon code !";
                            currentCode = "";
                        }
                    }

                    return;
                }

                if (item.GetComponent<Door>() != null)
                {
                    Door clickedDoor = item.GetComponent<Door>();
                    print($"door clicked - opening {clickedDoor.doorPassage}");

                    if (clickedDoor.isLocked)
                    {
                        print($"door is locked {clickedDoor} is locked to passage {clickedDoor.doorPassage}");
                        gameText.text = "La porte est verrouillée. Il y a un clavier.";
                        clickedDoor.OpenKeyPad();
                        return;
                    }

                    int getDoorPassageNumber = item.GetComponent<Door>().doorPassage;

                    gameText.text = "Tu ouvres la porte.";

                    activeScene.ActivateScene(getDoorPassageNumber);


                    return;
                }
                else if (item.isTimeLever)
                {


                    print("time lever has been clicked");




                    Animator itemAnim = item.GetComponent<Animator>();

                    itemAnim.SetTrigger("Play");
                }

            else if (item != null)
            {
                    string[] texts = { item.objectText, item.objectText2, item.objectText3 };

                    if (itemclickedtiem >= 0 && itemclickedtiem < texts.Length && !string.IsNullOrEmpty(texts[itemclickedtiem]))
                    {
                        gameText.text = texts[itemclickedtiem];
                    }
                    else if (item.objectText == "")

                    {
                        Debug.LogError($"This item does not have text attached{item.name}");
                    }

                    item.ProcessClick();
                }
                olditem = item;
            }


        }




    }
    public void StartEndOfGame()
    {
        print("end of games starting");

        gameText.text = "Tu as maintenant tous les objets nécessaires pour créer un... citron.";

        screenFader.FadeToWhite(false, 1, false);
    }


    private IEnumerator ScreenFadeOut()
    {
        yield return new Break();
    }
}
