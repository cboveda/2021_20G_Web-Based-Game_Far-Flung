using System.Runtime.InteropServices;

public static class CImport {

    [DllImport("__Internal")]
    private static extern void TestPrintFromC();

    public static void DoTheTest() {

        TestPrintFromC();
    }
}