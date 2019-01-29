using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Dawid8.DFB.Navigation;
using Dawid8.DFB.Navigation.Events;

namespace DFB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            InitializeComponent();


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Navigator nav = new Navigator
            {
                PathDisplay = textBox1,
                FilesDisplay = listView2,
                DrivesDisplay = listView1,
            };

            NavEvents navevents = new NavEvents
            {
                _navigator = nav,
            };    

            listView2.DoubleClick += new EventHandler(navevents.goToDirorOpenFile);
            button1.Click += new EventHandler(navevents.goBack);
            listView1.DoubleClick += new EventHandler(navevents.selectDrive);

            nav.SetDrives();
            nav.RefreshFileDisplay();

        }
        

    }


}
