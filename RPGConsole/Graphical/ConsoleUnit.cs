using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Graphical
{
    public class ConsoleUnit
    {
        //used to draw things to console....
        public List<ConsoleItem> consoleItems = new List<ConsoleItem>();

        public string lastMessage = "";
        public string buildMessage = "";
        public string firstmessage = "";
        public DiscordMessage Finalmessage;
        /// <summary>
        /// For those real "basic bitches!" this can add a whole "ConsoleItem" item to the list!
        /// </summary>
        /// <param name="item">The console item that you have instantiated!</param>
        public void AddConsoleItem(ConsoleItem item, DiscordMessage message)
        {
            consoleItems.Add(item);
            if (!Program.discordBot)
            {
                Console.WriteLine(item.text);
            }
            else
            {
                if(item.text == null || item.text == string.Empty || item.text == " ") { item.text = "Waiting for message..."; }
                Console.WriteLine("[EXTERNAL][" + DateTime.Now + "] - " + item.text);
                if (message != null)
                {

                    if (lastMessage == string.Empty || Finalmessage == null)
                    {
                        Finalmessage = message.RespondAsync(item.text).Result;
                        firstmessage = item.text + "\n";
                        if (Program.debugMode) { Console.WriteLine("Deployed beginning reply message with " + Finalmessage.Content); }
                    }
                    else
                    {
                        if(Finalmessage != null)
                        {
                            buildMessage = buildMessage + item.text + "\n";
                            if (Program.debugMode) { Console.WriteLine("Added " + item.text + " to the buildMessage variable making it " + buildMessage); }
                            //find if the end point is valid if so modify the message at the end there
                        }
                        else
                        {
                            if (Program.debugMode) { Console.WriteLine("Unfortunately Console Writer was unable to write to that message"); }
                        }
                    }

                    if(item.id == "/")
                    {
                        item.id = "/" + message.Content.Replace("!", "");
                        if (Program.debugMode) { Console.WriteLine("Found ending message with no command value added a command value!"); }
                    }

                    if (item.id == null || item.id == string.Empty) {
                        //get the command from the command
                        if (Program.debugMode) { Console.WriteLine("Found no command avaliable... adding one..."); }
                        lastMessage = message.Content.Replace("!", "");
                    }
                    else
                    {
                        if (Program.debugMode) { Console.WriteLine("Adding message to the last message variable"); }
                        lastMessage = item.id;
                    }

                    if (lastMessage.Contains("/"))
                    {
                        if (Program.debugMode) { Console.WriteLine("Found endpoint engaging ending sequence"); }
                        lastMessage.Replace("/", "");
                        if (Program.debugMode) { Console.WriteLine("Removed / line"); }
                        DiscordMessageBuilder build = new DiscordMessageBuilder();
                        build.Content = firstmessage + buildMessage;
                        if (Program.debugMode) { Console.WriteLine("Created and added a Discord Builder with " + build.Content); }
                        if(build.Content.Contains("Waiting for message..."))
                        {
                            build.Content = build.Content.Replace("Waiting for message...", "");
                        }
                        Finalmessage.ModifyAsync(build);
                        if (Program.debugMode) { Console.WriteLine("Modified Message complete"); }
                        lastMessage = string.Empty;
                        buildMessage = string.Empty;
                        firstmessage = string.Empty;
                        if (Finalmessage.Content == "Waiting for message...")
                        {
                            //huh an error occured
                            if (Program.debugMode) { Console.WriteLine("Unfortunately a error has occured..."); }
                            Finalmessage.ModifyAsync("Unfortunately, this bot didnt respond to this command correctly! please report this error to the bot's creator!");
                        }
                        Finalmessage = null;
                        if (Program.debugMode) { Console.WriteLine("Finished and cleaned bot message"); }
                    }
                }
            }
        }

        public void AddConsoleItem(ConsoleItem item)
        {
            AddConsoleItem(item, null);
        }

        /// <summary>
        /// This is where we can add a time for the duration of the message
        /// </summary>
        /// <param name="text">text to send to the console!</param>
        /// <param name="time">the time it takes for the message to dissapear!</param>
        public void AddConsoleItem(string text, int time, string id, DiscordMessage message)
        {
            ConsoleItem item = new ConsoleItem(time, text, id);
            AddConsoleItem(item, message);
        }

        public void AddConsoleItem(string text, int time, DiscordMessage message)
        {
            AddConsoleItem(text, time, "", message);
        }

        public void AddConsoleItem(string text, int time, string id)
        {
            AddConsoleItem(text, time, id, null);
        }

        public void AddConsoleItem(string text, int time)
        {
            AddConsoleItem(text, time, "");
        }

        /// <summary>
        /// This is for the shortened version!
        /// </summary>
        /// <param name="text">the text to send to the console!</param>
        public void AddConsoleItem(string text, string id, DiscordMessage message)
        {
            AddConsoleItem(text, 3, id, message);
        }

        public void AddConsoleItem(string text, DiscordMessage message)
        {
            AddConsoleItem(text, "", message);
        }

        public void AddConsoleItem(string text)
        {
            AddConsoleItem(text, "", null);
        }

        public void AddConsoleItem(string text, string id)
        {
            AddConsoleItem(text, id);
        }

        /// <summary>
        /// When you want to remove a item from the console you can do it here!
        /// </summary>
        /// <param name="item">what item do you want to remove?</param>
        public void RemoveConsoleItem(ConsoleItem item)
        {
            if(Program.debugMode == true)
            {
                //dont remove
                Console.WriteLine("tried to remove a console element!");
            }
            else
            {
                consoleItems.Remove(item);
            }
        }

        public void DisplayUnit()
        {

        }
    }
}
