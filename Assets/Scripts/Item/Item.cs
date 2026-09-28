using UnityEngine;

public class Item : MonoBehaviour
{
    protected Rigidbody rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public virtual void OnHit(Vector3 direction, float force)
    {
        Debug.Log(gameObject.name + " was hit with force: " + force);
        rb.linearVelocity = direction * force;
    }
}
