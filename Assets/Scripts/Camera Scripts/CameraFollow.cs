using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public float resetSpeed = 0.5f;
    public float cameraSpeed = 0.3f;
    public Bounds cameraBounds;
    private Transform target;
    private float offsetZ;
    private Vector3 lastTargetPosition;
    private Vector3 currentVelocity;

    private bool isFollowsPlayer;

    private void Awake()
    {
        BoxCollider2D coolider = GetComponent<BoxCollider2D>();
        coolider.size = new Vector2(Camera.main.aspect * 2f * Camera.main.orthographicSize, 11f);
        cameraBounds = coolider.bounds;
    }


    void Start()
    {
        target = GameObject.FindGameObjectWithTag(MyTags.PLAYER_TAG).transform;
        lastTargetPosition = target.position;
        offsetZ = (transform.position - transform.position).z;
        isFollowsPlayer = true;
    }

    private void FixedUpdate()
    {
        if (isFollowsPlayer)
        {
            Vector3 aheadTargetPos = target.position + Vector3.forward * offsetZ;
            if (aheadTargetPos.x >= transform.position.x)
            {
                Vector3 newCameraPosition = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref currentVelocity, cameraSpeed);
                transform.position = new Vector3(newCameraPosition.x, transform.position.y, 0);
                lastTargetPosition = target.position;
            }
        }
    }

    void Update()
    {
        
    }
}
