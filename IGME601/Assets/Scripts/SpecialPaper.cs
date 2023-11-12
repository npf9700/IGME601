using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPaper : MonoBehaviour
{
    [SerializeField]
    private Player player;

    private Vector2 position;

    private bool moveTriggered;

    [SerializeField]
    private Sprite arrow;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        position = this.transform.position;
        moveTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (CheckOverlap(player))
        {
            if (Input.GetKeyDown(KeyCode.Space) && !moveTriggered)
            {
                moveTriggered = true;
            }
            else if(Input.GetKeyDown(KeyCode.Space) && moveTriggered)
            {
                TransformTrigger();
            }
        }

        if (moveTriggered && position.x < 9.00f)
        {
            position.x += 0.2f;
        }

        this.transform.position = position;
    }

    private bool CheckOverlap(Player p)
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

    private void TransformTrigger()
    {
        this.GetComponent<SpriteRenderer>().sprite = arrow;
        Quaternion rot = new Quaternion(0f, 0f, 0f, 0f);
        this.transform.rotation = rot;
        player.DaydreamActivated = true;
    }
}

