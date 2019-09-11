// PontoApp.cpp : Defines the entry point for the console application.
//

#include "Ponto.h"
#include <iostream>
#include <stdio.h>

int main(int argc, char * argv[])
{
	Ponto* p = new Ponto(5, 7);
	p->print();
	printf("p._x = %d\n", p->_x);
	printf("p._y = %d\n", p->_y);
	free(p);
	return 0;
}

