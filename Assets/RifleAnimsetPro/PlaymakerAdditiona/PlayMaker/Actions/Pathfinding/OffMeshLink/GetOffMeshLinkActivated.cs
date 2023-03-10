// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.NavMesh)]
	[Tooltip("Gets the active state of an OffMeshLink. \nNOTE: The Game Object must have an OffMeshLink component attached.")]
	public class GetOffMeshLinkActivated : FsmStateAction
	{
		
		[RequiredField]
		[Tooltip("The Game Object to work with. NOTE: The Game Object must have an OffMeshLink component attached.")]
		[CheckForComponent(typeof(OffMeshLink))]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("Store flag of the offLinkMesh active state")]
		[UIHint(UIHint.Variable)]
		public FsmBool storeResult;
		
		public bool everyFrame;
		
		private OffMeshLink _offMeshLink;

		private void _getOffMeshLink()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) 
			{
				return;
			}
			
			_offMeshLink =  go.GetComponent<OffMeshLink>();
		}
		
		public override void Reset()
		{
			gameObject = null;
			storeResult = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			_getOffMeshLink();

			DoGetActivated();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetActivated();
		}

		void DoGetActivated()
		{
			if (storeResult == null || _offMeshLink == null) 
			{
				return;
			}

			storeResult.Value = _offMeshLink.activated;
		}

	}
}