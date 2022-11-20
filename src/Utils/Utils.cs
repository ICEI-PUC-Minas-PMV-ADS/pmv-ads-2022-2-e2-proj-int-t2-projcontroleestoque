using System.Globalization;

namespace ProjControleEstoque.Utils
{
    public class Utils
    {
        public static DateTime? strToDateTime(string? strDate)
        {
            if (strDate == null || strDate?.Length == 0)
            {
                return null;
            }
            return Convert.ToDateTime(strDate, new CultureInfo("pt-BR"));
        }

        public static DateTime? fixSearchStartDate(string date)
        {
            string[] dateArr = date.Split("-");
            return DateTime.ParseExact(string.Format("{2}/{1}/{0} 00:00:00", dateArr[0], dateArr[1], dateArr[2]), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        }

        public static DateTime? fixSearchEndDate(string date)
        {
            string[] dateArr = date.Split("-");
            return DateTime.ParseExact(string.Format("{2}/{1}/{0} 23:59:59", dateArr[0], dateArr[1], dateArr[2]), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        }
    }
}
