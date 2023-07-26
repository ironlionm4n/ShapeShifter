using System;
using UnityEngine;

namespace SLTut
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Controller2D : MonoBehaviour
    {
        [SerializeField] private LayerMask collisionMask;

        private float _maxClimbAngle = 80;
        private const float skinWidth = .015f;
        public int horizontalRayCount = 4;
        public int verticalRayCount = 4;
        public CollisionInfo collisionInfo;

        private float _horizontalRaySpacing;
        private float _verticalRaySpacing;

        private BoxCollider2D _collider;
        private RaycastOrigins _raycastOrigins;

        private void Start()
        {
            _collider = GetComponent<BoxCollider2D>();
            CalculateRaySpacing();
        }

        public void Move(Vector3 velocity)
        {
            UpdateRaycastOrigins();
            collisionInfo.Reset();
            if (velocity.x != 0) HorizontalCollisions(ref velocity);
            if (velocity.y != 0) VerticalCollisions(ref velocity);

            transform.Translate(velocity);
        }

        private void HorizontalCollisions(ref Vector3 velocity)
        {
            var directionX = Mathf.Sign(velocity.x);
            var rayLength = Mathf.Abs(velocity.x) + skinWidth;

            for (var i = 0; i < horizontalRayCount; i++)
            {
                var rayOrigin = directionX == -1 ? _raycastOrigins.bottomLeft : _raycastOrigins.bottomRight;
                rayOrigin += Vector2.up * (_horizontalRaySpacing * i);
                var hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

                Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

                if (hit)
                {
                    var slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                    if (i == 0 && slopeAngle <= _maxClimbAngle)
                    {
                        var distanceToSlopeStart = 0f;
                        if (slopeAngle != collisionInfo.oldSlopeAngle)
                        {
                            distanceToSlopeStart = hit.distance - skinWidth;
                            velocity.x -= distanceToSlopeStart * directionX;
                        }
                        ClimbSlope(ref velocity, slopeAngle);
                        velocity.x += distanceToSlopeStart * directionX;
                    }

                    if (!collisionInfo.climbingSlope || slopeAngle > _maxClimbAngle)
                    {
                        velocity.x = (hit.distance - skinWidth) * directionX;
                        rayLength = hit.distance;

                        if (collisionInfo.climbingSlope)
                        {
                            velocity.y = Mathf.Tan(collisionInfo.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x);
                        }

                        collisionInfo.left = directionX == -1;
                        collisionInfo.right = directionX == 1;
                    }

                }
            }
        }

        private void ClimbSlope(ref Vector3 velocity, float slopeAngle)
        {
            var moveDistance = Mathf.Abs(velocity.x);
            var climbVelocityY = Mathf.Sign(slopeAngle * Mathf.Deg2Rad) * moveDistance;
            if (velocity.y <= climbVelocityY)
            {
                velocity.y = climbVelocityY;
                velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
                collisionInfo.below = true;
                collisionInfo.climbingSlope = true;
                collisionInfo.slopeAngle = slopeAngle;
            }
        }

        private void VerticalCollisions(ref Vector3 velocity)
        {
            var directionY = Mathf.Sign(velocity.y);
            var rayLength = Mathf.Abs(velocity.y) + skinWidth;

            for (var i = 0; i < verticalRayCount; i++)
            {
                var rayOrigin = directionY == -1 ? _raycastOrigins.bottomLeft : _raycastOrigins.topLeft;
                rayOrigin += Vector2.right * (_verticalRaySpacing * i + velocity.x);
                var hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

                Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

                if (hit)
                {
                    velocity.y = (hit.distance - skinWidth) * directionY;
                    rayLength = hit.distance;

                    if (collisionInfo.climbingSlope)
                    {
                        velocity.x = velocity.y / Mathf.Tan(collisionInfo.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(velocity.x);
                    }
                    collisionInfo.below = directionY == -1;
                    collisionInfo.above = directionY == 1;
                }
            }
        }

        private void UpdateRaycastOrigins()
        {
            var bounds = _collider.bounds;
            bounds.Expand(skinWidth * -2);

            _raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
            _raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
            _raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
            _raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
        }

        private void CalculateRaySpacing()
        {
            var bounds = _collider.bounds;
            bounds.Expand(skinWidth * -2);

            horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
            verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

            _horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
            _verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
        }

        private struct RaycastOrigins
        {
            public Vector2 topLeft, topRight;
            public Vector2 bottomLeft, bottomRight;
        }

        public struct CollisionInfo
        {
            public bool above, below, left, right, climbingSlope;
            public float slopeAngle, oldSlopeAngle;
            
            public void Reset()
            {
                above = below = left = right = climbingSlope = false;
                oldSlopeAngle = slopeAngle;
                slopeAngle = 0;
            }
        }
    }
}