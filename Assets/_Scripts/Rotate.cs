using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 100f;

    private void Update()
    {
        transform.Rotate(Vector3.forward * (rotateSpeed * Time.deltaTime),Space.Self);
    }
}
