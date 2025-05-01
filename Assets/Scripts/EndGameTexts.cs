using TMPro;
using UnityEngine;

public class EndGameTexts : MonoBehaviour
{
    ActiveScene activeScene;
    private void Awake()
    {
        activeScene = FindFirstObjectByType<ActiveScene>();
    }

    [SerializeField] string squeezeFR, worshipFR, sellFR, squeezeENG, worshipENG, sellENG;
    [SerializeField] TextMeshProUGUI _squeeze, _worship, _sell;


    private void Start()
    {
        SetTextLanguage();
    }

    public void SetTextLanguage()
    {
        if (activeScene.isFrench)
        {
            print("set to french");
            _squeeze.text = squeezeFR;
            _worship.text = worshipFR;
            _sell.text = sellFR;
        }
        else
        {
            print("set to eng");
            _squeeze.text = squeezeENG;
            _worship.text = worshipENG;
            _sell.text = sellENG;
        }
    }
}
