﻿using System.Collections;
using System.IO;
using Core;
using Player;
using SaveLoad;
using UnityEngine;
using Weapons;

namespace Enemy.AI
{
    public class SniperEnemy : EnemyBase, ISaveable
    {
        protected override void Start()
        {
            base.Start();
            StartCoroutine(AI());
        }

        private IEnumerator AI()
        {
            // this will stop once the game object is destroyed so it's fine.
            while (true)
            {
                yield return new WaitUntil(() => PlayerController.Instance.Object.GameState != GameState.Paused);
                LookTowardsTarget();
                if (HasLineOfSight)
                {
                    Bullet.Spawn(gameObject, BulletTypes.Sniper, DirectionOfTarget, attackPoint.position);
                    yield return new WaitForSeconds(4f);
                }
                else
                {
                    yield return new WaitForSeconds(.1f);
                }
            }
        }

        public void Save(BinaryWriter binaryWriter)
        {
            ((Vector2) transform.position).SaveToBinary(binaryWriter);
        }

        public void Load(BinaryReader binaryReader)
        {
            var transform1 = transform;
            transform1.position = ((Vector2) transform1.position).LoadFromBinary(binaryReader);
        }
    }
}