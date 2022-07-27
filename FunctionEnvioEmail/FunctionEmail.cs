using System;
using FunctionEnvioEmail.Dominio;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionEnvioEmail
{
    public static class FunctionEmail
    {
        //https://docs.microsoft.com/pt-br/azure/azure-functions/functions-bindings-timer?tabs=csharp
        //{second} {minute} {hour} {day} {month} {day-of-week}

        [Function("FunctionEmail")]
        public static void Run([TimerTrigger("0 */10 * * * *")] MyInfo myTimer, FunctionContext context)
        {
            var logger = context.GetLogger("Function1");
            logger.LogInformation($"Ultima execução da functionEmail: {DateTime.Now}");
            //logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");

            using (var filaEmail = new LeituraFilaEmail())
            {
                filaEmail.EnvioEmails();
            }
        }
    }

    public class MyInfo
    {
        public MyScheduleStatus ScheduleStatus { get; set; }

        public bool IsPastDue { get; set; }
    }

    public class MyScheduleStatus
    {
        public DateTime Last { get; set; }

        public DateTime Next { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
