using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classes;
using Dapper;
using System.Transactions;

namespace Provider
{
    public class SQL
    {
        public static string filename
        { get; set; }

        public static void CreateDB()
        {
            SQLiteConnection.CreateFile(filename);
        }

        public static string stringconnection()
        {
            return "Data Source=" + filename + "; Version=3;";
            //return "Data Source=" + filename + "; Version=3; Synchronous=Off; Journal Mode=MEMORY ";
        }
               

        public static void InitializeDB()
        { 
            List<string> sql = new List<string>();

            sql.Add("create table IF NOT EXISTS A01General(bridgename varchar(50), ngirder int, txtspan varchar(50))");
            sql.Add("create table IF NOT EXISTS A02Mat (Name varchar(50) Unique, Type varchar(20), Ws double, Es double, G double, Fy double, Fu double, Wc double, fc double, Ec double, Lib varchar(20))");
            sql.Add("create table IF NOT EXISTS A03Matuse (Name varchar(50), Type varchar(20), Ws double, Es double, G double, Fy double, Fu double, Wc double, fc double, Ec double, Lib varchar(20)) ");
            sql.Add("create table IF NOT EXISTS A04Loading (Trucktype int, Truckgrade int, Laneload double, Pload double, ADTT double, Overloading double, Pforms double, Pparapet double, tAsphalt double, gAsphalt double, Lane1 double" +
                ", Lane2 double, Lane3 double, Lane4 double, Lane5 double) ");
            sql.Add("create table IF NOT EXISTS A05Truck (Coor double, ALoad double) ");
            sql.Add("create table IF NOT EXISTS A06Across (Type double, ID double, Length double)");
            sql.Add("create table IF NOT EXISTS A07Atran (Type double, ID double, Length double)");
            sql.Add("create table IF NOT EXISTS A08Asection (Length double, ID double) ");
            sql.Add("create table IF NOT EXISTS A09Ahaunch (L1 double, L2 double , L3 double, H1 double, H2 double, H3 double)");
            sql.Add("create table IF NOT EXISTS A10Acbox (L1 double, L2 double) ");
            sql.Add("create table IF NOT EXISTS A11Acon (Length double, H1 double, H2 double, H3 double, H4 double, H5 double, H6 double, H7 double, H8 double, H9 double, H10 double, ID double)");
            sql.Add("create table IF NOT EXISTS A12Atop (Type double, ID double, Length double, Width double, Thickness double)");
            sql.Add("create table IF NOT EXISTS A13Abot (Length double, Thickness double) ");
            sql.Add("create table IF NOT EXISTS A14Aweb (Length double, Thickness double) ");
            sql.Add("create table IF NOT EXISTS A15ADims (ts double, th double, bh double, drt double, art double, crt double, drb double, arb double, crb double, Sr double, Sd double, Ss double, Sindex double, w double, D1 double, ctop double, cbot double) ");
            sql.Add("create table IF NOT EXISTS A16Atranstiff (Type double, ID double, Length double) ");
            sql.Add("create table IF NOT EXISTS A17Aribbot (Length double, Amount double, Depth double, Thickness double)  ");
            sql.Add("create table IF NOT EXISTS A18Aribtop (Type double, ID double, Length double, Amount double, Depth double, Thickness double) ");
            sql.Add("create table IF NOT EXISTS A19ns (ns double) ");
            sql.Add("create table IF NOT EXISTS A20Crossbeam (type string, ttop double, btop double, tbot double, bbot double, D double, tw double, nw double ) ");
            sql.Add("create table IF NOT EXISTS A21Parapet (type string, H1 double, H2 double, H3 double, B1 double, B2 double, B3 double, e double ) ");
            sql.Add("create table IF NOT EXISTS A22KFrame (Station double, Location int, Description string) ");
            sql.Add("create table IF NOT EXISTS A23Analysis (KFrame int, Schanged int, numseg1 double, numseg2 double) ");

            sql.Add("create table IF NOT EXISTS A24Support (Support varchar(50)) ");
            sql.Add("create table IF NOT EXISTS A25Shoe (Girder int, Support int, Label varchar(20), EA int, A double, B double, Joint int, X double, Y double, Z double, D double, Type varchar(20)) ");

            sql.Add("create table IF NOT EXISTS BNode (Joint int, BeamID int, Type int, Label varchar(50), Haunch int, X double, Y double, Z double, Restrain varchar(50), ntop double, btop double, ttop double, ctop double," +
                "bbot double, tbot double, cbot double, D double, tw double, Hc double, ts double, w double, th double, bh double, Leff double, bs double, bleft double, bright double, aleft double, aright double, e double, drt double," +
                "art double, crt double, drb double, arb double, crb double, S double, nst double, Hst double, tst double, nsb double, Hsb double, tsb double, ns double, Lb double, d0 double, Hw double," +
                "Ac double, Ic double, As1 double, Is1 double, Ah double, Ih double, Srt double, Srb double, Irt double, Irb double) ");
            sql.Add("create table IF NOT EXISTS BSec (Element varchar(50), Node int, Station double, A1 double, YL1 double, YU1 double, Ix1 double, Iy1 double, SL1 double, SU1 double, J1 double, " +
                "A2s double, YL2s double, YU2s double, Ix2s double, Iy2s double, SL2s double, SU2s double, J2s double, " +
                "A2l double, YL2l double, YU2l double, Ix2l double, Iy2l double, SL2l double, SU2l double, J2l double, " +
                "A3s double, YL3s double, YU3s double, Ix3s double, Iy3s double, SL3s double, SU3s double, J3s double, " +
                "A3l double, YL3l double, YU3l double, Ix3l double, Iy3l double, SL3l double, SU3l double, J3l double, " +
                "A4s double, YL4s double, YU4s double, Ix4s double, Iy4s double, SL4s double, SU4s double, J4s double, " +
                "A4l double, YL4l double, YU4l double, Ix4l double, Iy4l double, SL4l double, SU4l double, J4l double," +
                "A5s double, YL5s double, YU5s double, Ix5s double, Iy5s double, SL5s double, SU5s double, J5s double) ");
            sql.Add("create table IF NOT EXISTS BMoment (Element varchar(50), Node int, Station double, Description varchar(50), DC1 double, DC2 double, DC3 double, DC4 double, DW double, Truckmax double, " +
               "Truckmin double, Lanemax double, Lanemin double, PLmax double, PLmin double, LLfmax double, LLfmin double, LLmax double, LLmin double) ");
            sql.Add("create table IF NOT EXISTS BShear (Element varchar(50), Node int, Station double, Description varchar(50), DC1 double, DC2 double, DC3 double, DC4 double, DW double, Truckmax double, " +
               "Truckmin double, Lanemax double, Lanemin double, PLmax double, PLmin double, LLfmax double, LLfmin double, LLmax double, LLmin double) ");
            sql.Add("create table IF NOT EXISTS BTorsion (Element varchar(50), Node int, Station double, Description varchar(50), DC1 double, DC2 double, DC3 double, DC4 double, DW double, Truckmax double, " +
               "Truckmin double, Lanemax double, Lanemin double, PLmax double, PLmin double, LLfmax double, LLfmin double, LLmax double, LLmin double) ");
            sql.Add("create table IF NOT EXISTS BDeflection (Node int, Station double, Description varchar(50), DC1 double, DC2 double, DC3 double, DC4 double, DW double, Truckmax double, " +
              "Truckmin double, Lanemax double, Lanemin double, PLmax double, PLmin double, LLfmax double, LLfmin double, LLmax double, LLmin double) ");
            sql.Add("create table IF NOT EXISTS BReaction (Node int, Station double, Description varchar(50), DC1 double, DC2 double, DC3 double, DC4 double, DW double, Truckmax double, " +
             "Truckmin double, Lanemax double, Lanemin double, PLmax double, PLmin double, LLfmax double, LLfmin double, LLmax double, LLmin double)  ");
            sql.Add("create table IF NOT EXISTS BStress (Element varchar(50), Node int, Station double, S1_top double, S1_bot double, S2_top double, S2_bot double, S3_top_short double, S3_bot_short double, S3_top_long double, S3_bot_long double, " +
            "S4_top double, S4_bot double, Sw_top double, Sw_bot double, Slmax_top double, Slmax_bot double, Slmin_top double, Slmin_bot double, Sfmax_top double, Sfmax_bot double, Sfmin_top double, Sfmin_bot double, Sc_top double, Sc_bot double, " +
            "Su_top double, Su_bot double, Ss1_top double, Ss1_bot double, Ss2_top double, Ss2_bot double, Sf_top double, Sf_bot double) ");

            //Write to DB
            SQLiteConnection con = new SQLiteConnection(stringconnection());
            con.Open();
            SQLiteCommand command = new SQLiteCommand(con);

            SQLiteTransaction transaction = con.BeginTransaction();
            for (int i = 0; i < sql.Count; i++)
            {
                command.CommandText = sql[i];
                command.ExecuteNonQuery();
            }

            transaction.Commit();
            con.Close();
        }


       
        public static void delTable(string str)
        {
            SQLiteConnection con = new SQLiteConnection(stringconnection());
            con.Open();
            SQLiteCommand command = new SQLiteCommand(con);
            command.CommandText = "delete from " + str;
            command.ExecuteNonQuery();
            con.Close();
        }

