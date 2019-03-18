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
        string loginPG = "postgres";
        string passPG = "pgpass";
        string requetes;
        string paramConnexion;
        Boolean mesTests = true;
        string portServeur;
        string adrServeur;
        string portClient;
        string adrClient;
        private OdbcConnection chaineConnexionListeBases = new OdbcConnection();
        private OdbcCommand mesRequetes;
        private OdbcDataReader retourRequetes;
        string maintenant = DateTime.Now.ToString("yyMMdd_HHmmss");
        List<string> listFacturesClients = new List<string>();
        List<string> listFacturesServeur = new List<string>();
        string[] tabFacturesClients = new string[0];
        string[] tabFacturesServeur = new string[0];
        IEnumerable<string> listFactures = new List<string>();
        int i = 0;
        

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

        private void buttonDemarrage_Click(object sender, EventArgs e)
        {
            String ficLog = Application.StartupPath + "\\log_" + maintenant + ".txt";

            monFicLog.Log("Traitement côté base client pour récupérer la liste des factures dont le net à payer est inférieur à 0 mais dont le montant d'acompte est tout de même égal à la somme des encaissements versés.", ficLog);

            // Initialisations des paramètres pour le passage de la requête
            requetes = "SELECT f.id_facture AS INDEX, f.numdoc AS NUMFAC, f.titre, f.accompte AS Acompte_fact, f.uid AS UID FROM facture f WHERE netapayer< 0 AND f.accompte = (SELECT SUM(montant) FROM encaissement e WHERE e.doc_id = f.id_facture AND type_doc like 'F') ORDER BY f.numdoc";
            //requetes = "SELECT * FROM facture f WHERE netapayer< 0 AND f.accompte = (SELECT SUM(montant) FROM encaissement e WHERE e.doc_id = f.id_facture AND type_doc like 'F') ORDER BY f.numdoc";

            paramConnexion = "Driver={PostgreSQL UNICODE};Server=" + textBoxAdrClient.Text + ";Port=" + textBoxPortClient.Text + ";Database=" + comboBoxBaseClient.Text + ";Uid=" + loginPG + ";Pwd=" + passPG + ";";

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
                    monFicLog.Log("Erreur de connexion ODBC à la récupération des factures côtés client.", ficLog);
                    monFicLog.Log(ex.ToString(), ficLog);
                    mesTests = false;
                    MessageBox.Show("Erreur à la connexion à la base client.");
                }
                finally
                {
                    // Exécution si pas d'exception
                    if (mesTests)
                    {
                        // Exécution de la requête
                        retourRequetes = mesRequetes.ExecuteReader();

                        i = 0;

                        // Tant qu'il y a des données retournées par la requête
                        while (retourRequetes.Read())
                        {
                            Array.Resize(ref tabFacturesClients, tabFacturesClients.Length + 1);
                            tabFacturesClients[i] = retourRequetes["UID"].ToString();
                            //tabFacturesClients[i].identifiant = retourRequetes["INDEX"].ToString();
                            //tabFacturesClients[i].numero = retourRequetes["NUMFAC"].ToString();
                            //tabFacturesClients[i].uid = retourRequetes["UID"].ToString();
                            
                            //listFacturesClients.Add(retourRequetes["f.uid"].ToString());
                            monFicLog.Log("Facture n°" + retourRequetes["NUMFAC"].ToString() + " identifiée comme ayant un encaissement de trop.", ficLog);

                            i++;
                        }
                    }

                    retourRequetes.Close();
                }
            }

            monFicLog.Log("Traitement côté base serveur pour récupérer la liste des factures dont le net à payer est égal à 0 mais dont le montant d'acompte est inférieur à la somme des encaissements versés.", ficLog);

            // Initialisations des paramètres pour le passage de la requête
            // Le montant d'acompte des factures est augmenté de 1 centime, l'opérateur d'infériorité renvoyant des montants égaux entre l'acompte et la somme des encaisssements
            requetes = "SELECT f.id_facture, f.numdoc AS NUMFAC, f.titre, f.accompte AS Acompte_fact, f.uid AS UID FROM facture f WHERE netapayer = 0 AND f.accompte + 0.01 < (SELECT SUM(montant) FROM encaissement e WHERE e.doc_id = f.id_facture AND type_doc like 'F') ORDER BY f.numdoc";
            paramConnexion = "Driver={PostgreSQL UNICODE};Server=" + textBoxAdrServeur.Text + ";Port=" + textBoxPortServeur.Text + ";Database=" + comboBoxBaseServeur.Text + ";Uid=" + loginPG + ";Pwd=" + passPG + ";";

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
                    monFicLog.Log("Erreur de connexion ODBC à la récupération des factures côtés serveur.", ficLog);
                    monFicLog.Log(ex.ToString(), ficLog);
                    mesTests = false;
                    MessageBox.Show("Erreur à la connexion à la base serveur.");
                }
                finally
                {
                    // Exécution si pas d'exception
                    if (mesTests)
                    {
                        // Exécution de la requête
                        retourRequetes = mesRequetes.ExecuteReader();

                        i = 0;

                        // Tant qu'il y a des données retournées par la requête
                        while (retourRequetes.Read())
                        {
                            Array.Resize(ref tabFacturesServeur, tabFacturesServeur.Length + 1);
                            tabFacturesServeur[i] = retourRequetes["UID"].ToString();
                            //tabFacturesServeur[i].identifiant = retourRequetes["INDEX"].ToString();
                            //tabFacturesServeur[i].numero = retourRequetes["NUMFAC"].ToString();
                            //tabFacturesServeur[i].uid = retourRequetes["UID"].ToString();
                            
                            monFicLog.Log("Facture n°" + retourRequetes["NUMFAC"].ToString() + " identifiée comme ayant un encaissement de trop.", ficLog);

                            i++;
                        }

                        retourRequetes.Close();
                    }
                }
            }

            listFactures = tabFacturesClients.Intersect(tabFacturesServeur);

            foreach (string uid in listFactures)
            {
                string requeteIdentFacture = "SELECT id_facture FROM facture WHERE uid = " + uid;
                paramConnexion = "Driver={PostgreSQL UNICODE};Server=" + textBoxAdrServeur.Text + ";Port=" + textBoxPortServeur.Text + ";Database=" + comboBoxBaseServeur.Text + ";Uid=" + loginPG + ";Pwd=" + passPG + ";";

                using (chaineConnexionListeBases = new OdbcConnection(paramConnexion))
                {
                    try
                    {
                        // Passage de la commande
                        mesRequetes = new OdbcCommand(requeteIdentFacture, chaineConnexionListeBases);
                        // Ouverture
                        chaineConnexionListeBases.Open();
                    }
                    catch (Exception ex)
                    {
                        monFicLog.Log("Erreur de connexion ODBC à la récupération de la liste des factures côtés serveur.", ficLog);
                        monFicLog.Log(ex.ToString(), ficLog);
                        mesTests = false;
                        MessageBox.Show("Erreur à la connexion à la base serveur.");
                    }
                    finally
                    {
                        // Exécution si pas d'exception
                        if (mesTests)
                        {

                        }
                    }
                }
            }
        }
    }
}
