using AdventOfCode2023.classes;


namespace AdventOfCode2023 {
    internal class CityBlockCoordinate {

        public CityBlock cityBlock;
        public CityBlockFacing facing;

        public CityBlockCoordinate(CityBlock cityBlock, CityBlockFacing facing) {
            
            this.cityBlock = cityBlock;
            this.facing = facing;
        }

        public int x {  get {  return this.cityBlock.x; } }
        public int y {  get { return this.cityBlock.y; } }
    }
}
