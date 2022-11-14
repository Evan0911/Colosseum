using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using UnityEditor;

public static class BDD
{
    static string connection = "URI=file:" + Application.streamingAssetsPath + "/Colosseum_Chronicles_BDD.db";
    
    static IDbConnection dbcon = new SqliteConnection(connection);

    static void Connection()
    {
        dbcon.Open();        
    }

    static void Disconnection()
    {
        dbcon.Close();
    }

    public static List<CreatureStats> GetOwnedCreature()
    {
        Connection();

        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        List<CreatureStats> cCrea = new List<CreatureStats>();

        string query = "SELECT c.NumCreaDomp, NomCrea, Level, BaseAtk, BaseDef, BaseHP, NbExp, BaseExpToUp, Stade, CodeType from Creatures_Domptees c join Inventaire_Creature i on c.NumCreaDomp = i.NumCreaDomp join Liste_Creatures l on c.NumCrea = l.NumCrea where stade != 0";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        while (reader.Read())
        {
            CreatureStats uneCrea = new CreatureStats(Convert.ToInt32(reader[0]),reader[1].ToString(), Convert.ToInt32(reader[2]), Convert.ToInt32(reader[3]), Convert.ToInt32(reader[4]), Convert.ToInt32(reader[5]), Convert.ToInt32(reader[6]), Convert.ToInt32(reader[7]), Convert.ToInt32(reader[8]), reader[9].ToString());
            cCrea.Add(uneCrea);
        }

        Disconnection();
        return cCrea;
    }

    public static List<CreatureStats> GetEggs()
    {
        Connection();

        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        List<CreatureStats> cCrea = new List<CreatureStats>();

        string query = "SELECT c.NumCreaDomp, NomCrea, CodeType from Creatures_Domptees c join Inventaire_Creature i on c.NumCreaDomp = i.NumCreaDomp join Liste_Creatures l on c.NumCrea = l.NumCrea where stade = 0";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        while (reader.Read())
        {
            CreatureStats uneCrea = new CreatureStats(Convert.ToInt32(reader[0]), reader[1].ToString(), 0, 0, 0, 0, 0, 0, 0, reader[2].ToString());
            cCrea.Add(uneCrea);
        }

        Disconnection();
        return cCrea;
    }

    public static List<Item> GetRunesOwned()
    {
        Connection();

        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        List<Item> cItems = new List<Item>();

        string query = "SELECT idObjet, nom, prix from Objets join Inventaire_Objet on idObjet = NumObjet";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        while (reader.Read())
        {
            Item unItem = new Item(Convert.ToInt32(reader[0]), reader[1].ToString(), Convert.ToInt32(reader[2]));
            cItems.Add(unItem);
        }

        Disconnection();
        return cItems;
    }

    public static bool HasRuneToHatch(string type)
    {
        Connection();

        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;

        string query = "SELECT * from Inventaire_Objet join Objets on NumObjet = idObjet join Runes on idObjet = idRune where Type = '" + type + "'";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        if (reader.Read())
        {
            Disconnection();
            return true;
        }
        else
        {
            Disconnection();
            return false;
        }
    }

    public static void HatchEgg(int idCrea, string type)
    {
        Connection();
        
        //On enlève la rune
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;

        string query = "SELECT Qte, NumInv from Objets join Inventaire_Objet on idObjet = NumObjet join Runes on IdObjet = IdRune where Type = '" + type + "'";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        reader.Read();
        if (Convert.ToInt32(reader[0]) > 1)
        {
            int idInv = Convert.ToInt32(reader[1]);
            reader.Close();
            query = "update Inventaire_Objet set Qte = Qte - 1 where NumInv = " + idInv;
            cmnd_read.CommandText = query;
            cmnd_read.ExecuteNonQuery();
        }
        else
        {
            int idInv = Convert.ToInt32(reader[1]);
            reader.Close();
            query = "delete from Inventaire_Objet where NumInv = " + idInv;
            cmnd_read.CommandText = query;
            cmnd_read.ExecuteNonQuery();
        }

        //On monte le stade de la créature à 1

        query = "update Creatures_Domptees set stade = 1, Level = 1 where NumCreaDomp = " + idCrea;
        cmnd_read.CommandText = query;
        cmnd_read.ExecuteNonQuery();

        Disconnection();
    }

