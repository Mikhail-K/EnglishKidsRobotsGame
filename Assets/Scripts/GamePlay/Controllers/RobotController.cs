using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EnglishKids.RobotsConstructor
{
    public class RobotController : MonoBehaviour
    {
        [SerializeField]
        private Robot robotData;
        [SerializeField]
        private RobotView robotView;

        public Robot Robot { get { return robotData; } }
        public RobotView RobotView { get { return robotView; } }

        private void Start()
        {
            InitRobotParts();
        }

        private void InitRobotParts()
        {
            foreach (var part in RobotView.Parts)
            {
                var rpc = part.GetComponent<RobotPartController>();
                rpc.robotPart.ParentRobot = robotData;
                rpc.onMounted += OnPartMounted;
            }
        }

        public string GetRobotName()
        {
            return robotData.robotName;
        }

        public string GetRobotColorName()
        {
            return robotData.robotColor;
        }

        public List<GameObject> GetRobotParts()
        {
            return robotView.Parts;
        }

        public bool AllPartsMounted()
        {
            return robotView.Parts.All(x => x.GetComponent<RobotPartController>().CheckIfMounted());
        }

        public void DestroyRobot()
        {
            foreach (var part in RobotView.Parts)
                part.GetComponent<RobotPartController>().DestroyPart();
            Destroy(gameObject);
        }

        private void OnPartMounted(RobotPartController rpc)
        {
            if (AllPartsMounted())
            {
                robotView.SetActiveRobotAnimation(true);
                robotView.SetActiveMainBody(false);
            }

            rpc.onMounted -= OnPartMounted;
        }
    }
}
