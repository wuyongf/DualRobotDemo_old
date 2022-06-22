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
            DualRobotLib.Core core = new Core();
            core.Connect(Model.CR15, "127.0.0.1", 9021);
            core.Connect(Model.CR7, "127.0.0.1", 60008);
            // core.Connect(Model.CR15, "192.168.3.125", 60008);
            // core.Connect(Model.CR7, "192.168.3.124", 60008);

            // examples: calibrated user frame data
            double[] Pos_Cr7_CalliBase = { 825.25, 6.38, 134.67, -2.11, 46.69, -0.40 };
            double[] Pos_Cr15_CalliBase = { 1223.50, -1.77, -304.52, 1.61, 44.25, -177.78 };
            core.RobotBaseCalibrationInit(Pos_Cr7_CalliBase, Pos_Cr15_CalliBase);

            // step4:
            // a. install testing tools.
            // b. tools setting, tcp speed.
            // c. define scene params.
            // d. robots move to init position.
            // e. define user frame
            //
            // b. examples: tcp data
            float[] tcp_cr7 = { 9, 0, 123, 0, -45, 0 };
            float[] tcp_cr15 = { 0, 0, 140, 0, 45, -90 };
            core.SetTCP(Model.CR15, tcp_cr15);
            core.SetTCP(Model.CR7, tcp_cr7);
            core.SetSpeed(Model.CR15, 100);
            core.SetSpeed(Model.CR7, 100);

            core.MoveTo(Model.CR15, Position.Home);
            core.MoveTo(Model.CR7, Position.Home);
            core.SceneParamInit(SceneName.Scene1C, new double[] { 1000, 0, 0, 0, 0, 0 });
            core.SceneRobotInit(SceneName.Scene1C);
            core.SetUserFrame(Model.CR15);
            core.SetUserFrame(Model.CR7);


            double[] param1C = {1000, 400, 400, 200, 200, 0.00};
            core.SceneParamInit(SceneName.Scene1C, param1C);
            core.SceneRobotInit(SceneName.Scene1C); // this method need wait
            core.SetUserFrame(Model.CR15);
            core.SetUserFrame(Model.CR7);
            core.Scene1C(MovementType.QuickCheck);

            core.SceneRobotInit(SceneName.Scene1C);
            core.Scene1C(MovementType.QuickCheck);
        }
    }
}
