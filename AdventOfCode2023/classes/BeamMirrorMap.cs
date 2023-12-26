using System;

namespace AdventOfCode2023 {

    public enum BeamDirection {

        Up = 0,
        Down,
        Left,
        Right
    }

    public class BeamMirrorMap {

        static int _nextBeamId = 1;
        public static int nextBeamId {

            get {
                return _nextBeamId++;
            }
        }

        public BeamMirrorTracking[,] mapEnergized;
        public char[,] map;
        int height;
        int width;

        public BeamMirrorMap(string[] lines) {

            this.height = lines.Length;
            this.width = lines[0].Length;
            this.map = new char[width, height];
            this.mapEnergized = new BeamMirrorTracking[width, height];

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    this.map[x, y] = lines[y][x];
                    this.mapEnergized[x, y] = new BeamMirrorTracking();
                }
            }
        }

        public void print() {

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    Console.Write(this.map[x, y]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    if (this.mapEnergized[x, y].count > 0) {
                        Console.Write('X');
                    }
                    else {
                        Console.Write('.');
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public bool IsLegalCell(int x, int y) {

            return (x >= 0 && x < width && y >= 0 && y < height) ;
        }

        public bool IsLoopingMarkIfNot(Beam beam) {

            // energize this tile.
            this.mapEnergized[beam.x, beam.y].count += 1;

            switch (beam.direction) {

                case BeamDirection.Up:

                    if (this.mapEnergized[beam.x, beam.y].wentUp) return true ;
                    this.mapEnergized[beam.x, beam.y].wentUp = true;
                    return false;

                case BeamDirection.Down:

                    if (this.mapEnergized[beam.x, beam.y].wentDown) return true;
                    this.mapEnergized[beam.x, beam.y].wentDown = true;
                    return false;

                case BeamDirection.Left:

                    if (this.mapEnergized[beam.x, beam.y].wentLeft) return true;
                    this.mapEnergized[beam.x, beam.y].wentLeft = true;
                    return false;

                case BeamDirection.Right:

                    if (this.mapEnergized[beam.x, beam.y].wentRight) return true;
                    this.mapEnergized[beam.x, beam.y].wentRight = true;
                    return false;
            }

            throw new Exception();
        }


        public void ProcessBeam(Beam beam) {

            int loopCount = 0;

            while (true) {

                loopCount++;

                // If i left the map, kill me.
                if (!IsLegalCell(beam.x, beam.y)) {
                    beam.print("terminate");
                    return;
                }
             
                // Have I been here before?
                if (this.IsLoopingMarkIfNot(beam)) {
                    beam.print("looping");
                    return;
                }

                // Let's start moving through the map.
                char currentChar = this.map[beam.x, beam.y];
                switch (currentChar) {

                    case '.':

                        // do nothing.  Later, at the end, we'll move to the next cell.
                        break;

                    case '-':

                        // split the beam.  The beam will go left, but fork a child to go right.
                        if (beam.direction == BeamDirection.Up || beam.direction == BeamDirection.Down) {

                            beam.direction = BeamDirection.Left;

                            Beam newBeam = new Beam(beam.x + 1, beam.y, BeamDirection.Right, beam);
                            this.ProcessBeam(newBeam);
                        }

                        break;

                    case '|':

                        // split the beam.  The beam will go up, but fork a child to go down.
                        if (beam.direction == BeamDirection.Left || beam.direction == BeamDirection.Right) {

                            beam.direction = BeamDirection.Up;

                            Beam newBeam = new Beam(beam.x, beam.y + 1, BeamDirection.Down, beam);
                            this.ProcessBeam(newBeam);
                        }

                        break;

                    case '/':

                        switch (beam.direction) {

                            case BeamDirection.Up:
                         
                                beam.direction = BeamDirection.Right;
                                break;

                            case BeamDirection.Down:
 
                                beam.direction = BeamDirection.Left;
                                break;

                            case BeamDirection.Left:
                             
                                beam.direction = BeamDirection.Down;
                                break;

                            case BeamDirection.Right:

                                beam.direction = BeamDirection.Up;
                                break;

                            default:
                                throw new Exception();

                        }
                        break;

                    case '\\':

                        switch (beam.direction) {

                            case BeamDirection.Up:

                                beam.direction = BeamDirection.Left;
                                break;

                            case BeamDirection.Down:

                                beam.direction = BeamDirection.Right;
                                break;

                            case BeamDirection.Left:

                                beam.direction = BeamDirection.Up;
                                break;

                            case BeamDirection.Right:

                                beam.direction = BeamDirection.Down;
                                break;

                            default:
                                throw new Exception();
                        }

                        break;

                    default:
                        throw new Exception();
                }

                // Move the light forward.
                switch (beam.direction) {
                    case BeamDirection.Up:
                        beam.y--;
                        break;
                    case BeamDirection.Down:
                        beam.y++;
                        break;
                    case BeamDirection.Left:
                        beam.x--;
                        break;
                    case BeamDirection.Right:
                        beam.x++;
                        break;
                    default:
                        throw new Exception();
                }
            }
        }

        public long SumOfEnergizedMirrors() {

            long sum = 0;

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {

                    if (this.mapEnergized[x, y].count > 0) {
                        sum++;
                    };
                }
            }

            return sum;
        }
    }
}

