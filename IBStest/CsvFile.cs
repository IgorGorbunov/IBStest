using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBStest
{
    class CsvFile
    {
        private char delimetr = ';';
        private char quotes = '"';

        public CsvFile(string fileName, PersonModel person)
        {
            CreateFile(fileName, person);
        }

        private void CreateFile(string fileName, PersonModel person)
        {
            using (StreamWriter writer = new StreamWriter(fileName, false, Encoding.Unicode))
            {
                writer.Write(GetRightString(person.Surname));
                writer.Write(delimetr);
                writer.Write(GetRightString(person.Name));
                writer.Write(delimetr);
                writer.Write(GetRightString(person.Country));
                writer.Write(delimetr);
                writer.Write(GetRightString(person.Icq));
                writer.Write(delimetr);

                writer.Close();
            }
        }

        private string GetRightString(string value)
        {
            string newValue = string.Empty;
            foreach (char c in value)
            {
                if (c == quotes)
                {
                    newValue += c;
                }
                newValue += c;
            }
            return string.Format("\"{0}\"", newValue);
        }
    }
}
