using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIBehaviour/Fighter")]
public class FighterBrain : AIBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float timeBetweenShoots;
    [SerializeField] private float timeToNextShot;
    private bool canShoot;

    public override void ExecuteBehaviour(EnemyBehaviourManager behaviourManager)
    {
        Vector3 temp = behaviourManager.transform.position;
        temp.x += -speed * Time.deltaTime;
        behaviourManager.transform.position = temp;

        Shoot(behaviourManager, timeBetweenShoots, timeToNextShot);
    }

    private void Shoot(EnemyBehaviourManager behaviourManager, float timeBetweenShots, float timeToNextShot)
    {
        GameObject obj = ObjectPooling.instance.GetPooledObjects("EnemyBullet");

        if (obj == null)
            return;

        if (timeBetweenShoots < timeToNextShot)
            timeBetweenShoots += Time.deltaTime;
        else if (timeBetweenShoots >= timeToNextShot)
            canShoot = true;

        if (canShoot)
        {
            canShoot = false;
            timeBetweenShoots = 0.0f;

            obj.transform.position = behaviourManager.projectileSpawn.transform.position;
            obj.transform.rotation = behaviourManager.projectileSpawn.transform.rotation;
            obj.SetActive(true);
        }
    }
}
