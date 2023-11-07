using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorknob : MonoBehaviour
{
    public Transform doorknob;
    public Transform start;
    public Transform end;

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
}
