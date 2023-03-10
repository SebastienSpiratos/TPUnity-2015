// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.NavMeshAgent)]
	[Tooltip("Gets the flag a NavMesh Agent if it should update the transform position. \n" +
		"NOTE: The Game Object must have a NavMeshAgent component attached.")]
	public class GetAgentUpdatePosition : FsmStateAction
	{
		
		[RequiredField]
		[Tooltip("The Game Object to work with. NOTE: The Game Object must have a NavMeshAgent component attached.")]
		[CheckForComponent(typeof(NavMeshAgent))]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("Store flag of the agent if it should update the position.")]
		[UIHint(UIHint.Variable)]
		public FsmBool storeResult;

		
		private NavMeshAgent _agent;

		private void _getAgent()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) 
			{
				return;
			}
			
			_agent =  go.GetComponent<NavMeshAgent>();
		}
		
		public override void Reset()
		{
			gameObject = null;
			storeResult = null;
		}

		void DoGetUpdatePosition()
		{
			if (storeResult == null || _agent == null) 
			{
				return;
			}

			storeResult.Value = _agent.updatePosition;
		}

	}
}