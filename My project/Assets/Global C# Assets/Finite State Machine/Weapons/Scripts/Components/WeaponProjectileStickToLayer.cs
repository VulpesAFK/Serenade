using FoxTail.Unlinked;
using UnityEngine;

namespace FoxTail
{
    [RequireComponent(typeof(WeaponProjectileHitBox))]
    public class WeaponProjectileStickToLayer : WeaponProjectileComponent
    {
        [field: SerializeField] public LayerMask LayerMask { get; private set; }
        [field: SerializeField] public float CheckDistance { get; private set; }
        [field: SerializeField] public string InactiveSortingLayerName { get; private set; }

        private bool isStuck;
        private bool subscribeToDisableNotifier;

        private WeaponProjectileHitBox hitBox;

        private string activeSortingLayerName;

        private SpriteRenderer sR;

        private OnDisableNotifier onDisableNotifier;

        private Transform referenceTransform;
        private Transform _transform;

        private Vector3 offsetPosition;
        private Quaternion offsetRotation;


        private void HandleRaycastHit2D(RaycastHit2D[] hits)
        {
            if (isStuck) return;

            SetStuck();

            var lineHit = Physics2D.Raycast(_transform.position, _transform.right, CheckDistance, LayerMask);

            if (lineHit) {
                SetReferenceTransformAndPoint(lineHit.transform, lineHit.point);
                return;
            }
            
            foreach (var hit in hits)
            {
                if (!LayerMaskUtilities.IsLayerInMask(hit, LayerMask)) continue;

                SetReferenceTransformAndPoint(lineHit.transform, lineHit.point);
                return;
            }

            
            SetUnstuck();
        }

        private void SetReferenceTransformAndPoint(Transform newReferenceTransform, Vector2 newPoint)
        {
            if (newReferenceTransform.TryGetComponent(out onDisableNotifier))
            {
                onDisableNotifier.OnDisableEvent += HandleDisableNotifier;
                subscribeToDisableNotifier = true;
            }

            // Set projectile position to detected point
            _transform.position = newPoint;

            // Set reference transform and cache position and rotation offset
            referenceTransform = newReferenceTransform;
            offsetPosition = _transform.position - referenceTransform.position;
            offsetRotation = Quaternion.Inverse(referenceTransform.rotation) * _transform.rotation;
        }
        // Set Rigidbody2D bodyType to static so that it is not affected by gravity and set sorting layer such that projectile appears behind other items
        private void SetStuck()
        {
            isStuck = true;

            sR.sortingLayerName = InactiveSortingLayerName;
            rB.velocity = Vector2.zero;
            rB.bodyType = RigidbodyType2D.Static;
        }

        // Set Rigidbody2D bodyType to dynamic so that it is affected by gravity again and set sorting layer such that projectile appears in front of other items
        private void SetUnstuck()
        {
            isStuck = false;

            sR.sortingLayerName = activeSortingLayerName;
            rB.bodyType = RigidbodyType2D.Dynamic;
        }
        // If the body we are stuck in gets disabled or destroyed, make projectile dynamic again
        private void HandleDisableNotifier()
        {
            SetUnstuck();

            if (!subscribeToDisableNotifier)
                return;

            onDisableNotifier.OnDisableEvent -= HandleDisableNotifier;
            subscribeToDisableNotifier = false;
        }

        protected override void InIt()
        {
            base.InIt();

            isStuck = false;
        }

        protected override void Awake()
        {
            base.Awake();
            _transform = transform; 

            sR = GetComponentInChildren<SpriteRenderer>();
            activeSortingLayerName = sR.sortingLayerName;
            
            hitBox = GetComponent<WeaponProjectileHitBox>();

            hitBox.OnRaycastHit2D += HandleRaycastHit2D;
        }

        protected override void Update()
        {
            base.Update();

            if (!isStuck)
                return;

            // Update position and rotation based on reference transform
            var referenceRotation = referenceTransform.rotation;
            _transform.position = referenceTransform.position + referenceRotation * offsetPosition;
            _transform.rotation = referenceRotation * offsetRotation;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            hitBox.OnRaycastHit2D -= HandleRaycastHit2D;

            if (subscribeToDisableNotifier)
            {
                onDisableNotifier.OnDisableEvent -= HandleDisableNotifier;
            }
        }
    }
}