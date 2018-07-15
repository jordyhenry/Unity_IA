using UnityEngine;

public class Attack : NPCBaseFSM 
{
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		base.OnStateEnter(animator, stateInfo, layerIndex);
		NPC.GetComponent<FSMTankAI>().StartFiring();
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		NPC.transform.LookAt(enemy.transform.position);
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		NPC.GetComponent<FSMTankAI>().StopFiring();
	}
}
