using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ClickManager : MonoBehaviour
{

    [SerializeField] ActiveScene activeScene;
    [SerializeField] TextMeshProUGUI gameText;
    [SerializeField] ProgressionManager pm;
    [SerializeField] PocketManager pocketManager;
    ClickableItem olditem;

    int itemclickedtiem;

    ScreenFader screenFader;

    private string currentCode = "";
  [SerializeField] int doorCode = 6965;

    [SerializeField] ClickableItem moon;

    public GameObject finalText;

    [SerializeField] ClickableItem bookWithCode, secondBookWithCode, treeWithCode;

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
        doorCode = Random.Range(0, 99999);
        print(doorCode);

        bookWithCode.objectText = $"Le code pour la porte est {doorCode}";
        secondBookWithCode.objectText = $"Le code pour la porte est {doorCode.ToString().Substring(0,2)}, les dernières chiffres sont grattés. ";
        treeWithCode.objectText = $"Une note indique que le code est {doorCode} dommage que ce soit trop tard.";
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (Input.GetMouseButtonDown(0)) // Left-click
        {




            if (hit.collider != null && Input.GetMouseButtonDown(0))
            {

                ClickableItem item = hit.collider.GetComponent<ClickableItem>();
                if (item == null)
                    return;

                if (item == olditem)
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
                        gameText.text = "D’accord… alors j’ai creusé, creusé, creusé, et j’ai trouvé… une ancre ?";
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
                if (item.objectText != "" && itemclickedtiem == 0)
                {
                    gameText.text = item.objectText;
                }

                if (item.objectText2 != "" && itemclickedtiem == 1)
                    {

                        gameText.text = item.objectText2;

                    }
                if (item.objectText3 != "" && itemclickedtiem == 2)
                    {

                    gameText.text = item.objectText3;

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

        screenFader.FadeToWhite();
    }


    private IEnumerator ScreenFadeOut()
    {
        yield return new Break();
    }
}
