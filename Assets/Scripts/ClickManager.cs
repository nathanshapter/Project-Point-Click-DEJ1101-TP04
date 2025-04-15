using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ClickManager : MonoBehaviour
{

   [SerializeField] ActiveScene activeScene;
    [SerializeField] TextMeshProUGUI gameText;


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
                    if(item.objectText != null)
                    {

                        gameText.text = item.objectText;
                    }
                    else if(item.objectText == "")

                    {
                        Debug.LogError($"This item does not have text attached{item.name}");
                    }

                        item.ProcessClick();
                }
            }

          
        }

      


    }
 
}
