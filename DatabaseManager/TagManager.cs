﻿using System;
using DatabaseManager.TagServiceReference;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            return new DigitalInputTag
            {
                TagName = tagName,
                Description = description,
                Address = address,
                ScanTime = scanTime,
                Driver = "Driver"
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

            return new AnalogInputTag
            {
                TagName = tagName,
                Description = description,
                Address = address,
                ScanTime = scanTime,
                LowLimit = lowLimit,
                HighLimit = highLimit
            };
        }
        static Tag createAnalogOutput(string tagName, string description, string address)
        {
            int initialValue = integerValidator("Unesite InitialValue (integer > 0):");
            int lowLimit = integerValidator("Unesite LowLimit (integer > 0):");
            int highLimit = integerValidator("Unesite HighLimit (integer > 0):");

            return new AnalogOutputTag
            {
                TagName = tagName,
                Description = description,
                Address = address,
                InitialValue = initialValue,
                LowLimit = lowLimit,
                HighLimit = highLimit
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
    }
}
