using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tarea_5_core.NetPlay
{
    [Serializable]
        public class NetInformation
    {
        public double enviadoId { get; protected set; }
                public string contentOfEnviado { get; private set; }
                public double vectorX { get; private set;}
                public double vectorY { get; private set; }
                public double width { get; private set; }
                public double height { get; private set; }
                        public GameStatus _gamesStatus { get; protected set; }
        public GameCommands comando { get; protected set; }
      public colores colok { get; private set;}

        public NetInformation(GameStatus estado, GameCommands _comando)
        {
            _gamesStatus = estado;

            comando = _comando;
           enviadoId  = -1;
           width = -1;
           height = -1;
                       vectorX = -1;
           vectorY = -1;
                       contentOfEnviado = null;
                       colok = colores.black;

                    }

        public NetInformation(GameStatus _estado, GameCommands _comando, double ID, string Content, double x, double y, double w, double h, colores col)
        {
            enviadoId = ID;
            vectorX = x;
            vectorY = y;
            height = h;
            width = w;
            contentOfEnviado = Content;
            comando = _comando;
            _gamesStatus = _estado;
            colok = col;

        }

        public NetInformation(GameStatus estado, GameCommands _comando, double id, string content)
        {
            enviadoId = id;
            contentOfEnviado = content;
            comando = _comando;
            _gamesStatus = estado;
            width = -1;
            height = -1;
            vectorX = -1;
            vectorY = -1;
            colok = colores.black;

        }

        public NetInformation(GameStatus estado, GameCommands _comando, double id, double x, double y)
        {
            enviadoId = id;
            contentOfEnviado = null;
            comando = _comando;
            _gamesStatus = estado;
            vectorX = x;
            vectorY = y;
            height = -1;
            width = -1;
            colok = colores.black;

        }
                            }

    public enum GameStatus
    {
        ready, end_loose, end_win, pauce
    }

    public enum GameCommands
    {
        shoot, changeWeapon, moveLeft, moveRight, createScv, createShield, rechargeWeapon1, rechargeWeapon2, rechhargeWeapon3, createNuke, upgradeShields, upgradeShieldsMirror, upgradeSuperScv, restoreCanonHp, restoreMineralMine, suicide, killFirstMember, addElement, removeElement, updateElementInformation, moveElement, AddBullet, RemoveBullet, moveBullet, impactBullet, playSound, updateMineralInformation, addMineralInformation, addCanonData, reguleCanonData, addFabricsData, reguleFabricsData, addFabricsAndMineralP1Information, RemoveMineral, onlyChangeStatus
    }
    }
