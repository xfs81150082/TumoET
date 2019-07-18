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



}
