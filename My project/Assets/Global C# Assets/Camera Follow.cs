using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform followingEntity;
    private Vector3 zOffset = new Vector3(0, 0, -10);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform cameraTransform = Camera.main.GetComponent<Transform>();

        cameraTransform.position = followingEntity.position + zOffset;
    }
}
