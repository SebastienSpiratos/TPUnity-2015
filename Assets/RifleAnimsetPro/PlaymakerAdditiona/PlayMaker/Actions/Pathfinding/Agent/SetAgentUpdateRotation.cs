// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.NavMeshAgent)]
	[Tooltip("Set the flag for a NavMesh Agent to update the transform rotation. \n" +
		"NOTE: The Game Object must have a NavMeshAgent component attached.")]
	public class SetAgentUpdateRotation : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Game Object to work with. NOTE: The Game Object must have a NavMeshAgent component attached.")]
		[CheckForComponent(typeof(NavMeshAgent))]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("Should the Agent update the transform rotation")]
		public FsmBool updateRotation;

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
			updateRotation = null;

		}

		public override void OnEnter()
		{
			_getAgent();
			
			DoSetUpdateRotation();

			Finish();		
		}
		
		void DoSetUpdateRotation()
		{
			if (updateRotation == null || _agent == null) 
			{
				return;
			}
			
			_agent.updateRotation = updateRotation.Value;
		}

	}
}