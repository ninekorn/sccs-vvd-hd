using System.Text.RegularExpressions;

namespace sccsscriptrewrite
{
    internal static class Program
    {





        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            sccsScriptAutoCorrectNMultiplyRewriteCleanPurify();


            Application.Run(new Form1());



        }


        public static void sccsScriptAutoCorrectNMultiplyRewriteCleanPurify()
        {
            //Console.WriteLine("1_mainThreadStarter");
            var updateMainUITitle2 = new Action(() =>
            {



                //_mainUpdateThread();
            });
            // XmlDocument doc = new XmlDocument();
            //doc.Load("");

            string pathToDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            //Console.WriteLine("2_mainThreadStarter");
            string folderName = "#VEscriptgen";
            DirectoryInfo directoryFolder = new DirectoryInfo(folderName);
            directoryFolder.Refresh();
            string pathToDesktopFolder = pathToDesktop + @"\" + folderName;

            //Console.WriteLine("2_mainThreadStarter");
            if (!Directory.Exists(pathToDesktopFolder))
            {
                Console.WriteLine("0the directory !exists");
                Directory.CreateDirectory(pathToDesktopFolder);
            }
            else
            {
                Console.WriteLine("0the directory exists");
            }



            string folderNameOriginal = "original";
            DirectoryInfo dirnameOriginal = new DirectoryInfo(folderNameOriginal);
            dirnameOriginal.Refresh();
            //Console.WriteLine("2_mainThreadStarter");
            if (!Directory.Exists(pathToDesktop + @"\" + folderName))
            {
                Console.WriteLine("1the directory !exists");
                Directory.CreateDirectory(folderNameOriginal);
            }
            else
            {
                Console.WriteLine("1the directory exists");
            }

            string finalPathOriginal = pathToDesktop + @"\" + folderName + @"\" + folderNameOriginal;
            Console.WriteLine(finalPathOriginal);




            string folderNameMul = "multiplied";
            string mulPath = pathToDesktop + @"\" + folderName + @"\" + folderNameMul;
            if (!Directory.Exists(mulPath))
            {
                Console.WriteLine("2the directory !exists");
                Directory.CreateDirectory(mulPath);
            }
            else
            {
                Console.WriteLine("2the directory exists");
            }


            var arrayOfFiles = Directory.GetFiles(finalPathOriginal);


            if (arrayOfFiles.Length > 0)
            {
                Console.WriteLine("Got Files");
            }
            else
            {
                Console.WriteLine("!Got Files");
            }

            DirectoryInfo dirnameMulNewFiles = new DirectoryInfo(mulPath);

            string[] arrayOfOnlyfileNamesFormationOne = new string[arrayOfFiles.Length];
            string[] arrayOfOnlyfileNamesFormationORIGINAL = new string[arrayOfFiles.Length];

            var indexToChange = 0;
            var mainIterator = 0;

            var formationLength = 5;


            for (int i = 0; i < arrayOfOnlyfileNamesFormationORIGINAL.Length; i++)
            {
                arrayOfOnlyfileNamesFormationORIGINAL[i] = arrayOfFiles[i];
            }


            for (int f = 0; f < formationLength; f++)
            {

                var indexMain = f + 1;
                for (int i = 0; i < arrayOfFiles.Length; i++)
                {
                    arrayOfFiles[i] = arrayOfOnlyfileNamesFormationORIGINAL[i];
                    /*if (mainIterator >= arrayOfFiles.Length)
                    {
                        mainIterator = 0;
                        indexToChange += 1;
                    }*/
                    //Console.WriteLine("3the directory exists");

                    var filenewPath = folderNameMul + @"\" + arrayOfFiles[i];

                    var someString = pathToDesktop + @"\" + folderName + @"\" + folderNameOriginal + @"\";
                    //var test = arrayOfFiles[i];
                    var newstring = arrayOfFiles[i].Substring(someString.Length);
                    arrayOfOnlyfileNamesFormationOne[i] = newstring;

                    string readtextfromfile = File.ReadAllText(arrayOfFiles[i]);



                    //INCORPORATE CODE TO SEARCH FOR CODE TO CLEAN THE SCRIPTS THOROUGHLY.
                    //INCORPORATE CODE TO SEARCH FOR CODE TO CLEAN THE SCRIPTS THOROUGHLY.
                    //INCORPORATE CODE TO SEARCH FOR CODE TO CLEAN THE SCRIPTS THOROUGHLY.
                    //INCORPORATE CODE TO SEARCH FOR CODE TO CLEAN THE SCRIPTS THOROUGHLY.
                    //INCORPORATE CODE TO SEARCH FOR CODE TO CLEAN THE SCRIPTS THOROUGHLY.

                    var somePathMul = Path.Combine(pathToDesktop + @"\" + folderName + @"\" + folderNameMul, newstring);


                    //Console.WriteLine(somePathMul);
                    File.WriteAllText(somePathMul, readtextfromfile);
                    //Path.ChangeExtension(somePathMul, ".cs")


                    if (Directory.Exists(pathToDesktop + @"\" + folderName + @"\" + folderNameMul))
                    {
                        FileInfo filinfo = new FileInfo(pathToDesktop + @"\" + folderName + @"\" + folderNameMul);
                        filinfo.Refresh();
                    }

                    if (File.Exists(somePathMul))
                    {
                        //Console.WriteLine("02File.Exists");
                        FileInfo filinfo = new FileInfo(somePathMul);
                        filinfo.Refresh();

                        //Path.ChangeExtension(somePathMul, ".cs");
                    }
                    else
                    {
                        if (File.Exists(somePathMul))
                        {
                            //Console.WriteLine("02File.Exists");
                            FileInfo filinfo = new FileInfo(somePathMul);
                            filinfo.Refresh();

                            //Path.ChangeExtension(somePathMul, ".cs");
                        }
                    }
                    //Path.ChangeExtension(lastPath, ".xml");

                    //File.WriteAllText(someLastString, File.ReadAllText(someLastString).Replace("Combat_cc_", lastString).Replace("_1", lastString2));
                }
            }


            /*

            for (int f = 0; f < formationLength; f++)
            {

                var indexMain = f + 1;
                for (int i = 0; i < arrayOfFiles.Length; i++)
                {

                    var someString = pathToDesktop + @"\" + folderName + @"\" + folderNameOriginal + @"\";
                    var newstring = arrayOfFiles[i].Substring(someString.Length);

                    var somePathMul = Path.Combine(pathToDesktop + @"\" + folderName + @"\" + folderNameMul, newstring);


                 

                    //Path.ChangeExtension(lastPath, ".xml");

                    //File.WriteAllText(someLastString, File.ReadAllText(someLastString).Replace("Combat_cc_", lastString).Replace("_1", lastString2));
                }
            }
            */


        }



















        static int someInitItems = 0;

        //UI THREAD TEST
        //////////////////////////////////
        //////////////////////////////////
        public static void sccscopymodifymultiplyfilesmodded()
        {



            //Console.WriteLine("0_mainThreadStarter");
            if (someInitItems == 0)
            {
                //Console.WriteLine("1_mainThreadStarter");
                var updateMainUITitle2 = new Action(() =>
                {



                    //_mainUpdateThread();
                });
                // XmlDocument doc = new XmlDocument();
                //doc.Load("");

                string pathToDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                //Console.WriteLine("2_mainThreadStarter");
                string folderName = "#VEscriptgen";
                DirectoryInfo directoryFolder = new DirectoryInfo(folderName);
                directoryFolder.Refresh();
                string pathToDesktopFolder = pathToDesktop + @"\" + folderName;

                //Console.WriteLine("2_mainThreadStarter");
                if (!Directory.Exists(pathToDesktopFolder))
                {
                    Console.WriteLine("0the directory !exists");
                    Directory.CreateDirectory(pathToDesktopFolder);
                }
                else
                {
                    Console.WriteLine("0the directory exists");
                }



                string folderNameOriginal = "original";
                DirectoryInfo dirnameOriginal = new DirectoryInfo(folderNameOriginal);
                dirnameOriginal.Refresh();
                //Console.WriteLine("2_mainThreadStarter");
                if (!Directory.Exists(pathToDesktop + @"\" + folderName))
                {
                    Console.WriteLine("1the directory !exists");
                    Directory.CreateDirectory(folderNameOriginal);
                }
                else
                {
                    Console.WriteLine("1the directory exists");
                }

                string finalPathOriginal = pathToDesktop + @"\" + folderName + @"\" + folderNameOriginal;
                Console.WriteLine(finalPathOriginal);




                string folderNameMul = "multiplied";
                string mulPath = pathToDesktop + @"\" + folderName + @"\" + folderNameMul;
                if (!Directory.Exists(mulPath))
                {
                    Console.WriteLine("2the directory !exists");
                    Directory.CreateDirectory(mulPath);
                }
                else
                {
                    Console.WriteLine("2the directory exists");
                }


                var arrayOfFiles = Directory.GetFiles(finalPathOriginal);


                if (arrayOfFiles.Length > 0)
                {
                    Console.WriteLine("Got Files");
                }
                else
                {
                    Console.WriteLine("!Got Files");
                }

                DirectoryInfo dirnameMulNewFiles = new DirectoryInfo(mulPath);

                string[] arrayOfOnlyfileNamesFormationOne = new string[arrayOfFiles.Length];
                string[] arrayOfOnlyfileNamesFormationORIGINAL = new string[arrayOfFiles.Length];

                var indexToChange = 0;
                var mainIterator = 0;

                var formationLength = 5;


                for (int i = 0; i < arrayOfOnlyfileNamesFormationORIGINAL.Length; i++)
                {
                    arrayOfOnlyfileNamesFormationORIGINAL[i] = arrayOfFiles[i];
                }


                for (int f = 0; f < formationLength; f++)
                {

                    var indexMain = f + 1;
                    for (int i = 0; i < arrayOfFiles.Length; i++)
                    {
                        arrayOfFiles[i] = arrayOfOnlyfileNamesFormationORIGINAL[i];
                        /*if (mainIterator >= arrayOfFiles.Length)
                        {
                            mainIterator = 0;
                            indexToChange += 1;
                        }*/
                        //Console.WriteLine("3the directory exists");

                        var filenewPath = folderNameMul + @"\" + arrayOfFiles[i];

                        var someString = pathToDesktop + @"\" + folderName + @"\" + folderNameOriginal + @"\";
                        //var test = arrayOfFiles[i];
                        var newstring = arrayOfFiles[i].Substring(someString.Length);
                        arrayOfOnlyfileNamesFormationOne[i] = newstring;

                        //var removedExtension = arrayOfFiles[i].Substring(someString.Length);

                        var someOtherString = Path.ChangeExtension(newstring, "");
                        //Console.WriteLine(newstring);

                        var somePathOri = Path.Combine(pathToDesktop + @"\" + folderName + @"\" + folderNameOriginal, newstring);
                        var somePathMul = Path.Combine(pathToDesktop + @"\" + folderName + @"\" + folderNameMul, newstring);

                        var someStringOri = pathToDesktop + @"\" + folderName + @"\" + folderNameMul + @"\";
                        var somestuff = somePathMul;// Path.ChangeExtension(somePathMul, ".cs");

                        somestuff = somestuff.Substring(someStringOri.Length);

                        var arrayOfFilesToMod = somestuff;
                        string st = "_" + (indexMain);
                        //arrayOfFilesToMod.Replace("_1", st);

                        /*Regex regex = new Regex(@"cc_");
                        var input = arrayOfFilesToMod;
                        var output = regex.Replace(input, "Mining_");*/

                        Regex regex2 = new Regex(@"_1");
                        var input2 = arrayOfFilesToMod; //output
                        var output2 = regex2.Replace(input2, "_" + indexMain);

                        /*foreach (var test in arrayOfFilesToMod.Where(x => x == "_1"))
                        {
                            
                        }*/

                        //File.ReadAllText(arrayOfFilesToMod).Replace("_1", st);
                        //Console.WriteLine(output);

                        var lastPath = pathToDesktop + @"\" + folderName + @"\" + folderNameMul + @"\" + output2;
                        File.Copy(arrayOfFiles[i], lastPath, true); //Path.ChangeExtension(lastPath, ".cs")
                    }
                }

                dirnameMulNewFiles.Refresh();

                arrayOfOnlyfileNamesFormationOne = Directory.GetFiles(mulPath);

                string[] arrayOfFilesNames = new string[arrayOfOnlyfileNamesFormationOne.Length];
                for (int i = 0; i < arrayOfOnlyfileNamesFormationOne.Length; i++)
                {
                    arrayOfFilesNames[i] = arrayOfOnlyfileNamesFormationOne[i];// Replace("1", "2");
                }

                /*string[] arrayOfOnlyfileNamesFormationTwo = new string[arrayOfOnlyfileNamesFormationOne.Length];
                string[] arrayOfOnlyfileNamesFormationThree = new string[arrayOfOnlyfileNamesFormationOne.Length];
                string[] arrayOfOnlyfileNamesFormationFour = new string[arrayOfOnlyfileNamesFormationOne.Length];
                string[] arrayOfOnlyfileNamesFormationFive = new string[arrayOfOnlyfileNamesFormationOne.Length];

                for (int i = 0; i < arrayOfOnlyfileNamesFormationOne.Length; i++)
                {
                    arrayOfOnlyfileNamesFormationTwo[i] = arrayOfOnlyfileNamesFormationOne[i];// Replace("1", "2");
                    arrayOfOnlyfileNamesFormationThree[i] = arrayOfOnlyfileNamesFormationOne[i];// .Replace("2", "3");
                    arrayOfOnlyfileNamesFormationFour[i] = arrayOfOnlyfileNamesFormationOne[i];// .Replace("3", "4");
                    arrayOfOnlyfileNamesFormationFive[i] = arrayOfOnlyfileNamesFormationOne[i];// .Replace("4", "5");
                }*/




                for (int f = 0; f < formationLength; f++)
                {
                    var indexMain = f + 1;

                    var currentStringIndexMain = "_" + indexMain;

                    for (int i = 0; i < arrayOfOnlyfileNamesFormationOne.Length; i++)
                    {
                        //if (arrayOfOnlyfileNamesFormationOne[i].Contains(currentStringIndexMain))
                        {
                            var someString = pathToDesktop + @"\" + folderName + @"\" + folderNameMul + @"\";
                            var newstring = arrayOfOnlyfileNamesFormationOne[i];//.Substring(someString.Length);
                            var someLastString = newstring;// someString + newstring;

                            Console.WriteLine(someLastString);


                            var menuOption = 2;


                            if (menuOption == 1)
                            {

                            }
                            else if (menuOption == 0)
                            {

                                var lastString = "Mining_";
                                var lastString2 = "_" + indexMain;
                                File.WriteAllText(someLastString, File.ReadAllText(someLastString).Replace("Combat_cc_", lastString).Replace("_1", lastString2));



                                if (File.Exists(someLastString))
                                {
                                    //Console.WriteLine("02File.Exists");
                                    FileInfo filinfo = new FileInfo(someLastString);
                                    filinfo.Refresh();
                                }


                                //var newString = File.ReadAllText(someLastString);

                                lastString = "Mining_";
                                lastString2 = "_" + indexMain;
                                File.WriteAllText(someLastString, File.ReadAllText(someLastString).Replace("cc_", lastString).Replace("_1", lastString2));

                            }
                            else
                            {
                                /*var lastString = "Mining_";
                                var lastString2 = "_" + indexMain;
                                File.WriteAllText(someLastString, File.ReadAllText(someLastString).Replace("Combat_cc_", lastString).Replace("_1", lastString2));*/



                                if (File.Exists(someLastString))
                                {
                                    //Console.WriteLine("02File.Exists");
                                    FileInfo filinfo = new FileInfo(someLastString);
                                    filinfo.Refresh();
                                }


                                //var newString = File.ReadAllText(someLastString);

                                //lastString = "Mining_";
                                var lastString2 = "_" + indexMain;
                                File.WriteAllText(someLastString, File.ReadAllText(someLastString).Replace("_1", lastString2));
                            }









                             File.Copy(someLastString, someLastString, true);// Path.ChangeExtension(someLastString, ".js")




                            //Console.WriteLine(someLastString);

                            //File.WriteAllText(someLastString, File.ReadAllText(someLastString).Replace("_1", lastString));

                            //Regex regex2 = new Regex(@"_1");
                            // var input2 = arrayOfFilesToMod;
                            //var output2 = regex2.Replace(input2, "_" + indexMain);


                            //Regex regex = new Regex(@"cc_1");
                            //var input = someLastString;
                            //var output = regex.Replace(input, "Mining_" + indexMain);

                            //File.WriteAllText(someLastString, output);

                            //File.Copy(someLastString, someLastString, true);// Path.ChangeExtension(someLastString, ".js")
                        }
                    }
                }

                dirnameMulNewFiles.Refresh();

                //arrayOfOnlyfileNamesFormationOne = Directory.GetFiles(mulPath);



                for (int i = 0; i < arrayOfFilesNames.Length; i++)
                {
                    //Console.WriteLine(arrayOfFilesNames[i]);
                    //Directory.Fil(mulPath + @"\" + arrayOfOnlyfileNamesFormationOne[i]);
                    if (File.Exists(arrayOfOnlyfileNamesFormationOne[i]))
                    {
                        //Console.WriteLine("02File.Exists");
                        FileInfo filinfo = new FileInfo(arrayOfOnlyfileNamesFormationOne[i]);
                        filinfo.Delete();
                    }

                }



                dirnameMulNewFiles.Refresh();





                Console.WriteLine("ended scripts multiplication");

                //var someString = pathToDesktop + @"\" + folderName + @"\" + folderNameMul + @"\";
                //var test = arrayOfFiles[i];
                //var newstring = arrayOfFiles[i].Substring(someString.Length);
                //var someTest = newstring; // arrayOfOnlyfileNamesFormationOne[i] 



                /*for (int i = 0; i < arrayOfOnlyfileNamesFormationOne.Length; i++)
                {
                    if (arrayOfOnlyfileNamesFormationOne[i].Contains(""))
                    {

                    }
                    //Console.WriteLine(arrayOfOnlyfileNamesFormationFive[i]);

                    if (File.Exists(arrayOfOnlyfileNamesFormationOne[i]))
                    {
                        //Console.WriteLine("02File.Exists");
                        FileInfo filinfo = new FileInfo(arrayOfOnlyfileNamesFormationOne[i]);
                        filinfo.Refresh();
                    }
                    var filenewPath = folderNameMul + @"\" + arrayOfOnlyfileNamesFormationOne[i];

                    var someString = pathToDesktop + @"\" + folderName + @"\" + folderNameMul + @"\";
                    //var test = arrayOfOnlyfileNamesFormationOne[i];
                    var newstring = arrayOfOnlyfileNamesFormationOne[i].Substring(someString.Length);

                    //newstring = newstring+ ".xml";

                    //var somePathMul = Path.Combine(pathToDesktop + @"\" + folderName + @"\" + folderNameMul, newstring);

                    var someLastString = someString + newstring;

                    //Console.WriteLine(someLastString);

                    if (File.Exists(someLastString))
                    {
                        //Console.WriteLine("02File.Exists");
                        FileInfo filinfo = new FileInfo(someLastString);
                        filinfo.Refresh();

                        var someResults = File.ReadAllText(someLastString);

                        someResults.Replace("_1", "_5");

                        //File.WriteAllText(someLastString, someResults);

                        File.WriteAllText(someLastString, File.ReadAllText(someLastString).Replace("_1", "_5"));
                    }
                    else
                    {
                       // Console.WriteLine("02File.!Exists");
                    }
                }*/

























                //arrayOfOnlyfileNames[i]
                //Console.WriteLine(someResults);








                /*using (XmlReader xmlreader = new XmlTextReader(someLastString))
                {
                    //Console.WriteLine("element: ");
                    while (xmlreader.Read())
                    {
                        Console.WriteLine("element: ");
                        //var element = xmlreader.Name;
                        //Console.WriteLine("element: " + element);
                    }
                }*/
                /*XDocument doc = XDocument.Load(someLastString);

                using (var reader = XmlReader.Create(someLastString))
                {
                    reader.Dispose();
                    //doc = XDocument.Load(lastPath2);
                }*/



                //XDocument doc = new XDocument();
                //string xmlString = "<book><title>Oberon's Legacy</title></book>";
                //doc.Load(new StringReader(someLastString));
                //var lastPath2 = pathToDesktop + @"\" + folderName + @"\" + folderNameMul + @"\" + someOtherString + ".xml";
                //XmlDocument doc = new XmlDocument();
                //doc.Load(lastPath);










                //File.Move(arrayOfFiles[i], Path.ChangeExtension(lastPath, ".xml"));
                //dirnameMulNewFiles.Refresh();

                // Console.WriteLine("4the directory exists " + arrayOfFiles[i]);

                /*if (!File.Exists(lastPath))
                {
                    Console.WriteLine("00!File.Exists");
                    FileInfo filinfo = new FileInfo(lastPath);
                    filinfo.Refresh();
                }
                else
                {
                    Console.WriteLine("01File.Exists");
                }*/
                //var result = Path.ChangeExtension(lastPath, ".xml");


                /*var lastPath2 = pathToDesktop + @"\" + folderName + @"\" + folderNameMul + @"\" + someOtherString + ".xml";
                //XmlDocument doc = new XmlDocument();
                //doc.Load(lastPath);


                if (File.Exists(lastPath2))
                {
                    Console.WriteLine("02File.Exists");
                    FileInfo filinfo = new FileInfo(lastPath2);
                    filinfo.Refresh();
                }
                else
                {
                    Console.WriteLine("02File.!Exists");
                }*/
                //XDocument doc = XDocument.Load(lastPath2);
                //doc.Save(lastPath2);


                /*using (XmlTextReader textReader = new XmlTextReader(lastPath2))
                {
                    while (textReader.Read())
                    {
                        var element = textReader.Name;
                        //Console.WriteLine(element + "");

                    }
                    textReader.Close();
                }*/


                //XmlDocument doc = new XmlDocument();
                //doc.Load(lastPath2);


                //                   System.Text.Encoding.UTF8







                //textReader.Read();




                /*XDocument doc = XDocument.Load(lastPath2);

                using (var reader = XmlReader.Create(lastPath2))
                {
                    reader.Dispose();
                    //doc = XDocument.Load(lastPath2);
                }*/



                //XDocument doc = XDocument.Load(lastPath);
                //doc.Save(lastPath2);




                //System.Xml.XmlText

                //XmlReader xmlreader = new XmlReader();

                //XmlTextWriter writer = new XmlTextWriter(path, System.Text.Encoding.UTF8);
                /*using (XmlTextReader xmlreader = new XmlTextReader(lastPath2))
                {
                    while (xmlreader.Read())
                    {

                    }
                }*/
                //new StringReader(lastPath2))

                //File.Move();

                /*if (File.Exists(arrayOfFiles[i]))
                {
                    FileInfo filinfo = new FileInfo(arrayOfFiles[i]);
                    filinfo.Refresh();
                }*/

                /*var result = Path.ChangeExtension(lastPath,".xml");
                Console.WriteLine(result);
                if (File.Exists(lastPath))
                {
                    FileInfo filinfo = new FileInfo(lastPath);
                    filinfo.Refresh();
                }*/




                /*for (int i = 0; i < arrayOfOnlyfileNamesFormationOne.Length; i++)
                {
                    arrayOfOnlyfileNamesFormationTwo[i].Replace("1", "2");
                    arrayOfOnlyfileNamesFormationThree[i].Replace("1", "3");
                    arrayOfOnlyfileNamesFormationFour[i].Replace("1", "4");
                    arrayOfOnlyfileNamesFormationFive[i].Replace("1", "5");
                }*/






                //XmlDocument doc = new XmlDocument();
                //doc.Load("");










                //Console.WriteLine("3_mainThreadStarter");
                //System.Windows.Threading.Dispatcher.Invoke(updateMainUITitle2);

                /*if (!File.Exists(folderName))
                {
                    FileInfo filinfo = new FileInfo(folderName);
                    filinfo.Refresh();
                }*/

                //threadOneGrammarLoad();
                someInitItems = 3;
            }






            //Console.Title = "" + _mainThreadFrameCounter.ToString();

            //SC_GCGollect.GCCollectUtility(100);
        }
        //////////////////////////////////
        //////////////////////////////////

    }
}