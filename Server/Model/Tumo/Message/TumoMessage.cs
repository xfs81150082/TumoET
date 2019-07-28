using System;
using System.Collections.Generic;
using System.Text;
using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using scg = global::System.Collections.Generic;

namespace ETModel
{
    #region  ///20190613
    public partial class M2C_GetEnemyUnits : pb::IMessage
    {
        private static readonly pb::MessageParser<M2C_GetEnemyUnits> _parser = new pb::MessageParser<M2C_GetEnemyUnits>(() => (M2C_GetEnemyUnits)MessagePool.Instance.Fetch(typeof(M2C_GetEnemyUnits)));
        public static pb::MessageParser<M2C_GetEnemyUnits> Parser { get { return _parser; } }

        private int rpcId_;
        public int RpcId
        {
            get { return rpcId_; }
            set
            {
                rpcId_ = value;
            }
        }

        private long actorId_;
        public long ActorId
        {
            get { return actorId_; }
            set
            {
                actorId_ = value;
            }
        }

        private static readonly pb::FieldCodec<global::ETModel.UnitInfo> _repeated_units_codec
            = pb::FieldCodec.ForMessage(10, global::ETModel.UnitInfo.Parser);
        private pbc::RepeatedField<global::ETModel.UnitInfo> units_ = new pbc::RepeatedField<global::ETModel.UnitInfo>();
        public pbc::RepeatedField<global::ETModel.UnitInfo> Units
        {
            get { return units_; }
            set { units_ = value; }
        }

        public void WriteTo(pb::CodedOutputStream output)
        {
            units_.WriteTo(output, _repeated_units_codec);
            if (RpcId != 0)
            {
                output.WriteRawTag(208, 5);
                output.WriteInt32(RpcId);
            }
            if (ActorId != 0L)
            {
                output.WriteRawTag(232, 5);
                output.WriteInt64(ActorId);
            }
        }

