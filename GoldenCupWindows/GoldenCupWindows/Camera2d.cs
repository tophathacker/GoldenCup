using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
public class Camera2d
{
    protected float _zoom; // Camera Zoom
    public Matrix _transform; // Matrix Transform
    private Vector2 _pos; // Camera Position
    protected float _rotation; // Camera Rotation
    public float ViewportWidth;
    public float ViewportHeight;

    public Camera2d()
    {
        _zoom = 1.0f;
        _rotation = 0.0f;
        _pos = Vector2.Zero;
    }

    // Sets and gets zoom
    public float Zoom
    {
        get { return _zoom; }
        set { _zoom = value; if (_zoom < 0.1f) _zoom = 0.1f; } // Negative zoom will flip image
    }

    public float Rotation
    {
        get { return _rotation; }
        set { _rotation = value; }
    }

    // Auxiliary function to move the camera
    public void Move(Vector2 amount)
    {
        _pos += amount;
    }
    // Get set position
    public Vector2 Pos
    {
        get { return _pos; }
        set { _pos = value; }
    }

    public Matrix get_transformation(GraphicsDevice graphicsDevice)
    {
        _transform =       // Thanks to o KB o for this solution
          Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
                                     Matrix.CreateRotationZ(Rotation) *
                                     Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                     Matrix.CreateTranslation(new Vector3(ViewportWidth * 0.5f, ViewportHeight * 0.5f, 0));
        return _transform;
    }


    /*
    public Vector2 get_mouse_vpos()
    {
        Vector2 mp = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

        return ((mp * this.Zoom) + this.Pos); // Camera is a member of the class in this example
    }
     */
    public Vector2 get_mouse_vpos()
    {
        float MouseWorldX = (Mouse.GetState().X - ViewportWidth * 0.5f + (this.Pos.X) * (float)Math.Pow(this.Zoom, 1)) /
        (float)Math.Pow(this.Zoom, 1);
        float MouseWorldY = ((Mouse.GetState().Y - ViewportHeight * 0.5f + (this.Pos.Y) * (float)Math.Pow(this.Zoom, 1))) /
        (float)Math.Pow(this.Zoom, 1);
        return new Vector2(MouseWorldX, MouseWorldY);
    }
}