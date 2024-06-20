using CoreService.Repository.Interface;
using CoreService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SharedLibrary.Model;
using CoreService.Service;

namespace CoreService
{
    public class TagService : ITagService
    {
        private static ITagRepository tagRepository = new TagRepository();
        private static List<Tag> digitalInputTag;
        private static List<Tag> digitalOutputTag;
        private static List<Tag> analogInputTag;
        private static List<Tag> analogOutputTag;
        public TagService()
        {
            Dictionary<string, List<Tag>> map = tagRepository.GetTags();
            digitalInputTag = map.ContainsKey("DigitalInputTag") ? map["DigitalInputTag"] : new List<Tag>();
            digitalOutputTag = map.ContainsKey("DigitalOutputTag") ? map["DigitalOutputTag"] : new List<Tag>();
            analogInputTag = map.ContainsKey("AnalogInputTag") ? map["AnalogInputTag"] : new List<Tag>();
            analogOutputTag = map.ContainsKey("AnalogOutputTag") ? map["AnalogOutputTag"] : new List<Tag>();
            TagProcessing tagProcessing = new TagProcessing(digitalInputTag, analogInputTag);
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

        public void ChangeOutputTag(string tagName, int value)
        {
            if (tagName[0] == 'D')
            {
                DigitalOutputTag tagToUpdate = digitalOutputTag.FirstOrDefault(t => t.TagName == tagName) as DigitalOutputTag;
                //digitalOutputTag.Remove(tagToUpdate);
                tagToUpdate.InitialValue = value;
                //digitalOutputTag.Add(tagToUpdate);
            }
            else
            {
                AnalogOutputTag tagToUpdate = analogOutputTag.FirstOrDefault(t => t.TagName == tagName) as AnalogOutputTag;
                //analogOutputTag.Remove(tagToUpdate);
                tagToUpdate.InitialValue = value;
                //analogOutputTag.Add(tagToUpdate);
            }
            SaveTags();
        }   
    }
}
