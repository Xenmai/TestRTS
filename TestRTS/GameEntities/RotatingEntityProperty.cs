using FreneticGameCore;
using FreneticGameGraphics.ClientSystem.EntitySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRTS.GameEntities
{
    /// <summary>
    /// Represents a an entity that rotates.
    /// </summary>
    public class RotatingEntityProperty : ClientEntityProperty
    {
        public override void OnSpawn()
        {
            Entity.OnTick += Tick;
        }

        public override void OnDespawn()
        {
            Entity.OnTick -= Tick;
        }

        public double angle = 0;

        public void Tick()
        {
            angle += Entity.Engine.Delta;
            if (angle > Math.PI)
            {
                angle = 0;
            }
            Entity.GetProperty<EntitySimple2DRenderableBoxProperty>().RenderAngle = (float) angle;
        }
    }
}
