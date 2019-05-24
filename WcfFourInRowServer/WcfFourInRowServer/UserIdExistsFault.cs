using System.Runtime.Serialization;

namespace WcfFourInRowServer
{
    [DataContract]
    public class UserIdExistsFault
    {
        [DataMember]
        public string Message { get; set; }
    }
}