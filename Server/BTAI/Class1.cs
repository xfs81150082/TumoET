﻿using System;

namespace BTAI
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// 这只是脚本系统
    /// 行为树会从Root节点开始遍历子节点。Update中执行
    /// 每个节点都有相关的操作，但是基本上就是返回三种状态
    /// ● Success​: 节点成功完成任务
    /// ● Failure​: 节点未通过任务
    /// ● Continue​:节点尚未完成任务。
    /// 但是每个节点的父节点对子节点的结果处理方式还不同。  例如
    /// ● Test 节点： 测试节点将调用其子节点并在测试为真时返回子节点状态，如果测试为假，则返回Failure而不调用其子节点。
    /// 行为树的一种构造方式如下：
    /// Root aiRoot = BT.Root(); 
    /// aiRoot.Do(  
    /// BT.If(TestVisibleTarget).Do(
    ///  BT.Call(Aim),
    ///  BT.Call(Shoot)
    ///  ),
    ///  BT.Sequence().Do(
    ///  BT.Call(Walk),
    ///  BT.Wait(5.0f),
    ///  BT.Call(Turn),
    ///  BT.Wait(1.0f),
    ///  BT.Call(Turn)
    ///  )
    /// ); 
    ///然后在Update中 调用   ​aiRoot.Tick()​ 。  刚刚构造的行为树是怎么样的检查过程呢？  
    ///1、首先检查TestVisibleTarget是否返回Ture，如果是继续执行子节点执行Aim函数和Shoot函数
    ///2、TestVisibleTarget是否返回false,if节点将返回Failure， 然后Root 将转向下一个子节点。这是个Sequence节点，它从执行第一个子节点开始。
    ///   1）将调用Walk函数，直接返回 Success，以便Sequence将下一个子节点激活并执行它。
    ///   2）执行Wait 节点，只是要等待5秒，还是第一次调用，所以肯定返回Running状态， 当Sequence从子节点上得到Running状态时，不会更改激活的子节点索引，下次Update的时候还是从这个节点开始执行
    ///3、Update的执行，当Wait节点等待的时间到了的时候，将会返回Success， 以便序列将转到下一个孩子。
    ///脚本中的Node列表 
    /// Sequence：
    //一个接一个地执行子节点。如果子节点返回：
    //●Success：Sequence将选择下一帧的下一个孩子开始。
    //●Failure：Sequence将返回到下一帧的第一个子节点（从头开始）。
    //●Continue：Sequence将在下一帧再次调用该节点。
    //RandomSequence：
    // 每次调用时，从子列表中执行一个随机子节点。您可以在构造函数中指定要应用于每个子项的权重列表作为int数组，以使某些子项更有可能被选中。
    //Selector ：
    //按顺序执行所有子项，直到一个返回Success，然后退出而不执行其余子节点。如果没有返回Success，则此节点将返回Failure。

    // Condition ：
    // 如果给定函数返回true，则此节点返回Success;如果为false，则返回Failure。
    // 与其他依赖于子节点结果的节点链接时很有用（例如，Sequence，Selector等）
    // If ：
    //调用给定的函数。
    // ●如果返回true，则调用当前活动的子级并返回其状态。
    // ●否则，它将在不调用其子项的情况下返回Failure
    // While：
    //只要给定函数返回true，就返回Continue（因此，下一帧将再次从该节点开始，而不会评估所有先前的节点）。
    //子节点们将陆续被执行。
    //当函数返回false并且循环中断时，将返回Failure。
    // Call 
    //调用给定的函数，它将始终返回Success。是动作节点！
    //Repeat 
    //将连续执行给定次数的所有子节点。
    //始终返回Continue，直到达到计数，并返回Success。
    //Wait
    //将返回Continue，直到达到给定时间（首次调用时开始），然后返回Success。
    //Trigger 
    //允许在给定的动画师animator中设置Trigger参数（如果最后一个参数设置为false，则取消设置触发器）。始终返回成功。
    //SetBool
    //允许在给定的animator中设置布尔参数的值。始终返回成功
    //SetActive 
    //设置给定GameObject的活动/非活动状态。始终返回成功。
    /// </summary>
    namespace BTAI
    {
        public enum BTState
        {
            Failure,
            Success,
            Continue,
            Abort
        }

        /// <summary>
        /// 节点 对象工厂
        /// </summary>
        public static class BT
        {
            public static Root Root() { return new Root(); }
            public static Sequence Sequence() { return new Sequence(); }
            public static Selector Selector(bool shuffle = false) { return new Selector(shuffle); }
            public static Action RunCoroutine(System.Func<IEnumerator<BTState>> coroutine) { return new Action(coroutine); }
            public static Action Call(System.Action fn) { return new Action(fn); }
            public static ConditionalBranch If(System.Func<bool> fn) { return new ConditionalBranch(fn); }
            public static While While(System.Func<bool> fn) { return new While(fn); }
            public static Condition Condition(System.Func<bool> fn) { return new Condition(fn); }
            public static Repeat Repeat(int count) { return new Repeat(count); }
            public static Wait Wait(float seconds) { return new Wait(seconds); }
            public static Trigger Trigger(Animator animator, string name, bool set = true) { return new Trigger(animator, name, set); }
            public static WaitForAnimatorState WaitForAnimatorState(Animator animator, string name, int layer = 0) { return new WaitForAnimatorState(animator, name, layer); }
            public static SetBool SetBool(Animator animator, string name, bool value) { return new SetBool(animator, name, value); }
            public static SetActive SetActive(GameObject gameObject, bool active) { return new SetActive(gameObject, active); }
            public static WaitForAnimatorSignal WaitForAnimatorSignal(Animator animator, string name, string state, int layer = 0) { return new WaitForAnimatorSignal(animator, name, state, layer); }
            public static Terminate Terminate() { return new Terminate(); }
            public static Log Log(string msg) { return new Log(msg); }
            public static RandomSequence RandomSequence(int[] weights = null) { return new BTAI.RandomSequence(weights); }

        }

        /// <summary>
        /// 节点抽象类
        /// </summary>
        public abstract class BTNode
        {
            public abstract BTState Tick();
        }

        /// <summary>
        /// 包含子节点的组合 节点基类
        /// </summary>
        public abstract class Branch : BTNode
        {
            protected int activeChild;
            protected List<BTNode> children = new List<BTNode>();
            public virtual Branch OpenBranch(params BTNode[] children)
            {
                for (var i = 0; i < children.Length; i++)
                    this.children.Add(children[i]);
                return this;
            }

            public List<BTNode> Children()
            {
                return children;
            }

            public int ActiveChild()
            {
                return activeChild;
            }

            public virtual void ResetChildren()
            {
                activeChild = 0;
                for (var i = 0; i < children.Count; i++)
                {
                    Branch b = children[i] as Branch;
                    if (b != null)
                    {
                        b.ResetChildren();
                    }
                }
            }
        }

        /// <summary>
        /// 装饰节点    只包含一个子节点，用于某种方式改变这个节点的行为
        /// 比如过滤器（用于决定是否允许子节点运行的，如：Until Success, Until Fail等），这种节点的子节点应该是条件节点，条件节点一直检测“视线中是否有敌人”，知道发现敌人为止。
        /// 或者 Limit 节点，用于指定某个子节点的最大运行次数
        /// 或者 Timer节点，设置了一个计时器，不会立即执行子节点，而是等一段时间，时间到了开始执行子节点
        /// 或者 TimerLimit节点，用于指定某个子节点的最长运行时间。
        /// 或者 用于产生某个返回状态，
        /// </summary>
        public abstract class Decorator : BTNode
        {
            protected BTNode child;
            public Decorator Do(BTNode child)
            {
                this.child = child;
                return this;
            }
        }

        /// <summary>
        /// 顺序节点 （从左到右依次执行所有子节点，只要子节点返回Success就继续执行后续子节点，直到遇到Failure或者Runing， 
        /// 停止后续执行，并把这个节点返回给父节点，只有它的所有子节点都是Success他才会向父节点返回Success）
        /// </summary>
        public class Sequence : Branch
        {
            public override BTState Tick()
            {
                var childState = children[activeChild].Tick();
                switch (childState)
                {
                    case BTState.Success:
                        activeChild++;
                        if (activeChild == children.Count)
                        {
                            activeChild = 0;
                            return BTState.Success;
                        }
                        else
                            return BTState.Continue;
                    case BTState.Failure:
                        activeChild = 0;
                        return BTState.Failure;
                    case BTState.Continue:
                        return BTState.Continue;
                    case BTState.Abort:
                        activeChild = 0;
                        return BTState.Abort;
                }
                throw new System.Exception("This should never happen, but clearly it has.");
            }
        }

        /// <summary>
        /// 选择节点从左到右依次执行所有子节点 ，只要遇到failure就继续执行后续子节点，直到遇到一个节点返回Success或Running为止。向父节点返回Success或Running
        /// 所有子节点都是Fail， 那么向父节点凡湖Fail
        /// 选择节点 用来在可能的行为集合中选择第一个成功的。 比如一个试图躲避枪击的AI角色， 它可以通过寻找隐蔽点， 或离开危险区域， 或寻找援助等多种方式实现目标。
        /// 利用选择节点，他会尝试寻找Cover，失败后在试图逃离危险区域。
        /// </summary>
        public class Selector : Branch
        {
            public Selector(bool shuffle)
            {
                if (shuffle)
                {
                    var n = children.Count;
                    while (n > 1)
                    {
                        n--;
                        var k = Mathf.FloorToInt(Random.value * (n + 1));
                        var value = children[k];
                        children[k] = children[n];
                        children[n] = value;
                    }
                }
            }

            public override BTState Tick()
            {
                var childState = children[activeChild].Tick();
                switch (childState)
                {
                    case BTState.Success:
                        activeChild = 0;
                        return BTState.Success;
                    case BTState.Failure:
                        activeChild++;
                        if (activeChild == children.Count)
                        {
                            activeChild = 0;
                            return BTState.Failure;
                        }
                        else
                            return BTState.Continue;
                    case BTState.Continue:
                        return BTState.Continue;
                    case BTState.Abort:
                        activeChild = 0;
                        return BTState.Abort;
                }
                throw new System.Exception("This should never happen, but clearly it has.");
            }
        }

        /// <summary>
        /// 行为节点  调用方法，或运行协程。完成实际工作， 例如播放动画，让角色移动位置，感知敌人，更换武器，播放声音，增加生命值等。
        /// </summary>
        public class Action : BTNode
        {
            System.Action fn;
            System.Func<IEnumerator<BTState>> coroutineFactory;
            IEnumerator<BTState> coroutine;
            public Action(System.Action fn)
            {
                this.fn = fn;
            }
            public Action(System.Func<IEnumerator<BTState>> coroutineFactory)
            {
                this.coroutineFactory = coroutineFactory;
            }
            public override BTState Tick()
            {
                if (fn != null)
                {
                    fn();
                    return BTState.Success;
                }
                else
                {
                    if (coroutine == null)
                        coroutine = coroutineFactory();
                    if (!coroutine.MoveNext())
                    {
                        coroutine = null;
                        return BTState.Success;
                    }
                    var result = coroutine.Current;
                    if (result == BTState.Continue)
                        return BTState.Continue;
                    else
                    {
                        coroutine = null;
                        return result;
                    }
                }
            }

            public override string ToString()
            {
                return "Action : " + fn.Method.ToString();
            }
        }

        /// <summary>
        /// 条件节点   调用方法，如果方法返回true则返回成功，否则返回失败。
        /// 用来测试当前是否满足某些性质或条件，例如“玩家是否在20米之内？”“是否能看到玩家？”“生命值是否大于50？”“弹药是否足够？”等
        /// </summary>
        public class Condition : BTNode
        {
            public System.Func<bool> fn;

            public Condition(System.Func<bool> fn)
            {
                this.fn = fn;
            }
            public override BTState Tick()
            {
                return fn() ? BTState.Success : BTState.Failure;
            }

            public override string ToString()
            {
                return "Condition : " + fn.Method.ToString();
            }
        }

        /// <summary>
        /// 当方法为True的时候 尝试执行当前  子节点
        /// </summary>
        public class ConditionalBranch : Block
        {
            public System.Func<bool> fn;
            bool tested = false;
            public ConditionalBranch(System.Func<bool> fn)
            {
                this.fn = fn;
            }
            public override BTState Tick()
            {
                if (!tested)
                {
                    tested = fn();
                }
                if (tested)
                {
                    // 当前子节点执行完就进入下一个节点(超上限就返回到第一个)
                    var result = base.Tick();
                    // 没执行完
                    if (result == BTState.Continue)
                        return BTState.Continue;
                    else
                    {
                        tested = false;
                        // 最后一个子节点执行完，才会为Ture
                        return result;
                    }
                }
                else
                {
                    return BTState.Failure;
                }
            }

            public override string ToString()
            {
                return "ConditionalBranch : " + fn.Method.ToString();
            }
        }

        /// <summary>
        /// While节点   只要方法  返回True 就执行所有子节点， 否则返回 Failure
        /// </summary>
        public class While : Block
        {
            public System.Func<bool> fn;

            public While(System.Func<bool> fn)
            {
                this.fn = fn;
            }

            public override BTState Tick()
            {
                if (fn())
                    base.Tick();
                else
                {
                    //if we exit the loop
                    ResetChildren();
                    return BTState.Failure;
                }

                return BTState.Continue;
            }

            public override string ToString()
            {
                return "While : " + fn.Method.ToString();
            }
        }

        /// <summary>
        /// 阻塞节点  如果当前子节点是Continue 说明没有执行完，阻塞着，执行完之后在继续它后面的兄弟节点 不管成功失败。
        /// 如果当前结点是最后一个节点并执行完毕，说明成功！否则就是处于Continue状态。 
        /// 几个基本上是抽象节点， 像是让所有子节点都执行一遍， 当前子节点执行完就进入下一个节点(超上限就返回到第一个)
        /// </summary>
        public abstract class Block : Branch
        {
            public override BTState Tick()
            {
                switch (children[activeChild].Tick())
                {
                    case BTState.Continue:
                        return BTState.Continue;
                    default:
                        activeChild++;
                        if (activeChild == children.Count)
                        {
                            activeChild = 0;
                            return BTState.Success;
                        }
                        return BTState.Continue;
                }
            }
        }

        public class Root : Block
        {
            public bool isTerminated = false;

            public override BTState Tick()
            {
                if (isTerminated) return BTState.Abort;
                while (true)
                {
                    switch (children[activeChild].Tick())
                    {
                        case BTState.Continue:
                            return BTState.Continue;
                        case BTState.Abort:
                            isTerminated = true;
                            return BTState.Abort;
                        default:
                            activeChild++;
                            if (activeChild == children.Count)
                            {
                                activeChild = 0;
                                return BTState.Success;
                            }
                            continue;
                    }
                }
            }
        }

        /// <summary>
        /// 多次运行子节点（一个子节点执行一次就算一次）
        /// </summary>
        public class Repeat : Block
        {
            public int count = 1;
            int currentCount = 0;
            public Repeat(int count)
            {
                this.count = count;
            }
            public override BTState Tick()
            {
                if (count > 0 && currentCount < count)
                {
                    var result = base.Tick();
                    switch (result)
                    {
                        case BTState.Continue:
                            return BTState.Continue;
                        default:
                            currentCount++;
                            if (currentCount == count)
                            {
                                currentCount = 0;
                                return BTState.Success;
                            }
                            return BTState.Continue;
                    }
                }
                return BTState.Success;
            }

            public override string ToString()
            {
                return "Repeat Until : " + currentCount + " / " + count;
            }
        }


        /// <summary>
        /// 随机的顺序  执行子节点 
        /// </summary>
        public class RandomSequence : Block
        {
            int[] m_Weight = null;
            int[] m_AddedWeight = null;

            /// <summary>
            /// 每次再次触发时，将选择一个随机子节点
            /// </summary>
            /// <param name="weight">保留null，以便所有子节点具有相同的权重。
            /// 如果权重低于子节点， 则后续子节点的权重都为1</param>
            public RandomSequence(int[] weight = null)
            {
                activeChild = -1;

                m_Weight = weight;
            }

            public override Branch OpenBranch(params BTNode[] children)
            {
                m_AddedWeight = new int[children.Length];

                for (int i = 0; i < children.Length; ++i)
                {
                    int weight = 0;
                    int previousWeight = 0;

                    if (m_Weight == null || m_Weight.Length <= i)
                    {//如果没有那个权重， 就将权重 设置为1
                        weight = 1;
                    }
                    else
                        weight = m_Weight[i];

                    if (i > 0)
                        previousWeight = m_AddedWeight[i - 1];

                    m_AddedWeight[i] = weight + previousWeight;
                }

                return base.OpenBranch(children);
            }

            public override BTState Tick()
            {
                if (activeChild == -1)
                    PickNewChild();

                var result = children[activeChild].Tick();

                switch (result)
                {
                    case BTState.Continue:
                        return BTState.Continue;
                    default:
                        PickNewChild();
                        return result;
                }
            }

            void PickNewChild()
            {
                int choice = Random.Range(0, m_AddedWeight[m_AddedWeight.Length - 1]);

                for (int i = 0; i < m_AddedWeight.Length; ++i)
                {
                    if (choice - m_AddedWeight[i] <= 0)
                    {
                        activeChild = i;
                        break;
                    }
                }
            }

            public override string ToString()
            {
                return "Random Sequence : " + activeChild + "/" + children.Count;
            }
        }


        /// <summary>
        /// 暂停执行几秒钟。
        /// </summary>
        public class Wait : BTNode
        {
            public float seconds = 0;
            float future = -1;
            public Wait(float seconds)
            {
                this.seconds = seconds;
            }

            public override BTState Tick()
            {
                if (future < 0)
                    future = Time.time + seconds;

                if (Time.time >= future)
                {
                    future = -1;
                    return BTState.Success;
                }
                else
                    return BTState.Continue;
            }

            public override string ToString()
            {
                return "Wait : " + (future - Time.time) + " / " + seconds;
            }
        }

        /// <summary>
        /// 设置动画  trigger 参数
        /// </summary>
        public class Trigger : BTNode
        {
            Animator animator;
            int id;
            string triggerName;
            bool set = true;

            //如果 set == false, 则重置trigger而不是设置它。
            public Trigger(Animator animator, string name, bool set = true)
            {
                this.id = Animator.StringToHash(name);
                this.animator = animator;
                this.triggerName = name;
                this.set = set;
            }

            public override BTState Tick()
            {
                if (set)
                    animator.SetTrigger(id);
                else
                    animator.ResetTrigger(id);

                return BTState.Success;
            }

            public override string ToString()
            {
                return "Trigger : " + triggerName;
            }
        }

        /// <summary>
        /// 设置动画 boolean 参数
        /// </summary>
        public class SetBool : BTNode
        {
            Animator animator;
            int id;
            bool value;
            string triggerName;

            public SetBool(Animator animator, string name, bool value)
            {
                this.id = Animator.StringToHash(name);
                this.animator = animator;
                this.value = value;
                this.triggerName = name;
            }

            public override BTState Tick()
            {
                animator.SetBool(id, value);
                return BTState.Success;
            }

            public override string ToString()
            {
                return "SetBool : " + triggerName + " = " + value.ToString();
            }
        }

        /// <summary>
        /// 等待animator达到一个状态。
        /// </summary>
        public class WaitForAnimatorState : BTNode
        {
            Animator animator;
            int id;
            int layer;
            string stateName;

            public WaitForAnimatorState(Animator animator, string name, int layer = 0)
            {
                this.id = Animator.StringToHash(name);
                if (!animator.HasState(layer, this.id))
                {
                    Debug.LogError("The animator does not have state: " + name);
                }
                this.animator = animator;
                this.layer = layer;
                this.stateName = name;
            }

            public override BTState Tick()
            {
                var state = animator.GetCurrentAnimatorStateInfo(layer);
                if (state.fullPathHash == this.id || state.shortNameHash == this.id)
                    return BTState.Success;
                return BTState.Continue;
            }

            public override string ToString()
            {
                return "Wait For State : " + stateName;
            }
        }

        /// <summary>
        /// 设置 GameObject 的激活状态
        /// </summary>
        public class SetActive : BTNode
        {

            GameObject gameObject;
            bool active;

            public SetActive(GameObject gameObject, bool active)
            {
                this.gameObject = gameObject;
                this.active = active;
            }

            public override BTState Tick()
            {
                gameObject.SetActive(this.active);
                return BTState.Success;
            }

            public override string ToString()
            {
                return "Set Active : " + gameObject.name + " = " + active;
            }
        }

        /// <summary>
        /// 等待animator从SendSignal状态机行为 接收信号。   SendSignal : StateMachineBehaviour
        /// </summary>
        public class WaitForAnimatorSignal : BTNode
        {
            // 进入或退出动画都为 False， 只有执行中为True
            internal bool isSet = false;
            string name;
            int id;

            public WaitForAnimatorSignal(Animator animator, string name, string state, int layer = 0)
            {
                this.name = name;
                this.id = Animator.StringToHash(name);
                if (!animator.HasState(layer, this.id))
                {
                    Debug.LogError("The animator does not have state: " + name);
                }
                else
                {
                    SendSignal.Register(animator, name, this);
                }
            }

            public override BTState Tick()
            {
                if (!isSet)
                    return BTState.Continue;
                else
                {
                    isSet = false;
                    return BTState.Success;
                }

            }

            public override string ToString()
            {
                return "Wait For Animator Signal : " + name;
            }
        }

        /// <summary>
        /// 终止节点  切换到中止 状态
        /// </summary>
        public class Terminate : BTNode
        {

            public override BTState Tick()
            {
                return BTState.Abort;
            }

        }

        /// <summary>
        /// Log  输出Log 的节点
        /// </summary>
        public class Log : BTNode
        {
            string msg;

            public Log(string msg)
            {
                this.msg = msg;
            }

            public override BTState Tick()
            {
                Debug.Log(msg);
                return BTState.Success;
            }
        }

    }

#if UNITY_EDITOR
namespace BTAI
{
    public interface IBTDebugable
    {
        Root GetAIRoot();
    }
}
#endif




}
