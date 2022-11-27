using Newtonsoft.Json;
using ProjControleEstoque.Models;
using System.Globalization;
using System.Text.Json.Nodes;

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

        public static AgendaInventario? getAgendaInventario(AgendaInventario[]? agendaInventario)
        {
            if (agendaInventario != null)
            {
                foreach (var r in agendaInventario)
                {
                    var notificar = false;

                    switch (r.Tipo)
                    {
                        case "periodico_semanal":
                            notificar = verificarAgendamentoInvetario_semanal(r);
                            break;

                        case "periodico_mensal":
                            notificar = verificarAgendamentoInvetario_mensal(r);
                            break;

                        case "extraordinario":
                            notificar = verificarAgendamentoInvetario_extraordinario(r);
                            break;
                    }

                    if (notificar)
                    {
                        return r;
                    }
                }
            }
            return null;
        }

        public static bool verificarAgendamentoInvetario(AgendaInventario[]? agendaInventario)
        {
            if (agendaInventario != null)
            {
                foreach (var r in agendaInventario)
                {
                    var notificar = false;

                    switch (r.Tipo)
                    {
                        case "periodico_semanal":
                            notificar = verificarAgendamentoInvetario_semanal(r);
                            break;

                        case "periodico_mensal":
                            notificar = verificarAgendamentoInvetario_mensal(r);
                            break;

                        case "extraordinario":
                            notificar = verificarAgendamentoInvetario_extraordinario(r);
                            break;
                    }

                    if (notificar)
                    {
                        return notificar;
                    }
                }
            }
            return false;
        }

        public static bool verificarAgendamentoInvetario_semanal(AgendaInventario agendaInventario)
        {
            var dayOfWeek = DateTime.Today.DayOfWeek;

            var agendamento = JsonConvert.DeserializeObject<Dictionary<string, string>>(agendaInventario.Agendamento ?? "");

            var value = agendamento["value"];

            switch (value)
            {
                case "Segunda-feira": return dayOfWeek == DayOfWeek.Monday;
                case "Terca-feira":   return dayOfWeek == DayOfWeek.Tuesday;
                case "Quarta-feira":  return dayOfWeek == DayOfWeek.Wednesday;
                case "Quinta-feira":  return dayOfWeek == DayOfWeek.Thursday;
                case "Sexta-feira":   return dayOfWeek == DayOfWeek.Friday;
                case "Sabado":        return dayOfWeek == DayOfWeek.Saturday;
                case "Domingo":       return dayOfWeek == DayOfWeek.Sunday;
            }
            return false;
        }

        public static bool verificarAgendamentoInvetario_mensal(AgendaInventario agendaInventario)
        {
            var todayDate = DateTime.Today.Day;

            var agendamento = JsonConvert.DeserializeObject<Dictionary<string, string>>(agendaInventario.Agendamento ?? "");

            var value = agendamento?["value"];
            var proximoDiaUtil = agendamento?["proximoDiaUtil"] == "true";

            if (proximoDiaUtil)
            {
                if (DateTime.Today.DayOfWeek == DayOfWeek.Sunday)
                {
                    return int.Parse(value) + 1 == todayDate;
                }
            }
            return int.Parse(value) == todayDate;
        }

        public static bool verificarAgendamentoInvetario_extraordinario(AgendaInventario agendaInventario)
        {
            var todayDate = DateTime.Today;

            var agendamento = JsonConvert.DeserializeObject<Dictionary<string, string>>(agendaInventario.Agendamento ?? "");
            var valueDate = DateTime.ParseExact(agendamento?["value"] ?? "", "yyyy-MM-dd", CultureInfo.InvariantCulture);

            return DateTime.Compare(todayDate, valueDate) == 0;
        }
    }
}
