using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float vitessePerso = 8f;
    private Vector2 positionSouris;
    public GameObject present;
    public GameObject past;
    private Animator anim;
    private float miHautPiece;
    private float miLargPiece;
    private float hautPiece;
    private float basPiece;
    private float droitePiece;
    private float gauchePiece;
    private float posFinalePresent;
    private float posFinalePasse;
    private bool deplacement = false;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();

        miHautPiece = present.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        miLargPiece = present.GetComponent<SpriteRenderer>().bounds.size.x / 2;

        hautPiece = present.transform.position.y + miHautPiece;
        basPiece = present.transform.position.y - miHautPiece;
        droitePiece = present.transform.position.x + miLargPiece;
        gauchePiece = present.transform.position.x - miLargPiece;
        posFinalePresent = present.transform.position.y - miHautPiece + 2.2f;
        posFinalePasse = past.transform.position.y - miHautPiece + 2f;
    }

    void Update()
    {
        //Changer le temps
        if (Input.GetKeyDown(KeyCode.Space))
        {
            deplacement = false;
            if (transform.position.y > basPiece && transform.position.y < hautPiece &&
                transform.position.x > gauchePiece && transform.position.x < droitePiece)
            {
                transform.position = new Vector2(transform.position.x, posFinalePasse);
            }
            else
            {
                transform.position = new Vector2(transform.position.x, posFinalePresent);
            }
        }

        //Déplacement avec le clic droit de la souris
        if (Input.GetMouseButtonDown(1))
        {
            
            deplacement = true;
            positionSouris = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            positionSouris.y = transform.position.y;

            if (positionSouris.x < transform.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            if (positionSouris.x > transform.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        if (deplacement)
        {
            transform.position = Vector2.MoveTowards(transform.position, positionSouris, vitessePerso * Time.deltaTime);

            if (transform.position != (Vector3)positionSouris)
            {
                anim.SetBool("EnMarche", true);
            }
            else
            {
                anim.SetBool("EnMarche", false);
                deplacement = false;
            }
        }

       
    }
}
