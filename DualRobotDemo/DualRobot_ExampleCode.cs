using System;
using System.Threading;
using DualRobotLib;

namespace DualRobotDemo
{
    internal class DualRobot_ExampleCode
    {
        /// Methods
        public void Connect()
        {
            DualRobotLib.Core core = new Core();

            // 1. establish connection
            // core.Connect(Model.CR7, "192.168.3.124", 60008);
            var result = core.Connect(Model.CR7, "127.0.0.1", 60008);

            Console.WriteLine("result:" + result);
        }

        public void Disconnect()
        {
            DualRobotLib.Core core = new Core();

            // 1. establish connection
            // core.Connect(Model.CR7, "192.168.3.124", 60008);
            var result = core.Connect(Model.CR7, "127.0.0.1", 60008);
            result = core.Connect(Model.CR7, "127.0.0.1", 60008);
            result = core.Disconnect(Model.CR7);
            result = core.Connect(Model.CR7, "127.0.0.1", 60008);
            result = core.Disconnect(Model.CR7);
            result = core.Connect(Model.CR7, "127.0.0.1", 60008);
            result = core.Disconnect(Model.CR7);
            result = core.Connect(Model.CR7, "127.0.0.1", 60008);
            result = core.Disconnect(Model.CR7);
            result = core.Connect(Model.CR7, "127.0.0.1", 60008);


            core.Connect(Model.CR15, "127.0.0.1", 9021);
            core.Disconnect(Model.CR15);
            core.Connect(Model.CR15, "127.0.0.1", 9021);

            Console.WriteLine("result:" + result);
        }

        public void GetTcpDistance()
        {
            // (1) prerequisite: finish step3
            // (2) define tcp
            // (3) get tcp distance in real time

            // (1)
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

            // (2)
            float[] tcp_cr7 = { 9, 0, 123, 0, -45, 0 };
            float[] tcp_cr15 = { 0, 0, 140, 0, 45, -90 };
            core.SetTCP(Model.CR15, tcp_cr15);
            core.SetTCP(Model.CR7, tcp_cr7);

            // (3)
            var tcp_distance = core.GetTcpDistance();

            Console.WriteLine("tcp_distance: " + tcp_distance);
        }

        public void GetSpanningRange()
        {
            // 0, 200, 180, 100, 90, 0
            // 200, 500, 300, 250, 150, 0
            // 400, 600, 500, 300, 250, 0
            // 500, 600, 500, 300, 250, 0
            // 800, 800, 600, 400, 300, 0
            // 1000, 800, 400, 400, 200, 0


            DualRobotLib.Core core = new Core();

            var h = core.GetSpanningHeight(1000);

            var w = core.GetSpanningWidth(1000);

            Console.WriteLine("width: " + w);
            Console.WriteLine("height: " + h);

        }

        public void GetMoveFlag()
        {
            // Thread th1 = new Thread(() => core.thread_MoveFlag(Model.CR15));
            // th1.Start();
        }

        public void BasicMovementDemo()
        {
            DualRobotLib.Core core = new Core();
            core.Connect(Model.CR15, "127.0.0.1", 9021);
            core.Connect(Model.CR7, "127.0.0.1", 60008);

            // For Cr15

            // 1. Get UF Cur Pos
            var uf_pos = core.GetUFCurPos(Model.CR15);

            // 2. Get RB Cur Pos
            var rb_pos = core.GetRBCurPos(Model.CR15);

            // 3. UFMove
            float[] tcp_cr15 = { 0, 0, 140, 0, 45, -90 };
            core.SetTCP(Model.CR15, tcp_cr15);
            core.SetUserFrame(Model.CR15);

            double[] pos = new[] { 0.0, 0.0, 200, 0.0, 0.0, 0.0 };
            core.UFMove(Model.CR15, pos);

            // 4. RBMove
            double[] pos1 = new[] { 1277.247, 4.829875, 100.56811, -178.5574, 36.90373, -178.0444 };
            core.UFMove(Model.CR15, pos1);

            // For Cr7

            // 1. Get UF Cur Pos
            var uf_pos1 = core.GetUFCurPos(Model.CR7);

            // 2. Get RB Cur Pos
            var rb_pos1 = core.GetRBCurPos(Model.CR7);

            // 3. UFMove
            float[] tcp_cr7 = { 9, 0, 123, 0, -45, 0 };
            core.SetTCP(Model.CR7, tcp_cr7);
            core.SetUserFrame(Model.CR7);

            double[] pos2 = new[] { 0.0, 0.0, 50, 0.0, 0.0, 0.0 };
            core.UFMove(Model.CR7, pos2);

            // 4. RBMove
            double[] pos3 = new[] { 624.72, 4.60, 270.40, -1.87, 39.35, -0.05 };
            core.UFMove(Model.CR7, pos3);
            
        }

        /// CR7
        public void CR7_MoveTo_Demo()
        {
            DualRobotLib.Core core = new Core();

            // 1. establish connection
            // core.Connect(Model.CR7, "192.168.3.124", 60008);
            core.Connect(Model.CR7, "127.0.0.1", 60008);

            // 2. movement test

            core.MoveTo(Model.CR7, Position.Home);

            core.MoveTo(Model.CR7, Position.InitPos_Scene1B);

            core.MoveTo(Model.CR7, Position.Home);

            core.MoveTo(Model.CR7, Position.InitPos_Scene1B);
        }

