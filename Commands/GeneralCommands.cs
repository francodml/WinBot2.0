﻿using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WBot2.Commands.Attributes;
using WBot2.Helpers.Interfaces;

namespace WBot2.Commands
{
    public class GeneralCommands : BaseCommandModule
    {
        public override string ModuleName => "General Commands";
        protected readonly ILogger _logger;
        protected readonly IHelpFormatter<DiscordEmbedBuilder> _helpFormatter;

        public GeneralCommands(IServiceProvider serviceProvider, ICommandHandler commandHandler) : base(serviceProvider, commandHandler)
        {
            _logger = serviceProvider.GetRequiredService<ILogger<GeneralCommands>>();
            _helpFormatter = serviceProvider.GetRequiredService<IHelpFormatter<DiscordEmbedBuilder>>();
        }

        [Command("help"), Description("Shows all commands and their descriptions")]
        public async Task Help(CommandContext ctx)
        {
            DiscordEmbedBuilder embed = await _helpFormatter.FormatHelp(RegisteringHandler);
            embed.WithAuthor(_discordClient.CurrentUser.Username, iconUrl:_discordClient.CurrentUser.AvatarUrl)
                .WithColor(ctx.Member.Color);
            await ctx.Message.RespondAsync($"{ctx.User.Mention} these are the available commands",embed);
        }

        [Command("ping"), Description("Pings the bot, and gets a reply!")]
        [Alias("pong", "p")]
        public async Task Ping(CommandContext ctx)
        {
            await ctx.Message.RespondAsync($"{ctx.User.Mention} Pong!");
        }
        [Command("say"), Description("Makes the bot say something")]
        public async Task Say(CommandContext ctx, string args)
        {
            await ctx.Message.RespondAsync($"You told me to say: '{string.Join(" ", args)}'");
        }

        [Command("uwu")]
        [NeedsPermissions(DSharpPlus.Permissions.Administrator | DSharpPlus.Permissions.BanMembers)]
        public async Task Uwu(CommandContext ctx, int waitTime)
        {
            await ctx.RespondAsync($"Ok, I'll wait {waitTime} seconds!");
            waitTime *= 1000;
            await Task.Delay(waitTime);
            await ctx.TriggerTypingAsync();
            await ctx.Message.RespondAsync("UwU");
        }
    }
}
