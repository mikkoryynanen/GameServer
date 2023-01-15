using Shared.Types;

namespace GameSever.Services.ClientPosition
{
    public interface IClientPositionService
    {
        Vector2D GetPosition(string clientId);
        void SetPosition(string clientId, Vector2D positionVector2D);
    }
}