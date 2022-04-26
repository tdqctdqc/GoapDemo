using Godot;
using System;

public class CameraController : Camera2D
{
    private float _speed = 1000f; 
    public void Input(InputEvent evnt)
    {
        float delta = GetProcessDeltaTime();
        if (evnt is InputEventKey k)
        {
            if (k.Scancode == (int) KeyList.Left)
            {
                Position = Position + Vector2.Left * _speed * delta;
            }
            if (k.Scancode == (int) KeyList.Right)
            {
                Position = Position + Vector2.Right * _speed * delta;
            }
            if (k.Scancode == (int) KeyList.Up)
            {
                Position = Position + Vector2.Up * _speed * delta;
            }
            if (k.Scancode == (int) KeyList.Down)
            {
                Position = Position + Vector2.Down * _speed * delta;
            }
        }

        if (evnt is InputEventMouseButton m)
        {
            
        }
    }
}
