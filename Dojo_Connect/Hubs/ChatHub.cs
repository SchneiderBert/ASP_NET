using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Dojo_Connect.Models;

namespace Dojo_Connect.Hubs {

    public class ChatHub : Hub {

        private MyContext _context {get; set;}

        public ChatHub(MyContext context){

            this._context = context;
        }

        public async Task SendMessage(string user, string message, int userId){
            var u = this._context.Users.Where(u => u.Alias == user);
            await Clients.All.SendAsync("ReceiveMessage", user, message);
            Message newMessage =  new Message();
            newMessage.UserId = userId;
            newMessage.MessageContent = message;


            this._context.Add(newMessage);
            this._context.SaveChanges();
        }
        // public async Task SendMessage(string user, string message)
        // {
        //     await Clients.All.SendAsync("ReceiveMessage", user, message);
        // }
    }
}