    public static List<int> GetLevels()
    {
        Connection();

        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        List<int> levels = new List<int>();

        string query = "select IsClear from Progression where IsAvailable order by NumProg asc";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        while (reader.Read())
        {
            levels.Add(Convert.ToInt32(reader[0]));
        }

        Disconnection();
        return levels;
    }

    public static CreatureStats GetAllyCreature(int idCrea)
    {
        Connection();

        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        CreatureStats crea = new CreatureStats(0,"",0,0,0,0,0,0,0, "");

        string query = "SELECT c.NumCreaDomp, NomCrea, Level, BaseAtk, BaseDef, BaseHP, NbExp, BaseExpToUp, Stade, CodeType from Creatures_Domptees c join Liste_Creatures l on c.NumCrea = l.NumCrea where NumCreaDomp = " + idCrea;
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        while (reader.Read())
        {
            crea = new CreatureStats(Convert.ToInt32(reader[0]), reader[1].ToString(), Convert.ToInt32(reader[2]), Convert.ToInt32(reader[3]), Convert.ToInt32(reader[4]), Convert.ToInt32(reader[5]), Convert.ToInt32(reader[6]), Convert.ToInt32(reader[7]), Convert.ToInt32(reader[8]), reader[9].ToString());
        }

        Disconnection();
        return crea;
    }

    public static CreatureStats GetEnemyCreature(int numLevel)
    {
        Connection();

        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        CreatureStats crea = new CreatureStats(0,"",0,0,0,0,0,0,0, "");

        string query = "SELECT l.NumCrea, NomCrea, Level, BaseAtk, BaseDef, BaseHP, NbExp, BaseExpToUp, Stade, CodeType from Creatures_Domptees c join Liste_Creatures l on c.NumCrea = l.NumCrea join Progression p on p.NumCreaDomp = c.NumCreaDomp where NumProg = " + numLevel;
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        while (reader.Read())
        {
            crea = new CreatureStats(Convert.ToInt32(reader[0]), reader[1].ToString(), Convert.ToInt32(reader[2]), Convert.ToInt32(reader[3]), Convert.ToInt32(reader[4]), Convert.ToInt32(reader[5]), Convert.ToInt32(reader[6]), Convert.ToInt32(reader[7]), Convert.ToInt32(reader[8]), reader[9].ToString());
        }

        Disconnection();
        return crea;
    }

    public static LevelInfos GetLevelInfos(int numLevel)
    {
        Connection();

        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        LevelInfos crea = new LevelInfos(0, 0, 0, 0, 0);

        string query = "SELECT p.NumProg, BaseExpToGive, Stade, TauxDropOeuf, MoneyDrop from Creatures_Domptees c join Liste_Creatures l on c.NumCrea = l.NumCrea join Progression p on p.NumCreaDomp = c.NumCreaDomp where NumProg = " + numLevel;
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        while (reader.Read())
        {
            crea = new LevelInfos(Convert.ToInt32(reader[0]), Convert.ToInt32(reader[1]), Convert.ToInt32(reader[2]), Convert.ToInt32(reader[3]), Convert.ToInt32(reader[4]));
        }

        Disconnection();
        return crea;
    }


    public static void SaveCreature(int currentExp, int idCrea)
    {
        Connection();

        IDbCommand dbcmd = dbcon.CreateCommand();
        dbcmd = dbcon.CreateCommand();
        string query = "update Creatures_Domptees set NbExp = " + currentExp + " where NumCreaDomp = " + idCrea;
        dbcmd.CommandText = query;
        int result = dbcmd.ExecuteNonQuery();

        Disconnection();
    }
    public static void SaveCreature(int currentExp, int lv, int idCrea)
    {
        Connection();

        IDbCommand dbcmd = dbcon.CreateCommand();
        dbcmd = dbcon.CreateCommand();
        string query = "update Creatures_Domptees set NbExp = " + currentExp + ", Level =" + lv + " where NumCreaDomp = " + idCrea;
        dbcmd.CommandText = query;
        int result = dbcmd.ExecuteNonQuery();

        Disconnection();
    }

