using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Whiteboard.Hubs
{
    public class WhiteboardHub : Hub
    {
        public WhiteboardHub()
        {

        }

        public void Heartbeat()
        {
            Clients.All.Heartbeat();
        }

        public async Task StartDraw(Pixel p)
        {
            await Clients.Others.startDraw(p);
        }

        public async Task MoveDraw(Pixel p)
        {
            await Clients.Others.moveDraw(p);
        }

        public async Task Save(string image)
        {
            cachedImage = image;
            await Clients.Others.loadImage(image);
        }

        private static string cachedImage = "";

        public override async Task OnConnected()
        {
            //send latest picture from cache
            if (cachedImage.Length > 0)
            {
                Clients.Caller.loadImage(cachedImage);
            }

            await base.OnConnected();
        }
    }
}