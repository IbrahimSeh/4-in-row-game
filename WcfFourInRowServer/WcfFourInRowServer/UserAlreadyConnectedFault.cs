using System.Runtime.Serialization;

namespace WcfFourInRowServer
{
    [DataContract]
    public class UserAlreadyConnectedFault
    {
        [DataMember]
        public string Message { get; set; }
    }
}
