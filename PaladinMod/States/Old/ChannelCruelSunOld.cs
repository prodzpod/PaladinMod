﻿using PaladinMod.Misc;
using UnityEngine;

namespace PaladinMod.States.Spell
{
    public class ChannelCruelSunOld : BaseChannelSpellState
    {
        private GameObject chargeEffect;
        private PaladinSwordController swordController;

        public override void OnEnter()
        {
            this.chargeEffectPrefab = null;
            this.chargeSoundString = Modules.Sounds.ChannelTorpor;
            this.maxSpellRadius = CastCruelSunOld.sunPrefabDiameter * 0.5f;
            this.baseDuration = StaticValues.cruelSunChannelDurationOld;
            this.swordController = base.gameObject.GetComponent<PaladinSwordController>();
            this.overrideAreaIndicatorMat = Modules.Assets.areaIndicatorMat;

            base.OnEnter();

            ChildLocator childLocator = base.GetModelChildLocator();
            if (childLocator)
            {
                this.chargeEffect = childLocator.FindChild("CruelSunChannelEffect").gameObject;
                this.chargeEffect.SetActive(false);
                this.chargeEffect.SetActive(true);
            }
        }

        protected override void PlayChannelAnimation()
        {
            base.PlayAnimation("Gesture, Override", "ChannelSun", "Spell.playbackRate", 2f);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (this.swordController) this.swordController.sunPosition = this.areaIndicatorInstance.transform.position;
        }

        public override void OnExit()
        {
            base.OnExit();

            if (this.chargeEffect)
            {
                this.chargeEffect.GetComponentInChildren<ParticleSystem>().Stop();
            }
        }

        protected override BaseCastChanneledSpellState GetNextState()
        {
            return new CastCruelSunOld();
        }
    }
}