    public static void SaveNewEgg(int idCrea)
    {
        Connection();

        IDbCommand dbcmd = dbcon.CreateCommand();
        dbcmd = dbcon.CreateCommand();
        string query = "insert into Creatures_Domptees values (null, " + idCrea + ", 0, 0, 0)";
        dbcmd.CommandText = query;
        dbcmd.ExecuteNonQuery();

        IDataReader reader;
        CreatureStats crea = new CreatureStats(0, "", 0, 0, 0, 0, 0, 0, 0, "");

        query = "select max(NumCreaDomp) from Creatures_Domptees";
        dbcmd.CommandText = query;
        reader = dbcmd.ExecuteReader();
        reader.Read();
        int idCreaDomp = Convert.ToInt32(reader[0]);

        dbcmd = dbcon.CreateCommand();
        query = "insert into Inventaire values (null)";
        dbcmd.CommandText = query;
        dbcmd.ExecuteNonQuery();

        query = "select max(IdInv) from Inventaire";
        dbcmd.CommandText = query;
        reader = dbcmd.ExecuteReader();
        reader.Read();
        int idInv = Convert.ToInt32(reader[0]);

        dbcmd = dbcon.CreateCommand();
        query = "insert into Inventaire_Creature values (" + idCreaDomp + ", " + idInv + ")";
        dbcmd.CommandText = query;
        dbcmd.ExecuteNonQuery();

        Disconnection();
    }

    #region Shop
    public static List<Item> GetEggsShop()
    {
        Connection();

        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        List<Item> cItem = new List<Item>();

        string query = "select NumCrea, NomCrea, PrixOeuf from liste_creatures";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        while (reader.Read())
        {
            Item unItem = new Item(Convert.ToInt32(reader[0]), reader[1].ToString(), Convert.ToInt32(reader[2]));
            cItem.Add(unItem);
        }

        Disconnection();
        return cItem;
    }

    public static List<Item> GetRunesShop()
    {
        Connection();

        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        List<Item> cItem = new List<Item>();

        string query = "select idObjet, Nom, Prix from runes r join objets o on idRune = idObjet";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        while (reader.Read())
        {
            Item unItem = new Item(Convert.ToInt32(reader[0]), reader[1].ToString(), Convert.ToInt32(reader[2]));
            cItem.Add(unItem);
        }

        Disconnection();
        return cItem;
    }

    public static void SaveNewItem(int idItem)
    {
        Connection();

        IDbCommand dbcmd = dbcon.CreateCommand();

        IDataReader reader;


        //Vérifie si l'objet existe déjà
        string query = "select Qte from Inventaire_Objet where NumObjet = " + idItem;
        dbcmd.CommandText = query;
        reader = dbcmd.ExecuteReader();
        if (reader.Read())
        {
            reader.Close();
            //Si oui, ajoute +1 à la quantité
            query = "update Inventaire_Objet set Qte = Qte + 1 where NumObjet = " + idItem;
            dbcmd.CommandText = query;
            dbcmd.ExecuteNonQuery();
        }
        else
        {
            reader.Close();
            //Sinon crée une nouvelle instance d'inventaire pour l'ajouter à l'inventaire
            query = "insert into Inventaire values (null)";
            dbcmd.CommandText = query;
            dbcmd.ExecuteNonQuery();

            query = "select max(IdInv) from Inventaire";
            dbcmd.CommandText = query;
            reader = dbcmd.ExecuteReader();
            reader.Read();
            int idInv = Convert.ToInt32(reader[0]);
            reader.Close();

            query = "insert into Inventaire_Objet values (" + idInv + ", " + idItem + ", 1)";
            dbcmd.CommandText = query;
            dbcmd.ExecuteNonQuery();
        }

        Disconnection();
    }
    #endregion

