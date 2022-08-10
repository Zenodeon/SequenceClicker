using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;

using SequenceClicker.View.BasicSequencer.Component;

namespace SequenceClicker
{
    public class ActiveTaskTab<T> : BindingList<T> where T : ContentPresenter
    {
        int index = -1;
        private SortedList<int, TaskTab> pairs = new SortedList<int, TaskTab>();

        public void Initialize()
        {
            this.AllowEdit = true;
            this.AllowNew = true;
            this.AllowRemove = true;
        }

        public void Add(TaskTab taskT)
        {
            taskT.taskIndex = index++;

            pairs.Add(index, taskT);
            this.Insert(index, (T)taskT.frame);
        }

        public void Remove(TaskTab taskT)
        {
            pairs.Remove(taskT.taskIndex);
            base.Remove((T)taskT.frame);
        }

        public void Add(List<TaskTab> taskTList)
        {
            this.RaiseListChangedEvents = false;

            foreach (TaskTab taskT in taskTList)
                Add(taskT);

            this.RaiseListChangedEvents = true;
            this.ResetBindings();
        }

        public void Remove(List<TaskTab> taskTList)
        {
            this.RaiseListChangedEvents = false;

            foreach (TaskTab taskT in taskTList)
                Remove(taskT);

            this.RaiseListChangedEvents = true;
            this.ResetBindings();
        }

        public new void Clear()
        {
            pairs.Clear();
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
    }
}
