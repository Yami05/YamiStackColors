using UnityEngine;

public class CollectorController : MonoBehaviour
{
    private Vector3 firstClickPos;
    private Vector3 lastClickPos;
    public Vector3 diffPos;
    [SerializeField] private float sensitivity;
    [SerializeField] private Camera cam;



    private bool canMove;
    public bool leftRight;

    private Rigidbody rb;
    [SerializeField] private float maxSpeed;




    private GameEvents gameEvents;


    public ColorDecider colorDeciderOfPlayer;
    public Color playerColor;
    private MaterialController materialController;
    public Material materialOfPlayer;

    private MeshRenderer meshRenderer;
    private Vector3 biggerSize;
    private Vector3 standartSize;
    private BoxCollider boxCollider;
    private void Start()
    {

        leftRight = false;
        boxCollider = GetComponent<BoxCollider>();

        canMove = false;
        meshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();

        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        gameEvents = GameEvents.instance;
        gameEvents.CameraFollow += InEnd;
        materialController = MaterialController.instance;
        gameEvents.StartTheGame += StartTheGame;
        gameEvents.FeverModeStatusDecider += OnFeverMode;
        gameEvents.FinalPartSet += InFinalPart;



        materialOfPlayer = meshRenderer.material;
        materialOfPlayer.SetColor("_Color", playerColor);
        materialController.ChangeColorStatus(colorDeciderOfPlayer, materialOfPlayer);
    }

    private void Update()
    {
        if (canMove == true)
        {

            if (Input.GetMouseButtonDown(0))
            {
                OnmouseClick();
            }
            if (Input.GetMouseButton(0))
            {
                OnMouseHold();
            }
            if (Input.GetMouseButtonUp(0))
            {
                OnMouseUp();
            }

        }
        if (transform.childCount == 1)
        {
            GameEvents.instance.GameOver?.Invoke();
        }

    }

    private void FixedUpdate()
    {
        MoveCharacterForward();

    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<IInteract>()?.Interact(transform, colorDeciderOfPlayer);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<IInteract>()?.Interact(transform, colorDeciderOfPlayer);

    }



    private void OnTriggerExit(Collider other)
    {

        other.gameObject.GetComponent<IInteractExit>()?.Exit(transform);
    }
    private void OnmouseClick()
    {
        firstClickPos = cam.ScreenToWorldPoint(Input.mousePosition);
        lastClickPos = firstClickPos;
    }

    private void OnMouseHold()
    {
        lastClickPos = cam.ScreenToWorldPoint(Input.mousePosition);
        diffPos = lastClickPos - firstClickPos;
        diffPos *= sensitivity;

    }

    private void OnMouseUp()
    {
        firstClickPos = Vector3.zero;
        lastClickPos = Vector3.zero;
        diffPos = Vector3.zero;

    }



    private void MoveCharacterForward()
    {

        rb.velocity = Vector3.Lerp(rb.velocity, new Vector3(diffPos.x * 4, rb.velocity.y, maxSpeed), 0.2f);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -6, 6), transform.position.y, transform.position.z);
        //rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

    }


    private void GameOver()
    {

        rb.isKinematic = true;
        canMove = false;
        leftRight = false;

    }
    private void StartTheGame()
    {
        canMove = true;
        rb.isKinematic = false;
        gameEvents.GameOver += GameOver;

    }

    private void OnFeverMode(FeverModeStatus feverMode, GameObject gameObject)
    {

        gameObject = this.gameObject;


        standartSize = new Vector3(3, 0.3f, 1);
        biggerSize = new Vector3(10, 0.3f, 1);

        if (feverMode == FeverModeStatus.Activated)
        {
            boxCollider.size = biggerSize;
            this.transform.GetChild(0).localScale = biggerSize;

            maxSpeed = 45;
        }
        else
        {
            boxCollider.size = standartSize;

            this.transform.GetChild(0).localScale = standartSize;
            maxSpeed = 20;
        }



    }
    private void InFinalPart(FinalPartStatus finalPartStatus)
    {
        if (finalPartStatus == FinalPartStatus.Active)
        {
            this.transform.GetChild(0).localScale = standartSize;
            canMove = false;
            leftRight = false;
            maxSpeed = 5;
        }
    }


    private void InEnd()
    {
        leftRight = false;
        maxSpeed = 0;
    }



}
