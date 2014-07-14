﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PkmnFoundations.Structures;
using System.Configuration;

namespace PkmnFoundations.Data
{
    public abstract class DataAbstract
    {
        #region Initialization
        private static DataAbstract m_instance;

        public static DataAbstract Instance
        {
            get
            {
                // fixme: this is not thread safe
                if (m_instance == null)
                {
                    m_instance = CreateInstance();
                }
                return m_instance;
            }
        }

        private static DataAbstract CreateInstance()
        {
            ConnectionStringSettings connStr = ConfigurationManager.ConnectionStrings["pkmnFoundationsConnectionString"];
            if (connStr != null)
            {
                switch (connStr.ProviderName)
                {
                    case "MySql.Data.MySqlClient":
                        return new DataMysql(connStr.ConnectionString);
                    default:
                        throw new NotSupportedException("Database provider not supported.");
                }
            }
            else throw new NotSupportedException("No database connection string provided. Please add one in web.config or app.config.");
        }
        #endregion

        #region Utility
        #endregion

        #region GTS 4
        public const int GTS_VERSION_4 = 0;

        public abstract GtsRecord4 GtsDataForUser4(int pid);

        public abstract bool GtsDepositPokemon4(GtsRecord4 record);

        public abstract bool GtsDeletePokemon4(int pid);

        public abstract bool GtsTradePokemon4(int pidSrc, int pidDest);
        public abstract bool GtsTradePokemon4(GtsRecord4 upload, GtsRecord4 result);

        public abstract GtsRecord4[] GtsSearch4(int pid, ushort species, Genders gender, byte minLevel, byte maxLevel, byte country, int count);
        public abstract int GtsAvailablePokemon4();
        #endregion

        #region GTS 5
        public const int GTS_VERSION_5 = 0;

        public abstract GtsRecord5 GtsDataForUser5(int pid);

        public abstract bool GtsDepositPokemon5(GtsRecord5 record);

        public abstract bool GtsDeletePokemon5(int pid);

        public abstract bool GtsTradePokemon5(int pidSrc, int pidDest);
        public abstract bool GtsTradePokemon5(GtsRecord5 upload, GtsRecord5 result);

        public abstract GtsRecord5[] GtsSearch5(int pid, ushort species, Genders gender, byte minLevel, byte maxLevel, byte country, int count);
        public abstract int GtsAvailablePokemon5();
        #endregion

        #region Global Terminal 4
        public const int DRESSUP_VERSION_4 = 1;
        public const int BOX_VERSION_4 = 1;
        public const int BATTLEVIDEO_VERSION_4 = 1;

        public abstract long DressupUpload4(DressupRecord4 record);
        public abstract DressupRecord4[] DressupSearch4(ushort species, int count);

        public abstract long BoxUpload4(BoxRecord4 record);
        public abstract BoxRecord4[] BoxSearch4(BoxLabels4 label, int count);

        public abstract long BattleVideoUpload4(BattleVideoRecord4 record);
        public abstract BattleVideoHeader4[] BattleVideoSearch4(ushort species, BattleVideoRankings4 ranking, BattleVideoMetagames4 metagame, byte country, byte region, int count);
        public abstract BattleVideoRecord4 BattleVideoGet4(long serial, bool incrementViews = false);
        #endregion

        #region Global Terminal 5
        public const int MUSICAL_VERSION_5 = 1;
        public const int BATTLEVIDEO_VERSION_5 = 1;

        public abstract long MusicalUpload5(MusicalRecord5 record);
        public abstract MusicalRecord5[] MusicalSearch5(ushort species, int count);

        public abstract long BattleVideoUpload5(BattleVideoRecord5 record);
        public abstract BattleVideoHeader5[] BattleVideoSearch5(ushort species, BattleVideoRankings5 ranking, BattleVideoMetagames5 metagame, byte country, byte region, int count);
        public abstract BattleVideoRecord5 BattleVideoGet5(long serial, bool incrementViews = false);
        #endregion
    }
}
