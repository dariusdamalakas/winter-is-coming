# Winter is Coming game ##

## Compiling and run
Use VisualStudio to compile and run

**p.s. Sorry, no support for .Net Core yet!**

## Connecting to server
Use any websocket CLI tools. Hashrocket has a small utility `ws`
```
go get -u github.com/hashrocket/ws
```

Once installed, connect to server with
```
ws ws://localhost:6002
```

The server will not announce for new players.

## Joining a game
There are 4 commands:
 * START - join an existing or start new game
 * SHOOT - shoot zombie 
 * QUIT - quit current game
 * STATS - list all players states in the game

### START
To start a game issue `START` command with players name and board name.
Only 1 game can be active for a player at a time.  Multiple players can join same game by specifying same board name
```
START <player_name> <board_name>
START john_snow arena1
```

### SHOOT
Shoot a zombie and earn points.  If zombie is shot, a new one will spawn after 10 seconds. Each killed zombie gives 1 point.  If zombie reaches wall, all players 

```
SHOOT 4 2
```

if zombie is hit, server will respond with total number of points for current player
``` 
BOOM <player_name> <score> <zombie-name>
BOOM john_snow 2 night-walker
```

### STATS
This will print all players active on current board

```
STATS

# output:
< All player stats:
< Player (1): other_player, Score: 0
< Player (2): john_snow, Score: 1 << you
```

### QUIT
You can leave current game by quitting. Note that the game will continue to run, and you can re-join it later

```
QUIT

# response:
> < You have left game: arena2
```
