using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_IS.ViewModel.Service
{
    class StorageService
    {
        private OracleConnection _conn;

        public StorageService()
        {
            this.SetConnection();
        }

        public DataTable GetStorageData()
        {
            try
            {
                _conn.Open();
                string sql = @"select id_med as ID, nazov as Názov, typ as Typ, vyrobca as Výrobca, sum(pocet) as Počet
                                    from 
                                        (
                                        select id_med, NVL(SUBSTR(med.NAME, 0, INSTR(med.NAME, '-')-1), med.NAME) as nazov,
                                            med.TYPE.type as typ, med.DESCRIPTION, man.name as vyrobca, st.quantity as pocet
                                        from NOVAKOVA25.MEDICAMENT med join NOVAKOVA25.MANUFACTURER man using(id_man)
                                        join  NOVAKOVA25.storage st using(id_med)
                                        order by 1
                                        )
                                    group by nazov, typ, vyrobca, id_med
                                    order by 1";
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

