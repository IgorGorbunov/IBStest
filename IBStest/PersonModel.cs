using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBStest
{
    /// <summary>
    /// Класс с данными о личности
    /// </summary>
    public class PersonModel : IDataErrorInfo
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Страна
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Аська
        /// </summary>
        public string Icq { get; set; }

        /// <summary>
        /// Любимые блюда
        /// </summary>
        public List<FavoriteDish> Dishes { get; set; }

        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public PersonModel()
        {
            Dishes = new List <FavoriteDish>();
        }

        /// <summary>
        /// Конструктор для задания параметров личности
        /// </summary>
        /// <param name="surname">Фамилия личности</param>
        /// <param name="name">Имя личности</param>
        /// <param name="country">Страна проживания личности</param>
        /// <param name="icq">Аська личности</param>
        /// <param name="dishes">Любимые блюда личности</param>
        public PersonModel(string surname, string name, string country, string icq, List<FavoriteDish> dishes)
        {
            Surname = surname;
            Name = name;
            Country = country;
            Icq = icq;
            Dishes = dishes;
        }

        /// <summary>
        /// Метод, возвращающий все параметры в одной строке
        /// </summary>
        /// <returns></returns>
        public string GetAllParametrs()
        {
            string paramss = string.Format("{0} {1} {2} {3} ", Surname, Name, Country, Icq);
            foreach (FavoriteDish dish in Dishes)
            {
                paramss += string.Format("{0} ", dish.Name);
            }
            return paramss;
        }

        /// <summary>
        /// Метод, проверяющий корректность номера аськи
        /// </summary>
        /// <param name="icqVal">Номер аськи</param>
        /// <param name="error">Сообщение об ошибке</param>
        /// <returns></returns>
        public static bool IsCorrectIcq(string icqVal, out string error)
        {
            error = "Номер ICQ должен быть больше 0 и состоять максимум из 9ти знаков";
            int icqV;
            try
            {
                if (string.IsNullOrEmpty(icqVal))
                {
                    error = string.Empty;
                    return true;
                }
                icqV = int.Parse(icqVal);
            }
            catch (FormatException)
            {
                return false;
            }
            catch (OverflowException)
            {
                return false;
            }

            if (icqVal.Length > 9 || icqVal.Length <= 0)
            {
                return false;
            }
            try
            {
                icqV = int.Parse(icqVal);
            }
            catch (FormatException)
            {
                return false;
            }
            catch (OverflowException)
            {
                return false;
            }

            error = string.Empty;
            return true;
        }
        /// <summary>
        /// Метод для проверки значений параметров личности после ввода
        /// </summary>
        /// <param name="columnName">Параметр личности</param>
        /// <returns></returns>
        public string this[string columnName]
        {
            get
            {
                string validError = String.Empty;
                switch (columnName)
                {
                    case "Icq":
                        if (IsCorrectIcq(Icq, out validError))
                        {
                            validError = String.Empty;
                        }
                        break;
                }
                return validError;
            }
        }
        /// <summary>
        /// Метод, возвращающий ошибку проверки
        /// </summary>
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}
