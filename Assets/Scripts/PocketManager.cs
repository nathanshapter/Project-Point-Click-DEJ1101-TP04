using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class PocketManager : MonoBehaviour
{
    public bool hasRedbull { get; private set; }
    public bool hasMoonLight { get; private set; }
    public bool hasRadioactiveEarth { get; private set; }
    public bool hasShovel { get; private set; }
    [SerializeField] ProgressionManager progressionManager;

    [SerializeField] ClickableItem moon;

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
                moon.canBePutInPocket = true;

                print("picked up shovel");
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
