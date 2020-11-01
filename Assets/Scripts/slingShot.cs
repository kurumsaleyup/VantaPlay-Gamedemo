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


    private Vector3 playerVelocity, basePosition;
    private float gravityValue = -9.81f;
    private bool groundedPlayer, jump, fly;
    private float deltaY, deltaX, slingPower; //for force calculations.
    private CharacterController controller;



    private void Start()
    {
        mcScript = GetComponent<moveCubes>();
        ik = GetComponent<FullBodyBipedIK>();
        initialChestPosition = mcScript.GetChestPointerPosition();
        controller = GetComponent<CharacterController>();
        basePosition = new Vector3(0.18f, 1.7f, 0f);//somehow it takes 0,0,0 therefore presetted like that.
        gameObject.transform.position = basePosition;

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
                //setIKPositions();
                fly = true;

                if (groundedPlayer && fly && jump)
                {
                    gravityValue = -9.81f;
                    slingPower = Mathf.Abs(deltaX) + Mathf.Abs(deltaY);
                    playerVelocity.y += Mathf.Sqrt(slingPower * -2f * gravityValue);
                    jump = false;//able to jump
                    fly = false;//able to fly
                    groundedPlayer = false;//is ground
                    freeIKPositions();
                }

            }
        }
        if (playerVelocity.y <= 0 && 0.3f > Vector3.Distance(gameObject.transform.position, basePosition))
        {
            //Debug.Log("0'lamaya girdim");
            groundedPlayer = true;
            playerVelocity.y = 0f;
            gravityValue = 0f;
            setIKPositions();
        }


        playerVelocity.y += gravityValue * Time.deltaTime;//movement
        controller.Move(playerVelocity * Time.deltaTime);//movement

    }//end update

    void moveModelDown()
    {
        if (deltaX > MINGAP_X)//if gap between cubes are enough
        {
            if (slingJoystick.Direction.y < 0)
            {
                if (deltaY < MAX_Y)//limitting movement
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

        if (slingJoystick.Direction.y < 0)//bending the model on y axis.
        {
            chestPointer.position = chestPointer.position;
        }
        else //resetting the bend on y axis.
        {
            chestPointer.position = mcScript.GetResetPointerPosition();
        }
    }
}//end class
