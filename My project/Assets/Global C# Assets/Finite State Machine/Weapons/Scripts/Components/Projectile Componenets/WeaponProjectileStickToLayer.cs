using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FoxTail
{
    // Ensuring that the projectile gets stuck in a specific layer that was detected
    [RequireComponent(typeof(WeaponProjectileHitBox))]
    public class WeaponProjectileStickToLayer : WeaponProjectileComponent
    {
        [SerializeField] public UnityEvent setStuck;
        [SerializeField] public UnityEvent setUnstuck;

        [field: SerializeField] public LayerMask LayerMask { get; private set; }
        [field: SerializeField] public string InactiveSortingLayerName { get; private set; }
        [field: SerializeField] public float CheckDistance { get; private set; }

        private bool isStuck;
        private bool subscribedToDisableNotifier;
        private WeaponProjectileHitBox hitBox;

        private string activeSortingLayerName;
        private SpriteRenderer sr; 

        private OnDisableNotifier onDisableNotifier;

        private Transform referenceTransform;
        private Transform _transform;

        private Vector3 offsetPosition;
        private Quaternion offsetRotation;

        private float gravityScale;

        private void HandleRaycastHit2D(RaycastHit2D[] hits) {
            if (isStuck) {
                return;
            }

            SetStuck();

            //NOTE - REMOVE
            // var lineHit = Physics2D.Raycast(transform.position, transform.right, CheckDistance, LayerMask);

            // sr.sortingLayerName = InactiveSortingLayerName;
            // rb.velocity = Vector2.zero;
            // rb.bodyType = RigidbodyType2D.Static;

            // isStuck = true;

            var lineHit = Physics2D.Raycast(_transform.position, _transform.right, CheckDistance, LayerMask);

            if (lineHit) {

                //NOTE - REMOVE
                // transform.position = lineHit.point;

                SetReferenceTransformAndPoint(lineHit.transform, lineHit.point);
                return;
            }

            foreach (var hit in hits)
            {
                if(!LayerMaskUtilities.IsLayerInMask(hit, LayerMask)) {
                    continue;
                }
                //NOTE - REMOVE
                // transform.position = hit.point;

                SetReferenceTransformAndPoint(hit.transform, hit.point);

                return;
            }

            //NOTE - REMOVE
            // sr.sortingLayerName = activeSortingLayerName;

            SetUnstuck();
        }

        private void SetReferenceTransformAndPoint(Transform newReferenceTransform, Vector2 newPoint) {
            if (newReferenceTransform.TryGetComponent(out onDisableNotifier))
            {
                onDisableNotifier.OnDisableEvent += HandleDisableNotifier;
                subscribedToDisableNotifier = true;
            }

            _transform.position = newPoint;

            referenceTransform = newReferenceTransform;
            offsetPosition = _transform.position - referenceTransform.position;
            offsetRotation = Quaternion.Inverse(referenceTransform.rotation) * _transform.rotation;
        }

        private void SetStuck()
        {
            isStuck = true;

            sr.sortingLayerName = InactiveSortingLayerName;
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;

            setStuck?.Invoke();
        }

        private void SetUnstuck() {

            isStuck = false;

            sr.sortingLayerName = activeSortingLayerName;

            rb.bodyType = RigidbodyType2D.Dynamic;

            rb.gravityScale = gravityScale;

            setUnstuck?.Invoke();
        }

        private void HandleDisableNotifier() {
            SetUnstuck();

            if (!subscribedToDisableNotifier) return;
            
            onDisableNotifier.OnDisableEvent -= HandleDisableNotifier;
            subscribedToDisableNotifier = false;
        }

        protected override void Awake()
        {
            base.Awake();

            gravityScale = rb.gravityScale;

            _transform = transform;

            sr = GetComponentInChildren<SpriteRenderer>();

            activeSortingLayerName = sr.sortingLayerName;

            hitBox = GetComponent<WeaponProjectileHitBox>();

            hitBox.OnRaycastHit2D += HandleRaycastHit2D;
        }

        protected override void Update()
        {
            base.Update();

            if (!isStuck) {
                return;
            }

            if (!referenceTransform) {
                SetUnstuck();
                return;
            }

            var referenceRotation = referenceTransform.rotation;
            _transform.position = referenceTransform.position + referenceRotation * offsetPosition;
            _transform.rotation = referenceRotation * offsetRotation;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            hitBox.OnRaycastHit2D -= HandleRaycastHit2D;

            if (subscribedToDisableNotifier)
            {
                onDisableNotifier.OnDisableEvent -= HandleDisableNotifier;
            }

        }

        protected override void Reset()
        {
            base.Reset();

            SetStuck();
        }
    }
}
