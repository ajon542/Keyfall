using System;

// This seems like overkill but the reason it exists (for now):
// 1. We can use null in NodeCostChainOfResponsibilities if a node doesn't exist (otherwise we'd have to use -1 or something)
// 2. We can use an object rather than an "int", which gives it type safety
public class NodeCost : IEquatable<NodeCost>, IComparable<NodeCost>
{
    public readonly int Value;

    public NodeCost(int value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public override bool Equals(object other)
    {
        return Equals(other as NodeCost);
    }

    public bool Equals(NodeCost other)
    {
        if (ReferenceEquals(other, null))
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (GetType() != other.GetType())
            return false;

        return Value == other.Value;
    }

    public override int GetHashCode()
    {
        return Value;
    }

    public static bool operator ==(NodeCost lhs, NodeCost rhs)
    {
        if (ReferenceEquals(lhs, null))
        {
            if (ReferenceEquals(rhs, null))
                return true;
            return false;
        }

        return lhs.Equals(rhs);
    }

    public static bool operator !=(NodeCost lhs, NodeCost rhs)
    {
        return !(lhs == rhs);
    }

    public int CompareTo(NodeCost other)
    {
        if (other == null) return 1;

        return Value.CompareTo(other.Value);
    }

    public static bool operator >(NodeCost operand1, NodeCost operand2)
    {
        return operand1.CompareTo(operand2) == 1;
    }

    public static bool operator <(NodeCost operand1, NodeCost operand2)
    {
        return operand1.CompareTo(operand2) == -1;
    }

    public static bool operator >=(NodeCost operand1, NodeCost operand2)
    {
        return operand1.CompareTo(operand2) >= 0;
    }

    public static bool operator <=(NodeCost operand1, NodeCost operand2)
    {
        return operand1.CompareTo(operand2) <= 0;
    }
}