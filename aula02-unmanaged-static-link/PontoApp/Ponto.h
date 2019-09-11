// This class is exported from the Ponto.lib

class Ponto {

public:
	int _z, _x, _y;
	Ponto(int x, int y);
    Ponto(int x, int y, int z);
	double Ponto::getModule();
	void Ponto::print();
};