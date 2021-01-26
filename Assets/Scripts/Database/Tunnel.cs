namespace DefaultNamespace
{

    /// <summary>
    /// 
    /// @author
    /// </summary>
    public class Tunnel : BoardComponent
    {
        /// <summary>
        /// 
        /// </summary>
        public ushort PairID;

        /// <summary>
        /// 
        /// @author
        /// </summary>
        /// <param name="identification"></param>
        /// <param name="pairID"></param>
        public Tunnel(ushort identification, ushort pairID) : base(identification)
        {
            this.PairID = pairID;
        }
    }
}