        public void CR7_BasicMovement_Demo()
        {
            DualRobotLib.Core core = new Core();

            // 1. establish connection
            core.Connect(Model.CR7, "192.168.3.124", 60008);
            // core.Connect(Model.CR7, "127.0.0.1", 60008);

            // 2. movement test

            // 2.1 define the tcp
            float[] tcp_cr7 = { 9, 0, 123, 0, -45, 0 };
            core.SetTCP(Model.CR7, tcp_cr7);

            // 2.2 define the tcp speed
            core.SetSpeed(Model.CR7, 100);

            // 2.3 define the user frame
            core.SetUserFrame(Model.CR7);

            // 2.4.1 Set Single Point
            float[] pos_cr7 = { 50, 0, 0, 0, 0, 0 };
            core.SetSinglePoint(Model.CR7, pos_cr7);

            // 2.4.2 Move
            core.MoveSinglePoint(Model.CR7);

            // 2.4.3 Set Single Point
            float[] pos_cr7_2 = { -50, 0, 0, 0, 0, 0 };
            core.SetSinglePoint(Model.CR7, pos_cr7_2);

            // 2.4.4 Move
            core.MoveSinglePoint(Model.CR7);

            // 2.4.5 Set Single Point
            float[] pos_cr7_3 = { 0, 0, 0, 0, 0, 0 };
            core.SetSinglePoint(Model.CR7, pos_cr7_3);

            // 2.4.6 Move
            core.MoveSinglePoint(Model.CR7);
        }

        /// CR15
        public void CR15_MoveTo_Demo()
        {
            DualRobotLib.Core core = new Core();

            // 1. establish connection
            core.Connect(Model.CR15, "127.0.0.1", 9021);
            // core.Connect(Model.CR15, "192.168.3.125", 60008);

            // 2. movement test

            core.MoveTo(Model.CR15, Position.Home);

            core.MoveTo(Model.CR15, Position.InitPos_Scene1B);

            core.MoveTo(Model.CR15, Position.Home);

            core.MoveTo(Model.CR15, Position.InitPos_Scene1B);
        }

        public void CR15_BasicMovement_Demo()
        {
            DualRobotLib.Core core = new Core();
            core.Connect(Model.CR15, "127.0.0.1", 9021);
            core.Connect(Model.CR7, "127.0.0.1", 60008);

            // Get UF Cur Pos
            var uf_pos = core.GetUFCurPos(Model.CR15);

            // Get RB Cur Pos
            var rb_pos = core.GetRBCurPos(Model.CR15);

            // UFMove
            float[] tcp_cr15 = { 0, 0, 140, 0, 45, -90 };
            core.SetTCP(Model.CR15, tcp_cr15);
            core.SetUserFrame(Model.CR15);

            double[] pos = new[] { 0.0, 0.0, 200, 0.0, 0.0, 0.0 };
            core.UFMove(Model.CR15, pos);

            // RBMove
            double[] pos1 = new[] { 1277.247, 4.829875, 100.56811, -178.5574, 36.90373, -178.0444 };
            core.UFMove(Model.CR15, pos1);
        }

        /// DualRobot
        public void DualRobot_Scene1A_Demo_WithCalibration()
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
            double[] Pos_Cr7_PlateCenter = { 828.51, -50, 561.10, 2.74, 1.11, -90.00 };
            core.SetStationAntennaTCP_Cr7(Pos_Cr7_PlateCenter);

            // step4:
            // a. install testing tools.
            // b. tools setting, tcp speed.
            // c. define scene params.
            // d. robots move to init position.
            // e. define user frame
            //
            // b. examples: tcp data
            float[] tcp_cr7 = { 9, 0, 123, 0, -45, 0 };
            float[] tcp_cr15 = { 0, 134, 182, 0, 0, 90 };
            core.SetTCP(Model.CR15, tcp_cr15);
            core.SetTCP(Model.CR7, tcp_cr7);
            core.SetSpeed(Model.CR15, 100);
            core.SetSpeed(Model.CR7, 100);
            // c. examples.
            double[] param = { 300, 180, 10, 250, 90, 45 };
            core.SceneParamInit(SceneName.Scene1A, param);
            // d.
            core.SceneRobotInit(SceneName.Scene1A);
            // e.
            core.SetUserFrame(Model.CR15);
            core.SetUserFrame(Model.CR7);

            // step5: Scene1A
            core.Scene1A(MovementType.QuickCheck);
        }

        public void DualRobot_Scene1B_Demo_WithCalibration()
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
            // c. examples.
            // double[] param = { 1000, 130, 10, 180, 90, 180, 45 };
            // double[] param = { 1000, 130, 0.5, 180, 90, 180, 45 };
            // double[] param = { 200, 102, 34, 102, 34, 180, 60 };
            double[] param = { 1000, 130, 10, 50, 25, 180, 45 };
            core.SceneParamInit(SceneName.Scene1B, param);
            // d.
            core.SceneRobotInit(SceneName.Scene1B);
            // e.
            core.SetUserFrame(Model.CR15);
            core.SetUserFrame(Model.CR7);

