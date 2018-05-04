using BoboTech.EncyclopaediaMetallumViewer.UILogic.Services;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using static System.FormattableString;

namespace BoboTech.EncyclopaediaMetallumViewer.UILogic.ViewModels
{
    public class StartupViewModel : BaseViewModel
    {
        #region Fields

        List<AssemblyName> _assembliesToLoad = new List<AssemblyName>
        {
            new AssemblyName("PresentationFramework-SystemXml, Version=4.0.4.0, Culture=neutral, PublicKeyToken=b77a5c561934e089, processor architecture=MSIL"),
            new AssemblyName("System.Net.Http, Version=4.0.4.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processor architecture=MSIL"),
            new AssemblyName("System.Data, Version=4.0.4.0, Culture=neutral, PublicKeyToken=b77a5c561934e089, processor architecture=32"),
            new AssemblyName("System.ServiceModel, Version=4.0.4.0, Culture=neutral, PublicKeyToken=b77a5c561934e089, processor architecture=MSIL"),
            new AssemblyName("System.Numerics, Version=4.0.4.0, Culture=neutral, PublicKeyToken=b77a5c561934e089, processor architecture=MSIL"),
            new AssemblyName("PresentationFramework-SystemData, Version=4.0.4.0, Culture=neutral, PublicKeyToken=b77a5c561934e089, processor architecture=MSIL"),
            new AssemblyName("NLog"),
            new AssemblyName("Newtonsoft.Json"),
            new AssemblyName("AutoMapper")
        };

        #endregion

        #region Properties

        public virtual string State { get; set; } = "Loading app ...";

        public virtual string Copyright { get; set; } = Invariant($"Copyright © BoboTech {DateTime.Today:yyyy}");

        public virtual bool IsIndeterminate { get; set; } = true;

        public virtual double Progress { get; set; }

        #endregion

        #region Public methods (will be transformed to commands by ViewModelSource)

        public async Task ViewLoadedAsync()
        {
            var caller = $"{nameof(StartupViewModel)}.{nameof(ViewLoadedAsync)}";
            var debug = new Action<string>(x => Logger.Log.Debug(x, caller));
            try
            {
                double counter = 0;
                debug("Starting to manually load assemblies.");
                IsIndeterminate = false;
                foreach (var assemblyName in _assembliesToLoad)
                {
                    var sleepTask = Task.Delay(150);
                    State = $"Loading {assemblyName.Name} ...";
                    Assembly.Load(assemblyName);
                    await sleepTask;
                    counter++;
                    Progress = ((counter) / (_assembliesToLoad.Count + 2)) * 100;
                }
                debug("There shouln't be any more \"Loaded 'bla bla bla'\" except \"Anonymously Hosted DynamicMethods Assembly\" after this.");
                var initializeAutoMapperSleepTask = Task.Delay(250);
                State = "Initializing AutoMapper ...";
                MapperService.SingletonInstance.Initialize();
                Progress = ((counter + 1) / (_assembliesToLoad.Count + 2)) * 100;
                await initializeAutoMapperSleepTask;
                State = "App loaded.";
                Progress = 100;
                await Task.Delay(500);
                HostService.ShowInitialView();
                HostService.Close();
            }
            catch (Exception ex)
            {
                var errorId = Invariant($"{DateTime.Now:yyyyMMdd_HHmmss}");
                Logger.Log.Error(ex, "Failed to load app.", caller, errorId);
                MessageBoxService.ShowMessage($"Failed to load app. See log for more info. Error id is {errorId}.", WindowTitle, MessageButton.OK, MessageIcon.Error);
            }
        }

        #endregion
    }
}