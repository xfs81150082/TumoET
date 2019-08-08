using ETModel;
namespace ETModel
{
    #region ///20190613
    [Message(OuterOpcode.M2C_GetEnemyUnits)]
    public partial class M2C_GetEnemyUnits : IActorMessage { }
    [Message(OuterOpcode.M2C_AddUnits)]
    public partial class M2C_AddUnits : IActorMessage { }
    [Message(OuterOpcode.M2C_RemoveUnits)]
    public partial class M2C_RemoveUnits : IActorMessage { }
    [Message(OuterOpcode.M2C_AddUnit)]
    public partial class M2C_AddUnit : IActorMessage { }
    [Message(OuterOpcode.M2C_RemoveUnit)]
    public partial class M2C_RemoveUnit : IActorMessage { }
    [Message(OuterOpcode.W2M_DeathActorRequest)]
    public partial class W2M_DeathActorRequest : IActorLocationRequest { }
    [Message(OuterOpcode.M2W_DeathActorResponse)]
    public partial class M2W_DeathActorResponse : IActorLocationResponse { }

    [Message(OuterOpcode.C2G_PingRequest)]
    public partial class C2G_PingRequest : IRequest { }
    [Message(OuterOpcode.G2C_PingResponse)]
    public partial class G2C_PingResponse : IResponse  {  }

    [Message(OuterOpcode.SqrDistance_Map)]
    public partial class SqrDistance_Map : IActorLocationMessage { }
    [Message(OuterOpcode.Patrol_Map)]
    public partial class Patrol_Map : IActorLocationMessage { }
    [Message(OuterOpcode.See_Map)]
    public partial class See_Map : IActorLocationMessage { }
    [Message(OuterOpcode.Attack_Map)]
    public partial class Attack_Map : IActorLocationMessage { }
    [Message(OuterOpcode.Move_Map)]
    public partial class Move_Map : IActorLocationMessage { }
    [Message(OuterOpcode.Move_KeyCodeMap)]
    public partial class Move_KeyCodeMap : IActorMessage { }

    [Message(OuterOpcode.C2M_KeyboardPathResult)]
    public partial class C2M_KeyboardPathResult : IActorLocationMessage { }
    [Message(OuterOpcode.M2C_KeyboardPosition)]
    public partial class M2C_KeyboardPosition : IActorMessage { }
    [Message(OuterOpcode.M2C_KeyboardEulerAnglers)]
    public partial class M2C_KeyboardEulerAnglers : IActorMessage { }
    [Message(OuterOpcode.C2M_KeyboardSkillResult)]
    public partial class C2M_KeyboardSkillResult : IActorLocationMessage { }

    [Message(OuterOpcode.C2M_KeyboardSkillRequest)]
    public partial class C2M_KeyboardSkillRequest : IActorLocationRequest { }
    [Message(OuterOpcode.M2C_KeyboardSkillResponse)]
    public partial class M2C_KeyboardSkillResponse : IActorLocationResponse { }

    #endregion


    #region
    [Message(OuterOpcode.C2M_TestRequest)]
	public partial class C2M_TestRequest : IActorLocationRequest {}

	[Message(OuterOpcode.M2C_TestResponse)]
	public partial class M2C_TestResponse : IActorLocationResponse {}

	[Message(OuterOpcode.Actor_TransferRequest)]
	public partial class Actor_TransferRequest : IActorLocationRequest {}

	[Message(OuterOpcode.Actor_TransferResponse)]
	public partial class Actor_TransferResponse : IActorLocationResponse {}

	[Message(OuterOpcode.C2G_EnterMap)]
	public partial class C2G_EnterMap : IRequest {}

	[Message(OuterOpcode.G2C_EnterMap)]
	public partial class G2C_EnterMap : IResponse {}

// 自己的unit id
// 所有的unit
	[Message(OuterOpcode.UnitInfo)]
	public partial class UnitInfo {}

	[Message(OuterOpcode.M2C_CreateUnits)]
	public partial class M2C_CreateUnits : IActorMessage {}

	[Message(OuterOpcode.Frame_ClickMap)]
	public partial class Frame_ClickMap : IActorLocationMessage {}

	[Message(OuterOpcode.M2C_PathfindingResult)]
	public partial class M2C_PathfindingResult : IActorMessage {}

	[Message(OuterOpcode.C2R_Ping)]
	public partial class C2R_Ping : IRequest {}

	[Message(OuterOpcode.R2C_Ping)]
	public partial class R2C_Ping : IResponse {}

	[Message(OuterOpcode.G2C_Test)]
	public partial class G2C_Test : IMessage {}

	[Message(OuterOpcode.C2M_Reload)]
	public partial class C2M_Reload : IRequest {}

	[Message(OuterOpcode.M2C_Reload)]
	public partial class M2C_Reload : IResponse {}
    #endregion


}
namespace ETModel
{
    public static partial class OuterOpcode
    {
        #region ///20190613
        public const ushort M2C_GetEnemyUnits = 1401;
        public const ushort M2C_AddUnits = 1403;
        public const ushort M2C_RemoveUnits = 1404;
        public const ushort M2C_AddUnit = 1405;
        public const ushort M2C_RemoveUnit = 1406;
        public const ushort W2M_DeathActorRequest = 1407;
        public const ushort M2W_DeathActorResponse = 1408;

        public const ushort C2G_PingRequest = 1421;
        public const ushort G2C_PingResponse = 1422;

        public const ushort SqrDistance_Map = 1431;
        public const ushort Patrol_Map = 1433;
        public const ushort See_Map = 1434;
        public const ushort Attack_Map = 1435;
        public const ushort Move_Map = 1436;
        public const ushort Move_KeyCodeMap = 1437;

        public const ushort C2M_KeyboardPathResult = 1439;
        public const ushort M2C_KeyboardPosition = 1443;
        public const ushort M2C_KeyboardEulerAnglers = 1445;
        public const ushort C2M_KeyboardSkillResult = 1447;

        public const ushort C2M_KeyboardSkillRequest = 1449;
        public const ushort M2C_KeyboardSkillResponse = 1451;


        #endregion


        #region
        public const ushort C2M_TestRequest = 101;
        public const ushort M2C_TestResponse = 102;
        public const ushort Actor_TransferRequest = 103;
        public const ushort Actor_TransferResponse = 104;
        public const ushort C2G_EnterMap = 105;
        public const ushort G2C_EnterMap = 106;
        public const ushort UnitInfo = 107;
        public const ushort M2C_CreateUnits = 108;
        public const ushort Frame_ClickMap = 109;
        public const ushort M2C_PathfindingResult = 110;
        public const ushort C2R_Ping = 111;
        public const ushort R2C_Ping = 112;
        public const ushort G2C_Test = 113;
        public const ushort C2M_Reload = 114;
        public const ushort M2C_Reload = 115;
        #endregion
    }
}