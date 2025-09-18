using Attribute.Attributes;
using System.Reflection;

namespace Attribute.Entities;

public class Printer
{
    public Printer(object obj)
    {
        var membersToPrint = obj.GetType()
            .GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(m => m is PropertyInfo || m is FieldInfo)
            .ToList();

        foreach (var member in membersToPrint)
        {
            PrintMember(member, obj);
        }
    }

    private static void PrintMember(MemberInfo member, object obj)
    {
        var value = (member switch
        {
            PropertyInfo prop => prop.GetValue(obj),
            FieldInfo field => field.GetValue(obj),
        }).ToString();


        ConsoleColor? foregroundColor = null;
        var foregroundAttrData = member.CustomAttributes
            .FirstOrDefault(attr => attr.AttributeType == typeof(ForegroundColorAttribute));
        if (foregroundAttrData != null && foregroundAttrData.ConstructorArguments.Any())
        {
            foregroundColor = (ConsoleColor)foregroundAttrData.ConstructorArguments[0].Value;
        }

        ConsoleColor? backgroundColor = null;
        var backgroundAttrData = member.CustomAttributes
            .FirstOrDefault(attr => attr.AttributeType == typeof(BackgroundColorAttribute));
        if (backgroundAttrData != null && backgroundAttrData.ConstructorArguments.Any())
        {
            backgroundColor = (ConsoleColor)backgroundAttrData.ConstructorArguments[0].Value;
        }

        bool? choosenCase = null;
        var caseAttrData = member.CustomAttributes
            .FirstOrDefault(attr => attr.AttributeType == typeof(CaseAttribute));
        if (caseAttrData != null && caseAttrData.ConstructorArguments.Any())
        {
            choosenCase = (bool)caseAttrData.ConstructorArguments[0].Value;
        }

        int? paddingSize = null;
        PaddingSide? side = null;
        var paddingAttrData = member.CustomAttributes
            .FirstOrDefault(attr => attr.AttributeType == typeof(PaddingAttribute));
        if (paddingAttrData != null && paddingAttrData.ConstructorArguments.Count >= 2)
        {
            paddingSize = (int)paddingAttrData.ConstructorArguments[0].Value;
            side = (PaddingSide)paddingAttrData.ConstructorArguments[1].Value;
        }




        if (foregroundColor.HasValue)
            Console.ForegroundColor = foregroundColor.Value;

        if (backgroundColor.HasValue)
            Console.BackgroundColor = backgroundColor.Value;

        if (choosenCase.HasValue)
            value = choosenCase.Value ? value.ToUpper() : value.ToLower();

        if (paddingSize.HasValue)
            value = side == PaddingSide.Left ? value.PadLeft(value.Length + paddingSize.Value) : value.PadRight(value.Length + paddingSize.Value);

        Console.WriteLine($"{member.Name}: {value}");
        Console.ResetColor();
    }


}