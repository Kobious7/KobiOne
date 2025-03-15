using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace InfiniteMap
{
    public class PotentialUI : GMono
    {
        [SerializeField] private List<PotentialStatUI> potentialStatUIs;
        [SerializeField] private TextMeshProUGUI remainPoint;
        private List<Stat> potential;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadPotentialStatUIs();
            LoadRemainPoint();
        }

        protected override void Start()
        {
            base.Start();
            StartCoroutine(WaitNextFrame());
            
        }

        private void LoadPotentialStatUIs()
        {
            if (potentialStatUIs.Count > 0) return;

            potentialStatUIs = GetComponentsInChildren<PotentialStatUI>().ToList();
        }

        private void LoadRemainPoint()
        {
            if (remainPoint != null) return;

            remainPoint = transform.Find("RemainPoints").Find("Point").GetComponent<TextMeshProUGUI>();
        }

        private IEnumerator WaitNextFrame()
        {
            yield return null;
            ShowPotentialPoint();
            Game.Instance.Player.Stats.OnPotentialChange += UpdatePotential;
        }

        private void ShowPotentialPoint()
        {
            potential = Game.Instance.Player.Stats.Potential;

            for (int i = 0; i < potentialStatUIs.Count; i++)
            {
                potentialStatUIs[i].ShowPoint(potential[i]);
                potentialStatUIs[i].AddClickListener(potential[i]);
            }

            remainPoint.text = $"{Game.Instance.Player.Stats.RemainPoints}";
        }

        private void UpdatePotential(int index)
        {
            potentialStatUIs[index].ShowPoint(potential[index]);
            remainPoint.text = $"{Game.Instance.Player.Stats.RemainPoints}";
        }
    }
}