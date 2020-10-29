using UnityEngine;
using RootMotion.FinalIK;

public class slingShot : MonoBehaviour
{
    public Joystick slingJoystick; //to move in vertical.
    public Transform chestPointer;

    private FullBodyBipedIK ik; //to adjust body weight while flying.
    private moveCubes mcScript; //to reach deltaX.   
    private Vector3 initialChestPosition;
    private const float MAX_Y = 2.5f, MINGAP_X = 2f;


    private Vector3 playerVelocity;
    private float gravityValue = -9.81f;
    private bool groundedPlayer, jump, fly;
    private float deltaY, deltaX, slingPower; //for force calculations.
    private CharacterController controller;


    private void Start()
    {
        mcScript = GetComponent<moveCubes>();
        ik = GetComponent<FullBodyBipedIK>();
        initialChestPosition = mcScript.GetChestPointerPosition();
        controller = gameObject.GetComponent<CharacterController>();

    }//end start
    private void Update()
    {

        deltaX = mcScript.GetDeltaX();
        if (slingJoystick.Direction != Vector2.zero)//if joystick is moving
        {
            moveModelDown();
        }
        else
        {
            if (0.35f < deltaY && deltaX > MINGAP_X)
            {
                //Debug.Log("fly");
                freeIKPositions();
                fly = true;

            }
        }

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            setIKPositions();
        }
        if (jump && groundedPlayer && fly)
        {
            slingPower = Mathf.Abs(deltaX) + Mathf.Abs(deltaY);
            playerVelocity.y += Mathf.Sqrt(slingPower * -2f * gravityValue);
            jump = false;
            fly = false;
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

    }//end update

    void moveModelDown()
    {
        if (deltaX > MINGAP_X)//if gap between cubes are enough
        {
            if (slingJoystick.Direction.y < 0)
            {
                if (deltaY < MAX_Y)
                {
                    chestPointer.position += Vector3.down * Time.deltaTime;
                    jump = true;
                }
                deltaY = Vector3.Distance(initialChestPosition, chestPointer.position);
            }
        }
    }

    void freeIKPositions()
    {
        ik.solver.IKPositionWeight = 0f;
    }

    void setIKPositions()
    {
        ik.solver.IKPositionWeight = 1f;
        chestPointer.position = mcScript.GetResetPointerPosition();
    }
}//end class
