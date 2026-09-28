using UnityEngine;

public class Bomb : Item
{
    [SerializeField] private float explosionForce = 10f;
    [SerializeField] private float explosionRange = 2.5f;
    [SerializeField] private float explosionDelay = 2f;

    public override void OnHit(Vector3 direction, float force)
    {
        base.OnHit(direction, force);
        Invoke(nameof(Explode), explosionDelay);
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRange);
        foreach (Collider hit in colliders)
        {
            if (hit.gameObject.CompareTag("Item"))
            {
                hit.GetComponent<Item>().
                OnHit((hit.transform.position - transform.position).normalized, explosionForce);
            }
        }
        
        Destroy(gameObject); // Destroy the bomb after exploding
    }
}
