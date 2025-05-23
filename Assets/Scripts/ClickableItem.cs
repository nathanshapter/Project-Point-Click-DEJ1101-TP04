﻿using System.Collections;
using TMPro;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class ClickableItem : MonoBehaviour
{

    [FoldoutGroup("Interaction Settings")]
    [SerializeField] private bool isDoor = false;

    [FoldoutGroup("Interaction Settings")]
    public bool isTimeLever = false;

    [FoldoutGroup("Interaction Settings")]
    public bool canBePutInPocket = false;

    [FoldoutGroup("Interaction Settings")]
    [SerializeField] public bool isPocketBackGround;

    [FoldoutGroup("Interaction Settings")]
    public bool isDiggable = false;

    [FoldoutGroup("Interaction Settings")]

    [SerializeField] private bool isNuclearDial = false;

    [FoldoutGroup("Interaction Settings")]
    [SerializeField] private bool isStartMenu = false;

    [FoldoutGroup("Interaction Settings")]
    public bool isKeypad = false;

    [FoldoutGroup("Interaction Settings")]
    [ShowIf("isKeypad")]
    public int keypadNumber;

    [FoldoutGroup("Interaction Settings")]


    [FoldoutGroup("Interaction Settings")]
    Vector3 originalScale;

    [FoldoutGroup("Interaction Settings")]
    [ShowIf("doesZoomOnHover")]
    [SerializeField] float increasedScale = 1.1f;

    [FoldoutGroup("Interaction Settings")]
    float timeToIncrease = 0.3f;

    [FoldoutGroup("Interaction Settings")]
    [SerializeField] bool scaleHasBeenIncreased = false;

    [FoldoutGroup("Interaction Settings")]
    [SerializeField] private bool doesClickingOpenSomething = false;

    [FoldoutGroup("Interaction Settings")]
    [ShowIf("doesClickingOpenSomething")]
    [SerializeField] private GameObject itemToOpen;

    [FoldoutGroup("Interaction Settings")]
    [ShowIf("doesClickingOpenSomething")]
    [SerializeField] private GameObject itemToOpen2;

    [FoldoutGroup("Interaction Settings")]
    [SerializeField] bool isFinalButton = false;

    [FoldoutGroup("Interaction Settings")]
    [ShowIf("isFinalButton")]
    [SerializeField] GameObject finalObjectToShow;

    [FoldoutGroup("Interaction Settings")]
    [SerializeField] bool changesLanguage = false;

    [FoldoutGroup("Interaction Settings")]
    [ShowIf("changesLanguage")]
    [SerializeField] bool isFrench = false;

    [FoldoutGroup("TextFR")]
    public string objectText;

    [FoldoutGroup("TextFR")]
    public string objectText2;

    [FoldoutGroup("TextFR")]
    public string objectText3;

    [FoldoutGroup("TextENG")]
    public string objectTextENG;

    [FoldoutGroup("TextENG")]
    public string objectText2ENG;

    [FoldoutGroup("TextENG")]
    public string objectText3ENG;

    [FoldoutGroup("Text")]
    [HideInInspector] public string textetodisplay;

    [FoldoutGroup("Pocket")]
    [SerializeField] private int itemInPocketID;

    [FoldoutGroup("Pocket")]
    [SerializeField] public GameObject putInPocketButton;

    [FoldoutGroup("Pocket")]
    [SerializeField] public TextMeshProUGUI putInPocketText;

    [FoldoutGroup("Pocket")]
    [SerializeField] private GameObject itemToDisable;

    [FoldoutGroup("Pocket")]
    [SerializeField] private GameObject itemToDisable2;

    [FoldoutGroup("Start Menu")]
    [SerializeField] private int startMenuID = 0;



    [FoldoutGroup("Scene")]
    private ActiveScene activeScene;

    [FoldoutGroup("Scene")]
    private Camera cam;

    [FoldoutGroup("Scene")]
    public Color pastColor = new Color32(0xEF, 0xD1, 0x84, 0xFF);

    [FoldoutGroup("Scene")]
    public Color presentColor = new Color32(0xD8, 0xE6, 0xEC, 0xFF);

    [FoldoutGroup("Scene")]
    public float lerpSpeed = 2f;

    [FoldoutGroup("Audio")]
    [SerializeField] bool willPlaySound = false;

    [FoldoutGroup("Audio")]
    [ShowIf("willPlaySound")]
    [SerializeField] AudioClip musicOnClick;

  

    [FoldoutGroup("Audio")]
    public bool willPlaySFX = false;

    [FoldoutGroup("Audio")]
    [ShowIf("willPlaySFX")]
    public AudioClip SFXOnClick;

    [FoldoutGroup("Audio")]
    [ShowIf("willPlaySFX")]
    [Range(0,1)] public float volumeSFX =1;

    private Rigidbody2D rb;

    ActiveScene activeSceneManager;
    /*
     * 1 = start game
     * 2 = options
     * 3 = credits 
     * 4 = quit game
     */
    Material mats;
    void OnMouseOver()
    {
        if (mats != null)
        {
            mats.SetInt("_Hovered", 1);
        }
    }
    void OnMouseExit()
    {
        if (mats != null)
        {
            mats.SetInt("_Hovered", 0);
        }
    }

GameObject previtem ;
ClickableItem previtemscript;

    private void Awake()
    {
        activeScene = FindFirstObjectByType<ActiveScene>();
    }
    private void Start()
    {
        
        if (GetComponent<SpriteRenderer>() != null) 
        {
            mats = GetComponent<SpriteRenderer>().material;
            mats.SetInt("_Hovered", 0);
        }
       

        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        cam = Camera.main;

        if (canBePutInPocket)
        {
            putInPocketButton.gameObject.SetActive(false);
            putInPocketText.gameObject.SetActive(false);
        }
        activeScene = FindFirstObjectByType<ActiveScene>();
        if (activeScene.activeScene == 1)
        {
            cam.backgroundColor = presentColor;
        }

        originalScale = transform.localScale;

    }

    public void IncreaseScale(bool yes)
    {
        if (isDoor)
            return;

        if (yes && !scaleHasBeenIncreased)
        {
            //  transform.localScale *= increasedScale;
            transform.DOScale(originalScale * increasedScale, timeToIncrease).SetEase(Ease.InSine);

            scaleHasBeenIncreased = true;
        }
        else
        {
            transform.localScale = originalScale;
            scaleHasBeenIncreased = false;
        }
        
    }


    

    public void ProcessClick(GameObject oldback,GameObject oldtext)
    {

        if (changesLanguage)
        {
            FindFirstObjectByType<ActiveScene>().isFrench = isFrench;

            FindFirstObjectByType<ClickManager>().SetDoorCodeOnBooks();
        }


        if (willPlaySFX)
        {
            if (SFXOnClick != null)
            {
                print("sfx here" + SFXOnClick.name);
                SoundManager.Instance.PlaySFX(SFXOnClick, volumeSFX);
            }
        }
        if (willPlaySound)
        {
            if (musicOnClick != null)
            {

                SoundManager.Instance.CrossfadeMusic(musicOnClick, 3);

                FindFirstObjectByType<ScreenFader>().FadeToWhite(true, 8.5f, true);
            }
           
        }

        


        if (isFinalButton)
        {
            print("final button pressed");

            finalObjectToShow.SetActive(true);

            this.gameObject.SetActive(false);

            FindFirstObjectByType<ClickManager>().finalText.SetActive(false);

            itemToDisable.SetActive(false);

            return;
        }


        if (isNuclearDial)
        {
            itemToDisable.SetActive(false);

            if (itemToDisable2 != null)
            {
                itemToDisable2.SetActive(false);
            }
            return;
        }

        if (doesClickingOpenSomething)
        {
            itemToOpen.gameObject.SetActive(true);


            if (itemToOpen2 != null)
            {
                itemToOpen2.gameObject.SetActive(true);
            }
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
         
            print(oldback +"GOSA");

        }
        
        
        if (canBePutInPocket == false )
        {   if(oldback != null&&oldtext != null)
        {
            print(oldback+"bla");
            
           oldback.SetActive(false);
            oldtext.SetActive(false);
        }
        }
        if (isPocketBackGround)
        {
            PocketManager pocketManager = FindFirstObjectByType<PocketManager>();
            pocketManager.PutItemInPocket(itemInPocketID);

            itemToDisable.SetActive(false);
            putInPocketText.gameObject.SetActive(false);
            putInPocketButton.gameObject.SetActive(false);

        }
    }


public void Makedisaprea(GameObject s,GameObject x)
{
s.SetActive(false);
x.SetActive(false);
}
    public void ChangeTime() // controlled by the lever in nuclear scene to change time from past to present and present to past
    {

        if (cam == null)
            cam = Camera.main;

        TextMeshProUGUI gameText = FindFirstObjectByType<ActiveScene>().gameText;

        VerticalDragManager vm = FindFirstObjectByType<VerticalDragManager>();

        if (activeScene.activeScene == 2)
        {


            activeScene.ActivateScene(5);
            cam.backgroundColor = pastColor;

            SetBackgroundText(gameText);

        }
        else if (activeScene.activeScene == 5)
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
            SetBackgroundText(gameText);
        }
        else if (activeScene.activeScene == 7)
        {
            activeScene.ActivateScene(5);
            cam.backgroundColor = pastColor;
            SetBackgroundText(gameText);
        }






    }

    private void SetBackgroundText(TextMeshProUGUI gameText)
    {
        if (activeScene.isFrench)
        {
            gameText.text = "Hmm, la couleur de fond a changé… je me demande ce que le développeur essaie de me dire.";
        }
        else
        {
            gameText.text = "Hmm, the background colour changed, I wonder what the developer is trying to tell me.";
        }
    }

    public void Desactiver()
    {
        gameObject.SetActive(false);
    }
}
