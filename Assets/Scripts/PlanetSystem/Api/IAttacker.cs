namespace PlanetSystem.Api
{
    public interface IPlanetAttacker
    {
        public void Setup(IInputAttacker inputAttacker, IPlanetController planetController);
    }
}