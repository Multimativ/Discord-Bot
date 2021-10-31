using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot
{
    public class CommandsModule : ModuleBase<SocketCommandContext>
    {
        [Command("hallo")]
        [Alias("hi")]
        public async Task Hallo([Remainder] string user)
        {
            await Context.Channel.SendMessageAsync("Hallo " + user);
        }

        [Command("calc")]
        public async Task Calc(int i1, int i2)
        {
            int resultat = i1 + i2;
            await Context.Channel.SendMessageAsync(resultat.ToString());
        }

        [Command("love")]
        public async Task lieben([Remainder] string user)
        {
            await Context.Channel.SendMessageAsync("Ich liebe " + user);
        }
        
        [Command("hug")]
        public async Task Umarmen([Remainder] string user)
        {
            await Context.Channel.SendMessageAsync(user + " bekommt eine ganz dicke Umarmung <3");
        }
    }
}
