using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classes;

namespace Provider
{
    public class Access
    {
        public static DataTable getDataTable(string str, OleDbConnection cn)
        {
            cn.Close();
            cn.Open();
            OleDbDataAdapter dar;
            dar = new OleDbDataAdapter(str, cn);
            DataTable dt = new DataTable();
            dar.Fill(dt);
            return dt;
            //cn.Close();
        }

        public static double[,] getMatrix(string str, OleDbConnection cn)
        {
            cn.Close();
            cn.Open();
            OleDbDataAdapter dar;
            dar = new OleDbDataAdapter(str, cn);
            DataTable dt = new DataTable();
            dar.Fill(dt);

            double[,] mtr = new double[dt.Columns.Count, dt.Rows.Count];

            for (int i = 0; i < mtr.GetLength(0); i++)
                for (int j = 0; j < mtr.GetLength(1); j++)
                    mtr[i, j] = Convert.ToDouble(dt.Rows[j][i]);

            return mtr;
            //cn.Close();
        }

        public static void delTable(string str, OleDbConnection cn)
        {
            cn.Close();
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("delete from " + str, cn);
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        

        public static void writeDataTable(DataTable dt, string field1, string table, OleDbConnection cn)
        {
            cn.Close();
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("delete from " + table, cn);
            cmd.ExecuteNonQuery();
            cmd = new OleDbCommand();
            cmd.Connection = cn;

            string[] field = field1.Split(',');
            for (int k = 0; k < dt.Rows.Count; k++)
            {

                string cmd1 = "insert into " + table + " (" + field[0];
                string cmd2 = ") Values ('";
                if (field.GetLength(0) == 1)
                    cmd1 = cmd1 + ") Values('" + dt.Rows[k][field[0]] + "')";
                else
                {
                    for (int i = 1; i < field.GetLength(0); i++)
                    {
                        cmd1 = cmd1 + ", " + field[i];
                    }
                    cmd1 = cmd1 + cmd2 + dt.Rows[k][field[0]].ToString() + "'";

                    for (int i = 1; i < field.GetLength(0); i++)
                    {
                        cmd1 = cmd1 + ", '" + dt.Rows[k][field[i]].ToString() + "'";
                    }
                    cmd1 = cmd1 + ")";
                }

                cmd.CommandText = cmd1;
                cmd.ExecuteNonQuery();
            }

            cn.Close();
        }

        public static void writeList<T>(List<T> dt, string table_name, OleDbConnection cn, string var_name)
        {
            delTable(table_name, cn);
            cn.Close();
            cn.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = cn;
            string[] name = var_name.Split(',');
            if (name[0] == "All")
            {
                int n = dt[0].GetType().GetProperties().Length;
                var prop = dt[0].GetType().GetProperties();

                for (int k = 0; k < dt.Count(); k++)
                {
                    string cmd1 = "insert into " + table_name + " (" + prop[0].Name;
                    string cmd2 = ") Values ('";
                    if (n == 1)
                        cmd1 = cmd1 + ") Values('" + prop[0].GetValue(dt[k], null) + "')";

                    else
                    {
                        for (int i = 1; i < n; i++)
                        {
                            cmd1 = cmd1 + ", " + prop[i].Name;
                        }
                        cmd1 = cmd1 + cmd2 + prop[0].GetValue(dt[k], null) + "'";

                        for (int i = 1; i < n; i++)
                        {
                            cmd1 = cmd1 + ", '" + prop[i].GetValue(dt[k], null) + "'";
                        }
                        cmd1 = cmd1 + ")";
                    }
                    cmd.CommandText = cmd1;
                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                int n = name.GetLength(0);
                for (int k = 0; k < dt.Count(); k++)
                {
                    string cmd1 = "insert into " + table_name + " (" + name[0];
                    string cmd2 = ") Values ('";
                    if (n == 1)
                        cmd1 = cmd1 + ") Values('" + dt[k].GetType().GetProperty(name[0]).GetValue(dt[k], null).ToString() + "')";

                    else
                    {
                        for (int i = 1; i < n; i++)
                        {
                            cmd1 = cmd1 + ", " + name[i];
                        }
                        cmd1 = cmd1 + cmd2 + dt[k].GetType().GetProperty(name[0]).GetValue(dt[k], null).ToString() + "'";

                        for (int i = 1; i < n; i++)
                        {
                            cmd1 = cmd1 + ", '" + dt[k].GetType().GetProperty(name[i]).GetValue(dt[k], null).ToString() + "'";
                        }
                        cmd1 = cmd1 + ")";
                    }
                    cmd.CommandText = cmd1;
                    cmd.ExecuteNonQuery();
                }
            }


            cn.Close();
        }

        public static void writemat(Mat Mat1, string table_name, OleDbConnection cn)
        {
            cn.Close();
            cn.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = cn;
            string cmd1 = "insert into " + table_name + " (Name, Type, Ws, Es, G, Fy, Fu, Wc, fc, Ec) Values ('" + Mat1.Name + "' , '" + Mat1.Type + "' , '" + Mat1.Ws + "' , '" + Mat1.Es
                + "' , '" + Mat1.G + "' , '" + Mat1.Fy + "' , '" + Mat1.Fu + "' , '" + Mat1.Wc + "' , '" + Mat1.fc + "' , '" + Mat1.Ec + "' )";
            cmd.CommandText = cmd1;
            cmd.ExecuteNonQuery();
        }

        public static void writetruck(List<List<double>> truck1, string table_name, OleDbConnection cn)
        {
            cn.Close();
            cn.Open();           

            OleDbCommand cmd = new OleDbCommand("delete from " + table_name, cn);
            cmd.ExecuteNonQuery();
            
            cmd = new OleDbCommand();
            cmd.Connection = cn;

            string cmd1;
            for (int i = 0; i < truck1.Count; i ++)
            {
                cmd1 = "insert into " + table_name + " (Coor, ALoad) Values ('" + truck1[i][0] + "' , '" + truck1[i][1]  + "' )";
                cmd.CommandText = cmd1;
                cmd.ExecuteNonQuery();
            }
            
            
        }

        //Write general to db
        public static void General(string bridgename, int ngirder, string txtspan, string table_name, OleDbConnection cn)
        {
            cn.Close();
            cn.Open();

            OleDbCommand cmd = new OleDbCommand("delete from " + table_name, cn);
            cmd.ExecuteNonQuery();

            cmd = new OleDbCommand();
            cmd.Connection = cn;

            string cmd1 = "insert into " + table_name + " (bridgename, ngirder, txtspan) Values ('" + bridgename + "' , '" + ngirder + "' , '" + txtspan + "' )";           
            cmd.CommandText = cmd1;
            cmd.ExecuteNonQuery();
        }

        public static void WriteAcross(double[,] Across, string table_name, OleDbConnection cn)
        {
            cn.Close();
            cn.Open();

            OleDbCommand cmd = new OleDbCommand("delete from " + table_name, cn);
            cmd.ExecuteNonQuery();

            cmd = new OleDbCommand();
            cmd.Connection = cn;

            string cmd1;
            for (int i = 0; i < Across.GetLength(1); i++)
            {                
                cmd1 = "insert into " + table_name + " (Type, Order1, Length) Values ('" + Across[0, i] + "' , '" + Across[1, i] + "' , '" + Across[2, i] + "' )";
                cmd.CommandText = cmd1;
                cmd.ExecuteNonQuery();
            }
        }

        public static void WriteAsection(double[,] Asection, string table_name, OleDbConnection cn)
        {
            cn.Close();
            cn.Open();

            OleDbCommand cmd = new OleDbCommand("delete from " + table_name, cn);
            cmd.ExecuteNonQuery();

            cmd = new OleDbCommand();
            cmd.Connection = cn;

            string cmd1;
            for (int i = 0; i < Asection.GetLength(1); i++)
            {
                cmd1 = "insert into " + table_name + " (Length, ID) Values ('" + Asection[0, i] + "' , '" + Asection[1, i]  + "' )";
                cmd.CommandText = cmd1;
                cmd.ExecuteNonQuery();
            }
        }

        public static void WriteDTHaunch(DataTable DTHaunch, string table_name, OleDbConnection cn)
        {
            cn.Close();
            cn.Open();

            OleDbCommand cmd = new OleDbCommand("delete from " + table_name, cn);
            cmd.ExecuteNonQuery();

            cmd = new OleDbCommand();
            cmd.Connection = cn;

            string cmd1;
            for (int i = 0; i < DTHaunch.Rows.Count; i++)
            {
                cmd1 = "insert into " + table_name + " (L1, L2, L3, H1, H2, H3) Values ('" + DTHaunch.Rows[i][0] + "' , '" + DTHaunch.Rows[i][1] + "' , '" + DTHaunch.Rows[i][2] + "' , '" + DTHaunch.Rows[i][3] + "' , '" + DTHaunch.Rows[i][4] + "' , '" + DTHaunch.Rows[i][5] + "' )";
                cmd.CommandText = cmd1;
                cmd.ExecuteNonQuery();
            }
        }

        public static void delmat(string str, OleDbConnection cn)
        {
            cn.Close();
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("delete * from Mat Where Name = '" + str + "'", cn);
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public static void upadatemat(Mat Mat1, OleDbConnection cn)
        {
            cn.Close();
            cn.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = cn;
            string cmd1 = "UPDATE Mat SET Type = '" + Mat1.Type + "', Ws = '" + Mat1.Ws + "', Es = '" + Mat1.Es + "', G = '" + Mat1.G + "', Fy = '" + Mat1.Fy + "', Fu = '" + Mat1.Fu + "', Wc = '" + Mat1.Wc + "', fc = '" + Mat1.fc
                + "', Ec = '" + Mat1.Ec + "' WHERE Name = '" + Mat1.Name + "'";
            cmd.CommandText = cmd1;
            cmd.ExecuteNonQuery();
            cn.Close();
        }

    }
}
