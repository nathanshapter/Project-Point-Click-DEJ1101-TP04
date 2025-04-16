using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class PocketManager : MonoBehaviour
{
    [SerializeField] bool hasRedbull, hasMoonLight, hasRadioctiveEarth;
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
                hasRadioctiveEarth = true;
                
                break;


        }
        print(hasRedbull);
        print(hasMoonLight);
        print(hasRadioctiveEarth);
    }
}
