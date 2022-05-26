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
            // step5: Scene2

            // step2:
            // a. install calibration tool.
            // b. install calibration plate & calibration pin.
            // c. 6 points method.
            // d. record calibrated tcp data.

            // step3:
            // a. uninstall calibration pin.
            // b. 3 point method.
            // c. record calibrated user frame data. 
            //  c.1. robot connection.
            //  c.2. record calibrated user frame data. 
            //**c.3. record calibrated rotate plate center data.

            DualRobotLib.Core core = new Core();
            core.Connect(Model.CR15, "127.0.0.1", 9021);
            core.Connect(Model.CR7, "127.0.0.1", 60008);
            // core.Connect(Model.CR15, "192.168.3.125", 60008);
            // core.Connect(Model.CR7, "192.168.3.124", 60008);

            // examples: calibrated user frame data
            double[] Pos_Cr7_CalliBase = { 825.25, 6.38, 134.67, -2.11, 46.69, -0.40 };
            double[] Pos_Cr15_CalliBase = { 1223.50, -1.77, -304.52, 1.61, 44.25, -177.78 };
            core.RobotBaseCalibrationInit(Pos_Cr7_CalliBase, Pos_Cr15_CalliBase);

            // examples: rotate plate center data.
            double[] Pos_Cr7_PlateCenter = { 828.51, -50, 61.10, 2.74, 1.11, -90.00 };
            core.RotationPlateCalibrationInit(Pos_Cr7_PlateCenter);

            // step4:
            // a. install testing tools.
            // b. tools setting, tcp speed.
            // c. define scene params.
            // d. robots move to init position.
            // e. define user frame
            //
            // b. examples: tcp data
            float[] tcp_cr7 = { -125, 50, 45, 90, 0, 180 };
            float[] tcp_cr15 = { 0, 134, 182, 0, 0, 90 };
            core.SetTCP(Model.CR15, tcp_cr15);
            core.SetTCP(Model.CR7, tcp_cr7);
            core.SetSpeed(Model.CR15, 100);
            core.SetSpeed(Model.CR7, 100);
            // c. examples.
            double[] param = { 430, 100, 10, 250, 90, 45 };
            core.SceneParamInit(SceneName.Scene2, param);
            // d.
            core.SceneRobotInit(SceneName.Scene2);
            // e.
            core.SetUserFrame(Model.CR15);
            core.SetUserFrame(Model.CR7);

            // step5: Scene2
            core.Scene2(MovementType.QuickCheck);

        }
    }
}
