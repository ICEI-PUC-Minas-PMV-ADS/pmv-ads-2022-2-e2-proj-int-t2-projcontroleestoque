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
    }
}
