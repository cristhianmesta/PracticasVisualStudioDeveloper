using PatronRepositorio.Interfaces;
using System;
using System.Data;
//using System.Data.OracleClient;

namespace PatronRepositorio.Implementaciones
{/*
    public class OracleBasedeDatos : BasedeDatos
    {
        public OracleBasedeDatos(string cadenaConexion):base(cadenaConexion)
        {
            this.conexionActiva = new OracleConnection(cadenaConexion);
            this.conexionActiva.Open();
        }
        protected override void AsignarParametros(IDbCommand comando, Parametro[] parametros)
        {
            foreach (var parametro in parametros)
                comando.Parameters.Add(new OracleParameter(parametro.Nombre, parametro.Valor));
        }

        protected override IDbCommand ObtenerComando(string comandoSql)
        {
            return new OracleCommand(comandoSql, this.conexionActiva as OracleConnection);
        }
    }*/
}
