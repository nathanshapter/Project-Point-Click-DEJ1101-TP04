using UnityEngine;

public class VerticalDragManager : MonoBehaviour
{
    [SerializeField] VerticalDrag button1, button2, button3;

    [SerializeField] Rotatehandle rotator;

    [SerializeField] float allCorrectRotatorSpeed = 1;

    private bool rotationTriggered = false;

    public bool explosionHasCommenced = false;


    [SerializeField] Door door;
    public bool AllInCorrectPosition()
    {
        if (button1.isInCorrectPosition && button2.isInCorrectPosition && button3.isInCorrectPosition)
        {
            print("all buttons are in correct positions");
            rotationTriggered = true;

            FindFirstObjectByType<ActiveScene>().gameText.text = "Uhmm.. yikes";
            explosionHasCommenced = true;

            door.doorPassage = 7;


            return true;
        }
        else
        {

            return false;
        }
    }

    private void Update()
    {
        if (rotationTriggered)
        {
            rotator.baseAngle += allCorrectRotatorSpeed * Time.deltaTime;
        }
    }
}
