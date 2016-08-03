using System;
using System.Data;
using PatronRepositorio.Interfaces;
using System.Data.SqlClient;

namespace PatronRepositorio.Implementaciones
{
    public class SqlBasedeDatos : BasedeDatos
    {
        //private SqlConnection otraConexion;
        public SqlBasedeDatos(string cadenaConexion): base(cadenaConexion)
        {
            this.conexionActiva = new SqlConnection(cadenaConexion);
            //otraConexion = new SqlConnection(cadenaConexion);
            this.conexionActiva.Open();
        }
        protected override void AsignarParametros(IDbCommand comando, Parametro[] parametros)
        {
            foreach (var parametro in parametros)
                comando.Parameters.Add(new SqlParameter(parametro.Nombre, parametro.Valor));
        }

        protected override IDbCommand ObtenerComando(string comandoSql)
        {
            if (string.IsNullOrEmpty(comandoSql)) throw new ArgumentException("El parametro comandoSql no puede ser nulo o vacio");

            return new SqlCommand(comandoSql, this.conexionActiva as SqlConnection);
        }

        //protected override void Dispose(bool eliminando)
        //{
        //    base.Dispose(eliminando);

        //    otraConexion.Dispose();

        //}
    }
}
