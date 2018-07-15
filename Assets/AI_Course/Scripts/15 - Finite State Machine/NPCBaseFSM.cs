using UnityEngine;

public class NPCBaseFSM : StateMachineBehaviour 
{
	public GameObject NPC;
	public GameObject enemy;
	public UnityEngine.AI.NavMeshAgent agent;
	public float speed = 2.0f;
	public float rotSpeed = 1.0f;
	public float accuracy = 3.0f;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		NPC = animator.gameObject;
		enemy = NPC.GetComponent<FSMTankAI>().player;
		agent = NPC.GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
}
