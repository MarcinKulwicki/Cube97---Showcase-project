using Cube.Data;

namespace Cube.Controllers
{
    public interface IGameController
    {
        public void ChangeUserName(string userName);
        public void SetMusic(bool enableMusic);
        public void SetEffects(bool enableEffects);
        public AbstractGameSettings GetGameSettings();
    }
}
