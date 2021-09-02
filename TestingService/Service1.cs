using CoreLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TestingService
{
    public partial class Service1 : ServiceBase
    {
        Timer timer1;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer1 = new Timer(600000);
            timer1.Interval = 1000;
            timer1.Start();
            timer1.Elapsed += new ElapsedEventHandler(Timer_Elapsed);

        }

        protected override void OnStop()
        {
            timer1.Stop();

        }
        void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {

            try
            {
                Util.DatabaseUpdate();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                timer1.Start();
            }
        }
    }
}

