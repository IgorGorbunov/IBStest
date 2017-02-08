using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBStest
{
    public class PersonModel : IDataErrorInfo
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Icq { get; set; }

        public List<FavoriteDish> Dishes { get; set; }

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
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}
