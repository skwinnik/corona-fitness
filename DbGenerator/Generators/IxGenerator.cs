using System;
using System.Threading.Tasks;


//todo figure out how to execute generators depending on "After"
//then remove Priority
namespace DbGenerator.Generators
{
    public interface IxGenerator
    {
        int Priority { get; }
        Type After { get; }
        Task Generate();
    }
}