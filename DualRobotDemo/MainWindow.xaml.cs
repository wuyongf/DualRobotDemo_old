using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DualRobotLib;

namespace DualRobotDemo
{
    /// <summary>
    /// Dual Robot Example Code
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DualRobotLib.Core core = new Core();

            // 1. establish connection
            // core.Connect(Model.CR7, "192.168.3.124", 60008);
            var result1 = core.Connect(Model.CR7, "127.0.0.1", 60008);

            var result2 = core.GetProgramStatus(Model.CR7);

            Console.WriteLine("result:" + result1);
            Console.WriteLine("result:" + result2);
        }
    }
}
