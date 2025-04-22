using TMPro;
using UnityEngine;
using System.Collections;

public class TypeWrite : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    [TextArea] public string fullText;
    [SerializeField] private float delay = 0.05f;

    private Coroutine typingCoroutine;

    private void Start()
    {
     
    }

    public void StartTyping(string message)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(message));
    }

    private IEnumerator TypeText(string message)
    {

        yield return new WaitForSeconds(1);

        textComponent.text = "";
        foreach (char letter in message.ToCharArray())
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(delay);
        }
    }
}
