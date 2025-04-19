using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class VerticalDrag : MonoBehaviour
{
    bool isDragging = false;
    private Vector3 offset;
    private Camera cam;

    [SerializeField] float minY, maxY;
    [SerializeField] float goalYPosition;
    public bool isInCorrectPosition = false;
    [SerializeField] float tolerance = 0.3f;

    VerticalDragManager vdm;
    [SerializeField] Rotatehandle rotator;


    [SerializeField] float rotatorMinIncreaseValue = 35;
    [SerializeField] float rotatorMaxIncreaseValue = 70;

    private bool hasIncreasedValue = false;
    private void Start()
    {
        cam = Camera.main;
        vdm = FindFirstObjectByType<VerticalDragManager>();
    }

    void OnMouseDown()
    {
        isDragging = true;

       
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(transform.position.x, mouseWorldPos.y, transform.position.z);
    }

    void OnMouseUp()
    {
        isDragging = false;

        print(this.transform.position.y);
    }

    void Update()
    {


        if (isDragging)
        {
            Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);

            float targetY = mouseWorldPos.y + offset.y; 
            float clampedY = Mathf.Clamp(targetY, minY, maxY);

            transform.position = new Vector3(
                transform.position.x,
                clampedY,
                transform.position.z
            );

            if (CheckPosition())
            {
                print("button in correct spot");
                isInCorrectPosition = true;
                vdm.AllInCorrectPosition();

                if (!hasIncreasedValue)
                {
                    rotator.baseAngle += (Random.Range(rotatorMinIncreaseValue, rotatorMaxIncreaseValue));
                    hasIncreasedValue = true;
                }
           
            }
            else
            {
                isInCorrectPosition = false;
            }
        }
    }

     bool CheckPosition() 
    {
        if(Mathf.Abs(goalYPosition - transform.position.y) < tolerance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
