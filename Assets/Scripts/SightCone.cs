using System.Collections.Generic;
using System.Linq;
using TicToc.Mechanics;
using UnityEngine;

public class SightCone : MonoBehaviour
{
    [SerializeField]
    private float sightRange = 15;

    [SerializeField]
    private float sightAngle = 30;

    [SerializeField]
    private int sections = 30;

    private List<Ray> rays = new List<Ray>();
    private float rayAngle;

    private List<SightState> objectsInSight = new List<SightState>();

    private void Start()
    {
        rayAngle = sightAngle / sections;
    }

    private void Update()
    {
        rays.Clear();
        float startingAngle = -sightAngle / 2;
        for(int i = 0; i <= sections; ++i)
        {
            Vector3 rayDirection = Quaternion.AngleAxis(startingAngle + i * rayAngle, transform.right) * transform.forward;
            rays.Add(new Ray(transform.position, rayDirection));
            //Debug.DrawRay(transform.position, rayDirection * sightRange, Color.yellow, Time.deltaTime);
        }

        RaycastHit obstacleHit;
        RaycastHit[] hits;

        List<SightState> hitObjects = new List<SightState>();
        foreach (Ray ray in rays)
        {
            float distance = sightRange;

            if (Physics.Raycast(ray, out obstacleHit, sightRange, 1 << 9))
            {
                distance = obstacleHit.distance;
                //Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, Time.deltaTime);
            }
            else
            {
                //Debug.DrawRay(ray.origin, ray.direction * sightRange, Color.yellow, Time.deltaTime);
            }

            hits = Physics.RaycastAll(ray, distance);

            Debug.DrawRay(ray.origin, ray.direction * distance, Color.yellow, Time.deltaTime);

            foreach (RaycastHit hit in hits)
            {
                SightState sightState = hit.collider.gameObject.GetComponent<SightState>();
                if (sightState != null)
                {
                    sightState.SetSightState(true);
                    hitObjects.Add(sightState);
                }
            }

            IEnumerable<SightState> lostSight = objectsInSight.Where(sightState => hitObjects.Contains(sightState) == false);
            foreach(SightState sightState in lostSight)
            {
                sightState.SetSightState(false);
            }

            objectsInSight = hitObjects;
        }

        Debug.DrawRay(transform.position, transform.forward * sightRange, Color.cyan, Time.deltaTime);
        //Debug.DrawRay(transform.position, Quaternion.AngleAxis(sightAngle / 2, transform.right) * transform.forward * sightRange, Color.red, Time.deltaTime);
        //Debug.DrawRay(transform.position, Quaternion.AngleAxis(-sightAngle / 2, transform.right) * transform.forward * sightRange, Color.red, Time.deltaTime);
    }
}
