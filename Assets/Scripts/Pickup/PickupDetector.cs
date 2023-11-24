using UnityEngine;

namespace Pickup
{

    public class PickupDetector : MonoBehaviour
    {
        // This will store the currently detected pickup-able object
        private GameObject _currentPickupObject;

        private GameObject _objectToHold;
        
        public float forwardOffset = 2.0f;
        public float upwardOffset = 1.0f;
        public float sideOffset = 0.5f;
        
            //DEBUG STATEMENTS
            /*(private void Update()
        {
            if (_objectToHold != null)
            {
                // Calculate the local position of _objectToHold relative to the player
                Vector3 localPosition = this.transform.InverseTransformPoint(_objectToHold.transform.position);
                Debug.Log($"Local position of _objectToHold: {localPosition}");
            }
    
            // Debug the calculated newPosition (if applicable)
            Debug.Log($"New Position Calculated: {newPosition}");
        }*/


        private void OnTriggerEnter(Collider other)
        {
            // Check if the collided object has the "Pickup-able" tag
            if (other.gameObject.CompareTag("GameController"))
            {
                _currentPickupObject = other.gameObject;
                Debug.Log("detected pickup-able object");
                PickUp();
                //add more logic here if needed, like highlighting the object
            }
        }

        private void OnTriggerExit(Collider other)
        {
            // Check if the leaving object is the currently detected pickup-able object
            if (other.gameObject == _currentPickupObject)
            {
                _currentPickupObject = null;
                // logic when the object is no longer in range can go here
            }
        }
        
        private void PickUp()
        {
            if (_currentPickupObject != null)
            {
                // Instantiate the pickup object
                _objectToHold = Instantiate(_currentPickupObject, Vector3.zero, Quaternion.identity);
                _objectToHold.transform.SetParent(this.transform);
                
                Vector3 localPosition = this.transform.InverseTransformPoint(this.transform.position 
                                                                             + this.transform.forward * forwardOffset 
                                                                             + this.transform.up * upwardOffset
                                                                             + this.transform.right * sideOffset);

                _objectToHold.transform.localPosition = localPosition;

                // Disable physics on the pickup-able object
                if (_objectToHold.TryGetComponent(out Rigidbody rb))
                {
                    rb.isKinematic = true; // Makes the object not be affected by physics
                    rb.useGravity = false; // Disables gravity
                }

                // Additional logic as needed...

                // Debug the local position of the pickup object
                Debug.Log($"Local position of pickup object after pickup: {_objectToHold.transform.localPosition}");

                // Deactivate the original pickup object (optional)
                _currentPickupObject.SetActive(false);
            }
        }
    }
}