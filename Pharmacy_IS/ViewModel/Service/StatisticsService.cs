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
    public class StatisticsService
    {
        private OracleConnection _conn;

        public StatisticsService()
        {
            this.SetConnection();
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

        public DataTable GetBestSellers(String dateFrom, String dateTo)
        {
            try
            {

                string sql = "";

                if (dateFrom == null || dateTo == null)
                {
                    sql = @"WITH agg_sale_item AS (
                    SELECT si.id_med, m.name, pl.price,  SUM(si.quantity) AS total_sold, SUM(si.quantity)*pl.price AS total_price
                    FROM novakova25.sale_item si
                    INNER JOIN novakova25.medicament m ON m.id_med = si.id_med
                    INNER JOIN novakova25.price_list pl ON pl.id_med = m.id_med
                    INNER JOIN novakova25.sales_history sl ON sl.id_sale = si.id_sale
                    WHERE sl.sales_date >= TO_DATE('01.01.2022', 'dd.mm.yyyy')
                    AND sl.sales_date <= TO_DATE('01.01.2028', 'dd.mm.yyyy')
                    GROUP BY si.id_med, m.name, pl.price
                    )
                    
                    SELECT * 
                    FROM (
                    SELECT a.*, DENSE_RANK() OVER (ORDER BY total_price DESC) AS FINAL_RANK 
                    FROM agg_sale_item a
                    ) x
                    WHERE x.final_rank <= 10
                    ";
                }
                else
                {

                    sql = @"WITH agg_sale_item AS (
                    SELECT si.id_med, m.name, pl.price,  SUM(si.quantity) AS total_sold, SUM(si.quantity)*pl.price AS total_price
                    FROM novakova25.sale_item si
                    INNER JOIN novakova25.medicament m ON m.id_med = si.id_med
                    INNER JOIN novakova25.price_list pl ON pl.id_med = m.id_med
                    INNER JOIN novakova25.sales_history sl ON sl.id_sale = si.id_sale
                    WHERE sl.sales_date >= " + "TO_DATE('" + dateFrom + "'" + ", 'dd.mm.yyyy')" + " AND sl.sales_date <= " + "TO_DATE('" + dateTo + "'" + ", 'dd.mm.yyyy') " + 
                    "GROUP BY si.id_med, m.name, pl.price " +
                    ") " +
                    " " +
                    "SELECT * " +
                    "FROM ( " +
                    "SELECT a.*, DENSE_RANK() OVER (ORDER BY total_price DESC) AS FINAL_RANK  " +
                    "FROM agg_sale_item a " +
                    ") x " +
                    "WHERE x.final_rank <= 10 ";
                }

                Console.WriteLine("SQL is: " + sql);

                _conn.Open();
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
                return null;
            }
            finally
            {
                _conn.Close();
            }
        }

        public DataTable GetEmployeesRanking(String dateFrom, String dateTo)
        {
            try
            {

                string sql = "";

                if (dateFrom == null || dateTo == null)
                {
                     sql = @"SELECT p.user_name, SUM(si.price) AS Total_income
                            FROM novakova25.sales_history si
                            JOIN novakova25.person p ON p.id_user = si.id_user
                            WHERE si.sales_date >= TO_DATE('2000-01-01', 'yyyy-mm-dd') AND si.sales_date <= TO_DATE('2030-12-12', 'yyyy-mm-dd')
                            GROUP BY p.user_name
                            ORDER BY Total_income DESC";
                }
                else
                {
                    sql =  @"SELECT p.user_name, SUM(si.price) AS Total_income
                            FROM novakova25.sales_history si
                            JOIN novakova25.person p ON p.id_user = si.id_user
                            WHERE si.sales_date >=" + "TO_DATE('" +   dateFrom  + "'" +  ", 'dd.mm.yyyy') AND si.sales_date <= " + "TO_DATE('" + dateTo + "'"  + ", 'dd.mm.yyyy')" + 
                            "GROUP BY p.user_name " + 
                            "ORDER BY Total_income DESC";
                }
                _conn.Open();
                //Console.WriteLine("SQL is: "  + sql);
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
                return null;
            }
            finally
            {
                _conn.Close();
            }
        }
    
        public DataTable GetMonthsIncome(String dateFrom, String dateTo)
        {
            try
            {

                string sql = "";

                if (dateFrom == null || dateTo == null)
                {
                    sql = @"SELECT TO_CHAR(si.sales_date,'yyyy-mm') AS DATE_PERIOD, SUM(si.price) AS Total_income
                            FROM novakova25.sales_history si
                            JOIN novakova25.person p ON p.id_user = si.id_user
                            WHERE si.sales_date >= TO_DATE('2000-01-01', 'yyyy-mm-dd') AND si.sales_date <= TO_DATE('2030-12-12', 'yyyy-mm-dd')
                            GROUP BY TO_CHAR(si.sales_date, 'yyyy-mm')
                            ORDER BY DATE_PERIOD ASC";
                }
                else
                {
                    sql = @"SELECT TO_CHAR(si.sales_date,'yyyy-mm') AS DATE_PERIOD, SUM(si.price) AS Total_income
                            FROM novakova25.sales_history si
                            JOIN novakova25.person p ON p.id_user = si.id_user
                            WHERE si.sales_date >= " + "TO_DATE('" + dateFrom + "'" + ", 'dd.mm.yyyy')" + " AND si.sales_date <= " + "TO_DATE('" + dateTo + "'" + ", 'dd.mm.yyyy')" +
                            "GROUP BY TO_CHAR(si.sales_date, 'yyyy-mm') " +
                            "ORDER BY DATE_PERIOD ASC";
                }
                    _conn.Open();
     
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
                return null;
            }
            finally
            {
                _conn.Close();
            }
            
        }
    }
}
