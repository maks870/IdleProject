using UnityEngine;

public class BlindEnemy : Enemy
{
    [SerializeField] float blindRange;
    private bool isBlind = false;
    private Vector3 blindMoveDirection;

    protected override void Update()
    {
        base.Update();
        if (InBlindRange() && isBlind)
        {
            moveDirection = blindMoveDirection;
        }
    }

    private bool InBlindRange()
    {
        float rangeToPlayer = (Player.instance.transform.position - transform.position).magnitude;

        if (rangeToPlayer < blindRange)
        {
            if (!isBlind)
            {
                blindMoveDirection = moveDirection;
                isBlind = true;
            }
            return true;
        }
        else
        {
            isBlind = false;
            return false;
        }

    }
}
