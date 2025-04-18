using UnityEngine;
using UnityEngine.UI;

public class ProgressionManager : MonoBehaviour
{
    public static ProgressionManager Instance { get; private set; }



    public int currentProgression = 1;

   

    public bool hasRedbull, hasMoonLight, hasRadioactiveEarth;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            Debug.LogWarning($"multiple progression managers in scene. The extra one was added in this scene - it needs to be removed");
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
