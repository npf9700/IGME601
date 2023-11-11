using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorknob : MonoBehaviour
{
    public Transform doorknob;
    public Transform start;
    public Transform end;

    [SerializeField]
    private bool isPickUpable;

    [SerializeField]
    private Player player;

    public float speed = 1.5f;

    public GameObject doorknobTrail;

    int direction = 1;

    public void Update()
    {
        Vector2 target = currentMovementTarget();

        doorknob.position = Vector2.Lerp(doorknob.position, target, speed * Time.deltaTime);

        float distance = (target - (Vector2)doorknob.position).magnitude;

        if(distance <= 0.1f)
        {
            doorknob.transform.SetPositionAndRotation(start.position, start.rotation);
            doorknobTrail.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }

        if(CheckOverlap(player) && isPickUpable && Input.GetKeyDown(KeyCode.Space))
        {
            player.AddInventoryItem(this.transform.GetChild(0).gameObject);
            doorknobTrail.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }

    Vector2 currentMovementTarget()
    {
        if(direction == 1)
        {
            return end.position;
        }

        else
        {
            return start.position;
        }
    }

    private void OnDrawGizmos()
    {
        if (doorknob != null && start != null && end != null)
        {
            Gizmos.DrawLine(doorknob.transform.position, start.transform.position);
            Gizmos.DrawLine(doorknob.transform.position, end.transform.position);
        }
    }
    public bool CheckOverlap(Player p)
    {
        float x = p.transform.position.x;
        float y = p.transform.position.y;
        float width = p.GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2;
        float height = p.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2;
        if (this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.bounds.size.x + this.transform.GetChild(0).transform.position.x < x - width)
            return false;
        if (this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.bounds.size.y + this.transform.GetChild(0).transform.position.y < y - height)
            return false;
        if (this.transform.GetChild(0).transform.position.x - this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.bounds.size.x > x + width)
            return false;
        if (this.transform.GetChild(0).transform.position.y - this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.bounds.size.y > y + height)
            return false;
        return true;
    }
}