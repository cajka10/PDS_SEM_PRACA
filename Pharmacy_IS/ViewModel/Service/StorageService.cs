using Oracle.ManagedDataAccess.Client;
using Pharmacy_IS.Model.Entities;
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
                string sql = @"select id_storage as Id, id_med, nazov as Názov, typ as Typ, vyrobca as Výrobca, sum(pocet) as Počet
                                    from 
                                        (
                                        select id_storage, id_med, NVL(SUBSTR(med.NAME, 0, INSTR(med.NAME, '-')-1), med.NAME) as nazov,
                                            med.TYPE.type as typ, med.DESCRIPTION, man.name as vyrobca, st.quantity as pocet
                                        from NOVAKOVA25.MEDICAMENT med join NOVAKOVA25.MANUFACTURER man using(id_man)
                                        join  NOVAKOVA25.storage st using(id_med)
                                        order by 1
                                        )
                                    group by nazov, typ, vyrobca, id_med, id_storage
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

        internal StoredItem GetStoredItem(int id)
        {

            try
            {
                StoredItem outputItem = new StoredItem();
                _conn.Open();


                string sql = @"select stor.id_med, stor.quantity, stor.expiration_date
                            FROM storage stor
                            WHERE stor.id_storage = " + id;
                using (OracleCommand command = new OracleCommand(sql, _conn))
                {
                    OracleDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        outputItem.Id = id;
                        outputItem.MedicamentId = Convert.ToInt32(reader[0]);
                        outputItem.Quantity = Convert.ToInt32(reader[1]);
                        outputItem.ExpirationDate = Convert.ToDateTime(reader[2]);
                    }
                    return outputItem;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;

            }
            finally
            {
                _conn.Close();
            }
        }

        internal void UpdateStoredItem(StoredItem storedItem)
        {
            storedItem.MedicamentId = 45005;
            //storedItem.Quantity = 666;
            try
            {
                int temp = 0;
                _conn.Open();
                OracleCommand command = new OracleCommand("usp_update_storage_item", _conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("PARAM0", OracleDbType.Int32).Value = storedItem.Id;
                command.Parameters.Add("PARAM1", OracleDbType.Date).Value = storedItem.ExpirationDate;
                command.Parameters.Add("PARAM2", OracleDbType.Int32).Value = storedItem.Quantity;
                command.Parameters.Add("PARAM3", OracleDbType.Int16).Value = temp;

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

        internal void InsertStoredItem(StoredItem storedItem)
        {
            //storedItem.MedicamentId = 45005;
            //storedItem.Quantity = 666;
            try
            {
                int temp = 0;
                _conn.Open();
                OracleCommand command = new OracleCommand("usp_insert_storage_item", _conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("PARAM1", OracleDbType.Int32).Value = storedItem.MedicamentId;
                command.Parameters.Add("PARAM2", OracleDbType.Int32).Value = storedItem.Quantity;
                command.Parameters.Add("PARAM3", OracleDbType.Date).Value = storedItem.ExpirationDate;
                command.Parameters.Add("PARAM4", OracleDbType.Int16).Value = temp;

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

