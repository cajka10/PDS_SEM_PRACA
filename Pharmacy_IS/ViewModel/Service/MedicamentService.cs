using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
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

        internal void InsertMedicament(Medicament medicament, string path)
        {
            byte[] blob = new byte[0];
            if (path != null && !path.Equals(string.Empty))
            {
                blob = this.GetBlobFromPath(path);
            }

            try
            {
                int temp = 0;
                _conn.Open();
                OracleCommand command = new OracleCommand("usp_insert_medicament", _conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("PARAM1", OracleDbType.Varchar2).Value = medicament.ManufacturerId;
                command.Parameters.Add("PARAM2", OracleDbType.Varchar2).Value = medicament.MedName;
                command.Parameters.Add("PARAM3", OracleDbType.Char).Value = medicament.IsPrescribed ? '1' : '0';
                command.Parameters.Add("PARAM4", OracleDbType.Varchar2).Value = medicament.MedType;
                command.Parameters.Add("PARAM5", OracleDbType.Varchar2).Value = medicament.Amount;
                command.Parameters.Add("PARAM6", OracleDbType.Varchar2).Value = medicament.Description;
                command.Parameters.Add("PARAM7", OracleDbType.Varchar2).Value = medicament.ActiveIngredients;
                command.Parameters.Add("PARAM8", OracleDbType.Blob).Value = blob.Length != 0 ? blob : null;
                command.Parameters.Add("PARAM9", OracleDbType.Int16).Value = temp;

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

        private byte[] GetBlobFromPath(string path)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    byte[] imageData = new byte[fs.Length];

                    fs.Read(imageData, 0, System.Convert.ToInt32(fs.Length));

                    return imageData;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new byte[0];
        }

        internal void UpdateMedicament(Medicament medicament, string path)
        {
            byte[] blob = new byte[0];
            if (path != null && !path.Equals(string.Empty))
            {
                blob = this.GetBlobFromPath(path);
            }

            try
            {
                int temp = 0;
                _conn.Open();
                OracleCommand command = new OracleCommand("usp_update_medicament", _conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("PARAM1", OracleDbType.Int32).Value = medicament.Id;
                command.Parameters.Add("PARAM2", OracleDbType.Varchar2).Value = medicament.ManufacturerId;
                command.Parameters.Add("PARAM3", OracleDbType.Varchar2).Value = medicament.MedName;
                command.Parameters.Add("PARAM4", OracleDbType.Char).Value = medicament.IsPrescribed ? '1' : '0';
                command.Parameters.Add("PARAM5", OracleDbType.Varchar2).Value = medicament.MedType;
                command.Parameters.Add("PARAM6", OracleDbType.Varchar2).Value = medicament.Amount;
                command.Parameters.Add("PARAM7", OracleDbType.Varchar2).Value = medicament.Description;
                command.Parameters.Add("PARAM8", OracleDbType.Varchar2).Value = medicament.ActiveIngredients;
                command.Parameters.Add("PARAM9", OracleDbType.Blob).Value = blob.Length != 0 ? blob : null;          
                command.Parameters.Add("PARAM10", OracleDbType.Int16).Value = temp;

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
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
                string sql = @"select id_med as ID,  med.NAME as Nazov, med.TYPE.type as Typ, med.DESCRIPTION as Popis, man.name as Vyrobca,
                                is_prescribed as ""Na predpis""
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

        internal Dictionary<string, string> GetManufacturers()
        {
            Dictionary<string, string> output = new Dictionary<string, string>();
            try
            {
                _conn.Open();
                string sql = @"select id_man as ID, NAME as Nazov
                            from NOVAKOVA25.MANUFACTURER  ";
                using (OracleCommand command = new OracleCommand(sql, _conn))
                {
                    List<string> temp = new List<string>();
                    OracleDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        output.Add(Convert.ToString(reader[0]), Convert.ToString(reader[1]));
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

        internal Dictionary<int, string> getMedicamentsNames()
        {
            Dictionary<int, string> output = new Dictionary<int, string>();
            try
            {
                _conn.Open();
                string sql = @"select id_med as ID, NAME as Nazov
                            from NOVAKOVA25.MEDICAMENT fetch first 100 rows only";
                using (OracleCommand command = new OracleCommand(sql, _conn))
                {
                    List<string> temp = new List<string>();
                    OracleDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        output.Add(Convert.ToInt32(reader[0]), Convert.ToString(reader[1]));
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

        internal Medicament GetMedicament(int id)
        {          
            try
            {
                Medicament outpuMedicament = new Medicament();
                _conn.Open();
                //string sql = @"select med.NAME as Nazov, med.TYPE.type as Typ, med.DESCRIPTION as Popis, id_man, is_prescribed as ""Na predpis"",
                //            x.column_value as Ingrediencie
                //            from NOVAKOVA25.MEDICAMENT med, TABLE(select p.active_ingredients from medicament p
                //            where p.id_med = medicament.id_med ) x  where id_med = " + id;

                //string sql = @" select med.NAME as Nazov, med.TYPE.type as Typ,
                //                med.DESCRIPTION as Popis, id_man, is_prescribed as ""Na predpis"", t.* 
                //                FROM novakova25.medicament med, 
                //                table(med.active_ingredients) t WHERE med.id_med = " + id;

                string sql = @"select med.NAME as Nazov, med.TYPE.type as Typ,
                            med.DESCRIPTION as Popis, med.id_man, is_prescribed as ""Na predpis"",
                            usf_get_textingredients(med.active_ingredients)
                            FROM medicament med
                            WHERE med.id_med = " + id;
                using (OracleCommand command = new OracleCommand(sql, _conn))
                {
                    List<string> temp = new List<string>();
                    OracleDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        outpuMedicament.Id = id;
                        outpuMedicament.MedName = Convert.ToString(reader[0]);
                        outpuMedicament.MedType = Convert.ToString(reader[1]);
                        outpuMedicament.Description = Convert.ToString(reader[2]);
                        outpuMedicament.ManufacturerId = Convert.ToString(reader[3]);
                        outpuMedicament.IsPrescribed = Convert.ToBoolean(Convert.ToInt32(reader[4]));

                        temp.Add(Convert.ToString(reader[5]));
                        //outpuMedicament.ActiveIngredients = ;

                        
                    }
                    outpuMedicament.ActiveIngredients = string.Join(", ", temp);
                    return outpuMedicament;
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

        public BitmapImage GetMedImage(int id)
        {
            try
            {
                _conn.Open();
                string sql = @"select image from novakova25.medicament where id_med = " + id ;
                using (OracleCommand command = new OracleCommand(sql, _conn))
                {
                    OracleDataReader dr = command.ExecuteReader();
                    dr.Read();

                    //Get your blob type column which is Index 4 
                    OracleBinary imgBinary = dr.GetOracleBinary(0);
                    // Get the bytes from the binary obj
                    byte[] imgBytes = imgBinary.IsNull ? null : imgBinary.Value;
                    //Test 
                    using (var ms = new MemoryStream(imgBytes))
                    {
                        var bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = ms;
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.EndInit();

                        return bitmapImage;
                    }

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

    }
}