            // step5: Scene1B
            core.Scene1B(MovementType.QuickCheck);
        }

        public void DualRobot_Scene1B_Demo_WithCalibration_TBC()
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
            float[] tcp_cr7 = { -61.7107f, 0, 193.7107f, 0, -45, 0 };
            float[] tcp_cr15 = { 0, 0, 140, 0, 45, -90 };
            core.SetTCP(Model.CR15, tcp_cr15);
            core.SetTCP(Model.CR7, tcp_cr7);
            core.SetSpeed(Model.CR15, 100);
            core.SetSpeed(Model.CR7, 100);
            // c. examples.
            // double[] param = { 1000, 130, 10, 180, 90, 180, 45 };
            // double[] param = { 1000, 130, 0.5, 180, 90, 180, 45 };
            // double[] param = { 200, 102, 34, 102, 34, 180, 60 };
            double[] param = { 10, 130, 10, 50, 25, 180, 45 };
            core.SceneParamInit(SceneName.Scene1B, param);
            // d.
            core.SceneRobotInit(SceneName.Scene1B);
            // e.
            core.SetUserFrame(Model.CR15);
            core.SetUserFrame(Model.CR7);

            // step5: Scene1B
            core.Scene1B(MovementType.QuickCheck);
        }

        public void DualRobot_Scene1C_Demo_WithCalibration()
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
            // c. examples.
            double[] param = { 250, 299.9, 229.9, 14, 11, 0 };
            core.SceneParamInit(SceneName.Scene1C, param);
            // d.
            core.SceneRobotInit(SceneName.Scene1C);
            // e.
            core.SetUserFrame(Model.CR15);
            core.SetUserFrame(Model.CR7);

            // step5: Scene1C
            // Thread th_scene1c = new Thread(() => core.Scene1C(MovementType.QuickCheck));
            // th_scene1c.Start();

            // Thread th1 = new Thread(() => core.thread_MoveFlag(Model.CR15));
            // th1.Start();

            core.Scene1C(MovementType.QuickCheck);

            Console.WriteLine("Waiting...");

            // Thread.Sleep(100000000);

            Console.WriteLine("Done");
        }

        public void DualRobot_Scene1C_Demo_WithCalibration_TBC()
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
            float[] tcp_cr7 = { -61.7107f, 0, 193.7107f, 0, -45, 0 };
            float[] tcp_cr15 = { 0, 0, 140, 0, 45, -90 };
            core.SetTCP(Model.CR15, tcp_cr15);
            core.SetTCP(Model.CR7, tcp_cr7);
            core.SetSpeed(Model.CR15, 100);
            core.SetSpeed(Model.CR7, 100);
            // c. examples.
            //double[] param = { 200, 350, 350, 4, 4, 0 };
            double[] param = { 20, 350, 350, 4, 4, 0 };
            core.SceneParamInit(SceneName.Scene1C, param);
            // d.
            core.SceneRobotInit(SceneName.Scene1C);
            // e.
            core.SetUserFrame(Model.CR15);
            core.SetUserFrame(Model.CR7);

            // step5: Scene1C
            // Thread th_scene1c = new Thread(() => core.Scene1C(MovementType.QuickCheck));
            // th_scene1c.Start();

            // Thread th1 = new Thread(() => core.thread_MoveFlag(Model.CR15));
            // th1.Start();

            core.Scene1C(MovementType.QuickCheck);

            Console.WriteLine("Waiting...");

            // Thread.Sleep(100000000);

            Console.WriteLine("Done");
        }

        public void DualRobot_Scene2_Demo_WithCalibration()
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
            core.SetStationAntennaTCP_Cr7(Pos_Cr7_PlateCenter);

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

        // CityU-Demo
        public void DualRobot_Scene1B_CityU()
        {
            // (1) Connection
            DualRobotLib.Core core = new Core();
            core.Connect(Model.CR15, "127.0.0.1", 9021);
            core.Connect(Model.CR7, "127.0.0.1", 60008);
            // core.Connect(Model.CR15, "192.168.0.125", 60008);
            // core.Connect(Model.CR7, "192.168.0.124", 60008);

            // (2) Get Calibrated Co-Frame Data
            // % Pos_Cr7_CalliBase 
            // % cal-0: 779.422, -37.794, -339.305, 0.351, 0.523, -92.267
            // % cal-1: 779.282, -38.284, -286.598, 0.588, 0.510, -92.060
            // % cal-2: 778.281, -38.520, -336.517, 0.810, 0.687, -92.389
            //
            // % Pos_Cr15_CalliBase 
            // % cal-0: 1249.821, 22.977, -774.474, 0.149, 0.109, 89.261 //error: 3-4mm
            // % cal-1: 1252.218, 23.278, -722.122, 0.082, 0.179, 89.359 //error: 2.5mm
            // % cal-2: 1252.171, 24.657, -770.478, 0.269, 0.423, 89.225 //error: 0.2mm
            double[] Pos_Cr7_CalliBase = { 778.281, -38.520, -336.517, 0.810, 0.687, -92.389 };
            double[] Pos_Cr15_CalliBase = { 1252.171, 24.657, -770.478, 0.269, 0.423, 89.225 };
            core.RobotBaseCalibrationInit(Pos_Cr7_CalliBase, Pos_Cr15_CalliBase);

            // (3) Get Tool Antenna TCP Data 

            float[] origin_wpr_cr7 = { 176.753f, 8.741f, 174.332f };
            float[] default_wpr_cr7 = { -3.884f, 53.543f, -9.229f };

            float[] origin_wpr_cr15 = { -0.596f, 29.023f, -2.64f };
            float[] default_wpr_cr15 = { 105.071f, -0.865f, 89.288f };

            var cal_wpr_cr7 = core.GetToolFixtureWPR(SceneName.Scene1B, Model.CR7, origin_wpr_cr7, default_wpr_cr7);
            var cal_wpr_cr15 = core.GetToolFixtureWPR(SceneName.Scene1B, Model.CR15, origin_wpr_cr15, default_wpr_cr15);

            float[] cal_pin_tcp_cr7 = { -61.97f, 1.016f, 193.006f, cal_wpr_cr7[0], cal_wpr_cr7[1], cal_wpr_cr7[2] };
            float[] cal_pin_tcp_cr15 = { -1.946f, -35.828f, 174.092f, cal_wpr_cr15[0], cal_wpr_cr15[1], cal_wpr_cr15[2] };
            float cal_pin_length_cr7 = 45.23f + 5.02f;
            float cal_pin_length_cr15 = 45.33f + 5.02f;
            var fixture_tcp_cr7 = core.GetToolFixtureTCP(Model.CR7, cal_pin_tcp_cr7, cal_pin_length_cr7);
            var fixture_tcp_cr15 = core.GetToolFixtureTCP(Model.CR15, cal_pin_tcp_cr15, cal_pin_length_cr15);

            // offset-1: cr7: 6.27f;  cr15:6.24f;
            // offset-2: cr7: 16.05f; cr15: 16.03f;
            float[] antenna_offset_cr7 = { 0.0f, 0.0f, 6.27f, 0.0f, 0.0f, 0.0f };
            float[] antenna_offset_cr15 = { 0.0f, 0.0f, 6.24f, 0.0f, 0.0f, 0.0f };
            var tcp_cr7 = core.GetToolAntennaTCP(Model.CR7, fixture_tcp_cr7, antenna_offset_cr7);
            var tcp_cr15 = core.GetToolAntennaTCP(Model.CR15, fixture_tcp_cr15, antenna_offset_cr15);
            Console.WriteLine("tcp_cr7: " + tcp_cr7);
            Console.WriteLine("tcp_cr15: " + tcp_cr15);

            // (4) Robot Initialization
            core.SetTCP(Model.CR15, tcp_cr15);
            core.SetTCP(Model.CR7, tcp_cr7);
            core.SetSpeed(Model.CR15, 100);
            core.SetSpeed(Model.CR7, 100);
            // c. examples.
            // double[] param = { 1000, 130, 10, 180, 90, 180, 45 };
            // double[] param = { 200, 102, 34, 102, 34, 180, 60 };
            double[] param = { 200, 130, 10, 50, 25, 180, 45 };
            core.SceneParamInit(SceneName.Scene1B, param);
            // d.
            core.SceneRobotInit(SceneName.Scene1B);
            // e.
            core.SetUserFrame(Model.CR15);
            core.SetUserFrame(Model.CR7);

            // step5: Scene1B
            core.Scene1B(MovementType.QuickCheck);
        }
        public void DualRobot_Scene1C_CityU()
        {
            // (1) Connection
            DualRobotLib.Core core = new Core();
            core.Connect(Model.CR15, "127.0.0.1", 9021);
            core.Connect(Model.CR7, "127.0.0.1", 60008);
            // core.Connect(Model.CR15, "192.168.0.125", 60008);
            // core.Connect(Model.CR7, "192.168.0.124", 60008);

            // (2) Get Calibrated Co-Frame Data
            // % Pos_Cr7_CalliBase 
            // % cal-0: 779.422, -37.794, -339.305, 0.351, 0.523, -92.267
            // % cal-1: 779.282, -38.284, -286.598, 0.588, 0.510, -92.060
            // % cal-2: 778.281, -38.520, -336.517, 0.810, 0.687, -92.389
            //
            // % Pos_Cr15_CalliBase 
            // % cal-0: 1249.821, 22.977, -774.474, 0.149, 0.109, 89.261 //error: 3-4mm
            // % cal-1: 1252.218, 23.278, -722.122, 0.082, 0.179, 89.359 //error: 2.5mm
            // % cal-2: 1252.171, 24.657, -770.478, 0.269, 0.423, 89.225 //error: 0.2mm
            double[] Pos_Cr7_CalliBase = { 778.281, -38.520, -336.517, 0.810, 0.687, -92.389 };
            double[] Pos_Cr15_CalliBase = { 1252.171, 24.657, -770.478, 0.269, 0.423, 89.225 };
            core.RobotBaseCalibrationInit(Pos_Cr7_CalliBase, Pos_Cr15_CalliBase);

            // (3) Get Tool Antenna TCP Data 

            float[] origin_wpr_cr7 = { 176.753f, 8.741f, 174.332f };
            float[] default_wpr_cr7 = { -3.884f, 53.543f, -9.229f };

            float[] origin_wpr_cr15 = { -0.596f, 29.023f, -2.64f };
            float[] default_wpr_cr15 = { 105.071f, -0.865f, 89.288f };

            var cal_wpr_cr7 = core.GetToolFixtureWPR(SceneName.Scene1B, Model.CR7, origin_wpr_cr7, default_wpr_cr7);
            var cal_wpr_cr15 = core.GetToolFixtureWPR(SceneName.Scene1B, Model.CR15, origin_wpr_cr15, default_wpr_cr15);

            float[] cal_pin_tcp_cr7 = { -61.97f, 1.016f, 193.006f, cal_wpr_cr7[0], cal_wpr_cr7[1], cal_wpr_cr7[2] };
            float[] cal_pin_tcp_cr15 = { -1.946f, -35.828f, 174.092f, cal_wpr_cr15[0], cal_wpr_cr15[1], cal_wpr_cr15[2] };
            float cal_pin_length_cr7 = 45.23f + 5.02f;
            float cal_pin_length_cr15 = 45.33f + 5.02f;
            var fixture_tcp_cr7 = core.GetToolFixtureTCP(Model.CR7, cal_pin_tcp_cr7, cal_pin_length_cr7);
            var fixture_tcp_cr15 = core.GetToolFixtureTCP(Model.CR15, cal_pin_tcp_cr15, cal_pin_length_cr15);

            // offset-1: cr7: 6.27f;  cr15:6.24f;
            // offset-2: cr7: 16.05f; cr15: 16.03f;
            // offset-3: cr7: 71.53f; cr15: 16.02f;
            // offset-4: cr7: 16.05f; cr15: 16.02f;
            // offset-5: cr7: 71.53f; cr15: 105.13f;
            // offset-6: cr7: 16.05f; cr15: 105.13f;
            // offset-7: cr7: 45.14f; cr15: 105.13f;
            float[] antenna_offset_cr7 = { 0.0f, 0.0f, 45.14f, 0.0f, 0.0f, 0.0f };
            float[] antenna_offset_cr15 = { 0.0f, 0.0f, 105.13f, 0.0f, 0.0f, 0.0f };
            var tcp_cr7 = core.GetToolAntennaTCP(Model.CR7, fixture_tcp_cr7, antenna_offset_cr7);
            var tcp_cr15 = core.GetToolAntennaTCP(Model.CR15, fixture_tcp_cr15, antenna_offset_cr15);
            Console.WriteLine("tcp_cr7: " + tcp_cr7);
            Console.WriteLine("tcp_cr15: " + tcp_cr15);

            // (4) Robot Initialization
            // examples: tcp data
            //float[] tcp_cr7 = { -61.7107f, 0, 193.7107f, 0, -45, 0 };
            //float[] tcp_cr15 = { 0, 0, 140, 0, 45, -90 };
            core.SetTCP(Model.CR15, tcp_cr15);
            core.SetTCP(Model.CR7, tcp_cr7);
            core.SetSpeed(Model.CR15, 100);
            core.SetSpeed(Model.CR7, 100);

            // c. examples.
            //double[] param = { 200, 350, 350, 4, 4, 0 };
            double[] param = { 100, 350, 350, 4, 4, 0 };
            core.SceneParamInit(SceneName.Scene1C, param);
            // d.
            core.SceneRobotInit(SceneName.Scene1C);
            // e.
            core.SetUserFrame(Model.CR15);
            core.SetUserFrame(Model.CR7);

            // step5: Scene1C
            // Thread th_scene1c = new Thread(() => core.Scene1C(MovementType.QuickCheck));
            // th_scene1c.Start();

            // Thread th1 = new Thread(() => core.thread_MoveFlag(Model.CR15));
            // th1.Start();

            core.Scene1C(MovementType.QuickCheck);
        }
        public void DualRobot_Scene2_CityU_Sim()
        {
            // (1) Connection
            DualRobotLib.Core core = new Core();
            core.Connect(Model.CR15, "127.0.0.1", 9021);
            core.Connect(Model.CR7, "127.0.0.1", 60008);
            // core.Connect(Model.CR15, "192.168.0.125", 60008);
            // core.Connect(Model.CR7, "192.168.0.124", 60008);

            // (2) Get Calibrated Co-Frame Data
            // % cal-2: current error: 0.2mm
            double[] Pos_Cr7_CalliBase = { 778.281, -38.520, -336.517, 0.810, 0.687, -92.389 };
            double[] Pos_Cr15_CalliBase = { 1252.171, 24.657, -770.478, 0.269, 0.423, 89.225 };
            core.RobotBaseCalibrationInit(Pos_Cr7_CalliBase, Pos_Cr15_CalliBase);

            // (3) Get Tool Antenna TCP Data 
            float[] origin_wpr_cr7 = { -179.345f, 49.731f, 179.724f };
            float[] default_wpr_cr7 = { 50.913f, -0.687f, 87.608f };

            float[] origin_wpr_cr15 = { 1.09f, 50.908f, -1.546f };
            float[] default_wpr_cr15 = { -130.267f, -0.423f, -90.775f };

            var cal_wpr_cr7 = core.GetToolFixtureWPR(SceneName.Scene2_Sim, Model.CR7, origin_wpr_cr7, default_wpr_cr7);
            var cal_wpr_cr15 = core.GetToolFixtureWPR(SceneName.Scene2_Sim, Model.CR15, origin_wpr_cr15, default_wpr_cr15);

            float[] cal_pin_tcp_cr7 = { -122.4f, 187.9f, 135.0f, cal_wpr_cr7[0], cal_wpr_cr7[1], cal_wpr_cr7[2] };
            float[] cal_pin_tcp_cr15 = { 0.0f, -11.8f, 355.0f, cal_wpr_cr15[0], cal_wpr_cr15[1], cal_wpr_cr15[2] };
            float cal_pin_length_cr7 = 50.0f;
            float cal_pin_length_cr15 = 105.0f;
            var fixture_tcp_cr7 = core.GetToolFixtureTCP(Model.CR7, cal_pin_tcp_cr7, cal_pin_length_cr7);
            var fixture_tcp_cr15 = core.GetToolFixtureTCP(Model.CR15, cal_pin_tcp_cr15, cal_pin_length_cr15);

            // offset-1: cr7: 6.27f;  cr15:6.24f;
            // offset-2: cr7: 16.05f; cr15: 16.03f;
            float[] antenna_offset_cr7 = { 0.0f, 0.0f, 50.0f, 0.0f, 0.0f, 0.0f };
            float[] antenna_offset_cr15 = { 0.0f, 0.0f, 105.0f, 0.0f, 0.0f, 0.0f };
            var tcp_cr7 = core.GetToolAntennaTCP(Model.CR7, fixture_tcp_cr7, antenna_offset_cr7);
            var tcp_cr15 = core.GetToolAntennaTCP(Model.CR15, fixture_tcp_cr15, antenna_offset_cr15);
            Console.WriteLine("tcp_cr7: " + tcp_cr7);
            Console.WriteLine("tcp_cr15: " + tcp_cr15);

            // (4) Robot Initialization
            #region examples: tcp data

            // float[] tcp_cr7 = { -125, 180, 135, 0, 0, -90 };
            // float[] tcp_cr15 = { 0, 0, 355, 0, 0, 90 };

            // config1: cr7: -125, 180, 85, 90, 0, -180
            // config1: cr15: 0, 0, 300, 0, 0, -90 // x-105

            // with antenna
            // config2: cr7: -125, 180, 135, 90, 0, -180
            // config2: cr15: 0, 0, 325, 0, 0, -90 // x-80

            // config3: cr7: -125, 180, 135, 90, 0, -180
            // config3: cr15: 0, 0, 355, 0, 0, -90 //x -50

            // test-1: cr15: 0, -110, 185, 0, 45, -90 // xxx
            // test-2: cr15: 0, -140, 185, 0, 0, -90 // xxx
            // test-3: cr15: 0, -30, 170, 0, -30, -90 // xxx

            // final version:
            // cr7: -125, 180, 135, 90, 0, -180
            // cr15: 0, 0, 355, 0, 0, -90

            #endregion

            core.SetTCP(Model.CR15, tcp_cr15);
            core.SetTCP(Model.CR7, tcp_cr7);
            core.SetSpeed(Model.CR15, 100);
            core.SetSpeed(Model.CR7, 100);

            // (5) Set Station Antenna TCP (Cr7) (Cal. tool + UF: 0)
            float[] station_cal_pin_tcp_cr7 = { 834.368f, -101.383f, -246.147f, 0.810f, 0.687f, -92.389f }; // switch to tool:5 user frame:0
            float station_cal_pin_length = 110.2f;
            var station_center_zero_tcp = core.GetStationCenterZeroTCP(station_cal_pin_tcp_cr7, station_cal_pin_length);

            float[] antenna_offset_station = { 0.0f, 0.0f, 110.2f, 0.0f, 0.0f, 0.0f };
            float station_offset = 500; // 0-290

            var station_antenna_tcp_cr7 = core.GetStationAntennaTCP(station_center_zero_tcp, antenna_offset_station, station_offset);
            Console.WriteLine("station_antenna_tcp_cr7: " + station_antenna_tcp_cr7);

            core.SetStationAntennaTCP_Cr7(station_antenna_tcp_cr7);

            // (6) Scene Initialization
            // a. examples.
            // double[] param = { 250, 90, 10, 180, 30, 20, 45, 22, 290 };
            // double[] param = { 250, 90, 10, 180, 30, 200, 45, 22, 290 };
            double[] param = { 160, 90, 10, 180, 30, 50, 45, 22, 290 };
            core.SceneParamInit(SceneName.Scene2_Sim, param);
            // b.
            core.SceneRobotInit(SceneName.Scene2_Sim);
            // c.
            core.SetUserFrame(Model.CR15);
            core.SetUserFrame(Model.CR7);

            // (7) Execute Scene2_Sim
            core.Scene2_Sim(MovementType.QuickCheck);
        }

        //todo:
        public void DualRobot_Scene2_CityU()
        {
            /// (1) Connection
            DualRobotLib.Core core = new Core();
            core.Connect(Model.CR15, "127.0.0.1", 9021);
            core.Connect(Model.CR7, "127.0.0.1", 60008);
            // core.Connect(Model.CR15, "192.168.0.125", 60008);
            // core.Connect(Model.CR7, "192.168.0.124", 60008);

            // (2) Get Calibrated Co-Frame Data
            // % cal-2: current error: 0.2mm
            double[] Pos_Cr7_CalliBase = { 778.281, -38.520, -336.517, 0.810, 0.687, -92.389 };
            double[] Pos_Cr15_CalliBase = { 1252.171, 24.657, -770.478, 0.269, 0.423, 89.225 };
            core.RobotBaseCalibrationInit(Pos_Cr7_CalliBase, Pos_Cr15_CalliBase);

            #region (3) Get Tool Antenna TCP Data TODO: TCP Calculation

            // // (3) Get Tool Antenna TCP Data 
            //
            // float[] origin_wpr_cr7 = { 176.753f, 8.741f, 174.332f };
            // float[] default_wpr_cr7 = { -3.884f, 53.543f, -9.229f };
            //
            // float[] origin_wpr_cr15 = { -0.596f, 29.023f, -2.64f };
            // float[] default_wpr_cr15 = { 105.071f, -0.865f, 89.288f };
            //
            // var cal_wpr_cr7 = core.GetToolFixtureWPR(SceneName.Scene1B, Model.CR7, origin_wpr_cr7, default_wpr_cr7);
            // var cal_wpr_cr15 = core.GetToolFixtureWPR(SceneName.Scene1B, Model.CR15, origin_wpr_cr15, default_wpr_cr15);
            //
            // float[] cal_pin_tcp_cr7 = { -61.97f, 1.016f, 193.006f, cal_wpr_cr7[0], cal_wpr_cr7[1], cal_wpr_cr7[2] };
            // float[] cal_pin_tcp_cr15 = { -1.946f, -35.828f, 174.092f, cal_wpr_cr15[0], cal_wpr_cr15[1], cal_wpr_cr15[2] };
            // float cal_pin_length_cr7 = 45.23f + 5.02f;
            // float cal_pin_length_cr15 = 45.33f + 5.02f;
            // var fixture_tcp_cr7 = core.GetToolFixtureTCP(Model.CR7, cal_pin_tcp_cr7, cal_pin_length_cr7);
            // var fixture_tcp_cr15 = core.GetToolFixtureTCP(Model.CR15, cal_pin_tcp_cr15, cal_pin_length_cr15);
            //
            // // offset-1: cr7: 6.27f;  cr15:6.24f;
            // // offset-2: cr7: 16.05f; cr15: 16.03f;
            // float[] antenna_offset_cr7 = { 0.0f, 0.0f, 6.27f, 0.0f, 0.0f, 0.0f };
            // float[] antenna_offset_cr15 = { 0.0f, 0.0f, 6.24f, 0.0f, 0.0f, 0.0f };
            // var tcp_cr7 = core.GetToolAntennaTCP(Model.CR7, fixture_tcp_cr7, antenna_offset_cr7);
            // var tcp_cr15 = core.GetToolAntennaTCP(Model.CR15, fixture_tcp_cr15, antenna_offset_cr15);
            // Console.WriteLine("tcp_cr7: " + tcp_cr7);
            // Console.WriteLine("tcp_cr15: " + tcp_cr15);

            #endregion

            // (4) Robot Initialization
            // examples: tcp data
            float[] tcp_cr7 = { -125, 180, 85, 90, 0, -180 };
            float[] tcp_cr15 = { 0, 0, 300, 0, 0, -90 };

            core.SetTCP(Model.CR15, tcp_cr15);
            core.SetTCP(Model.CR7, tcp_cr7);
            core.SetSpeed(Model.CR15, 100);
            core.SetSpeed(Model.CR7, 100);

            // (5) Set Station Antenna TCP (Cr7) (Cal. tool + UF: 0)
            float[] station_cal_pin_tcp_cr7 = { 834.368f, -101.383f, -246.147f, 0.810f, 0.687f, -92.389f }; // switch to tool:5 user frame:0
            float station_cal_pin_length = 110.2f;
            var station_center_zero_tcp = core.GetStationCenterZeroTCP(station_cal_pin_tcp_cr7, station_cal_pin_length);

            float[] antenna_offset_station = { 0.0f, 0.0f, 110.2f, 0.0f, 0.0f, 0.0f };
            float station_offset = 350; // 0-300 

            var station_antenna_tcp_cr7 = core.GetStationAntennaTCP(station_center_zero_tcp, antenna_offset_station, station_offset);
            Console.WriteLine("station_antenna_tcp_cr7: " + station_antenna_tcp_cr7);

            core.SetStationAntennaTCP_Cr7(station_antenna_tcp_cr7);

            // (6) Scene Initialization
            // a. examples.
            double[] param = { 20, 100, 10, 180, 30, 200, 90, 22, 290 };
            core.SceneParamInit(SceneName.Scene2, param);
            // b.
            core.SceneRobotInit(SceneName.Scene2);
            // c.
            core.SetUserFrame(Model.CR15);
            core.SetUserFrame(Model.CR7);

            // (7) Execute Scene2
            core.Scene2_Sim(MovementType.QuickCheck);
            // todo: Modify Scene2()
        }

        // Motor
        public void Motor_Demo()
        {
            DualRobotLib.Core core = new Core();
            
            // 1. Connect
            core.Connect(Model.Motor, "COM3");

            // 2. Init(init params + move to zero)
            core.MotorInit();

            // 3. Absolute Movement
            core.MotorAbsMoveTo(30.5);

            // Optional Methods
            //
            // (1). Disconnect
            // core.Disconnect(Model.Motor);
            // (2). IsConnected
            // core.MotorIsConnected();
            // (3). IsMoving
            // var isMoving = core.MotorIsMoving();
            // (4). Get Current Degree
            // var curDegree = core.MotorGetDegree();
            // (5). Relative Move
            // core.MotorRelMoveTo(14.5);
        }

        // LiftTable
        public void LiftTable_Demo()
        {
            DualRobotLib.Core core = new Core();

            // 1. Connect
            core.Connect(Model.LiftTable, "192.168.0.119", 50000, "COM4");

            // 2. Init
            core.LiftTableInit();

            // 3. Absolute Move
            core.LiftTableAbsMoveTo(100);

            // Optional Methods
            //
            // (1). Disconnect
            // core.Disconnect(Model.LiftTable);
            // (2). IsConnected
            // core.LiftTableIsConnected();
            // (3). IsMoving
            // core.LiftTableIsMoving();
            // (4). Get Current Height
            // core.LiftTableGetCurHeight();

            // todo: Relative Movement
            // core.LiftTableRelMoveTo();
        }

        // LED
        // core.LedInit();           // 
        // core.LedOff();            //
        // core.LedOn(Color.Red);    //

        public void threadDemo()
        {
            // public delegate void SumOfNumbersCallback(int SumOfNumbers);
            //
            // class Number
            // {
            //     private int _target;
            //     SumOfNumbersCallback _callbackMethod;
            //
            //     public Number(int target, SumOfNumbersCallback callbackMethod)
            //     {
            //         this._target = target;
            //         _callbackMethod = callbackMethod;
            //     }
            //
                  // do something and return value.
            //     public void PrintSumOfNumbers()
            //     {
            //         int sum = 0;
            //         for (int i = 1; i <= _target; i++)
            //         {
            //             sum += i;
            //         }
            //
            //         if (_callbackMethod != null)
            //             _callbackMethod(sum);
            //     }
            // }
            // 
               // thread 1

            // public static void PrintSum(int sum)
            // {
            //     Console.WriteLine("Sum of numbers = " + sum);
            // }
            //

               // thread 1

            // public MainWindow()
            // {
            //     int target = 5;
            //
            //     SumOfNumbersCallback callback = new SumOfNumbersCallback(PrintSum);
            //
            //     Number number = new Number(target, callback);
            //
            //     Thread T1 = new Thread(new ThreadStart(number.PrintSumOfNumbers));
            //
            //     T1.Start();
            // }
        }

        // DevLog - 2022.08.29

        // (2) Get Calibrated Co-Frame Data
        // % Pos_Cr7_CalliBase 
        // % cal-0: 779.422, -37.794, -339.305, 0.351, 0.523, -92.267
        // % cal-1: 779.282, -38.284, -286.598, 0.588, 0.510, -92.060
        // % cal-2: 778.281, -38.520, -336.517, 0.810, 0.687, -92.389
        //
        // % Pos_Cr15_CalliBase 
        // % cal-0: 1249.821, 22.977, -774.474, 0.149, 0.109, 89.261 //error: 3-4mm
        // % cal-1: 1252.218, 23.278, -722.122, 0.082, 0.179, 89.359 //error: 2.5mm
        // % cal-2: 1252.171, 24.657, -770.478, 0.269, 0.423, 89.225 //error: 0.2mm

        // (3) Scene2 Related
        // % simulation tcp data
        // % tcp_cr7 - sim1: -125, 50, 45, 90, 0, 180
        // % tcp_cr7 - sim2: -125, 130, 55, 90, 0, -180 //v
        // % tcp_cr7 - sim3: -125, 180, 85, 90, 0, -180 //v
        // 
        // % tcp_cr15 - sim1: 0, 134, 182, 0, 0, 90
        // % tcp_cr15 - sim2: 0, 200, 182, 0, 0, 90
        // % tcp_cr15 - sim3: 0, 200, 248, 0, 0, 90
        // % tcp_cr15 - sim4: 0, 324, 110, 0, 0, 90
        // % tcp_cr15 - sim5: 0, 250, 220, 0, 0, 90 
        // % tcp_cr15 - sim6: 0, 0,   215, 0, 0, -90
        // % tcp_cr15 - sim7: 0, 0,   260, 0, 0, -90 // v
        // % tcp_cr15 - sim8: 0, 0,   300, 0, 0, -90 // v
        // 
        // 


    }
}
