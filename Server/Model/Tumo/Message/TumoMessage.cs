using System;
using System.Collections.Generic;
using System.Text;
using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using scg = global::System.Collections.Generic;

namespace ETModel
{
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

        private int keyType_;
        public int KeyType
        {
            get { return keyType_; }
            set
            {
                keyType_ = value;
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

        private float ws_;
        public float WS
        {
            get { return ws_; }
            set
            {
                ws_ = value;
            }
        }

        private float ad_;
        public float AD
        {
            get { return ad_; }
            set
            {
                ad_ = value;
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
            if (WS != 0F)
            {
                output.WriteRawTag(37);
                output.WriteFloat(WS);
            }
            if (AD != 0F)
            {
                output.WriteRawTag(45);
                output.WriteFloat(AD);
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
            if (WS != 0F)
            {
                size += 1 + 4;
            }
            if (AD != 0F)
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
            ws_ = 0f;
            ad_ = 0f;
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
                            WS = input.ReadFloat();
                            break;
                        }
                    case 45:
                        {
                            AD = input.ReadFloat();
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

    public partial class Frame_KeyCodeMap : pb::IMessage
    {
        private static readonly pb::MessageParser<Frame_KeyCodeMap> _parser = new pb::MessageParser<Frame_KeyCodeMap>(() => (Frame_KeyCodeMap)MessagePool.Instance.Fetch(typeof(Frame_KeyCodeMap)));
        public static pb::MessageParser<Frame_KeyCodeMap> Parser { get { return _parser; } }

        private int rpcId_;
        public int RpcId
        {
            get { return rpcId_; }
            set
            {
                rpcId_ = value;
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

        private float ws_;
        public float WS
        {
            get { return ws_; }
            set
            {
                ws_ = value;
            }
        }

        private float ad_;
        public float AD
        {
            get { return ad_; }
            set
            {
                ad_ = value;
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
            if (WS != 0F)
            {
                output.WriteRawTag(37);
                output.WriteFloat(WS);
            }
            if (AD != 0F)
            {
                output.WriteRawTag(45);
                output.WriteFloat(AD);
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
            if (WS != 0F)
            {
                size += 1 + 4;
            }
            if (AD != 0F)
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
            ws_ = 0f;
            ad_ = 0f;
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
                            WS = input.ReadFloat();
                            break;
                        }
                    case 45:
                        {
                            AD = input.ReadFloat();
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



}
