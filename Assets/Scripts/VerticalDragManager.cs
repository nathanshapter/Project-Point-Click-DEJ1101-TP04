using UnityEngine;

public class VerticalDragManager : MonoBehaviour
{
    [SerializeField] VerticalDrag button1, button2, button3;

    [SerializeField] Rotatehandle rotator;

    [SerializeField] float allCorrectRotatorSpeed = 1;

    public bool AllInCorrectPosition()
    {
        if(button1.isInCorrectPosition && button2.isInCorrectPosition && button3.isInCorrectPosition)
        {
            print("all buttons are in correct positions");
            return true;
        }
        else
        {
           
            return false;
        }
    }

    private void Update()
    {
        if (AllInCorrectPosition())
        {
            rotator.baseAngle += allCorrectRotatorSpeed * Time.deltaTime;
        }
    }
}
