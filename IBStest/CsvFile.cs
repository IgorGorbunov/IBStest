using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBStest
{
    /// <summary>
    /// Класс для обработки CSV файлов
    /// </summary>
    public class CsvFile
    {
        private char delimetr = ';';
        private char quotes = '"';

        private string _fileName;

        /// <summary>
        /// Конструктор создания нового csv файла
        /// </summary>
        /// <param name="fileName">Путь к файлу</param>
        /// <param name="person">Личность</param>
        public CsvFile(string fileName, PersonModel person)
        {
            CreateFile(fileName, person);
        }

        /// <summary>
        /// Конструктор открытия существующего csv файла
        /// </summary>
        /// <param name="fileName">Путь к файлу</param>
        public CsvFile(string fileName)
        {
            _fileName = fileName;
        }

        /// <summary>
        /// Метод получения данных личности
        /// </summary>
        /// <returns></returns>
        public PersonModel GetPerson()
        {
            List <string> dataList = GetDataList(_fileName);
            string surname = dataList[0];
            string name = dataList[1];
            string country = dataList[2];
            string icq = dataList[3];
            List <FavoriteDish> dishes = new List <FavoriteDish>();
            for (int i = 4; i < dataList.Count; i++)
            {
                FavoriteDish dish = new FavoriteDish(dataList[i]);
                dishes.Add(dish);
            }
            return new PersonModel(surname, name, country, icq, dishes);
        }

        private List<string> GetDataList(string fileName)
        {
            List <string> list = new List <string>();
            using (StreamReader reader = new StreamReader(fileName, Encoding.UTF8))
            {
                string parametr = string.Empty;
                bool isParametr = false;

                while (!reader.EndOfStream)
                {
                    char c = (char)reader.Read();
                    if (c == quotes)
                    {
                        if (isParametr)
                        {
                            if (reader.EndOfStream)
                            {
                                list.Add(parametr);
                                break;
                            }
                            char c1 = (char)reader.Read();
                            if (c1 == delimetr)
                            {
                                list.Add(parametr);
                                isParametr = false;
                            }
                            else
                            {
                                parametr += c;
                            }
                        }
                        else
                        {
                            parametr = string.Empty;
                            isParametr = true;
                        }
                    }
                    else
                    {
                        parametr += c;
                    }
                }
                reader.Close();
            }
            return list;
        }

        private void CreateFile(string fileName, PersonModel person)
        {
            using (StreamWriter writer = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                writer.Write(GetRightString(person.Surname));
                writer.Write(delimetr);
                writer.Write(GetRightString(person.Name));
                writer.Write(delimetr);
                writer.Write(GetRightString(person.Country));
                writer.Write(delimetr);
                writer.Write(GetRightString(person.Icq));
                writer.Write(delimetr);

                foreach (FavoriteDish dish in person.Dishes)
                {
                    writer.Write(GetRightString(dish.Name));
                    writer.Write(delimetr);
                }

                writer.Close();
            }
        }

        private string GetRightString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "\"\"";
            }

            //защита от кавычек в параметре
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
