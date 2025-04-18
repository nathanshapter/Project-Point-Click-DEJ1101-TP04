using UnityEngine;

public class Door : MonoBehaviour
{

  public  bool isLocked = false;
   public int doorPassage;
   public GameObject keyPad;

    private void Start()
    {
        if(keyPad != null)
        {
            keyPad.SetActive(false);
        }

       
    }

    public void OpenKeyPad()
    {
        if(keyPad != null)
        {
            keyPad.SetActive(true);
        }
        
    }

    public void CloseKeyPad()
    {
        keyPad.SetActive(false);
    }

}
