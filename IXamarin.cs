using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WebPractice
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IXamarin" in both code and config file together.
    [ServiceContract]
    public interface IXamarin
    {
        [OperationContract]
        [WebGet(UriTemplate = "CheckLogin/{Email,Password}", ResponseFormat = WebMessageFormat.Json)]
        string UserLogin(string uid, string pass);
    }
}
