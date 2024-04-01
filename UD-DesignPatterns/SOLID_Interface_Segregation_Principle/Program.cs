using static System.Console;

namespace SOLID_Interface_Segregation_Principle
{
    /// <summar>
    /// No code should be forced to depend on methods it does not use
    /// </summary>

    public class Document { }
    #region Bad Implementation
    /*
    public interface IMachine
    {
        void Print(Document d);
        void Scan(Document d);
        void Fax(Document d);
    }

    // Has all the IMachine functions implemented
    public class MultiFuncionPrinter : IMachine
    {
        public void Fax(Document d)
        {
            //
        }

        public void Print(Document d)
        {
            //
        }

        public void Scan(Document d)
        {
            //
        }
    }

    // Does not have an implementation for Fax or Scan, only print
    public class OldFashionedPrinter : IMachine
    {
        public void Fax(Document d)
        {
            throw new NotImplementedException();
        }

        public void Print(Document d)
        {
            //
        }

        public void Scan(Document d)
        {
            throw new NotImplementedException();
        }
    }
    */
    #endregion

    #region Principle Applied
    public interface IPrinter
    {
        void Print(Document document);
    }
    
    public interface IScanner
    {
        void Scan(Document document);
    }

    public interface IMultiFunctionDevice : IScanner, IPrinter //....
    {

    }

    public class Photocopier : IMultiFunctionDevice
    {
        public void Print(Document document)
        {
            //
        }

        public void Scan(Document document)
        {
            //
        }
    }

    // OR

    public class MultiFunctionMachine: IMultiFunctionDevice
    {
        private IPrinter printer;
        private IScanner scanner;

        public MultiFunctionMachine(IPrinter printer, IScanner scanner)
        {
            this.printer = printer ?? throw new ArgumentNullException(nameof(printer));
            this.scanner = scanner ?? throw new ArgumentNullException(nameof(scanner));
        }

        // Decorator Pattern
        public void Print(Document document)
        {
            printer.Print(document);
        }

        public void Scan(Document document)
        {
            scanner.Scan(document);
        }
    }

    #endregion
    internal class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Hello, World!");
        }
    }
}
