#include <stdio.h>

extern void TestPrintFromC() {
    printf("What happens here?\n");
}

extern int GetNumberFromC() {
    return 2339;
}