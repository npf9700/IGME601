using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private bool isLocked;

    [SerializeField]
    private bool isLeft;

    public bool IsLocked
    {
        get { return isLocked; }
        set { isLocked = value; }
    }

    public bool IsLeft
    {
        get { return isLeft; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocked)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public bool CheckOverlap(Player p)
    {
        float x = p.transform.position.x;
        float y = p.transform.position.y;
        float width = p.GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2;
        float height = p.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2;
        if (this.GetComponent<SpriteRenderer>().sprite.bounds.size.x + this.transform.position.x < x - width)
            return false;
        if (this.GetComponent<SpriteRenderer>().sprite.bounds.size.y + this.transform.position.y < y - height)
            return false;
        if (this.transform.position.x - this.GetComponent<SpriteRenderer>().sprite.bounds.size.x > x + width)
            return false;
        if (this.transform.position.y - this.GetComponent<SpriteRenderer>().sprite.bounds.size.y > y + height)
            return false;
        return true;
    }
}
