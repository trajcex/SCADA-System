using CoreService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CoreService
{
    [ServiceContract]
    public interface ITagService
    {
        [OperationContract]
        void SaveTags();
        [OperationContract]
        List<Tag> GetTags();
        [OperationContract]
        bool AddTag(Tag tag);
        [OperationContract]
        bool DeleteTag(String tagName);
        [OperationContract]
        string GetAllTagNames();
        [OperationContract]
        string GetOutputTags();
        [OperationContract]
        List<Tag> GetAllOutput();

        [OperationContract]
        void ChangeOutputTag(string tagName, int value, string type);
    }
}
