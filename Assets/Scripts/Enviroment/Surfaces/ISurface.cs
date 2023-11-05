public interface ISurface
{
    void OnEntityEnter(IEntity entity);
    void OnEntityStay(IEntity entity);
    void OnEntityExit(IEntity entity);
    void SnapToGround();
    void SetLifetime(float time); 
}