using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIBehaviour/Bomber")]
public class BomberBrain :   AIBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float offset = 90f;
    [SerializeField] private float damage;

    public override void ExecuteBehaviour(EnemyBehaviourManager behaviourManager)
    {
        var currentPos = behaviourManager.gameObject.transform.position;
        var targetPos = behaviourManager.player.transform.position;

        if (Vector2.Distance(currentPos, targetPos) > 0.5f)
        {
            currentPos = Vector2.MoveTowards(currentPos, targetPos, speed * Time.deltaTime);
        }

        RotateTowardsTarget(behaviourManager, speed, offset);

        behaviourManager.gameObject.transform.position = currentPos;
    }

    public void RotateTowardsTarget(EnemyBehaviourManager behaviourManager, float speed, float offset)
    {
        Vector2 direction = behaviourManager.player.transform.position - behaviourManager.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - offset;

        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

        behaviourManager.transform.rotation = Quaternion.Lerp(behaviourManager.transform.rotation, q, speed * Time.deltaTime);
    }
}
