namespace DefaultNamespace
{
    public class Tunnel : BoardComponent
    {
        public ushort PairID;

        public Tunnel(ushort identification, ushort pairID) : base(identification)
        {
            this.PairID = pairID;
        }
    }
}