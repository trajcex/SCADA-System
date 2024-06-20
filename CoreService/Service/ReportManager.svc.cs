using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CoreService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ReportManager" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ReportManager.svc or ReportManager.svc.cs at the Solution Explorer and start debugging.
    public class ReportManager : IReportManager
    {
        private static UserContextDB _contextDb = new UserContextDB();

        public string GetLastValueAi()
        {
            var items = _contextDb.TagValues.ToList();

            var lastAiValues = items
                .Where(tv => tv.Type == "AI")
                .GroupBy(tv => tv.TagName)
                .Select(g => g.OrderByDescending(tv => long.Parse(tv.Time)).First())
                .ToList();

            var sb = new StringBuilder();

            sb.AppendLine("\nPoslednja vrednost svih AI tagova:");
            foreach (var tag in lastAiValues)
            {
                sb.AppendLine($"Tag: {tag.TagName}, Vrednost: {tag.Value}, Vreme: {tag.Time}");
            }
            
            return sb.ToString();
        }

        public string GetLastValueDi()
        {
            var items = _contextDb.TagValues.ToList();

            var lastDiValues = items
                .Where(tv => tv.Type == "DI")
                .GroupBy(tv => tv.TagName)
                .Select(g => g.OrderByDescending(tv => long.Parse(tv.Time)).First())
                .ToList();
            var sb = new StringBuilder();

            sb.AppendLine("\nPoslednja vrednost svih DI tagova:");
            foreach (var tag in lastDiValues)
            {
                sb.AppendLine($"Tag: {tag.TagName}, Vrednost: {tag.Value}, Vreme: {tag.Time}");
            }

            return sb.ToString();
        }

        public string GetSortedValuesByTagName(string tagName)
        {
            var items = _contextDb.TagValues.ToList();
            var values = items
                .Where(tag => tag.TagName == tagName)
                .OrderBy(tag => tag.Value)
                .Select(tag => tag.Value)
                .Take(100)
                .ToList();

            var result = new StringBuilder();
            result.AppendLine($"\nIdentifikator: {tagName}");

            int counter = 1;
            foreach (var value in values)
            {
                result.AppendLine($"{counter}. {value}");
                counter++;
            }

            return result.ToString();
        }

        public List<string> GetAllTagNames()
        {
            var items = _contextDb.TagValues.ToList();
            return items
                .Select(tag => tag.TagName)
                .Distinct()
                .ToList();
        }
    }
}
