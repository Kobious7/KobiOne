using UnityEngine;

public class ParticleAttractor : MonoBehaviour
{
    public ParticleSystem ps;
    public Transform target;
    public float attractionForce = 10f;
    public float minDistanceToKill = 0.1f;
    public int particlesCount = 0;

    private ParticleSystem.Particle[] particles;

    void Start()
    {
        particles = new ParticleSystem.Particle[particlesCount];
    }

    void Update()
    {
        if (ps == null || target == null) return;

        int count = ps.GetParticles(particles);

        for (int i = 0; i < count; i++)
        {
            Vector3 toTarget = target.position - particles[i].position;
            float distance = toTarget.magnitude;

            if (distance < minDistanceToKill)
            {
                particles[i].remainingLifetime = 0f;
                continue;
            }

            Vector3 attraction = toTarget.normalized * attractionForce;
            particles[i].velocity += attraction * Time.deltaTime;

            Debug.DrawLine(particles[i].position, target.position, Color.red);
        }

        ps.SetParticles(particles, count);

        if (!ps.IsAlive())
        {
            gameObject.SetActive(false);
        }
    }
}
