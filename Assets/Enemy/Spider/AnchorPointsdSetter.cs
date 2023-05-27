using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorPointsdSetter : MonoBehaviour
{
    public Vector2 anchorPoint;
    private void Start()
    {
        anchorPoint = this.transform.position;
        SetAnchorPoint(anchorPoint);
    }

    private void SetAnchorPoint(Vector2 anchor)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.probeAnchor.position = anchor;
        }
    }
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        
    }
}
