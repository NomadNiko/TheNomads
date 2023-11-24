using System;
using System.Collections;
using System.Collections.Generic;
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
        
        private bool hasEnteredTrigger = false;
        
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
                if (other.gameObject.CompareTag("PickupAble") && !hasEnteredTrigger)
                {
                    _currentPickupObject = other.gameObject;
                    PickUp();
                    hasEnteredTrigger = true;
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
            if (_currentPickupObject != null && _objectToHold == null) // Check if _objectToHold is not already assigned
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
                    rb.isKinematic = false; // Makes the object not be affected by physics
                    rb.useGravity = false; // Disables gravity
                }

                // Deactivate the original pickup object (optional)
                _currentPickupObject.SetActive(false);
            }
        }
        
        
        private float _throwTimer = 3.0f; // Set the timer duration (3 seconds in this case)
        private bool _isThrowing = false;

        private void Start()
        {
            // Start the timer when the object is picked up
            _isThrowing = true;
        }

        private void Update()
        {
            if (_isThrowing)
            {
                _throwTimer -= Time.deltaTime;
                if (_throwTimer <= 0)
                {
                    ThrowObject();
                    _isThrowing = false; // Prevent further throwing
                }
            }
        }


        
        private void ThrowObject()
        {
            if (_objectToHold != null)
            {
                // Detach the object from the player
                _objectToHold.transform.parent = null;

                // Get the Rigidbody component of the object
                if (_objectToHold.TryGetComponent(out Rigidbody rb))
                {
                    // Apply a forward force to simulate throwing (adjust the force value as needed)
                    float throwForce = 10.0f; // Adjust this value as needed
                    rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
                }

                // Set objectToHold to null since it's no longer held
                _objectToHold = null;
            }
        }

        

    }
}