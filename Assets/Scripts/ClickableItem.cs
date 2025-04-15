using UnityEngine;

public class ClickableItem : MonoBehaviour
{

    [SerializeField] bool isDoor = false;

   public string objectText;
   public void ProcessClick()
    {
        


        print($"You have clicked {this.name} ");
    }
}
