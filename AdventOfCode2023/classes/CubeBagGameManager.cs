using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace AdventOfCode2023 {
    
    internal class CubeBagGameManager {

        Collection<CubeBagGame> cubeBagGames = new Collection<CubeBagGame>();

        public CubeBagGameManager(string[] lines) {
   
            foreach (string line in lines) {
                this.cubeBagGames.Add(new CubeBagGame(line));
            }
        }

        public void print() {

            foreach (CubeBagGame game in this.cubeBagGames) {
                game.print();
            }
        }

        public int GetIdSumWithConstraint(CubeBagGameSet constraintSet) {

           int sum = 0;

            foreach(CubeBagGame game in this.cubeBagGames) {
                if (game.passConstraint(constraintSet))  {
                    sum += game.id;
                }
            }

            return sum;
        }

        public int GetMinimalSetPowerSum() {

            int sum = 0;

            foreach (CubeBagGame game in this.cubeBagGames)  {
                sum += game.MinimalSet().power;
            }

            return sum;
        }
    }
}
