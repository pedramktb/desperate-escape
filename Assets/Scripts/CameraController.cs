using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector2 offset;
    [SerializeField] float smoothValue;
    GameObject objectToFollow;
    public bool ShouldFollow { get; set; }


    public void Initialize(GameObject objectToFollow)
    {
        this.objectToFollow = objectToFollow;
    }
    private void FixedUpdate()
    {
        if (!ShouldFollow)
            return;
        Vector3 destination = new Vector3(objectToFollow.transform.position.x + offset.x,
                                            objectToFollow.transform.position.y + offset.y,
                                            transform.position.z);
        Vector3 smoothedDestination = Vector3.Lerp(transform.position, destination, smoothValue);
        transform.position = smoothedDestination;
    }
}
