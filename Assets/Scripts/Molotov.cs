using UnityEngine;

public class MolotovFire : MonoBehaviour
{
    public GameObject fireEffectPrefab;
    public float minVelocity = 1.0f;
    private Rigidbody _rb;
    private bool _isThrown; // Flag to check if thrown

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void EnableCollisionLogic()
    {
        _isThrown = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isThrown && _rb.velocity.magnitude > minVelocity)
        {
            Instantiate(fireEffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

