
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    public void Update()
    {
        //Debug.Log(_hasSizeBuff);
    }

    private bool _hasSizeBuff;

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasSizeBuff && collision.gameObject.CompareTag("Enemy")) // Assuming your enemies have the tag "Enemy"
        {
            //Debug.Log("is colliding");
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Flatten();
            }
        }
    }

    public void SetSizeBuff(bool active)
    {
        _hasSizeBuff = active;
    }
}
