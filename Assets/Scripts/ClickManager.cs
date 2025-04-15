using Unity.VisualScripting;
using UnityEngine;

public class ClickManager : MonoBehaviour
{

   [SerializeField] ActiveScene activeScene;


    private void Awake()
    {
        if(activeScene == null)
        {
            FindFirstObjectByType<ActiveScene>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // click 

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (Input.GetMouseButtonDown(0)) // Left-click
        {
          

            if (hit.collider != null && Input.GetMouseButtonDown(0))
            {
                ClickableItem item = hit.collider.GetComponent<ClickableItem>();

                if(item.GetComponent<Door>() != null)
                {
                    print("door clicked");

                    int getDoorPassageNumber = item.GetComponent<Door>().doorPassage;
                    activeScene.ActivateScene(getDoorPassageNumber);


                    return;
                }

                else if (item != null)
                {
                    item.ProcessClick();
                }
            }

          
        }

      


    }
 
}
