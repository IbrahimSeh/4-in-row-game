using System.Runtime.Serialization;

namespace WcfFourInRowServer
{
    [DataContract]
    public class UserIdNotExistFault
    {
        [DataMember]
        public string Message { get; set; }
    }
}