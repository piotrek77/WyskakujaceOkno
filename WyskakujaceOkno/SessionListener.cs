using Soneta.Business;
using Soneta.Business.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using WyskakujaceOkno;

[assembly: Service(typeof(ISessionListener), typeof(SessionListener), ServiceScope.Session)]
namespace WyskakujaceOkno
{
    internal class SessionListener : ISessionListener
    {


        static System.Timers.Timer timer = null;

        Session SessionGlobal { get; set; }


        public void BusinessCommit(Session session)
        {
            
        }

        public void Created(Session session)
        {
            if (session == null)
            {

                return;
            }
            if (!session.IsClosed && session.ToString().Equals("Sesja: Loginer"))
            {
                SessionGlobal = session;


                if (timer == null)
                {

                     



                    timer = new System.Timers.Timer(10000);



                    timer.Elapsed += OnTimerTick;
                    timer.AutoReset = true;
                    timer.Start();

                }



            }
        }

        private void OnTimerTick(object sender, ElapsedEventArgs e)
        {
            if (SessionGlobal == null)
                return;

            using (Session session = SessionGlobal.Login.CreateSession(false, false, "WyskakujaceOkno"))
            {
                Guid guid = new Guid("0e0af773-0172-44b4-86cd-7d552942e720");
                Soneta.Handel.HandelModule handelModule = Soneta.Handel.HandelModule.GetInstance(session);
                var row = handelModule.DokHandlowe[guid];

                Context cx = Context.Empty.Clone(session);
                cx[row.GetType()] = row;

                var on = new ObjectNavigator(row, session, cx);

                FormManager.OpenForm(on, false);
            }
            }

        public void OnAdded(Row row)
        {
            
        }

        public void OnDeleted(Row row)
        {
            
        }

        public void OnDeleting(Row row)
        {
            
        }

        public void OnLoaded(Row row)
        {
            
        }

        public void Saved(Session session)
        {
            
        }

        public void Saving(Session session)
        {
            
        }

        public void ServerSaved(Session session)
        {
            
        }

        public void ServerSaving(Session session)
        {
            
        }
    }
}
