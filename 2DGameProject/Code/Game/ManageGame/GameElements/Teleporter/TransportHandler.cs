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
        }

        TransportHandler(TransportHandler _transporthandler)
        {
            transporterList = new List<Transporter>();
            foreach(Transporter trans in _transporthandler.transporterList)
            {
                transporterList.Add(trans.Copy());
            }
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

        public void Draw(RenderTexture win, View view, Vector2f relViewDis)
        {
            foreach(Transporter trans in transporterList)
            {
                trans.Draw(win, view, relViewDis);
            }
        }
    }
}
