using CoreService.Repository.Interface;
using CoreService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SharedLibrary.Model;
using CoreService;

namespace CoreService
{
    public class TagService : ITagService
    {
        private static ITagRepository tagRepository = new TagRepository();
        private static List<Tag> digitalInputTag;
        private static List<Tag> digitalOutputTag;
        private static List<Tag> analogInputTag;
        private static List<Tag> analogOutputTag;
        private static TagProcessing tagProcessing;
        private static UserContextDB _contextDb = new UserContextDB();

        public TagService()
        {
            Dictionary<string, List<Tag>> map = tagRepository.GetTags();
            digitalInputTag = map.ContainsKey("DigitalInputTag") ? map["DigitalInputTag"] : new List<Tag>();
            digitalOutputTag = map.ContainsKey("DigitalOutputTag") ? map["DigitalOutputTag"] : new List<Tag>();
            analogInputTag = map.ContainsKey("AnalogInputTag") ? map["AnalogInputTag"] : new List<Tag>();
            analogOutputTag = map.ContainsKey("AnalogOutputTag") ? map["AnalogOutputTag"] : new List<Tag>();
            tagProcessing = new TagProcessing(digitalInputTag, analogInputTag);
        }
        public List<Tag> GetTags()
        {
            ITagRepository tagRepository = new TagRepository();
            return tagRepository.GetTags()["DigitalInputTag"];
        }
        public void SaveTags()
        {
            ITagRepository tagRepository = new TagRepository();

            Dictionary<string, List<Tag>> map = new Dictionary<string, List<Tag>>
            {
                { "DigitalInputTags", digitalInputTag },
                { "DigitalOutputTags", digitalOutputTag },
                { "AnalogInputTags", analogInputTag },
                { "AnalogOutputTags", analogOutputTag }
            };
            tagRepository.SaveTags(map);
        }
        public bool AddTag(Tag tag)
        {
            try
            {
                if (checkIfTagExist(tag.TagName)) return false;
                if (tag.GetType() == typeof(DigitalInputTag))
                    digitalInputTag.Add(tag);

                else if (tag.GetType() == typeof(DigitalOutputTag))
                    digitalOutputTag.Add(tag);

                else if (tag.GetType() == typeof(AnalogInputTag))
                    analogInputTag.Add(tag);

                else if (tag.GetType() == typeof(AnalogOutputTag))
                    digitalOutputTag.Add(tag);

                SaveTags();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool checkIfTagExist(String tagName)
        {
            if (digitalInputTag.Exists(t => t.TagName == tagName) || digitalOutputTag.Exists(t => t.TagName == tagName) ||
                analogInputTag.Exists(t => t.TagName == tagName) || analogOutputTag.Exists(t => t.TagName == tagName)) return true;
            return false;

        }
        public bool DeleteTag(String tagName)
        {
            try
            {
                
                var tagValuesToDelete = _contextDb.TagValues.Where(tv => tv.TagName == tagName).ToList();
                _contextDb.TagValues.RemoveRange(tagValuesToDelete);
                _contextDb.SaveChanges();

                digitalInputTag.RemoveAll(tag => tag.TagName.Equals(tagName));
                digitalOutputTag.RemoveAll(tag => tag.TagName.Equals(tagName));
                analogInputTag.RemoveAll(tag => tag.TagName.Equals(tagName));
                analogOutputTag.RemoveAll(tag => tag.TagName.Equals(tagName));
                SaveTags();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public string GetAllTagNames()
        {
            var allTags = digitalInputTag
                .Concat(digitalOutputTag)
                .Concat(analogInputTag)
                .Concat(analogOutputTag);

            string tagNames = string.Join("\n", allTags.Select((tag, index) => $"{index + 1}. {tag.TagName}"));

            return tagNames;
        }
        public Dictionary<string, bool> GetAllTagsAndScanStatus()
        {
            Dictionary<string,bool> tagAndScanPair = new Dictionary<string,bool>();

            foreach(DigitalInputTag tag in digitalInputTag)
            {
                tagAndScanPair.Add(tag.TagName, tag.Scan);
            }
            foreach(AnalogInputTag tag in analogInputTag)
            {
                tagAndScanPair.Add(tag.TagName, tag.Scan);
            }
            return tagAndScanPair;
        }
        public string GetOutputTags()
        {
            return ConvertTagsToString(GetAllOutput());
        }

        private string ConvertTagsToString(List<Tag> tags)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var tag in tags)
            {
                sb.AppendLine(tag.ToString());
            }

            return sb.ToString();
        }

        public bool CheckOutputTag(string tagName)
        {
            return digitalOutputTag.Any(t => t.TagName == tagName) || analogOutputTag.Any(t => t.TagName == tagName);
        }

        public List<Tag> GetAllOutput()
        {
            ITagRepository tagRepository = new TagRepository();
            List<Tag> outputTags = new List<Tag>();
            outputTags.AddRange(tagRepository.GetTags()["DigitalOutputTag"]);
            outputTags.AddRange(tagRepository.GetTags()["AnalogOutputTag"]);
            return outputTags;
        }

        public void ChangeOutputTag(string tagName, int value, string type)
        {

            if (type[0] == 'D')
            {
                DigitalOutputTag tagToUpdate = digitalOutputTag.FirstOrDefault(t => t.TagName == tagName) as DigitalOutputTag;
                tagToUpdate.InitialValue = value;
            }
            else
            {
                AnalogOutputTag tagToUpdate = analogOutputTag.FirstOrDefault(t => t.TagName == tagName) as AnalogOutputTag;
                tagToUpdate.InitialValue = value;
            }
            SaveTags();
        }

        public void StartTag(string tagName)
        {
            Tag tag;
            tag = digitalInputTag.Find(t => t.TagName == tagName);
            if (tag == null)
            {
                
                tag = analogInputTag.Find(t => t.TagName == tagName);
                if (tag != null)
                {
                    AnalogInputTag ai = (AnalogInputTag)tag;
                    ai.Scan = true;
                    tagProcessing.StartTag(tag);
                }
            } 
            else
            {
                DigitalInputTag di = (DigitalInputTag)tag;
                di.Scan = true;
                tagProcessing.StartTag(tag);
            }
             
            SaveTags();
        }

        public void StopTag(string tagName)
        {
            Tag tag;
            tag = digitalInputTag.Find(t => t.TagName == tagName);
            if (tag == null)
            {
                tag = analogInputTag.Find(t => t.TagName == tagName);
                AnalogInputTag ai = (AnalogInputTag)tag;
                ai.Scan = false;
            }
            else
            {
                DigitalInputTag di = (DigitalInputTag)tag;
                di.Scan = false;
            }
            tagProcessing.StopTag(tagName);
            SaveTags();
        }
    }
}
