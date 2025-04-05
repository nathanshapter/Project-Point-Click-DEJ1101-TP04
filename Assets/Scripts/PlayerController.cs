using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float vitessePerso = 8f;
    private Vector2 positionSouris;
    public GameObject present;
    public GameObject past;
    private float miHautPiece;
    private float miLargPiece;
    private float hautPiece;
    private float basPiece;
    private float droitePiece;
    private float gauchePiece;
    private bool deplacement;

    void Start()
    {
        miHautPiece = present.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        miLargPiece = present.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        hautPiece = present.transform.position.y + miHautPiece;
        basPiece = present.transform.position.y - miHautPiece;
        droitePiece = present.transform.position.x + miLargPiece;
        gauchePiece = present.transform.position.x - miLargPiece;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            deplacement = false;
            if (transform.position.y > basPiece && transform.position.y < hautPiece &&
                transform.position.x > gauchePiece && transform.position.x < droitePiece)
            {
                transform.position = new Vector2(transform.position.x, past.transform.position.y);
            }
            else
            {
                transform.position = new Vector2(transform.position.x, present.transform.position.y);
            }
            print(transform.position.x);
            print(droitePiece);
            print(gauchePiece);
            print(present.GetComponent<SpriteRenderer>().bounds.size.x);
        }

        if (Input.GetMouseButtonDown(1))
        {
            deplacement = true;
            positionSouris = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            positionSouris.y = transform.position.y;
        }
        if (deplacement)
        {
            transform.position = Vector2.MoveTowards(transform.position, positionSouris, vitessePerso * Time.deltaTime);
        }
    }
}
