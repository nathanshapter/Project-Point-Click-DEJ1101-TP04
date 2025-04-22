using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{ Material mats;
    void  OnMouseOver()
    {
        print("hovering over " + this.name);
      mats.SetInt("_Hovered", 1);
        
       
            
        
    }
   void OnMouseExit()
    {
         print("not hovering  " + this.name);
            mats.SetInt("_Hovered", 0);
        
    }
}
