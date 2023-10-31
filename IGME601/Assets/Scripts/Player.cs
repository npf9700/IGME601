using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameManager gameMgr;
    private CharacterController character;
    private float playerSpeed;
    private Vector2 position;
    private bool isHoldingItem;
    [SerializeField]
    private Color heldPaperColor;
    private bool hasKey;

    private GameObject overlappedObject;

    [SerializeField]
    private Camera mainCam;
    [SerializeField]
    private Camera leftCam;
    [SerializeField]
    private Camera rightCam;
    private int curCam;

    private Camera[] cams;

    public Color HeldPaperColor
    {
        get { return heldPaperColor; }
        set { heldPaperColor = value; }
    }
    public bool IsHoldingItem
    {
        get { return isHoldingItem; }
        set { isHoldingItem = value; }
    }
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

    // Start is called before the first frame update
    void Start()
    {
        character = this.GetComponent<CharacterController>();
        playerSpeed = 10f;
        position = this.transform.position;
        curCam = 1;
        cams = new Camera[] { leftCam, mainCam, rightCam};
        isHoldingItem = false;
        hasKey = false;
    }

    // Update is called once per frame
    void Update()
    {
        character.Move(new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed,
            Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed, 0f));
        position = this.transform.position;
        CheckCameraBoundsY();
        CheckCameraBoundsX();
        this.transform.position = position;
    }

    public void CheckCameraBoundsX()
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(position);

        if (curCam == 1)
        {
            if (gameMgr.IsDoorActive && hasKey == false)
            {
                if (screenPos.x < 0)
                {
                    screenPos.x = 0;
                    position = Camera.main.ScreenToWorldPoint(screenPos);
                }
                else if (screenPos.x > Camera.main.pixelWidth)
                {
                    Debug.Log("Right");
                    ScreenTransition(1);
                }
            }
            else if (hasKey || gameMgr.IsDoorActive == false)
            {
                if (screenPos.x < 0)
                {
                    Debug.Log("Left");
                    ScreenTransition(-1);
                    gameMgr.IsDoorActive = false;
                    Destroy(gameMgr.Door.gameObject);
                    hasKey = false;
                }
                else if (screenPos.x > Camera.main.pixelWidth)
                {
                    Debug.Log("Right");
                    ScreenTransition(1);
                }
            }
        }
        else if(curCam == 0)
        {
            if (screenPos.x < 0)
            {
                screenPos.x = 0;
                position = Camera.main.ScreenToWorldPoint(screenPos);
            }
            else if(screenPos.x > Camera.main.pixelWidth)
            {
                ScreenTransition(1);
            }
        }
        else if(curCam == 2)
        {
            if (screenPos.x > Camera.main.pixelWidth)
            {
                screenPos.x = Camera.main.pixelWidth;
                position = Camera.main.ScreenToWorldPoint(screenPos);
            }
            else if(screenPos.x < 0)
            {
                ScreenTransition(-1);
            }
        }
        
    }

    public void CheckCameraBoundsY()
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(position);
        if (screenPos.y < 0)
        {
            Debug.Log("OB down");
            screenPos.y = 0f;
        }
        if(screenPos.y > Camera.main.pixelHeight)
        {
            Debug.Log("OB up");
            screenPos.y = Camera.main.pixelHeight;
        }

        position = Camera.main.ScreenToWorldPoint(screenPos);
        this.transform.position = position;
    }

    private void ScreenTransition(int dir)
    {
        curCam += dir;
        cams[curCam].enabled = true;
        cams[curCam - dir].enabled = false;
    }

    private void CheckOverlap()
    {
        if(overlappedObject != null)
        {
            if (this.position.x > overlappedObject.transform.position.x + (overlappedObject.GetComponent<SpriteRenderer>().sprite.bounds.size.x))
                position.x = overlappedObject.transform.position.x + (overlappedObject.GetComponent<SpriteRenderer>().sprite.bounds.size.x);
            if (this.position.x < overlappedObject.transform.position.x - (overlappedObject.GetComponent<SpriteRenderer>().sprite.bounds.size.x))
                position.x = overlappedObject.transform.position.x - (overlappedObject.GetComponent<SpriteRenderer>().sprite.bounds.size.x);
            if (this.position.y > overlappedObject.transform.position.y + (overlappedObject.GetComponent<SpriteRenderer>().sprite.bounds.size.y))
                position.y = overlappedObject.transform.position.y + (overlappedObject.GetComponent<SpriteRenderer>().sprite.bounds.size.y);
            if (this.position.y > overlappedObject.transform.position.y - (overlappedObject.GetComponent<SpriteRenderer>().sprite.bounds.size.y))
                position.y = overlappedObject.transform.position.y - (overlappedObject.GetComponent<SpriteRenderer>().sprite.bounds.size.y);
        }
    }
}
