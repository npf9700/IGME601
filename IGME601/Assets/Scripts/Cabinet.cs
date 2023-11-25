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
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
    }
}
