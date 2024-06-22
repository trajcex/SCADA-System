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

        public static void Init()
        {
            tagServiceClient.Init();
        }
        public static void addTag()
        {
            Printer.TagTypes();
            int tagType = integerValidator("Unesite tip taga:", 1, 4);
            Tag newTag = createTag(tagType);
            bool addingTagStatus = tagServiceClient.AddTag(newTag);
            if(addingTagStatus)
            {
                Console.WriteLine("\nUspesno ste dodali TAG -> " + newTag.TagName);
            }
            else
            {
                Console.WriteLine("\nDoslo je do greske prilikom dodavanja. Pokusajte ponovo.");
            }

        }
        public static void deleteTag()
        {
            Console.WriteLine("\nPostojeci tagovi u sistemu:");
            string tagsPrint = tagServiceClient.GetAllTagNames() + "\n0. Nazad";
            string[] tagsWithNumbers = tagsPrint.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            int deleteTagOption = integerValidatorWithBack(tagsPrint ,1,tagsWithNumbers.Length);
            if (deleteTagOption == -1) return;
            Dictionary<int, string> tags = tagsWithNumbers
            .Select(line => line.Split(new[] { '.' }, 2))
            .ToDictionary(
                parts => int.Parse(parts[0].Trim()),
                parts => parts[1].Trim()
            );
            string tagForDeleteName = GetTagByNumber(tags, deleteTagOption);
            bool deleteTagStatus =  tagServiceClient.DeleteTag(tagForDeleteName);
            if (deleteTagStatus)
            {
                Console.WriteLine("\nUspesno obrisan TAG -> " + tagForDeleteName);
            }else
            {
                Console.WriteLine("\nDoslo je do greske prilikom brisanja TAG-a -> " + tagForDeleteName);
            }
        }
        static Tag createTag(int tipTaga)
        {
            string tagName = stringValidator("Unesite TagName:");
            string description = stringValidator("Unesite Description:");

            switch (tipTaga)
            {
                case 1:
                    return createDigitalInput(tagName, description);
                case 2:
                    return createDigitalOutput(tagName, description);
                case 3:
                    return createAnalogInput(tagName, description);
                case 4:
                    return createAnalogOutput(tagName, description);
                default:
                    return null;
            }
        }
        static Tag createDigitalInput(string tagName, string description)
        {
            int scanTime = integerValidator("Unesite ScanTime (integer > 0):");
            string driver = getDriverFromCLI();
            string address = "S";

            if (driver.Equals("Simulation"))
            {
                int addressOption = integerValidator("1. Sinus\n2. Cosinus\n3. Ramp", 1, 3);
                if (addressOption == 2) address = "C";
                else if (addressOption == 3) address = "R";
            }
            else if (driver.Equals("RTU"))
            {
                address = stringValidator("Unesite Address:");
            }

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
        static Tag createDigitalOutput(string tagName, string description)
        {
            int initialValue = integerValidator("Unesite InitialValue (0 ili 1):", 0, 1);
            string address = stringValidator("Unesite Address:");

            return new DigitalOutputTag
            {
                TagName = tagName,
                Description = description,
                Address = address,
                InitialValue = initialValue
            };
        }
        static Tag createAnalogInput(string tagName, string description)
        {
            int scanTime = integerValidator("Unesite ScanTime (integer > 0):");
            int lowLimit = integerValidator("Unesite LowLimit (integer > 0):");
            int highLimit = integerValidator("Unesite HighLimit (integer > 0):",lowLimit);
            string units = stringValidator("Unesite Units:");
            string driver = getDriverFromCLI();
            string address = "S";

            if (driver.Equals("Simulation"))
            {
                int addressOption = integerValidator("\nUnesite vrednost za simulation funkciju\n1. Sinus\n2. Cosinus\n3. Ramp",1,3);
                if (addressOption == 2) address = "C";
                else if (addressOption == 3) address = "R";
            }else if (driver.Equals("RTU"))
            {
                address = stringValidator("Unesite Address:");
            }
            
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
        static Tag createAnalogOutput(string tagName, string description)
        {
            int initialValue = integerValidator("Unesite InitialValue (integer > 0):");
            int lowLimit = integerValidator("Unesite LowLimit (integer > 0):");
            int highLimit = integerValidator("Unesite HighLimit (integer > 0):", lowLimit);
            string units = stringValidator("Unesite Units:");
            string address = stringValidator("Unesite Address:"); 

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
                    Console.WriteLine($"Unos mora biti broj veci od {minValue}{(maxValue.HasValue ? $" i manji od {maxValue}" : "")}. Pokušajte ponovo.");
                }
            } while (!validInput);

            return input;
        }
        static int integerValidatorWithBack(string prompt, int minValue = 1, int? maxValue = null)
        {
            int input;
            bool validInput;
            bool validRange = true;
            do
            {
                Console.WriteLine(prompt);
                validInput = int.TryParse(Console.ReadLine(), out input);

                if (!validInput)
                {
                    Console.WriteLine($"Unos mora biti broj između {minValue}{(maxValue.HasValue ? $" i {maxValue}" : "")}. Pokušajte ponovo.");
                    continue;
                }
                if (input == 0) return -1;

                validRange = input >= minValue && (!maxValue.HasValue || input <= maxValue.Value);
                if (!(validInput && validRange))
                {
                    Console.WriteLine($"Unos mora biti broj između {minValue}{(maxValue.HasValue ? $" i {maxValue}" : "")}. Pokušajte ponovo.");
                }

            } while (!(validInput && validRange));

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
            int driverOption = integerValidator("\nUnesite vrednost za driver\n1. Real time unit\n2. Simulation driver\n>>", 1, 2);
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
            string outputType = "";
            Type type = selectedTag.GetType();

            if (type == typeof(DigitalOutputTag))
            {
                value = integerValidator("Unesite vrednost(0 ili 1):", 0, 1);
                outputType = "D";
            }
            else
            {
                value = integerValidator("Unesite vrednost (integer > 0):");
                outputType = "A";
            }

            tagServiceClient.ChangeOutputTag(tagname, value, outputType);
        }

        public static void ChangeTagScanStatus()
        {
            Dictionary<string, bool> tagScanPair = tagServiceClient.GetAllTagsAndScanStatus();
            int i = 0;
            Dictionary<int, string> tagOptionPair = new Dictionary<int, string>();
            Console.WriteLine("\nUnesite broj ispred TAG NAME-a ciji scan status zelite da promenite");
            foreach(var tag in tagScanPair.Keys)
            {
                i++;
                tagOptionPair.Add(i, tag);
                string scanValue;
                if (tagScanPair[tag]) scanValue = " ON";
                else scanValue = " OFF";
                Console.WriteLine(i.ToString() +". TAG NAME -> " +  tag +  "; SCAN STATUS -> " + scanValue);
            }
            int tagOption = integerValidatorWithBack("0. Vrati se nazad",1,i);
            if (tagOption == -1) return;

            if (tagScanPair[tagOptionPair[tagOption]]) tagServiceClient.StopTag(tagOptionPair[tagOption]);
            else tagServiceClient.StartTag(tagOptionPair[tagOption]);
        }

    }
}
