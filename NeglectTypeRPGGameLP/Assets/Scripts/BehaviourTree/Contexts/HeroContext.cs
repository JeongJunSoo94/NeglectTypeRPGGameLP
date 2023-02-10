using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NeglectTypeRPG
{
    public class HeroContext : Context, IComparable<HeroContext>
    {
        public HeroBase info;

        public GameObject statBar;

        public Slider hpBar;
        public Slider mpBar;

        public bool myTurn;
        public bool skillAvailable;

        public Vector3 originPos;
        public Vector3 targetPos;

        public Vector3 originRotation;

        public List<GameObject> targets;

        public Animator animator;
        public string currentAniState;

        public List<BehaviourTree> AttackBehavior;

        public int syncBehavior;

        private void OnDisable()
        {
            myTurn = false;
        }

        private void OnEnable()
        {
            if (statBar)
            { 
                statBar.SetActive(true);
                info.heroUI();
            }
        }

        public void SyncEvent(int count)
        {
            syncBehavior = count;
        }

        public void Initialized()
        {
            originPos = transform.position;
            info.Initialized();
        }

        public override void InitContext()
        {
            if (info == null)
            {
                info = new HeroBase();
                info.heroInfo = Instantiate(DataManager.Instance.heroInfo[2]);
                AttackBehavior.Add(DataManager.Instance.characterBehaviors[info.heroInfo.normalAttack.BehaviorTreeID - 1].Clone(gameObject.GetComponent<Blackboard>()));
                for (int i = 0; i < info.heroInfo.skills.Count; ++i)
                {
                    if (info.heroInfo.skills[i].BehaviorTreeID != 0)
                        AttackBehavior.Add(DataManager.Instance.characterBehaviors[info.heroInfo.skills[i].BehaviorTreeID - 1].Clone(gameObject.GetComponent<Blackboard>()));
                }
            }
            info.heroUI += new HeroUI(HealthBarUpdate);
            info.heroUI += new HeroUI(ManaBarUpdate);
            originPos = transform.position;
            info.Initialized();
        }

        public void HealthBarUpdate()
        {
            if(hpBar)
                hpBar.value = info.curHealth / info.maxHealth;
        }

        public void ManaBarUpdate()
        {
            if (mpBar)
                mpBar.value = info.curMana / info.maxMana;
        }

        public void UIAdd(GameObject UI)
        {
            statBar= UI;
            hpBar = UI.GetComponent<TargetFollow>().hpBar;
            mpBar = UI.GetComponent<TargetFollow>().mpBar;
            UI.GetComponent<TargetFollow>().target = gameObject;
            UI.GetComponent<TargetFollow>().gameObject.SetActive(true);
            info.heroUI();
        }

        public int CompareTo(HeroContext x)
        {
            if (info.curStat.Luck > x.info.curStat.Luck)
                return 1;
            else if (info.curStat.Luck == x.info.curStat.Luck)
                return 0;
            else
                return -1;
        }
    }
}

