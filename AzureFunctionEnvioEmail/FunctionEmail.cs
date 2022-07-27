using FunctionEnvioEmail.Dominio;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;

namespace FunctionEnvioEmail
{
    public static class FunctionEmail
    {
        //https://docs.microsoft.com/pt-br/azure/azure-functions/functions-bindings-timer?tabs=csharp
        //{second} {minute} {hour} {day} {month} {day-of-week}

        [FunctionName("FunctionEmail")]
        public static void Run([TimerTrigger("*/10 * * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"Ultima execução da functionEmail: {DateTime.Now}");

            using (var filaEmail = new LeituraFilaEmail())
            {
                filaEmail.EnvioEmails();
            }
        }
    }

}
