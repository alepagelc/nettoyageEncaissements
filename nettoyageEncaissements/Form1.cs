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
        String portServeur;
        String adrServeur;
        String portClient;
        String adrClient;
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
            String ficLog = Application.StartupPath + "\\log_" + maintenant + ".txt";

            // Initialisations des paramètres pour le passage de la requête
            requetes = "SELECT datname FROM pg_database WHERE datistemplate=FALSE and datname not like 'postgres'";
            paramConnexion = "Driver={PostgreSQL Unicode};Server=" + textBoxAdrServeur.Text + ";Port=" + textBoxPortServeur.Text + ";Database=postgres;Uid=" + loginPG + ";Pwd=" + passPG + ";";

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
            paramConnexion = "Driver={PostgreSQL Unicode};Server=" + textBoxAdrClient.Text + ";Port=" + textBoxPortClient.Text + ";Database=postgres;Uid=" + loginPG + ";Pwd=" + passPG + ";";

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

            // On récupère la première valeur
            comboBoxBaseServeur.Text = comboBoxBaseServeur.Items[0].ToString();
            comboBoxBaseClient.Text = comboBoxBaseClient.Items[0].ToString();
        }

        private void textBoxAdrServeur_KeyUp(object sender, KeyEventArgs e)
        {
            String ficLog = Application.StartupPath + "\\log_" + maintenant + ".txt";

            if ((e.KeyCode == Keys.Return) && (textBoxPortServeur.Text != null))
            {
                // Initialisation des variables
                mesTests = true;

                // Récupération des valeurs en textBoxs
                portServeur = textBoxPortServeur.Text;
                adrServeur = textBoxAdrServeur.Text;

                // Initialisations des paramètres pour le passage de la requête
                requetes = "SELECT datname FROM pg_database WHERE datistemplate=FALSE and datname not like 'postgres'";
                paramConnexion = "Driver={PostgreSQL Unicode};Server=" + adrServeur + ";Port=" + portServeur + ";Database=postgres;Uid=" + loginPG + ";Pwd=" + passPG + ";";

                // Si le port et l'adresse sont bien renseignés
                // Mise à jour du comboBox
                // Connexion à la base
                if ((portServeur != null) && (adrServeur != null))
                {
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
                            MessageBox.Show("Erreur à la connexion à la base, vérifiez les paramètres.");
                        }
                        finally
                        {
                            // Exécution si pas d'exception
                            if (mesTests)
                            {
                                // Exeécution de la requête
                                retourRequetes = mesRequetes.ExecuteReader();

                                // On vide la comboBox
                                comboBoxBaseServeur.Items.Clear();

                                // Remplissage du comboBox
                                while (retourRequetes.Read())
                                {
                                    comboBoxBaseServeur.Items.Add(retourRequetes["datname"]);
                                }

                                // Fermeture de la connexion
                                retourRequetes.Close();

                                // On récupère la première valeur
                                comboBoxBaseServeur.Text = comboBoxBaseServeur.Items[0].ToString();
                            }
                        }
                    }
                }
            }
        }

        private void textBoxPortServeur_KeyUp(object sender, KeyEventArgs e)
        {
            String ficLog = Application.StartupPath + "\\log_" + maintenant + ".txt";

            if ((e.KeyCode == Keys.Return) && (textBoxAdrServeur.Text != null))
            {
                // Initialisation des variables
                mesTests = true;

                // Récupération des valeurs en textBoxs
                portServeur = textBoxPortServeur.Text;
                adrServeur = textBoxAdrServeur.Text;

                // Initialisations des paramètres pour le passage de la requête
                requetes = "SELECT datname FROM pg_database WHERE datistemplate=FALSE and datname not like 'postgres'";
                paramConnexion = "Driver={PostgreSQL Unicode};Server=" + adrServeur + ";Port=" + portServeur + ";Database=postgres;Uid=" + loginPG + ";Pwd=" + passPG + ";";

                // Si le port et l'adresse sont bien renseignés
                // Mise à jour du comboBox
                // Connexion à la base
                if ((portServeur != null) && (adrServeur != null))
                {
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
                            MessageBox.Show("Erreur à la connexion à la base, vérifiez les paramètres.");
                        }
                        finally
                        {
                            // Exécution si pas d'exception
                            if (mesTests)
                            {
                                // Exeécution de la requête
                                retourRequetes = mesRequetes.ExecuteReader();

                                // On vide la comboBox
                                comboBoxBaseServeur.Items.Clear();

                                // Remplissage du comboBox
                                while (retourRequetes.Read())
                                {
                                    comboBoxBaseServeur.Items.Add(retourRequetes["datname"]);
                                }

                                // Fermeture de la connexion
                                retourRequetes.Close();

                                // On récupère la première valeur
                                comboBoxBaseServeur.Text = comboBoxBaseServeur.Items[0].ToString();
                            }
                        }
                    }
                }
            }
        }

        private void textBoxAdrClient_KeyUp(object sender, KeyEventArgs e)
        {
            String ficLog = Application.StartupPath + "\\log_" + maintenant + ".txt";

            if ((e.KeyCode == Keys.Return) && (textBoxPortClient.Text != null))
            {
                // Initialisation des variables
                mesTests = true;

                // Récupération des valeurs en textBoxs
                portClient = textBoxPortClient.Text;
                adrClient = textBoxAdrClient.Text;

                // Initialisations des paramètres pour le passage de la requête
                requetes = "SELECT datname FROM pg_database WHERE datistemplate=FALSE and datname not like 'postgres'";
                paramConnexion = "Driver={PostgreSQL Unicode};Server=" + adrClient + ";Port=" + portClient + ";Database=postgres;Uid=" + loginPG + ";Pwd=" + passPG + ";";

                // Si le port et l'adresse sont bien renseignés
                // Mise à jour du comboBox
                // Connexion à la base
                if ((portClient != null) && (adrClient != null))
                {
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
                            MessageBox.Show("Erreur à la connexion à la base, vérifiez les paramètres.");
                        }
                        finally
                        {
                            // Exécution si pas d'exception
                            if (mesTests)
                            {
                                // Exeécution de la requête
                                retourRequetes = mesRequetes.ExecuteReader();

                                // On vide la comboBox
                                comboBoxBaseClient.Items.Clear();

                                // Remplissage du comboBox
                                while (retourRequetes.Read())
                                {
                                    comboBoxBaseClient.Items.Add(retourRequetes["datname"]);
                                }

                                // Fermeture de la connexion
                                retourRequetes.Close();

                                // On récupère la première valeur
                                comboBoxBaseClient.Text = comboBoxBaseServeur.Items[0].ToString();
                            }
                        }
                    }
                }
            }
        }

        private void textBoxPortClient_KeyUp(object sender, KeyEventArgs e)
        {
            String ficLog = Application.StartupPath + "\\log_" + maintenant + ".txt";

            if ((e.KeyCode == Keys.Return) && (textBoxAdrClient.Text != null))
            {
                // Initialisation des variables
                mesTests = true;

                // Récupération des valeurs en textBoxs
                portClient = textBoxPortClient.Text;
                adrClient = textBoxAdrClient.Text;

                // Initialisations des paramètres pour le passage de la requête
                requetes = "SELECT datname FROM pg_database WHERE datistemplate=FALSE and datname not like 'postgres'";
                paramConnexion = "Driver={PostgreSQL Unicode};Server=" + adrClient + ";Port=" + portClient + ";Database=postgres;Uid=" + loginPG + ";Pwd=" + passPG + ";";

                // Si le port et l'adresse sont bien renseignés
                // Mise à jour du comboBox
                // Connexion à la base
                if ((portClient != null) && (adrClient != null))
                {
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
                            MessageBox.Show("Erreur à la connexion à la base, vérifiez les paramètres.");
                        }
                        finally
                        {
                            // Exécution si pas d'exception
                            if (mesTests)
                            {
                                // Exeécution de la requête
                                retourRequetes = mesRequetes.ExecuteReader();

                                // On vide la comboBox
                                comboBoxBaseClient.Items.Clear();

                                // Remplissage du comboBox
                                while (retourRequetes.Read())
                                {
                                    comboBoxBaseClient.Items.Add(retourRequetes["datname"]);
                                }

                                // Fermeture de la connexion
                                retourRequetes.Close();

                                // On récupère la première valeur
                                comboBoxBaseClient.Text = comboBoxBaseServeur.Items[0].ToString();
                            }
                        }
                    }
                }
            }
        }
    }
}
