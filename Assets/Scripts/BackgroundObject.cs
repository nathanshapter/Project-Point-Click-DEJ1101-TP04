using Unity.VisualScripting;
using UnityEngine;

public class BackgroundObject : MonoBehaviour
{
    public Transform pointA, pointB;
    [SerializeField] float moveSpeed = 1f;
    public bool vaVersGauche = true;
    public GameObject enleverObjet;


    private Vector3 target;


    private void Start()
    {
        target = pointB.position;
        transform.position = pointA.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed);

        if (vaVersGauche == true && transform.position.x <= pointB.position.x)
        {
            gameObject.SetActive(false);
            enleverObjet.SetActive(false);
        }
    }
}
