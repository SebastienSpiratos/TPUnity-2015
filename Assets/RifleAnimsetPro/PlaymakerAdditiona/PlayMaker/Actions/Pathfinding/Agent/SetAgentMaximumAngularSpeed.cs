// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.NavMeshAgent)]
	[Tooltip("Set the maximum rotation speed in (deg/s) of a NavMesh Agent. \n" +
		"NOTE: The Game Object must have a NavMeshAgent component attached.")]
	public class SetAgentMaximumAngularSpeed : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Game Object to work with. NOTE: The Game Object must have a NavMeshAgent component attached.")]
		[CheckForComponent(typeof(NavMeshAgent))]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("The maximum rotation speed in (deg/s) of the navMesh Agent.")]
		public FsmFloat maximumAngularSpeed;

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
			maximumAngularSpeed = null;

		}

		public override void OnEnter()
		{
			_getAgent();
			
			DoSetMaximumAngularSpeed();

			Finish();		
		}
		
		void DoSetMaximumAngularSpeed()
		{
			if (maximumAngularSpeed == null || _agent == null) 
			{
				return;
			}
			
			_agent.angularSpeed = maximumAngularSpeed.Value;
		}

	}
}