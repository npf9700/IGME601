using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabinet : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private Color cabinetColor;
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

    // Start is called before the first frame update
    void Start()
    {
        cabinetColor = this.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        storedSuccess = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
