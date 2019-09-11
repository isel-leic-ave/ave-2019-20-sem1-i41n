class PontoApp {

    public static void main(String[] args)
    {
        Ponto p = new Ponto(5, 7);
        p.print();
        System.out.printf("p._x = %d\n", p._x);
        System.out.printf("p._y = %d\n", p._y);
    }
}