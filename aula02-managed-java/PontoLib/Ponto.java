class Ponto {

	public int _y;
    public int _x;

	Ponto(int x, int y) {
        this._x = x;
        this._y = y;
    }

	public void print() {
        System.out.printf("Print V1 (x = %d, y = %d)\n", _x, _y);
    }
};