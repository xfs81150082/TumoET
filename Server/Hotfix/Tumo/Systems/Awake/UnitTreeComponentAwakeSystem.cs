using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    [ObjectSystem]
    public class UnitTreeComponentAwakeSystem : AwakeSystem<UnitTreeComponent>
    {
        public override void Awake(UnitTreeComponent self)
        {
            self.root = BT.Root();

            switch (self.GetParent<Unit>().UnitType)
            {
                case UnitType.Monster:
                    MonsterUnitTreeInit(self);
                    break;
                case UnitType.Player:
                    PlayerUnitTreeInit(self);
                    break;
            }
        }

        /// <summary>
        /// AI 小怪智能 行为树模式
        /// </summary>
        /// <param name="self"></param>
        void MonsterUnitTreeInit(UnitTreeComponent self)
        {
            self.root.OpenBranch(
                BT.Selector().OpenBranch(
                    BT.Sequence().OpenBranch(
                        BT.Condition(self.CheckDeath),
                        BT.Call(self.RemoveUnit),
                        BT.Call(self.GetExpAndCoin)
                    ),
                    BT.Selector().OpenBranch(
                        BT.Sequence().OpenBranch(
                            BT.Condition(self.CheckBattling),
                            BT.Call(self.RecoverHp),
                            BT.Call(self.Patrol)
                            ),
                        BT.Sequence().OpenBranch(
                            BT.Call(self.CheckSeekTarget),
                            BT.Selector().OpenBranch(
                                BT.Sequence().OpenBranch(
                                    BT.Condition(self.CheckAttckTarget),
                                    BT.Call(self.AttackTarget)
                                    ),
                                BT.Call(self.SeekTarget)
                                )
                            )
                        )
                     )
                  );
        }

        void PlayerUnitTreeInit(UnitTreeComponent self)
        {
            self.root.OpenBranch(
                BT.Selector().OpenBranch(
                    BT.Sequence().OpenBranch(
                        BT.Condition(self.CheckDeath),
                        BT.Call(self.RemoveUnit),
                        BT.Call(self.GetExpAndCoin)
                        ),
                    BT.Selector().OpenBranch(
                        BT.Sequence().OpenBranch(
                            BT.Condition(self.CheckBattling),
                            BT.Call(self.RecoverHp)
                            ),
                        BT.Sequence().OpenBranch(
                            BT.Condition(self.CheckAttckTarget),
                            BT.Call(self.AttackTarget)
                            )
                        )
                     )
                  );
        }


    }
}
