using UnityEngine;

namespace Pickup
{
    public class Pickup : MonoBehaviour
    {
        // This will store the currently detected pickup-able object
        private GameObject _currentPickupObject;
        private GameObject _objectToHold;
        
        public float forwardOffset = 2.0f;
        public float upwardOffset = 1.0f;
        public float sideOffset = 0.5f;
        public float throwForce = 10.0f;

        private bool _isNearPickupable;

        void Update()
        {
            // If the player presses the 'E' key
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_objectToHold != null)
                {
                    ThrowObject(); // If holding an object, throw it
                }
                else if (_isNearPickupable)
                {
                    PickUp(); // If near a pickup-able object and not holding anything, pick it up
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
                // Check if the collided object has the "Pickup-able" tag
            if (other.gameObject.CompareTag("PickupAble") && _objectToHold == null)
            {
                    _currentPickupObject = other.gameObject;
                    _isNearPickupable = true;
                    //add more logic here if needed, like highlighting the object
            }
        }

        private void OnTriggerExit(Collider other)
        {
            // Check if the leaving object is the currently detected pickup-able object
            if (other.gameObject == _currentPickupObject)
            {
                _currentPickupObject = null;
                _isNearPickupable = false;
                // logic when the object is no longer in range can go here
            }
        }
        
        private void PickUp() 
        {
            if (_currentPickupObject != null && _objectToHold == null)
            {
                // Assign the current object to the holding variable
                _objectToHold = _currentPickupObject;

                // Disable the collider while holding the object
                Collider component = _objectToHold.GetComponent<Collider>();
                if (component != null) component.enabled = false;

                // Parent the object to the player
                _objectToHold.transform.SetParent(transform);

                // Set the position relative to the player
                _objectToHold.transform.localPosition = new Vector3(sideOffset, upwardOffset, forwardOffset);
                _objectToHold.transform.localRotation = Quaternion.identity;

                // Optionally, disable gravity and make the rigidbody kinematic
                Rigidbody rb = _objectToHold.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.useGravity = false;
                    rb.isKinematic = true;
                }
        
                // Clear the current pickup object so we don't pick it up again
                _currentPickupObject = null;
            }
        }

        private void ThrowObject()
        {
            if (_objectToHold != null)
            {
                // Check if the object is a molotov and enable its collision logic
                MolotovFire molotovScript = _objectToHold.GetComponent<MolotovFire>();
                if (molotovScript != null)
                {
                    molotovScript.EnableCollisionLogic();
                    Debug.Log("can blow up");
                }
                // Re-enable the collider
                Collider component = _objectToHold.GetComponent<Collider>();
                if (component != null) component.enabled = true;

                // Remove the parent
                _objectToHold.transform.SetParent(null);

                // Apply force to throw the object
                Rigidbody rb = _objectToHold.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.useGravity = true;
                    rb.isKinematic = false;
                    rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
                }

                // Clear the holding variable
                _objectToHold = null;
            }
        }
    }
}