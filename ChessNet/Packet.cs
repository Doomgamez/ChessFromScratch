using System.Drawing;
using System.IO;

namespace ChessNet
{
    public class Move
    {
        public Point From { get; set; }
        public Point To { get; set; }
        public Piece? promotion { get; set; }
    }

    public enum PacketType
    {
        Normal,
        Chat,
        Resign,
        Ping
    }

    public class Packet
    {
        public const ushort ProtocolVersion = 1;
        public uint timeelapsed { get; set; }
        public Move move { get; set; }
        public PacketType packettype { get; set; }
        public string message { get; set; }
        public HostType hosttype { get; set; } //client; host if client fakes being host game is dced
        public GameState gamestate { get; set; }

        public static Packet Read(BinaryReader reader)
        {
            return new Packet()
            {
                packettype = (PacketType)reader.ReadByte(),
                timeelapsed = reader.ReadUInt32(),
                move = reader.ReadBoolean() ? new Move()
                {
                    From = new Point(reader.ReadSByte(), reader.ReadSByte()),
                    To = new Point(reader.ReadSByte(), reader.ReadSByte()),
                    promotion = (Piece)reader.ReadByte(),
                } : null,
                message = reader.ReadString(),
                hosttype = (HostType)reader.ReadByte(),
                gamestate = (GameState)reader.ReadByte(),
            };
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write((byte)packettype);
            writer.Write(timeelapsed);
            writer.Write(move != null);
            if (move != null)
            {
                writer.Write((sbyte)move.From.X);
                writer.Write((sbyte)move.From.Y);
                writer.Write((sbyte)move.To.X);
                writer.Write((sbyte)move.To.Y);
                writer.Write((byte)move.promotion);
            }
            writer.Write(message);
            writer.Write((byte)hosttype);
            writer.Write((byte)gamestate);
        }
    }
}
