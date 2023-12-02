using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabinet : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private Color cabinetColor;
    [SerializeField]
    private GameObject paper;
    [SerializeField]
    private Sprite daydreamSprite;

    private Sprite desiredSprite;

    private bool storedSuccess;

    public Color CabinetColor
    {
        get { return cabinetColor; }
    }
    public bool StoredSuccess
    {
        get { return storedSuccess; }
        set { storedSuccess = value; }
    }
    public Sprite DesiredSprite
    {
        get { return desiredSprite; }
    }

    // Start is called before the first frame update
    void Start()
    {
        cabinetColor = this./*transform.GetChild(0).*/GetComponent<SpriteRenderer>().color;
        storedSuccess = false;
        desiredSprite = paper.GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StoreFile()
    {
        storedSuccess = true;
        this.GetComponent<SpriteRenderer>().color = Color.yellow;
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    public void ResetCabinet()
    {
        storedSuccess = false;
        this.GetComponent<SpriteRenderer>().color = cabinetColor;
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = cabinetColor;
    }
    
    public void AlterSprite()
    {
        this.GetComponent<SpriteRenderer>().sprite = daydreamSprite;
        this.transform.localScale = new Vector2(0.75f, 0.75f);
        this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 1f);
        this.transform.GetChild(0).gameObject.SetActive(false);
    }
}
