using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Engine
{
    public class ThreadHelper
    {
        private static List<Action> sm_actions = new List<Action>();
        static List<Action> sm_acts = new List<Action>();
        public static void RunOnMainThread(Action action)
        {
            lock (sm_actions)
            {
                sm_actions.Add(action);
            }
        }
        public static void Update()
        {
           
            lock (sm_actions)
            {
                if (sm_actions.Count <= 0)
                {
                    return;
                }
                sm_acts.Clear();
                sm_acts.AddRange(sm_actions);
                sm_actions.Clear();
            }
            foreach (var action in sm_acts)
            {
                action();
            }
        }
    }
}
