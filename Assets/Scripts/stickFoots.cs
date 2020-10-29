using UnityEngine;
using RootMotion.FinalIK;

public class stickFoots : MonoBehaviour
{
    public Transform leftFootPosition, rightFootPosition;
    public Transform chestPosition;
    public FullBodyBipedIK ik;

    private void LateUpdate()
    {
        leftFootPos();
        rightFootPos();
        chestPositionSet();
    }//end update

    void leftFootPos()
    {
        //Left foot position update
        ik.solver.leftFootEffector.position = leftFootPosition.position;
        ik.solver.leftFootEffector.positionWeight = 1f;
        ik.solver.leftFootEffector.rotationWeight = 1f;
    }
    void rightFootPos()
    {
        //Right foot position update
        ik.solver.rightFootEffector.position = rightFootPosition.position;
        ik.solver.rightFootEffector.positionWeight = 1f;
        ik.solver.rightFootEffector.rotationWeight = 1f;
    }
    void chestPositionSet()
    {
        //chest position update
        ik.solver.bodyEffector.position = chestPosition.position;
        ik.solver.bodyEffector.positionWeight = 1f;
        ik.solver.bodyEffector.rotationWeight = 1f;
    }
}//end class
