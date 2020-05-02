using UnityEngine;

namespace TicToc.Input
{
    public class LookAtPoint : MonoBehaviour
    {
        GameObject sphere;

        private void Start()
        {
            sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        }

        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 50, 1 << 8))
            {
                sphere.transform.position = new Vector3(hit.point.x, hit.point.y, transform.position.z);
                transform.rotation = Quaternion.LookRotation(new Vector3(hit.point.x, hit.point.y, transform.position.z) - transform.position, Vector3.up);
            }
        }
    }
}