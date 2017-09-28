﻿using FreneticGameCore;
using FreneticGameCore.Collision;
using FreneticGameGraphics.ClientSystem.EntitySystem;
using OpenTK.Input;
using System.Collections.Generic;
using System.Linq;
using TestRTS.GameEntities.GameInterfaces;

namespace TestRTS.GameEntities
{
    /// <summary>
    /// Represents a an entity that rotates.
    /// </summary>
    public class CameraControllerProperty : ClientEntityProperty
    {
        /// <summary>
        /// Fired when entity is spawned.
        /// </summary>
        public override void OnSpawn()
        {
            Engine.Window.MouseDown += Window_MouseDown;
            Engine.Window.MouseUp+= Window_MouseUp;
            Engine.Window.KeyDown += Window_KeyDown;
            Engine.Window.KeyUp += Window_KeyUp;
            Entity.OnTick += Tick;
        }

        /// <summary>
        /// Fired when entity is despawned.
        /// </summary>
        public override void OnDespawn()
        {
            Engine.Window.MouseDown -= Window_MouseDown;
            Engine.Window.MouseUp -= Window_MouseUp;
            Engine.Window.KeyDown -= Window_KeyDown;
            Engine.Window.KeyUp -= Window_KeyUp;
            Entity.OnTick -= Tick;
        }

        /// <summary>
        /// Ticks the entity.
        /// </summary>
        public void Tick()
        {
            if (KeySpace)
            {
                
            }
            if (KeyZoomIn)
            {
                Engine2D.Zoom -= 0.001f;
            }
            if (KeyZoomOut)
            {
                Engine2D.Zoom += 0.001f;
            }
            OpenTK.Vector2 motion = OpenTK.Vector2.Zero;
            if (Engine2D.Window.Mouse.X < 50)
            {
                motion.X = Engine2D.Window.Mouse.X / 50f - 1;
            }
            if (Engine2D.Window.Mouse.X > Engine2D.Window.Width - 50)
            {
                motion.X = 1 - (Engine2D.Window.Width - Engine2D.Window.Mouse.X) / 50f;
            }
            if (Engine2D.Window.Mouse.Y < 50)
            {
                motion.Y = 1- Engine2D.Window.Mouse.Y / 50f;
            }
            if (Engine2D.Window.Mouse.Y > Engine2D.Window.Height - 50)
            {
                motion.Y = (Engine2D.Window.Height - Engine2D.Window.Mouse.Y) / 50f - 1;
            }
            Engine2D.ViewCenter += motion;
        }

        /// <summary>
        /// Is the  key down.
        /// </summary>
        public bool KeySpace;

        /// <summary>
        /// Is the right key down.
        /// </summary>
        public bool KeyZoomIn;

        /// <summary>
        /// Is the forward key down.
        /// </summary>
        public bool KeyZoomOut;
        
        /// <summary>
        /// Tracks key presses.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event data.</param>
        private void Window_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Space:
                    if (Selected != null)
                    {
                        if (Target != null)
                        {
                            int i = Selected.IndexOf(Target);
                            if (i + 1 == Selected.Count<ClientEntity>())
                            {
                                Target = Selected.First<ClientEntity>();
                            }
                            else
                            {
                                Target = Selected.ElementAt<ClientEntity>(i + 1);
                            }
                        }
                        else
                        {
                            Target = Selected.First<ClientEntity>();
                        }
                        Engine2D.ViewCenter = new OpenTK.Vector2((float)Target.LastKnownPosition.X, (float)Target.LastKnownPosition.Y);
                    }
                    break;
                case Key.Plus:
                    KeyZoomIn = true;
                    break;
                case Key.Minus:
                    KeyZoomOut = true;
                    break;
            }
        }
        
        /// <summary>
        /// Tracks key releases.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event data.</param>
        private void Window_KeyUp(object sender, KeyboardKeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Plus:
                    KeyZoomIn = false;
                    break;
                case Key.Minus:
                    KeyZoomOut = false;
                    break;
            }
        }

        /// <summary>
        /// Which entity is selected.
        /// </summary>
        public List<ClientEntity> Selected = null;

        /// <summary>
        /// Which entity is selected.
        /// </summary>
        public ClientEntity Target = null;

        /// <summary>
        /// First selection point.
        /// </summary>
        public Location First;

        /// <summary>
        /// Tracks mouse presses.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event data.</param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.Left)
            {
                if (Selected != null)
                {
                    foreach (ClientEntity ent in Selected)
                    {
                        ent?.SignalAllInterfacedProperties<ISelectable>((p) => p.Deselect());
                    }
                    Selected = null;
                }
                First = new Location(Engine2D.MouseCoords.X, Engine2D.MouseCoords.Y, -5f);
            }
        }

        /// <summary>
        /// Tracks mouse releases.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event data.</param>
        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.Left)
            {
                Target = null;
                AABB box = new AABB()
                {
                    Min = First,
                    Max = First
                };
                box.Include(new Location(Engine2D.MouseCoords.X, Engine2D.MouseCoords.Y, 5f));
                Selected = Engine2D.PhysicsWorld.GetEntitiesInBox(box).Where((ent) => ent.GetAllInterfacedProperties<ISelectable>().Count() > 0).ToList<ClientEntity>();
                foreach (ClientEntity ent in Selected)
                {
                    ent?.SignalAllInterfacedProperties<ISelectable>((p) => p.Select());
                }
            }
        }
    }
}
