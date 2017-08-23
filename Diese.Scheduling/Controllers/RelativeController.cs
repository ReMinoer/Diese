﻿namespace Diese.Scheduling.Controllers
{
    public class RelativeController<TController, T> : IRelativeController<RelativeController<TController, T>, T>
    {
        private readonly SchedulerBase<TController, T> _scheduler;
        private readonly SchedulerGraph<T>.Vertex _vertex;

        public RelativeController(SchedulerBase<TController, T> scheduler, SchedulerGraph<T>.Vertex vertex)
        {
            _scheduler = scheduler;
            _vertex = vertex;
        }

        public RelativeController<TController, T> Before(T dependent)
        {
            if (!_scheduler.ItemsVertex.TryGetValue(dependent, out SchedulerGraph<T>.Vertex otherVertex))
                otherVertex = _scheduler.AddItemVertex(dependent);

            if (!_scheduler.SchedulerGraph.ContainsEdge(otherVertex, _vertex))
                _scheduler.SchedulerGraph.AddEdge(otherVertex, _vertex, new SchedulerGraph<T>.Edge());

            _scheduler.Refresh();
            return this;
        }

        public RelativeController<TController, T> After(T dependency)
        {
            if (!_scheduler.ItemsVertex.TryGetValue(dependency, out SchedulerGraph<T>.Vertex otherVertex))
                otherVertex = _scheduler.AddItemVertex(dependency);

            if (!_scheduler.SchedulerGraph.ContainsEdge(_vertex, otherVertex))
                _scheduler.SchedulerGraph.AddEdge(_vertex, otherVertex, new SchedulerGraph<T>.Edge());

            _scheduler.Refresh();
            return this;
        }

        void IRelativeController<T>.After(T item)
        {
            After(item);
        }

        void IRelativeController<T>.Before(T item)
        {
            Before(item);
        }
    }
}