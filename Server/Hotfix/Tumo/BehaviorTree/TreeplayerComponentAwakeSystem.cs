using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    [ObjectSystem]
    public class TreePlayerComponentAwakeSystem : AwakeSystem<PlayerBehaviorTreeComponent>
    {
        public override void Awake(PlayerBehaviorTreeComponent self)
        {
            TreePlayerInit(self);   /// AI 玩家智能 行为树模式
        }

        /// <summary>
        /// AI 玩家智能 行为树模式
        /// </summary>
        /// <param name="self"></param>
        void TreePlayerInit(PlayerBehaviorTreeComponent self)
        {
            self.root = BT.Root();

            self.root.OpenBranch(
                BT.Selector().OpenBranch(
                    BT.Sequence().OpenBranch(
                        BT.Call(self.GetParent<Unit>().GetComponent<AttackComponent>().CheckDeath),
                        BT.Send(self.GetParent<Unit>().GetComponent<AttackComponent>().GetExpAndCoin),
                        BT.Send(self.GetParent<Unit>().GetComponent<AttackComponent>().RemoveUnit)
                        ),
                    BT.Selector().OpenBranch(
                        BT.Sequence().OpenBranch(
                            BT.Call(self.GetParent<Unit>().GetComponent<AttackComponent>().CheckIsBattling),
                            BT.Send(self.GetParent<Unit>().GetComponent<RecoverComponent>().RecoverHp)
                            ),
                        BT.Sequence().OpenBranch(
                            BT.Call(self.GetParent<Unit>().GetComponent<AttackComponent>().PlayerCheckAttackTarget),                            
                            BT.Send(self.GetParent<Unit>().GetComponent<AttackComponent>().AttackTarget)
                            )
                        )
                     )
                  );
        }


    }
}
