using SharedLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Repository.Interface
{
    internal interface ITagRepository
    {
        Dictionary<string, List<Tag>> GetTags();
        void SaveTags(Dictionary<string, List<Tag>> tags);
    }
}
