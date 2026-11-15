using UnityEngine;

public class Bomb : Item
{
    [SerializeField] private float explosionForce = 10f;
    [SerializeField] private float explosionRange = 2.5f;
    [SerializeField] private float explosionDelay = 2f;
    [SerializeField] private ParticleSystem explosionEffect;

    // public override void OnHit(Vector3 direction, float force)
    // {
    //     base.OnHit(direction, force);
    //     Invoke(nameof(Explode), explosionDelay);
    // }

    private void Explode()
    {
        // Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRange);
        // foreach (Collider hit in colliders)
        // {
        //     if (hit.gameObject.CompareTag("Item"))
        //     {
        //         hit.GetComponent<Item>().
        //         OnHit((hit.transform.position - transform.position).normalized, explosionForce);
        //     }
        // }
        ResetExplosionEffect();
        explosionEffect.Play(true);

        gameObject.GetComponent<MeshRenderer>().enabled = false; // Hide the bomb mesh
        gameObject.GetComponent<Collider>().enabled = false; // Disable the bomb collider

        Invoke(nameof(Disable), explosionEffect.main.duration);
    }

    private void Disable()
    {
        ResetExplosionEffect();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true; // Show the bomb mesh
        gameObject.GetComponent<Collider>().enabled = true; // Enable the bomb collider

        CancelInvoke();
        explosionEffect.gameObject.SetActive(true);
        var main = explosionEffect.main;
        main.stopAction = ParticleSystemStopAction.None;
        ResetExplosionEffect();

        Invoke(nameof(Explode), explosionDelay);
    }

    private void OnDisable()
    {
        CancelInvoke();
        ResetExplosionEffect();
    }

    private void ResetExplosionEffect()
    {
        explosionEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        explosionEffect.Clear(true);
    }
}
