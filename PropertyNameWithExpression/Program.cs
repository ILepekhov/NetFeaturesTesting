using System.Linq.Expressions;

string GetMemberName<TSource, TMember>(TSource source, Expression<Func<TSource, TMember>> expression)
{
    if (expression is null) throw new ArgumentNullException(nameof(expression));

    return GetMemberNamePrivate(expression.Body);
}

string GetMemberNamePrivate(Expression expression)
{
    if (expression is null) throw new ArgumentNullException(nameof(expression));

    if (expression is MemberExpression memberExpression)
        return memberExpression.Member.Name;

    if (expression is MethodCallExpression methodCallExpression)
        return methodCallExpression.Method.Name;

    if (expression is UnaryExpression unaryExpression)
        return GetMemberNamePrivate(unaryExpression.Operand);

    throw new ArgumentException();
}

var instance = new SomeDummyClass();

Console.WriteLine(GetMemberName(instance, x => x.Property1));
Console.WriteLine(GetMemberName(instance, x => x.Property2));


class SomeDummyClass
{
    public int Property1 { get; set; }

    public int Property2 { get; set; }
}
