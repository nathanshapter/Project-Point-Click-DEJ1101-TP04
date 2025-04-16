using UnityEngine;

public class ActiveScene : MonoBehaviour
{
   [SerializeField] GameObject[] scenes;
    /* 0 = outside present
     * 1 = main room present
     * 2 = nuclear room present
     * 3 = outside past
     * 4 = main room past
     * 5 = nuclear room past
     */

   public int activeScene = 1; // used for door to know which time to go to

    
    private void Start()
    {
        foreach (var item in scenes)
        {
            item.SetActive(true);
        }

        ActivateScene(1);

     
    }

   public void ActivateScene(int i)
    {
        foreach (var item in scenes)
        {
            item.gameObject.SetActive(false);
        }
        scenes[i].gameObject.SetActive(true);

        activeScene = i; 
    }
}
