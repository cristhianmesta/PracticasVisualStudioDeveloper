using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatronRepositorio.Interfaces
{
    public abstract class BasedeDatos : IDisposable
    {
        protected DbConnection conexionActiva;
        protected string cadenaConexion;

        protected BasedeDatos(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
            eliminado = false;
        }

        public void EjecutarComando(string comandoSql, Parametro[] parametros)
        {
            using (IDbCommand comando = ObtenerComando(comandoSql))
            {
                AsignarParametros(comando, parametros);
                comando.ExecuteNonQuery();
            }
        }

        public IDataReader EjecutarConsulta(string comandoSql, Parametro[] parametros)
        {
            using (IDbCommand comando = ObtenerComando(comandoSql))
            {
                AsignarParametros(comando, parametros);
                return comando.ExecuteReader();
            }
        }

        public object EjecutarEscalar(string comandoSql, Parametro[] parametros)
        {
            using (IDbCommand comando = ObtenerComando(comandoSql))
            {
                AsignarParametros(comando, parametros);
                return comando.ExecuteScalar();
            }
        }

        public object EjecutarComandoyEscalar(string comandoSql_Comando, string comandoSql_Escalar, Parametro[] parametros)
        {
            using (IDbCommand comando = ObtenerComando(comandoSql_Comando))
            {
                AsignarParametros(comando, parametros);
                comando.ExecuteNonQuery();
                comando.CommandText = comandoSql_Escalar;
                return comando.ExecuteScalar();
            }
        }

        protected abstract void AsignarParametros(IDbCommand comando, Parametro[] parametros);

        protected abstract IDbCommand ObtenerComando(string comandoSql);

        #region Implementacion de IDiposable
        private bool eliminado;

        public void Dispose()
        {
            //TODO se tiene implementar mejor
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool eliminando)
        {
            if (eliminado) return;
            if (eliminando)
            {
                // debo llamar a todos los metodos Dispose de 
                // los objetos internos que implementen IDisposable
                conexionActiva.Dispose();
            }
            eliminado = true;
        }

        ~BasedeDatos()
        {
            Dispose(false);
        }
        #endregion
    }
}
