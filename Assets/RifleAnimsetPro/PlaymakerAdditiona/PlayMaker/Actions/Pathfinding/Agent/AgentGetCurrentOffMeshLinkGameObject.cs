// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.NavMeshAgent)]
	[Tooltip("Gets the reference to the game object of current OffMeshLink of a NavMeshAgent. \n" +
		"NOTE: The Game Object must have a NavMeshAgentcomponent attached.")]
	public class AgentGetCurrentOffMeshLinkGameObject : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Game Object to work with. NOTE: The Game Object must have a NavMeshAgent component attached.")]
		[CheckForComponent(typeof(NavMeshAgent))]
		public FsmOwnerDefault gameObject;
	
		[RequiredField]	
		[Tooltip("The GameObject with the current OffMeshLink component attached")]
		[UIHint(UIHint.Variable)]
		public FsmGameObject storeOffMeshLinkGameObject;
		
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
			storeOffMeshLinkGameObject = new FsmGameObject { UseVariable = true};
		}

		public override void OnEnter()
		{
			_getAgent();
			
			DoGetCurrentOffMeshLinkGameObject();

			Finish();		
		}

		void DoGetCurrentOffMeshLinkGameObject()
		{
			if (_agent == null)
			{
				return;
			}
			
			if (!_agent.isOnOffMeshLink)
			{
				return;
			}
			
			OffMeshLink comp = _agent.currentOffMeshLinkData.offMeshLink;// .GetCurrentOffMeshLinkGameObject();
			if (comp!=null){
				storeOffMeshLinkGameObject.Value = comp.gameObject;
			}
		
		}

	}
}