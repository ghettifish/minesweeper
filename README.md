[ + = = = = ]
[ 1 1 1 = 1 ]
[ 1 + 1 = + ]
[ 1 1 1 = 1 ]
[ = = = = = ]




[ # * * * * ]
[ * * * * * ]
[ * * * * * ]
[ * * * * * ]
[ * * * * * ]
MINE is here: [0,0]
Adjacent ptrs: [[0,1],[2,1],[2,2]]


[ 0 0 1 * * ]
[ 0 0 * * * ]
[ 0 0 1 # * ]
[ 0 1 1 * * ]
[ * * * * * ]
MINE is here: [2,3]
Adjacent ptrs: [[1,2],[1,3],[1,4]]

When a user digs a zero:
* All surrounding elements are passed into the function as long as they are not revealed
* If the revealed tile is a 0 it starts the function again on that tile.


[ # * * * * ]
[ * * * * * ]
[ * * * # * ]
[ * * * * * ]
[ * * * * * ]
MINE is here: [2,3]
Adjacent ptrs: [[1,2],[1,3],[1,4]]

So it has to be less than or greater than both indexes but no less than the bottom of the array
and no greater than the top of the array.



[ * * * * * ]
[ * * * * * ]
[ * * * * * ]
[ * * * * * ]
[ * * * * * ]

[
  [
    {
      TileState: HIDDEN,
      Number: 1,
      IsMine: false;
    },
    {
      
    }
  ]
]