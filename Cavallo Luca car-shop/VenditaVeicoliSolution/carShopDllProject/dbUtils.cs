using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.IO;

namespace carShopDllProject
{
    public class dbUtils
    {
        //connectionString 
        public string conStr = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName}\\CarShop.accdb";

        public void creaTabella(string tabName)
        {
            if (conStr != null)
            {
                OleDbConnection con = new OleDbConnection(conStr);
                using (con)
                {
                    con.Open();

                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = con;

                    try
                    {
                        string command = $@"CREATE TABLE {tabName}( id INT identity(1,1) NOT NULL PRIMARY KEY, marca VARCHAR(255) NOT NULL, modello VARCHAR(255) NOT NULL,
                                colore VARCHAR(255), cilindrata INT, potenzaKw DOUBLE,
                                immatricolazione DATE, usato VARCHAR(255), kmZero VARCHAR(255),
                                kmPercorsi INT, prezzo DOUBLE,";
                        if (tabName == "AUTO") 
                            command += " numAirbag INT)";
                        else 
                            command += " marcaSella VARCHAR(255))";
                        cmd.CommandText = command;
                        cmd.ExecuteNonQuery();
                    }
                    catch (OleDbException ex)
                    {
                        Console.WriteLine($"\n\n{ex.Message}");
                        System.Threading.Thread.Sleep(2000);
                        return;
                    }
                }
            }
        }

        public void aggiungiItem(string tabName, string marca, string modello, string colore, int cilindrata,
            double potenzaKw, DateTime immatricolazione, string usata, string isKm0, int kmPercorsi, 
            double prezzo, int numAirbag, string marcaSella)
        {
            if (conStr != null)
            {
                OleDbConnection con = new OleDbConnection(conStr);
                using (con)
                {
                    con.Open();

                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = con;
                    string command = string.Empty;
                    if (tabName == "AUTO")
                        command = $"INSERT INTO {tabName}" +
                            $"(marca, modello, colore, cilindrata, potenzaKw, immatricolazione, usato, kmZero," +
                            $" kmPercorsi, prezzo, numAirbag) VALUES(@marca, @modello, @colore, @cilindrata, @potenzaKw, " +
                            $"@immatricolazione, @usata, @isKm0, @kmPercorsi, @prezzo, @numAirbag)";
                    else
                        command = $"INSERT INTO {tabName}" +
                            $"(marca, modello, colore, cilindrata, potenzaKw, immatricolazione, usato, kmZero, " +
                            $"kmPercorsi, prezzo, marcaSella) VALUES(@marca, @modello, @colore, @cilindrata, @potenzaKw," +
                            $" @immatricolazione, @usata, @isKm0, @kmPercorsi, @prezzo, @marcaSella)";
                    cmd.CommandText = command;

                    cmd.Parameters.Add(new OleDbParameter("@marca", OleDbType.VarChar, 255)).Value = marca;
                    cmd.Parameters.Add(new OleDbParameter("@modello", OleDbType.VarChar, 255)).Value = modello;
                    cmd.Parameters.Add(new OleDbParameter("@colore", OleDbType.VarChar, 255)).Value = colore;
                    cmd.Parameters.Add("@cilindrata", OleDbType.Integer).Value = cilindrata;
                    cmd.Parameters.Add("@potenzaKw", OleDbType.Double).Value = potenzaKw;
                    cmd.Parameters.Add(new OleDbParameter("@immatricolazione", OleDbType.Date)).Value = immatricolazione.ToShortDateString();
                    cmd.Parameters.Add(new OleDbParameter("@usato", OleDbType.VarChar, 255)).Value = usata;
                    cmd.Parameters.Add(new OleDbParameter("@isKm0", OleDbType.VarChar, 255)).Value = isKm0;
                    cmd.Parameters.Add("@kmPercorsi", OleDbType.Integer).Value = kmPercorsi;
                    cmd.Parameters.Add("@prezzo", OleDbType.Double).Value = prezzo;
                    if (tabName == "AUTO")
                        cmd.Parameters.Add("@numAirbag", OleDbType.Integer).Value = numAirbag;
                    else
                        cmd.Parameters.Add(new OleDbParameter("@marcaSella", OleDbType.VarChar, 255)).Value = marcaSella;
                    cmd.Prepare();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void listaTabella(string tabName)
        {
            if (conStr != null)
            {
                OleDbConnection con = new OleDbConnection(conStr);
                using (con)
                {
                    con.Open();

                    OleDbCommand cmd = new OleDbCommand($"SELECT * FROM {tabName}", con);

                    OleDbDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        Console.WriteLine("\n");
                        while (reader.Read())
                        {
                            string immatricolazione = reader.GetDateTime(6).ToShortDateString();
                            
                            string final = $"{reader.GetInt32(0)} - {reader.GetString(1)} - {reader.GetString(2)} - " +
                                            $"{reader.GetString(3)} - {reader.GetInt32(4)} - {reader.GetDouble(5)} - " +
                                            $"{immatricolazione} - {reader.GetString(7)} - {reader.GetString(8)} - " +
                                            $"{reader.GetInt32(9)} - {reader.GetDouble(10)}";
                            if (tabName == "AUTO")
                                final += $" - {reader.GetInt32(11)}";
                            else 
                                final += $" - {reader.GetString(11)}";
                            Console.WriteLine(final);
                            System.Threading.Thread.Sleep(6000);
                        }
                    }
                    else Console.WriteLine("\n\nNessun risultato.");
                    reader.Close();
                }
            }
        }

        public void eliminaElemento(string tabName, int id)
        {
            if (conStr != null)
            {
                OleDbConnection con = new OleDbConnection(conStr);
                
                using (con)
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand($"DELETE FROM {tabName} WHERE id={id}", con);
                    try
                    {
                        cmd.Prepare();
                        cmd.ExecuteNonQuery();
                    }
                    catch (OleDbException ex)
                    {
                        Console.WriteLine($"\n\n{ex.Message}");
                        System.Threading.Thread.Sleep(2000);
                        return;
                    }
                }
            }
        }

        public void eliminaTabella(string tabName)
        {
            if (conStr != null)
            {
                OleDbConnection con = new OleDbConnection(conStr);
                using (con)
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = con;
                    try
                    {
                        string command = $"DROP TABLE {tabName}";
                        cmd.CommandText = command;
                        cmd.ExecuteNonQuery();
                        
                    }
                    catch (OleDbException ex)
                    {
                        Console.WriteLine($"\n\n{ex.Message}");
                        System.Threading.Thread.Sleep(2000);
                        return;
                    }
                }
            }
        }

        public int contaElementi(string tabName)
        {
            if (conStr != null)
            {
                OleDbConnection con = new OleDbConnection(conStr);

                using (con)
                {
                    con.Open();

                    OleDbCommand command = new OleDbCommand($"SELECT MAX(id) FROM {tabName}", con);

                    OleDbDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            return rdr.GetInt32(0);
                        }
                    }
                    else Console.WriteLine("\n\nLa tabella è vuota.");
                    rdr.Close();
                }
            }
            return -1;
        }
    }
}
