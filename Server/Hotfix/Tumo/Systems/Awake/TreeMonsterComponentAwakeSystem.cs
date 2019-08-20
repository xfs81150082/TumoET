using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    [ObjectSystem]
    public class TreeMonsterComponentAwakeSystem : AwakeSystem<TreeMonsterComponent>
    {
        public override void Awake(TreeMonsterComponent self)
        {
            TreeMonsterInit(self);    /// AI 小怪智能 行为树模式
        }

        /// <summary>
        /// AI 小怪智能 行为树模式
        /// </summary>
        /// <param name="self"></param>
        void TreeMonsterInit(TreeMonsterComponent self)
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
                            BT.Call(self.GetParent<Unit>().GetComponent<SeekComponent>().CheckSeekTarget),
                            BT.Selector().OpenBranch(
                                BT.Sequence().OpenBranch(
                                    BT.Call(self.GetParent<Unit>().GetComponent<AttackComponent>().MonsterCheckAttackTarget),
                                    BT.Send(self.GetParent<Unit>().GetComponent<AttackComponent>().AttackTarget)
                                    ),
                                BT.Send(self.GetParent<Unit>().GetComponent<SeekComponent>().SeekTarget)
                                )
                            ),
                        BT.Sequence().OpenBranch(
                            BT.Send(self.GetParent<Unit>().GetComponent<RecoverComponent>().RecoverHp),
                            BT.Send(self.GetParent<Unit>().GetComponent<PatrolComponent>().UpdatePatrol)
                            )
                        )
                     )
                  );
        }  
    

    }
}
