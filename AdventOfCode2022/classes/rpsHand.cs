using System;

namespace AdventOfCode2022 {

    internal class RpsHand {

        public char choice1;
        public char choice2;

        public RpsHand(string line) {

            string[] hand = line.Split(" ");
            choice1 = hand[0][0];
            choice2 = hand[1][0];
        }

        public void print() {
            Console.WriteLine("choice 1: " + choice1 + ", choice 2: " + choice2 + ", score: " + this.score() + 
                ", score2: " + this.score2());
        }

        public int choiceScore(char choice) {

            int x;

            switch (choice) {

                case 'X':
                    x = 1;
                    break;
                case 'Y':
                    x = 2;
                    break;
                case 'Z':
                    x = 3;
                    break;
                default:
                    throw new Exception();
            }

            return x;
        }


        public int score() {

            int resultScore = 0;

            if ((choice2 == 'X') && (choice1 == 'A')) resultScore = 3; // me rock, you rock.  Tie.
            if ((choice2 == 'X') && (choice1 == 'B')) resultScore = 0; // me rock, you paper;  Loss.
            if ((choice2 == 'X') && (choice1 == 'C')) resultScore = 6; // me rock, you scissors.  Win.
            if ((choice2 == 'Y') && (choice1 == 'A')) resultScore = 6; // me paper, you rock;  Win.
            if ((choice2 == 'Y') && (choice1 == 'B')) resultScore = 3; // me paper, you paper.  Tie.
            if ((choice2 == 'Y') && (choice1 == 'C')) resultScore = 0; // me paper, you scissors.  Loss.
            if ((choice2 == 'Z') && (choice1 == 'A')) resultScore = 0; // me scissors, you rock;  Loss.
            if ((choice2 == 'Z') && (choice1 == 'B')) resultScore = 6; // me scissors, you Paper.  Win.
            if ((choice2 == 'Z') && (choice1 == 'C')) resultScore = 3; // me scissors, you scissors.  Tie.

            return (this.choiceScore(this.choice2) + resultScore);
        }

        public int score2() {

            char choiceThem = this.choice1;
            char result = this.choice2;
            char choiceMe = 'Q';

            if (result == 'X') { // lose
                if (choiceThem == 'A') choiceMe = 'Z';
                if (choiceThem == 'B') choiceMe = 'X';
                if (choiceThem == 'C') choiceMe = 'Y';
            }
            else if (result == 'Y') {  // tie
                if (choiceThem == 'A') choiceMe = 'X';
                if (choiceThem == 'B') choiceMe = 'Y';
                if (choiceThem == 'C') choiceMe = 'Z';
            }
            else if (result == 'Z') { // win
                if (choiceThem == 'A') choiceMe = 'Y';
                if (choiceThem == 'B') choiceMe = 'Z';
                if (choiceThem == 'C') choiceMe = 'X';
            }
            else {
                throw new Exception();
            }

            int resultScore;
            switch (result) {
                case 'X':
                    resultScore = 0;
                    break;
                case 'Y':
                    resultScore = 3;
                    break;
                case 'Z':
                    resultScore = 6;
                    break;
                default:
                    throw new Exception();
            }

            return (this.choiceScore(choiceMe) + resultScore);
        }
    }
}
