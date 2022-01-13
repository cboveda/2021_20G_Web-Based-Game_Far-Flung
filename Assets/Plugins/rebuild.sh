#!/bin/bash

rm libperlinfast.so 
gcc -Wall -fPIC -c PerlinFast.c
gcc -shared -o libperlinfast.so PerlinFast.o
rm PerlinFast.o