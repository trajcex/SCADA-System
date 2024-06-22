using CoreService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CoreService.Service
{
    public class ReportManagerService : IReportManagerService
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
                DateTime dateTime = ConvertFromMilliseconds(long.Parse(tag.Time));
                string formattedDate = dateTime.ToString("dd.MM.yyyy HH:mm:ss");
                sb.AppendLine($"Tag: {tag.TagName}, Vrednost: {tag.Value}, Vreme: {formattedDate}");
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
                DateTime dateTime = ConvertFromMilliseconds(long.Parse(tag.Time));
                string formattedDate = dateTime.ToString("dd.MM.yyyy HH:mm:ss");
                sb.AppendLine($"Tag: {tag.TagName}, Vrednost: {tag.Value}, Vreme: {formattedDate}");
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
                .Take(1000)
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

        public string GetTagValuesInPeriod(DateTime startTime, DateTime endTime)
        {
            long startMillis = ConvertToMilliseconds(startTime);
            long endMillis = ConvertToMilliseconds(endTime);

            var values = _contextDb.TagValues.ToList();
            var items = values
                .Where(tv => long.Parse(tv.Time) >= startMillis && long.Parse(tv.Time) <= endMillis)
                .OrderByDescending(tv => long.Parse(tv.Time))
                .Take(1000)
                .ToList();

            if (items.Count == 0)
            {
                return $"\nU periodu od {startTime:d} do {endTime:d}, nema dostupnih tagova.";
            }
            var result = new StringBuilder();
            result.AppendLine($"\nU periodu od {startTime:d} do {endTime:d}, ovo su vrednosti tagova:");

            int counter = 1;
            foreach (var val in items)
            {
                DateTime dateTime = ConvertFromMilliseconds(long.Parse(val.Time));
                string formattedDate = dateTime.ToString("dd.MM.yyyy HH:mm:ss");
                result.AppendLine($"{counter}. Tag: {val.TagName}, Vrednost: {val.Value}, Vreme: {formattedDate}");
                counter++;
            }

            return result.ToString();
        }

        private long ConvertToMilliseconds(DateTime dateTime)
        {
            DateTimeOffset dateTimeOffset = new DateTimeOffset(dateTime);
            return dateTimeOffset.ToUnixTimeMilliseconds();
        }

        private static DateTime ConvertFromMilliseconds(long milliseconds)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(milliseconds);
            return dateTimeOffset.DateTime;
        }

        public string GetAlarmsInPeriod(DateTime startTime, DateTime endTime)
        {
            var values = _contextDb.Alarms.ToList();
            var alarms = values
                .Where(a => a.DateTime >= startTime && a.DateTime <= endTime)
                .OrderByDescending(a => a.Priority)
                .ThenByDescending(a => a.DateTime)
                .Take(1000)
                .ToList();

            if (alarms.Count == 0)
            {
                return $"\nU periodu od {startTime:d} do {endTime:d}, nema dostupnih alarma.";
            }
            var result = new StringBuilder();
            result.AppendLine($"\nU periodu od {startTime:d} do {endTime:d}, se nije desio nijedan alarm.");

            int counter = 1;
            foreach (var val in alarms)
            {
                string formattedDate = val.DateTime.ToString("dd.MM.yyyy HH:mm:ss");
                result.AppendLine($"{counter}. AlarmId: {val.AlarmId}, Tip: {val.Type}, Vreme: {formattedDate}, Prioritet: {val.Priority}");
                counter++;
            }

            return result.ToString();
        }

        public string GetAlarmsByPriority(int priority)
        {
            var values = _contextDb.Alarms.ToList();
            var alarms = values
                .Where(a => a.Priority == priority)
                .OrderByDescending(a => a.DateTime)
                .ToList();

            if (alarms.Count == 0)
            {
                return $"\nNema dostupnih alarma sa prioritetom {priority}.";
            }

            var result = new StringBuilder();
            result.AppendLine($"\nAlarmi sa prioritetom {priority}:");

            int counter = 1;
            foreach (var alarm in alarms)
            {
                string formattedDate = alarm.DateTime.ToString("dd.MM.yyyy HH:mm:ss");
                result.AppendLine($"{counter}. AlarmId: {alarm.AlarmId}, Tip: {alarm.Type}, Vreme: {formattedDate}, Prioritet: {alarm.Priority}");
                counter++;
            }

            return result.ToString();
        }

        public List<string> GetAllPriorities()
        {
            var values = _contextDb.Alarms.ToList();
            var items = values
                .Select(a => a.Priority.ToString()) // Pretvaramo prioritet u string
                .Distinct() // Uklanjamo duplikate
                .OrderByDescending(p => int.Parse(p)) // Sortiramo po prioritetu u opadajućem redosledu
                .ToList();

            return items;
        }
    }
}
