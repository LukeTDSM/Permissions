using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Permissions;
using Terraria_Server.Plugin;
using Terraria_Server;
using Terraria_Server.Commands;
using Terraria_Server.Events;

namespace Permissions
{
	public class Permissions : Plugin
    {
        public static string permissionsxml;
        public static string pluginFolder;
		public override void Load()
		{
			Name = "Permissions";
			Description = "Plugin for easy permissions";
			Author = "Luke";
			Version = "4.0";
			TDSMBuild = 25;
			Console.WriteLine("Initializing permissions...");
			PermissionManager.LoadPermissions();
			Console.WriteLine("Done!");

            pluginFolder = Statics.PluginPath + Path.DirectorySeparatorChar + "Permissions";
            permissionsxml = pluginFolder + Path.DirectorySeparatorChar + "permissions.xml";

			//Load hooks
			registerHook(Hooks.PLAYER_CHAT);
			
			//Create Directory
            if(!Directory.Exists(pluginFolder))
                Directory.CreateDirectory(pluginFolder);
			
            //Create permissions file
            if (!File.Exists(permissionsxml))
                File.Create(permissionsxml);
		}

		public override void Enable()
		{
			
		}

		public override void Disable()
		{
			
		}
		
		public override void onPlayerChat(MessageEvent Event)
        {
            var player = ((Player) Event.Sender).whoAmi;
            var pname = ((Player) Event.Sender).Name;
            var message = Event.Message;
            Console.WriteLine("<" + Server + "> " + message); //Displays chat in the console.
            try
            {
                SendAllMessage(
                  "<" + PermissionManager.FindPlayer(player).GetPrefix() + pname + "> " + message,
                  PermissionManager.FindPlayer(player).GetColor()[0],
                  PermissionManager.FindPlayer(player).GetColor()[1],
                  PermissionManager.FindPlayer(player).GetColor()[2]);
            }
            catch (Exception)
            {
                SendAllMessage("<" + pname + "> " + message, 255f, 240f, 20f);
            }
            Event.Cancelled = true;
            return;
        }
        
        public void SendAllMessage(string message, float Red, float Green, float Blue)
        {
            NetMessage.SendData(25, -1, -1, message, 255, Red, Green, Blue);
        }
	}
}

