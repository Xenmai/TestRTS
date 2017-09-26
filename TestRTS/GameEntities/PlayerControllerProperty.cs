using BEPUphysics.Character;
using BEPUutilities;
using FreneticGameCore;
using FreneticGameCore.EntitySystem;
using FreneticGameGraphics.ClientSystem.EntitySystem;
using OpenTK.Input;

namespace TestRTS.GameEntities
{
    /// <summary>
    /// Represents a an entity that rotates.
    /// </summary>
    public class PlayerControllerProperty : ClientEntityProperty
    {
        /// <summary>
        /// Fired when entity is spawned.
        /// </summary>
        public override void OnSpawn()
        {
            Engine.Window.KeyDown += Window_KeyDown;
            Engine.Window.KeyUp += Window_KeyUp;
            Entity.OnTick += Tick;
            Entity.OnSpawnEvent.AddEvent(OnSpawnSecond, this, 0);
        }

        /// <summary>
        /// The physics character controller.
        /// </summary>
        public CharacterController PhysChar;

        /// <summary>
        /// Fired on the secondary spawn event.
        /// </summary>
        /// <param name="e">Event data.</param>
        public void OnSpawnSecond(FreneticEventArgs<EntitySpawnEventArgs> e)
        {
            PhysChar = Entity.GetProperty<ClientEntityPhysicsProperty>().OriginalObject as CharacterController;
        }

        /// <summary>
        /// Fired when entity is despawned.
        /// </summary>
        public override void OnDespawn()
        {
            Engine.Window.KeyDown -= Window_KeyDown;
            Engine.Window.KeyUp -= Window_KeyUp;
            Entity.OnTick -= Tick;
            Entity.OnSpawnEvent.RemoveBySource(this);
        }

        /// <summary>
        /// Ticks the entity.
        /// </summary>
        public void Tick()
        {
            Vector2 motion = Vector2.Zero;
            if (KeyUp)
            {
                motion.Y += 1;
            }
            if (KeyDown)
            {
                motion.Y -= 1;
            }
            if (KeyRight)
            {
                motion.X += 1;
            }
            if (KeyLeft)
            {
                motion.X -= 1;
            }
            if (motion.LengthSquared() > 0)
            {
                motion.Normalize();
            }
            PhysChar.ViewDirection = new Vector3(0, 1, 0);
            PhysChar.HorizontalMotionConstraint.MovementDirection = motion;
        }

        /// <summary>
        /// Is the left key down.
        /// </summary>
        public bool KeyLeft;

        /// <summary>
        /// Is the right key down.
        /// </summary>
        public bool KeyRight;

        /// <summary>
        /// Is the forward key down.
        /// </summary>
        public bool KeyUp;

        /// <summary>
        /// Is the back key down.
        /// </summary>
        public bool KeyDown;

        /// <summary>
        /// Tracks key releases.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event data.</param>
        private void Window_KeyUp(object sender, KeyboardKeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    KeyUp = false;
                    break;
                case Key.A:
                    KeyLeft = false;
                    break;
                case Key.S:
                    KeyDown = false;
                    break;
                case Key.D:
                    KeyRight = false;
                    break;
            }
        }

        /// <summary>
        /// Tracks key presses.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event data.</param>
        private void Window_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    KeyUp = true;
                    break;
                case Key.A:
                    KeyLeft = true;
                    break;
                case Key.S:
                    KeyDown = true;
                    break;
                case Key.D:
                    KeyRight = true;
                    break;
            }
        }
    }
}
