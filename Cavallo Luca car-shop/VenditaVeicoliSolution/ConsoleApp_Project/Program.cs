using System;
using Microsoft.VisualBasic;
using System.Data.OleDb;
using carShopDllProject;
using System.IO;

namespace ConsoleApp_Project
{
    class Program
    {
        public static dbUtils _dbUtils = new dbUtils();
        
        static void Main(string[] args)
        {
            char scelta;
            do
            {
                menu();
                Console.Write("- DIGITA LA TUA SCELTA: ");
                scelta = Console.ReadKey().KeyChar;
                string veicolo;
                switch (scelta)
                {
                    case '1':
                        veicolo = scegliVeicolo();
                        _dbUtils.creaTabella(veicolo);
                        break;
                    case '2':
                        veicolo = scegliVeicolo();
                        if (veicolo == "AUTO")
                            sceltaAuto(veicolo);
                        else
                            sceltaMoto(veicolo);
                        break;
                    case '3':
                        veicolo = scegliVeicolo();
                        _dbUtils.listaTabella(veicolo);
                        break;
                    case '4':
                        veicolo = scegliVeicolo();
                        int id = ottieniId(veicolo);
                        _dbUtils.eliminaElemento(veicolo, id);
                        break;
                    case '5':
                        veicolo = scegliVeicolo();
                        _dbUtils.eliminaTabella(veicolo);
                        break;
                    default:
                        break;
                }
            } while (scelta != 'X' && scelta != 'x');
        }
        private static void menu()
        {
            Console.Clear();
            Console.WriteLine("*** CAR SHOP - DB MANAGEMENT ***\n");
            Console.WriteLine(" Menu:");
            Console.WriteLine(" 1 - CREA TABELLA");
            Console.WriteLine(" 2 - AGGIUNGI NUOVO ELEMENTO");
            Console.WriteLine(" 3 - LISTA VEICOLI");
            Console.WriteLine(" 4 - CANCELLA ELEMENTO");
            Console.WriteLine(" 5 - CANCELLA TABELLA");
            Console.WriteLine("\n X - ESCI\n\n");
        }

        private static void sceltaAuto(string veicolo)
        {
            string marca = Interaction.InputBox("Inserisci la marca: ", veicolo),
                   modello = Interaction.InputBox("Inserisci il modello: ", veicolo),
                   colore = Interaction.InputBox("Inserisci il colore: ", veicolo),
                   usata = Interaction.InputBox("E' già stata usata?[SI/NO] ", veicolo),
                 isKm0 = Interaction.InputBox("E' km zero?[SI/NO] ", veicolo);
            DateTime immatricolazione = Convert.ToDateTime(Interaction.InputBox("Inserisci la data d'immatricolazione: ", veicolo));
            int cilindrata = Convert.ToInt32(Interaction.InputBox("Inserisci la cilindrata: ", veicolo)),
                kmPercorsi = Convert.ToInt32(Interaction.InputBox("Inserisci i km percorsi: ", veicolo)),
                numAirbag = Convert.ToInt32(Interaction.InputBox("Inserisci il numero degli airbag: ", veicolo));
            double potenzaKw = Convert.ToDouble(Interaction.InputBox("Inserisci la potenza: ", veicolo)),
                   prezzo = Convert.ToDouble(Interaction.InputBox("Inserisci il prezzo: ", veicolo));

            _dbUtils.aggiungiItem(veicolo, marca, modello, colore, cilindrata, potenzaKw,
                    immatricolazione, usata, isKm0, kmPercorsi,prezzo, numAirbag, null);
        }

        private static void sceltaMoto(string veicolo)
        {
            string marca = Interaction.InputBox("Inserisci la marca: ", veicolo),
                   modello = Interaction.InputBox("Inserisci il modello: ", veicolo),
                   colore = Interaction.InputBox("Inserisci il colore: ", veicolo),
                   usata = Interaction.InputBox("E' già stata usata?[SI/NO] ", veicolo),
                   isKm0 = Interaction.InputBox("E' km zero?[SI/NO] ", veicolo),
                   sella = Interaction.InputBox("Inserisci la marca della sella: ", veicolo);
            DateTime immatricolazione = Convert.ToDateTime(Interaction.InputBox("Inserisci la data d'immatricolazione: ", veicolo));
            int cilindrata = Convert.ToInt32(Interaction.InputBox("Inserisci la cilindrata: ", veicolo)),
                kmPercorsi = Convert.ToInt32(Interaction.InputBox("Inserisci i km percorsi: ", veicolo));
            double potenzaKw = Convert.ToDouble(Interaction.InputBox("Inserisci la potenza: ", veicolo)),
                   prezzo = Convert.ToDouble(Interaction.InputBox("Inserisci il prezzo: ", veicolo));

            _dbUtils.aggiungiItem(veicolo, marca, modello, colore, cilindrata, potenzaKw,
                    immatricolazione, usata, isKm0, kmPercorsi,prezzo, Convert.ToInt32(null), sella);
        }

        private static string scegliVeicolo()
        {
            Console.Write("\n");
            string veicolo;
            do
            {
                Console.Write("- AUTO o MOTO? ");
                veicolo = Console.ReadLine().ToUpper();
            } while (veicolo != "AUTO" && veicolo != "MOTO");
            return veicolo;
        }

        private static int ottieniId(string tabName)
        {
            int nItems = _dbUtils.contaElementi(tabName);
            int id;
            do
            {
                Console.WriteLine($"Inserisci id dell'elemento da cancellare (da 1 a {nItems}): ");
                id = Convert.ToInt32(Console.ReadLine());
            } while (id < 1 || id > nItems);

            return id;
        }
    }
}
