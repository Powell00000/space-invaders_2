# space-invaders_2

Follow up on [space-invaders](https://github.com/Powell00000/space-invaders) project.

What is new:
 - shields
 - sounds
 - special enemy ship
 - local leaderboards (load/save)
 - enemies now shoot only, if there is no other enemy in front of them (<code>Raycast2D</code>)
 - enemies distributed evenly on grid (just properly setup <code>GridArea</code>)
 - every row has different type of enemy (<code>OverrideEnemyStats</code>)
 - player gets additional points at the end of the game (more the longer he played)
 - <code>WaveManagerBase</code>
 - <code>EnemySpawnerBase</code>
  
 FAQ
 1. Why enemies cast for obstruction, instead of using some grid array and neighbours?  
   I did not want to create a new data structure, but to add new features without breaking old ones. Casting used is non-alloc and checks only for one object, with project of this scope, the performance of Raycast is negligible.
   
 2. Why local leaderboard and not POST/GET?  
    I ran out of time to create local database and check if it works, so I decided to just leave it undone.
    
 3. Why it is so ugly?  
    I am not an artist :<
    
 4. Sometimes you use pooling, and sometimes not. Why?  
   Pooling needs to be reworked, as creating new type of object requires new type of pool, lots of additional copy-pasted code. Sometimes, it was just faster to skip pooling, especially with objects, which are created once or twice during gameplay.
