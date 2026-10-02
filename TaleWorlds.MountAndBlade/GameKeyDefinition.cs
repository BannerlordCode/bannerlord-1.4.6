using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000229 RID: 553
	public enum GameKeyDefinition
	{
		// Token: 0x04000B87 RID: 2951
		Up,
		// Token: 0x04000B88 RID: 2952
		Down,
		// Token: 0x04000B89 RID: 2953
		Left,
		// Token: 0x04000B8A RID: 2954
		Right,
		// Token: 0x04000B8B RID: 2955
		Leave,
		// Token: 0x04000B8C RID: 2956
		ShowIndicators,
		// Token: 0x04000B8D RID: 2957
		InitiateAllChat,
		// Token: 0x04000B8E RID: 2958
		InitiateTeamChat,
		// Token: 0x04000B8F RID: 2959
		FinalizeChat,
		// Token: 0x04000B90 RID: 2960
		Attack,
		// Token: 0x04000B91 RID: 2961
		Defend,
		// Token: 0x04000B92 RID: 2962
		EquipPrimaryWeapon,
		// Token: 0x04000B93 RID: 2963
		EquipSecondaryWeapon,
		// Token: 0x04000B94 RID: 2964
		Action,
		// Token: 0x04000B95 RID: 2965
		Jump,
		// Token: 0x04000B96 RID: 2966
		Crouch,
		// Token: 0x04000B97 RID: 2967
		Kick,
		// Token: 0x04000B98 RID: 2968
		ToggleWeaponMode,
		// Token: 0x04000B99 RID: 2969
		EquipWeapon1,
		// Token: 0x04000B9A RID: 2970
		EquipWeapon2,
		// Token: 0x04000B9B RID: 2971
		EquipWeapon3,
		// Token: 0x04000B9C RID: 2972
		EquipWeapon4,
		// Token: 0x04000B9D RID: 2973
		DropWeapon,
		// Token: 0x04000B9E RID: 2974
		SheathWeapon,
		// Token: 0x04000B9F RID: 2975
		Zoom,
		// Token: 0x04000BA0 RID: 2976
		ViewCharacter,
		// Token: 0x04000BA1 RID: 2977
		LockTarget,
		// Token: 0x04000BA2 RID: 2978
		CameraToggle,
		// Token: 0x04000BA3 RID: 2979
		MissionScreenHotkeyCameraZoomIn,
		// Token: 0x04000BA4 RID: 2980
		MissionScreenHotkeyCameraZoomOut,
		// Token: 0x04000BA5 RID: 2981
		ToggleWalkMode,
		// Token: 0x04000BA6 RID: 2982
		Cheer,
		// Token: 0x04000BA7 RID: 2983
		Taunt,
		// Token: 0x04000BA8 RID: 2984
		PushToTalk,
		// Token: 0x04000BA9 RID: 2985
		EquipmentSwitch,
		// Token: 0x04000BAA RID: 2986
		ShowMouse,
		// Token: 0x04000BAB RID: 2987
		BannerWindow,
		// Token: 0x04000BAC RID: 2988
		CharacterWindow,
		// Token: 0x04000BAD RID: 2989
		InventoryWindow,
		// Token: 0x04000BAE RID: 2990
		EncyclopediaWindow,
		// Token: 0x04000BAF RID: 2991
		KingdomWindow,
		// Token: 0x04000BB0 RID: 2992
		ClanWindow,
		// Token: 0x04000BB1 RID: 2993
		QuestsWindow,
		// Token: 0x04000BB2 RID: 2994
		PartyWindow,
		// Token: 0x04000BB3 RID: 2995
		FacegenWindow,
		// Token: 0x04000BB4 RID: 2996
		ManageFleetWindow,
		// Token: 0x04000BB5 RID: 2997
		MapMoveUp,
		// Token: 0x04000BB6 RID: 2998
		MapMoveDown,
		// Token: 0x04000BB7 RID: 2999
		MapMoveLeft,
		// Token: 0x04000BB8 RID: 3000
		MapMoveRight,
		// Token: 0x04000BB9 RID: 3001
		PartyMoveUp,
		// Token: 0x04000BBA RID: 3002
		PartyMoveDown,
		// Token: 0x04000BBB RID: 3003
		PartyMoveLeft,
		// Token: 0x04000BBC RID: 3004
		PartyMoveRight,
		// Token: 0x04000BBD RID: 3005
		QuickSave,
		// Token: 0x04000BBE RID: 3006
		MapFastMove,
		// Token: 0x04000BBF RID: 3007
		MapZoomIn,
		// Token: 0x04000BC0 RID: 3008
		MapZoomOut,
		// Token: 0x04000BC1 RID: 3009
		MapRotateLeft,
		// Token: 0x04000BC2 RID: 3010
		MapRotateRight,
		// Token: 0x04000BC3 RID: 3011
		MapTimeStop,
		// Token: 0x04000BC4 RID: 3012
		MapTimeNormal,
		// Token: 0x04000BC5 RID: 3013
		MapTimeFastForward,
		// Token: 0x04000BC6 RID: 3014
		MapTimeTogglePause,
		// Token: 0x04000BC7 RID: 3015
		MapCameraFollowMode,
		// Token: 0x04000BC8 RID: 3016
		MapToggleFastForward,
		// Token: 0x04000BC9 RID: 3017
		MapTrackSettlement,
		// Token: 0x04000BCA RID: 3018
		MapGoToEncylopedia,
		// Token: 0x04000BCB RID: 3019
		ViewOrders,
		// Token: 0x04000BCC RID: 3020
		SelectOrder1,
		// Token: 0x04000BCD RID: 3021
		SelectOrder2,
		// Token: 0x04000BCE RID: 3022
		SelectOrder3,
		// Token: 0x04000BCF RID: 3023
		SelectOrder4,
		// Token: 0x04000BD0 RID: 3024
		SelectOrder5,
		// Token: 0x04000BD1 RID: 3025
		SelectOrder6,
		// Token: 0x04000BD2 RID: 3026
		SelectOrder7,
		// Token: 0x04000BD3 RID: 3027
		SelectOrder8,
		// Token: 0x04000BD4 RID: 3028
		SelectOrderReturn,
		// Token: 0x04000BD5 RID: 3029
		EveryoneHear,
		// Token: 0x04000BD6 RID: 3030
		Group0Hear,
		// Token: 0x04000BD7 RID: 3031
		Group1Hear,
		// Token: 0x04000BD8 RID: 3032
		Group2Hear,
		// Token: 0x04000BD9 RID: 3033
		Group3Hear,
		// Token: 0x04000BDA RID: 3034
		Group4Hear,
		// Token: 0x04000BDB RID: 3035
		Group5Hear,
		// Token: 0x04000BDC RID: 3036
		Group6Hear,
		// Token: 0x04000BDD RID: 3037
		Group7Hear,
		// Token: 0x04000BDE RID: 3038
		HoldOrder,
		// Token: 0x04000BDF RID: 3039
		SelectLeftFormation,
		// Token: 0x04000BE0 RID: 3040
		SelectRightFormation,
		// Token: 0x04000BE1 RID: 3041
		ApplySelection,
		// Token: 0x04000BE2 RID: 3042
		ToggleSelection,
		// Token: 0x04000BE3 RID: 3043
		HideUI,
		// Token: 0x04000BE4 RID: 3044
		CameraRollLeft,
		// Token: 0x04000BE5 RID: 3045
		CameraRollRight,
		// Token: 0x04000BE6 RID: 3046
		TakePicture,
		// Token: 0x04000BE7 RID: 3047
		TakePictureWithAdditionalPasses,
		// Token: 0x04000BE8 RID: 3048
		ToggleCameraFollowMode,
		// Token: 0x04000BE9 RID: 3049
		ToggleMouse,
		// Token: 0x04000BEA RID: 3050
		ToggleVignette,
		// Token: 0x04000BEB RID: 3051
		ToggleCharacters,
		// Token: 0x04000BEC RID: 3052
		IncreaseFocus,
		// Token: 0x04000BED RID: 3053
		DecreaseFocus,
		// Token: 0x04000BEE RID: 3054
		IncreaseFocusStart,
		// Token: 0x04000BEF RID: 3055
		DecreaseFocusStart,
		// Token: 0x04000BF0 RID: 3056
		IncreaseFocusEnd,
		// Token: 0x04000BF1 RID: 3057
		DecreaseFocusEnd,
		// Token: 0x04000BF2 RID: 3058
		Reset,
		// Token: 0x04000BF3 RID: 3059
		AcceptPoll,
		// Token: 0x04000BF4 RID: 3060
		DeclinePoll,
		// Token: 0x04000BF5 RID: 3061
		ToggleSail,
		// Token: 0x04000BF6 RID: 3062
		ToggleOarsmen,
		// Token: 0x04000BF7 RID: 3063
		ChangeShipCamera,
		// Token: 0x04000BF8 RID: 3064
		SelectShip,
		// Token: 0x04000BF9 RID: 3065
		AttemptBoarding,
		// Token: 0x04000BFA RID: 3066
		ToggleRangedWeaponOrderMode,
		// Token: 0x04000BFB RID: 3067
		TotalGameKeyCount
	}
}
