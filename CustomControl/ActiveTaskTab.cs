using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;

using SequenceClicker.View.BasicSequencer.Component;

namespace SequenceClicker
{
    public class ActiveTaskTab<T> : BindingList<T> where T : ContentPresenter
    {
        public List<TaskTab> tabs = new List<TaskTab>();

        public void Initialize()
        {
            base.AllowEdit = true;
            base.AllowNew = true;
            base.AllowRemove = true;
        }

        public void Add(TaskTab taskT)
        {
            tabs.Add(taskT);
            base.Add((T)taskT.frame);
        }

        public void Remove(TaskTab taskT)
        {
            tabs.Remove(taskT);
            base.Remove((T)taskT.frame);
        }

        public void Add(List<TaskTab> taskTList)
        {
            base.RaiseListChangedEvents = false;

            foreach (TaskTab taskT in taskTList)
                Add(taskT);

            base.RaiseListChangedEvents = true;
            base.ResetBindings();
        }

        public void Remove(List<TaskTab> taskTList)
        {
            base.RaiseListChangedEvents = false;

            foreach (TaskTab taskT in taskTList)
                Remove(taskT);

            base.RaiseListChangedEvents = true;
            base.ResetBindings();
        }

        public void SwitchItem(int orgin, int destination)
        {
            if (orgin < 0 || orgin >= tabs.Count)
                return;

            if (destination < 0 || destination >= tabs.Count)
                return;

            TaskTab tab = tabs[orgin];

            base[orgin] = base[destination];
            base[destination] = (T)tab.frame;

            tabs[orgin] = tabs[destination];
            tabs[destination] = tab;
        }

        public void Insert(int index, TaskTab taskT)
        {
            tabs.Insert(index, taskT);
            base.Insert(index, (T)taskT.frame);
        }

        public int IndexOf(TaskTab tab)
        {
            return tabs.IndexOf(tab);
        }

        public new void Clear()
        {
            tabs.Clear();
            base.Clear();
        }


        [Obsolete]
        public new void Add(T item)
        {
        }

        [Obsolete]
        public new void Remove(T item)
        {
        }

        [Obsolete]
        public new void Insert(int index, T item)
        {
        }

        [Obsolete]
        public new void IndexOf(T item)
        {
        }
    }
}
