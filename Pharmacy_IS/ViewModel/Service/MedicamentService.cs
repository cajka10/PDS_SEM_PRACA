using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using Pharmacy_IS.Model.Entities;

namespace Pharmacy_IS.ViewModel.Service
{
    class MedicamentService
    {
        private OracleConnection _conn;

        public MedicamentService()
        {
            this.SetConnection();
        }

        internal void InsertMedicament(Medicament medicament)
        {
            try
            {
                _conn.Open();
                OracleCommand command = new OracleCommand("p_insert_medicament", _conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("PARAM1", OracleDbType.Varchar2).Value = medicament.MedName;
                command.Parameters.Add("PARAM2", OracleDbType.Varchar2).Value = medicament.MedType;
                command.Parameters.Add("PARAM3", OracleDbType.Varchar2).Value = medicament.Description;
                command.Parameters.Add("PARAM4", OracleDbType.Int32).Value = medicament.Amount;
                command.Parameters.Add("PARAM5", OracleDbType.Varchar2).Value = medicament.ActiveIngredients;

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        internal DataTable GetMedicaments()
        {
            try
            {
                _conn.Open();
                string sql = @"select id_med as ID,  med.NAME as Nazov, med.TYPE.type as Typ, med.DESCRIPTION as Popis, man.name as Vyrobca
                            from NOVAKOVA25.MEDICAMENT med join NOVAKOVA25.MANUFACTURER man using (id_man)";
                using (OracleCommand command = new OracleCommand(sql, _conn))
                {
                    DataTable dt = new DataTable();
                    OracleDataAdapter da = new OracleDataAdapter(command);
                    da.Fill(dt);
                    return dt;
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                _conn.Close();
            }

        }

        private void SetConnection()
        {
            string connString = ConfigurationManager.ConnectionStrings["OrclConnectionString"].ConnectionString;
            try
            {
                _conn = new OracleConnection(connString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

    }
}
