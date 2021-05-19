using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EnglishKids.RobotsConstructor
{
    public class TransporterData : MonoBehaviour
    {
        public int maxPartsOnLine = 4;

        private Queue<RobotPartController> currentParts = new Queue<RobotPartController>();
        private List<RobotPartController> currentUsedParts = new List<RobotPartController>();

        private RobotController leftRobot;
        private RobotController rightRobot;

        public int MaxPartsOnLine { get { return maxPartsOnLine; } }

        public void SetCurrentRobots(RobotController leftRobot, RobotController rightRobot)
        {
            this.leftRobot = leftRobot;
            this.rightRobot = rightRobot;
        }

        public void AddRobotPart(RobotPartController part)
        {
            currentParts.Enqueue(part);
        }

        public void RemoveFromCurrentParts(RobotPartController part)
        {
            currentUsedParts.Remove(part);
        }

        public List<RobotPartController> SetTranporterLine()
        {
            List<RobotPartController> lineParts = new List<RobotPartController>();
            for (int i = 0; i < maxPartsOnLine; i++)
                lineParts.Add(GetNextRobotPart());

            return lineParts;
        }

        public RobotPartController GetNextRobotPart()
        {
            if (currentParts.Count > 0)
            {
                var rpc = currentParts.Dequeue();
                currentUsedParts.Add(rpc);
                return rpc;
            }
            return null;
        }

        public bool HaveFreeParts()
        {
            return currentParts.Count > 0;
        }

        public bool CurrenPartsMounted()
        {
            return currentUsedParts.All(x => x.CheckIfMounted());
            //return leftRobot.AllPartsMounted() && rightRobot.AllPartsMounted();
            //return currentParts.All(x => x.CheckIfMounted());
        }

        public bool AreAllMounted()
        {
            return leftRobot.AllPartsMounted() && rightRobot.AllPartsMounted();
            //return currentParts.All(x => x.CheckIfMounted());
        }

        public void ClearTransporterData()
        {
            currentParts.Clear();
            currentUsedParts.Clear();

            leftRobot.DestroyRobot();
            rightRobot.DestroyRobot();
        }
    }
}
