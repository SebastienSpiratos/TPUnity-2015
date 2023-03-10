// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.NavMeshAgent)]
	[Tooltip("Set the radius a NavMesh Agent. \nNOTE: The Game Object must have a NavMeshAgent component attached.")]
	public class SetAgentRadius : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Game Object to work with. NOTE: The Game Object must have a NavMeshAgent component attached.")]
		[CheckForComponent(typeof(NavMeshAgent))]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("The radius of the navMesh Agent.")]
		public FsmFloat radius;

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
			radius = null;

		}

		public override void OnEnter()
		{
			_getAgent();
			
			DoSetRadius();

			Finish();		
		}
		
		void DoSetRadius()
		{
			if (radius == null || _agent == null) 
			{
				return;
			}
			
			_agent.radius = radius.Value;
		}

	}
}