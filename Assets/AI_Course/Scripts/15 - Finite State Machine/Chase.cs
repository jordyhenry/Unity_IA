using UnityEngine;

public class Chase : NPCBaseFSM 
{

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		base.OnStateEnter(animator, stateInfo, layerIndex);
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		agent.SetDestination(enemy.transform.position);
		/*
		Vector3 direction = enemy.transform.position - NPC.transform.position;
		NPC.transform.rotation = Quaternion.Slerp(
			NPC.transform.rotation,
			Quaternion.LookRotation(direction),
			Time.deltaTime * rotSpeed
		);

		NPC.transform.Translate(Vector3.forward * Time.deltaTime * speed * 2f);
		*/
	}
}
