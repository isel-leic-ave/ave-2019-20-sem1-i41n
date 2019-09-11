// Ponto.cpp : Defines the exported functions by Ponto.obj
//

#include "Ponto.h"
#include <math.h>
#include <iostream>

/*
 * This is the constructor of a class that has been exported.
 * see aula01_Ponto.h for the class definition
 */
Ponto::Ponto(int x, int y)
{
	this->_x = x;
	this->_y = y;
    this->_z = 99;
}

Ponto::Ponto(int x, int y, int z)
{
	this->_x = x;
	this->_y = y;
    this->_z = z;
}


double Ponto::getModule() {	 
	return sqrt((double)_x*_x + _y*_y);
}

void Ponto::print(){
	printf("Print V3 super master (x = %d, y = %d)\n", this->_x, _y);
}
