namespace PlanetSystem.Api
{
    public interface IHealthable
    {
        public float Hp { get; }
        
        public void TakeDamage(float damage);
    }
}