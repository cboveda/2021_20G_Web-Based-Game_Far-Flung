#include <stdio.h>

extern void TestPrintFromC() {
    printf("What happens here?\n");
}

extern int GetNumberFromC() {
    return 2339;
}

static int perlin_octaves = 5;
static float lacunarity = 2.0;
static float persistance = 0.4;

extern float GetTerrainHeightsFast( ) {}

extern float PerlinCalculatorFast() {

    float amplitude = persistance;
    float frequency = lacunarity;
    float height = 0;


}

