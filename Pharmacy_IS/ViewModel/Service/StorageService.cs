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


        internal Dictionary<int, string> getStorageMedicamentsNames()
        {
            Dictionary<int, string> output = new Dictionary<int, string>();
            try
            {
                _conn.Open();
                string sql = @"select distinct id_med, nazov as Názov
                                    from 
                                        (
                                        select id_storage, id_med, NVL(SUBSTR(med.NAME, 0, INSTR(med.NAME, '-')-1), med.NAME) as nazov,
                                            med.TYPE.type as typ, med.DESCRIPTION, man.name as vyrobca, st.quantity as pocet
                                        from NOVAKOVA25.MEDICAMENT med join NOVAKOVA25.MANUFACTURER man using(id_man)
                                        join  NOVAKOVA25.storage st using(id_med)
                                        where quantity > 0
                                        order by 1
                                        )
                                    group by nazov, typ, vyrobca, id_med, id_storage
                                    order by 1";
                using (OracleCommand command = new OracleCommand(sql, _conn))
                {
                    List<string> temp = new List<string>();
                    OracleDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        if (output.Count == 679)
                        {
                            Console.WriteLine();
                        }
                        output.Add(Convert.ToInt32(reader[0]), Convert.ToString(reader[1]));

                    }

                    return output;
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

        internal int getItemQuantity(int id)
        {

            int output = 0;
            try
            {
                _conn.Open();
                string sql = @"select sum(quantity) from NOVAKOVA25.storage
                               where id_med =" + id.ToString();


                using (OracleCommand command = new OracleCommand(sql, _conn))
                {
                    List<string> temp = new List<string>();
                    OracleDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        output = Convert.ToInt32(reader[0]);
                    }

                    return output;
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


                string sql = @"select stor.id_med, m.name, stor.quantity, stor.expiration_date
                            FROM NOVAKOVA25.storage stor
                            JOIN NOVAKOVA25.medicament m ON m.id_med = stor.id_med
                            WHERE stor.id_storage = " + id;
                using (OracleCommand command = new OracleCommand(sql, _conn))
                {
                    OracleDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
  
                        outputItem.Id = id;
                        outputItem.MedicamentId = Convert.ToInt32(reader[0]);
                        outputItem.Name = Convert.ToString(reader[1]);
                        outputItem.Quantity = Convert.ToInt32(reader[2]);
                        outputItem.ExpirationDate = Convert.ToDateTime(reader[3]);
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
            //storedItem.MedicamentId = 45005;
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

        internal void DeleteStoredItem(int StoredItemId)
        {
            //storedItem.MedicamentId = 45005;
            //storedItem.Quantity = 666;
            try
            {
                int status = 0;
                _conn.Open();
                OracleCommand command = new OracleCommand("usp_delete_storage_item", _conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("PARAM1", OracleDbType.Int32).Value = StoredItemId;
                command.Parameters.Add("out", OracleDbType.Int16).Value = ParameterDirection.Output;
                command.ExecuteNonQuery();
                status = int.Parse(command.Parameters["out"].Value.ToString());

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

        internal void SellStoredItem(Dictionary<int, int> orderList)
        {
            //storedItem.MedicamentId = 45005;
            //storedItem.Quantity = 666;
            try
            {
                int temp = 0;
                _conn.Open();
                OracleCommand command = new OracleCommand("usp_create_sale", _conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("PARAM1", OracleDbType.Int32).Value = ((LoggedUser)App.Current.Properties["LoggedUser"]).Id;
                DateTime localDate = DateTime.Now;
                command.Parameters.Add("PARAM2", OracleDbType.Date).Value = localDate;
                command.Parameters.Add("out", OracleDbType.Int16).Value = ParameterDirection.Output;
                command.ExecuteNonQuery();
                temp = int.Parse(command.Parameters["out"].Value.ToString());
                int result = 0;
                double totalPrice = 0;
                if(temp != 0)
                {
                    foreach (var item in orderList)
                    {
                        command = new OracleCommand("usp_create_sale_item", _conn);
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("PARAM1", OracleDbType.Int32).Value = item.Key;
                        command.Parameters.Add("PARAM2", OracleDbType.Int32).Value = temp;
                        command.Parameters.Add("PARAM3", OracleDbType.Int16).Value = null;
                        command.Parameters.Add("PARAM2", OracleDbType.Int32).Value = item.Value;
                        command.Parameters.Add("PARAM3", OracleDbType.Varchar2).Value ="";
                        command.Parameters.Add("out", OracleDbType.Int16).Value = ParameterDirection.Output;
                        command.ExecuteNonQuery();

                        result = int.Parse(command.Parameters["out"].Value.ToString());
                    }

                    if (result != 0)
                    {
                        command = new OracleCommand("usp_update_sale_price", _conn);
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("PARAM1", OracleDbType.Int32).Value = temp;
                        command.Parameters.Add("out", OracleDbType.Double).Value = ParameterDirection.Output;

                        command.ExecuteNonQuery();
                        string outPut = command.Parameters["out"].Value.ToString();
                        Console.WriteLine(outPut);
                        totalPrice = double.Parse(outPut);
                    }
                }



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

        public DataTable GetSaleHistory()
        {
            try
            {
                _conn.Open();
                string sql = @"select id_sale, p.name, price, sales_date
                                from sales_history join person p using (id_user)";
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

