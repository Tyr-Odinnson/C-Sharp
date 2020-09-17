using UnityEngine;

public class PlayerVirtualJoystick : Player
{
    protected override void GetInput()
    {

    }

    public void SetDirection(Vector2 _direction)
    {
        direction.x = _direction.x * turnSpeed;
        direction.y = _direction.y * speed;
        
    }
}
