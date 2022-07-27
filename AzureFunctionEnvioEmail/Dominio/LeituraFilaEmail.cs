using Infra;
using Infra.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FunctionEnvioEmail.Dominio
{
    public class LeituraFilaEmail : IDisposable
    {
        private readonly DbContextOptions<ContextoBase> _OptionsBuilder;

        public LeituraFilaEmail()
        {
            _OptionsBuilder = new DbContextOptions<ContextoBase>();
        }

        public void EnvioEmails()
        {
            var dadosEmail = new DadosEmail();

            using (var dataBase = new ContextoBase(_OptionsBuilder))
            {
                var encontrouEmail = false;

                var listaSolicitacoes = dataBase.Set<SolicitacaoEmail>()
                    .Where(s => !s.Enviado).AsNoTracking().ToList();


                foreach (var solicitacao in listaSolicitacoes)
                {
                    try
                    {
                        dadosEmail.EnviarEmail(solicitacao.Titulo, solicitacao.Mensagem,solicitacao.Destinatarios);
                        solicitacao.Enviado = true;

                        dataBase.SolicitacaoEmail.Update(solicitacao);
                        dataBase.SaveChanges();

                        encontrouEmail = true;

                    }
                    catch (Exception)
                    {

                    }

                }

                //if (encontrouEmail)
                //{
                //    var listaSolicitacoesDeletar = dataBase.Set<SolicitacaoEmail>()
                //    .Where(s => s.Enviado).AsNoTracking().ToList();

                //    if (listaSolicitacoesDeletar.Any())
                //    {
                //        dataBase.SolicitacaoEmail.RemoveRange(listaSolicitacoesDeletar);
                //        dataBase.SaveChanges();
                //    }
                //}

            }
        }



        #region Disposed https://docs.microsoft.com/pt-br/dotnet/standard/garbage-collection/implementing-dispose
      
        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            disposed = true;
        }
        #endregion


    }
}
