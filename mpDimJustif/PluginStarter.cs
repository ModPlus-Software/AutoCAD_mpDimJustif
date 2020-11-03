namespace mpDimJustif
{
    using System;
    using Autodesk.AutoCAD.Runtime;
    using AcApp = Autodesk.AutoCAD.ApplicationServices.Core.Application;

    /// <summary>
    /// Запуск плагина
    /// </summary>
    public class PluginStarter
    {
        private SelectCommandWindow _selectCommandWindow;

        [CommandMethod("ModPlus", "mpDimJustif", CommandFlags.Modal)]
        public void StartFunction()
        {
#if !Debug
            ModPlusAPI.Statistic.SendCommandStarting(new ModPlusConnector());
#endif
            if (_selectCommandWindow == null)
            {
                _selectCommandWindow = new SelectCommandWindow();
                _selectCommandWindow.Closed += win_Closed;
            }

            if (_selectCommandWindow.IsLoaded)
                _selectCommandWindow.Activate();
            else
                AcApp.ShowModelessWindow(AcApp.MainWindow.Handle, _selectCommandWindow);
        }

        private void win_Closed(object sender, EventArgs e)
        {
            _selectCommandWindow = null;
            Autodesk.AutoCAD.Internal.Utils.SetFocusToDwgView();
        }
    }
}
