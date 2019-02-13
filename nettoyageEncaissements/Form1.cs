using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;

namespace nettoyageEncaissements
{
    public partial class Form1 : Form
    {
        logApp monFicLog = new logApp();
        String loginPG = "postgres";
        String passPG = "pgpass";
        String requetes;
        String paramConnexion;
        Boolean mesTests = true;
        private OdbcConnection chaineConnexionListeBases = new OdbcConnection();
        private OdbcCommand mesRequetes;
        private OdbcDataReader retourRequetes;
        String maintenant = DateTime.Now.ToString("yyMMdd_HHmmss");
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            String ficLog = Application.StartupPath + "log_" + maintenant + ".txt";

            // Initialisations des paramètres pour le passage de la requête
            requetes = "SELECT datname FROM pg_database WHERE datistemplate=FALSE and datname not like 'postgres'";
            paramConnexion = "Driver={PostgreSQL Unicode};Server=" + textBoxAdrServeur.Text + ";Port=" + textBoxPortServeur + ";Database=postgres;Uid=" + loginPG + ";Pwd=" + passPG + ";";

            // Remplissage du comboBox
            // Connexion à la base
            using (chaineConnexionListeBases = new OdbcConnection(paramConnexion))
            {
                try
                {
                    // Passage de la commande
                    mesRequetes = new OdbcCommand(requetes, chaineConnexionListeBases);
                    // Ouverture
                    chaineConnexionListeBases.Open();
                }
                catch (Exception ex)
                {
                    monFicLog.Log("Erreur à la connexion ODBC pour la récupération des bases côté serveur.", ficLog);
                    monFicLog.Log(ex.ToString(), ficLog);
                    mesTests = false;
                }
                finally
                {
                    // Exécution si pas d'exception
                    if (mesTests)
                    {
                        // Exeécution de la requête
                        retourRequetes = mesRequetes.ExecuteReader();

                        int i = 0;

                        // Remplissage du comboBox
                        while (retourRequetes.Read())
                        {
                            comboBoxBaseServeur.Items.Add(retourRequetes["datname"]);
                            i++;
                        }

                        if (i != 0)
                        {
                            monFicLog.Log("Récupération des bases serveurs = OK.", ficLog);
                        }

                        // Fermeture de la connexion
                        retourRequetes.Close();
                    }
                }
            }

            // Initialisations des paramètres pour le passage de la requête
            requetes = "SELECT datname FROM pg_database WHERE datistemplate=FALSE and datname not like 'postgres'";
            paramConnexion = "Driver={PostgreSQL Unicode};Server=" + textBoxAdrClient.Text + ";Port=" + textBoxPortClient + ";Database=postgres;Uid=" + loginPG + ";Pwd=" + passPG + ";";

            // Remplissage du comboBox
            // Connexion à la base
            using (chaineConnexionListeBases = new OdbcConnection(paramConnexion))
            {
                try
                {
                    // Passage de la commande
                    mesRequetes = new OdbcCommand(requetes, chaineConnexionListeBases);
                    // Ouverture
                    chaineConnexionListeBases.Open();
                }
                catch (Exception ex)
                {
                    monFicLog.Log("Erreur à la connexion ODBC pour la récupération des bases côté client.", ficLog);
                    monFicLog.Log(ex.ToString(), ficLog);
                    mesTests = false;
                }
                finally
                {
                    // Exécution si pas d'exception
                    if (mesTests)
                    {
                        // Exeécution de la requête
                        retourRequetes = mesRequetes.ExecuteReader();

                        int i = 0;

                        // Remplissage du comboBox
                        while (retourRequetes.Read())
                        {
                            comboBoxBaseClient.Items.Add(retourRequetes["datname"]);
                            i++;
                        }

                        if (i != 0)
                        {
                            monFicLog.Log("Récupération des bases serveurs = OK.", ficLog);
                        }

                        // Fermeture de la connexion
                        retourRequetes.Close();
                    }
                }
            }
        }
    }
}
