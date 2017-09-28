using FreneticGameCore;
using FreneticGameGraphics.ClientSystem;
using FreneticGameGraphics.ClientSystem.EntitySystem;
using OpenTK;
using OpenTK.Input;
using FreneticGameCore.EntitySystem.PhysicsHelpers;
using TestRTS.GameEntities;

namespace TestRTS.MainGame
{
    /// <summary>
    /// The game's entry point.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// The primary backing game client.
        /// </summary>
        public GameClientWindow Client;

        /// <summary>
        /// Starts the game.
        /// </summary>
        public void Start()
        {
            Client = new GameClientWindow(threed: false);
            Client.Engine2D.UseLightEngine = true;
            Client.OnWindowLoad += Engine_WindowLoad;
            Client.Engine2D.Zoom = 0.1f;
            Client.Start();
        }

        /// <summary>
        /// Called by the engine when it loads up.
        /// </summary>
        public void Engine_WindowLoad()
        {
            Client.Window.KeyDown += Window_KeyDown;
            Client.Engine2D.PhysicsWorld.Gravity = new Location(0, 0, -9.81);
            // Ground
            Client.Engine2D.SpawnEntity(new EntitySimple2DRenderableBoxProperty()
            {
                BoxSize = new Vector2(200, 200),
                BoxColor = new Color4F(1f, 0.2f, 0.2f, 1f),
                BoxTexture = Client.Engine2D.Textures.White,
                CastShadows = false
            }, new ClientEntityPhysicsProperty()
            {
                Position = new Location(0, 0, 0),
                Shape = new EntityBoxShape() { Size = new Location(200, 200, 10) },
                Mass = 0
            });
            // Entity 1
            Client.Engine2D.SpawnEntity(new EntitySimple2DRenderableBoxProperty()
            {
                BoxSize = new Vector2(10, 10),
                BoxColor = new Color4F(0.2f, 1f, 0.2f, 1f),
                BoxTexture = Client.Engine2D.Textures.White
            }, new ClientEntityPhysicsProperty()
            {
                Position = new Location(75, 75, 10),
                Shape = new EntityBoxShape() { Size = new Location(10, 10, 10) },
                Mass = 10
            }, new UnitEntityProperty()
            {
                UnitSize = 10f
            });
            // Entity 2
            Client.Engine2D.SpawnEntity(new EntitySimple2DRenderableBoxProperty()
            {
                BoxSize = new Vector2(15, 15),
                BoxColor = new Color4F(0.8f, 0.4f, 0.4f, 1f),
                BoxTexture = Client.Engine2D.Textures.White
            }, new ClientEntityPhysicsProperty()
            {
                Position = new Location(20, 35, 10),
                Shape = new EntityBoxShape() { Size = new Location(15, 15, 10) },
                Mass = 20
            }, new UnitEntityProperty()
            {
                UnitSize = 15f
            });
            // Camera
            Client.Engine2D.SpawnEntity(new CameraControllerProperty());
            // Sky light
            Client.Engine2D.SpawnEntity(new EntityLight2DCasterProperty()
            {
                LightPosition = new Vector2(-20, 20),
                LightColor = Color4F.White,
                LightStrength = 128f,
                IsSkyLight = true
            });
            /* UI3DSubEngine subeng = new UI3DSubEngine(new UIPositionHelper(Client.MainUI).Anchor(UIAnchor.TOP_LEFT).ConstantXY(0, 0).ConstantWidthHeight(350, 350));
            Client.MainUI.DefaultScreen.AddChild(subeng);
            // Ground
            subeng.SubEngine.SpawnEntity(new EntitySimple3DRenderableModelProperty()
            {
                EntityModel = Client.Models.Cube,
                Scale = new Location(10, 10, 10),
                RenderAt = new Location(0, 0, -10),
                DiffuseTexture = Client.Textures.White
            });
            // Light
            subeng.SubEngine.SpawnEntity(new EntityPointLight3DProperty()
            {
                LightPosition = new Location(0, 0, 1),
                LightStrength = 15f
            });
            subeng.SubEngine.MainCamera.Position = new Location(0, 0, 0);
            subeng.SubEngine.MainCamera.Direction = new Location(0.1, -0.1, -1).Normalize(); */
        }

        /// <summary>
        /// Handles escape key pressing to exit.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event data.</param>
        private void Window_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Client.Window.Close();
            }
        }
    }
}
