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

        internal Medicament GetMedicament(int id)
        {          
            try
            {
                Medicament outpuMedicament = new Medicament();
                _conn.Open();
                string sql = @"select med.NAME as Nazov, med.TYPE.type as Typ, med.DESCRIPTION as Popis, id_man, is_prescribed as ""Na predpis"",
                            x.column_value as Ingrediencie
                            from NOVAKOVA25.MEDICAMENT med, TABLE(select p.active_ingredients from medicament p
                            where p.id_med = medicament.id_med ) x  where id_med = " + id;

                string sql1 = @" select med.NAME as Nazov, med.TYPE.type as Typ,
                                med.DESCRIPTION as Popis, id_man, is_prescribed as ""Na predpis"", t.* 
                                FROM novakova25.medicament med, 
                                table(med.active_ingredients) t WHERE med.id_med = 45004 ";
                using (OracleCommand command = new OracleCommand(sql1, _conn))
                {
                    OracleDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        outpuMedicament.MedName = Convert.ToString(reader[0]);
                        outpuMedicament.MedType = Convert.ToString(reader[1]);
                        outpuMedicament.Description = Convert.ToString(reader[2]);
                        outpuMedicament.ManufacturerId = Convert.ToInt32(reader[3]);
                        outpuMedicament.IsPrescribed = Convert.ToBoolean(Convert.ToUInt32(reader[4]));
                        outpuMedicament.ActiveIngredients = Convert.ToString(reader[5]);

                        return outpuMedicament;
                    }
                    return null;
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
