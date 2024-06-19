using CoreService.Interface;
using CoreService.Model;
using CoreService.Repository;
using CoreService.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CoreService
{
    public class CoreService : ICore,IUser
    {
        public List<Tag> GetTags()
        {
            ITagRepository tagRepository = new TagRepository();
            return tagRepository.GetTags()["DigitalInputTag"];
        }

        public void RegisterUser(string username, string password)
        {
            using(var db = new UserContextDB())
            {
                User user = new User(username,password);
                db.Users.Add(user);
                db.SaveChanges();
                foreach (var item in db.Users)
                {
                    Console.WriteLine(item.Username);
                }
            }
        }

        public void SaveTags()
        {
            ITagRepository tagRepository = new TagRepository();
            List<Tag> di = new List<Tag>
            {
                new DigitalInputTag("DI1", "Digital Input Tag 1", "Address1", "Driver1", 1, true),
                new DigitalInputTag("DI2", "Digital Input Tag 2", "Address2", "Driver2", 1, false)
            };

                List<Tag> d0 = new List<Tag>
            {
                new DigitalOutputTag(100, "DO1", "Digital Output Tag 1", "Address1"),
                new DigitalOutputTag(200, "DO2", "Digital Output Tag 2", "Address2")
            };

                List<Tag> ai = new List<Tag>
            {
                new AnalogInputTag("AI1", "Analog Input Tag 1", "Address1", new List<Alarm>(), "Driver1", 1, true, "Volts", 0, 100),
                new AnalogInputTag("AI2", "Analog Input Tag 2", "Address2", new List<Alarm>(), "Driver2", 2, false, "Amps", 0, 50)
            };

                List<Tag> ao = new List<Tag>
            {
                new AnalogOutputTag("AO1", "Analog Output Tag 1", "Address1", 50, "Volts", 0, 100),
                new AnalogOutputTag("AO2", "Analog Output Tag 2", "Address2", 75, "Amps", 0, 50)
            };

            Dictionary<string, List<Tag>> map = new Dictionary<string, List<Tag>>();
            map.Add("DigitalInputTags", di);
            map.Add("DigitalOutputTags", d0);
            map.Add("AnalogInputTags", ai);
            map.Add("AnalogOutputTags", ao);
            tagRepository.SaveTags(map);
        }
    }
}