        public int CalculateSize()
        {
            int size = 0;
            if (RpcId != 0)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt32Size(RpcId);
            }
            if (ActorId != 0L)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt64Size(ActorId);
            }
            size += units_.CalculateSize(_repeated_units_codec);
            return size;
        }

        public void MergeFrom(pb::CodedInputStream input)
        {
            for (int i = 0; i < units_.Count; i++) { MessagePool.Instance.Recycle(units_[i]); }
            units_.Clear();
            rpcId_ = 0;
            actorId_ = 0;
            uint tag;
            while ((tag = input.ReadTag()) != 0)
            {
                switch (tag)
                {
                    default:
                        input.SkipLastField();
                        break;
                    case 10:
                        {
                            units_.AddEntriesFrom(input, _repeated_units_codec);
                            break;
                        }
                    case 720:
                        {
                            RpcId = input.ReadInt32();
                            break;
                        }
                    case 744:
                        {
                            ActorId = input.ReadInt64();
                            break;
                        }
                }
            }
        }

    }
    public partial class M2C_AddUnits : pb::IMessage
    {
        private static readonly pb::MessageParser<M2C_AddUnits> _parser = new pb::MessageParser<M2C_AddUnits>(() => (M2C_AddUnits)MessagePool.Instance.Fetch(typeof(M2C_AddUnits)));
        public static pb::MessageParser<M2C_AddUnits> Parser { get { return _parser; } }

        private int rpcId_;
        public int RpcId
        {
            get { return rpcId_; }
            set
            {
                rpcId_ = value;
            }
        }

        private long actorId_;
        public long ActorId
        {
            get { return actorId_; }
            set
            {
                actorId_ = value;
            }
        }

        private int unitType_;
        public int UnitType
        {
            get { return unitType_; }
            set
            {
                unitType_ = value;
            }
        }

        private static readonly pb::FieldCodec<global::ETModel.UnitInfo> _repeated_units_codec
            = pb::FieldCodec.ForMessage(10, global::ETModel.UnitInfo.Parser);
        private pbc::RepeatedField<global::ETModel.UnitInfo> units_ = new pbc::RepeatedField<global::ETModel.UnitInfo>();
        public pbc::RepeatedField<global::ETModel.UnitInfo> Units
        {
            get { return units_; }
            set { units_ = value; }
        }

        public void WriteTo(pb::CodedOutputStream output)
        {
            units_.WriteTo(output, _repeated_units_codec);
            if (RpcId != 0)
            {
                output.WriteRawTag(208, 5);
                output.WriteInt32(RpcId);
            }
            if (ActorId != 0L)
            {
                output.WriteRawTag(232, 5);
                output.WriteInt64(ActorId);
            }
            if (UnitType != 0)
            {
                output.WriteRawTag(240, 5);
                output.WriteInt32(UnitType);
            }
        }

        public int CalculateSize()
        {
            int size = 0;
            if (RpcId != 0)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt32Size(RpcId);
            }
            if (ActorId != 0L)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt64Size(ActorId);
            }
            if (UnitType != 0)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt32Size(UnitType);
            }
            size += units_.CalculateSize(_repeated_units_codec);
            return size;
        }

        public void MergeFrom(pb::CodedInputStream input)
        {
            for (int i = 0; i < units_.Count; i++) { MessagePool.Instance.Recycle(units_[i]); }
            units_.Clear();
            rpcId_ = 0;
            actorId_ = 0;
            unitType_ = 0;
            uint tag;
            while ((tag = input.ReadTag()) != 0)
            {
                switch (tag)
                {
                    default:
                        input.SkipLastField();
                        break;
                    case 10:
                        {
                            units_.AddEntriesFrom(input, _repeated_units_codec);
                            break;
                        }
                    case 720:
                        {
                            RpcId = input.ReadInt32();
                            break;
                        }
                    case 744:
                        {
                            ActorId = input.ReadInt64();
                            break;
                        }
                    case 752:
                        {
                            UnitType = input.ReadInt32();
                            break;
                        }
                }
            }
        }

    }
    public partial class M2C_RemoveUnits : pb::IMessage
    {
        private static readonly pb::MessageParser<M2C_RemoveUnits> _parser = new pb::MessageParser<M2C_RemoveUnits>(() => (M2C_RemoveUnits)MessagePool.Instance.Fetch(typeof(M2C_RemoveUnits)));
        public static pb::MessageParser<M2C_RemoveUnits> Parser { get { return _parser; } }

        private int rpcId_;
        public int RpcId
        {
            get { return rpcId_; }
            set
            {
                rpcId_ = value;
            }
        }

        private long actorId_;
        public long ActorId
        {
            get { return actorId_; }
            set
            {
                actorId_ = value;
            }
        }

        private int unitType_;
        public int UnitType
        {
            get { return unitType_; }
            set
            {
                unitType_ = value;
            }
        }

        private static readonly pb::FieldCodec<global::ETModel.UnitInfo> _repeated_units_codec
            = pb::FieldCodec.ForMessage(10, global::ETModel.UnitInfo.Parser);
        private pbc::RepeatedField<global::ETModel.UnitInfo> units_ = new pbc::RepeatedField<global::ETModel.UnitInfo>();
        public pbc::RepeatedField<global::ETModel.UnitInfo> Units
        {
            get { return units_; }
            set { units_ = value; }
        }

        public void WriteTo(pb::CodedOutputStream output)
        {
            units_.WriteTo(output, _repeated_units_codec);
            if (RpcId != 0)
            {
                output.WriteRawTag(208, 5);
                output.WriteInt32(RpcId);
            }
            if (ActorId != 0L)
            {
                output.WriteRawTag(232, 5);
                output.WriteInt64(ActorId);
            }
            if (UnitType != 0)
            {
                output.WriteRawTag(240, 5);
                output.WriteInt32(UnitType);
            }
        }

        public int CalculateSize()
        {
            int size = 0;
            if (RpcId != 0)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt32Size(RpcId);
            }
            if (ActorId != 0L)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt64Size(ActorId);
            }
            if (UnitType != 0)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt32Size(UnitType);
            }
            size += units_.CalculateSize(_repeated_units_codec);
            return size;
        }

        public void MergeFrom(pb::CodedInputStream input)
        {
            for (int i = 0; i < units_.Count; i++) { MessagePool.Instance.Recycle(units_[i]); }
            units_.Clear();
            rpcId_ = 0;
            actorId_ = 0;
            unitType_ = 0;
            uint tag;
            while ((tag = input.ReadTag()) != 0)
            {
                switch (tag)
                {
                    default:
                        input.SkipLastField();
                        break;
                    case 10:
                        {
                            units_.AddEntriesFrom(input, _repeated_units_codec);
                            break;
                        }
                    case 720:
                        {
                            RpcId = input.ReadInt32();
                            break;
                        }
                    case 744:
                        {
                            ActorId = input.ReadInt64();
                            break;
                        }
                    case 752:
                        {
                            UnitType = input.ReadInt32();
                            break;
                        }
                }
            }
        }

    }
    public partial class M2C_AddUnit : pb::IMessage
    {
        private static readonly pb::MessageParser<M2C_AddUnit> _parser = new pb::MessageParser<M2C_AddUnit>(() => (M2C_AddUnit)MessagePool.Instance.Fetch(typeof(M2C_AddUnit)));
        public static pb::MessageParser<M2C_AddUnit> Parser { get { return _parser; } }

        private int rpcId_;
        public int RpcId
        {
            get { return rpcId_; }
            set
            {
                rpcId_ = value;
            }
        }

        private long actorId_;
        public long ActorId
        {
            get { return actorId_; }
            set
            {
                actorId_ = value;
            }
        }

        private string message_ = "";
        public string Message
        {
            get { return message_; }
            set
            {
                message_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
            }
        }

        private string request_ = "";
        public string Request
        {
            get { return request_; }
            set
            {
                request_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
            }
        }

        public void WriteTo(pb::CodedOutputStream output)
        {
            if (Request.Length != 0)
            {
                output.WriteRawTag(10);
                output.WriteString(Request);
            }
            if (RpcId != 0)
            {
                output.WriteRawTag(208, 5);
                output.WriteInt32(RpcId);
            }
            if (ActorId != 0)
            {
                output.WriteRawTag(232, 5);
                output.WriteInt64(ActorId);
            }
            if (Message.Length != 0)
            {
                output.WriteRawTag(242, 5);
                output.WriteString(Message);
            }
        }

        public int CalculateSize()
        {
            int size = 0;
            if (RpcId != 0)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt32Size(RpcId);
            }
            if (ActorId != 0)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt64Size(ActorId);
            }
            if (Message.Length != 0)
            {
                size += 2 + pb::CodedOutputStream.ComputeStringSize(Message);
            }
            if (Request.Length != 0)
            {
                size += 1 + pb::CodedOutputStream.ComputeStringSize(Request);
            }
            return size;
        }

        public void MergeFrom(pb::CodedInputStream input)
        {
            request_ = "";
            rpcId_ = 0;
            actorId_ = 0;
            message_ = "";
            uint tag;
            while ((tag = input.ReadTag()) != 0)
            {
                switch (tag)
                {
                    default:
                        input.SkipLastField();
                        break;
                    case 10:
                        {
                            Request = input.ReadString();
                            break;
                        }
                    case 720:
                        {
                            RpcId = input.ReadInt32();
                            break;
                        }
                    case 744:
                        {
                            ActorId = input.ReadInt64();
                            break;
                        }
                    case 754:
                        {
                            Message = input.ReadString();
                            break;
                        }
                }
            }
        }

    }
    public partial class M2C_RemoveUnit : pb::IMessage
    {
        private static readonly pb::MessageParser<M2C_RemoveUnit> _parser = new pb::MessageParser<M2C_RemoveUnit>(() => (M2C_RemoveUnit)MessagePool.Instance.Fetch(typeof(M2C_RemoveUnit)));
        public static pb::MessageParser<M2C_RemoveUnit> Parser { get { return _parser; } }

        private int rpcId_;
        public int RpcId
        {
            get { return rpcId_; }
            set
            {
                rpcId_ = value;
            }
        }

        private long actorId_;
        public long ActorId
        {
            get { return actorId_; }
            set
            {
                actorId_ = value;
            }
        }

        private string message_ = "";
        public string Message
        {
            get { return message_; }
            set
            {
                message_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
            }
        }

        private string request_ = "";
        public string Request
        {
            get { return request_; }
            set
            {
                request_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
            }
        }

        public void WriteTo(pb::CodedOutputStream output)
        {
            if (Request.Length != 0)
            {
                output.WriteRawTag(10);
                output.WriteString(Request);
            }
            if (RpcId != 0)
            {
                output.WriteRawTag(208, 5);
                output.WriteInt32(RpcId);
            }
            if (ActorId != 0)
            {
                output.WriteRawTag(232, 5);
                output.WriteInt64(ActorId);
            }
            if (Message.Length != 0)
            {
                output.WriteRawTag(242, 5);
                output.WriteString(Message);
            }
        }

        public int CalculateSize()
        {
            int size = 0;
            if (RpcId != 0)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt32Size(RpcId);
            }
            if (ActorId != 0)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt64Size(ActorId);
            }
            if (Message.Length != 0)
            {
                size += 2 + pb::CodedOutputStream.ComputeStringSize(Message);
            }
            if (Request.Length != 0)
            {
                size += 1 + pb::CodedOutputStream.ComputeStringSize(Request);
            }
            return size;
        }

        public void MergeFrom(pb::CodedInputStream input)
        {
            request_ = "";
            rpcId_ = 0;
            actorId_ = 0;
            message_ = "";
            uint tag;
            while ((tag = input.ReadTag()) != 0)
            {
                switch (tag)
                {
                    default:
                        input.SkipLastField();
                        break;
                    case 10:
                        {
                            Request = input.ReadString();
                            break;
                        }
                    case 720:
                        {
                            RpcId = input.ReadInt32();
                            break;
                        }
                    case 744:
                        {
                            ActorId = input.ReadInt64();
                            break;
                        }
                    case 754:
                        {
                            Message = input.ReadString();
                            break;
                        }
                }
            }
        }

    }
    public partial class W2M_DeathActorRequest : pb::IMessage
    {
        private static readonly pb::MessageParser<W2M_DeathActorRequest> _parser = new pb::MessageParser<W2M_DeathActorRequest>(() => (W2M_DeathActorRequest)MessagePool.Instance.Fetch(typeof(W2M_DeathActorRequest)));
        public static pb::MessageParser<W2M_DeathActorRequest> Parser { get { return _parser; } }

        private int rpcId_;
        public int RpcId
        {
            get { return rpcId_; }
            set
            {
                rpcId_ = value;
            }
        }

        private long actorId_;
        public long ActorId
        {
            get { return actorId_; }
            set
            {
                actorId_ = value;
            }
        }

        private string info_ = "";
        public string Info
        {
            get { return info_; }
            set
            {
                info_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
            }
        }

        public void WriteTo(pb::CodedOutputStream output)
        {
            if (Info.Length != 0)
            {
                output.WriteRawTag(10);
                output.WriteString(Info);
            }
            if (RpcId != 0)
            {
                output.WriteRawTag(208, 5);
                output.WriteInt32(RpcId);
            }
            if (ActorId != 0L)
            {
                output.WriteRawTag(216, 5);
                output.WriteInt64(ActorId);
            }
        }

        public int CalculateSize()
        {
            int size = 0;
            if (RpcId != 0)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt32Size(RpcId);
            }
            if (ActorId != 0L)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt64Size(ActorId);
            }
            if (Info.Length != 0)
            {
                size += 1 + pb::CodedOutputStream.ComputeStringSize(Info);
            }
            return size;
        }

        public void MergeFrom(pb::CodedInputStream input)
        {
            info_ = "";
            rpcId_ = 0;
            actorId_ = 0;
            uint tag;
            while ((tag = input.ReadTag()) != 0)
            {
                switch (tag)
                {
                    default:
                        input.SkipLastField();
                        break;
                    case 10:
                        {
                            Info = input.ReadString();
                            break;
                        }
                    case 720:
                        {
                            RpcId = input.ReadInt32();
                            break;
                        }
                    case 728:
                        {
                            ActorId = input.ReadInt64();
                            break;
                        }
                }
            }
        }

    }
    public partial class M2W_DeathActorResponse : pb::IMessage
    {
        private static readonly pb::MessageParser<M2W_DeathActorResponse> _parser = new pb::MessageParser<M2W_DeathActorResponse>(() => (M2W_DeathActorResponse)MessagePool.Instance.Fetch(typeof(M2W_DeathActorResponse)));
        public static pb::MessageParser<M2W_DeathActorResponse> Parser { get { return _parser; } }

        private int rpcId_;
        public int RpcId
        {
            get { return rpcId_; }
            set
            {
                rpcId_ = value;
            }
        }

        private int error_;
        public int Error
        {
            get { return error_; }
            set
            {
                error_ = value;
            }
        }

        private string message_ = "";
        public string Message
        {
            get { return message_; }
            set
            {
                message_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
            }
        }

        private string info_ = "";
        public string Info
        {
            get { return info_; }
            set
            {
                info_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
            }
        }

        public void WriteTo(pb::CodedOutputStream output)
        {
            if (Info.Length != 0)
            {
                output.WriteRawTag(10);
                output.WriteString(Info);
            }
            if (RpcId != 0)
            {
                output.WriteRawTag(208, 5);
                output.WriteInt32(RpcId);
            }
            if (Error != 0)
            {
                output.WriteRawTag(216, 5);
                output.WriteInt32(Error);
            }
            if (Message.Length != 0)
            {
                output.WriteRawTag(226, 5);
                output.WriteString(Message);
            }
        }

        public int CalculateSize()
        {
            int size = 0;
            if (RpcId != 0)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt32Size(RpcId);
            }
            if (Error != 0)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt32Size(Error);
            }
            if (Message.Length != 0)
            {
                size += 2 + pb::CodedOutputStream.ComputeStringSize(Message);
            }
            if (Info.Length != 0)
            {
                size += 1 + pb::CodedOutputStream.ComputeStringSize(Info);
            }
            return size;
        }

        public void MergeFrom(pb::CodedInputStream input)
        {
            info_ = "";
            rpcId_ = 0;
            error_ = 0;
            message_ = "";
            uint tag;
            while ((tag = input.ReadTag()) != 0)
            {
                switch (tag)
                {
                    default:
                        input.SkipLastField();
                        break;
                    case 10:
                        {
                            Info = input.ReadString();
                            break;
                        }
                    case 720:
                        {
                            RpcId = input.ReadInt32();
                            break;
                        }
                    case 728:
                        {
                            Error = input.ReadInt32();
                            break;
                        }
                    case 738:
                        {
                            Message = input.ReadString();
                            break;
                        }
                }
            }
        }

    }

    public partial class C2G_PingRequest : pb::IMessage
    {
        private static readonly pb::MessageParser<C2G_PingRequest> _parser = new pb::MessageParser<C2G_PingRequest>(() => (C2G_PingRequest)MessagePool.Instance.Fetch(typeof(C2G_PingRequest)));
        public static pb::MessageParser<C2G_PingRequest> Parser { get { return _parser; } }

        private int rpcId_;
        public int RpcId
        {
            get { return rpcId_; }
            set
            {
                rpcId_ = value;
            }
        }

        public void WriteTo(pb::CodedOutputStream output)
        {
            if (RpcId != 0)
            {
                output.WriteRawTag(208, 5);
                output.WriteInt32(RpcId);
            }
        }

        public int CalculateSize()
        {
            int size = 0;
            if (RpcId != 0)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt32Size(RpcId);
            }
            return size;
        }

        public void MergeFrom(pb::CodedInputStream input)
        {
            rpcId_ = 0;
            uint tag;
            while ((tag = input.ReadTag()) != 0)
            {
                switch (tag)
                {
                    default:
                        input.SkipLastField();
                        break;
                    case 720:
                        {
                            RpcId = input.ReadInt32();
                            break;
                        }
                }
            }
        }

    }
    public partial class G2C_PingResponse : pb::IMessage
    {
        private static readonly pb::MessageParser<G2C_PingResponse> _parser = new pb::MessageParser<G2C_PingResponse>(() => (G2C_PingResponse)MessagePool.Instance.Fetch(typeof(G2C_PingResponse)));
        public static pb::MessageParser<G2C_PingResponse> Parser { get { return _parser; } }

        private int rpcId_;
        public int RpcId
        {
            get { return rpcId_; }
            set
            {
                rpcId_ = value;
            }
        }

        private int error_;
        public int Error
        {
            get { return error_; }
            set
            {
                error_ = value;
            }
        }

        private string message_ = "";
        public string Message
        {
            get { return message_; }
            set
            {
                message_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
            }
        }

        public void WriteTo(pb::CodedOutputStream output)
        {
            if (RpcId != 0)
            {
                output.WriteRawTag(208, 5);
                output.WriteInt32(RpcId);
            }
            if (Error != 0)
            {
                output.WriteRawTag(216, 5);
                output.WriteInt32(Error);
            }
            if (Message.Length != 0)
            {
                output.WriteRawTag(226, 5);
                output.WriteString(Message);
            }
        }

        public int CalculateSize()
        {
            int size = 0;
            if (RpcId != 0)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt32Size(RpcId);
            }
            if (Error != 0)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt32Size(Error);
            }
            if (Message.Length != 0)
            {
                size += 2 + pb::CodedOutputStream.ComputeStringSize(Message);
            }
            return size;
        }

        public void MergeFrom(pb::CodedInputStream input)
        {
            rpcId_ = 0;
            error_ = 0;
            message_ = "";
            uint tag;
            while ((tag = input.ReadTag()) != 0)
            {
                switch (tag)
                {
                    default:
                        input.SkipLastField();
                        break;
                    case 720:
                        {
                            RpcId = input.ReadInt32();
                            break;
                        }
                    case 728:
                        {
                            Error = input.ReadInt32();
                            break;
                        }
                    case 738:
                        {
                            Message = input.ReadString();
                            break;
                        }
                }
            }
        }

    }

    #endregion

    #region  ///20190722
    public partial class SqrDistance_Map : pb::IMessage
    {
        private static readonly pb::MessageParser<SqrDistance_Map> _parser = new pb::MessageParser<SqrDistance_Map>(() => (SqrDistance_Map)MessagePool.Instance.Fetch(typeof(SqrDistance_Map)));
        public static pb::MessageParser<SqrDistance_Map> Parser { get { return _parser; } }

        private int rpcId_;
        public int RpcId
        {
            get { return rpcId_; }
            set
            {
                rpcId_ = value;
            }
        }

        private long actorId_;
        public long ActorId
        {
            get { return actorId_; }
            set
            {
                actorId_ = value;
            }
        }

        private long id_;
        public long Id
        {
            get { return id_; }
            set
            {
                id_ = value;
            }
        }

        private float x_;
        public float X
        {
            get { return x_; }
            set
            {
                x_ = value;
            }
        }

        private float y_;
        public float Y
        {
            get { return y_; }
            set
            {
                y_ = value;
            }
        }

        private float z_;
        public float Z
        {
            get { return z_; }
            set
            {
                z_ = value;
            }
        }

        public void WriteTo(pb::CodedOutputStream output)
        {
            if (X != 0F)
            {
                output.WriteRawTag(13);
                output.WriteFloat(X);
            }
            if (Y != 0F)
            {
                output.WriteRawTag(21);
                output.WriteFloat(Y);
            }
            if (Z != 0F)
            {
                output.WriteRawTag(29);
                output.WriteFloat(Z);
            }
            if (RpcId != 0)
            {
                output.WriteRawTag(208, 5);
                output.WriteInt32(RpcId);
            }
            if (ActorId != 0L)
            {
                output.WriteRawTag(232, 5);
                output.WriteInt64(ActorId);
            }
            if (Id != 0L)
            {
                output.WriteRawTag(240, 5);
                output.WriteInt64(Id);
            }
        }

        public int CalculateSize()
        {
            int size = 0;
            if (RpcId != 0)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt32Size(RpcId);
            }
            if (ActorId != 0L)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt64Size(ActorId);
            }
            if (Id != 0L)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt64Size(Id);
            }
            if (X != 0F)
            {
                size += 1 + 4;
            }
            if (Y != 0F)
            {
                size += 1 + 4;
            }
            if (Z != 0F)
            {
                size += 1 + 4;
            }
            return size;
        }

        public void MergeFrom(pb::CodedInputStream input)
        {
            x_ = 0f;
            y_ = 0f;
            z_ = 0f;
            rpcId_ = 0;
            actorId_ = 0;
            id_ = 0;
            uint tag;
            while ((tag = input.ReadTag()) != 0)
            {
                switch (tag)
                {
                    default:
                        input.SkipLastField();
                        break;
                    case 13:
                        {
                            X = input.ReadFloat();
                            break;
                        }
                    case 21:
                        {
                            Y = input.ReadFloat();
                            break;
                        }
                    case 29:
                        {
                            Z = input.ReadFloat();
                            break;
                        }
                    case 720:
                        {
                            RpcId = input.ReadInt32();
                            break;
                        }
                    case 744:
                        {
                            ActorId = input.ReadInt64();
                            break;
                        }
                    case 752:
                        {
                            Id = input.ReadInt64();
                            break;
                        }
                }
            }
        }

    }
    public partial class Patrol_Map : pb::IMessage
    {
        private static readonly pb::MessageParser<Patrol_Map> _parser = new pb::MessageParser<Patrol_Map>(() => (Patrol_Map)MessagePool.Instance.Fetch(typeof(Patrol_Map)));
        public static pb::MessageParser<Patrol_Map> Parser { get { return _parser; } }

        private int rpcId_;
        public int RpcId
        {
            get { return rpcId_; }
            set
            {
                rpcId_ = value;
            }
        }

        private long actorId_;
        public long ActorId
        {
            get { return actorId_; }
            set
            {
                actorId_ = value;
            }
        }

        private long id_;
        public long Id
        {
            get { return id_; }
            set
            {
                id_ = value;
            }
        }

        private float x_;
        public float X
        {
            get { return x_; }
            set
            {
                x_ = value;
            }
        }

        private float y_;
        public float Y
        {
            get { return y_; }
            set
            {
                y_ = value;
            }
        }

        private float z_;
        public float Z
        {
            get { return z_; }
            set
            {
                z_ = value;
            }
        }

        public void WriteTo(pb::CodedOutputStream output)
        {
            if (X != 0F)
            {
                output.WriteRawTag(13);
                output.WriteFloat(X);
            }
            if (Y != 0F)
            {
                output.WriteRawTag(21);
                output.WriteFloat(Y);
            }
            if (Z != 0F)
            {
                output.WriteRawTag(29);
                output.WriteFloat(Z);
            }
            if (RpcId != 0)
            {
                output.WriteRawTag(208, 5);
                output.WriteInt32(RpcId);
            }
            if (ActorId != 0L)
            {
                output.WriteRawTag(232, 5);
                output.WriteInt64(ActorId);
            }
            if (Id != 0L)
            {
                output.WriteRawTag(240, 5);
                output.WriteInt64(Id);
            }
        }

        public int CalculateSize()
        {
            int size = 0;
            if (RpcId != 0)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt32Size(RpcId);
            }
            if (ActorId != 0L)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt64Size(ActorId);
            }
            if (Id != 0L)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt64Size(Id);
            }
            if (X != 0F)
            {
                size += 1 + 4;
            }
            if (Y != 0F)
            {
                size += 1 + 4;
            }
            if (Z != 0F)
            {
                size += 1 + 4;
            }
            return size;
        }

        public void MergeFrom(pb::CodedInputStream input)
        {
            x_ = 0f;
            y_ = 0f;
            z_ = 0f;
            rpcId_ = 0;
            actorId_ = 0;
            id_ = 0;
            uint tag;
            while ((tag = input.ReadTag()) != 0)
            {
                switch (tag)
                {
                    default:
                        input.SkipLastField();
                        break;
                    case 13:
                        {
                            X = input.ReadFloat();
                            break;
                        }
                    case 21:
                        {
                            Y = input.ReadFloat();
                            break;
                        }
                    case 29:
                        {
                            Z = input.ReadFloat();
                            break;
                        }
                    case 720:
                        {
                            RpcId = input.ReadInt32();
                            break;
                        }
                    case 744:
                        {
                            ActorId = input.ReadInt64();
                            break;
                        }
                    case 752:
                        {
                            Id = input.ReadInt64();
                            break;
                        }
                }
            }
        }

    }
    public partial class See_Map : pb::IMessage
    {
        private static readonly pb::MessageParser<See_Map> _parser = new pb::MessageParser<See_Map>(() => (See_Map)MessagePool.Instance.Fetch(typeof(See_Map)));
        public static pb::MessageParser<See_Map> Parser { get { return _parser; } }

        private int rpcId_;
        public int RpcId
        {
            get { return rpcId_; }
            set
            {
                rpcId_ = value;
            }
        }

        private long actorId_;
        public long ActorId
        {
            get { return actorId_; }
            set
            {
                actorId_ = value;
            }
        }

        private long id_;
        public long Id
        {
            get { return id_; }
            set
            {
                id_ = value;
            }
        }

        private float x_;
        public float X
        {
            get { return x_; }
            set
            {
                x_ = value;
            }
        }

        private float y_;
        public float Y
        {
            get { return y_; }
            set
            {
                y_ = value;
            }
        }

        private float z_;
        public float Z
        {
            get { return z_; }
            set
            {
                z_ = value;
            }
        }

        public void WriteTo(pb::CodedOutputStream output)
        {
            if (X != 0F)
            {
                output.WriteRawTag(13);
                output.WriteFloat(X);
            }
            if (Y != 0F)
            {
                output.WriteRawTag(21);
                output.WriteFloat(Y);
            }
            if (Z != 0F)
            {
                output.WriteRawTag(29);
                output.WriteFloat(Z);
            }
            if (RpcId != 0)
            {
                output.WriteRawTag(208, 5);
                output.WriteInt32(RpcId);
            }
            if (ActorId != 0L)
            {
                output.WriteRawTag(232, 5);
                output.WriteInt64(ActorId);
            }
            if (Id != 0L)
            {
                output.WriteRawTag(240, 5);
                output.WriteInt64(Id);
            }
        }

        public int CalculateSize()
        {
            int size = 0;
            if (RpcId != 0)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt32Size(RpcId);
            }
            if (ActorId != 0L)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt64Size(ActorId);
            }
            if (Id != 0L)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt64Size(Id);
            }
            if (X != 0F)
            {
                size += 1 + 4;
            }
            if (Y != 0F)
            {
                size += 1 + 4;
            }
            if (Z != 0F)
            {
                size += 1 + 4;
            }
            return size;
        }

        public void MergeFrom(pb::CodedInputStream input)
        {
            x_ = 0f;
            y_ = 0f;
            z_ = 0f;
            rpcId_ = 0;
            actorId_ = 0;
            id_ = 0;
            uint tag;
            while ((tag = input.ReadTag()) != 0)
            {
                switch (tag)
                {
                    default:
                        input.SkipLastField();
                        break;
                    case 13:
                        {
                            X = input.ReadFloat();
                            break;
                        }
                    case 21:
                        {
                            Y = input.ReadFloat();
                            break;
                        }
                    case 29:
                        {
                            Z = input.ReadFloat();
                            break;
                        }
                    case 720:
                        {
                            RpcId = input.ReadInt32();
                            break;
                        }
                    case 744:
                        {
                            ActorId = input.ReadInt64();
                            break;
                        }
                    case 752:
                        {
                            Id = input.ReadInt64();
                            break;
                        }
                }
            }
        }

    }
    public partial class Attack_Map : pb::IMessage
    {
        private static readonly pb::MessageParser<Attack_Map> _parser = new pb::MessageParser<Attack_Map>(() => (Attack_Map)MessagePool.Instance.Fetch(typeof(Attack_Map)));
        public static pb::MessageParser<Attack_Map> Parser { get { return _parser; } }

        private int rpcId_;
        public int RpcId
        {
            get { return rpcId_; }
            set
            {
                rpcId_ = value;
            }
        }

        private long actorId_;
        public long ActorId
        {
            get { return actorId_; }
            set
            {
                actorId_ = value;
            }
        }

        private long id_;
        public long Id
        {
            get { return id_; }
            set
            {
                id_ = value;
            }
        }

        private float x_;
        public float X
        {
            get { return x_; }
            set
            {
                x_ = value;
            }
        }

        private float y_;
        public float Y
        {
            get { return y_; }
            set
            {
                y_ = value;
            }
        }

        private float z_;
        public float Z
        {
            get { return z_; }
            set
            {
                z_ = value;
            }
        }

        public void WriteTo(pb::CodedOutputStream output)
        {
            if (X != 0F)
            {
                output.WriteRawTag(13);
                output.WriteFloat(X);
            }
            if (Y != 0F)
            {
                output.WriteRawTag(21);
                output.WriteFloat(Y);
            }
            if (Z != 0F)
            {
                output.WriteRawTag(29);
                output.WriteFloat(Z);
            }
            if (RpcId != 0)
            {
                output.WriteRawTag(208, 5);
                output.WriteInt32(RpcId);
            }
            if (ActorId != 0L)
            {
                output.WriteRawTag(232, 5);
                output.WriteInt64(ActorId);
            }
            if (Id != 0L)
            {
                output.WriteRawTag(240, 5);
                output.WriteInt64(Id);
            }
        }

        public int CalculateSize()
        {
            int size = 0;
            if (RpcId != 0)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt32Size(RpcId);
            }
            if (ActorId != 0L)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt64Size(ActorId);
            }
            if (Id != 0L)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt64Size(Id);
            }
            if (X != 0F)
            {
                size += 1 + 4;
            }
            if (Y != 0F)
            {
                size += 1 + 4;
            }
            if (Z != 0F)
            {
                size += 1 + 4;
            }
            return size;
        }

        public void MergeFrom(pb::CodedInputStream input)
        {
            x_ = 0f;
            y_ = 0f;
            z_ = 0f;
            rpcId_ = 0;
            actorId_ = 0;
            id_ = 0;
            uint tag;
            while ((tag = input.ReadTag()) != 0)
            {
                switch (tag)
                {
                    default:
                        input.SkipLastField();
                        break;
                    case 13:
                        {
                            X = input.ReadFloat();
                            break;
                        }
                    case 21:
                        {
                            Y = input.ReadFloat();
                            break;
                        }
                    case 29:
                        {
                            Z = input.ReadFloat();
                            break;
                        }
                    case 720:
                        {
                            RpcId = input.ReadInt32();
                            break;
                        }
                    case 744:
                        {
                            ActorId = input.ReadInt64();
                            break;
                        }
                    case 752:
                        {
                            Id = input.ReadInt64();
                            break;
                        }
                }
            }
        }

    }
    public partial class Move_Map : pb::IMessage
    {
        private static readonly pb::MessageParser<Move_Map> _parser = new pb::MessageParser<Move_Map>(() => (Move_Map)MessagePool.Instance.Fetch(typeof(Move_Map)));
        public static pb::MessageParser<Move_Map> Parser { get { return _parser; } }

        private int rpcId_;
        public int RpcId
        {
            get { return rpcId_; }
            set
            {
                rpcId_ = value;
            }
        }

        private long actorId_;
        public long ActorId
        {
            get { return actorId_; }
            set
            {
                actorId_ = value;
            }
        }

        private int keyType_;
        public int KeyType
        {
            get { return keyType_; }
            set
            {
                keyType_ = value;
            }
        }

        private long id_;
        public long Id
        {
            get { return id_; }
            set
            {
                id_ = value;
            }
        }

        private float x_;
        public float X
        {
            get { return x_; }
            set
            {
                x_ = value;
            }
        }

        private float y_;
        public float Y
        {
            get { return y_; }
            set
            {
                y_ = value;
            }
        }

        private float z_;
        public float Z
        {
            get { return z_; }
            set
            {
                z_ = value;
            }
        }

        private float ax_;
        public float AX
        {
            get { return ax_; }
            set
            {
                ax_ = value;
            }
        }

        private float ay_;
        public float AY
        {
            get { return ay_; }
            set
            {
                ay_ = value;
            }
        }

        private float az_;
        public float AZ
        {
            get { return az_; }
            set
            {
                az_ = value;
            }
        }

        private float v_;
        public float V
        {
            get { return v_; }
            set
            {
                v_ = value;
            }
        }

        private float h_;
        public float H
        {
            get { return h_; }
            set
            {
                h_ = value;
            }
        }

        public void WriteTo(pb::CodedOutputStream output)
        {
            if (X != 0F)
            {
                output.WriteRawTag(13);
                output.WriteFloat(X);
            }
            if (Y != 0F)
            {
                output.WriteRawTag(21);
                output.WriteFloat(Y);
            }
            if (Z != 0F)
            {
                output.WriteRawTag(29);
                output.WriteFloat(Z);
            }
            if (AX != 0F)
            {
                output.WriteRawTag(37);
                output.WriteFloat(AX);
            }
            if (AY != 0F)
            {
                output.WriteRawTag(45);
                output.WriteFloat(AY);
            }
            if (AZ != 0F)
            {
                output.WriteRawTag(53);
                output.WriteFloat(AZ);
            }
            if (V != 0F)
            {
                output.WriteRawTag(61);
                output.WriteFloat(V);
            }
            if (H != 0F)
            {
                output.WriteRawTag(69);
                output.WriteFloat(H);
            }
            if (RpcId != 0)
            {
                output.WriteRawTag(208, 5);
                output.WriteInt32(RpcId);
            }
            if (ActorId != 0L)
            {
                output.WriteRawTag(232, 5);
                output.WriteInt64(ActorId);
            }
            if (Id != 0L)
            {
                output.WriteRawTag(240, 5);
                output.WriteInt64(Id);
            }
            if (KeyType != 0)
            {
                output.WriteRawTag(248, 5);
                output.WriteInt32(KeyType);
            }
        }

        public int CalculateSize()
        {
            int size = 0;
            if (RpcId != 0)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt32Size(RpcId);
            }
            if (ActorId != 0L)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt64Size(ActorId);
            }
            if (Id != 0L)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt64Size(Id);
            }
            if (KeyType != 0)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt32Size(KeyType);
            }
            if (X != 0F)
            {
                size += 1 + 4;
            }
            if (Y != 0F)
            {
                size += 1 + 4;
            }
            if (Z != 0F)
            {
                size += 1 + 4;
            }
            if (AX != 0F)
            {
                size += 1 + 4;
            }
            if (AY != 0F)
            {
                size += 1 + 4;
            }
            if (AZ != 0F)
            {
                size += 1 + 4;
            }
            if (V != 0F)
            {
                size += 1 + 4;
            }
            if (H != 0F)
            {
                size += 1 + 4;
            }
            return size;
        }

        public void MergeFrom(pb::CodedInputStream input)
        {
            x_ = 0f;
            y_ = 0f;
            z_ = 0f;
            ax_ = 0f;
            ay_ = 0f;
            az_ = 0f;
            v_ = 0f;
            h_ = 0f;
            rpcId_ = 0;
            actorId_ = 0;
            id_ = 0;
            keyType_ = 0;
            uint tag;
            while ((tag = input.ReadTag()) != 0)
            {
                switch (tag)
                {
                    default:
                        input.SkipLastField();
                        break;
                    case 13:
                        {
                            X = input.ReadFloat();
                            break;
                        }
                    case 21:
                        {
                            Y = input.ReadFloat();
                            break;
                        }
                    case 29:
                        {
                            Z = input.ReadFloat();
                            break;
                        }
                    case 37:
                        {
                            AX = input.ReadFloat();
                            break;
                        }
                    case 45:
                        {
                            AY = input.ReadFloat();
                            break;
                        }
                    case 53:
                        {
                            AZ = input.ReadFloat();
                            break;
                        }
                    case 61:
                        {
                            V = input.ReadFloat();
                            break;
                        }
                    case 69:
                        {
                            H = input.ReadFloat();
                            break;
                        }
                    case 720:
                        {
                            RpcId = input.ReadInt32();
                            break;
                        }
                    case 744:
                        {
                            ActorId = input.ReadInt64();
                            break;
                        }
                    case 752:
                        {
                            Id = input.ReadInt64();
                            break;
                        }
                    case 760:
                        {
                            KeyType = input.ReadInt32();
                            break;
                        }
                }
            }
        }
    }
    public partial class Move_KeyCodeMap : pb::IMessage
    {
        private static readonly pb::MessageParser<Move_KeyCodeMap> _parser = new pb::MessageParser<Move_KeyCodeMap>(() => (Move_KeyCodeMap)MessagePool.Instance.Fetch(typeof(Move_KeyCodeMap)));
        public static pb::MessageParser<Move_KeyCodeMap> Parser { get { return _parser; } }

        private int rpcId_;
        public int RpcId
        {
            get { return rpcId_; }
            set
            {
                rpcId_ = value;
            }
        }

        private long actorId_;
        public long ActorId
        {
            get { return actorId_; }
            set
            {
                actorId_ = value;
            }
        }

        private int keyType_;
        public int KeyType
        {
            get { return keyType_; }
            set
            {
                keyType_ = value;
            }
        }

        private long id_;
        public long Id
        {
            get { return id_; }
            set
            {
                id_ = value;
            }
        }

        private float x_;
        public float X
        {
            get { return x_; }
            set
            {
                x_ = value;
            }
        }

        private float y_;
        public float Y
        {
            get { return y_; }
            set
            {
                y_ = value;
            }
        }

        private float z_;
        public float Z
        {
            get { return z_; }
            set
            {
                z_ = value;
            }
        }

        private float ax_;
        public float AX
        {
            get { return ax_; }
            set
            {
                ax_ = value;
            }
        }

        private float ay_;
        public float AY
        {
            get { return ay_; }
            set
            {
                ay_ = value;
            }
        }

        private float az_;
        public float AZ
        {
            get { return az_; }
            set
            {
                az_ = value;
            }
        }

        private float v_;
        public float V
        {
            get { return v_; }
            set
            {
                v_ = value;
            }
        }

        private float h_;
        public float H
        {
            get { return h_; }
            set
            {
                h_ = value;
            }
        }

        public void WriteTo(pb::CodedOutputStream output)
        {
            if (X != 0F)
            {
                output.WriteRawTag(13);
                output.WriteFloat(X);
            }
            if (Y != 0F)
            {
                output.WriteRawTag(21);
                output.WriteFloat(Y);
            }
            if (Z != 0F)
            {
                output.WriteRawTag(29);
                output.WriteFloat(Z);
            }
            if (AX != 0F)
            {
                output.WriteRawTag(37);
                output.WriteFloat(AX);
            }
            if (AY != 0F)
            {
                output.WriteRawTag(45);
                output.WriteFloat(AY);
            }
            if (AZ != 0F)
            {
                output.WriteRawTag(53);
                output.WriteFloat(AZ);
            }
            if (V != 0F)
            {
                output.WriteRawTag(61);
                output.WriteFloat(V);
            }
            if (H != 0F)
            {
                output.WriteRawTag(69);
                output.WriteFloat(H);
            }
            if (RpcId != 0)
            {
                output.WriteRawTag(208, 5);
                output.WriteInt32(RpcId);
            }
            if (ActorId != 0L)
            {
                output.WriteRawTag(232, 5);
                output.WriteInt64(ActorId);
            }
            if (Id != 0L)
            {
                output.WriteRawTag(240, 5);
                output.WriteInt64(Id);
            }
            if (KeyType != 0)
            {
                output.WriteRawTag(248, 5);
                output.WriteInt32(KeyType);
            }
        }

        public int CalculateSize()
        {
            int size = 0;
            if (RpcId != 0)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt32Size(RpcId);
            }
            if (ActorId != 0L)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt64Size(ActorId);
            }
            if (Id != 0L)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt64Size(Id);
            }
            if (KeyType != 0)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt32Size(KeyType);
            }
            if (X != 0F)
            {
                size += 1 + 4;
            }
            if (Y != 0F)
            {
                size += 1 + 4;
            }
            if (Z != 0F)
            {
                size += 1 + 4;
            }
            if (AX != 0F)
            {
                size += 1 + 4;
            }
            if (AY != 0F)
            {
                size += 1 + 4;
            }
            if (AZ != 0F)
            {
                size += 1 + 4;
            }
            if (V != 0F)
            {
                size += 1 + 4;
            }
            if (H != 0F)
            {
                size += 1 + 4;
            }
            return size;
        }

        public void MergeFrom(pb::CodedInputStream input)
        {
            x_ = 0f;
            y_ = 0f;
            z_ = 0f;
            ax_ = 0f;
            ay_ = 0f;
            az_ = 0f;
            v_ = 0f;
            h_ = 0f;
            rpcId_ = 0;
            actorId_ = 0;
            id_ = 0;
            keyType_ = 0;
            uint tag;
            while ((tag = input.ReadTag()) != 0)
            {
                switch (tag)
                {
                    default:
                        input.SkipLastField();
                        break;
                    case 13:
                        {
                            X = input.ReadFloat();
                            break;
                        }
                    case 21:
                        {
                            Y = input.ReadFloat();
                            break;
                        }
                    case 29:
                        {
                            Z = input.ReadFloat();
                            break;
                        }
                    case 37:
                        {
                            AX = input.ReadFloat();
                            break;
                        }
                    case 45:
                        {
                            AY = input.ReadFloat();
                            break;
                        }
                    case 53:
                        {
                            AZ = input.ReadFloat();
                            break;
                        }
                    case 61:
                        {
                            V = input.ReadFloat();
                            break;
                        }
                    case 69:
                        {
                            H = input.ReadFloat();
                            break;
                        }
                    case 720:
                        {
                            RpcId = input.ReadInt32();
                            break;
                        }
                    case 744:
                        {
                            ActorId = input.ReadInt64();
                            break;
                        }
                    case 752:
                        {
                            Id = input.ReadInt64();
                            break;
                        }
                    case 760:
                        {
                            KeyType = input.ReadInt32();
                            break;
                        }
                }
            }
        }
    }

    public partial class C2M_KeyboardPathResult : pb::IMessage
    {
        private static readonly pb::MessageParser<C2M_KeyboardPathResult> _parser = new pb::MessageParser<C2M_KeyboardPathResult>(() => (C2M_KeyboardPathResult)MessagePool.Instance.Fetch(typeof(C2M_KeyboardPathResult)));
        public static pb::MessageParser<C2M_KeyboardPathResult> Parser { get { return _parser; } }

        private int rpcId_;
        public int RpcId
        {
            get { return rpcId_; }
            set
            {
                rpcId_ = value;
            }
        }

        private long actorId_;
        public long ActorId
        {
            get { return actorId_; }
            set
            {
                actorId_ = value;
            }
        }

        private int keyType_;
        public int KeyType
        {
            get { return keyType_; }
            set
            {
                keyType_ = value;
            }
        }

        private long id_;
        public long Id
        {
            get { return id_; }
            set
            {
                id_ = value;
            }
        }

        private float x_;
        public float X
        {
            get { return x_; }
            set
            {
                x_ = value;
            }
        }

        private float y_;
        public float Y
        {
            get { return y_; }
            set
            {
                y_ = value;
            }
        }

        private float z_;
        public float Z
        {
            get { return z_; }
            set
            {
                z_ = value;
            }
        }

        private int ax_;
        public int KeyOne
        {
            get { return ax_; }
            set
            {
                ax_ = value;
            }
        }

        private int ay_;
        public int KeyTwo
        {
            get { return ay_; }
            set
            {
                ay_ = value;
            }
        }

        private int az_;
        public int KeyThree
        {
            get { return az_; }
            set
            {
                az_ = value;
            }
        }

        private float v_;
        public float V
        {
            get { return v_; }
            set
            {
                v_ = value;
            }
        }

        private float h_;
        public float H
        {
            get { return h_; }
            set
            {
                h_ = value;
            }
        }

        public void WriteTo(pb::CodedOutputStream output)
        {
            if (X != 0F)
            {
                output.WriteRawTag(13);
                output.WriteFloat(X);
            }
            if (Y != 0F)
            {
                output.WriteRawTag(21);
                output.WriteFloat(Y);
            }
            if (Z != 0F)
            {
                output.WriteRawTag(29);
                output.WriteFloat(Z);
            }
            if (KeyOne != 0F)
            {
                output.WriteRawTag(37);
                output.WriteInt32(KeyOne);
            }
            if (KeyTwo != 0F)
            {
                output.WriteRawTag(45);
                output.WriteInt32(KeyTwo);
            }
            if (KeyThree != 0F)
            {
                output.WriteRawTag(53);
                output.WriteInt32(KeyThree);
            }
            if (V != 0F)
            {
                output.WriteRawTag(61);
                output.WriteFloat(V);
            }
            if (H != 0F)
            {
                output.WriteRawTag(69);
                output.WriteFloat(H);
            }
            if (RpcId != 0)
            {
                output.WriteRawTag(208, 5);
                output.WriteInt32(RpcId);
            }
            if (ActorId != 0L)
            {
                output.WriteRawTag(232, 5);
                output.WriteInt64(ActorId);
            }
            if (Id != 0L)
            {
                output.WriteRawTag(240, 5);
                output.WriteInt64(Id);
            }
            if (KeyType != 0)
            {
                output.WriteRawTag(248, 5);
                output.WriteInt32(KeyType);
            }
        }

        public int CalculateSize()
        {
            int size = 0;
            if (RpcId != 0)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt32Size(RpcId);
            }
            if (ActorId != 0L)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt64Size(ActorId);
            }
            if (Id != 0L)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt64Size(Id);
            }
            if (KeyType != 0)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt32Size(KeyType);
            }
            if (X != 0F)
            {
                size += 1 + 4;
            }
            if (Y != 0F)
            {
                size += 1 + 4;
            }
            if (Z != 0F)
            {
                size += 1 + 4;
            }
            if (KeyOne != 0)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt32Size(KeyOne);
            }
            if (KeyTwo != 0)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt32Size(KeyTwo);
            }
            if (KeyThree != 0)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt32Size(KeyThree);
            }
            if (V != 0F)
            {
                size += 1 + 4;
            }
            if (H != 0F)
            {
                size += 1 + 4;
            }
            return size;
        }

        public void MergeFrom(pb::CodedInputStream input)
        {
            x_ = 0;
            y_ = 0;
            z_ = 0;
            ax_ = 0;
            ay_ = 0;
            az_ = 0;
            v_ = 0f;
            h_ = 0f;
            rpcId_ = 0;
            actorId_ = 0;
            id_ = 0;
            keyType_ = 0;
            uint tag;
            while ((tag = input.ReadTag()) != 0)
            {
                switch (tag)
                {
                    default:
                        input.SkipLastField();
                        break;
                    case 13:
                        {
                            X = input.ReadFloat();
                            break;
                        }
                    case 21:
                        {
                            Y = input.ReadFloat();
                            break;
                        }
                    case 29:
                        {
                            Z = input.ReadFloat();
                            break;
                        }
                    case 37:
                        {
                            KeyOne = input.ReadInt32();
                            break;
                        }
                    case 45:
                        {
                            KeyTwo = input.ReadInt32();
                            break;
                        }
                    case 53:
                        {
                            KeyThree = input.ReadInt32();
                            break;
                        }
                    case 61:
                        {
                            V = input.ReadFloat();
                            break;
                        }
                    case 69:
                        {
                            H = input.ReadFloat();
                            break;
                        }
                    case 720:
                        {
                            RpcId = input.ReadInt32();
                            break;
                        }
                    case 744:
                        {
                            ActorId = input.ReadInt64();
                            break;
                        }
                    case 752:
                        {
                            Id = input.ReadInt64();
                            break;
                        }
                    case 760:
                        {
                            KeyType = input.ReadInt32();
                            break;
                        }
                }
            }
        }
    }
    public partial class M2C_ServerPathResult : pb::IMessage
    {
        private static readonly pb::MessageParser<M2C_ServerPathResult> _parser = new pb::MessageParser<M2C_ServerPathResult>(() => (M2C_ServerPathResult)MessagePool.Instance.Fetch(typeof(M2C_ServerPathResult)));
        public static pb::MessageParser<M2C_ServerPathResult> Parser { get { return _parser; } }

        private long actorId_;
        public long ActorId
        {
            get { return actorId_; }
            set
            {
                actorId_ = value;
            }
        }

        private long id_;
        public long Id
        {
            get { return id_; }
            set
            {
                id_ = value;
            }
        }

        private float x_;
        public float X
        {
            get { return x_; }
            set
            {
                x_ = value;
            }
        }

        private float y_;
        public float Y
        {
            get { return y_; }
            set
            {
                y_ = value;
            }
        }

        private float z_;
        public float Z
        {
            get { return z_; }
            set
            {
                z_ = value;
            }
        }

        private float w_;
        public float W
        {
            get { return w_; }
            set
            {
                w_ = value;
            }
        }

        private static readonly pb::FieldCodec<float> _repeated_xs_codec
            = pb::FieldCodec.ForFloat(142);
        private pbc::RepeatedField<float> xs_ = new pbc::RepeatedField<float>();
        public pbc::RepeatedField<float> Xs
        {
            get { return xs_; }
            set { xs_ = value; }
        }

        private static readonly pb::FieldCodec<float> _repeated_ys_codec
            = pb::FieldCodec.ForFloat(150);
        private pbc::RepeatedField<float> ys_ = new pbc::RepeatedField<float>();
        public pbc::RepeatedField<float> Ys
        {
            get { return ys_; }
            set { ys_ = value; }
        }

        private static readonly pb::FieldCodec<float> _repeated_zs_codec
            = pb::FieldCodec.ForFloat(158);
        private pbc::RepeatedField<float> zs_ = new pbc::RepeatedField<float>();
        public pbc::RepeatedField<float> Zs
        {
            get { return zs_; }
            set { zs_ = value; }
        }

        public void WriteTo(pb::CodedOutputStream output)
        {
            if (Id != 0L)
            {
                output.WriteRawTag(8);
                output.WriteInt64(Id);
            }
            if (X != 0F)
            {
                output.WriteRawTag(21);
                output.WriteFloat(X);
            }
            if (Y != 0F)
            {
                output.WriteRawTag(29);
                output.WriteFloat(Y);
            }
            if (Z != 0F)
            {
                output.WriteRawTag(37);
                output.WriteFloat(Z);
            }
            if (W != 0F)
            {
                output.WriteRawTag(45);
                output.WriteFloat(W);
            }
            xs_.WriteTo(output, _repeated_xs_codec);
            ys_.WriteTo(output, _repeated_ys_codec);
            zs_.WriteTo(output, _repeated_zs_codec);
            if (ActorId != 0L)
            {
                output.WriteRawTag(232, 5);
                output.WriteInt64(ActorId);
            }
        }

        public int CalculateSize()
        {
            int size = 0;
            if (ActorId != 0L)
            {
                size += 2 + pb::CodedOutputStream.ComputeInt64Size(ActorId);
            }
            if (Id != 0L)
            {
                size += 1 + pb::CodedOutputStream.ComputeInt64Size(Id);
            }
            if (X != 0F)
            {
                size += 1 + 4;
            }
            if (Y != 0F)
            {
                size += 1 + 4;
            }
            if (Z != 0F)
            {
                size += 1 + 4;
            }
            if (W != 0F)
            {
                size += 1 + 4;
            }
            size += xs_.CalculateSize(_repeated_xs_codec);
            size += ys_.CalculateSize(_repeated_ys_codec);
            size += zs_.CalculateSize(_repeated_zs_codec);
            return size;
        }

        public void MergeFrom(pb::CodedInputStream input)
        {
            id_ = 0;
            x_ = 0f;
            y_ = 0f;
            z_ = 0f;
            w_ = 0f;
            xs_.Clear();
            ys_.Clear();
            zs_.Clear();
            actorId_ = 0;
            uint tag;
            while ((tag = input.ReadTag()) != 0)
            {
                switch (tag)
                {
                    default:
                        input.SkipLastField();
                        break;
                    case 8:
                        {
                            Id = input.ReadInt64();
                            break;
                        }
                    case 21:
                        {
                            X = input.ReadFloat();
                            break;
                        }
                    case 29:
                        {
                            Y = input.ReadFloat();
                            break;
                        }
                    case 37:
                        {
                            Z = input.ReadFloat();
                            break;
                        }
                    case 45:
                        {
                            W = input.ReadFloat();
                            break;
                        }
                    case 142:
                    case 145:
                        {
                            xs_.AddEntriesFrom(input, _repeated_xs_codec);
                            break;
                        }
                    case 150:
                    case 153:
                        {
                            ys_.AddEntriesFrom(input, _repeated_ys_codec);
                            break;
                        }
                    case 158:
                    case 161:
                        {
                            zs_.AddEntriesFrom(input, _repeated_zs_codec);
                            break;
                        }
                    case 744:
                        {
                            ActorId = input.ReadInt64();
                            break;
                        }
                }
            }
        }

    }



    #endregion





}
