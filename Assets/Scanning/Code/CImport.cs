using System.Runtime.InteropServices;

public static class CImport {

    [DllImport("libperlinfast.so")]
    public static extern void TestPrintFromC();

    [DllImport("libperlinfast.so")]
    public static extern int GetNumberFromC();
}