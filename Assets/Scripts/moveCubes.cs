using UnityEngine;

public class moveCubes : MonoBehaviour
{
    public Transform rightCube, leftcube, chestPointer, resetPointer;
    public Joystick joystick;
    private float deltaX = 0.1f, moveSpeed = 0.9f;
    private const float MAXGAP = 4f, MINGAP = 1.4f;

    private void Start()
    {
        resetPointer.position = chestPointer.position;
    }

    void Update()
    {

        if (joystick.Direction != Vector2.zero)
        {
            if (deltaX <= MAXGAP && joystick.Direction.x > 0)
            {
                PlusCubeMovement();
            }
            else if (deltaX >= MINGAP && joystick.Direction.x < 0)
            {
                MinusCubeMovement();
            }

            deltaX = Vector3.Distance(rightCube.position, leftcube.position);
            //Debug.Log(deltaX);
        }

    }//end update

    void PlusCubeMovement()
    {
        rightCube.position += Vector3.right * Time.deltaTime * moveSpeed;
        leftcube.position += Vector3.left * Time.deltaTime * moveSpeed;
        chestPointer.position += Vector3.down * Time.deltaTime;
        resetPointer.position += Vector3.down * Time.deltaTime;
    }
    void MinusCubeMovement()
    {
        rightCube.position -= Vector3.right * Time.deltaTime * moveSpeed;
        leftcube.position -= Vector3.left * Time.deltaTime * moveSpeed;
        chestPointer.position += Vector3.up * Time.deltaTime;
        resetPointer.position += Vector3.up * Time.deltaTime;
    }

    public float GetDeltaX()
    {
        return deltaX;
    }
    public Vector3 GetChestPointerPosition()
    {
        return chestPointer.position;
    }
    public Vector3 GetResetPointerPosition()
    {
        return resetPointer.position;
    }
}//end class