        //Write to DB -----------------------------------------------------------------------------------
        //Write List to DB
        public static void WriteList<T>(List<T> dt, string table_name, string var_name)
        {
            SQLiteConnection con = new SQLiteConnection(stringconnection());
            con.Open();
            SQLiteCommand command = new SQLiteCommand(con);

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
                    command.CommandText = cmd1;
                    command.ExecuteNonQuery();
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
                    command.CommandText = cmd1;
                    command.ExecuteNonQuery();
                }
            }


            con.Close();
        }


        //Save to Database
        //Write General tab to DB
        public static void WriteGeneral(string bridgename, int ngirder, string txtspan, string table_name)
        {
            SQLiteConnection con = new SQLiteConnection(stringconnection());
            con.Open();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = "delete from " + table_name;
            command.ExecuteNonQuery();

            command = new SQLiteCommand(con);

            command.CommandText = "insert into " + table_name + " (bridgename, ngirder, txtspan) Values ('" + bridgename + "' , '" + ngirder + "' , '" + txtspan + "' )";
            command.ExecuteNonQuery();
            con.Close();
        }

        //Write Mat tab to DB
        public static void WriteMat(List<Mat> Mat, List<Mat> Matuse, string tableMat, string tableMatuse)
        {
            SQLiteConnection con = new SQLiteConnection(stringconnection());
            con.Open();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = "delete from " + tableMat;
            command.ExecuteNonQuery();

            for (int i = 0; i < Mat.Count; i++)
            {
                command = new SQLiteCommand(con);
                command.CommandText = "insert into " + tableMat + " (Name, Type, Ws, Es, G, Fy, Fu, Wc, fc, Ec, Lib) Values ('" + Mat[i].Name + "' , '" + Mat[i].Type + "' , '" + Mat[i].Ws + "' , '" + Mat[i].Es
                + "' , '" + Mat[i].G + "' , '" + Mat[i].Fy + "' , '" + Mat[i].Fu + "' , '" + Mat[i].Wc + "' , '" + Mat[i].fc + "' , '" + Mat[i].Ec + "' , '" + Mat[i].Lib + "' )";
                command.ExecuteNonQuery();
            }

            command = new SQLiteCommand(con);
            command.CommandText = "delete from " + tableMatuse;
            command.ExecuteNonQuery();

            for (int i = 0; i < Matuse.Count; i++)
            {
                command = new SQLiteCommand(con);
                command.CommandText = "insert into " + tableMatuse + " (Name, Type, Ws, Es, G, Fy, Fu, Wc, fc, Ec, Lib) Values ('" + Matuse[i].Name + "' , '" + Matuse[i].Type + "' , '" + Matuse[i].Ws + "' , '" + Matuse[i].Es
                + "' , '" + Matuse[i].G + "' , '" + Matuse[i].Fy + "' , '" + Matuse[i].Fu + "' , '" + Matuse[i].Wc + "' , '" + Matuse[i].fc + "' , '" + Matuse[i].Ec + "' , '" + Matuse[i].Lib + "' )";
                command.ExecuteNonQuery();
            }

            con.Close();
        }


        //Write loading to DB
        public static void Writeloading(int Trucktype, int Truckgrade, double Laneload, double Pload, double ADTT, double Overloading, double Pforms, double Pparapet, double tAsphalt, double gAsphalt, List<double> Lanefactor, List<Tuple<double, double>> Truckaxle, string table_name, string table_truck)
        {
            SQLiteConnection con = new SQLiteConnection(stringconnection());
            con.Open();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = "delete from " + table_name;
            command.ExecuteNonQuery();

            command = new SQLiteCommand(con);
            command.CommandText = "insert into " + table_name + " (Trucktype, Truckgrade, Laneload, Pload, ADTT , Overloading, Pforms, Pparapet, tAsphalt, gAsphalt, Lane1, Lane2, Lane3, Lane4, Lane5) Values " +
                "('" + Trucktype + "' , '" + Truckgrade + "' , '" + Laneload + "' , '" + Pload + "' , '" + ADTT + "' , '" + Overloading + "' , '" + Pforms + "' , '" + Pparapet + "' , '" + tAsphalt + "' , '" + gAsphalt + "' , '" + Lanefactor[0] + "' , '" + Lanefactor[1] + "' , '" + Lanefactor[2] + "' , '" + Lanefactor[3] + "' , '" + Lanefactor[4] + "' )";
            command.ExecuteNonQuery();

            command.CommandText = "delete from " + table_truck;
            command.ExecuteNonQuery();

            for (int i = 0; i < Truckaxle.Count; i++)
            {
                command = new SQLiteCommand(con);
                command.CommandText = "insert into " + table_truck + " (Coor, ALoad) Values ('" + Truckaxle[i].Item1 + "' , '" + Truckaxle[i].Item2 + "' )";
                command.ExecuteNonQuery();
            }
            con.Close();
        }

        //Write grid to DB
        public static void WriteGrid(double[,] Across, double[,] Atran, double[,] Asection, List<string> Support, string tableAcross, string tableAtran, string tableAsection, string tableSupport)
        {
            SQLiteConnection con = new SQLiteConnection(stringconnection());
            con.Open();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = "delete from " + tableAcross;
            command.ExecuteNonQuery();

            for (int i = 0; i < Across.GetLength(1); i++)
            {
                command = new SQLiteCommand(con);
                command.CommandText = "insert into " + tableAcross + " (Type, ID, Length) Values ('" + Across[0, i] + "' , '" + Across[1, i] + "' , '" + Across[2, i] + "' )";
                command.ExecuteNonQuery();
            }

            command = new SQLiteCommand(con);
            command.CommandText = "delete from " + tableAtran;
            command.ExecuteNonQuery();

            for (int i = 0; i < Atran.GetLength(1); i++)
            {
                command = new SQLiteCommand(con);
                command.CommandText = "insert into " + tableAtran + " (Type, ID, Length) Values ('" + Atran[0, i] + "' , '" + Atran[1, i] + "' , '" + Atran[2, i] + "' )";
                command.ExecuteNonQuery();
            }

            command = new SQLiteCommand(con);
            command.CommandText = "delete from " + tableAsection;
            command.ExecuteNonQuery();

            for (int i = 0; i < Asection.GetLength(1); i++)
            {
                command = new SQLiteCommand(con);
                command.CommandText = "insert into " + tableAsection + " (Length, ID) Values ('" + Asection[0, i] + "' , '" + Asection[1, i] + "' )";
                command.ExecuteNonQuery();
            }            

            command = new SQLiteCommand(con);
            command.CommandText = "delete from " + tableSupport;
            command.ExecuteNonQuery();

            for (int i = 0; i < Support.Count; i++)
            {
                command = new SQLiteCommand(con);
                command.CommandText = "insert into " + tableSupport + " (Support ) Values ('" + Support[i] + "' )";
                command.ExecuteNonQuery();
            }
            con.Close();


        }

        //Write haunch to DB
        public static void WriteHaunch(double[,] Ahaunch, double[,] Acbox, double[,] Acon, string tableAhaunch, string tableAcbox, string tableAcon)
        {
            SQLiteConnection con = new SQLiteConnection(stringconnection());
            con.Open();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = "delete from " + tableAhaunch;
            command.ExecuteNonQuery();

            for (int i = 0; i < Ahaunch.GetLength(0); i++)
            {
                command = new SQLiteCommand(con);
                command.CommandText = "insert into " + tableAhaunch + " (L1, L2, L3, H1, H2, H3) Values ('" + Ahaunch[i, 0] + "' , '" + Ahaunch[i, 1] + "' , '" + Ahaunch[i, 2] + "' , '" + Ahaunch[i, 3] + "' , '" + Ahaunch[i, 4] + "' , '" + Ahaunch[i, 5] + "' )";
                command.ExecuteNonQuery();
            }

            command = new SQLiteCommand(con);
            command.CommandText = "delete from " + tableAcbox;
            command.ExecuteNonQuery();

            for (int i = 0; i < Acbox.GetLength(0); i++)
            {
                command = new SQLiteCommand(con);
                command.CommandText = "insert into " + tableAcbox + " (L1, L2) Values ('" + Acbox[i, 0] + "' , '" + Acbox[i, 1] + "' )";
                command.ExecuteNonQuery();
            }

            command = new SQLiteCommand(con);
            command.CommandText = "delete from " + tableAcon;
            command.ExecuteNonQuery();

            for (int i = 0; i < Acon.GetLength(1); i++)
            {
                command = new SQLiteCommand(con);
                command.CommandText = "insert into " + tableAcon + " (Length, H1, H2, H3, H4, H5, H6, H7, H8, H9, H10, ID) Values ('" + Acon[0, i] + "' , '" + Acon[1, i] + "' , '" + Acon[2, i] + "' , '" + Acon[3, i] + "' , '" + Acon[4, i] + "' , '" + Acon[5, i] + "' , '" + Acon[6, i] + "' , '" + Acon[7, i] + "' , '" + Acon[8, i] + "' , '" + Acon[9, i] + "' , '" + Acon[10, i] + "' , '" + Acon[11, i] + "' )";
                command.ExecuteNonQuery();
            }
            con.Close();
        }

        //Write Dim to DB
        public static void WriteDim(double[,] Atop, double[,] Abot, double[,] Aweb, double ts, double th, double bh, double drt, double art, double crt, double drb, double arb, double crb, double S, double Sd, double w, double D1, double ctop, double cbot, string tableAtop, string tableAbot, string tableAweb, string tableADims)
        {
            SQLiteConnection con = new SQLiteConnection(stringconnection());
            con.Open();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = "delete from " + tableAtop;
            command.ExecuteNonQuery();

            for (int i = 0; i < Atop.GetLength(1); i++)
            {
                command = new SQLiteCommand(con);
                command.CommandText = "insert into " + tableAtop + " (Type, ID, Length, Width, Thickness) Values ('" + Atop[0, i] + "' , '" + Atop[1, i] + "' , '" + Atop[2, i] + "' , '" + Atop[3, i] + "' , '" + Atop[4, i] + "' )";
                command.ExecuteNonQuery();
            }

            command.CommandText = "delete from " + tableAbot;
            command.ExecuteNonQuery();

            for (int i = 0; i < Abot.GetLength(1); i++)
            {
                command = new SQLiteCommand(con);
                command.CommandText = "insert into " + tableAbot + " (Length, Thickness) Values ('" + Abot[0, i] + "' , '" + Abot[1, i] + "' )";
                command.ExecuteNonQuery();
            }

            command.CommandText = "delete from " + tableAweb;
            command.ExecuteNonQuery();

            for (int i = 0; i < Aweb.GetLength(1); i++)
            {
                command = new SQLiteCommand(con);
                command.CommandText = "insert into " + tableAweb + " (Length, Thickness) Values ('" + Aweb[0, i] + "' , '" + Aweb[1, i] + "' )";
                command.ExecuteNonQuery();
            }

            command = new SQLiteCommand(con);
            command.CommandText = "delete from " + tableADims;
            command.ExecuteNonQuery();
            command = new SQLiteCommand(con);
            command.CommandText = "insert into " + tableADims + " (ts, th, bh, drt, art, crt, drb, arb, crb, S, Sd, w, D1, ctop, cbot) Values ('" + ts + "' , '" + th + "' , '" + bh + "' , '" + drt + "' , '" + art + "' , '" + crt + "' , '" + drb + "' , '" + arb + "' , '" + crb + "' , '" + S + "' , '" + Sd + "' , '" + w + "' , '" + D1 + "' , '" + ctop + "' , '" + cbot + "' )";
            command.ExecuteNonQuery();


            con.Close();
        }

        //Write Stiff to DB
        public static void WriteStiff(double[,] Atranstiff, double[,] Aribbot, double[,] Aribtop, double ns, string tableAtranstiff, string tableAribbot, string tableAribtop, string tablens)
        {
            SQLiteConnection con = new SQLiteConnection(stringconnection());
            con.Open();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = "delete from " + tableAtranstiff;
            command.ExecuteNonQuery();

            for (int i = 0; i < Atranstiff.GetLength(1); i++)
            {
                command = new SQLiteCommand(con);
                command.CommandText = "insert into " + tableAtranstiff + " (Type, ID, Length) Values ('" + Atranstiff[0, i] + "' , '" + Atranstiff[1, i] + "' , '" + Atranstiff[2, i] + "' )";
                command.ExecuteNonQuery();
            }

            command.CommandText = "delete from " + tableAribbot;
            command.ExecuteNonQuery();

            for (int i = 0; i < Aribbot.GetLength(1); i++)
            {
                command = new SQLiteCommand(con);
                command.CommandText = "insert into " + tableAribbot + " (Length, Amount, Depth, Thickness) Values ('" + Aribbot[0, i] + "' , '" + Aribbot[1, i] + "' , '" + Aribbot[2, i] + "' , '" + Aribbot[3, i] + "' )";
                command.ExecuteNonQuery();
            }

            command.CommandText = "delete from " + tableAribtop;
            command.ExecuteNonQuery();

            for (int i = 0; i < Aribtop.GetLength(1); i++)
            {
                command = new SQLiteCommand(con);
                command.CommandText = "insert into " + tableAribtop + " (Type, ID, Length, Amount, Depth, Thickness) Values ('" + Aribtop[0, i] + "' , '" + Aribtop[1, i] + "' , '" + Aribtop[2, i] + "' , '" + Aribtop[3, i] + "' , '" + Aribtop[4, i] + "' , '" + Aribtop[5, i] + "' )";
                command.ExecuteNonQuery();
            }

            command = new SQLiteCommand(con);
            command.CommandText = "delete from " + tablens;
            command.ExecuteNonQuery();
            command = new SQLiteCommand(con);
            command.CommandText = "insert into " + tablens + " (ns ) Values ('" + ns + "' )";
            command.ExecuteNonQuery();


            con.Close();
        }

        //Write Other to DB
        public static void Writeother(List<Crossbeam> Crossbeam, List<Parapet> Parapet, List<KFrame> KFrame, string tablecrossbeam, string tableparapet, string tableKFrame)
        {
            SQLiteConnection con = new SQLiteConnection(stringconnection());
            con.Open();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = "delete from " + tablecrossbeam;
            command.ExecuteNonQuery();

            for (int i = 0; i < Crossbeam.Count; i++)
            {
                command = new SQLiteCommand(con);
                command.CommandText = "insert into " + tablecrossbeam + " (type, ttop, btop, tbot, bbot, D, tw, nw ) Values ('" + Crossbeam[i].type + "' , '" + Crossbeam[i].ttop + "' , '" + Crossbeam[i].btop + "' , '" + Crossbeam[i].tbot + "' , '" + Crossbeam[i].bbot + "' , '" + Crossbeam[i].D + "' , '" + Crossbeam[i].tw + "' , '" + Crossbeam[i].nw + "' )";
                command.ExecuteNonQuery();
            }

            command.CommandText = "delete from " + tableparapet;
            command.ExecuteNonQuery();

            for (int i = 0; i < Parapet.Count; i++)
            {
                command = new SQLiteCommand(con);
                command.CommandText = "insert into " + tableparapet + " (type, H1, H2 , H3 , B1 , B2 , B3 , e ) Values ('" + Parapet[i].type + "' , '" + Parapet[i].H1 + "' , '" + Parapet[i].H2 + "' , '" + Parapet[i].H3 + "' , '" + Parapet[i].B1 + "' , '" + Parapet[i].B2 + "' , '" + Parapet[i].B3 + "' , '" + Parapet[i].e + "' )";
                command.ExecuteNonQuery();
            }

            command.CommandText = "delete from " + tableKFrame;
            command.ExecuteNonQuery();

            for (int i = 0; i < KFrame.Count; i++)
            {
                command = new SQLiteCommand(con);
                command.CommandText = "insert into " + tableKFrame + " (Station, Location, Description ) Values ('" + KFrame[i].Station + "' , '" + (KFrame[i].Location == true ? 1 : 0) + "' , '" + KFrame[i].Description + "' )";
                command.ExecuteNonQuery();
            }
            con.Close();
        }

        //Write Analysis to DB
        public static void WriteAnalysis(List<int> Divindex, double numseg1, double numseg2, string tableAnalysis)
        {
            SQLiteConnection con = new SQLiteConnection(stringconnection());
            con.Open();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = "delete from " + tableAnalysis;
            command.ExecuteNonQuery();

            command = new SQLiteCommand(con);
            command.CommandText = "insert into " + tableAnalysis + " (KFrame, Schanged, numseg1, numseg2) Values ('" + Divindex[0] + "' , '" + Divindex[1] + "' , '" + numseg1 + "' , '" + numseg2 + "' )";
            command.ExecuteNonQuery();


            con.Close();
        }

        //Write Analysis to DB
        public static void WriteNodeexnormal(List<Node> Node, string tablename)
        {
            SQLiteConnection con = new SQLiteConnection(stringconnection());
            con.Open();
            SQLiteCommand command = new SQLiteCommand("begin", con);

            command.CommandText = "delete from " + tablename;
            command.ExecuteNonQuery();

            command = new SQLiteCommand(con);
            SQLiteTransaction transaction = con.BeginTransaction();
            for (int i = 0; i < Node.Count; i++)
            {
                //command = new SQLiteCommand(con);
                command.CommandText = "insert into " + tablename + " (Joint , BeamID , Type , Label , Haunch , X , Y , Z , Restrain , ntop , btop , ttop , ctop ," +
                "bbot , tbot , cbot , D , tw , Hc , ts , w , th , bh , Leff , bs , bleft , bright , aleft , aright, e , drt ," +
                "art , crt , drb , arb , crb  , S , nst , Hst , tst , nsb , Hsb , tsb , ns , Lb , d0 , Hw ," +
                "Ac , Ic , As1 , Is1 , Ah , Ih , Srt , Srb , Irt , Irb ) Values ('" + Node[i].Joint + "' , '" + Node[i].BeamID + "' , '" + Node[i].Type + "' , '" + Node[i].Label + 
                "' , '" + Node[i].Haunch + "' , '" + Node[i].X + "' , '" + Node[i].Y + "' , '" + Node[i].Z + "' , '" + Node[i].Restrain + "' , '" + Node[i].ntop + 
                "' , '" + Node[i].btop + "' , '" + Node[i].ttop + "' , '" + Node[i].ctop + "' , '" + Node[i].bbot + "' , '" + Node[i].tbot + "' , '" + Node[i].cbot +
                "' , '" + Node[i].D + "' , '" + Node[i].tw + "' , '" + Node[i].Hc + "' , '" + Node[i].ts + "' , '" + Node[i].w + "' , '" + Node[i].th + "' , '" + Node[i].bh + 
                "' , '" + Node[i].Leff + "' , '" + Node[i].bs + "' , '" + Node[i].bleft + "' , '" + Node[i].bright + "' , '" + Node[i].aleft + "' , '" + Node[i].aright + "' , '" + Node[i].e +
                "' , '" + Node[i].drt + "' , '" + Node[i].art + "' , '" + Node[i].crt + "' , '" + Node[i].drb + "' , '" + Node[i].arb + "' , '" + Node[i].crb + "' , '" + Node[i].S +
                "' , '" + Node[i].nst + "' , '" + Node[i].Hst + "' , '" + Node[i].tst + "' , '" + Node[i].nsb + "' , '" + Node[i].Hsb + "' , '" + Node[i].tsb + "' , '" + Node[i].ns +
                "' , '" + Node[i].Lb + "' , '" + Node[i].d0 + "' , '" + Node[i].Hw + "' , '" + Node[i].Ac + "' , '" + Node[i].Ic + "' , '" + Node[i].As1 + "' , '" + Node[i].Is1 +
                "' , '" + Node[i].Ah + "' , '" + Node[i].Ih + "' , '" + Node[i].Srt + "' , '" + Node[i].Srb + "' , '" + Node[i].Irt + "' , '" + Node[i].Irb +  "' )";
                command.ExecuteNonQuery();
            }
            transaction.Commit();
            //command = new SQLiteCommand("end", con);
            
            con.Close();
        }

        public static void WriteNodeexScope(List<Node> Node, string tablename)
        {
            SQLiteConnection con = new SQLiteConnection(stringconnection());
            con.Open();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = "delete from " + tablename;
            command.ExecuteNonQuery();

            using (TransactionScope scope = new TransactionScope())
            {
                string sql = "insert into " + tablename + " (Joint , BeamID , Type , Label , Haunch , X , Y , Z , Restrain , ntop , btop , ttop , ctop ," +
                "bbot , tbot , cbot , D , tw , Hc , ts , w , th , bh , Leff , bs , bleft , bright , aleft , aright, e , drt ," +
                "art , crt , drb , arb , crb  , S , nst , Hst , tst , nsb , Hsb , tsb , ns , Lb , d0 , Hw ," +
                "Ac , Ic , As1 , Is1 , Ah , Ih , Srt , Srb , Irt , Irb ) Values (@Joint , @BeamID , @Type , @Label , @Haunch , @X , @Y , @Z , @Restrain , @ntop , @btop , @ttop , @ctop ," +
                "@bbot , @tbot , @cbot , @D , @tw , @Hc , @ts , @w , @th , @bh , @Leff , @bs , @bleft , @bright , @aleft , @aright , @drt ," +
                "@art , @crt , @drb , @arb , @crb  , @S , @nst , @Hst , @tst , @nsb , @Hsb , @tsb , @ns , @Lb , @d0 , @Hw ," +
                "@Ac , @Ic , @As1 , @Is1 , @Ah , @Ih , @Srt , @Srb , @Irt , @Irb  )";

                command = new SQLiteCommand(sql, con);

               for (int i = 0; i < Node.Count; i++)
                {
                    command.Parameters.AddWithValue("@Joint", Node[i].Joint);
                    command.Parameters.AddWithValue("@BeamID", Node[i].BeamID);
                    command.Parameters.AddWithValue("@Type", Node[i].Type);
                    command.Parameters.AddWithValue("@Label", Node[i].Label);
                    command.Parameters.AddWithValue("@Haunch", Node[i].Haunch);
                    command.Parameters.AddWithValue("@X", Node[i].X);
                    command.Parameters.AddWithValue("@Y", Node[i].Y);
                    command.Parameters.AddWithValue("@Z", Node[i].Z);
                    command.Parameters.AddWithValue("@Restrain", Node[i].Restrain);
                    command.Parameters.AddWithValue("@ntop", Node[i].ntop);
                    command.Parameters.AddWithValue("@btop", Node[i].btop);
                    command.Parameters.AddWithValue("@ttop", Node[i].ttop);
                    command.Parameters.AddWithValue("@ctop", Node[i].ctop);
                    command.Parameters.AddWithValue("@bbot", Node[i].bbot);
                    command.Parameters.AddWithValue("@tbot", Node[i].tbot);
                    command.Parameters.AddWithValue("@cbot", Node[i].cbot);
                    command.Parameters.AddWithValue("@D", Node[i].D);
                    command.Parameters.AddWithValue("@tw", Node[i].tw);
                    command.Parameters.AddWithValue("@Hc", Node[i].Hc);
                    command.Parameters.AddWithValue("@ts", Node[i].ts);
                    command.Parameters.AddWithValue("@w", Node[i].w);
                    command.Parameters.AddWithValue("@th", Node[i].th);
                    command.Parameters.AddWithValue("@bh", Node[i].bh);
                    command.Parameters.AddWithValue("@Leff", Node[i].Leff);
                    command.Parameters.AddWithValue("@bs", Node[i].bs);
                    command.Parameters.AddWithValue("@bleft", Node[i].bleft);
                    command.Parameters.AddWithValue("@bright", Node[i].bright);
                    command.Parameters.AddWithValue("@aleft", Node[i].aleft);
                    command.Parameters.AddWithValue("@aright", Node[i].aright);
                    command.Parameters.AddWithValue("@e", Node[i].e);
                    command.Parameters.AddWithValue("@drt", Node[i].drt);
                    command.Parameters.AddWithValue("@art", Node[i].art);
                    command.Parameters.AddWithValue("@crt", Node[i].crt);
                    command.Parameters.AddWithValue("@drb", Node[i].drb);
                    command.Parameters.AddWithValue("@arb", Node[i].arb);
                    command.Parameters.AddWithValue("@crb", Node[i].crb);
                    command.Parameters.AddWithValue("@S", Node[i].S);
                    command.Parameters.AddWithValue("@nst", Node[i].nst);
                    command.Parameters.AddWithValue("@Hst", Node[i].Hst);
                    command.Parameters.AddWithValue("@tst", Node[i].tst);
                    command.Parameters.AddWithValue("@nsb", Node[i].nsb);
                    command.Parameters.AddWithValue("@Hsb", Node[i].Hsb);
                    command.Parameters.AddWithValue("@tsb", Node[i].tsb);
                    command.Parameters.AddWithValue("@ns", Node[i].ns);
                    command.Parameters.AddWithValue("@Lb", Node[i].Lb);
                    command.Parameters.AddWithValue("@d0", Node[i].d0);
                    command.Parameters.AddWithValue("@Hw", Node[i].Hw);
                    command.Parameters.AddWithValue("@Ac", Node[i].Ac);
                    command.Parameters.AddWithValue("@Ic", Node[i].Ic);
                    command.Parameters.AddWithValue("@As1", Node[i].As1);
                    command.Parameters.AddWithValue("@Is1", Node[i].Is1);
                    command.Parameters.AddWithValue("@Ah", Node[i].Ah);
                    command.Parameters.AddWithValue("@Ih", Node[i].Ih);
                    command.Parameters.AddWithValue("@Srt", Node[i].Srt);
                    command.Parameters.AddWithValue("@Srb", Node[i].Srb);
                    command.Parameters.AddWithValue("@Irt", Node[i].Irt);
                    command.Parameters.AddWithValue("@Irb", Node[i].Irb);
                    command.ExecuteNonQuery();

                }

                scope.Complete();
            }          

            con.Close();
        }

        public static void WriteNodeex(List<Node> Node, string tablename)
        {
            SQLiteConnection con = new SQLiteConnection(stringconnection());
            con.Open();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = "delete from " + tablename;
            command.ExecuteNonQuery();

            string CommandText = "insert into " + tablename + " values (@Joint , @BeamID , @Type , @Label , @Haunch , @X , @Y , @Z , @Restrain , @ntop , @btop , @ttop , @ctop ," +
               "@bbot , @tbot , @cbot , @D , @tw , @Hc , @ts , @w , @th , @bh , @Leff , @bs , @bleft , @bright , @aleft , @aright, @e , @drt ," +
               "@art , @crt , @drb , @arb , @crb  , @S , @nst , @Hst , @tst , @nsb , @Hsb , @tsb , @ns , @Lb , @d0 , @Hw ," +
               "@Ac , @Ic , @As1 , @Is1 , @Ah , @Ih , @Srt , @Srb , @Irt , @Irb )";

            SQLiteTransaction transaction = con.BeginTransaction();
            foreach (var item in Node)
                con.Execute(CommandText, item);
            
            transaction.Commit();
            con.Close();
        }

        public static void WriteSec(List<Sec> Sec, string tablename)
        {
            SQLiteConnection con = new SQLiteConnection(stringconnection());
            con.Open();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = "delete from " + tablename;
            command.ExecuteNonQuery();

            string CommandText = "insert into " + tablename + " values (@Element, @Node, @Station, @A1 , @YL1 , @YU1 , @Ix1 , @Iy1 , @SL1 , @SU1 , @J1," +
                "@A2s, @YL2s, @YU2s, @Ix2s, @Iy2s, @SL2s, @SU2s, @J2s," +
                 "@A2l, @YL2l, @YU2l, @Ix2l, @Iy2l, @SL2l, @SU2l, @J2l," +
                  "@A3s, @YL3s, @YU3s, @Ix3s, @Iy3s, @SL3s, @SU3s, @J3s," +
                    "@A3l, @YL3l, @YU3l, @Ix3l, @Iy3l, @SL3l, @SU3l, @J3l," +
                    "@A4s, @YL4s, @YU4s, @Ix4s, @Iy4s, @SL4s, @SU4s, @J4s," +
                    "@A4l, @YL4l, @YU4l, @Ix4l, @Iy4l, @SL4l, @SU4l, @J4l," +
                    "@A5s, @YL5s, @YU5s, @Ix5s, @Iy5s, @SL5s, @SU5s, @J5s)";

            SQLiteTransaction transaction = con.BeginTransaction();
            foreach (var item in Sec)
                con.Execute(CommandText, item);
            
            transaction.Commit();
            con.Close();
        }

        public static void WriteElmForces(List<ElmForces> ElmForces, string tablename)
        {
            SQLiteConnection con = new SQLiteConnection(stringconnection());
            con.Open();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = "delete from " + tablename;
            command.ExecuteNonQuery();

            string CommandText = "insert into " + tablename + " values (@Element , @Node, @Station , @Description , @DC1 , @DC2 , @DC3 , @DC4 , @DW , @Truckmax," +
                 "@Truckmin, @Lanemax, @Lanemin, @PLmax, @PLmin, @LLfmax, @LLfmin, @LLmax, @LLmin)";

            SQLiteTransaction transaction = con.BeginTransaction();
            foreach (var item in ElmForces)            
                con.Execute(CommandText, item);
            
            transaction.Commit();
            con.Close();
        }

        public static void WriteNodeForces(List<NodeForces> NodeForces, string tablename)
        {
            SQLiteConnection con = new SQLiteConnection(stringconnection());
            con.Open();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = "delete from " + tablename;
            command.ExecuteNonQuery();

            string CommandText = "insert into " + tablename + " values (@Node, @Station , @Description , @DC1 , @DC2 , @DC3 , @DC4 , @DW , @Truckmax," +
                 "@Truckmin, @Lanemax, @Lanemin, @PLmax, @PLmin, @LLfmax, @LLfmin, @LLmax, @LLmin)";

            SQLiteTransaction transaction = con.BeginTransaction();
            foreach (var item in NodeForces)
                con.Execute(CommandText, item);
            
            transaction.Commit();
            con.Close();
        }

        public static void WriteStress(List<Stress> Stress, string tablename)
        {
            SQLiteConnection con = new SQLiteConnection(stringconnection());
            con.Open();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = "delete from " + tablename;
            command.ExecuteNonQuery();

            string CommandText = "insert into " + tablename + " values (@Element, @Node , @Station , @S1_top , @S1_bot , @S2_top , @S2_bot , @S3_top_short , @S3_bot_short," +
                 "@S3_top_long, @S3_bot_long, @S4_top, @S4_bot, @Sw_top, @Sw_bot, @Slmax_top, @Slmax_bot, @Slmin_top, @Slmin_bot, @Sfmax_top, @Sfmax_bot, @Sfmin_top, @Sfmin_bot, " +
                 "@Sc_top, @Sc_bot, @Su_top, @Su_bot, @Ss1_top, @Ss1_bot, @Ss2_top, @Ss2_bot, @Sf_top, @Sf_bot)";

            SQLiteTransaction transaction = con.BeginTransaction();
            foreach (var item in Stress)
                con.Execute(CommandText, item);

            transaction.Commit();
            con.Close();
        }

        //Write List Checkt to tablename-table in database
        public static void WriteList<T>(List<T> Check, string tablename)
        {
            int n = Check[0].GetType().GetProperties().Length;
            var prop = Check[0].GetType().GetProperties();

            string pname;
            string sql = "create table IF NOT EXISTS " + tablename + " (";
            for (int i = 0; i < n; i ++)
            {
                if (prop[i].PropertyType.Name == "Int32")
                    pname = "int";
                else if (prop[i].PropertyType.Name == "String")
                    pname = "varchar(50)";
                else if (prop[i].PropertyType.Name == "Double")
                    pname = "double";
                else
                    pname = prop[i].PropertyType.Name;

                sql = sql + prop[i].Name + " " + pname + " ,";
            }
            sql = sql.Remove(sql.Length - 1);
            sql = sql + ")";

            SQLiteConnection con = new SQLiteConnection(stringconnection());
            con.Open();
            SQLiteCommand command = new SQLiteCommand(con);
            command.CommandText = sql;
            command.ExecuteNonQuery();
            command.CommandText = "delete from " + tablename;
            command.ExecuteNonQuery();

            string CommandText = "insert into " + tablename + " values (";
            for (int i = 0; i < n; i++)
            {
                CommandText = CommandText + "@" + prop[i].Name + " ,";
            }
            CommandText = CommandText.Remove(CommandText.Length - 1);
            CommandText = CommandText + ")";

            SQLiteTransaction transaction = con.BeginTransaction();
            foreach (var item in Check)
                con.Execute(CommandText, item);

            transaction.Commit();
            con.Close();
        }


        //Write all table to DB
        public static void Savedata(string bridgename, int ngirder, string txtspan, List<Mat> Mat, List<Mat> Matuse, int Trucktype, int Truckgrade, double Laneload, double Pload, 
            double ADTT, double Overloading, double Pforms, double Pparapet, double tAsphalt, double gAsphalt, List<double> Lanefactor, List<Tuple<double, double>> Truckaxle, 
            double[,] Across, double[,] Atran, double[,] Asection, List<string> Support, List<Shoe> Shoe, double[,] Ahaunch, double[,] Acbox, double[,] Acon, double[,] Atop, double[,] Abot, 
            double[,] Aweb, double ts, double th, double bh, double drt, double art, double crt, double drb, double arb, double crb, double Sr, double Sd, double Ss, int Sindex, double w, double D1, 
            double ctop, double cbot, double[,] Atranstiff, double[,] Aribbot, double[,] Aribtop, double ns, List<Crossbeam> Crossbeam, List<Parapet> Parapet, List<KFrame> KFrame, 
            List<int> Divindex, double numseg1, double numseg2)
        {           

            List<string> sql = new List<string>();
            //General
            sql.Add("delete from A01General");
            sql.Add("insert into A01General (bridgename, ngirder, txtspan) Values ('" + bridgename + "' , '" + ngirder + "' , '" + txtspan + "' )");

            //Material
            if (Mat != null)
            {
                sql.Add("delete from A02Mat");
                for (int i = 0; i < Mat.Count; i++)
                    sql.Add("insert into A02Mat (Name, Type, Ws, Es, G, Fy, Fu, Wc, fc, Ec, Lib) Values ('" + Mat[i].Name + "' , '" + Mat[i].Type + "' , '" + Mat[i].Ws + "' , '" + Mat[i].Es
                    + "' , '" + Mat[i].G + "' , '" + Mat[i].Fy + "' , '" + Mat[i].Fu + "' , '" + Mat[i].Wc + "' , '" + Mat[i].fc + "' , '" + Mat[i].Ec + "' , '" + Mat[i].Lib + "' )");
            }

            if (Matuse != null)
            {
                sql.Add("delete from A03Matuse");
                for (int i = 0; i < Matuse.Count; i++)
                    sql.Add("insert into A03Matuse (Name, Type, Ws, Es, G, Fy, Fu, Wc, fc, Ec, Lib) Values ('" + Matuse[i].Name + "' , '" + Matuse[i].Type + "' , '" + Matuse[i].Ws + "' , '" + Matuse[i].Es
                    + "' , '" + Matuse[i].G + "' , '" + Matuse[i].Fy + "' , '" + Matuse[i].Fu + "' , '" + Matuse[i].Wc + "' , '" + Matuse[i].fc + "' , '" + Matuse[i].Ec + "' , '" + Matuse[i].Lib + "' )");
            }


            //Loading
            sql.Add("delete from A04Loading");
            sql.Add("insert into A04Loading (Trucktype, Truckgrade, Laneload, Pload, ADTT , Overloading, Pforms, Pparapet, tAsphalt, gAsphalt, Lane1, Lane2, Lane3, Lane4, Lane5) Values " +
                "('" + Trucktype + "' , '" + Truckgrade + "' , '" + Laneload + "' , '" + Pload + "' , '" + ADTT + "' , '" + Overloading + "' , '" + Pforms + "' , '" + Pparapet + "' , '" + tAsphalt + "' , '" + gAsphalt + "' , '" + Lanefactor[0] + "' , '" + Lanefactor[1] + "' , '" + Lanefactor[2] + "' , '" + Lanefactor[3] + "' , '" + Lanefactor[4] + "' )");

            sql.Add("delete from A05Truck");
            for (int i = 0; i < Truckaxle.Count; i++)
                sql.Add("insert into A05Truck (Coor, ALoad) Values ('" + Truckaxle[i].Item1 + "' , '" + Truckaxle[i].Item2 + "' )");

            //Grid Tab
            sql.Add("delete from A06Across");
            for (int i = 0; i < Across.GetLength(1); i++)
                sql.Add("insert into A06Across (Type, ID, Length) Values ('" + Across[0, i] + "' , '" + Across[1, i] + "' , '" + Across[2, i] + "' )");

            sql.Add("delete from A07Atran");
            for (int i = 0; i < Atran.GetLength(1); i++)
                sql.Add("insert into A07Atran (Type, ID, Length) Values ('" + Atran[0, i] + "' , '" + Atran[1, i] + "' , '" + Atran[2, i] + "' )");

            sql.Add("delete from A08Asection");
            for (int i = 0; i < Asection.GetLength(1); i++)
                sql.Add("insert into A08Asection (Length, ID) Values ('" + Asection[0, i] + "' , '" + Asection[1, i] + "' )");

            sql.Add("delete from A24Support");
            for (int i = 0; i < Support.Count; i++)
                sql.Add("insert into A24Support (Support ) Values ('" + Support[i] + "' )");

            sql.Add("delete from A25Shoe");
            for (int i = 0; i < Shoe.Count; i++)
                sql.Add("insert into A25Shoe (Girder, Support, Label, EA, A, B, Joint, X, Y, Z, D, Type) Values ('" + Shoe[i].Girder + "' , '" + Shoe[i].Support + "' , '" + Shoe[i].Label + "' , '" + Shoe[i].EA + "' , '" + Shoe[i].A + "' , '" + Shoe[i].B + "' , '" + Shoe[i].Joint + "' , '" + Shoe[i].X + "' , '" + Shoe[i].Y + "' , '" + Shoe[i].Z + "' , '" + Shoe[i].D + "' , '" + Shoe[i].Type + "' )");


            //Grid Haunch
            sql.Add("delete from A09Ahaunch");
            for (int i = 0; i < Ahaunch.GetLength(0); i++)
                sql.Add("insert into A09Ahaunch (L1, L2, L3, H1, H2, H3) Values ('" + Ahaunch[i, 0] + "' , '" + Ahaunch[i, 1] + "' , '" + Ahaunch[i, 2] + "' , '" + Ahaunch[i, 3] + "' , '" + Ahaunch[i, 4] + "' , '" + Ahaunch[i, 5] + "' )");

            sql.Add("delete from A10Acbox");
            for (int i = 0; i < Acbox.GetLength(0); i++)
                sql.Add("insert into A10Acbox (L1, L2) Values ('" + Acbox[i, 0] + "' , '" + Acbox[i, 1] + "' )");

            sql.Add("delete from A11Acon");
            for (int i = 0; i < Acon.GetLength(1); i++)
                sql.Add("insert into A11Acon (Length, H1, H2, H3, H4, H5, H6, H7, H8, H9, H10, ID) Values ('" + Acon[0, i] + "' , '" + Acon[1, i] + "' , '" + Acon[2, i] + "' , '" + Acon[3, i] + "' , '" + Acon[4, i] + "' , '" + Acon[5, i] + "' , '" + Acon[6, i] + "' , '" + Acon[7, i] + "' , '" + Acon[8, i] + "' , '" + Acon[9, i] + "' , '" + Acon[10, i] + "' , '" + Acon[11, i] + "' )");

            //Grid Dim
            sql.Add("delete from A12Atop");
            for (int i = 0; i < Atop.GetLength(1); i++)
                sql.Add("insert into A12Atop (Type, ID, Length, Width, Thickness) Values ('" + Atop[0, i] + "' , '" + Atop[1, i] + "' , '" + Atop[2, i] + "' , '" + Atop[3, i] + "' , '" + Atop[4, i] + "' )");

            sql.Add("delete from A13Abot");
            for (int i = 0; i < Abot.GetLength(1); i++)
                sql.Add("insert into A13Abot (Length, Thickness) Values ('" + Abot[0, i] + "' , '" + Abot[1, i] + "' )");

            sql.Add("delete from A14Aweb");
            for (int i = 0; i < Aweb.GetLength(1); i++)
                sql.Add("insert into A14Aweb (Length, Thickness) Values ('" + Aweb[0, i] + "' , '" + Aweb[1, i] + "' )");

            sql.Add("delete from A15ADims");
            sql.Add("insert into A15ADims (ts, th, bh, drt, art, crt, drb, arb, crb, Sr, Sd, Ss, Sindex, w, D1, ctop, cbot) Values ('" + ts + "' , '" + th + "' , '" + bh + "' , '" + drt + "' , '" + art + "' , '" + crt + "' , '" + drb + "' , '" + arb + "' , '" + crb + "' , '" + Sr + "' , '" + Sd + "' , '" + Ss + "' , '" + Sindex + "' , '" + w + "' , '" + D1 + "' , '" + ctop + "' , '" + cbot + "' )");

            //Stiff
            sql.Add("delete from A16Atranstiff");
            for (int i = 0; i < Atranstiff.GetLength(1); i++)
                sql.Add("insert into A16Atranstiff (Type, ID, Length) Values ('" + Atranstiff[0, i] + "' , '" + Atranstiff[1, i] + "' , '" + Atranstiff[2, i] + "' )");

            sql.Add("delete from A17Aribbot");
            for (int i = 0; i < Aribbot.GetLength(1); i++)
                sql.Add("insert into A17Aribbot (Length, Amount, Depth, Thickness) Values ('" + Aribbot[0, i] + "' , '" + Aribbot[1, i] + "' , '" + Aribbot[2, i] + "' , '" + Aribbot[3, i] + "' )");

            sql.Add("delete from A18Aribtop");
            for (int i = 0; i < Aribtop.GetLength(1); i++)
                sql.Add("insert into A18Aribtop (Type, ID, Length, Amount, Depth, Thickness) Values ('" + Aribtop[0, i] + "' , '" + Aribtop[1, i] + "' , '" + Aribtop[2, i] + "' , '" + Aribtop[3, i] + "' , '" + Aribtop[4, i] + "' , '" + Aribtop[5, i] + "' )");

            sql.Add("delete from A19ns");
            sql.Add("insert into A19ns (ns ) Values ('" + ns + "' )");

            //Other
            sql.Add("delete from A20Crossbeam");
            for (int i = 0; i < Crossbeam.Count; i++)
                sql.Add("insert into A20Crossbeam (type, ttop, btop, tbot, bbot, D, tw, nw ) Values ('" + Crossbeam[i].type + "' , '" + Crossbeam[i].ttop + "' , '" + Crossbeam[i].btop + "' , '" + Crossbeam[i].tbot + "' , '" + Crossbeam[i].bbot + "' , '" + Crossbeam[i].D + "' , '" + Crossbeam[i].tw + "' , '" + Crossbeam[i].nw + "' )");

            sql.Add("delete from A21Parapet");
            for (int i = 0; i < Parapet.Count; i++)
                sql.Add("insert into A21Parapet (type, H1, H2 , H3 , B1 , B2 , B3 , e ) Values ('" + Parapet[i].type + "' , '" + Parapet[i].H1 + "' , '" + Parapet[i].H2 + "' , '" + Parapet[i].H3 + "' , '" + Parapet[i].B1 + "' , '" + Parapet[i].B2 + "' , '" + Parapet[i].B3 + "' , '" + Parapet[i].e + "' )");

            sql.Add("delete from A22KFrame");
            for (int i = 0; i < KFrame.Count; i++)
                sql.Add("insert into A22KFrame (Station, Location, Description ) Values ('" + KFrame[i].Station + "' , '" + (KFrame[i].Location == true ? 1 : 0) + "' , '" + KFrame[i].Description + "' )");

            //Analysis
            sql.Add("delete from A23Analysis");
            sql.Add("insert into A23Analysis (KFrame, Schanged, numseg1, numseg2) Values ('" + Divindex[0] + "' , '" + Divindex[1] + "' , '" + numseg1 + "' , '" + numseg2 + "' )");

            //Write to DB
            SQLiteConnection con = new SQLiteConnection(stringconnection());
            con.Open();
            SQLiteCommand command = new SQLiteCommand(con);

            SQLiteTransaction transaction = con.BeginTransaction();
            for (int i = 0; i < sql.Count; i++)
            {
                command.CommandText = sql[i];
                command.ExecuteNonQuery();
            }

            transaction.Commit();
            con.Close();



        }

        

        //Write Nodeex to DB


        //Read from DB ---------------------------------------------------------------------------



        //Read DB and return DataTable
        public static DataTable getDataTable(string str)
        {
            SQLiteConnection con = new SQLiteConnection(stringconnection());
            //con.Open();
            SQLiteCommand command = new SQLiteCommand(con);
            command.CommandText = str;
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            //con.Close();

            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        //Read DB and return Matrix
        public static double[,] getMatrix(string str, bool direction)
        {
            SQLiteConnection con = new SQLiteConnection(stringconnection());
            //con.Open();
            SQLiteCommand command = new SQLiteCommand(con);
            command.CommandText = "select* from " + str;
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            //con.Close();

            DataTable dt = new DataTable();
            da.Fill(dt);

            if (direction)
            {
                double[,] M = new double[dt.Rows.Count, dt.Columns.Count];
                for (int i = 0; i < M.GetLength(0); i++)
                    for (int j = 0; j < M.GetLength(1); j++)
                        M[i, j] = (double)dt.Rows[i][j];
                return M;
            }
            else
            {

                double[,] M = new double[dt.Columns.Count, dt.Rows.Count];
                for (int i = 0; i < M.GetLength(0); i++)
                    for (int j = 0; j < M.GetLength(1); j++)
                        M[i, j] = (double)dt.Rows[j][i];
                return M;
            } 
        }

        ////Write material to DB
        //public static void Writemat(Mat Mat1, string table_name)
        //{
        //    SQLiteConnection con = new SQLiteConnection(stringconnection());
        //    con.Open();
        //    SQLiteCommand command = new SQLiteCommand(con);

        //    command.CommandText = "insert into " + table_name + " (Name, Type, Ws, Es, G, Fy, Fu, Wc, fc, Ec) Values ('" + Mat1.Name + "' , '" + Mat1.Type + "' , '" + Mat1.Ws + "' , '" + Mat1.Es
        //        + "' , '" + Mat1.G + "' , '" + Mat1.Fy + "' , '" + Mat1.Fu + "' , '" + Mat1.Wc + "' , '" + Mat1.fc + "' , '" + Mat1.Ec + "' )";
        //    command.ExecuteNonQuery();
        //    con.Close();
        //}

        ////Delete material
        //public static void delmat(string str)
        //{
        //    SQLiteConnection con = new SQLiteConnection(stringconnection());
        //    con.Open();
        //    SQLiteCommand command = new SQLiteCommand(con);
        //    command.CommandText = "delete from ILmat Where Name = '" + str + "'";
        //    command.ExecuteNonQuery();
        //    con.Close();
        //}

        ////Modify material
        //public static void upadatemat(Mat Mat1)
        //{
        //    SQLiteConnection con = new SQLiteConnection(stringconnection());
        //    con.Open();
        //    SQLiteCommand command = new SQLiteCommand(con);
        //    command.CommandText = "UPDATE ILmat SET Type = '" + Mat1.Type + "', Ws = '" + Mat1.Ws + "', Es = '" + Mat1.Es + "', G = '" + Mat1.G + "', Fy = '" + Mat1.Fy + "', Fu = '" + Mat1.Fu + "', Wc = '" + Mat1.Wc + "', fc = '" + Mat1.fc
        //        + "', Ec = '" + Mat1.Ec + "' WHERE Name = '" + Mat1.Name + "'";

        //    command.ExecuteNonQuery();
        //    con.Close();
        //}

        //Load from List
        public static List<Mat> getListmat(string table_name)
        {
            IDbConnection cnn = new SQLiteConnection(stringconnection());
            List<Mat> output = cnn.Query<Mat>("select * from " + table_name).ToList();
            return output;
        }

        //public static List<Support> getLisSupport(string table_name)
        //{
        //    IDbConnection cnn = new SQLiteConnection(stringconnection());
        //    List<Support> output = cnn.Query<Support>("select * from " + table_name).ToList();
        //    return output;
        //}

        public static List<Crossbeam> getListCrossbeam(string table_name)
        {
            IDbConnection cnn = new SQLiteConnection(stringconnection());
            List<Crossbeam> output = cnn.Query<Crossbeam>("select * from " + table_name).ToList();
            return output;
        }

        public static List<Shoe> getListShoe(string table_name)
        {
            SQLiteConnection con = new SQLiteConnection(stringconnection());
            SQLiteCommand command = new SQLiteCommand(con);
            command.CommandText = "select * from A25Shoe";
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);

            DataTable dt = new DataTable();
            da.Fill(dt);

            List<Shoe> Shoe = new List<Shoe>();
            for (int i = 0; i < dt.Rows.Count; i ++)
            {
                Shoe Shoe1 = new Shoe();
                Shoe1.Girder = Convert.ToInt32(dt.Rows[i][0].ToString());
                Shoe1.Support = Convert.ToInt32(dt.Rows[i][1].ToString());
                Shoe1.Label = dt.Rows[i][2].ToString();
                Shoe1.EA = Convert.ToInt32(dt.Rows[i][3].ToString());
                Shoe1.A = Convert.ToDouble(dt.Rows[i][4].ToString());
                Shoe1.B = Convert.ToDouble(dt.Rows[i][5].ToString());
                Shoe1.X = Convert.ToDouble(dt.Rows[i][7].ToString());
                Shoe1.Y = Convert.ToDouble(dt.Rows[i][8].ToString());                
                Shoe1.Type = dt.Rows[i][11].ToString();

                Shoe.Add(Shoe1);
            }

            return Shoe;
        }

        public static List<Parapet> getListParapet(string table_name)
        {
            IDbConnection cnn = new SQLiteConnection(stringconnection());
            List<Parapet> output = cnn.Query<Parapet>("select * from " + table_name).ToList();
            return output;
        }

        public static List<Node> getListnode(string table_name, string con1)
        {
            IDbConnection cnn = new SQLiteConnection(con1);
            List<Node> output = cnn.Query<Node>("select * from " + table_name).ToList();
            return output;
        }

        //Print

        public static DataTable getNode()
        {            
            SQLiteConnection con = new SQLiteConnection(stringconnection());
            SQLiteCommand command = new SQLiteCommand(con);
            command.CommandText = "select Joint, Label, X, Y, Z, btop, ttop, ctop, bbot, tbot, cbot, D, S, tw, Hc, ts, w, th, bh, drt, art, crt, drb, arb, crb, nst, Hst, tst, nsb, Hsb, tsb, ns from BNode Where BeamID <= 10";
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);

            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public static DataTable getSec(string Sectype, List<string> stage, int ngirder)
        {
            string sql1 = "";
            string a1 = "";
            for (int i = 0; i < stage.Count; i++)
            {
                if (stage[i] == "Type 1")
                    a1 = "1";
                else if (stage[i] == "Type 2")
                    a1 = "2s";
                else if (stage[i] == "Type 3")
                    a1 = "3s";
                else if (stage[i] == "Type 4")
                    a1 = "4s";
                else if (stage[i] == "Type 5")
                    a1 = "5s";

                sql1 = sql1 + " ," + Sectype + a1;
                //sql1.Remove(sql1.Length - 1);
            } 
            
            string sql2 = ngirder == 10 ? "" : " Where Node < " + ((ngirder + 2) * 100).ToString() + " AND Node > " + ((ngirder + 1) * 100).ToString(); 
            SQLiteConnection con = new SQLiteConnection(stringconnection());            
            SQLiteCommand command = new SQLiteCommand(con);
            command.CommandText = "select Station " + sql1 +" from BSec " + sql2;
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);            

            DataTable dt = new DataTable();
            da.Fill(dt);            
            return dt;            
        }

        public static DataTable getSec2(int ngirder)
        {
           
            string sql2 = ngirder == 10 ? "" : " Where Node < " + ((ngirder + 2) * 100).ToString() + " AND Node > " + ((ngirder + 1) * 100).ToString();
            SQLiteConnection con = new SQLiteConnection(stringconnection());
            SQLiteCommand command = new SQLiteCommand(con);
            command.CommandText = "select Element, Node, Station, A1, Ix1, Iy1, YU1, YL1, J1, A2s, Ix2s, Iy2s, YU2s, YL2s, J2s, A3s, Ix3s, Iy3s, YU3s, YL3s, J3s, A4s, Ix4s, Iy4s, YU4s, YL4s, J4s, A5s, Ix5s, Iy5s, YU5s, YL5s, J5s from BSec " + sql2;
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);

            DataTable dt = new DataTable();
            da.Fill(dt);           

            return dt;
        }

        public static DataTable getMST(int MST, int n)
        {
            string MSTs = "BMoment";
            if (MST == 0)
                MSTs = "BMoment";
            else if (MST == 1)
                MSTs = "BShear";
            else if (MST == 2)
                MSTs = "BTorsion";
            else if (MST == 3)
                MSTs = "BDeflection";
            else if (MST == 4)
                MSTs = "BReaction";

            string sql = n == 0 ? "" : " Where Node < " + ((n + 1) * 100).ToString() + " AND Node > " + (n * 100).ToString();


            SQLiteConnection con = new SQLiteConnection(stringconnection());
            SQLiteCommand command = new SQLiteCommand(con);
            command.CommandText = "select Node, Station, Description, DC1, DC2, DC3, DC4, DW, LLmax, LLmin, LLfmax, LLfmin from " + MSTs + sql;
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);

            DataTable dt = new DataTable();
            da.Fill(dt);
           
            return dt;
        }

        public static List<ElmPrint> getForces(int type, int n)
        {
            string tablename = "BMoment";
            if (type == 0)
                tablename = "BMoment";
            else if (type == 1)
                tablename = "BShear";
            else if (type == 2) 
                tablename = "BTorsion";
            else if (type == 3)
                tablename = "BDeflection";
            else if (type == 4)
                tablename = "BReaction";

            string sql = n == 10 ? "" : " Where Node < " + ((n + 2) * 100).ToString() + " AND Node > " + ((n + 1) * 100).ToString();

            IDbConnection cnn = new SQLiteConnection(stringconnection());
            List<ElmPrint> output = cnn.Query<ElmPrint>("select Node, Station, Description, DC1, DC2, DC3, DC4, DW, LLmax, LLmin, LLfmax, LLfmin from " + tablename + sql).ToList();
            return output;
        }

        public static DataTable getStress(List<string> stage, int ngirder, string topbot)
        {
            string sql1 = "";
            string a1 = "";
            for (int i = 0; i < stage.Count; i++)
            {
                if (stage[i] == "DC1")
                    a1 = "S1_" + topbot;
                else if (stage[i] == "DC2")
                    a1 = "S2_" + topbot;
                else if (stage[i] == "DC3")
                    a1 = "S3_" + topbot + "_long";
                else if (stage[i] == "DC4")
                    a1 = "S4_" + topbot;
                else if (stage[i] == "DW")
                    a1 = "Sw_" + topbot;
                else if (stage[i] == "LLmax")
                    a1 = "Slmax_" + topbot;
                else if (stage[i] == "LLmin")
                    a1 = "Slmin_" + topbot;
                else if (stage[i] == "Cons")
                    a1 = "Sc_" + topbot;
                else if (stage[i] == "ULS-I")
                    a1 = "Su_" + topbot;
                else if (stage[i] == "SLS-I")
                    a1 = "Ss1_" + topbot;
                else if (stage[i] == "SLS-II")
                    a1 = "Ss2_" + topbot;
                else if (stage[i] == "FLS")
                    a1 = "Sf_" + topbot;

                sql1 = sql1 + " ," + a1;               
            }

            string sql2 = ngirder == 10 ? "" : " Where Node < " + ((ngirder + 2) * 100).ToString() + " AND Node > " + ((ngirder + 1) * 100).ToString();
            SQLiteConnection con = new SQLiteConnection(stringconnection());
            SQLiteCommand command = new SQLiteCommand(con);
            
            command.CommandText = "select Station " + sql1 + " from BStress " + sql2;

            SQLiteDataAdapter da = new SQLiteDataAdapter(command);

            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataTable getCumStress(List<string> stage, int ngirder, string topbot)
        {
            string sql1 = "";
            string a1 = "";
            for (int i = 0; i < stage.Count; i++)
            {                
                if (stage[i] == "Cons")
                    a1 = "S1_" + topbot +", S2_" + topbot + ", S3_" + topbot + "_short";
                else if (stage[i] == "ULS-I")
                    a1 = "Su_" + topbot;
                else if (stage[i] == "SLS-I")
                    a1 = "Ss1_" + topbot;
                else if (stage[i] == "SLS-II")
                    a1 = "Ss2_" + topbot;
                else if (stage[i] == "FLS")
                    a1 = "Sf_" + topbot;

                sql1 = sql1 + " ," + a1;
            }

            string sql2 = ngirder == 10 ? "" : " Where Node < " + ((ngirder + 2) * 100).ToString() + " AND Node > " + ((ngirder + 1) * 100).ToString();
            SQLiteConnection con = new SQLiteConnection(stringconnection());
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = "select Station " + sql1 + " from BStress " + sql2;

            SQLiteDataAdapter da = new SQLiteDataAdapter(command);

            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        //Sectional checking
        public static DataTable getresultdata(string field1, int n, string tablename)
        {
            string[] field = field1.Split(',');
            string sql = " Where Joint < " + ((n + 2) * 100).ToString() + " AND Joint > " + ((n + 1) * 100).ToString();
            string sql1 = "";            
            for (int i = 0; i < field.GetLength(0); i++)
                sql1 = sql1 + field[i] + " ,";
                sql1 = sql1.Remove(sql1.Length - 1);
            

            SQLiteConnection con = new SQLiteConnection(stringconnection());
            SQLiteCommand command = new SQLiteCommand(con);
            command.CommandText = "select " + sql1 + "from " + tablename + sql;
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);

            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public static void WriteResulttoDB(Results R)
        {
            WriteElmForces(R.Moment, "BMoment");
            WriteElmForces(R.Shear, "BShear");           
            WriteElmForces(R.Torsion, "BTorsion");            
            WriteNodeForces(R.Deflection, "BDeflection");            
            WriteNodeForces(R.Reaction, "BReaction"); 
            WriteStress(R.Stress, "BStress");
            WriteList(R.Check_Cons, "CCheckCons");
            WriteList(R.Check_ULS, "CCheckULS");
            WriteList(R.Check_SLS, "CCheckSLS");
            WriteList(R.Check_FLS, "CCheckFLS");            
            WriteList(R.Node2, "CNode2");
        }

        public static List<T> getListdata<T>(int n, string field, string tablename)
        {            
            string condition = n == 0 ? "" : " Where Joint < " + ((n + 1) * 100).ToString() + " AND Joint > " + (n * 100).ToString();
            string sql = "Select " + field + " from " + tablename + condition;

            IDbConnection cnn = new SQLiteConnection(stringconnection());
            List<T> output = cnn.Query<T>(sql).ToList();
            return output;
        }

    }
}
