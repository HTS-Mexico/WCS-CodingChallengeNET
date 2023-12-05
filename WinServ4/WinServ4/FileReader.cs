using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WinServ4
{
    internal class FileReader
    {
        private Dictionary<string, Dictionary<string, string>> iniData;

        public FileReader(string filePath)
        {
            iniData = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                Dictionary<string, string> currentSection = null;

                foreach (string line in lines)
                {
                    string trimmedLine = line.Trim();

                    if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]"))
                    {
                        string sectionName = trimmedLine.Substring(1, trimmedLine.Length - 2);
                        currentSection = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                        iniData[sectionName] = currentSection;
                    }
                    else if (currentSection != null && trimmedLine.Contains('='))
                    {
                        string[] parts = trimmedLine.Split('=');
                        string key = parts[0].Trim();
                        string value = parts[1].Trim();
                        currentSection[key] = value;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading INI file: {ex.Message}");
            }
        }

        public string GetValue(string section, string key, string defaultValue = null)
        {
            if (iniData.ContainsKey(section) && iniData[section].ContainsKey(key))
            {
                return iniData[section][key];
            }

            return defaultValue;
        }
    }
}
