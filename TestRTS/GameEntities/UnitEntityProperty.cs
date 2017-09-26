using FreneticGameGraphics.ClientSystem.EntitySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRTS.GameEntities.GameInterfaces;

namespace TestRTS.GameEntities
{
    class UnitEntityProperty : ClientEntityProperty, ISelectable
    {
        public override void OnSpawn()
        {
            Entity.OnTick += Tick;
        }

        public override void OnDespawn()
        {
            Entity.OnTick -= Tick;
        }

        public bool selected = false;

        public float Radius = 10f;

        public void Tick()
        {
            if (selected)
            {
            }
            else
            {
            }
        }

        /// <summary>
        /// Highlights the entity.
        /// </summary>
        public void Select()
        {
            selected = true;
        }
    }
}
