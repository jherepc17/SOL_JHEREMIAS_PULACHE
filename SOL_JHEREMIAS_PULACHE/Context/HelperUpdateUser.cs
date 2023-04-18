using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SOL_JHEREMIAS_PULACHE.Context;
using SOL_JHEREMIAS_PULACHE.Models;
using static SOL_JHEREMIAS_PULACHE.Models.EnumAlert;
using Rotativa;
using SOL_JHEREMIAS_PULACHE.Models.ViewModel;
using System.ComponentModel;
using System.Threading;

namespace SOL_JHEREMIAS_PULACHE.Context
{
    public class HelperUpdateUser
    {
        public void StartScheduledTask()
        {
            var worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler((sender, e) =>
            {
                while (true)
                {
                    UpdateUserState();
                    Thread.Sleep(TimeSpan.FromMinutes(2));
                }
            });
            worker.RunWorkerAsync();
        }

        private async void UpdateUserState()
        {

            using (var ctx = new AppDatabaseContext())
            {
                var usuarios = await ctx.Prestamos.Include(a => a.Usuario)
                    .Where(p => p.FechaDevolucion == null)
                    .Select(p => p.Usuario).ToListAsync();


                usuarios.ForEach(a => a.Estado = false);
                ctx.SaveChanges();
            }
        }
    }
}