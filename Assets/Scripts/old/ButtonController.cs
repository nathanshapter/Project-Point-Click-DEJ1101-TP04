using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] int buttonID = 0;



    private void OnMouseDown()
    {
        print($"{buttonID} is now active");
        TimeManager.Instance.SetTime(buttonID);
    }
}
