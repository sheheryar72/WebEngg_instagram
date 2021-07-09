using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebEngg_instagram.Hubs
{
    public class Comments : Hub
    {
        public Comments()
        {

        }
        public void Hello()
        {
            Clients.All.hello();
        }
        public void Comment(string PostID,string username,string Comment)
        {
            Clients.All.newMessage(PostID,username,Comment);
        }
    }
}