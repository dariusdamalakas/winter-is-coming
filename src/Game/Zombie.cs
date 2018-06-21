namespace WinterIsComing.Server
{
    class Zombie : IGameObject
    {
        public Zombie(string name, int x, int y)
        {
            this.Name = name;
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public string Name { get; set; }

        public void MoveRandom()
        {
            this.X += 0.Random(3) - 1;
            if (X < 0)
                X = 0;
            if (X > 10)
                X = 10;

            this.Y += 0.Random(2);
            if (Y > 30)
                Y = 30;
        }
    }
}