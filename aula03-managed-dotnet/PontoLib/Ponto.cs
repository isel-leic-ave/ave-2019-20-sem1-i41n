using System;

public class Ponto {

	// public int _y;
    public int _x;

	public Ponto(int x, int y) {
        this._x = x;
        // this._y = y;
    }

	public void print() {
        Console.WriteLine("Print V1 (x = {0}, y = {1})\n", _x, 999);
    }
};