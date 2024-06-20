using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using SharedLibrary.Model;
using DatabaseManager.TagServiceReference;

namespace DatabaseManager
{
    public class TagManager
    {
        private static TagServiceClient tagServiceClient = new TagServiceClient();
        public static void addTag()
        {
            Printer.TagTypes();
            int tagType = integerValidator("Unesite tip taga:", 1, 4);
            Tag newTag = createTag(tagType);
            tagServiceClient.AddTag(newTag);

        }
        public static void deleteTag()
        {
            Console.WriteLine("\nPostojeci tagovi u sistemu:");
            string tagsPrint = tagServiceClient.GetAllTagNames();
            string[] tagsWithNumbers = tagsPrint.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            int deleteTagOption = integerValidator(tagsPrint,1,tagsWithNumbers.Length);
            
            Dictionary<int, string> tags = tagsWithNumbers
            .Select(line => line.Split(new[] { '.' }, 2))
            .ToDictionary(
                parts => int.Parse(parts[0].Trim()),
                parts => parts[1].Trim()
            );
            tagServiceClient.DeleteTag(GetTagByNumber(tags, deleteTagOption));
        }
        static Tag createTag(int tipTaga)
        {
            string tagName = stringValidator("Unesite TagName:");
            string description = stringValidator("Unesite Description:");
            string address = stringValidator("Unesite Address:");

            switch (tipTaga)
            {
                case 1:
                    return createDigitalInput(tagName, description, address);
                case 2:
                    return createDigitalOutput(tagName, description, address);
                case 3:
                    return createAnalogInput(tagName, description, address);
                case 4:
                    return createAnalogOutput(tagName, description, address);
                default:
                    return null;
            }
        }
        static Tag createDigitalInput(string tagName, string description, string address)
        {
            int scanTime = integerValidator("Unesite ScanTime (integer > 0):");
            string driver = getDriverFromCLI();

            return new DigitalInputTag
            {
                TagName = tagName,
                Description = description,
                Address = address,
                ScanTime = scanTime,
                Driver = driver,
                Scan = true
            };
        }
        static Tag createDigitalOutput(string tagName, string description, string address)
        {
            int initialValue = integerValidator("Unesite InitialValue (0 ili 1):", 0, 1);

            return new DigitalOutputTag
            {
                TagName = tagName,
                Description = description,
                Address = address,
                InitialValue = initialValue
            };
        }
        static Tag createAnalogInput(string tagName, string description, string address)
        {
            int scanTime = integerValidator("Unesite ScanTime (integer > 0):");
            int lowLimit = integerValidator("Unesite LowLimit (integer > 0):");
            int highLimit = integerValidator("Unesite HighLimit (integer > 0):");
            string units = stringValidator("Unesite Units:");
            string driver = getDriverFromCLI();
            List<Alarm> alarms = new List<Alarm>();
            while (true)
            {
                int addAlarmOption = integerValidator("Da li zelite kreirati Alarm\n1. Da\n2. Ne", 1, 2);
                if (addAlarmOption != 1) break;
                int alarmTypeOption = integerValidator("Unesite tip alarma\n1. Low\n2. High", 1, 2);

                AlarmType alarmType = AlarmType.LOW;
                if(alarmTypeOption == 2) alarmType = AlarmType.HIGH;

                int priorityAlarmOption = integerValidator("\nUnesite prioritet 1,2 ili 3:", 1, 3);
                int priorityAlarm;
                switch(priorityAlarmOption)
                {
                    case 1:
                        priorityAlarm = 1;
                        break;
                    case 2:
                        priorityAlarm = 2;
                        break;
                    case 3:
                        priorityAlarm = 3;
                        break;
                    default:
                        return null;
                }
                int border = integerValidator("Unesite granicu paljenja intervala:", lowLimit, highLimit);

                alarms.Add(new Alarm
                {
                    Id = Guid.NewGuid().ToString(),
                    Type = alarmType,
                    Priority = priorityAlarm,
                    Border = border
                });

            }
            foreach(Alarm alarm in alarms)
            {
                Console.Write("alarm", alarm.Type.ToString());
            }

            return new AnalogInputTag
            {
                TagName = tagName,
                Description = description,
                Address = address,
                ScanTime = scanTime,
                LowLimit = lowLimit,
                HighLimit = highLimit,
                Scan = true,
                Units = units,
                Driver = driver,
                Alarms = alarms
            };
        }
        static Tag createAnalogOutput(string tagName, string description, string address)
        {
            int initialValue = integerValidator("Unesite InitialValue (integer > 0):");
            int lowLimit = integerValidator("Unesite LowLimit (integer > 0):");
            int highLimit = integerValidator("Unesite HighLimit (integer > 0):");
            string units = stringValidator("Unesite Units:");

            while (true)
            {
                int addAlarmOption = integerValidator("\nAdd alarms?\n1. Yes\n2. No", 1, 2);
                if (addAlarmOption != 1) break;
                int alarmType = integerValidator("Set alarm Type\n1. Low\n2. High", 1, 2);
                
            }


            return new AnalogOutputTag
            {
                TagName = tagName,
                Description = description,
                Address = address,
                InitialValue = initialValue,
                LowLimit = lowLimit,
                HighLimit = highLimit,
                Units = units
            };
        }
        static int integerValidator(string prompt, int minValue = 1, int? maxValue = null)
        {
            int input;
            bool validInput;
            do
            {
                Console.WriteLine(prompt);
                validInput = int.TryParse(Console.ReadLine(), out input) && input >= minValue && (!maxValue.HasValue || input <= maxValue.Value);
                if (!validInput)
                {
                    Console.WriteLine($"Unos mora biti broj između {minValue}{(maxValue.HasValue ? $" i {maxValue}" : "")}. Pokušajte ponovo.");
                }
            } while (!validInput);

            return input;
        }
        static string stringValidator(string prompt)
        {
            string input;
            do
            {
                Console.WriteLine(prompt);
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Unos ne sme biti prazan. Pokušajte ponovo.");
                }
            } while (string.IsNullOrWhiteSpace(input));

