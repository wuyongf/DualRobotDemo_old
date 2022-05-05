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
            // step1: Robot Installation & Calibration. (skip)
            // step2: TCP Calibration.
            // step3: RobotBase Calibration.
            // step4: Measurement Initialization.
            // step5: Scene1B

            // step2:
            // a. install calibration tool.
            // b. install calibration plate & calibration pin.
            // c. 4 points method.
            // d. record calibrated tcp data.

            // step3:
            // a. uninstall calibration pin.
            // b. 3 point method.
            // c. record calibrated user frame data. 
            //  c.1. robot connection.
            //  c.2. record calibrated user frame data. 

            DualRobotLib.Core core = new Core();
            core.Connect(Model.CR15, "127.0.0.1", 9021);
            core.Connect(Model.CR7, "127.0.0.1", 60008);
            // core.Connect(Model.CR15, "192.168.3.125", 60008);
            // core.Connect(Model.CR7, "192.168.3.124", 60008);

            // examples: calibrated user frame data
            double[] Pos_Cr7_CalliBase = { 708.92, 1.98, 51.15, -2.11, 46.69, -0.4 };
            double[] Pos_Cr15_CalliBase = { 1337.41, -1.24, -386.08, -178.4, 44.25, -177.79 };
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
            // c. examples.
            double[] param = { 1000, 130, 0.5, 180, 90, 180, 45 };
            core.SceneParamInit(SceneName.Scene1B, param);
            // d.
            core.SceneRobotInit(SceneName.Scene1B);
            // e.
            core.SetUserFrame(Model.CR15);
            core.SetUserFrame(Model.CR7);

            // step5: Scene1B
            core.Scene1B(MovementType.QuickCheck);
        }
    }
}
