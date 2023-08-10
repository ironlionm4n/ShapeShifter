using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnSlopeHelper : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private int _previousDirection = 1;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void RotateOnSlope(Quaternion rotation, int direction, float angle)
    {
        transform.rotation = Quaternion.Euler(rotation.x,rotation.y, angle);

        if (_spriteRenderer != null)
        {
            if (_previousDirection != direction)
            {
                _spriteRenderer.flipX = direction == -1;
                _previousDirection = direction;
            }
        }
    }
    
    
}
