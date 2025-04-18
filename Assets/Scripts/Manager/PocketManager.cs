using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class PocketManager : MonoBehaviour
{
    [SerializeField] bool hasRedbull, hasMoonLight, hasRadioctiveEarth;
    [SerializeField] ProgressionManager progressionManager;

    public static PocketManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            Debug.LogWarning($"multiple pocket managers in scene. The extra one was added in this scene - it needs to be removed");
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);



        if (progressionManager == null)
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
                hasRadioctiveEarth = true;
                
                break;


        }
        CheckForAllItems();
    }

    private void CheckForAllItems()
    {
        if(hasMoonLight && hasRadioctiveEarth && hasRedbull)
        {
            FindAnyObjectByType<ClickManager>().StartEndOfGame();
        }
        else
        {
            print("still need more items");
        }
    }
}
