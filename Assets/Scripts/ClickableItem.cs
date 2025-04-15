using UnityEngine;

public class ClickableItem : MonoBehaviour
{

    [SerializeField] bool isDoor = false;
   public void ProcessClick()
    {
        


        print($"You have clicked {this.name} ");
    }
}
