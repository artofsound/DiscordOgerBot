﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace DiscordOgerBotWeb.Modules
{
    public class GenericCommands : ModuleBase<SocketCommandContext>
    {

        [Command("help")]
        [Alias("hilfe")]
        public async Task SendHelp()
        {

            var random = new Random();
            var embed = new EmbedBuilder
            {
                Title = "Meddl Leudde!:metal:"
            };
            var buildEmbed = embed

                .WithDescription(
                    "Ich versuche deutsche Sätze ins Meddlfrängische zu übersetzen! " + Environment.NewLine +
                    "Eine Discord Nachricht auf Hochdeutsch nervt dich? Kein Problem! **reagiere einfach auf die Nachricht mit :OgerBot: **" + Environment.NewLine +
                    Environment.NewLine +
                    "**Der Server braucht ein Emoji mit dem Namen OgerBot!!** (Wende dich an die Server Admins die wissen das ganz bestimmt)" + Environment.NewLine +
                    Environment.NewLine +
                    "Die Nachricht kann auch wieder gelöscht werden indem man die Reaction wieder entfernt")

                .AddField("Sounds",
                "Ich kann auch Sounds abspielen! Für eine Übersicht schreib einfach **og commands**")

                .AddField("Willst du mich auf deinem eigenen Discord Server?",
                    "Das kannst du ganz einfach [hier](https://discord.com/api/oauth2/authorize?client_id=761895612291350538&permissions=383040&scope=bot) machen!")

                .AddField("Weitere Hilfe",
                    "Sollte ich mal nicht richtig funktionieren, komm ins [DrachenlordKoreaDiscord](https://discord.gg/MmWQ5pCsHa) oder wende dich bitte an meinen Erbauer:" + Environment.NewLine +
                    "[Discord](https://discordapp.com/users/386989432148066306) | [Reddit](https://www.reddit.com/user/MoriPastaPizza)")

                .AddField("Links",
                    "[Github](https://github.com/MoriPastaPizza/DiscordOgerBotWeb) | " +
                    "[Lade den Bot auf deinen Server ein!](https://discord.com/api/oauth2/authorize?client_id=761895612291350538&permissions=383040&scope=bot) | " +
                    "[DrachenlordKoreaDiscord](https://discord.gg/MmWQ5pCsHa)")

                .WithAuthor(Context.Client.CurrentUser)
                .WithFooter(footer =>
                    footer.Text =
                        Controller.OgerBot.FooterDictionary[random.Next(Controller.OgerBot.FooterDictionary.Count)])
                .WithColor(Color.Blue)
                .WithCurrentTimestamp()
                .Build();

            await Context.Channel.SendMessageAsync(embed: buildEmbed);
        }

        [Command("commands")]
        [Alias("command", "kommando", "kommandos", "sounds")]
        public async Task SendCommands()
        {
            var random = new Random();
            var commands = Controller.OgerBot.CommandService.Commands.ToList();
            commands = commands
                .Where(m => m.Name != "asia")
                .OrderBy(m => m.Name)
                .ToList();

            var commandList = commands.Select(command => $"**{command.Aliases.Aggregate((i, j) => i + " " + j)}** {command.Summary}").ToList();
            var fieldString = commandList.Aggregate((i, j) => i + " | " + j).ToString();

            var embedBuilder = new EmbedBuilder
            {
                Title = "GenericCommands"
            };

            embedBuilder
                .WithDescription(fieldString)
                .AddField("Links",
                    "[Github](https://github.com/MoriPastaPizza/DiscordOgerBotWeb) | " +
                    "[Lade den Bot auf deinen Server ein!](https://discord.com/api/oauth2/authorize?client_id=761895612291350538&permissions=383040&scope=bot) | " +
                    "[DrachenlordKoreaDiscord](https://discord.gg/MmWQ5pCsHa)")
                .WithAuthor(Context.Client.CurrentUser)
                .WithFooter(footer =>
                    footer.Text =
                        Controller.OgerBot.FooterDictionary[random.Next(Controller.OgerBot.FooterDictionary.Count)])
                .WithColor(Color.Red)
                .WithCurrentTimestamp();

            await Context.Channel.SendMessageAsync(embed: embedBuilder.Build());
        }
    }
}
