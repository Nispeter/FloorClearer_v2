using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLongRangedController : EnemyController
{
    public float force = 60f;
      public override void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        float heightOffset = 0.0f;
        Vector3 adjustedPosition = new Vector3(player.position.x, player.position.y + heightOffset, player.position.z);
        transform.LookAt(adjustedPosition);

        if (!alreadyAttacked)
        {
            ///Attack code here
            Rigidbody rb = Instantiate(attack, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * force, ForceMode.Impulse);
            rb.AddForce(transform.up, ForceMode.Impulse);
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

            //Action Animation
            animator.SetTrigger("Cast Spell");
        }
    }
}
