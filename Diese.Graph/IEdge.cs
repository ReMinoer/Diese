﻿namespace Diese.Graph
{
    public interface IEdge<TGraph, out TVertex, TEdge, TVisitor>
        where TGraph : IGraph<TGraph, TVertex, TEdge, TVisitor>
        where TVertex : IVertex<TGraph, TVertex, TEdge, TVisitor>
        where TEdge : IEdge<TGraph, TVertex, TEdge, TVisitor>
        where TVisitor : IVisitor<TGraph, TVertex, TEdge, TVisitor>
    {
        TVertex Start { get; }
        TVertex End { get; }
    }
}