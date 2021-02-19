namespace GameSet
{
    public struct Coord
    {
        public int X;
        public int Y;
    
        public Coord TopLeft => new Coord(X-1, Y-1);
        public Coord BottomLeft => new Coord(X+1, Y-1);
        public Coord TopRight => new Coord(X-1, Y+1);
        public Coord BottomRight => new Coord(X+1, Y+1);

        public Coord JumpTopLeft => new Coord(X-2, Y-2);
        public Coord JumpBottomLeft => new Coord(X+2, Y-2);
        public Coord JumpTopRight => new Coord(X-2, Y+2);
        public Coord JumpBottomRight => new Coord(X+2, Y+2);

        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
