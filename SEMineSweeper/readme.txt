1 The Test

Create a console application that runs a minefield/minesweeper style game, in which the player navigates from one side of a chessboard grid to the other whilst 
trying to avoid hidden mines. The player has a number of lives, losing one each time a mine is hit, and the final score is the number of moves taken in order 
to reach the other side of the board. The console interface should be simple, allowing the player to input move direction (up, down, left, right) and the game 
to show the resulting position (e.g. C2 in chess board terminology) along with number of lives left and number of moves taken.

· You should take effort to demonstrate clean coding and use of recognized design principles in your solution

· Implement appropriate automated unit testing

When complete, upload your code to a public GitHub repository and forward the URL to us.

Be prepared to talk through you code and explain key design features and coding principles and why you have used them.

Good luck

1) As a user, I need to select a start position
2) I need to input a direction, l/r/u/d and get an updated game status of lives left and current position on the board
3) If I input a direction to move out of the grid bounds, I should get an invalid move message
4) If I hit a mine I should lose a life
5) If I lose all lives, the game ends
6) If I cross the board, I win

Notes
1) I haven't used dependency injection, but the Game object is ofcourse set up to have IMineFactory injected.
2) Have worked on the assumption that alpha characters define columns and numbers define rows and thus a max grid size of 26 is supported. 