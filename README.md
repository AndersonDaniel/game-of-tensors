# game-of-tensors
Game of Tensors repo for the #PoweredByTF2 Challenge. Contains game server, game client (in Unity), mobile client UI, and the code for model training (which includes the dataset).

## Game server
Very simple Flask server which waits for a socket connection (desktop UI) and then serves endpoints for the mobile clients to push action notifications. It then forwards the actions to the desktop UI.

## Model training code
