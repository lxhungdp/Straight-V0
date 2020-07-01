﻿using System;
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

        public static void delTable(string str, OleDbConnection cn)
        {
            cn.Close();
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("delete from " + str, cn);
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        

        public static void writeDataTable(DataTable dt, string[] field, string table, OleDbConnection cn)
        {
            cn.Close();
            cn.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = cn;

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
