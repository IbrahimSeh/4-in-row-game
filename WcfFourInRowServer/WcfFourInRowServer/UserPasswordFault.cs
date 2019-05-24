using System.Runtime.Serialization;

namespace WcfFourInRowServer
{
    [DataContract]
    public class UserPasswordFault
    {
        [DataMember]
        public string Message { get; set; }
    }
}