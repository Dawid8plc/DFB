using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Dawid8.DFB.Navigation;

namespace Dawid8.DFB.Navigation.Events
{
    class NavEvents
    {
        public Navigator _navigator;

        ///<summary>
        ///<para>Opens a file or a folder</para>
        ///</summary>
        public void goToDirorOpenFile(object sender, EventArgs e)
        {

            string[] test = (string[])_navigator.FilesDisplay.SelectedItems[0].Tag;
            if (test[1] == "Folder")
            {
                _navigator.CurDir = test[0];
                _navigator.PathDisplay.Text = _navigator.CurDir;
                _navigator.RefreshFileDisplay();
            }
            else if (test[1] == "File")
            {
                ProcessStartInfo proc = new ProcessStartInfo();
                proc.FileName = test[0];

                Process.Start(proc);
            }
        }

        ///<summary>
        ///<para>Goes back to the upper directory</para>
        ///</summary>
        public void goBack(object sender, EventArgs e)
        {

            _navigator.CurDir = _navigator.CurDir.TrimEnd(Convert.ToChar(@"\"));
            _navigator.CurDir = _navigator.CurDir.Remove(_navigator.CurDir.LastIndexOf(Convert.ToChar(@"\")) + 1);
            _navigator.RefreshFileDisplay();
            _navigator.PathDisplay.Text = _navigator.CurDir;
        }

        ///<summary>
        ///<para>Changes the currently selected drive</para>
        ///</summary>
        public void selectDrive(object sender, EventArgs e)
        {

            _navigator.CurDir = _navigator.DrivesDisplay.SelectedItems[0].Text;
            _navigator.RefreshFileDisplay();
            _navigator.PathDisplay.Text = _navigator.CurDir;
        }

        
    }
}
