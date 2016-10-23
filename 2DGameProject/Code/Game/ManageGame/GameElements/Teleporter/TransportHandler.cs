using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace MemoryMaze
{
    class TransportHandler
    {
        List<Transporter> transporterList;

        public TransportHandler(List<Transporter> _transporterList)
        {
            transporterList = new List<Transporter>();
            foreach(Transporter trans in _transporterList)
            {
                transporterList.Add(trans.Copy());
            }
            FlagDublicateExits();
            FlagDuplicateEntrances();
        }

        TransportHandler(TransportHandler _transporthandler)
        {
            transporterList = new List<Transporter>();
            foreach(Transporter trans in _transporthandler.transporterList)
            {
                transporterList.Add(trans.Copy());
            }
            FlagDublicateExits();
            FlagDuplicateEntrances();
        }

        public TransportHandler Copy()
        {
            return new TransportHandler(this);
        }

        public void Update(Player player, float deltaTime)
        {
            foreach(Transporter  trans in transporterList)
            {
                trans.Update(player, deltaTime);
            }
        }

        private void FlagDuplicateEntrances()
        {
            List<Vector2i> uniqueEntrances = new List<Vector2i>();
            List<Vector2i> uniqueExits = new List<Vector2i>();
            foreach(Transporter trans in transporterList)
            {
                if(trans is Teleporter)
                {
                    if(!uniqueEntrances.Contains(trans.entrance))
                    {
                        uniqueEntrances.Add(trans.entrance);
                    }
                    else
                    {
                        trans.drawEntrance = false;
                    }
                    if (!uniqueEntrances.Contains(trans.exit))
                    {
                        uniqueEntrances.Add(trans.exit);
                    }
                    else
                    {
                        trans.drawExit = false;
                    }
                }
                else
                {
                    if(!uniqueEntrances.Contains(trans.entrance))
                    {
                        uniqueEntrances.Add(trans.entrance);
                    }
                    else
                    {
                        trans.drawEntrance = false;
                    }
                    if(!uniqueExits.Contains(trans.exit))
                    {
                        uniqueExits.Add(trans.exit);
                    }
                    else
                    {
                        trans.drawExit = false;
                    }
                }
            }
        }

        private void FlagDublicateExits()
        {

        }

        public void Draw(RenderTexture win, View view, Vector2f relViewDis)
        {
            foreach(Transporter trans in transporterList)
            {
                trans.Draw(win, view, relViewDis);
            }
        }
    }
}
