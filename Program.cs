using System;
using System.Collections;
using System.IO;
using Newtonsoft.Json;

namespace TeamFortressData
{
    class Program
    {


        /// <summary>
        /// GLobal static data.
        /// </summary>
        static HttpServer DataServer;
        static string TF2Dir = @"C:\Program Files (x86)\Steam\steamapps\common\Team Fortress 2\tf";
        static int linesRead = 0;
        static ArrayList Players = new ArrayList();
        static TF2DataContainer tfdata = new TF2DataContainer();

        /// <summary>
        /// Setup, tf2 needs to log its console output into a file, you can do that by running a command "con_logfile "filename.log""
        /// A autoexec.cfg exists within the directory of tf2, which allows for autoexecution of certain commands. By changing the config
        /// to include the con_logfile command it makes sure the log files that this programmas needs to read exists.
        /// This is all done automatically.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            Console.WriteLine("Setting Up Auto-Config TF2 within default steam directory!");
            Console.WriteLine(TF2Dir);

            string ConfigData = "";
            bool OverWrite = false;
            using (StreamReader Reader = new StreamReader(new FileStream(TF2Dir + @"\cfg\autoexec.cfg", FileMode.Open, FileAccess.ReadWrite)))
            {
                ConfigData = Reader.ReadToEnd();   

                Console.WriteLine("Adding TFData command to cfg if not present!");

                if (!ConfigData.Contains("con_logfile \"tfdata.log\"")){

                    OverWrite = true;
                    Console.WriteLine("Log Command is not present in Config");
                    Console.WriteLine("Creating Backup of Current Config");
                    using (StreamWriter Writer = new StreamWriter(new FileStream(TF2Dir + @"\cfg\autoexecTFDBack.cfg", FileMode.OpenOrCreate, FileAccess.ReadWrite)))
                    {
                        Writer.Write(ConfigData);
                    }

                    ConfigData = ConfigData + Environment.NewLine + "echo \"TFData Script Succesfully Loaded!\"" + Environment.NewLine + "con_logfile \"tfdata.log\"";

                   
                } else
                {
                    Console.WriteLine("Config is compatible with TFData :D, you can launch TF2 now ^^");
                }


            }

            if (OverWrite)
            {
                Console.WriteLine("Writing new Config");
                using (StreamWriter Writer = new StreamWriter(new FileStream(TF2Dir + @"\cfg\autoexec.cfg", FileMode.OpenOrCreate, FileAccess.ReadWrite)))
                {
                    Writer.Write("");
                    Writer.Flush();
                    Writer.Write(ConfigData);
                    Writer.Flush();
                }

                Console.WriteLine("Config creation finished! You can launch TF2 now!");
            }

            tfdata.ServerIP = "0.0.0.0:0000";
            tfdata.TotalKills = 0;
            tfdata.TotalPlayers = 0;
            tfdata.TotalSuicides = 0;
            tfdata.TotalAmountOfSentryKills = 0;
            tfdata.TotalAmountOfHeadshots = 0;
            tfdata.TotalAmountOfCrits = 0;
            tfdata.TotalAmountOfBackstabs = 0;
            tfdata.TotalAmountOfReflectKills = 0;

            DataServer = new HttpServer(MessageReceivedCallback, 6365);


            Console.ReadLine();

        }

