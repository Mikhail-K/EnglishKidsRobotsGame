using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace EnglishKids.RobotsConstructor
{
    public class TransporterController : MonoBehaviour
    {
        public TransporterView transporterView;
        public TransporterData transporterData;

        public event Action allPartsMounted;
        public event Action currentPartsMounted;
        public event Action<RobotPartController> onePartMounted;

        public void PutRobotPartsOnLine(List<RobotController> robots)
        {
            List<GameObject> allParts = new List<GameObject>(robots[0].GetRobotParts());
            allParts.AddRange(robots[1].GetRobotParts());
            ShakeRobotsParts(allParts);

            foreach (var part in allParts)
            {
                RobotPartController rpc = part.GetComponent<RobotPartController>();
                rpc.onMounted += OnPartMounted;
                transporterData.AddRobotPart(rpc);
            }

            transporterData.SetCurrentRobots(robots[0], robots[1]);

            ShowRobotsPartsOnLine();
        }

        public void ShowRobotsPartsOnLine()
        {
            while (true)
            {
                RobotPartController rpc = transporterData.GetNextRobotPart();
                if (rpc == null)
                    break;
                if (!transporterView.AddPart(rpc.GetComponent<RectTransform>()))
                {
                    transporterData.AddRobotPart(rpc);
                    transporterData.RemoveFromCurrentParts(rpc);
                    break;
                }

                if (transporterView.GetAmountOfActiveParts() >= transporterData.MaxPartsOnLine)
                    break;
            }
        }

        public bool ContinueTransporter()
        {
            if (transporterData.HaveFreeParts())
            {
                transporterView.ResetTransporter();
                ShowRobotsPartsOnLine();
                LaunchTransporter();
                return true;
            }

            return false;
        }

        public void LaunchTransporter()
        {
            transporterView.ClearTransporterLine();
            transporterView.MoveTransporterLine();
        }

        public void TestClearLine()
        {
            transporterView.ClearTransporterLine();
        }

        public void ClearTransporter()
        {
            transporterView.ClearTransporterLine();
            transporterView.ResetTransporter();
            transporterData.ClearTransporterData();
        }

        private void OnPartMounted(RobotPartController rpc)
        {
            if (transporterData.CurrenPartsMounted())
            {
                transporterView.ClearTransporterLine();
                currentPartsMounted?.Invoke();
            }

            if (transporterData.AreAllMounted())
            {
                allPartsMounted?.Invoke();
                Debug.Log("Complete");
            }

            onePartMounted?.Invoke(rpc);
        }

        private void ShakeRobotsParts(List<GameObject> parts)
        {
            System.Random random = new System.Random();
            for (int i = parts.Count - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);
                var temp = parts[j];
                parts[j] = parts[i];
                parts[i] = temp;
            }
        }

    }
}
