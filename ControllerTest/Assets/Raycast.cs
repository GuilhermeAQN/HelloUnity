using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour{

    public LayerMask groundLayer;
    
    public const float skinWidth = .015f;
    const float distBetweenRays = .25f;
    [HideInInspector]
    public int horizontalRayCount;
    [HideInInspector]
    public int verticalRayCount;
    
    [HideInInspector]
    public float horizontalRaySpacing;
    [HideInInspector]
    public float verticalRaySpacing;

    [HideInInspector]
    public BoxCollider2D bCol2D;
    public RaycastOrigins raycastOrigins;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public struct RaycastOrigins{
        public Vector2 topRight, topLeft;
        public Vector2 bottomRight, bottomLeft;
    }
}