    /*static public void CreateDB()
    {
        dbcon.Open();

        IDbCommand dbcmd = dbcon.CreateCommand();
        dbcmd = dbcon.CreateCommand();
        string q_createTable = "CREATE TABLE IF NOT EXISTS Equipement(NumEquipement INTEGER PRIMARY KEY, LibEquipement CHAR, Prix INTEGER);"

+ "       CREATE TABLE IF NOT EXISTS Gemmes(NumGemme INTEGER PRIMARY KEY, LibGemme CHAR);"

+ "       CREATE TABLE IF NOT EXISTS Inventaire(IdInv INTEGER PRIMARY KEY AUTOINCREMENT);"

+ "       CREATE TABLE IF NOT EXISTS Type(CodeType CHAR PRIMARY KEY NOT NULL, LibType CHAR NOT NULL);"

+ "       CREATE TABLE IF NOT EXISTS Liste_Creatures(NumCrea INTEGER PRIMARY KEY ASC AUTOINCREMENT NOT NULL, NomCrea CHAR, BaseAtk INTEGER, BaseDef INTEGER, BaseHP INTEGER, PrixOeuf INTEGER, Stade INTEGER, CodeType CHAR REFERENCES Type (CodeType), EvolveIn REFERENCES Liste_Creatures (NumCrea));"

+ "       CREATE TABLE IF NOT EXISTS Creatures_Domptees(NumCreaDomp INTEGER PRIMARY KEY AUTOINCREMENT, NumCrea INTEGER REFERENCES Liste_Creatures (NumCrea), Level INTEGER);"

+ "       CREATE TABLE IF NOT EXISTS Inventaire_Creature(NumCreaDomp INTEGER REFERENCES Creatures_Domptees (NumCreaDomp), IdInv INTEGER REFERENCES Inventaire (IdInv), PRIMARY KEY (NumCreaDomp ASC, IdInv ASC));"

+ "       CREATE TABLE IF NOT EXISTS Inventaire_Equipement(IdInv INTEGER REFERENCES Inventaire (IdInv), IdEquipement INTEGER REFERENCES Equipement (NumEquipement), PRIMARY KEY (IdInv ASC, IdEquipement ASC));"

+ "       CREATE TABLE IF NOT EXISTS Inventaire_Objet(NumInv INTEGER REFERENCES Inventaire (IdInv), NumObjet INTEGER REFERENCES Gemmes (NumGemme), Qte INTEGER, PRIMARY KEY (NumInv ASC, NumObjet ASC));"

+ "       CREATE TABLE IF NOT EXISTS Progression(NumProg INTEGER PRIMARY KEY, NomDompteur CHAR, NumCreaDomp INTEGER REFERENCES Creatures_domptees (NumCreaDomp), TauxDropOeuf DECIMAL, IsClear BOOLEAN, IsAvailable BOOLEAN);";

        dbcmd.CommandText = q_createTable;
        int result = dbcmd.ExecuteNonQuery();
        Debug.Log("create tables: " + result);

        
        string query = "select * from liste_creatures";
        dbcmd.CommandText = query;
        IDataReader test = dbcmd.ExecuteReader();
        if (test.Read())
        {
            Disconnection();
            return;
        }
        test.Close();


        string q_insertDate = "INSERT INTO Inventaire(IdInv) VALUES(1);"
+ "        INSERT INTO Inventaire(IdInv) VALUES(2);"
+ "        INSERT INTO Inventaire(IdInv) VALUES(3);"
+ "        INSERT INTO Inventaire(IdInv) VALUES(4);"
+ "        INSERT INTO Inventaire(IdInv) VALUES(5);"

+ "        INSERT INTO Type(CodeType, LibType) VALUES('FEU', 'Feu');"
+ "        INSERT INTO Type(CodeType, LibType) VALUES('EAU', 'Eau');"
+ "        INSERT INTO Type(CodeType, LibType) VALUES('ELE', 'Electrique');"
+ "        INSERT INTO Type(CodeType, LibType) VALUES('TER', 'Terre');"
+ "        INSERT INTO Type(CodeType, LibType) VALUES('VEN', 'Vent');"
+ "        INSERT INTO Type(CodeType, LibType) VALUES('LUM', 'Lumière');"
+ "        INSERT INTO Type(CodeType, LibType) VALUES('TEN', 'Ténèbres');"

+ "        INSERT INTO Liste_Creatures(NumCrea, NomCrea, BaseAtk, BaseDef, BaseHP, PrixOeuf, Stade, CodeType, EvolveIn) VALUES(1, 'Megalodon_1', 5, 5, 20, 5000, 1, 'EAU', NULL);"
+ "        INSERT INTO Liste_Creatures(NumCrea, NomCrea, BaseAtk, BaseDef, BaseHP, PrixOeuf, Stade, CodeType, EvolveIn) VALUES(4, 'Smilodon_1', 6, 5, 18, 5000, 1, 'ELE', NULL);"
+ "        INSERT INTO Liste_Creatures(NumCrea, NomCrea, BaseAtk, BaseDef, BaseHP, PrixOeuf, Stade, CodeType, EvolveIn) VALUES(7, 'Salamander_1', 5, 4, 22, 5000, 1, 'FEU', NULL);"
+ "        INSERT INTO Liste_Creatures(NumCrea, NomCrea, BaseAtk, BaseDef, BaseHP, PrixOeuf, Stade, CodeType, EvolveIn) VALUES(10, 'Mammoth_1', 7, 4, 18, 5000, 1, 'TER', NULL);"
+ "        INSERT INTO Liste_Creatures(NumCrea, NomCrea, BaseAtk, BaseDef, BaseHP, PrixOeuf, Stade, CodeType, EvolveIn) VALUES(13, 'Dragon_1', 5, 5, 5, 5000, 1, 'VEN', NULL);"

+ "        INSERT INTO Creatures_Domptees(NumCreaDomp, NumCrea, Level) VALUES(1, 1, 5);"
+ "        INSERT INTO Creatures_Domptees(NumCreaDomp, NumCrea, Level) VALUES(2, 4, 5);"
+ "        INSERT INTO Creatures_Domptees(NumCreaDomp, NumCrea, Level) VALUES(3, 7, 5);"
+ "        INSERT INTO Creatures_Domptees(NumCreaDomp, NumCrea, Level) VALUES(4, 10, 5);"
+ "        INSERT INTO Creatures_Domptees(NumCreaDomp, NumCrea, Level) VALUES(5, 13, 5);"
+ "        INSERT INTO Creatures_Domptees(NumCreaDomp, NumCrea, Level) VALUES(6, 1, 5);"
+ "        INSERT INTO Creatures_Domptees(NumCreaDomp, NumCrea, Level) VALUES(8, 4, 5);"
+ "        INSERT INTO Creatures_Domptees(NumCreaDomp, NumCrea, Level) VALUES(9, 7, 5);"
+ "        INSERT INTO Creatures_Domptees(NumCreaDomp, NumCrea, Level) VALUES(10, 10, 5);"
+ "        INSERT INTO Creatures_Domptees(NumCreaDomp, NumCrea, Level) VALUES(11, 13, 5);"

+ "        INSERT INTO Inventaire_Creature(NumCreaDomp, IdInv) VALUES(1, 1);"
+ "        INSERT INTO Inventaire_Creature(NumCreaDomp, IdInv) VALUES(2, 2);"
+ "        INSERT INTO Inventaire_Creature(NumCreaDomp, IdInv) VALUES(3, 3);"
+ "        INSERT INTO Inventaire_Creature(NumCreaDomp, IdInv) VALUES(4, 4);"
+ "        INSERT INTO Inventaire_Creature(NumCreaDomp, IdInv) VALUES(5, 5);"

+ "        INSERT INTO Progression(NumProg, NomDompteur, NumCreaDomp, TauxDropOeuf, IsClear, IsAvailable) VALUES(1, 'Jean', 6, 5, 0, 1);"
+ "        INSERT INTO Progression(NumProg, NomDompteur, NumCreaDomp, TauxDropOeuf, IsClear, IsAvailable) VALUES(2, 'jsaispa', 8, 5, 0, 1);"
+ "        INSERT INTO Progression(NumProg, NomDompteur, NumCreaDomp, TauxDropOeuf, IsClear, IsAvailable) VALUES(3, 'a', 9, 5, 0, 1);"
+ "        INSERT INTO Progression(NumProg, NomDompteur, NumCreaDomp, TauxDropOeuf, IsClear, IsAvailable) VALUES(4, 'a', 10, 5, 0, 1);"
+ "        INSERT INTO Progression(NumProg, NomDompteur, NumCreaDomp, TauxDropOeuf, IsClear, IsAvailable) VALUES(5, 'a', 11, 5, 0, 1);";

        dbcmd.CommandText = q_insertDate;
        result = dbcmd.ExecuteNonQuery();
        Debug.Log("insert data: " + result);

        Disconnection();
    }*/

    static public void DeleteSave()
    {
        //Faudra créer cette fonction plus tard pour une fonction reset la save
    }
}