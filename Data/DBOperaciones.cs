using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MedToxQui.Data
{
    public class DBOperaciones
    {
        //consulta general sin parametros
        public IEnumerable<T> Get<T>(string sp)
        {
            IDbConnection cnn = null;
            var result = default(List<T>);
            try
            {
                cnn = new DBConnection().Open();
                result = cnn.Query<T>(sp, null, null, true, null, CommandType.StoredProcedure).ToList();
            }
            catch (Exception)
            {

            }
            finally
            {
                CloseConnection(cnn);
            }
            return result;
        }

        //consultando con un parametro enteros
        public IEnumerable<T> Getunparam<T>(string sp, int opcion)
        {
            IDbConnection cnn = null;
            var result = default(List<T>);
            try
            {
                cnn = new DBConnection().Open();
                result = cnn.Query<T>(sp, new { @opcion = opcion }, null, true, null, CommandType.StoredProcedure).ToList();
            }
            catch (Exception)
            {

            }
            finally
            {
                CloseConnection(cnn);
            }
            return result;
        }

        //consultando con dos parametros enteros
        public IEnumerable<T> Getdosparam<T>(string sp, int opcion, int id)
        {
            IDbConnection cnn = null;
            var result = default(List<T>);
            try
            {
                cnn = new DBConnection().Open();
                result = cnn.Query<T>(sp, new { @opcion = opcion, @id = id }, null, true, null, CommandType.StoredProcedure).ToList();
            }
            catch (Exception)
            {

            }
            finally
            {
                CloseConnection(cnn);
            }
            return result;
        }

        //para ejecutar procedimientos almacenados n parametros
        public IEnumerable<T> Getdosparam1<T>(string sp, object param)
        {
            IDbConnection cnn = null;
            var result = default(List<T>);
            try
            {
                cnn = new DBConnection().Open();
                result = cnn.Query<T>(sp, param, null, true, null, CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                CloseConnection(cnn);
            }
            return result;
        }

        //para ejecutar procedimientos almacenados con modelos
        public IEnumerable<T> Getdosparam2<T>(string sp, T param)
        {
            IDbConnection cnn = null;
            var result = default(List<T>);
            try
            {
                cnn = new DBConnection().Open();
                result = cnn.Query<T>(sp, param, null, true, null, CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                CloseConnection(cnn);
            }
            return result;
        }


        #region Reportes
        public IEnumerable<T> GetReportes<T>(string sp, int opcion)
        {
            IDbConnection cnn = null;
            var result = default(List<T>);
            try
            {
                cnn = new DBConnection().Open();
                result = cnn.Query<T>(sp, new { @opcion = opcion }, null, true, null, CommandType.StoredProcedure).ToList();
            }
            catch (Exception)
            {

            }
            finally
            {
                CloseConnection(cnn);
            }
            return result;
        }
        #endregion

        protected bool CloseConnection(IDbConnection cnn)
        {
            if (cnn != null)
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                    return true;
                }
            }
            return false;
        }
    }
}
