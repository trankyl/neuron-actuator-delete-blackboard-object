using Brain4FuncApp;
using System.Timers;

namespace nabbodel
{
    class Program
    {
        

        static int gduree_vie_bbo = 1000;

        
      private static  System.Timers.Timer gTimer = new System.Timers.Timer();

        static void Main(string[] args)
        {
            try
            {
                
                      Brain4FuncApp.Brain.load_setting(ref Brain4FuncApp.Brain.Setting);
         

                    string linput_memory = "";

                    
                    linput_memory = Brain4FuncApp.Brain.get_valeur("cacheblackboard" , Brain4FuncApp.Brain.Setting);
                
                    string lstr_duree_vie_stimuli = Brain4FuncApp.Brain.get_valeur("duree_vie_stimuli" , Brain4FuncApp.Brain.Setting);
                
               gduree_vie_bbo = int.Parse(lstr_duree_vie_stimuli);

               Brain4FuncApp.BlackBoard.Stimuli lbbo = new Brain4FuncApp.BlackBoard.Stimuli("valeur=un" , "pensee");
                lbbo.DateCreation = DateTime.Now;
                Brain4FuncApp.BlackBoard.BlackBoard.AddBlackBoardObjectEnLeSerialisant(lbbo);


        gTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        gTimer.Interval = 1000;
        gTimer.Enabled = true;


        Console.WriteLine("Press \'q\' to quit the sample.");
        while (Console.Read() != 'q') ;

            }
            catch
            {

            }
        }


          // Specify what you want to happen when the Elapsed event is raised.
        private static void OnTimedEvent(object source,  ElapsedEventArgs e)
        {
            try
            {

                gTimer.Enabled = false;

                DateTime ldate_bbo = DateTime.Now;
                DateTime ltemps_expiration_bbo = DateTime.Now.AddSeconds(gduree_vie_bbo);

                 var lliste_bbo =    Brain4FuncApp.BlackBoard.BlackBoard.inspect_static(Brain4FuncApp.Brain.Setting);

                foreach (var lbbo12 in lliste_bbo)
                {
                    ldate_bbo = lbbo12.DateCreation;
                    ltemps_expiration_bbo = ldate_bbo.AddSeconds(gduree_vie_bbo);

                    if (ltemps_expiration_bbo.CompareTo(DateTime.Now) < 0)
                        continue;

                    lbbo12.delete();

                    
                }
            }
            catch
            {

            }
            finally
            {
                gTimer.Enabled = true;
            }
        }
    }
}