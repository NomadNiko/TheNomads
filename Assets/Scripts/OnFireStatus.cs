using System;
using UnityEngine;

public class OnFireStatus : MonoBehaviour
{
    public GameObject fireEffectPrefab;
    private GameObject _instantiatedFireEffect;
    private float _remainingDuration;
    private float _damagePerSecond;

    public void RefreshBurning(float damage, float duration)
    {
        _damagePerSecond = damage;
        _remainingDuration = duration;
    
        if (_instantiatedFireEffect == null && fireEffectPrefab != null) // Null check for the prefab
        {
            // Instantiate the fire effect on the character
            _instantiatedFireEffect = Instantiate(fireEffectPrefab, transform.position, Quaternion.identity, transform);
            _instantiatedFireEffect.transform.localPosition = Vector3.zero; // Adjust if needed
        }
        if (!IsInvoking("ApplyBurnDamage"))
        {
            InvokeRepeating("ApplyBurnDamage", 0f, 1f);
        }
    }

    private void Awake()
    {
        fireEffectPrefab = Resources.Load<GameObject>("FireStatus");
    }

    private void Update()
    {
        if (_remainingDuration > 0)
        {
            _remainingDuration -= Time.deltaTime;
            Debug.Log(gameObject.name + " is Burning!");
        }
        else
        {
            CancelInvoke("ApplyBurnDamage");
            if (_instantiatedFireEffect != null)
            {
                // Optionally stop the particle system before destroying it
                var particleSystem = _instantiatedFireEffect.GetComponent<ParticleSystem>();
                if (particleSystem != null)
                {
                    particleSystem.Stop();
                }

                Destroy(_instantiatedFireEffect, particleSystem.main.duration);
            }
            Destroy(this); // Remove the component once burning is done
        }
    }

    private void ApplyBurnDamage()
    {
        Health health = GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(_damagePerSecond);
        }
    }
}