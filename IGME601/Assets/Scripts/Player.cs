using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cainos.PixelArtTopDown_Basic;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameManager gameMgr;
    [SerializeField]
    private Rigidbody2D rb;
    private float horizontal;
    private float vertical;
    //private CharacterController character;
    private float playerSpeed;
    private Vector2 position;
    [SerializeField]
    private Color heldPaperColor;
    private bool hasKey;

    [SerializeField]
    private List<GameObject> inventory;

    private GameObject overlappedObject;

    [SerializeField]
    private Camera mainCam;
    [SerializeField]
    private Camera leftCam;
    [SerializeField]
    private Camera rightCam;
    [SerializeField]
    private Camera puzzleCam1;
    [SerializeField]
    private Camera puzzleCam2;
    private int curCam;

    private bool daydreamActivated;
    private bool firstPuzzleSolved;

    private Camera[] cams;

    public GameObject OverlappedObject
    {
        get { return overlappedObject; }
        set { overlappedObject = value; }
    }
    public Vector2 Position
    {
        get { return position; }
        set { position = value; }
    }
    public bool HasKey
    {
        get { return hasKey; }
        set { hasKey = value; }
    }
    public List<GameObject> Inventory
    {
        get { return inventory; }
    }
    public bool DaydreamActivated
    {
        get { return daydreamActivated; }
        set { daydreamActivated = value; }
    }
    public bool FirstPuzzleSolved
    {
        get { return firstPuzzleSolved; }
        set { firstPuzzleSolved = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //character = this.GetComponent<CharacterController>();
        playerSpeed = 5f;
        position = this.transform.position;
        curCam = 1;
        cams = new Camera[] { leftCam, mainCam, rightCam};
        hasKey = false;
        inventory = new List<GameObject>();
        daydreamActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(horizontal * playerSpeed, vertical * playerSpeed);

        //character.Move(new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed,
        //    Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed, 0f));

        //if (Input.GetKey(KeyCode.D))
        //{
        //    position.x += Time.deltaTime * playerSpeed;
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    position.x -= Time.deltaTime * playerSpeed;
        //}
        //if (Input.GetKey(KeyCode.W))
        //{
        //    position.y += Time.deltaTime * playerSpeed;
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    position.y -= Time.deltaTime * playerSpeed;
        //}
        //this.transform.position = position;

        position = this.transform.position;
        //if (!daydreamActivated)
        //{
            CheckCameraBoundsY();
            CheckCameraBoundsX();
        //}
        this.transform.position = position;

        if (daydreamActivated)
        {
            cams[2] = puzzleCam1;
        }

        if (firstPuzzleSolved)
        {
            cams[1] = puzzleCam2;
        }
    }

    public void CheckCameraBoundsX()
    {
        Vector2 screenPos = cams[curCam].WorldToScreenPoint(position);

        if (screenPos.x < 0)
        {
            screenPos.x = 0;
            position = cams[curCam].ScreenToWorldPoint(screenPos);
            //Debug.Log("Left");
            //ScreenTransition(-1);
            //gameMgr.IsDoorActive = false;
            //Destroy(gameMgr.Door.gameObject);
            //hasKey = false;
        }
        else if (screenPos.x > cams[curCam].pixelWidth)
        {
            screenPos.x = cams[curCam].pixelWidth;
            position = cams[curCam].ScreenToWorldPoint(screenPos);
        }
    }

    public void CheckCameraBoundsY()
    {
        Vector2 screenPos = cams[curCam].WorldToScreenPoint(position);
        if (screenPos.y < 0)
        {
            screenPos.y = 0f;
        }
        if(screenPos.y > cams[curCam].pixelHeight)
        {
            screenPos.y = cams[curCam].pixelHeight;
        }

        position = cams[curCam].ScreenToWorldPoint(screenPos);
        this.transform.position = position;
    }

    public void ScreenTransition(int dir)
    {
        //if(curCam + dir == 2 && daydreamActivated)
        //{
        //    position = cams[curCam + dir].transform.position;
        //}
        curCam += dir;
        cams[curCam - dir].enabled = false;
        cams[curCam].enabled = true;
        Vector2 screenPos = cams[curCam].WorldToScreenPoint(position);
        screenPos.y = cams[curCam].pixelHeight / 2;
        position = cams[curCam].ScreenToWorldPoint(screenPos);

        this.transform.position = position;

        //if (curCam == 2 && daydreamActivated)
        //{
        //    cams[curCam].GetComponent<CameraFollow>().enabled = true;
        //    puzzleCam1.enabled = true;
        //    mainCam.enabled = false;
        //}
    }

    public void AddInventoryItem(GameObject item)
    {
        inventory.Add(item);
    }

    public void RemoveInventoryItem(Sprite sprite)
    {
        for(int i = 0; i < inventory.Count; i++)
        {
            Debug.Log(sprite.name);
            Debug.Log(inventory[i].GetComponent<SpriteRenderer>().sprite.name);
            if(inventory[i].GetComponent<SpriteRenderer>().sprite.name == sprite.name)
            {
                Debug.Log("Found");
                inventory.RemoveAt(i);
            }
        }
    }

    public void RemoveInventoryItem(Sprite sprite, Color color)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].GetComponent<SpriteRenderer>().sprite == sprite &&
                inventory[i].GetComponent<SpriteRenderer>().color == color)
            {
                inventory.RemoveAt(i);
            }
        }
    }

   public void StopVelo()
    {
        rb.velocity = new Vector2(0f, 0f);
    }
}
