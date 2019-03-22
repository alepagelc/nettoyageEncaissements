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
        private OdbcConnection chaineConnexionListeBases2 = new OdbcConnection();
        private OdbcCommand mesRequetes;
        private OdbcCommand mesRequetes2;
        private OdbcDataReader retourRequetes;
        private OdbcDataReader retourRequetes2;
        string maintenant = DateTime.Now.ToString("yyMMdd_HHmmss");
        List<Affaire> listAffairesClient = new List<Affaire>();
        List<Affaire> listAffairesServeur = new List<Affaire>();
        string[] tabAffairesClient = new string[0];
        string[] tabAffairesServeur = new string[0];
        List<string> listUidEncaissements = new List<string>();
        IEnumerable<string> listAffaires = new List<string>();
        IEnumerable<Affaire> listAffairesO = new List<Affaire>();
        int i = 0;
        string libelleTemp = "";
        

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

            if (chkBxBase.Checked == false)
            {
                monFicLog.Log("Traitement côté base client pour récupérer la liste des factures dont le net à payer est inférieur à 0 mais dont le montant d'acompte est tout de même égal à la somme des encaissements versés.", ficLog);

                // Initialisations des paramètres pour le passage de la requête
                requetes = @"SELECT f.id_facture AS ID, 'F' AS TYPEDOC, f.numdoc AS NUMAFF, f.accompte AS ACOMPTE, f.uid AS UID
                            FROM facture f
                            WHERE f.netapayer < 0
                            AND f.accompte = (SELECT SUM(montant) FROM encaissement e WHERE e.doc_id = f.id_facture AND type_doc like 'F')
                            UNION
                            SELECT fa.id_facture_arch AS ID, 'A' AS TYPEDOC, fa.numdoc AS NUMAFF, fa.accompte AS ACOMPTE, fa.uid AS UID
                            FROM facture_arch fa
                            WHERE fa.netapayer < 0
                            AND fa.accompte = (SELECT SUM(montant) FROM encaissement e WHERE e.doc_id = fa.id_facture_arch AND type_doc like 'A')
                            UNION
                            SELECT b.id_bonliv AS ID, 'B' AS TYPEDOC, b.numdoc AS NUMAFF, b.accompte AS ACOMPTE, b.uid AS UID
                            FROM bonliv b
                            WHERE b.netapayer < 0
                            AND b.accompte = (SELECT SUM(montant) FROM encaissement e WHERE e.doc_id = b.id_bonliv AND type_doc like 'B')
                            UNION
                            SELECT ba.id_bonliv_arch AS ID, 'L' AS TYPEDOC, ba.numdoc AS NUMAFF, ba.accompte AS ACOMPTE, ba.uid AS UID
                            FROM bonliv_arch ba
                            WHERE ba.netapayer < 0
                            AND ba.accompte = (SELECT SUM(montant) FROM encaissement e WHERE e.doc_id = ba.id_bonliv_arch AND type_doc like 'L')
                            UNION
                            SELECT c.id_cde AS ID, 'C' AS TYPEDOC, c.numdoc AS NUMAFF, c.accompte AS ACOMPTE, c.uid AS UID
                            FROM cde c
                            WHERE c.netapayer < 0
                            AND c.accompte = (SELECT SUM(montant) FROM encaissement e WHERE e.doc_id = c.id_cde AND type_doc like 'C')
                            UNION
                            SELECT ca.id_cde_arch AS ID, 'M' AS TYPEDOC, ca.numdoc AS NUMAFF, ca.accompte AS ACOMPTE, ca.uid AS UID
                            FROM cde_arch ca
                            WHERE ca.netapayer < 0
                            AND ca.accompte = (SELECT SUM(montant) FROM encaissement e WHERE e.doc_id = ca.id_cde_arch AND type_doc like 'M')
                            UNION
                            SELECT d.id_devis AS ID, 'M' AS TYPEDOC, d.numdoc AS NUMAFF, d.accompte AS ACOMPTE, d.uid AS UID
                            FROM devis d
                            WHERE d.netapayer < 0
                            AND d.accompte = (SELECT SUM(montant) FROM encaissement e WHERE e.doc_id = d.id_devis AND type_doc like 'D')
                            UNION
                            SELECT da.id_devis_arch AS ID, 'M' AS TYPEDOC, da.numdoc AS NUMAFF, da.accompte AS ACOMPTE, da.uid AS UID
                            FROM devis_arch da
                            WHERE da.netapayer < 0
                            AND da.accompte = (SELECT SUM(montant) FROM encaissement e WHERE e.doc_id = da.id_devis_arch AND type_doc like 'W')";
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
                        monFicLog.Log("Erreur de connexion ODBC à la récupération des affaires côté client.", ficLog);
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
                                // Objet
                                listAffairesClient.Add(new Affaire(int.Parse(retourRequetes["ID"].ToString()), retourRequetes["TYPEDOC"].ToString(), double.Parse(retourRequetes["ACOMPTE"].ToString()), retourRequetes["UID"].ToString(), retourRequetes["NUMAFF"].ToString()));

                                // Procédure
                                //Array.Resize(ref tabAffairesClient, tabAffairesClient.Length + 1);
                                //tabAffairesClient[i] = retourRequetes["UID"].ToString();

                                switch (retourRequetes["TYPEDOC"].ToString())
                                {
                                    case "F":
                                        libelleTemp = "Facture n° " + retourRequetes["NUMAFF"].ToString() + " identifiée";
                                        break;
                                    case "A":
                                        libelleTemp = "Facture archivée n° " + retourRequetes["NUMAFF"].ToString() + " identifiée";
                                        break;
                                    case "B":
                                        libelleTemp = "Bon de livraison n° " + retourRequetes["NUMAFF"].ToString() + " identifié";
                                        break;
                                    case "L":
                                        libelleTemp = "Bon de livraison archivé n° " + retourRequetes["NUMAFF"].ToString() + " identifié";
                                        break;
                                    case "C":
                                        libelleTemp = "Commande n° " + retourRequetes["NUMAFF"].ToString() + " identifiée";
                                        break;
                                    case "M":
                                        libelleTemp = "Commande archivée n° " + retourRequetes["NUMAFF"].ToString() + " identifiée";
                                        break;
                                    case "D":
                                        libelleTemp = "Devis n° " + retourRequetes["NUMAFF"].ToString() + " identifié";
                                        break;
                                    case "W":
                                        libelleTemp = "Devis archivé n° " + retourRequetes["NUMAFF"].ToString() + " identifié";
                                        break;
                                    default:
                                        break;
                                }
                                monFicLog.Log(libelleTemp + " comme ayant un encaissement de trop sur la base client.", ficLog);

                                i++;
                            }
                        }

                        retourRequetes.Close();
                    }
                }

                monFicLog.Log("Traitement côté base serveur pour récupérer la liste des affaires dont le net à payer est égal à 0 mais dont le montant d'acompte est inférieur à la somme des encaissements versés.", ficLog);

                // Initialisations des paramètres pour le passage de la requête
                // Le montant d'acompte des affaires est augmenté de 0,01 centime, l'opérateur d'infériorité renvoyant des montants égaux entre l'acompte et la somme des encaisssements
                requetes = @"SELECT f.id_facture AS ID, 'F' AS TYPEDOC, f.numdoc AS NUMAFF, f.accompte AS ACOMPTE, f.uid AS UID
                            FROM facture f
                            WHERE f.netapayer = 0
                            AND f.accompte + 0.00001 < (SELECT SUM(montant) FROM encaissement e WHERE e.doc_id = f.id_facture AND type_doc like 'F')
                            UNION
                            SELECT fa.id_facture_arch AS ID, 'W' AS TYPEDOC, fa.numdoc AS NUMAFF, fa.accompte AS ACOMPTE, fa.uid AS UID
                            FROM facture_arch fa
                            WHERE fa.netapayer = 0
                            AND fa.accompte + 0.00001 < (SELECT SUM(montant) FROM encaissement e WHERE e.doc_id = fa.id_facture_arch AND type_doc like 'W')
                            UNION
                            SELECT b.id_bonliv AS ID, 'B' AS TYPEDOC, b.numdoc AS NUMAFF, b.accompte AS ACOMPTE, b.uid AS UID
                            FROM bonliv b
                            WHERE b.netapayer = 0
                            AND b.accompte + 0.00001 < (SELECT SUM(montant) FROM encaissement e WHERE e.doc_id = b.id_bonliv AND type_doc like 'B')
                            UNION
                            SELECT ba.id_bonliv_arch AS ID, 'L' AS TYPEDOC, ba.numdoc AS NUMAFF, ba.accompte AS ACOMPTE, ba.uid AS UID
                            FROM bonliv_arch ba
                            WHERE ba.netapayer = 0
                            AND ba.accompte + 0.00001 < (SELECT SUM(montant) FROM encaissement e WHERE e.doc_id = ba.id_bonliv_arch AND type_doc like 'L')
                            UNION
                            SELECT c.id_cde AS ID, 'C' AS TYPEDOC, c.numdoc AS NUMAFF, c.accompte AS ACOMPTE, c.uid AS UID
                            FROM cde c
                            WHERE c.netapayer = 0
                            AND c.accompte + 0.00001 < (SELECT SUM(montant) FROM encaissement e WHERE e.doc_id = c.id_cde AND type_doc like 'C')
                            UNION
                            SELECT ca.id_cde_arch AS ID, 'M' AS TYPEDOC, ca.numdoc AS NUMAFF, ca.accompte AS ACOMPTE, ca.uid AS UID
                            FROM cde_arch ca
                            WHERE ca.netapayer = 0
                            AND ca.accompte + 0.00001 < (SELECT SUM(montant) FROM encaissement e WHERE e.doc_id = ca.id_cde_arch AND type_doc like 'M')
                            UNION
                            SELECT d.id_devis AS ID, 'D' AS TYPEDOC, d.numdoc AS NUMAFF, d.accompte AS ACOMPTE, d.uid AS UID
                            FROM devis d
                            WHERE d.netapayer = 0
                            AND d.accompte + 0.00001 < (SELECT SUM(montant) FROM encaissement e WHERE e.doc_id = d.id_devis AND type_doc like 'D')
                            UNION
                            SELECT da.id_devis_arch AS ID, 'W' AS TYPEDOC, da.numdoc AS NUMAFF, da.accompte AS ACOMPTE, da.uid AS UID
                            FROM devis_arch da
                            WHERE da.netapayer = 0
                            AND da.accompte + 0.00001 < (SELECT SUM(montant) FROM encaissement e WHERE e.doc_id = da.id_devis_arch AND type_doc like 'W')
                            ";
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
                        monFicLog.Log("Erreur de connexion ODBC à la récupération des affaires côtés serveur.", ficLog);
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
                                // Objet
                                listAffairesServeur.Add(new Affaire(int.Parse(retourRequetes["ID"].ToString()), retourRequetes["TYPEDOC"].ToString(), double.Parse(retourRequetes["ACOMPTE"].ToString()), retourRequetes["UID"].ToString(), retourRequetes["NUMAFF"].ToString()));

                                // Procédure
                                //Array.Resize(ref tabAffairesServeur, tabAffairesServeur.Length + 1);
                                //tabAffairesServeur[i] = retourRequetes["UID"].ToString();

                                switch (retourRequetes["TYPEDOC"].ToString())
                                {
                                    case "F":
                                        libelleTemp = "Facture n° " + retourRequetes["NUMAFF"].ToString() + " identifiée";
                                        break;
                                    case "A":
                                        libelleTemp = "Facture archivée n° " + retourRequetes["NUMAFF"].ToString() + " identifiée";
                                        break;
                                    case "B":
                                        libelleTemp = "Bon de livraison n° " + retourRequetes["NUMAFF"].ToString() + " identifié";
                                        break;
                                    case "L":
                                        libelleTemp = "Bon de livraison archivé n° " + retourRequetes["NUMAFF"].ToString() + " identifié";
                                        break;
                                    case "C":
                                        libelleTemp = "Commande n° " + retourRequetes["NUMAFF"].ToString() + " identifiée";
                                        break;
                                    case "M":
                                        libelleTemp = "Commande archivée n° " + retourRequetes["NUMAFF"].ToString() + " identifiée";
                                        break;
                                    case "D":
                                        libelleTemp = "Devis n° " + retourRequetes["NUMAFF"].ToString() + " identifié";
                                        break;
                                    case "W":
                                        libelleTemp = "Devis archivé n° " + retourRequetes["NUMAFF"].ToString() + " identifié";
                                        break;
                                    default:
                                        break;
                                }
                                monFicLog.Log(libelleTemp + " comme ayant un encaissement de trop sur la base serveur.", ficLog);

                                i++;
                            }

                            retourRequetes.Close();
                        }
                    }
                }

                // Intersection des deux listes d'affaires
                listAffairesO = listAffairesServeur.Intersect(listAffairesClient, new AffaireComparer());

                paramConnexion = "Driver={PostgreSQL UNICODE};Server=" + textBoxAdrServeur.Text + ";Port=" + textBoxPortServeur.Text + ";Database=" + comboBoxBaseServeur.Text + ";Uid=" + loginPG + ";Pwd=" + passPG + ";";

                foreach (Affaire affaire in listAffairesO)
                {
                    requetes = @"SELECT  e1.id_encaissement AS ID, e1.uid AS UID, e1.montant, e1.dtencaissement, (SELECT COUNT(*)
    FROM encaissement e2
    WHERE e2.doc_id = e1.doc_id
    AND e2.montant::text||'-'||e2.dtencaissement::text = e1.montant::text||'-'||e1.dtencaissement::text) AS NB_DOUBLON
    FROM encaissement e1
    WHERE e1.doc_id = " + affaire.id + " AND e1.type_doc = '" + affaire.type + "' ORDER BY NB_DOUBLON DESC, ddc DESC LIMIT 1";

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
                            monFicLog.Log("Erreur de connexion ODBC à la récupération de la liste des factures côtés serveur.", ficLog);
                            monFicLog.Log(ex.ToString(), ficLog);
                            mesTests = false;
                            MessageBox.Show("Erreur à la connexion à la base serveur lors de la récupération de la liste des factures comportant des erreurs.");
                        }
                        finally
                        {
                            // Exécution si pas d'exception
                            if (mesTests)
                            {
                                retourRequetes = mesRequetes.ExecuteReader();

                                string requeteSuppressionEncaissement = "";
                                string requeteSuppressionEncaissementRecordLog = "";
                                string idEncaissement = "";

                                while (retourRequetes.Read())
                                {
                                    requeteSuppressionEncaissement = "DELETE FROM encaissement WHERE id_encaissement = " + retourRequetes["ID"].ToString();
                                    requeteSuppressionEncaissementRecordLog = "DELETE FROM record_log WHERE record_id = " + retourRequetes["ID"].ToString();
                                    idEncaissement = retourRequetes["ID"].ToString();
                                    listUidEncaissements.Add(retourRequetes["UID"].ToString());
                                }

                                retourRequetes.Close();

                                using (chaineConnexionListeBases = new OdbcConnection(paramConnexion))
                                {
                                    try
                                    {
                                        // Passage de la commande
                                        mesRequetes = new OdbcCommand(requeteSuppressionEncaissement, chaineConnexionListeBases);
                                        // Ouverture
                                        chaineConnexionListeBases.Open();
                                    }
                                    catch (Exception ex)
                                    {
                                        monFicLog.Log("Erreur de connexion ODBC pour la suppression des encaissements côté serveur.", ficLog);
                                        monFicLog.Log(ex.ToString(), ficLog);
                                        mesTests = false;
                                        MessageBox.Show("Erreur à la connexion à la base serveur pour la suppression des encaissements.");
                                    }
                                    finally
                                    {
                                        if (mesTests)
                                        {
                                            int nbElemSupp = 0;

                                            try
                                            {
                                                nbElemSupp = mesRequetes.ExecuteNonQuery();
                                            }
                                            catch (Exception ex)
                                            {
                                                monFicLog.Log("Erreur lors de la tentative de suppression de l'encaissement identifié " + idEncaissement + " sur la base serveur " + comboBoxBaseServeur.Text, ficLog);
                                                monFicLog.Log(ex.ToString(), ficLog);
                                                mesTests = false;
                                                MessageBox.Show("Erreur à la suppression d'un encaissement sur la base serveur.");
                                            }
                                            finally
                                            {
                                                string typeAff = "";
                                                switch (affaire.type)
                                                {
                                                    case "D":
                                                        typeAff = "devis";
                                                        break;
                                                    case "W":
                                                        typeAff = "devis archivé";
                                                        break;
                                                    case "C":
                                                        typeAff = "commande";
                                                        break;
                                                    case "M":
                                                        typeAff = "commande archivée";
                                                        break;
                                                    case "B":
                                                        typeAff = "bon de livraison";
                                                        break;
                                                    case "L":
                                                        typeAff = "bon de livraison archivé";
                                                        break;
                                                    case "F":
                                                        typeAff = "facture";
                                                        break;
                                                    case "A":
                                                        typeAff = "facture archivée";
                                                        break;
                                                    default:
                                                        break;
                                                }

                                                if (nbElemSupp == -1)
                                                {
                                                    monFicLog.Log("Aucun encaissement à supprimer sur la base serveur " + comboBoxBaseServeur.Text + " pour l'affaire " + affaire.numaff + " de type " + typeAff, ficLog);
                                                }
                                                else
                                                {
                                                    monFicLog.Log("Suppression de l'encaissement d'id " + idEncaissement + "dans la base serveur " + comboBoxBaseServeur.Text + " pour l'affaire " + affaire.numaff + " de type " + typeAff, ficLog);
                                                }
                                            }
                                        }
                                    }
                                }

                                using (chaineConnexionListeBases = new OdbcConnection(paramConnexion))
                                {
                                    try
                                    {
                                        // Passage de la commande
                                        mesRequetes = new OdbcCommand(requeteSuppressionEncaissementRecordLog, chaineConnexionListeBases);
                                        // Ouverture
                                        chaineConnexionListeBases.Open();
                                    }
                                    catch (Exception ex)
                                    {
                                        monFicLog.Log("Erreur de connexion ODBC pour la seconde phase de suppression des encaissements côté serveur.", ficLog);
                                        monFicLog.Log(ex.ToString(), ficLog);
                                        mesTests = false;
                                        MessageBox.Show("Erreur à la connexion à la base serveur lors de la seconde phase de suppression des encaissements.");
                                    }
                                    finally
                                    {
                                        if (mesTests)
                                        {
                                            try
                                            {
                                                i = mesRequetes.ExecuteNonQuery();
                                            }
                                            catch (Exception ex)
                                            {
                                                monFicLog.Log("Erreur de connexion ODBC lors de la tentative de suppression de l'encaissement identifié " + idEncaissement + " sur la base serveur " + comboBoxBaseServeur.Text, ficLog);
                                                monFicLog.Log(ex.ToString(), ficLog);
                                                mesTests = false;
                                                MessageBox.Show("Erreur à la suppression d'un encaissement sur la base serveur.");
                                            }
                                            finally
                                            {
                                                string typeAff = "";
                                                switch (affaire.type)
                                                {
                                                    case "D":
                                                        typeAff = "devis";
                                                        break;
                                                    case "W":
                                                        typeAff = "devis archivé";
                                                        break;
                                                    case "C":
                                                        typeAff = "commande";
                                                        break;
                                                    case "M":
                                                        typeAff = "commande archivée";
                                                        break;
                                                    case "B":
                                                        typeAff = "bon de livraison";
                                                        break;
                                                    case "L":
                                                        typeAff = "bon de livraison archivé";
                                                        break;
                                                    case "F":
                                                        typeAff = "facture";
                                                        break;
                                                    case "A":
                                                        typeAff = "facture archivée";
                                                        break;
                                                    default:
                                                        break;
                                                }

                                                if (i == -1)
                                                {
                                                    monFicLog.Log("Aucun encaissement à supprimer en seconde phase sur la base serveur " + comboBoxBaseServeur.Text + " pour l'affaire " + affaire.numaff + " de type " + typeAff, ficLog);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                paramConnexion = "Driver={PostgreSQL UNICODE};Server=" + textBoxAdrClient.Text + ";Port=" + textBoxPortClient.Text + ";Database=" + comboBoxBaseClient.Text + ";Uid=" + loginPG + ";Pwd=" + passPG + ";";

                requetes = "SELECT id_encaissement AS ID FROM encaissement WHERE";
                i = 0;
                foreach (string uidEnc in listUidEncaissements)
                {
                    if (i == 0)
                    {
                        requetes = requetes + " uid like '" + uidEnc + "'";
                        i++;
                    }
                    else
                    {
                        requetes = requetes + " OR uid like '" + uidEnc + "'";
                    }
                }

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
                        monFicLog.Log("Erreur de connexion ODBC à la récupération de la liste des encaissements côté client.", ficLog);
                        monFicLog.Log(ex.ToString(), ficLog);
                        mesTests = false;
                        MessageBox.Show("Erreur à la connexion à la base serveur lors de la récupération de la liste des encaissements côté base client.");
                    }
                    finally
                    {
                        // Exécution si pas d'exception
                        if (mesTests)
                        {
                            string requeteSuppressionEncaissement = "";
                            string requeteSuppressionEncaissementRecordLog = "";
                            string idEncaissement = "";

                            retourRequetes = mesRequetes.ExecuteReader();

                            while (retourRequetes.Read())
                            {
                                requeteSuppressionEncaissement = "DELETE FROM encaissement WHERE id_encaissement = " + retourRequetes["ID"].ToString();
                                requeteSuppressionEncaissementRecordLog = "DELETE FROM record_log WHERE record_id = " + retourRequetes["ID"].ToString();
                                idEncaissement = retourRequetes["ID"].ToString();

                                using (chaineConnexionListeBases2 = new OdbcConnection(paramConnexion))
                                {
                                    try
                                    {
                                        // Passage de la commande
                                        mesRequetes2 = new OdbcCommand(requeteSuppressionEncaissement, chaineConnexionListeBases2);
                                        // Ouverture
                                        chaineConnexionListeBases2.Open();
                                    }
                                    catch (Exception ex)
                                    {
                                        monFicLog.Log("Erreur de connexion ODBC pour la suppression des encaissements côté serveur.", ficLog);
                                        monFicLog.Log(ex.ToString(), ficLog);
                                        mesTests = false;
                                        MessageBox.Show("Erreur à la connexion à la base serveur pour la suppression des encaissements.");
                                    }
                                    finally
                                    {
                                        if (mesTests)
                                        {
                                            try
                                            {
                                                i = mesRequetes2.ExecuteNonQuery();
                                            }
                                            catch (Exception ex)
                                            {
                                                monFicLog.Log("Erreur de connexion ODBC lors de la tentative de suppression de l'encaissement identifié " + idEncaissement + " sur la base client " + comboBoxBaseClient.Text, ficLog);
                                                monFicLog.Log(ex.ToString(), ficLog);
                                                mesTests = false;
                                                MessageBox.Show("Erreur à la suppression d'un encaissement sur la base client.");
                                            }
                                            finally
                                            {
                                                if (i == -1)
                                                {
                                                    monFicLog.Log("Aucun encaissement à supprimer sur la base serveur " + comboBoxBaseClient.Text, ficLog);
                                                }
                                                else
                                                {
                                                    monFicLog.Log("Suppression de l'encaissement d'id " + idEncaissement + "dans la base serveur " + comboBoxBaseClient.Text, ficLog);
                                                }
                                            }
                                        }
                                    }
                                }

                                using (chaineConnexionListeBases2 = new OdbcConnection(paramConnexion))
                                {
                                    try
                                    {
                                        // Passage de la commande
                                        mesRequetes2 = new OdbcCommand(requeteSuppressionEncaissementRecordLog, chaineConnexionListeBases2);
                                        // Ouverture
                                        chaineConnexionListeBases2.Open();
                                    }
                                    catch (Exception ex)
                                    {
                                        monFicLog.Log("Erreur de connexion ODBC pour la seconde phase de suppression des encaissements côté serveur.", ficLog);
                                        monFicLog.Log(ex.ToString(), ficLog);
                                        mesTests = false;
                                        MessageBox.Show("Erreur à la connexion à la base serveur lors de la seconde phase de suppression des encaissements.");
                                    }
                                    finally
                                    {
                                        if (mesTests)
                                        {
                                            try
                                            {
                                                i = mesRequetes2.ExecuteNonQuery();
                                            }
                                            catch (Exception ex)
                                            {
                                                monFicLog.Log("Erreur de connexion ODBC lors de la tentative de suppression de l'encaissement identifié " + idEncaissement + " sur la base serveur " + comboBoxBaseServeur.Text, ficLog);
                                                monFicLog.Log(ex.ToString(), ficLog);
                                                mesTests = false;
                                                MessageBox.Show("Erreur à la suppression d'un encaissement sur la base serveur.");
                                            }
                                            finally
                                            {
                                                if (i == -1)
                                                {
                                                    monFicLog.Log("Aucun encaissement à supprimer en seconde phase sur la base serveur " + comboBoxBaseServeur.Text, ficLog);
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            retourRequetes.Close();
                        }
                    }
                }
            }
            else
            {

            }
        }

        private void chkBxBase_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBxBase.Checked)
            {
                textBoxAdrClient.Enabled = false;
                textBoxPortClient.Enabled = false;
                comboBoxBaseClient.Enabled = false;
            }
            else
            {
                textBoxAdrClient.Enabled = true;
                textBoxPortClient.Enabled = true;
                comboBoxBaseClient.Enabled = true;
            }
        }
    }
}