            return input;
        }
        static string getDriverFromCLI()
        {
            string driverRet;
            int driverOption = integerValidator("1. Real time unit\n2. Simulation driver\n>>", 1, 2);
            switch (driverOption)
            {
                case 1:
                    driverRet = "RTU";
                    break;
                case 2:
                    driverRet = "Simulation";
                    break;
                default:
                    return null;
            }
            return driverRet;
        }
        static string GetTagByNumber(Dictionary<int, string> objectMap, int number)
        {
            if (objectMap.TryGetValue(number, out var obj))
            {
                return obj;
            }
            else
            {
                return $"Object with number {number} not found.";
            }
        }

        public static void GetOutputTags()
        {
            Console.WriteLine("\nTrenutne vrednosti izlaznih tagova:");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine(tagServiceClient.GetOutputTags());
        }

        public static void ChangeTag()
        {
            List<Tag> tags = new List<Tag>();
            tags.AddRange(tagServiceClient.GetAllOutput());
            if (tags.Count == 0)
            {
                Console.WriteLine("Ne postoji nijedan tag.");
                return;
            }

            Tag selectedTag = null;
            string tagname;

            do
            {
                GetOutputTags();
                Console.WriteLine("\n");
                tagname = stringValidator("Unesite TagName:");
                selectedTag = tags.FirstOrDefault(t => t.TagName == tagname);
                if (selectedTag == null)
                {
                    Console.WriteLine($"Tag '{tagname}' nije pronađen. Molimo vas da pokušate ponovo.");
                }
            } while (selectedTag == null);

            int value = 0;
            if (tagname[0] == 'D')
            {
                value = integerValidator("Unesite vrednost(0 ili 1):", 0, 1);
            }
            else
            {
                value = integerValidator("Unesite vrednost (integer > 0):");
            }

            tagServiceClient.ChangeOutputTag(tagname, value);
        }
    }
}
