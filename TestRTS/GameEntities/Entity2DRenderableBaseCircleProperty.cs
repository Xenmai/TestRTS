using FreneticGameGraphics.ClientSystem;
using FreneticGameGraphics.ClientSystem.EntitySystem;
using FreneticGameGraphics.GraphicsHelpers;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRTS.GameEntities
{
    /// <summary>
    /// Renders a simple 2D circle effect.
    /// </summary>
    public class Entity2DRenderableBaseCircleProperty : Entity2DRenderableProperty
    {
        /// <summary>
        /// The circle radius.
        /// </summary>
        public float Radius;

        /// <summary>
        /// The texture for this rendered circle.
        /// </summary>
        public Texture CircleTexture;

        /// <summary>
        /// What color to render the box as.
        /// </summary>
        public Vector4 BoxColor = Vector4.One;

        /// <summary>
        /// Render the entity as seen normally, in 2D.
        /// </summary>
        /// <param name="context">The render context.</param>
        public override void RenderStandard2D(RenderContext2D context)
        {
            if (context.CalcShadows && context.Engine.OneDLights)
            {
                context.Engine.Textures.White.Bind();
            }
            else
            {
                CircleTexture.Bind();
            }
            context.Engine.RenderHelper.SetColor(BoxColor);
            //context.Engine.RenderHelper.RenderRectangle(context, (float)RenderAt.X + BoxUpLeft.X, (float)RenderAt.Y + BoxUpLeft.Y,
            //    (float)RenderAt.X + BoxDownRight.X, (float)RenderAt.Y + BoxDownRight.Y, new Vector3(BoxUpLeft.X / sz.X, BoxDownRight.Y / sz.Y, RenderAngle));
        }
    }
}
