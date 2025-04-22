using UnityEngine;

public class DiggingEvent : MonoBehaviour
{


    public GameObject xMark;
    public GameObject sol;
    public GameObject grappin;
    private Vector2 posInit;

    void Start()
    {
        posInit = sol.transform.position;
    }

    void Update()
    {

        if (!xMark.activeSelf)
        {
            sol.transform.position = new Vector2(xMark.transform.position.x + 1.0f, sol.transform.position.y);
            grappin.SetActive(true);
        } else
        {
            sol.transform.position = posInit;
            grappin.SetActive(false);
        }
    }
}
