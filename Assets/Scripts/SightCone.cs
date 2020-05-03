using System.Collections.Generic;
using UnityEngine;

public class SightCone : MonoBehaviour
{
    [SerializeField]
    private float sightRange = 15;

    [SerializeField]
    private float sightAngle = 30;

    [SerializeField]
    private int numberOfRays = 30;

    private List<Ray> rays;
    private float rayAngle;

    private void Start()
    {
        rayAngle = sightAngle / numberOfRays;
    }

    private void Update()
    {
        rays = new List<Ray>();
        float startingAngle = -sightAngle / 2;
        for(int i = 0; i <= numberOfRays; ++i)
        {
            Vector3 rayDirection = Quaternion.AngleAxis(startingAngle + i * rayAngle, transform.right) * transform.forward;
            rays.Add(new Ray(transform.position, rayDirection));
            Debug.DrawRay(transform.position, rayDirection * sightRange, Color.yellow, Time.deltaTime);
        }

        Debug.DrawRay(transform.position, transform.forward * sightRange, Color.cyan, Time.deltaTime);
        Debug.DrawRay(transform.position, Quaternion.AngleAxis(sightAngle / 2, transform.right) * transform.forward * sightRange, Color.red, Time.deltaTime);
        Debug.DrawRay(transform.position, Quaternion.AngleAxis(-sightAngle / 2, transform.right) * transform.forward * sightRange, Color.red, Time.deltaTime);
    }
}
