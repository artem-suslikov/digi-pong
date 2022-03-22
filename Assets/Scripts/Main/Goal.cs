using UnityEngine;

public class Goal : MonoBehaviour
{
    public Side side;

    public static event SideAction GoalEvent;

    private void OnCollisionEnter(Collision collision)
    {
        GoalEvent?.Invoke(side);
    }
}
