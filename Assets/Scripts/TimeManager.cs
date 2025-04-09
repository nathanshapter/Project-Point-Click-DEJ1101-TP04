using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

   [SerializeField] TextMeshProUGUI timeText;


    [SerializeField] GameObject past, present, future;

    private void Awake()
    {
        if(Instance!= null &&Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        timeText.text = "Present";
        present.gameObject.SetActive(true);
        future.gameObject.SetActive(false);
        past.gameObject.SetActive(false);
    }


    public void SetTime(int i)
    {
        switch (i)
        {
            case 1:

                timeText.text = "Past";

                
                present.gameObject.SetActive(false);
                future.gameObject.SetActive(false);

                past.gameObject.SetActive(true);


                break;
            case 2:
                timeText.text = "Present";

                past.gameObject.SetActive(false);             
                future.gameObject.SetActive(false);

                present.gameObject.SetActive(true);
                break;
            case 3:
                timeText.text = "Future";

                past.gameObject.SetActive(false);
                present.gameObject.SetActive(false);


                future.gameObject.SetActive(true);
                break;
        }
       
    }

  


}
