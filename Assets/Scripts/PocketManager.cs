
using UnityEngine;


public class PocketManager : MonoBehaviour
{
    public bool hasRedbull { get; private set; }
    public bool hasMoonLight { get; private set; }
    public bool hasRadioactiveEarth { get; private set; }
    public bool hasShovel { get; private set; }
    public bool hasGrapple { get; private set; }
    [SerializeField] ProgressionManager progressionManager;

   

    private void Awake()
    {
        if(progressionManager == null)
        {
            progressionManager = FindFirstObjectByType<ProgressionManager>();
        }
    }

    public void PutItemInPocket(int i)
    {
        switch (i)
        {
            case 1:
                hasRedbull = true;
                break;
            case 2:
                hasMoonLight = true;
              
                break;
            case 3:
                hasRadioactiveEarth = true;
                break;
            case 4:
                hasShovel = true;
              
                print("picked up shovel");
                break;
            case 5:
                hasGrapple = true;

                print("picked up grapple");
                break;


        }
        CheckForAllItems();
    }

    private void CheckForAllItems()
    {
        if(hasMoonLight && hasRadioactiveEarth && hasRedbull)
        {
            FindAnyObjectByType<ClickManager>().StartEndOfGame();
        }
        else
        {
            print("still need more items");
        }
    }
}