        /// <summary>
        /// Sends Data back to client when clients asks for it. Fist the log file is being parsed on a few attributes, which are
        /// later combined into a json format which the client can easily parse!
        /// </summary>
        /// <param name="messagereceived"></param>
        private static void MessageReceivedCallback(string messagereceived)
        {
            Console.WriteLine("HTTP MESSAGE: " + messagereceived);

            if (messagereceived.Contains("tf2data"))
            {
                if(File.Exists(TF2Dir + @"\tfdata.log"))
                {
                    bool shouldRemove = false;
                    using (StreamReader Reader = new StreamReader(new FileStream(TF2Dir + @"\tfdata.log", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite)))
                    {
                        string Data = Reader.ReadToEnd();
                        string[] ConnectedParts = Data.Split(new string[] { "Connecting to" }, StringSplitOptions.None);
                        int amountOfConnections = ConnectedParts.Length;
                        string[] Lines = ConnectedParts[amountOfConnections - 1].Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                        for (int LinesRead = linesRead; LinesRead < Lines.Length; LinesRead++)
                        {
                            linesRead = LinesRead;
                            string Line = Lines[linesRead];
                            try
                            {
                                Line = Lines[linesRead + 1]; 
                            } catch
                            {
                                Line = Lines[linesRead];
                            }
                            
                            if (Line.ToLower().Contains("connected to"))
                            {
                                try
                                {
                                    Players = new ArrayList();
                                    tfdata = new TF2DataContainer();
                                    tfdata.TotalKills = 0;
                                    tfdata.TotalPlayers = 0;
                                    tfdata.TotalSuicides = 0;
                                    tfdata.TotalAmountOfSentryKills = 0;
                                    tfdata.TotalAmountOfHeadshots = 0;
                                    tfdata.TotalAmountOfCrits = 0;
                                    tfdata.TotalAmountOfBackstabs = 0;
                                    tfdata.TotalAmountOfReflectKills = 0;
                                    tfdata.ServerIP = Line.Replace("connected to", "");
                                    linesRead = 0;
                                } catch
                                {

                                }
                                
                            }

                            if (Line.ToLower().Contains("shutdowngc"))
                            {
                                shouldRemove = true;
                                
                            }

                            if (Line.ToLower().Contains("killed"))
                            {
                                tfdata.TotalKills = tfdata.TotalKills + 1;
                                string playerWhoKilled = Line.Split(' ')[0];
                                string playerWhoDied = Line.Split(' ')[2];
                                if (!Players.Contains(playerWhoKilled))
                                {
                                    Players.Add(playerWhoKilled);
                                }
                                if (!Players.Contains(playerWhoDied))
                                {
                                    Players.Add(playerWhoDied);
                                }
                            }

                            if (Line.ToLower().Contains("suicided"))
                            {
                                tfdata.TotalSuicides = tfdata.TotalSuicides + 1;
                            }

                            if (Line.ToLower().Contains("(crit)"))
                            {
                                tfdata.TotalAmountOfCrits = tfdata.TotalAmountOfCrits + 1;
                            }

                            if (Line.ToLower().Contains("obj_sentrygun"))
                            {
                                tfdata.TotalAmountOfSentryKills = tfdata.TotalAmountOfSentryKills + 1;
                            }

                            if (Line.ToLower().Contains("deflect_rocket"))
                            {
                                tfdata.TotalAmountOfReflectKills = tfdata.TotalAmountOfReflectKills + 1;
                            }

                            if ((Line.ToLower().Contains("sniperrifle") || Line.ToLower().Contains("machina") || Line.ToLower().Contains("awper_hand") || Line.ToLower().Contains("tf_projectile_arrow") || Line.ToLower().Contains("fortified_compound") || Line.ToLower().Contains("bazaar_bargain") || Line.ToLower().Contains("hitmans's_heatmaker") || Line.ToLower().Contains("classic") || Line.ToLower().Contains("ambassador")) && Line.ToLower().Contains("(crit)"))
                            {
                                tfdata.TotalAmountOfHeadshots = tfdata.TotalAmountOfHeadshots + 1;
                            }

                            if ((Line.ToLower().Contains("knife") || Line.ToLower().Contains("spy_cicle") || Line.ToLower().Contains("sharp_dresser") || Line.ToLower().Contains("black_rose") || Line.ToLower().Contains("saxxy") || Line.ToLower().Contains("your_eternal_reward") || Line.ToLower().Contains("wanga_prick") || Line.ToLower().Contains("connivers's_kunai") || Line.ToLower().Contains("big_earner")) && Line.ToLower().Contains("(crit)"))
                            {
                                tfdata.TotalAmountOfBackstabs = tfdata.TotalAmountOfBackstabs + 1;
                            }
                        }

                        tfdata.AllPlayers = Players;
                        var json = JsonConvert.SerializeObject(tfdata);
                        DataServer.JsonToSend(json);
                        Console.WriteLine("Succesfully send json!");
                    }

                    if (shouldRemove)
                    {
                        try
                        {
                            File.Delete(TF2Dir + @"\tfdata.log");
                            Console.WriteLine("TF2 IS NOT RUNNING (ANYMORE)");
                        }
                        catch (IOException ex)
                        {
                            Console.WriteLine("COULD NOT REMOVE LOG: " + ex.ToString());
                        }
                    }
                } else
                {
                    Console.WriteLine("File \"tfdata.log\" Does Not Exist :( ");
                    DataServer.SendMessage("FILE NOT AVAILABLE :(");
                }               
            }
        }
    }
}
