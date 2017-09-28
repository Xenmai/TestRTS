using FreneticGameCore;
using FreneticGameGraphics.ClientSystem.EntitySystem;
using TestRTS.GameEntities.GameInterfaces;

namespace TestRTS.GameEntities
{
    class UnitEntityProperty : ClientEntityProperty, ISelectable
    {
        /// <summary>
        /// The spawned effect entity.
        /// </summary>
        public ClientEntity Effect;

        /// <summary>
        /// The unit radius.
        /// </summary>
        public float UnitSize;

        /// <summary>
        /// Highlights the entity.
        /// </summary>
        public void Select()
        {
            Effect = Engine2D.SpawnEntity(new Entity2DRenderableBaseCircleProperty()
            {
                Radius = UnitSize * 0.7f,
                CircleTexture = Entity.Engine.Textures.White,
                RenderAt = Entity.LastKnownPosition - new Location(0, 0, 5),
                CastShadows = false
            });
            Entity.OnPositionChanged += PositionChanged;
        }

        /// <summary>
        /// Stops highlighting the entity.
        /// </summary>
        public void Deselect()
        {
            Entity.OnPositionChanged -= PositionChanged;
            Engine2D.DespawnEntity(Effect);
            Effect = null;
        }

        public void PositionChanged(Location loc)
        {
            Effect.SetPosition(loc - new Location(0, 0, 5));
        }
    }
}
